using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DocManager.Service
{
    public class LosIntegrationService : ILosIntegrationService
    {
        #region Private Variables

        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        #endregion

        #region Constructors

        public LosIntegrationService(HttpClient httpClient,
                                     IConfiguration configuration
        ,ILogger<LosIntegrationService> logger
        )
        {
            _httpClient = httpClient;
            _configuration = configuration;
           
        }

        #endregion

        #region Methods
        public async Task<string> SendFilesToBytePro(int loanApplicationId,
                                                     string documentLoanApplicationId,
                                                     string requestId,
                                                     string documentId,
                                                     string fileid,
                                                     IEnumerable<string> authHeader)
        {
            var content = new
                          {
                              LoanApplicationId = loanApplicationId,
                              DocumentLoanApplicationId = documentLoanApplicationId,
                              RequestId = requestId,
                              DocumentId = documentId,
                              Fileid= fileid
            };
           
        
            var request = new HttpRequestMessage
                          {
                              RequestUri =
                                  new Uri(uriString: _configuration[key: "LosIntegration:Url"] +
                                                     "/api/LosIntegration/Document/SendFileToExternalOriginator"),
                              Method = HttpMethod.Post,
                              Content = new StringContent(content: content.ToJson(),
                                                          encoding: Encoding.UTF8,
                                                          mediaType: "application/json")
                          };
            request.Headers.Add(name: "Authorization",
                                values: authHeader);
            var response = await _httpClient.SendAsync(request: request);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsStringAsync();
            return null;
        }

        #endregion
    }
}