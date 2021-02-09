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
    [Authorize(Roles = "MCU")]
    [ApiController]
    [Route(template: "api/DocumentManagement/[controller]")]
    public class DashboardMCUController : Controller
    {
        #region Private Variables

        private readonly IDashboardService dashboardService;
        private readonly ILogger<DashboardController> logger;
        private readonly IRainmakerService rainmakerService;

        #endregion

        #region Constructors

        public DashboardMCUController(IDashboardService dashboardService, ILogger<DashboardController> logger, IRainmakerService rainmakerService)
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

        #endregion

        #endregion
    }
}