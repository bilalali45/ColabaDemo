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


        [HttpGet(template: "GetDocuments")]
        public async Task<IActionResult> GetDocuments(int loanApplicationId,
                                                      int tenantId,
                                                      bool pending)
        {
            logger.LogInformation(message: $"GetDocuments requested for {loanApplicationId} from tenantId {tenantId} and value of pending is {pending}");
            var docQuery = await adminDashboardService.GetDocument(loanApplicationId: loanApplicationId,
                                                                   tenantId: tenantId,
                                                                   pending: pending);
            return Ok(value: docQuery);
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


        [HttpGet(template: "IsDocumentDraft")]
        public async Task<IActionResult> IsDocumentDraft(string id)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);

            var docQuery = await adminDashboardService.IsDocumentDraft(id: id,
                                                                       userId: userProfileId);
            if (!string.IsNullOrEmpty(value: docQuery))
                logger.LogInformation(message: $"Draft exists for user {userProfileId}, loan application {id}");
            return Ok(value: docQuery);
        }
    }
}