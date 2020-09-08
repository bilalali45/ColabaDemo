using ByteWebConnector.Model.Configuration;
using ByteWebConnector.Model.Models.Document;
using ByteWebConnector.Model.Models.ServiceRequestModels.Document;
using Extensions.ExtensionClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

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
                                     IOptions<ServiceDiscovery> serviceDiscovery)
        {
            _httpClient = httpClient;
            _baseUrl = _serviceDiscovery.LosIntegration.Url;
            _serviceDiscovery = serviceDiscovery.Value;
            _request = httpContextAccessor.HttpContext.Request;
        }


        public async Task<HttpResponseMessage> DocumentDelete(string content)
        {
            var token = _request.Headers[key: "Authorization"].ToString().Replace(oldValue: "Bearer ",
                                                                                  newValue: "");

            _httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue(scheme: "Bearer",
                                                parameter: token);

            return await _httpClient.PostAsync(requestUri:
                                               $"{_baseUrl}/api/LosIntegration/Document/Delete",
                                               content: new StringContent(content: content,
                                                                          encoding: Encoding.UTF8,
                                                                          mediaType: "application/json"));
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