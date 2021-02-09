using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Setting.Model;
using Extensions.ExtensionClasses;

namespace Setting.Service
{
    public class RainmakerService : IRainmakerService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public RainmakerService(HttpClient _httpClient, IConfiguration _configuration)
        {
            this._httpClient = _httpClient;
            this._configuration = _configuration;
        }

        public async Task<List<Model.UserRole>> GetUserRoles(IEnumerable<string> authHeader)
        {
            var request = new HttpRequestMessage()
                          {
                              RequestUri = new Uri(_configuration[key: "RainMaker:Url"] + "/api/RainMaker/Setting/GetUserRoles"),
                              Method = HttpMethod.Get
                          };
            request.Headers.Add("Authorization", authHeader);
            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Model.UserRole>>(await response.Content.ReadAsStringAsync());
            else
                return null;
        }

        public async Task<bool> UpdateUserRoles(List<Model.UserRole> userRoles, IEnumerable<string> authHeader)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(_configuration[key: "RainMaker:Url"] + "/api/RainMaker/Setting/UpdateUserRoles"),
                Method = HttpMethod.Post,
                Content = new StringContent(content: userRoles.ToJson(),
                                                          encoding: Encoding.UTF8,
                                                          mediaType: "application/json")
            };
            request.Headers.Add("Authorization", authHeader);
            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task SendBorrowerEmail(int loanApplicationId, string toAddess, string ccAddress, string fromAddress, string subject, string emailBody, int activityForId, int userId, string userName, string authHeader)
        {
            var content = new
            {
                loanApplicationId,
                emailBody,
                activityForId,
                toAddess,
                ccAddress,
                fromAddress,
                subject
            };

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(_configuration[key: "RainMaker:Url"] + "/api/rainmaker/LoanApplication/SendBorrowerEmail"),
                Method = HttpMethod.Post,
                Content = new StringContent(content: content.ToJson(),
                    encoding: Encoding.UTF8,
                    mediaType: "application/json")
            };
            request.Headers.Add("Authorization","Bearer " + authHeader);
            await _httpClient.SendAsync(request);
        }
    }
}
