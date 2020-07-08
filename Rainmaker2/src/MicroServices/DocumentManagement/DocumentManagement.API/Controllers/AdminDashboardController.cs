using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentManagement.Model;
using DocumentManagement.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagement.API.Controllers
{
    [Authorize(Roles ="MCU")]
    [ApiController]
    [Route("api/DocumentManagement/[controller]")]
    public class AdminDashboardController : Controller
    {
        private readonly IAdminDashboardService admindashboardService;
        public AdminDashboardController(IAdminDashboardService admindashboardService)
        {
            this.admindashboardService = admindashboardService;
        }
        [HttpGet("GetDocuments")]
        public async Task<IActionResult> GetDocuments(int loanApplicationId, int tenantId,bool pending)
        {
            var docQuery = await admindashboardService.GetDocument(loanApplicationId, tenantId, pending);
            return Ok(docQuery);
        }
        [HttpPut("[action]")]
        public async Task<IActionResult>  Delete(AdminDeleteModel model)
        {
            var docQuery = await admindashboardService.Delete(model);
            if (docQuery)
                return Ok();
            else
                return NotFound();
        }

    }
}
