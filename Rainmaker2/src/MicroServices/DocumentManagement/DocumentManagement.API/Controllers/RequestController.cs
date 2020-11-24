using DocumentManagement.Model;
using DocumentManagement.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.API.Controllers
{
    [Authorize(Roles = "MCU")]
    [ApiController]
    [Route("api/DocumentManagement/[controller]")]
    public class RequestController : ControllerBase
    {
        #region Private Variables

        private readonly IRequestService requestService;
        private readonly IRainmakerService rainmakerService;

        #endregion

        #region Constructors

        public RequestController(IRequestService requestService, IRainmakerService rainmakerService)
        {
            this.requestService = requestService;
            this.rainmakerService = rainmakerService;
        }

        #endregion

        #region Action Methods

        #region Post Actions
        [HttpPost("[action]")]
        public async Task<IActionResult> UploadFile(UploadFileModel model)
        {
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            string userName = User.FindFirst("FirstName").Value.ToString() + ' ' + User.FindFirst("LastName").Value.ToString();

            var responseBody = await rainmakerService.PostLoanApplication(model.loanApplicationId, false, Request.Headers["Authorization"].Select(x => x.ToString()));

            if (!String.IsNullOrEmpty(responseBody))
            {
                User user = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(responseBody);
                int custUserId = user.userId;
                string custUserName = user.userName;
                var fileId = await requestService.UploadFile(userProfileId,userName,tenantId,custUserId,custUserName,model, Request.Headers["Authorization"].Select(x => x.ToString()));
                if(!string.IsNullOrEmpty(fileId))
                    return Ok(new { fileId });
                return BadRequest();
            }
            return NotFound();
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Save(Model.LoanApplication loanApplication, bool isDraft)
        {
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            loanApplication.tenantId = tenantId;
            string userName = User.FindFirst("FirstName").Value.ToString() + ' ' + User.FindFirst("LastName").Value.ToString();

            var responseBody =await rainmakerService.PostLoanApplication(loanApplication.loanApplicationId,isDraft, Request.Headers["Authorization"].Select(x => x.ToString()));

            if (!String.IsNullOrEmpty(responseBody))
            {
                User user = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(responseBody);
                loanApplication.userId = user.userId;
                loanApplication.userName = user.userName;
                loanApplication.requests[0].userId = userProfileId;
                loanApplication.requests[0].userName = userName;
                
                await requestService.Save(loanApplication,isDraft, Request.Headers["Authorization"].Select(x => x.ToString()));
                if(!isDraft)
                {
                    await rainmakerService.SendBorrowerEmail(loanApplication.loanApplicationId, loanApplication.requests[0].email.toAddress, loanApplication.requests[0].email.CCAddress, loanApplication.requests[0].email.fromAddress, loanApplication.requests[0].email.subject, loanApplication.requests[0].email.emailBody, (int)ActivityForType.LoanApplicationDocumentRequestActivity, userProfileId, userName, Request.Headers["Authorization"].Select(x => x.ToString()));
                }
                return Ok();
            }
            else
                return NotFound();
        }

        #endregion

        #region Get Actions

        [HttpGet("GetDraft")]
        public async Task<IActionResult> GetDraft([FromQuery] GetDraft getDraft)
        {
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            var docQuery = await requestService.GetDraft(getDraft.loanApplicationId,tenantId);
            return Ok(docQuery);
        }

        [HttpGet("GetEmailTemplate")]
        public async Task<IActionResult> GetEmailTemplate()
        {
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            var docQuery = await requestService.GetEmailTemplate(tenantId: tenantId);
            return Ok(value: docQuery);
        }

        #endregion

        #endregion
    }
}
