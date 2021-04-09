using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Extensions.ExtensionClasses;
using Microsoft.Extensions.Configuration;
using ServiceCallHelper;

namespace LosIntegration.Service.InternalServices.Rainmaker
{
    public class RainmakerService : IRainmakerService
    {
        private readonly string _baseUrl;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;


        public RainmakerService(HttpClient httpClient,
                                IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _baseUrl = configuration[key: "ServiceAddress:RainMaker:Url"];
        }


        public async Task SendBorrowerEmail(int loanApplicationId,
                                            string emailBody,
                                            int activityForId,
                                            int userId,
                                            string userName,
                                            IEnumerable<string> authHeader)
        {
            var content = new
                          {
                              loanApplicationId,
                              emailBody,
                              activityForId
                          };

            var request = new HttpRequestMessage
                          {
                              RequestUri = new Uri(uriString: _configuration[key: "ServiceAddress:RainMaker:Url"] + "/api/rainmaker/LoanApplication/SendBorrowerEmail"),
                              Method = HttpMethod.Post,
                              Content = new StringContent(content: content.ToJson(),
                                                          encoding: Encoding.UTF8,
                                                          mediaType: "application/json")
                          };
            request.Headers.Add(name: "Authorization",
                                values: authHeader);
            await _httpClient.SendAsync(request: request);
        }


        public async Task SendEmailSupportTeam(int loanApplicationId,
                                               int TenantId,
                                               string ErrorDate,
                                               string EmailBody,
                                               int ErrorCode,
                                               string DocumentCategory,
                                               string DocumentName,
                                               string DocumentExension,
                                               IEnumerable<string> authHeader)
        {
            var content = new
                          {
                              loanApplicationId,
                              TenantId,
                              ErrorDate,
                              EmailBody,
                              ErrorCode,
                              DocumentName,
                              DocumentCategory,
                              DocumentExension,
                              Url = _configuration[key: "Urls"] + "/api/LosIntegration/Document/SendFileToExternalOriginator"
                          };

            var request = new HttpRequestMessage
                          {
                              RequestUri = new Uri(uriString: _configuration[key: "ServiceAddress:RainMaker:Url"] + "/api/rainmaker/LoanApplication/SendEmailSupportTeam"),
                              Method = HttpMethod.Post,
                              Content = new StringContent(content: content.ToJson(),
                                                          encoding: Encoding.UTF8,
                                                          mediaType: "application/json")
                          };
            request.Headers.Add(name: "Authorization",
                                values: authHeader);
            await _httpClient.SendAsync(request: request);
        }


        public async Task<HttpResponseMessage> GetLoanApplication(string encompassNumber)
        {
            var documentResponse = _httpClient.EasyGetAsync<byte[]>(requestUri: $"{_baseUrl}/api/rainmaker/LoanApplication/GetLoanApplication?encompassNumber={encompassNumber}",
                                                                    attachAuthorizationHeadersFromCurrentRequest: true).Result;

            return documentResponse.HttpResponseMessage;
        }
    }
}