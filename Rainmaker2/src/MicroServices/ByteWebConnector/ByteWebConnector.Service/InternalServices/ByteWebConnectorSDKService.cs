using System.Net.Http;
using ByteWebConnector.Model.Models;
using ByteWebConnector.Model.Models.ByteApi;
using ByteWebConnector.Model.Models.ByteSDK;
using ByteWebConnector.Model.Models.ServiceResponseModels.ByteWebConnectorSDK;
using ByteWebConnector.Service.DbServices;
using Extensions.ExtensionClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ServiceCallHelper;

namespace ByteWebConnector.Service.InternalServices
{
    public class ByteWebConnectorSdkService : IByteWebConnectorSdkService
    {
        private readonly string _baseUrl;
        private readonly HttpClient _httpClient;
        private readonly HttpRequest _request;
        private readonly ISettingService _settingService;


        public ByteWebConnectorSdkService(IHttpContextAccessor httpContextAccessor,
                                          HttpClient httpClient,
                                          IConfiguration configuration,
                                          ISettingService settingService)
        {
            _httpClient = httpClient;
            _settingService = settingService;
            _baseUrl = configuration[key: "ServiceAddress:ByteWebConnectorSDK:Url"];
            _request = httpContextAccessor.HttpContext.Request;
        }


        public CallResponse<SendSdkDocumentResponse> SendDocumentToByte(DocumentUploadRequest documentUploadRequest)
        {
            var byteProSettings = _settingService.GetByteProSettings();

            var content = new SdkSendDocumentRequest(byteProSettings: byteProSettings,
                                                     documentUploadRequest: documentUploadRequest);

            return _httpClient.Post<SendSdkDocumentResponse>(endPoint: $"{_baseUrl}/api/ByteWebConnectorSdk/Document/SendSdkDocument",
                                                             content: content.ToJson(),
                                                             request: _request,
                                                             attachBearerTokenFromCurrentRequest: true);
        }
    }
}