using System;
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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LosIntegration.API.Controllers
{
    [Route(template: "api/LosIntegration/[controller]")]
    [ApiController]
   // [Authorize(Roles = "MCU,Customer")]
    public class LoanApplicationController : BaseApiController<LoanApplicationController>
    {
        #region Constructor

        public LoanApplicationController( 
                                         ILogger<LoanApplicationController> logger,
                                         ILoanApplicationService loanApplicationService,
                                         IByteWebConnectorService byteWebConnectorService,
                                         IMilestoneService milestoneService) : base(logger)
        {
            _logger = logger;
            _loanApplicationService = loanApplicationService;
            _byteWebConnectorService = byteWebConnectorService;
            _milestoneService = milestoneService;
        }

        #endregion

        #region Action Methods 

        [Route(template: "[action]")]
        [HttpGet]
        public async Task<IActionResult> UpdateLoanStatusFromByte([FromQuery] int loanApplicationId)
        {
            short byteProLoanStatusId = 0;
            var loanApplicationDetail = _loanApplicationService.GetLoanApplicationWithDetails(loanApplicationId: loanApplicationId);
            if (loanApplicationDetail == null)
            {
                return NotFound();
            }
            else
            {
                byteProLoanStatusId = await this._byteWebConnectorService.GetLoanStatusAsync(loanApplicationDetail.ByteLoanNumber);
                if(byteProLoanStatusId <= 0)
                {
                    base._logger.LogWarning($"Loan status return from LOS is not valid. Loan Status : {byteProLoanStatusId}");
                    return BadRequest();
                }
                else
                {
                    //var loanApplication = _loanApplicationService.GetLoanApplicationWithDetails(loanApplicationId);
                    var updateRespose = await this.UpdateRainmakerLoanStatus(loanApplicationId, loanApplicationDetail.ByteFileName, byteProLoanStatusId);
                    return updateRespose;
                }
            }
        }


        private async Task<IActionResult> UpdateRainmakerLoanStatus(int loanApplicationId, string byteFileName, short losStatusId)
        {
            return await this._milestoneService.SyncRainmakerLoanStatusFromByte(base.TenantId, loanApplicationId, losStatusId, byteFileName);
        }

        #endregion

        #region Private Fields
        private readonly ILogger<LoanApplicationController> _logger;
        private readonly ILoanApplicationService _loanApplicationService;
        private readonly IByteWebConnectorService  _byteWebConnectorService;
        private readonly IMilestoneService _milestoneService;

        #endregion
    }
}