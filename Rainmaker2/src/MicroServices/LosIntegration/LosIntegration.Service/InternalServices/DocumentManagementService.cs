using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using LosIntegration.Model.Model.ActionModels.Document;
using LosIntegration.Model.Model.ServiceRequestModels.Document;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ServiceCallHelper;

namespace LosIntegration.Service.InternalServices
{
    public class DocumentManagementService : IDocumentManagementService
    {
        #region Constructors

        public DocumentManagementService(ILogger<DocumentManagementService> logger,
                                         HttpClient httpClient,
                                         IConfiguration configuration)
        {
            _logger = logger;
            _httpClient = httpClient;
            _configuration = configuration;
            _baseUrl = _configuration[key: "ServiceAddress:DocumentManagement:Url"];
        }

        #endregion

        #region Private Variables

        private readonly ILogger<DocumentManagementService> _logger;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl;

        #endregion

        #region Methods

        public async Task<HttpResponseMessage> GetFileDataFromDocumentManagement(string documentLoanApplicationId,
                                                                                 string requestId,
                                                                                 string documentId,
                                                                                 string fileId,
                                                                                 string tenantId)
        {
            var documentResponse = await _httpClient.EasyGetAsync<dynamic>(requestUri: $"{_baseUrl}/api/DocumentManagement/BytePro/view?id={documentLoanApplicationId}&requestId={requestId}&docId={documentId}&fileId={fileId}&tenantId={tenantId}",
                                                                           attachAuthorizationHeadersFromCurrentRequest: true,
                                                                                 deserializeResponse:false);

            if (!documentResponse.HttpResponseMessage.IsSuccessStatusCode)
                throw new Exception(message: "Unable to load Document from Document Management");

            return documentResponse.HttpResponseMessage;
        }


        public async Task<List<DocumentManagementDocument>> GetDocuments(int loanApplicationId)
        {
            var getDocumentsCallResponse = await _httpClient.EasyGetAsync<List<DocumentManagementDocument>>(
                                                                                                            requestUri: $"{_baseUrl}/api/DocumentManagement/BytePro/GetDocuments?loanApplicationId={loanApplicationId}&tenantId=1&pending=false",
                                                                                                            
                                                                                                            attachAuthorizationHeadersFromCurrentRequest: true
                                                                                                           );
            if (!getDocumentsCallResponse.HttpResponseMessage.IsSuccessStatusCode)
                throw new Exception(message: "Unable to get all documents from DocumentManagement");

            return getDocumentsCallResponse.ResponseObject;
        }


        public async Task<List<DocumentCategory>> GetDocumentCategories()
        {
            var getCategoriesResponse = await _httpClient.EasyGetAsync<List<DocumentCategory>>(
                                                                                               requestUri: $"{_baseUrl}/api/DocumentManagement/BytePro/GetCategoryDocument",
                                                                                               
                                                                                               attachAuthorizationHeadersFromCurrentRequest: true
                                                                                              );
            if (!getCategoriesResponse.HttpResponseMessage.IsSuccessStatusCode)
                throw new Exception(message: "Unable to get all document categories from DocumentManagement");

            return getCategoriesResponse.ResponseObject;
        }


        public async Task<bool> UpdateByteStatusInDocumentManagement(UpdateByteStatusRequest updateByteStatusRequest)
        {
            var updateByteProStatusResponse =
                await _httpClient.EasyPostAsync<bool>(requestUri: $"{_baseUrl}/api/Documentmanagement/BytePro/UpdateByteProStatus",
                                                      content: updateByteStatusRequest
                                                      );
            return updateByteProStatusResponse.ResponseObject;
        }

        public async Task<HttpResponseMessage> GetDocuments(int loanApplicationId, bool pending)
        {
            var documentResponse = _httpClient.EasyGetAsync<byte[]>(requestUri: $"{_baseUrl}/api/DocumentManagement/admindashboard/GetDocuments?loanApplicationId={loanApplicationId}&pending={false}",
                                                                    
                                                                    attachAuthorizationHeadersFromCurrentRequest: true).Result;

            return documentResponse.HttpResponseMessage;
        }

        #endregion
    }
}