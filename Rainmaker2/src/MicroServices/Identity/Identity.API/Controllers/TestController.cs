using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Identity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Identity.Controllers
{
    [Route(template: "api/Identity/[controller]")]
    [ApiController]
    public class TestController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;


        public TestController(HttpClient httpClient,
                              IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        [Route(template: "[action]")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
           var dd = await GetUser("danish",
                    true);
            return Ok(dd);
        }

        private async Task<UserProfile> GetUser(string userName,
                                                bool employee = false)
        {
             

            var content = new
                          {
                              userName,
                              employee
                          };

            var callResponse = await _httpClient.PostAsync(requestUri: $"{_configuration[key: "RainMaker:Url"]}/api/rainmaker/membership/GetUser",
                                                          content: new StringContent(content: content.ToJson(),
                                                                                     encoding: Encoding.UTF8,
                                                                                     mediaType: "application/json"));
            if (callResponse.IsSuccessStatusCode)
                return await callResponse.Content.ReadAsAsync<UserProfile>();
            return null;
        }
    }
}