using System.Threading.Tasks;
using DocumentManagement.Model;
using DocumentManagement.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DocumentManagement.API.Controllers
{
    [Authorize(Roles = "Customer")]
    [ApiController]
    [Route(template: "api/DocumentManagement/[controller]")]
    public class DashboardController : Controller
    {
        #region Private Variables

        private readonly IDashboardService dashboardService;
        private readonly ILogger<DashboardController> logger;

        #endregion

        #region Constructors

        public DashboardController(IDashboardService dashboardService, ILogger<DashboardController> logger)
        {
            this.dashboardService = dashboardService;
            this.logger = logger;
        }

        #endregion

        #region Action Methods

        #region GetMethods

        [HttpGet(template: "GetPendingDocuments")]
        public async Task<IActionResult> GetPendingDocuments([FromQuery] GetPendingDocuments moGetPendingDocuments)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            logger.LogInformation($"GetPendingDocument requested for {moGetPendingDocuments.loanApplicationId} tenantId {moGetPendingDocuments.tenantId} userId {userProfileId}");

            var docQuery = await dashboardService.GetPendingDocuments(loanApplicationId: moGetPendingDocuments.loanApplicationId,
                                                                      tenantId: moGetPendingDocuments.tenantId,
                                                                      userProfileId: userProfileId);
            return Ok(value: docQuery);
        }


        [HttpGet(template: "GetSubmittedDocuments")]
        public async Task<IActionResult> GetSubmittedDocuments([FromQuery] GetSubmittedDocuments moGetSubmittedDocuments)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            logger.LogInformation($"GetSubmittedDocuments requested for {moGetSubmittedDocuments.loanApplicationId} tenantId {moGetSubmittedDocuments.tenantId} userId {userProfileId}");
            var docQuery = await dashboardService.GetSubmittedDocuments(loanApplicationId: moGetSubmittedDocuments.loanApplicationId,
                                                                        tenantId: moGetSubmittedDocuments.tenantId,
                                                                        userProfileId: userProfileId);
            return Ok(value: docQuery);
        }


        [HttpGet(template: "[action]")]
        public async Task<IActionResult> GetDashboardStatus([FromQuery] GetDashboardStatus moGetDashboardStatus)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            logger.LogInformation($"GetDashboardStatus requested for {moGetDashboardStatus.loanApplicationId} tenantId {moGetDashboardStatus.tenantId} userId {userProfileId}");
            return Ok(value: await dashboardService.GetDashboardStatus(loanApplicationId: moGetDashboardStatus.loanApplicationId,
                                                                       tenantId: moGetDashboardStatus.tenantId,
                                                                       userProfileId: userProfileId));
        }


        [HttpGet(template: "GetFooterText")]
        public async Task<IActionResult> GetFooterText([FromQuery] GetFooterText moGetFooterText)
        {
            var docQuery = await dashboardService.GetFooterText(tenantId: moGetFooterText.tenantId,
                                                                businessUnitId: moGetFooterText.businessUnitId);
            return Ok(value: docQuery);
        }

        #endregion

        #endregion
    }
}