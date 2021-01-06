using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Rainmaker.Model;
using Rainmaker.Service;
using Rainmaker.Service.Helpers;
using RainMaker.Common;
using RainMaker.Common.Extensions;
using RainMaker.Entity.Models;
using RainMaker.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TrackableEntities.Common.Core;


namespace Rainmaker.API.Controllers
{
    [ApiController]
    [Route("api/RainMaker/[controller]")]
    public class LoanApplicationController : Controller
    {
        private readonly ILoanApplicationService loanApplicationService;
        private readonly ICommonService commonService;
        private readonly IFtpHelper ftp;
        private readonly IOpportunityService opportunityService;
        private readonly IActivityService activityService;
        private readonly IWorkQueueService workQueueService;
        private readonly IUserProfileService userProfileService;
        private readonly IEmployeeService _employeeService;
      
        private readonly ILogger<LoanApplication> _logger;
        public LoanApplicationController(ILoanApplicationService loanApplicationService, ICommonService commonService, IFtpHelper ftp, IOpportunityService opportunityService, IActivityService activityService, IWorkQueueService workQueueService, IUserProfileService userProfileService, IEmployeeService employeeService, ILogger<LoanApplication> logger)
        {
            this.loanApplicationService = loanApplicationService;
            this.commonService = commonService;
            this.ftp = ftp;
            this.opportunityService = opportunityService;
            this.activityService = activityService;
            this.workQueueService = workQueueService;
            this.userProfileService = userProfileService;
            this._employeeService = employeeService;
            this._logger = logger;
        }
        [Authorize(Roles = "Customer")]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetLoanInfo(int loanApplicationId)
        {
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());
            var loanApplication = await loanApplicationService.GetLoanSummary(loanApplicationId, userProfileId);
            return Ok(loanApplication);
        }
        [Authorize(Roles = "Customer")]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetLOInfo(int loanApplicationId)
        {
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());
            int businessUnitId = (await loanApplicationService.GetByLoanApplicationId(loanApplicationId)).BusinessUnitId.Value;
            Rainmaker.Model.LoanOfficer lo = await loanApplicationService.GetLOInfo(loanApplicationId, businessUnitId, userProfileId);
            if (lo == null || lo.FirstName == null)
            {
                lo = await loanApplicationService.GetDbaInfo(businessUnitId);
            }
            return Ok(lo);
        }
        [Authorize(Roles = "Customer")]
        [HttpPost("[action]")]
        public async Task<IActionResult> GetByLoanApplicationId([FromBody] GetLoanApplicationModel model)
        {
            return Ok(await loanApplicationService.GetByLoanApplicationId(model.loanApplicationId));
        }

        [Authorize(Roles = "MCU,Customer")]
        [HttpGet("[action]")]
        public IActionResult GetLoanApplication([FromQuery] string encompassNumber = null, [FromQuery] int? id = null)
        {
            var loanApplication = loanApplicationService.GetLoanApplicationWithDetails(id: id, encompassNumber: encompassNumber).SingleOrDefault();
            return Ok(loanApplication);
        }

        [Authorize(Roles = "MCU,Customer")]
        [HttpGet("[action]")]
        public IActionResult GetLoanApplicationForByte([FromQuery] string encompassNumber = null, [FromQuery] int? id = null, long? includes = null)
        {
            //var relatedEntities = (LoanApplicationService.RelatedEntities?)includes;


            var relatedEntities = LoanApplicationService.RelatedEntities.Borrower_EmploymentInfoes_AddressInfo |
                                  LoanApplicationService.RelatedEntities.Borrower_EmploymentInfoes_OtherEmploymentIncomes |
                                  LoanApplicationService.RelatedEntities.Borrower_BorrowerResidences_LoanAddress |
                                  LoanApplicationService.RelatedEntities.Borrower_BorrowerAccount_AccountType |
                                  LoanApplicationService.RelatedEntities.Borrower_OtherIncomes_IncomeType |
                                  LoanApplicationService.RelatedEntities.Borrower_BorrowerAssets |
                                  LoanApplicationService.RelatedEntities.Borrower_LoanContact_Ethnicity;




            var loanApplication = loanApplicationService.GetLoanApplicationWithDetails(id: id, encompassNumber: encompassNumber, includes: relatedEntities).SingleOrDefault();


            relatedEntities = LoanApplicationService.RelatedEntities.Borrower_LoanContact_Race |
                              LoanApplicationService.RelatedEntities.Borrower_LoanContact_Gender |
                              LoanApplicationService.RelatedEntities.Borrower_Liability |
                              LoanApplicationService.RelatedEntities.Borrower_PropertyInfo_AddressInfo |
                              LoanApplicationService.RelatedEntities.Borrower_BorrowerQuestionResponses_Question |
                              LoanApplicationService.RelatedEntities.Borrower_BorrowerQuestionResponses_QuestionResponse;




            loanApplicationService.GetLoanApplicationWithDetails(id: id, encompassNumber: encompassNumber, includes: relatedEntities);


            relatedEntities = LoanApplicationService.RelatedEntities.Opportunity_Employee_CompanyPhoneInfo |
                              LoanApplicationService.RelatedEntities.Opportunity_Employee_EmailAccount |
                              LoanApplicationService.RelatedEntities.Opportunity_Employee_UserProfile |
                              LoanApplicationService.RelatedEntities.Opportunity_Employee_Contact;




            loanApplicationService.GetLoanApplicationWithDetails(id: id, encompassNumber: encompassNumber, includes: relatedEntities);

            relatedEntities = LoanApplicationService.RelatedEntities.PropertyInfo_PropertyTaxEscrows |
                              LoanApplicationService.RelatedEntities.PropertyInfo_AddressInfo |
                              LoanApplicationService.RelatedEntities.PropertyInfo_MortgageOnProperties |
                              LoanApplicationService.RelatedEntities.BusinessUnit_LeadSource |
                              LoanApplicationService.RelatedEntities.LoanGoal;





            loanApplicationService.GetLoanApplicationWithDetails(id: id, encompassNumber: encompassNumber, includes: relatedEntities);


            var config = AutoMapperConfig.GetConfiguration();
            var mapper = config.CreateMapper();
            var loanApplicationModel = mapper.Map<Model.ServiceResponseModels.Rainmaker.LoanApplication>(loanApplication);


            return Ok(loanApplicationModel);
        }


        [Authorize(Roles = "Customer")]
        [HttpGet("[action]")]
        public async Task<string> GetPhoto(string photo, int loanApplicationId)
        {
            int businessUnitId = (await loanApplicationService.GetByLoanApplicationId(loanApplicationId)).BusinessUnitId.Value;
            var remoteFilePath = await commonService.GetSettingValueByKeyAsync<string>(SystemSettingKeys.FtpEmployeePhotoFolder, businessUnitId) + "/" + photo;
            Stream imageData = null;
            try
            {
                imageData = await ftp.DownloadStream(remoteFilePath);
            }
            catch
            {
                // this exception can be ignored
            }
            if (imageData == null)
            {
                imageData = new FileStream("Content\\images\\default-LO.jpg", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            }
            using MemoryStream ms = new MemoryStream();
            imageData.CopyTo(ms);
            imageData.Close();
            return Convert.ToBase64String(ms.ToArray());
        }

        [Authorize(Roles = "MCU,Customer")]
        [HttpPost("[action]")]
        public async Task<IActionResult> PostLoanApplication([FromBody] PostLoanApplicationModel model)
        {
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());
            return Ok(await loanApplicationService.PostLoanApplication(model.loanApplicationId, model.isDraft, userProfileId, opportunityService));
        }

        [Authorize(Roles = "MCU")]
        [HttpPost("[action]")]
        public async Task<IActionResult> SendBorrowerEmail([FromBody] SendBorrowerEmailModel model)
        {
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());
            var loanApplication = await loanApplicationService.GetByLoanApplicationId(model.loanApplicationId);
            var activityEnumType = (ActivityForType)model.activityForId;

            var userProfile = await userProfileService.GetUserProfileEmployeeDetail(userProfileId, UserProfileService.RelatedEntities.Employees_EmployeeBusinessUnitEmails_EmailAccount);

            var data = new Dictionary<FillKey, string>();
            data.Add(FillKey.CustomEmailHeader, "");
            data.Add(FillKey.CustomEmailFooter, "");
            data.Add(FillKey.FromEmail, model.fromAddress);
            data.Add(FillKey.EmailSubject, model.subject);
            data.Add(FillKey.EmailBody, model.emailBody.Replace(Environment.NewLine, "<br/>"));

            if (userProfile != null)
                data.Add(FillKey.EmailTag, String.IsNullOrEmpty(userProfile.Employees.SingleOrDefault().EmailTag) ? String.Empty : userProfile.Employees.SingleOrDefault().EmailTag);
            await SendLoanApplicationActivityEmail(data, loanApplication.OpportunityId.ToInt(), loanApplication.LoanRequestId.ToInt(), loanApplication.BusinessUnitId.ToInt(), activityEnumType,model.toAddess,model.ccAddress);
            return Ok();
        }

        private async Task SendLoanApplicationActivityEmail(Dictionary<FillKey, string> data, int opportunityId, int loanRequestId, int businessUnitId, ActivityForType emailtype,string toAddess,string ccAddress)
        {
            var activity = await activityService.GetCustomerActivity(businessUnitId, emailtype);
            if (activity != null)
            {
                var rnd = new Random();

                var random = rnd.Next(100, 1000);

                var witem = new WorkQueue
                {
                    CampaignId = null,
                    ActivityId = activity.Id,
                    ActivityTypeId = activity.ActivityTypeId,
                    CreatedBy = 1,//todo: employee userid
                    CreatedOnUtc = DateTime.UtcNow,
                    EntityRefId = opportunityId,
                    EntityRefTypeId = Constants.GetEntityType(typeof(Opportunity)),
                    EntityTypeId = Constants.GetEntityType(typeof(WorkQueue)),
                    IsActive = true,
                    IsDeleted = false,
                    RandomNo = random,
                    LoanRequestId = loanRequestId,
                    Code = activity.Id.ToString(System.Globalization.CultureInfo.InvariantCulture).Encrypt(random.ToString(System.Globalization.CultureInfo.InvariantCulture)),
                    ScheduleDateUtc = DateTime.UtcNow,
                    IsCustom = false,
                    ToAddress = toAddess,
                    CcAddress = ccAddress

                };
                foreach (var key in data)
                {
                    var keySymbol = EmailTemplateKeys.GetKeySymbol(key.Key);

                    if (!string.IsNullOrEmpty(keySymbol))
                        witem.WorkQueueKeyValues.Add(new WorkQueueKeyValue { KeyName = keySymbol, Value = key.Value, TrackingState = TrackingState.Added });
                }
                workQueueService.Insert(witem);
                await workQueueService.SaveChangesAsync();
            }
            else
            {
                throw new RainMakerException("Activity not found for Business unit");
            }
        }
        
        [Authorize(Roles = "MCU,Customer")]
        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateLoanInfo([FromBody] UpdateLoanInfo model)
        {
            await loanApplicationService.UpdateLoanInfo(model);
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SendEmailSupportTeam([FromBody] SendEmailSupportTeam model)

        {
            try
            {

                _logger.LogInformation(message: $"DocSync SendEmailSupportTeam model {model.ToJson()}");
                var loanApplication = await loanApplicationService.GetByLoanApplicationId(model.loanApplicationId);
                StringBuilder email = new StringBuilder();
                string commaseperated = ",";

                _logger.LogInformation(message: $"DocSync SendEmailSupportTeam  BusinessUnitId {loanApplication.BusinessUnitId.ToString()}");
                var employee = await _employeeService.GetEmployeeEmailByRoleName(Constants.SupportTeamRoleName);
                for (int i = 0; i < employee.Count; i++)
                {
                    if (i == employee.Count - 1)
                    {
                        commaseperated = string.Empty;
                    }
                    if (employee[i].EmployeeBusinessUnitEmails != null && employee[i].EmployeeBusinessUnitEmails.Any())
                    {
                        var emailAccount = employee[i].EmployeeBusinessUnitEmails.Where(x => x.BusinessUnitId == null || x.BusinessUnitId == loanApplication.BusinessUnitId)
                            .OrderByDescending(x => x.BusinessUnitId).FirstOrDefault()?.EmailAccount;
                        if (emailAccount != null)
                        {
                            email.Append(emailAccount.Email + commaseperated);
                        }
                        else
                        {
                            email.Append(string.Empty);
                        }
                    }
                }

                _logger.LogInformation(message: $"DocSync SendEmailSupportTeam email {email.ToString()}");

                model.EmailBody = "<table style='width:100%'> <tr> <td> <h2 style='font-size: 14px; font-weight: bold; font-family: 'Rubik', Arial, Helvetica, sans-serif; color: #000000; text-transform: uppercase;'>Hi RainMaker support team,</h2> <p style='font-size: 13px; font-weight: normal; font-family: 'Rubik', Arial, Helvetica, sans-serif; color: #000000;margin-bottom: 25px;'>Documents that have failed to sync</p> <h2 style='font-size: 14px; font-weight: bold; font-family: 'Rubik', Arial, Helvetica, sans-serif; color: #000000; text-transform: uppercase;margin-bottom: 0;'>" + model.DocumentCategory + "</h2> <p style='font-size: 13px; font-weight: normal; font-family: 'Rubik', Arial, Helvetica, sans-serif; color: #000000;margin-bottom: 25px;margin-top:5px'>" + model.DocumentName + "." + model.DocumentExension.Replace("jpeg", "jpg") + "</p></td> </tr> </table> ";
                var data = new Dictionary<FillKey, string>();
                data.Add(FillKey.CustomEmailHeader, "");
                data.Add(FillKey.CustomEmailFooter, "");
                data.Add(FillKey.EmailBody, model.EmailBody.Replace(Environment.NewLine, "<br/>"));
                data.Add(FillKey.LoanApplicationId, model.loanApplicationId.ToString());
                data.Add(FillKey.TenantId, model.TenantId.ToString());
                data.Add(FillKey.ActivityDate, model.ErrorDate==null?string.Empty:CommonHelper.DateFormat(model.ErrorDate.ToString()));
                data.Add(FillKey.ErrorCode, ((int)HttpStatusCode.InternalServerError).ToString() + ":" + HttpStatusCode.InternalServerError.ToString());
                data.Add(FillKey.ErrorUrl, model.Url);
                await SendEmailSupportActivityEmail(data, loanApplication.OpportunityId.ToInt(), loanApplication.LoanRequestId.ToInt(), loanApplication.BusinessUnitId.ToInt(), ActivityForType.DocumentSyncFailureActivity, email.ToString());


                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(message: $"DocSync SendEmailSupportTeam Exception  {ex.Message}");
                throw new RainMakerException("Activity not found for Support email");
            }
        }

        private async Task SendEmailSupportActivityEmail(Dictionary<FillKey, string> data, int opportunityId, int loanRequestId, int businessUnitId, ActivityForType emailtype, string emailAddress)
        {

            var activity = await activityService.GetCustomerActivity(businessUnitId, emailtype);
            if (activity != null)
            {
                var rnd = new Random();

                var random = rnd.Next(100, 1000);

                var witem = new WorkQueue
                {
                    CampaignId = null,
                    ActivityId = activity.Id,
                    ActivityTypeId = activity.ActivityTypeId,
                    CreatedBy = 1,//todo: employee userid
                    CreatedOnUtc = DateTime.UtcNow,
                    EntityRefId = opportunityId,
                    EntityRefTypeId = Constants.GetEntityType(typeof(Opportunity)),
                    EntityTypeId = Constants.GetEntityType(typeof(WorkQueue)),
                    IsActive = true,
                    IsDeleted = false,
                    RandomNo = random,
                    LoanRequestId = loanRequestId,
                    Code = activity.Id.ToString(System.Globalization.CultureInfo.InvariantCulture).Encrypt(random.ToString(System.Globalization.CultureInfo.InvariantCulture)),
                    ScheduleDateUtc = DateTime.UtcNow,
                    IsCustom = false,
                    ToAddress = emailAddress

                };
                foreach (var key in data)
                {
                    var keySymbol = EmailTemplateKeys.GetKeySymbol(key.Key);

                    if (!string.IsNullOrEmpty(keySymbol))
                        witem.WorkQueueKeyValues.Add(new WorkQueueKeyValue { KeyName = keySymbol, Value = key.Value, TrackingState = TrackingState.Added });
                }
                _logger.LogInformation(message: $"DocSync SendEmailSupportActivityEmail  {witem.ToJson()}");

                workQueueService.Insert(witem);
                await workQueueService.SaveChangesAsync();
            }
            else
            {
                throw new RainMakerException("Activity not found for Support email");
            }
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("[action]")]
        public async Task<string> GetBanner(int loanApplicationId)
        {
            string photo = await loanApplicationService.GetBanner(loanApplicationId);
            var remoteFilePath = await commonService.GetSettingValueByKeyAsync<string>(SystemSettingKeys.FtpEmployeePhotoFolder) + "/" + photo;
            Stream imageData = await ftp.DownloadStream(remoteFilePath);
            using MemoryStream ms = new MemoryStream();
            await imageData.CopyToAsync(ms);
            imageData.Close();
            return Convert.ToBase64String(ms.ToArray());
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("[action]")]
        public async Task<string> GetFavIcon(int loanApplicationId)
        {
            string photo = await loanApplicationService.GetFavIcon(loanApplicationId);
            var remoteFilePath = await commonService.GetSettingValueByKeyAsync<string>(SystemSettingKeys.FtpEmployeePhotoFolder) + "/" + photo;
            Stream imageData = await ftp.DownloadStream(remoteFilePath);
            using MemoryStream ms = new MemoryStream();
            await imageData.CopyToAsync(ms);
            imageData.Close();
            return Convert.ToBase64String(ms.ToArray());
        }

        [Authorize]
        [HttpPost("[action]")]
        public async Task<IActionResult> SendEmailToSupport([FromBody] SupportEmailModel model)
        {
            var loanApplicationId = await loanApplicationService.GetLoanApplicationId(model.loanId, model.losId);

            if (loanApplicationId <= 0)
            {
                _logger.LogError(message: $"Loan application not found for Loan Id  {model.loanId}");
            }
            else
            {
                var loanApplication = await loanApplicationService.GetByLoanApplicationId(loanApplicationId);

                StringBuilder email = new StringBuilder();
                string commaseperated = ",";
                var employee = await _employeeService.GetEmployeeEmailByRoleName(Constants.SupportTeamRoleName);
                for (int i = 0; i < employee.Count; i++)
                {
                    if (i == employee.Count - 1)
                    {
                        commaseperated = string.Empty;
                    }
                    if (employee[i].EmployeeBusinessUnitEmails != null && employee[i].EmployeeBusinessUnitEmails.Any())
                    {
                        var emailAccount = employee[i].EmployeeBusinessUnitEmails.Where(x => x.BusinessUnitId == null || x.BusinessUnitId == loanApplication.BusinessUnitId)
                            .OrderByDescending(x => x.BusinessUnitId).FirstOrDefault()?.EmailAccount;
                        if (emailAccount != null)
                        {
                            email.Append(emailAccount.Email + commaseperated);
                        }
                        else
                        {
                            email.Append(string.Empty);
                        }
                    }
                }

                //var emailBody = $"Unable to find status {model.milestone} against Byte File Name {model.loanId} for tenant {model.tenantId} Dated {DateTime.Now}";
                var data = new Dictionary<FillKey, string>();
                data.Add(FillKey.CustomEmailHeader, "");
                data.Add(FillKey.CustomEmailFooter, "");
                //data.Add(FillKey.EmailBody, emailBody.Replace(Environment.NewLine, "<br/>"));
                data.Add(FillKey.TenantId, model.tenantId.ToString());
                data.Add(FillKey.ByteFileName, model.loanId);
                data.Add(FillKey.ByteStatusId, model.milestone);
                data.Add(FillKey.ActivityDate, CommonHelper.DateFormat(DateTime.UtcNow.UtcToTimeZone().ToString()));
                data.Add(FillKey.ErrorCode, ((int)HttpStatusCode.BadRequest).ToString() + ":" + HttpStatusCode.BadRequest.ToString());
                data.Add(FillKey.ErrorUrl, model.url);

                await SendEmailSupportActivityEmail(data, loanApplication.OpportunityId.ToInt(), loanApplication.LoanRequestId.ToInt(), loanApplication.BusinessUnitId.ToInt(), ActivityForType.MileStoneActivity, email.ToString());

                _logger.LogInformation(message: $"Support email send  to: {email.ToString()}");
            }

            return Ok();

        }

        [Authorize(Roles = "MCU,Customer")]
        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateLoanApplication([FromBody] LoanApplication loanApplicationRequest)
        {
            await loanApplicationService.UpdateLoanApplication(loanApplicationRequest);
            return Ok();
        }
    }
}