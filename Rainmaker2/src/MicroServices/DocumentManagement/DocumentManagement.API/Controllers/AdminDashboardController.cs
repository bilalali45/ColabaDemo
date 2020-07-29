using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DocumentManagement.Model;
using DocumentManagement.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DocumentManagement.API.Controllers
{
    [Authorize(Roles = "MCU")]
    [ApiController]
    [Route(template: "api/DocumentManagement/[controller]")]
    public class AdminDashboardController : Controller
    {
        private readonly IAdminDashboardService adminDashboardService;
        private readonly ILogger<AdminDashboardController> logger;


        public AdminDashboardController(IAdminDashboardService adminDashboardService,
                                        ILogger<AdminDashboardController> logger)
        {
            this.adminDashboardService = adminDashboardService;
            this.logger = logger;
        }
        [HttpGet("GetDocuments")]
        public async Task<IActionResult> GetDocuments([FromQuery] GetDocuments moGetDocuments)
        {
            logger.LogInformation($"GetDocuments requested for {moGetDocuments.loanApplicationId} from tenantId {moGetDocuments.tenantId} and value of pending is {moGetDocuments.pending}");
            var docQuery = await adminDashboardService.GetDocument(moGetDocuments.loanApplicationId, moGetDocuments.tenantId, moGetDocuments.pending);
            return Ok(docQuery);
        }


        [HttpPut(template: "[action]")]
        public async Task<IActionResult> Delete(AdminDeleteModel model)
        {
            logger.LogInformation(message: $"document {model.docId} is being deleted as borrower to do");
            var docQuery = await adminDashboardService.Delete(model: model);
            if (docQuery)
                return Ok();
            return NotFound();
        }

        [HttpGet("IsDocumentDraft")]
        public async Task<IActionResult> IsDocumentDraft([FromQuery] IsDocumentDraft moIsDocumentDraft)
        {
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());
            var docQuery = await adminDashboardService.IsDocumentDraft(moIsDocumentDraft.id, userProfileId);
            return Ok(docQuery);
        }
    }
}