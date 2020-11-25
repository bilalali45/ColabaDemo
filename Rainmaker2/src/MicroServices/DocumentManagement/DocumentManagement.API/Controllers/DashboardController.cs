using DocumentManagement.Model;
using DocumentManagement.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        private readonly IRainmakerService rainmakerService;

        #endregion

        #region Constructors

        public DashboardController(IDashboardService dashboardService, ILogger<DashboardController> logger, IRainmakerService rainmakerService)
        {
            this.dashboardService = dashboardService;
            this.logger = logger;
            this.rainmakerService = rainmakerService;
        }

        #endregion

        #region Action Methods

        #region GetMethods

        [HttpGet(template: "GetPendingDocuments")]
        public async Task<IActionResult> GetPendingDocuments([FromQuery] GetPendingDocuments moGetPendingDocuments)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            logger.LogInformation($"GetPendingDocument requested for {moGetPendingDocuments.loanApplicationId} tenantId {tenantId} userId {userProfileId}");

            var docQuery = await dashboardService.GetPendingDocuments(loanApplicationId: moGetPendingDocuments.loanApplicationId,
                                                                      tenantId: tenantId,
                                                                      userProfileId: userProfileId);
            return Ok(value: docQuery);
        }

        [HttpPost(template: "GetPendingDocumentsByLoanApplication")]
        public async Task<IActionResult> GetPendingDocumentsByLoanApplication(GetPendingDocumentsByLoanApplication moGetPendingDocuments)
        {
            if (moGetPendingDocuments.loanApplicationId == null || moGetPendingDocuments.loanApplicationId.Length <= 0)
                return Ok(new List<TaskCountDTO>());
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            logger.LogInformation($"GetPendingDocument requested for {moGetPendingDocuments.loanApplicationId} tenantId {tenantId} userId {userProfileId}");

            var docQuery = await dashboardService.GetPendingDocumentsByLoanApplications(loanApplicationId: moGetPendingDocuments.loanApplicationId,
                                                                      tenantId: tenantId,
                                                                      userProfileId: userProfileId);
            return Ok(value: docQuery);
        }
        [HttpGet(template: "GetSubmittedDocuments")]
        public async Task<IActionResult> GetSubmittedDocuments([FromQuery] GetSubmittedDocuments moGetSubmittedDocuments)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            logger.LogInformation($"GetSubmittedDocuments requested for {moGetSubmittedDocuments.loanApplicationId} tenantId {tenantId} userId {userProfileId}");
            var docQuery = await dashboardService.GetSubmittedDocuments(loanApplicationId: moGetSubmittedDocuments.loanApplicationId,
                                                                        tenantId: tenantId,
                                                                        userProfileId: userProfileId);
            return Ok(value: docQuery);
        }


        [HttpGet(template: "[action]")]
        public async Task<IActionResult> GetDashboardStatus([FromQuery] GetDashboardStatus moGetDashboardStatus)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            logger.LogInformation($"GetDashboardStatus requested for {moGetDashboardStatus.loanApplicationId} tenantId {tenantId} userId {userProfileId}");
            return Ok(value: await dashboardService.GetDashboardStatus(loanApplicationId: moGetDashboardStatus.loanApplicationId,
                                                                       tenantId: tenantId,
                                                                       userProfileId: userProfileId));
        }


        [HttpGet(template: "GetFooterText")]
        public async Task<IActionResult> GetFooterText([FromQuery] GetFooterText moGetFooterText)
        {
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            var businessUnitId = (await rainmakerService.GetByLoanApplicationId(moGetFooterText.loanApplicationId, Request.Headers["Authorization"].Select(x => x.ToString()))).BusinessUnitId.Value;
            var docQuery = await dashboardService.GetFooterText(tenantId: tenantId,
                                                                businessUnitId: businessUnitId);
            return Ok(value: docQuery);
        }

        #endregion

        #endregion
    }
}