using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;

namespace DocumentManagement.Service
{
    public class LosIntegrationService : ILosIntegrationService
    {
        #region Private Variables

        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        //private readonly IMongoService _mongoService;

        #endregion

        #region Constructors

        public LosIntegrationService(HttpClient httpClient,
                                     IConfiguration configuration
        //IMongoService mongoService
        )
        {
            _httpClient = httpClient;
            _configuration = configuration;
            //_mongoService = mongoService;
        }

        #endregion

        #region Methods

        public async Task<string> SendDocumentToBytePro(int loanApplicationId,
                                                        string documentLoanApplicationId,
                                                        string requestId,
                                                        string documentId,
                                                        string fileId,
                                                        IEnumerable<string> authHeader)
        {
            var content = new
                          {
                              LoanApplicationId = loanApplicationId,
                              DocumentLoanApplicationId = documentLoanApplicationId,
                              RequestId = requestId,
                              DocumentId = documentId,
                              FileId = fileId
                          };

            var request = new HttpRequestMessage
                          {
                              RequestUri =
                                  new Uri(uriString: _configuration[key: "LosIntegration:Url"] +
                                                     "/api/LosIntegration/Document/SendToExternalOriginator"),
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


        public async Task<string> SendFilesToBytePro(int loanApplicationId,
                                                     string documentLoanApplicationId,
                                                     string requestId,
                                                     string documentId,
                                                     IEnumerable<string> authHeader)
        {
            var content = new
                          {
                              LoanApplicationId = loanApplicationId,
                              DocumentLoanApplicationId = documentLoanApplicationId,
                              RequestId = requestId,
                              DocumentId = documentId
                          };

            var request = new HttpRequestMessage
                          {
                              RequestUri =
                                  new Uri(uriString: _configuration[key: "LosIntegration:Url"] +
                                                     "/api/LosIntegration/Document/SendDocumentToExternalOriginator"),
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