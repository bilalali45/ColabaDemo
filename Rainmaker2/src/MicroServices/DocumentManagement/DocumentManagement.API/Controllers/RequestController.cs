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
        private readonly IRequestService requestService;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public RequestController(IRequestService requestService, HttpClient httpClient, IConfiguration configuration)
        {
            this.requestService = requestService;
            _httpClient = httpClient;
            _configuration = configuration;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Save(Model.LoanApplication loanApplication, bool isDraft)
        {
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());
            string userName = User.FindFirst("FirstName").Value.ToString() + ' ' + User.FindFirst("LastName").Value.ToString();

            var content = new
            {
                loanApplication.loanApplicationId,
                isDraft
            };

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(_configuration[key: "RainMaker:Url"]+"/api/rainmaker/LoanApplication/PostLoanApplication"),
                Method = HttpMethod.Post,
                Content = new StringContent(content: content.ToJson(),
                    encoding: Encoding.UTF8,
                    mediaType: "application/json")
            };
            request.Headers.Add("Authorization",Request.Headers["Authorization"].Select(x=>x.ToString()));
            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
               
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
    }
}
