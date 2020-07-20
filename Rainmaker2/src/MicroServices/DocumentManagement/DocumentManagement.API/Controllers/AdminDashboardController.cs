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
    [Route("api/DocumentManagement/[controller]")]
    public class AdminDashboardController : Controller
    {
        private readonly IAdminDashboardService admindashboardService;
        private readonly ILogger<AdminDashboardController> logger;
        public AdminDashboardController(IAdminDashboardService admindashboardService, ILogger<AdminDashboardController> logger)
        {
            this.admindashboardService = admindashboardService;
            this.logger = logger;
        }
        [HttpGet("GetDocuments")]
        public async Task<IActionResult> GetDocuments([FromQuery]  GetDocuments moGetDocuments)
        {
            logger.LogInformation($"GetDocuments requested for {moGetDocuments.loanApplicationId} from tenantId {moGetDocuments.tenantId} and value of pending is {moGetDocuments.pending}");
            var docQuery = await admindashboardService.GetDocument(moGetDocuments.loanApplicationId, moGetDocuments.tenantId, moGetDocuments.pending);
            return Ok(docQuery);
        }
        [HttpPut("[action]")]
        public async Task<IActionResult>  Delete(AdminDeleteModel model)
        {
            logger.LogInformation($"document {model.docId} is being deleted as borrower to do");
            var docQuery = await admindashboardService.Delete(model);
            if (docQuery)
                return Ok();
            else
                return NotFound();
        }

        [HttpGet("IsDocumentDraft")]
        public async Task<IActionResult> IsDocumentDraft([FromQuery] IsDocumentDraft moIsDocumentDraft )
        {
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());
            var docQuery = await admindashboardService.IsDocumentDraft(moIsDocumentDraft.id, userProfileId);
            if(!string.IsNullOrEmpty(docQuery))
                logger.LogInformation($"Draft exists for user {userProfileId}, loan application {moIsDocumentDraft.id}");
            return Ok(docQuery);
        }


    }
}
