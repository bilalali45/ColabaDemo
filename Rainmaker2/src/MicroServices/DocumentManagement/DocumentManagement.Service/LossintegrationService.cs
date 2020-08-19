using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Service
{
    public class LossintegrationService:ILossIntegrationService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IMongoService mongoService;
        public LossintegrationService(HttpClient _httpClient, IConfiguration _configuration, IMongoService mongoService)
        {
            this._httpClient = _httpClient;
            this._configuration = _configuration;
            this.mongoService = mongoService;
        }
        public async Task<string> SendDocumentToBytePro(int LoanApplicationId, string DocumentLoanApplicationId, string RequestId, string DocumentId, string FileId, IEnumerable<string> authHeader)
        {
            var content = new
            {
                LoanApplicationId,
                DocumentLoanApplicationId,
                RequestId,
                DocumentId,
                FileId
            };

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(_configuration[key: "Lossintegration:Url"] + "/api/LosIntegration/Document/SendToExternalOriginator"),
                Method = HttpMethod.Post,
                Content = new StringContent(content: content.ToJson(),
                    encoding: Encoding.UTF8,
                    mediaType: "application/json")
            };
            request.Headers.Add("Authorization", authHeader);
            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsStringAsync();
            else
                return null;
        }

    }
}
