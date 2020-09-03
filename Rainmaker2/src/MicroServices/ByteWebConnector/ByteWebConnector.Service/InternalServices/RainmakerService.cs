﻿using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ByteWebConnector.Model.Configuration;
using ByteWebConnector.Model.Models.Document;
using ByteWebConnector.Model.Models.ServiceRequestModels.Document;
using ByteWebConnector.Model.Models.ServiceRequestModels.Rainmaker.LoanApplication;
using ByteWebConnector.Model.Models.ServiceResponseModels.Rainmaker.LoanApplication;
using Extensions.ExtensionClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using ServiceCallHelper;

namespace ByteWebConnector.Service.InternalServices
{
    public class RainmakerService : IRainmakerService
    {
        private readonly string _baseUrl;
        private readonly HttpClient _httpClient;
        private readonly HttpRequest _request;

        public RainmakerService(IHttpContextAccessor httpContextAccessor,
                                HttpClient httpClient,
                                IOptions<ServiceDiscovery> serviceDiscovery)
        {
            _httpClient = httpClient;
            _baseUrl = serviceDiscovery.Value.RainMaker.Url;
            _request = httpContextAccessor.HttpContext.Request;
        }


        public CallResponse<GetLoanApplicationResponse> GetLoanApplication(int loanApplicationId)
        {
            return _httpClient.Get<GetLoanApplicationResponse>(endPoint: $"{_baseUrl}/api/RainMaker/LoanApplication/GetByLoanApplicationId?id={loanApplicationId}",
                                                                    request: _request,
                                                                    attachBearerTokenFromCurrentRequest: true
                                                                   );
        }


        public async Task<HttpResponseMessage> DocumentAddDocument(int fileDataId,
                                                                   List<EmbeddedDoc> embeddedDocs)
        {
            var content = new AddDocumentRequest(fileDataId: fileDataId,
                                                 embeddedDocs: embeddedDocs).ToJson();
            var token = _request.Headers[key: "Authorization"].ToString().Replace(oldValue: "Bearer ",
                                                                                  newValue: "");
            _httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue(scheme: "Bearer",
                                                parameter: token);

            return await _httpClient.PostAsync(requestUri:
                                               $"{_baseUrl}/api/LosIntegration/Document/AddDocument",
                                               content: new StringContent(content: content,
                                                                          encoding: Encoding.UTF8,
                                                                          mediaType: "application/json"));
        }
    }
}