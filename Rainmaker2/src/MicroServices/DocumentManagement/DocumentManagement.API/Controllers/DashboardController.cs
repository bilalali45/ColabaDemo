using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentManagement.Entity;
using DocumentManagement.Model;
using DocumentManagement.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace DocumentManagement.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/DocumentManagement/[controller]")]
    public class DashboardController : Controller
    {
        private readonly IDashboardService dashboardService;
        public DashboardController(IDashboardService dashboardService)
        {
            this.dashboardService = dashboardService;
        }
        [HttpGet("GetPendingDocuments")]
        public async Task<IActionResult> GetPendingDocuments(int loanApplicationId, int tenantId)
        {
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());
            var docQuery = await dashboardService.GetPendingDocuments(loanApplicationId,tenantId, userProfileId);
            return Ok(docQuery);
        }
        [HttpGet("GetSubmittedDocuments")]
        public async Task<IActionResult> GetSubmittedDocuments(int loanApplicationId, int tenantId)
        {
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());
            var docQuery = await dashboardService.GetSubmittedDocuments(loanApplicationId, tenantId, userProfileId);
            return Ok(docQuery);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetDashboardStatus(int loanApplicationId, int tenantId)
        {
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());
            return Ok(await dashboardService.GetDashboardStatus(loanApplicationId,tenantId,userProfileId));
        }
        [HttpGet("GetFooterText")]
        public async Task<IActionResult> GetFooterText(int tenantId,int businessUnitId)
        {
            var docQuery = await dashboardService.GetFooterText(tenantId,businessUnitId);
            return Ok(docQuery);
        }
    }
}