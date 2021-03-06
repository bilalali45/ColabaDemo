using DocumentManagement.Model;
using DocumentManagement.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

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
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());
            logger.LogInformation($"GetDocuments requested for {moGetDocuments.loanApplicationId} from tenantId {tenantId} and value of pending is {moGetDocuments.pending}");
            var docQuery = await adminDashboardService.GetDocument(moGetDocuments.loanApplicationId, tenantId, moGetDocuments.pending,userProfileId);
            return Ok(docQuery);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetDashboardSetting()
        {
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());
            return Ok(await adminDashboardService.GetDashboardSetting(userProfileId));
        }

        [HttpPut(template: "[action]")]
        public async Task<IActionResult> Delete(AdminDeleteModel model)
        {
            logger.LogInformation(message: $"document {model.docId} is being deleted as borrower to do");
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            var docQuery = await adminDashboardService.Delete(model: model,tenantId, Request.Headers["Authorization"].Select(x => x.ToString()));
            if (docQuery)
                return Ok();
            return NotFound(new ErrorModel { Code=404, Message="Unable to find document to delete"});
        }

        [HttpGet("IsDocumentDraft")]
        public async Task<IActionResult> IsDocumentDraft([FromQuery] IsDocumentDraft moIsDocumentDraft)
        {
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());
            var docQuery = await adminDashboardService.IsDocumentDraft(moIsDocumentDraft.loanApplicationId, userProfileId);
            return Ok(docQuery);
        }
    }
}