using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentManagement.Entity;
using DocumentManagement.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagement.API.Controllers
{
    [Authorize(Roles = "MCU")]
    [ApiController]
    [Route("api/DocumentManagement/[controller]")]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService requestService;

        public RequestController(IRequestService requestService)
        {
            this.requestService = requestService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SaveDraft(LoanApplication loanApplication)
        {
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());
            string userName = User.FindFirst("FirstName").Value.ToString() + ' ' +User.FindFirst("LastName").Value.ToString();
            loanApplication.userId = 1;
            loanApplication.userName = "abc";
            loanApplication.requests[0].userId = userProfileId;
            loanApplication.requests[0].userName = userName;

            var docQuery = await requestService.SaveDraft(loanApplication);

            if (docQuery)
                return Ok();
            else
                return NotFound();
        }
    }
}
