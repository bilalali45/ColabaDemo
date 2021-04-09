using System;
using System.Linq;
using System.Threading.Tasks;
using LosIntegration.API.Enums;
using LosIntegration.Model.Model.ServiceRequestModels.RainMaker;
using LosIntegration.Model.Model.ServiceResponseModels;
using LosIntegration.Service.InternalServices;
using LosIntegration.Service.InternalServices.Rainmaker;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog;
using Extensions.ExtensionClasses;
using Elasticsearch.Net;
using LosIntegration.Model.Model.ServiceResponseModels.ByteWebConnector;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LosIntegration.API.Controllers
{
    [Route(template: "api/LosIntegration/[controller]")]
    [ApiController]
    [Authorize(Roles = "MCU")]
    public class LoanApplicationController : BaseApiController<LoanApplicationController>
    {
        #region Constructor

        public LoanApplicationController(
                                         ILogger<LoanApplicationController> logger,
                                         ILoanApplicationService loanApplicationService,
                                         IByteWebConnectorService byteWebConnectorService,
                                         IMilestoneService milestoneService,
                                         ILoanRequestService loanRequestService,
                                         IThirdPartyCodeService thirdPartyCodeService) : base(logger)
        {
            _logger = logger;
            _loanApplicationService = loanApplicationService;
            _byteWebConnectorService = byteWebConnectorService;
            _milestoneService = milestoneService;
            _loanRequestService = loanRequestService;
            _thirdPartyCodeService = thirdPartyCodeService;
        }

        #endregion

        #region Action Methods 

        [Route(template: "[action]")]
        [HttpGet]
        public async Task<IActionResult> UpdateLoanStatusFromByte([FromQuery] int loanApplicationId)
        {
            try
            {
                var loanApplicationDetail = _loanApplicationService.GetLoanApplicationWithDetails(loanApplicationId: loanApplicationId);
                if (loanApplicationDetail == null)
                {


                    return NotFound(new
                    {
                        Message = "Unable to find loanApplicationId=" + loanApplicationId.ToString()
                    });
                }
                else
                {
                    var statusNameResponse = await this._byteWebConnectorService.GetByteLoanStatusNameViaSDK(loanApplicationDetail.ByteFileName);
                    if (statusNameResponse == null)
                    {
                        return NotFound(new
                        {
                            Message = "Cannot retrieve loan status response."
                        }) ;
                        //throw new Exception("Cannot retrieve loan status response.");
                    }
                    else
                    {
                        if (statusNameResponse.ResponseObject.Status == ApiResponseStatus.Success)
                        {
                            var milestoneMappedStatus = await this._milestoneService.GetMappingAll(tenantId: 1,
                                                                                             losId: 1);
                            if (milestoneMappedStatus == null || milestoneMappedStatus.Count <= 0)
                            {
                                base._logger.LogWarning($"Could not retrieve milestone list from milestone api : {statusNameResponse.ResponseObject.Status}");
                                return BadRequest(new
                                                  {
                                                      Message = "Milestone list is not configured."
                                                  });
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(statusNameResponse.ResponseObject.Data))
                                {
                                    var milestone = milestoneMappedStatus.Where(ms => ms.Name == statusNameResponse.ResponseObject.Data)
                                                                         .FirstOrDefault();
                                    if (milestone == null)
                                    {
                                        base._logger.LogWarning($"Cannot find matching status name in milestone list. BytePro Status Name {statusNameResponse.ResponseObject.Data}");
                                        return BadRequest(new
                                                          {
                                                              Message = "Milestone is not configured.".ToString()
                                                          });
                                    }
                                    else
                                    {
                                        var updateRespose = await this.UpdateRainmakerLoanStatus(loanApplicationId, loanApplicationDetail.ByteFileName, (short)milestone.StatusId);
                                        return Ok(updateRespose);
                                    }
                                }
                                else
                                {
                                    base._logger.LogWarning($"Loan status is not set in BytePro : {statusNameResponse.ResponseObject.Status}");
                                    return BadRequest(new
                                                      {
                                                          Message = "Loan status is not set in external originator."
                                                      });
                                }
                            }
                        }
                        else
                        {
                            base._logger.LogWarning($"Loan status return from LOS is not valid. Loan Status : {statusNameResponse.ResponseObject.Status}");
                            return BadRequest(new
                                              {
                                                  Message = "Unable to find loanApplicationId=" + loanApplicationId.ToString()
                                              });
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = "Internal Server Error"
                });
            }
        }


        private async Task<IActionResult> UpdateRainmakerLoanStatus(int loanApplicationId, string byteFileName, short losStatusId)
        {
            return await this._milestoneService.SyncRainmakerLoanStatusFromByte(base.TenantId, loanApplicationId, losStatusId, byteFileName);
        }

        [Route(template: "[action]")]
        [HttpPost]
        public async Task<ApiResponse<LoanFileInfo>> SendLoanApplicationToExternalOriginator([FromQuery] int loanApplicationId)
        {

            var loanApplication = _loanApplicationService.GetLoanApplicationWithDetails(loanApplicationId: loanApplicationId,
                                                                                        includes:
                                                                                                  LoanApplicationService.RelatedEntities.Borrower_EmploymentInfoes_AddressInfo |
                                                                                                  LoanApplicationService.RelatedEntities.Borrower_EmploymentInfoes_OtherEmploymentIncomes |
                                                                                                  LoanApplicationService.RelatedEntities.Borrower_BorrowerResidences_LoanAddress |
                                                                                                  LoanApplicationService.RelatedEntities.Borrower_BorrowerAccount_AccountType |
                                                                                                  LoanApplicationService.RelatedEntities.Borrower_LoanContact_Ethnicity |
                                                                                                  LoanApplicationService.RelatedEntities.Borrower_LoanContact_Race |
                                                                                                  LoanApplicationService.RelatedEntities.Borrower_LoanContact_Gender |
                                                                                                  LoanApplicationService.RelatedEntities.Borrower_OtherIncomes_IncomeType |
                                                                                                  LoanApplicationService.RelatedEntities.Borrower_Liability |
                                                                                                  LoanApplicationService.RelatedEntities.Opportunity_Employee_UserProfile |
                                                                                                  LoanApplicationService.RelatedEntities.Borrower_PropertyInfo_AddressInfo |
                                                                                                  LoanApplicationService.RelatedEntities.PropertyInfo_AddressInfo |
                                                                                                  LoanApplicationService.RelatedEntities.PropertyInfo_MortgageOnProperties |
                                                                                                  LoanApplicationService.RelatedEntities.Opportunity_Employee_Contact |
                                                                                                  LoanApplicationService.RelatedEntities.Opportunity_Employee_CompanyPhoneInfo |
                                                                                                  LoanApplicationService.RelatedEntities.Opportunity_Employee_EmailAccount |
                                                                                                  LoanApplicationService.RelatedEntities.BusinessUnit_LeadSource |
                                                                                                  LoanApplicationService.RelatedEntities.LoanGoal |
                                                                                                  LoanApplicationService.RelatedEntities.PropertyInfo_PropertyTaxEscrows |
                                                                                                  LoanApplicationService.RelatedEntities.Borrower_BorrowerQuestionResponses_Question |
                                                                                                  LoanApplicationService.RelatedEntities.Borrower_BorrowerQuestionResponses_QuestionResponse


                                                                                                  );
            _logger.LogInformation($"Loan applicaiton ID : {loanApplication.Id} Byte Organization Code : {loanApplication.BusinessUnit?.ByteOrganizationCode}");
            var loanRequest = _loanRequestService.GetLoanRequestWithDetails(loanApplicationId: loanApplicationId,
                                                                                       null);
            var byteProCodeList = _thirdPartyCodeService.GetRefIdByThirdPartyId((int)ThirdParty.BytePro);

            var sdkResponse = await _byteWebConnectorService.SendLoanApplicationViaSDK(loanApplication, byteProCodeList);

            //var sendLoanApplicationCallResponse = await _byteWebConnectorService.SendLoanApplication(loanApplication: loanApplication,
            //                                                                             loanRequest: loanRequest, byteProCodeList: byteProCodeList);
            var apiResponse = sdkResponse.ResponseObject;
            //apiResponse = sdkResponse.ResponseObject;
            if (apiResponse.Data != null)
            {
                var byteResponseFile = apiResponse.Data;
                loanApplication.ByteLoanNumber = byteResponseFile.FileDataId.ToString();
                loanApplication.ByteFileName = byteResponseFile.FileName;
                loanApplication.BytePostDateUtc = DateTime.UtcNow;
                var updateLoanApplicationRequest = new UpdateLoanApplicationRequest()
                {
                    Id = loanApplication.Id,
                    BytePostDateUtc = DateTime.UtcNow,
                    ByteLoanNumber = byteResponseFile.FileDataId.ToString(),
                    ByteFileName = byteResponseFile.FileName,
                };
                _loanApplicationService.UpdateLoanApplication(updateLoanApplicationRequest);
            }

            return sdkResponse.ResponseObject;

        }

        #endregion

        #region Private Fields
        private readonly ILogger<LoanApplicationController> _logger;
        private readonly ILoanApplicationService _loanApplicationService;
        private readonly IByteWebConnectorService _byteWebConnectorService;
        private readonly IMilestoneService _milestoneService;
        private readonly ILoanRequestService _loanRequestService;
        private readonly IThirdPartyCodeService _thirdPartyCodeService;
        #endregion
    }
}