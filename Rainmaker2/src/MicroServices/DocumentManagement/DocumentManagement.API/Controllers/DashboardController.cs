using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentManagement.Service;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : Controller
    {
        private readonly IDashboardService dashboardService;
        public DashboardController(IDashboardService dashboardService)
        {
            this.dashboardService = dashboardService;
        }
        [HttpGet("GetPendingDocuments")]
        public async Task<IActionResult> GetPendingDocuments(int loadApplicationId, int tenantId)
        {
            var docQuery = await dashboardService.GetPendingDocuments(loadApplicationId,tenantId);
            return null;
        }
    }
}