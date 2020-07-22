using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DocumentManagement.API.Helpers;
using DocumentManagement.Entity;
using DocumentManagement.Model;
using DocumentManagement.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.IO;

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
        public async Task<IActionResult> Save(Model.LoanApplication loanApplication, bool isDraft)
        {
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());
            string userName = User.FindFirst("FirstName").Value.ToString() + ' ' + User.FindFirst("LastName").Value.ToString();

            var responseBody =await rainmakerService.PostLoanApplication(loanApplication.loanApplicationId,isDraft, Request.Headers["Authorization"].Select(x => x.ToString()));

            if (!String.IsNullOrEmpty(responseBody))
            {
                User user = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(responseBody);
                loanApplication.userId = user.userId;
                loanApplication.userName = user.userName;
                loanApplication.requests[0].userId = userProfileId;
                loanApplication.requests[0].userName = userName;

                var docQuery = await requestService.Save(loanApplication,isDraft);

                return Ok();
            }
            else
                return NotFound();
        }

        #endregion

        #endregion
    }
}
