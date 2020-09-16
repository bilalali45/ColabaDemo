using Extensions.ExtensionClasses;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
namespace LosIntegration.Service
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


        public async Task SendBorrowerEmail(int loanApplicationId, string emailBody, int activityForId, int userId, string userName, IEnumerable<string> authHeader)
        {
            var content = new
            {
                loanApplicationId,
                emailBody,
                activityForId
            };

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(_configuration[key: "ServiceAddress:RainMaker:Url"] + "/api/rainmaker/LoanApplication/SendBorrowerEmail"),
                Method = HttpMethod.Post,
                Content = new StringContent(content: content.ToJson(),
                    encoding: Encoding.UTF8,
                    mediaType: "application/json")
            };
            request.Headers.Add("Authorization", authHeader);
            await _httpClient.SendAsync(request);
        }

        public async Task SendEmailSupportTeam(  int loanApplicationId, int TenantId, string ErrorDate, string EmailBody, int ErrorCode, string DocumentCategory,   string DocumentName, string DocumentExension, IEnumerable<string> authHeader)
        {
            var content = new
            {
                loanApplicationId,
                TenantId,
                ErrorDate,
                EmailBody,
                ErrorCode,
                DocumentName,
                DocumentCategory ,
                DocumentExension,
                Url = _configuration[key: "Urls"] + "/ api/LosIntegration/Document/SendFileToExternalOriginator"
    };

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(_configuration[key: "ServiceAddress:RainMaker:Url"] + "/api/rainmaker/LoanApplication/SendEmailSupportTeam"),
                Method = HttpMethod.Post,
                Content = new StringContent(content: content.ToJson(),
                    encoding: Encoding.UTF8,
                    mediaType: "application/json")
            };
            request.Headers.Add("Authorization", authHeader);
            await _httpClient.SendAsync(request);
        }


    }
}
