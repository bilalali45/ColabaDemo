using ByteWebConnector.Model.Models.ServiceResponseModels.ByteWebConnectorSDK;
using ByteWebConnector.Service.DbServices;
using Extensions.ExtensionClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ServiceCallHelper;
using System.Net.Http;
using ByteWebConnector.Model.Models.ActionModels.LoanFile;
using ByteWebConnector.Model.Models.OwnModels;
using ByteWebConnector.Model.Models.ServiceRequestModels;
using ByteWebConnector.Model.Models.ServiceRequestModels.ByteWebConnectorSDK;

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

            var sendSdkDocumentResponse =  _httpClient.EasyPost<SendSdkDocumentResponse>(out var callResponse,
                                                                                         $"{_baseUrl}/api/ByteWebConnectorSdk/Document/SendSdkDocument",
                                                                                         content,
                                                                                         true);

            return callResponse;
        }


        public ApiResponse<SendLoanApplicationResponse> SendLoanApplicationToByteViaSDK(LoanFileRequest loanFileRequest)
        {
            var byteProSettings = _settingService.GetByteProSettings();
            CreateLoanApplicationRequest content = new CreateLoanApplicationRequest()
                                                   {
                                                       ByteProSettings = byteProSettings,
                                                       LoanFileRequest = loanFileRequest
                                                   };
            var byteFileCreateResponse =  this._httpClient.EasyPost<ApiResponse<SendLoanApplicationResponse>>(out var callResponse,
                                                                                                $"{_baseUrl}/api/ByteWebConnectorSdk/LoanApplication/PostLoanApplicationToByte",
                                                                                                content,
                                                                                                true);

            return byteFileCreateResponse;
        }
        public ApiResponse<string> GetLoanApplicationStatusNameViaSDK(string byteFileName)
        {
            LoanApplicationStatusNameRequest request = new LoanApplicationStatusNameRequest()
                                                       {
                                                           ByteFileName = byteFileName,
                                                           ByteProSettings = _settingService.GetByteProSettings()
                                                       };
            var byteFileCreateResponse = this._httpClient.EasyPost<ApiResponse<string>>(out var callResponse,
                                                                                                             $"{_baseUrl}/api/ByteWebConnectorSdk/LoanApplication/GetLoanApplicationStatusName",
                                                                                                             content: request,
                                                                                                             true);

            return byteFileCreateResponse;
        }
    }
}