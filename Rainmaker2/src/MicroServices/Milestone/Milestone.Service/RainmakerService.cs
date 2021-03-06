using Microsoft.Extensions.Configuration;
using Milestone.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Milestone.Service
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
        public async Task<int> GetLoanApplicationId(string loanId, short losId, IEnumerable<string> auth)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(_configuration[key: "RainMaker:Url"] + "/api/rainmaker/milestone/GetLoanApplicationId"),
                Method = HttpMethod.Post,
                Content = new StringContent(@"{""loanId"":""" + loanId + @""",""losId"":" + losId + "}", Encoding.UTF8, "application/json"),
            };
            request.Headers.Add("Authorization", auth);
            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();
            return Convert.ToInt32(await response.Content.ReadAsStringAsync());
        }
        public async Task SendEmailToSupport(int tenantId, string milestone, string loanId, int rainmakerLosId, string url, IEnumerable<string> auth)
        {
            var contentString = @"{""tenantId"":" + tenantId + @",""milestone"":""" + milestone + @""",""loanId"":""" + loanId + @""",""losId"":" + rainmakerLosId + @",""url"":""" + url + @"""}";
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(_configuration[key: "RainMaker:Url"] + "/api/rainmaker/loanapplication/sendemailtosupport"),
                Method = HttpMethod.Post,
                Content = new StringContent(contentString, Encoding.UTF8, "application/json"),
            };
            request.Headers.Add("Authorization", auth);
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }
        public async Task SetMilestoneId(int loanApplicationId, int milestoneId, IEnumerable<string> auth)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(_configuration[key: "RainMaker:Url"] + "/api/rainmaker/milestone/setmilestoneid"),
                Method = HttpMethod.Post,
                Content = new StringContent(@"{""loanApplicationId"":"+loanApplicationId+@",""milestoneId"":"+milestoneId+"}",Encoding.UTF8,"application/json"),
            };
            request.Headers.Add("Authorization", auth);
            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();
        }
        public async Task SetBothLosAndMilestoneId(int loanApplicationId, int milestoneId, int losMilestoneId, IEnumerable<string> auth)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(_configuration[key: "RainMaker:Url"] + "/api/rainmaker/milestone/SetBothLosAndMilestoneId"),
                Method = HttpMethod.Post,
                Content = new StringContent(@"{""loanApplicationId"":" + loanApplicationId + @",""milestoneId"":" + milestoneId + @",""losMilestoneId"":"+losMilestoneId+"}", Encoding.UTF8, "application/json"),
            };
            request.Headers.Add("Authorization", auth);
            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();
        }
        public async Task<int> GetMilestoneId(int loanApplicationId, IEnumerable<string> auth)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(_configuration[key: "RainMaker:Url"] + "/api/rainmaker/milestone/getmilestoneid?loanApplicationId=" + loanApplicationId),
                Method = HttpMethod.Get
            };
            request.Headers.Add("Authorization", auth);
            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();
            return int.Parse(await response.Content.ReadAsStringAsync());
        }
        public async Task<BothLosMilestoneModel> GetBothLosAndMilestoneId(int loanApplicationId, IEnumerable<string> auth)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(_configuration[key: "RainMaker:Url"] + "/api/rainmaker/milestone/GetBothLosAndMilestoneId?loanApplicationId=" + loanApplicationId),
                Method = HttpMethod.Get
            };
            request.Headers.Add("Authorization", auth);
            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();
            return Newtonsoft.Json.JsonConvert.DeserializeObject<BothLosMilestoneModel>(await response.Content.ReadAsStringAsync());
        }
    }
}
