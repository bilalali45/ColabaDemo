
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using LosIntegration.Model.Model.ServiceRequestModels.ByteWebConnector;
using LosIntegration.Model.Model.ServiceRequestModels.Document;
using LosIntegration.Model.Model.ServiceResponseModels;
using LosIntegration.Model.Model.ServiceResponseModels.ByteWebConnector;
using LosIntegration.Model.Model.ServiceResponseModels.Rainmaker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ServiceCallHelper;

namespace LosIntegration.Service.InternalServices
{
    public class ByteWebConnectorService : IByteWebConnectorService
    {
        #region Constructors

        public ByteWebConnectorService(ILogger<ByteWebConnectorService> logger,
                                       HttpClient httpClient,
                                       IConfiguration configuration)
        {
            _logger = logger;
            _httpClient = httpClient;
            _configuration = configuration;
            _baseUrl = _configuration[key: "ServiceAddress:ByteWebConnector:Url"];
        }

        #endregion

        #region Methods 

        public async Task<CallResponse<SendSdkDocumentResponse>> SendDocument(SendDocumentRequest sendDocumentRequest)
        {
            var externalOriginatorSendDocumentResponse = await
                _httpClient.EasyPostAsync<SendSdkDocumentResponse>(requestUri: $"{_baseUrl}/api/ByteWebConnector/Document/SendDocument",
                                                                   content: sendDocumentRequest,
                                                                   attachAuthorizationHeadersFromCurrentRequest: true);
            return externalOriginatorSendDocumentResponse;
        }


        public async Task<EmbeddedDoc> GetDocumentDataFromByte(GetDocumentDataRequest documentDataRequest)
        {
            var documentDataResult = await
                _httpClient.EasyPostAsync<EmbeddedDoc>(requestUri: $"{_baseUrl}/api/ByteWebConnector/Document/GetDocumentDataFromByte",
                                                       content: documentDataRequest,
                                                       attachAuthorizationHeadersFromCurrentRequest: true);
            return documentDataResult.ResponseObject;
        }


        public async Task<UploadFileResponse> UploadFile(string uploadFileRequest)
        {
            var uploadFileResponse = await
                _httpClient.EasyPostAsync<UploadFileResponse>(requestUri: $"{_baseUrl}/api/DocumentManagement/request/UploadFile",
                                                              content: uploadFileRequest,
                                                              attachAuthorizationHeadersFromCurrentRequest: true);
            return uploadFileResponse.ResponseObject;
        }


        public async Task<CallResponse<ApiResponse<LoanFileInfo>>> SendLoanApplication(LoanApplication loanApplication,
                                                                                       LoanRequest loanRequest,
                                                                                       List<ThirdPartyCode> byteProCodeList)
        {

            var sendLoanFileRequest = new SendLoanFileRequest
            {
                LoanApplication = loanApplication,
                LoanRequest = loanRequest,
                ThirdPartyCodeList = new ThirdPartyCodeList() { ThirdPartyCodes = byteProCodeList }
            };
            var externalOriginatorSendDocumentResponse =
                _httpClient.EasyPostAsync<ApiResponse<LoanFileInfo>>(requestUri: $"{_baseUrl}/api/ByteWebConnector/LoanFile/SendLoanFile",
                                                           content: sendLoanFileRequest,
                                                           attachAuthorizationHeadersFromCurrentRequest: true
                                                           );
            return await externalOriginatorSendDocumentResponse;
        }


        public async Task<CallResponse<ApiResponse<LoanFileInfo>>> SendLoanApplicationViaSDK(LoanApplication loanApplication, List<ThirdPartyCode> byteProCodeList)
        {
            var sendLoanFileRequest = new SendLoanFileRequest
                                      {
                                          LoanApplication = loanApplication,
                                          LoanRequest = null,
                                          ThirdPartyCodeList = new ThirdPartyCodeList() { ThirdPartyCodes = byteProCodeList }
                                      };
            var response = await _httpClient.EasyPostAsync<ApiResponse<LoanFileInfo>>(requestUri: $"{_baseUrl}/api/ByteWebConnector/LoanFile/SendLoanFileViaSDK",
                                                                                 content: sendLoanFileRequest,
                                                                                 attachAuthorizationHeadersFromCurrentRequest: true
                                                                                );


            return response;
        }

        public async Task<CallResponse<ApiResponse<string>>> GetByteLoanStatusNameViaSDK(string byteFileName)
        {
            var response = await _httpClient.EasyGetAsync<ApiResponse<string>>(requestUri: $"{_baseUrl}/api/ByteWebConnector/LoanFile/GetLoanStatusNameViaSDK?byteFileName={byteFileName}",
                                                                                      attachAuthorizationHeadersFromCurrentRequest: true
                                                                                     );


            return response;
        }


        public class response
        {
            public short loanStatusId { get; set; }
        }


        #endregion

        #region Private Variables

        private readonly ILogger<ByteWebConnectorService> _logger;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl;

        #endregion
    }
}