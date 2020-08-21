using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Notification.Model;

namespace Notification.Service
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
        public async Task<List<int>> GetAssignedUsers(int loanApplicationId)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(_configuration[key: "RainMaker:Url"] + "/api/rainmaker/Notification/GetAssignedUsers?loanApplicationId="+loanApplicationId),
                Method = HttpMethod.Get
            };
            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
                return Newtonsoft.Json.JsonConvert.DeserializeObject <List<int>>(await response.Content.ReadAsStringAsync());
            else
                return null;
        }

        public async Task<LoanSummary> GetLoanSummary(int loanApplicationId)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(_configuration[key: "RainMaker:Url"] + "/api/rainmaker/notification/GetLoanInfo?loanApplicationId=" + loanApplicationId),
                Method = HttpMethod.Get
            };
            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
                return Newtonsoft.Json.JsonConvert.DeserializeObject<LoanSummary>(await response.Content.ReadAsStringAsync());
            else
                return null;
        }
    }
}
