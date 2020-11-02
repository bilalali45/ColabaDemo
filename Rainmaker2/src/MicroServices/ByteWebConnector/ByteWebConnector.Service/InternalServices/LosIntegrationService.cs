using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ByteWebConnector.Model.Configuration;
using ByteWebConnector.Model.Models;
using ByteWebConnector.Model.Models.ServiceRequestModels.Document;
using ByteWebConnector.Model.Models.ServiceResponseModels.ByteApi;
using Extensions.ExtensionClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using ServiceCallHelper;

namespace ByteWebConnector.Service.InternalServices
{
    public class LosIntegrationService : ILosIntegrationService
    {
        private readonly string _baseUrl;
        private readonly HttpClient _httpClient;
        private readonly HttpRequest _request;
        private readonly ServiceDiscovery _serviceDiscovery;


        public LosIntegrationService(IHttpContextAccessor httpContextAccessor,
                                     HttpClient httpClient,
                                     IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["ServiceAddress:LosIntegration:Url"];
            
            _request = httpContextAccessor.HttpContext.Request;
        }


        public async Task<bool> DocumentDelete(string content)
        {
            //var token = _request.Headers[key: "Authorization"].ToString().Replace(oldValue: "Bearer ",
            //                                                                      newValue: "");

            //_httpClient.DefaultRequestHeaders.Authorization
            //    = new AuthenticationHeaderValue(scheme: "Bearer",
            //                                    parameter: token);

            //return await _httpClient.PostAsync(requestUri:
            //                                   $"{_baseUrl}/api/LosIntegration/Document/Delete",
            //                                   content: new StringContent(content: content,
            //                                                              encoding: Encoding.UTF8,
            //                                                              mediaType: "application/json"));

            var response = await
                _httpClient.EasyPostAsync<HttpResponseMessage>(requestUri: $"{_baseUrl}/api/ByteWebConnector/Document/SendDocument",
                                                               content: content,
                                                               attachAuthorizationHeadersFromCurrentRequest: true);
            if (response.HttpResponseMessage.IsSuccessStatusCode)
                return true;

            return false;
        }


        public async Task<bool> DocumentAddDocument(int fileDataId,
                                                    List<EmbeddedDoc> embeddedDocs)
        {
            var content = new AddDocumentRequest(fileDataId: fileDataId,
                                                 embeddedDocs: embeddedDocs).ToJson();
            //var token = _request.Headers[key: "Authorization"].ToString().Replace(oldValue: "Bearer ",
            //                                                                      newValue: "");
            //_httpClient.DefaultRequestHeaders.Authorization
            //    = new AuthenticationHeaderValue(scheme: "Bearer",
            //                                    parameter: token);

            //return await _httpClient.PostAsync(requestUri:
            //                                   $"{_baseUrl}/api/LosIntegration/Document/AddDocument",
            //                                   content: new StringContent(content: content,
            //                                                              encoding: Encoding.UTF8,
            //                                                              mediaType: "application/json"));

            var response = await
                _httpClient.EasyPostAsync<dynamic>(requestUri: $"{_baseUrl}/api/LosIntegration/Document/AddDocument",
                                                   content: content,
                                                   attachAuthorizationHeadersFromCurrentRequest: true);

            if (response.HttpResponseMessage.IsSuccessStatusCode)
                return true;

            return false;
        }
    }
}