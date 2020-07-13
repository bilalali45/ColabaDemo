using System.Threading.Tasks;
using DocumentManagement.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagement.API.Controllers
{
    [Authorize(Roles = "Customer")]
    [ApiController]
    [Route(template: "api/DocumentManagement/[controller]")]
    public class DashboardController : Controller
    {
        #region Private Variables

        private readonly IDashboardService dashboardService;

        #endregion

        #region Constructors

        public DashboardController(IDashboardService dashboardService)
        {
            this.dashboardService = dashboardService;
        }

        #endregion

        #region Action Methods

        #region GetMethods

        [HttpGet(template: "GetPendingDocuments")]
        public async Task<IActionResult> GetPendingDocuments(int loanApplicationId,
                                                             int tenantId)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            var docQuery = await dashboardService.GetPendingDocuments(loanApplicationId: loanApplicationId,
                                                                      tenantId: tenantId,
                                                                      userProfileId: userProfileId);
            return Ok(value: docQuery);
        }


        [HttpGet(template: "GetSubmittedDocuments")]
        public async Task<IActionResult> GetSubmittedDocuments(int loanApplicationId,
                                                               int tenantId)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            var docQuery = await dashboardService.GetSubmittedDocuments(loanApplicationId: loanApplicationId,
                                                                        tenantId: tenantId,
                                                                        userProfileId: userProfileId);
            return Ok(value: docQuery);
        }


        [HttpGet(template: "[action]")]
        public async Task<IActionResult> GetDashboardStatus(int loanApplicationId,
                                                            int tenantId)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            return Ok(value: await dashboardService.GetDashboardStatus(loanApplicationId: loanApplicationId,
                                                                       tenantId: tenantId,
                                                                       userProfileId: userProfileId));
        }


        [HttpGet(template: "GetFooterText")]
        public async Task<IActionResult> GetFooterText(int tenantId,
                                                       int businessUnitId)
        {
            var docQuery = await dashboardService.GetFooterText(tenantId: tenantId,
                                                                businessUnitId: businessUnitId);
            return Ok(value: docQuery);
        }

        #endregion

        #endregion
    }
}