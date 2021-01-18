using LosIntegration.API.ExtensionMethods;
using LosIntegration.API.Models;
using LosIntegration.API.Models.ClientModels.Document;
using LosIntegration.API.Models.Document;
using LosIntegration.API.Models.LoanApplication;
using LosIntegration.Entity.Models;
using LosIntegration.Service;
using LosIntegration.Service.Interface;
using LosIntegration.Service.InternalServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServiceCallHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AddDocumentRequest = LosIntegration.API.Models.Document.AddDocumentRequest;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LosIntegration.API.Controllers
{
    [Route(template: "api/LosIntegration/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        #region Constructors

        public DocumentController(IHttpClientFactory clientFactory,
                                  IConfiguration configuration,
                                  IMappingService mappingService,
                                  ILogger<DocumentController> logger,
                                  IByteDocTypeMappingService byteDocTypeMappingService,
                                  IByteDocStatusMappingService byteDocStatusMappingService,
                                  IRainmakerService rainmakerService)
        {
            _configuration = configuration;
            _mappingService = mappingService;
            _logger = logger;
            _byteDocTypeMappingService = byteDocTypeMappingService;
            
            _byteDocStatusMappingService = byteDocStatusMappingService;
            _httpClient = clientFactory.CreateClient(name: "clientWithCorrelationId");
            _rainmakerService = rainmakerService;
        }

        #endregion

        #region Private Variables

        private readonly IConfiguration _configuration;
        private readonly IMappingService _mappingService;
        private readonly HttpClient _httpClient;
        private readonly ILogger<DocumentController> _logger;
        private readonly IByteDocTypeMappingService _byteDocTypeMappingService;
        
        private readonly IByteDocStatusMappingService _byteDocStatusMappingService;
        private readonly IRainmakerService _rainmakerService;

        #endregion

        #region Action Methods

        // GET: api/<DocumentController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new[]
                   {
                       "value1",
                       "value2"
                   };
        }


        // GET api/<DocumentController>/5
        [HttpGet(template: "{id}")]
        public string Get(int id)
        {
            return "value";
        }


        // POST api/<DocumentController>
        [Route(template: "[action]")]
        [HttpPost]
        public async Task<IActionResult> SendFileToExternalOriginator(
        [FromBody] SendFileToExternalOriginatorRequest request)
        {
            var tenantId = "1";
            _httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue(scheme: "Bearer",
                                                parameter: Request
                                                           .Headers[key: "Authorization"].ToString()
                                                           .Replace(oldValue: "Bearer ",
                                                                    newValue: ""));

            #region GetFileDataFromDocumentManagement

            var documentResponse =
                GetFileDataFromDocumentManagement(documentLoanApplicationId: request.DocumentLoanApplicationId,
                                                  requestId: request.RequestId,
                                                  documentId: request.DocumentId,
                                                  fileId: request.FileId,
                                                  tenantId: tenantId);

            var fileData = await documentResponse.Content.ReadAsByteArrayAsync();
            _logger.LogInformation(message: $"DocSync SendFileToExternalOriginator :fileData {fileData} ");
            if (fileData == null) return BadRequest(error: "FileData is Null");

            #endregion

            #region GetDocument Attributes

            var getDocumentsCallResponse = Call.Get<List<DocumentManagementDocument>>(httpClient: _httpClient,
                                                                                      endPoint:
                                                                                      $"{_configuration[key: "ServiceAddress:DocumentManagement:Url"]}/api/DocumentManagement/BytePro/GetDocuments?loanApplicationId={request.LoanApplicationId}&tenantId=1&pending=false",
                                                                                      request: Request,
                                                                                      attachBearerTokenFromCurrentRequest
                                                                                      : true);

            if (!getDocumentsCallResponse.HttpResponseMessage.IsSuccessStatusCode)
                return BadRequest(error: "Unable to get all documents from DocumentManagement");
            var documentManagementDocuments = getDocumentsCallResponse.ResponseObject;

            var document = documentManagementDocuments.Single(predicate: d => d.DocId == request.DocumentId);
            var file = document.Files.Single(predicate: f => f.Id == request.FileId);

            //GetDocument Categories
            //var getCategoriesResponse = Call.Get<List<DocumentCategory>>(httpClient: _httpClient,
            //                                                             endPoint:
            //                                                             $"{_configuration[key: "ServiceAddress:DocumentManagement:Url"]}/api/DocumentManagement/BytePro/GetCategoryDocument",
            //                                                             request: Request,
            //                                                             attachBearerTokenFromCurrentRequest: true);
            //_logger.LogInformation(message:
            //                       $"DocSync SendFileToExternalOriginator :getCategoriesResponse {getCategoriesResponse} ");

            //if (!getCategoriesResponse.HttpResponseMessage.IsSuccessStatusCode)
            //    return BadRequest(error: "Unable to get all document categories from DocumentManagement");
            //var categories = getCategoriesResponse.ResponseObject;

            //var documentType = categories.SelectMany(selector: c => c.DocumentTypes)
            //                             .SingleOrDefault(predicate: dt => dt.DocTypeId == document.TypeId);

            ByteDocTypeMapping byteDocTypeMapping = null;
            if (document?.DocName != null)
            {
                byteDocTypeMapping = _byteDocTypeMappingService
                                     .GetByteDocTypeMappingWithDetails(docType: document?.DocName,
                                                                       includes: ByteDocTypeMappingService
                                                                                 .RelatedEntities.ByteDocCategoryMapping)
                                     .SingleOrDefault();
                _logger.LogInformation(message:
                                       $"DocSync DocType Mapping  {document?.DocName} => {byteDocTypeMapping?.ByteDoctypeName} ");
            }

            if (byteDocTypeMapping == null)
            {
                _logger.LogInformation(message: "DocSync DocType Mapping  NOT Found reverting to fail safe Type ");
                byteDocTypeMapping = _byteDocTypeMappingService
                                     .GetByteDocTypeMappingWithDetails(docType: "Other",
                                                                       includes: ByteDocTypeMappingService
                                                                                 .RelatedEntities.ByteDocCategoryMapping)
                                     .Single();
            }

            var byteDocCategoryMapping = byteDocTypeMapping.ByteDocCategoryMapping;

            var byteDocStatusMapping = _byteDocStatusMappingService
                                       .GetByteDocStatusMappingWithDetails(status: document.Status).SingleOrDefault();

            #endregion

            #region SendDocumentToExternalOriginator

            _logger.LogInformation(message: $"DocumentCategory={byteDocCategoryMapping.ByteDocCategoryName}");
            _logger.LogInformation(message: $"DocumentStatus={byteDocStatusMapping?.ByteDocStatusName}");
            _logger.LogInformation(message: $"DocumentType={byteDocTypeMapping.ByteDoctypeName}");
            _logger.LogInformation(message: $"MediaType={documentResponse.Content.Headers.ContentType.MediaType}");
            _logger.LogInformation(message: $"FileId={file.Id} is getting from SendFileToExternalOriginator");
            var sendDocumentRequest = new SendDocumentRequest
                                      {
                                          LoanApplicationId = request.LoanApplicationId,
                                          FileData = fileData,
                                          DocumentCategory =
                                              byteDocCategoryMapping.ByteDocCategoryName ?? "MISC", // mapping required
                                          DocumentExension = string.IsNullOrEmpty(value: file.McuName)
                                              ? Path.GetExtension(path: file.ClientName).Replace(oldValue: ".",
                                                                                                 newValue: "")
                                              : Path.GetExtension(path: file.McuName).Replace(oldValue: ".",
                                                                                                 newValue: ""),
                                          DocumentName = Path.GetFileNameWithoutExtension(path: !string.IsNullOrEmpty(file.McuName) ? file.McuName : file.ClientName),
                                          DocumentStatus =
                                              byteDocStatusMapping?.ByteDocStatusName ?? "0", // mapping required
                                          DocumentType =
                                              byteDocTypeMapping.ByteDoctypeName ?? "Other", // mapping required
                                          MediaType = documentResponse.Content.Headers.ContentType.MediaType,
                                          TenantId = Convert.ToInt32(tenantId)
                                      };
            DocumentResponse byteDocumentResponse ;
            try
            {
                byteDocumentResponse = await SendDocumentToExternalOriginator(sendDocumentRequest: sendDocumentRequest);
                if (byteDocumentResponse == null)
                    return BadRequest(error: "External originator document response null");
                _logger.LogInformation(message:
                                       $"DocSync SendFileToExternalOriginator :byteDocumentResponse {byteDocumentResponse} ");

                #region UpdateByteProStatus

                var updateByteProStatusResponse =
                    UpdateByteStatusInDocumentManagement(request: new UpdateByteStatusRequest
                                                                  {
                                                                      DocumentId = request.DocumentId,
                                                                      DocumentLoanApplicationId =
                                                                          request.DocumentLoanApplicationId,
                                                                      FileId = request.FileId,
                                                                      RequestId = document.RequestId,
                                                                      isUploaded = true
                                                                  });

                if (!updateByteProStatusResponse.IsSuccessStatusCode)
                    throw new LosIntegrationException(message: "Unable to Update Status in Document Management");

                #endregion
            }
            catch (Exception e)
            {
                _logger.LogInformation(message: $"DocSync1 SendFileToExternalOriginator :Exception {e.Message} ");
                #region SendEmail in case of sync fail

                await _rainmakerService.SendEmailSupportTeam(loanApplicationId: sendDocumentRequest.LoanApplicationId,
                                                            TenantId: sendDocumentRequest.TenantId,
                                                            ErrorDate: DateTime.Now.ToString(),
                                                            EmailBody: e.Message,
                                                            ErrorCode: (int)HttpStatusCode.InternalServerError,
                                                            DocumentCategory: sendDocumentRequest.DocumentCategory,
                                                            DocumentName: sendDocumentRequest.DocumentName,
                                                            DocumentExension: sendDocumentRequest.DocumentExension,
                                                            authHeader: Request
                                                                        .Headers[key: "Authorization"]
                                                                        .Select(selector: x => x.ToString()));
           
              
                #endregion
                #region UpdateByteProStatus

                var updateByteProStatusResponse =
                    UpdateByteStatusInDocumentManagement(request: new UpdateByteStatusRequest
                                                                  {
                                                                      DocumentId = request.DocumentId,
                                                                      DocumentLoanApplicationId =
                                                                          request.DocumentLoanApplicationId,
                                                                      FileId = request.FileId,
                                                                      RequestId = document.RequestId,
                                                                      isUploaded = false
                                                                  });

                if (!updateByteProStatusResponse.IsSuccessStatusCode)
                    throw new LosIntegrationException(message: "Unable to Update Status in Document Management");

                #endregion

                throw;
            }

            #endregion

            #region SaveMapping

            var mapping = new _Mapping
                          {
                              RMEnittyId = request.FileId,
                              RMEntityName = "File",
                              ExtOriginatorEntityId = byteDocumentResponse.DocumentId.ToString(),
                              ExtOriginatorEntityName = "Document",
                              ExtOriginatorId = byteDocumentResponse.ExtOriginatorId
                          };

            _logger.LogInformation(message: $"mapping params = {mapping.ToJsonString()}");
            _mappingService.Insert(item: mapping);
            await _mappingService.SaveChangesAsync();
            _logger.LogInformation(message: "mapping Saved");

            #endregion

            return Ok();
        }


        [Route(template: "[action]")]
        [HttpPost]
        public async Task<IActionResult> SendDocumentToExternalOriginator(
        [FromBody] SendDocumentToExternalOriginatorRequest request)
        {
            _httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue(scheme: "Bearer",
                                                parameter: Request
                                                           .Headers[key: "Authorization"].ToString()
                                                           .Replace(oldValue: "Bearer ",
                                                                    newValue: ""));
            //ask to make specific api
            var getAllFilesByLoanId = Call.Get<List<DocumentManagementDocument>>(httpClient: _httpClient,
                                                                                 endPoint:
                                                                                 $"{_configuration[key: "ServiceAddress:DocumentManagement:Url"]}/api/Documentmanagement/BytePro/GetDocuments?loanApplicationId={request.LoanApplicationId}&tenantId=1&pending=false",
                                                                                 request: Request,
                                                                                 attachBearerTokenFromCurrentRequest:
                                                                                 true).ResponseObject.ToList();
            var document = getAllFilesByLoanId.FirstOrDefault(predicate: x => x.DocId == request.DocumentId);
            if (document != null)
            {
                document.Files = document
                                 .Files.Where(predicate: x =>
                                                  x.ByteProStatus != $"{_configuration[key: "ByteProStatus:Key"]}")
                                 .ToList();
                foreach (var file in document.Files)
                {
                    var sendFileToExternalOriginatorRequest = new SendFileToExternalOriginatorRequest();
                    sendFileToExternalOriginatorRequest.DocumentId = request.DocumentId;
                    sendFileToExternalOriginatorRequest.DocumentLoanApplicationId = request.DocumentLoanApplicationId;
                    sendFileToExternalOriginatorRequest.RequestId = request.RequestId;
                    sendFileToExternalOriginatorRequest.DocumentId = request.DocumentId;
                    sendFileToExternalOriginatorRequest.FileId = file.Id;
                    sendFileToExternalOriginatorRequest.LoanApplicationId = request.LoanApplicationId;
                    await SendFileToExternalOriginator(request: sendFileToExternalOriginatorRequest);
                }

                return Ok();
            }

            return BadRequest();
        }


        private HttpResponseMessage UpdateByteStatusInDocumentManagement(UpdateByteStatusRequest request)
        {
            var url =
                $"{_configuration[key: "ServiceAddress:DocumentManagement:Url"]}/api/Documentmanagement/BytePro/UpdateByteProStatus";
            _logger.LogInformation(message: $"request.ToJsonString() = {request.ToJsonString()}");
            var updateByteProStatusResponse = _httpClient.PostAsync(requestUri: url,
                                                                    content: new StringContent(content: request
                                                                                                   .ToJsonString(),
                                                                                               encoding: Encoding
                                                                                                   .UTF8,
                                                                                               mediaType:
                                                                                               "application/json"))
                                                         .Result;
            _logger.LogInformation(message: $"updateByteProStatusResponse = {updateByteProStatusResponse}");
            return updateByteProStatusResponse;
        }


        // POST api/<DocumentController>
        [Route(template: "[action]")]
        [HttpPost]
        public async Task<IActionResult> AddDocument([FromBody] AddDocumentRequest request)
        {
            var fileIds = new List<string>();
            //--Get LoanApplication Id from rm by externalLoan Application Id
            var token = Request
                        .Headers[key: "Authorization"].ToString()
                        .Replace(oldValue: "Bearer ",
                                 newValue: "");

            _httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue(scheme: "Bearer",
                                                parameter: token);

            var uri =
                $"{_configuration[key: "ServiceAddress:RainMaker:Url"]}/api/rainmaker/LoanApplication/GetLoanApplication?encompassNumber={request.FileDataId}";
            var loanApplicationHttpResponseMessage = _httpClient.GetAsync(requestUri: uri).Result;
            _logger.LogInformation(message:
                                   $"loanApplicationHttpResponseMessage.IsSuccessStatusCode = {loanApplicationHttpResponseMessage.IsSuccessStatusCode}");
            if (loanApplicationHttpResponseMessage.IsSuccessStatusCode)
            {
                var loanApplicationResult = loanApplicationHttpResponseMessage.Content.ReadAsStringAsync().Result;
                var loanApplicationResponseModel =
                    JsonConvert.DeserializeObject<LoanApplicationResponse>(value: loanApplicationResult);

                // get all files from document mang by loanapplication Id
                if (loanApplicationResponseModel != null)
                {
                    var loanApplicationId = loanApplicationResponseModel.Id;
                    _logger.LogInformation(message: $"LoanApplicationId = {loanApplicationResponseModel.Id}");
                    var getDocumentsUrl =
                        $"{_configuration[key: "ServiceAddress:DocumentManagement:Url"]}/api/DocumentManagement/admindashboard/GetDocuments?loanApplicationId={loanApplicationResponseModel.Id}&pending={false}";
                    var getDocumentResponse = _httpClient.GetAsync(requestUri: getDocumentsUrl).Result;

                    if (getDocumentResponse != null)
                    {
                        var documentResponseResult = getDocumentResponse.Content.ReadAsStringAsync().Result;
                        var documentManagementDocument =
                            JsonConvert
                                .DeserializeObject<List<DocumentManagementDocument>>(value: documentResponseResult);
                        if (documentManagementDocument != null)
                            fileIds = documentManagementDocument
                                      .SelectMany(selector: document => document.Files)
                                      .Select(selector: file => file.Id).ToList();
                    }

                    // --fetch mapping details to identify newly added file in Byte => embeddedDocId
                    var mappings = _mappingService.GetMapping(rmEnittyIds: fileIds,
                                                              rmEntityName: "File").ToList();
                    var fileIdsOnByte = request.EmbeddedDocs.Select(selector: doc => doc.DocumentId.ToString());
                    var fileIdsOnRm = mappings.Select(selector: map => map.ExtOriginatorEntityId);

                    var fileIdsAbsentOnRm = fileIdsOnByte.Except(second: fileIdsOnRm);

                    foreach (var fileIdAbsentOnRm in fileIdsAbsentOnRm)
                    {
                        // -- gey newly added file from BWC
                        var documentDataRequest = new GetDocumentDataRequest
                                                  {
                                                      FileDataId = request.FileDataId,
                                                      DocumentId = Convert.ToInt32(value: fileIdAbsentOnRm)
                                                  };
                        var documentDataResult =
                            _httpClient.PostAsync(requestUri:
                                                  $"{_configuration[key: "ServiceAddress:ByteWebConnector:Url"]}/api/ByteWebConnector/Document/GetDocumentDataFromByte",
                                                  content: new
                                                      StringContent(content: documentDataRequest.ToJsonString(),
                                                                    encoding: Encoding.UTF8,
                                                                    mediaType: "application/json")).Result;
                        if (documentDataResult.IsSuccessStatusCode)
                        {
                            var documentResult = documentDataResult.Content.ReadAsStringAsync().Result;
                            var embeddedDocModel = JsonConvert.DeserializeObject<EmbeddedDoc>(value: documentResult);

                            // push newly added file to DOC management
                            var uploadFileRequestContent = new UploadFileRequest
                                                           {
                                                               LoanApplicationId = loanApplicationId,
                                                               DocumentType =
                                                                   embeddedDocModel
                                                                       .DocumentType, // GetRainmakerTypeFromMapping(embeddedDocModel.DocumentType),
                                                               FileName = embeddedDocModel.DocumentName + "." +
                                                                          embeddedDocModel.DocumentExension,
                                                               FileData = embeddedDocModel.DocumentData
                                                           }.ToJsonString();
                            _logger.LogInformation(message: $"uploadFileRequestContent = {uploadFileRequestContent}");
                            var url =
                                $"{_configuration[key: "ServiceAddress:DocumentManagement:Url"]}/api/Documentmanagement/request/UploadFile";
                            var uploadFileRequestMessage = new HttpRequestMessage
                                                           {
                                                               Content =
                                                                   new StringContent(content: uploadFileRequestContent,
                                                                                     encoding: Encoding.UTF8,
                                                                                     mediaType: "application/json"),
                                                               Method = HttpMethod.Post,
                                                               RequestUri = new Uri(uriString: url)
                                                           };
                            var uploadFileHttpResponse =
                                _httpClient.SendAsync(request: uploadFileRequestMessage).Result;
                            if (uploadFileHttpResponse.IsSuccessStatusCode)
                            {
                                var uploadFileResponseResult =
                                    uploadFileHttpResponse.Content.ReadAsStringAsync().Result;
                                var uploadFileResponseModel =
                                    JsonConvert.DeserializeObject<UploadFileResponse>(value: uploadFileResponseResult);

                                //-- update mapping
                                if (uploadFileResponseModel != null)
                                {
                                    var mapping = new _Mapping
                                                  {
                                                      RMEnittyId =
                                                          uploadFileResponseModel
                                                              .FileId, // comes from doc management after upload
                                                      RMEntityName = "File",
                                                      ExtOriginatorEntityId = fileIdAbsentOnRm,
                                                      ExtOriginatorEntityName = "Document",
                                                      ExtOriginatorId = 1
                                                  };
                                    _mappingService.Insert(item: mapping);
                                }
                            }
                        }
                    }

                    await _mappingService.SaveChangesAsync();

                    return Ok();
                }
            }

            return BadRequest();
        }


        // PUT api/<DocumentController>/5
        [HttpPut(template: "{id}")]
        public void Put(int id,
                        [FromBody] string value)
        {
            throw new NotSupportedException();
        }


        // DELETE api/<DocumentController>/5
        [HttpPost]
        [Route(template: "[action]")]
        public async Task<IActionResult> Delete(DeleteRequest request)
        {
            var mapping = _mappingService
                          .GetMappingWithDetails(extOriginatorEntityId: request.ExtOriginatorFileId.ToString(),
                                                 extOriginatorEntityName: "Document",
                                                 rmEntityName: "File",
                                                 extOriginatorId: request.ExtOriginatorId).SingleOrDefault();

            if (mapping != null)
            {
                var token = Request
                            .Headers[key: "Authorization"].ToString()
                            .Replace(oldValue: "Bearer ",
                                     newValue: "");

                _httpClient.DefaultRequestHeaders.Authorization
                    = new AuthenticationHeaderValue(scheme: "Bearer",
                                                    parameter: token);

                var uri =
                    $"{_configuration[key: "ServiceAddress:RainMaker:Url"]}/api/rainmaker/LoanApplication/GetLoanApplication?encompassNumber={request.ExtOriginatorLoanApplicationId.ToString()}";
                var httpResponseMessage = _httpClient.GetAsync(requestUri: uri).Result;

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var result = httpResponseMessage.Content.ReadAsStringAsync().Result;
                    _logger.LogInformation(message: $"LoanApplicationResult = {result}");
                    var loanApplicationId = JObject.Parse(json: result)
                                                   .SelectToken(path: "id")
                                                   .Value<int>();

                    var content = new DeleteFileRequest
                                  {
                                      LoanApplicationId = loanApplicationId,
                                      FileId = mapping.RMEnittyId
                                  }.ToJsonString();

                    var url =
                        $"{_configuration[key: "ServiceAddress:DocumentManagement:Url"]}/api/DocumentManagement/document/DeleteFile";
                    var delRequest = new HttpRequestMessage
                                     {
                                         Content = new StringContent(content: content,
                                                                     encoding: Encoding.UTF8,
                                                                     mediaType: "application/json"),
                                         Method = HttpMethod.Delete,
                                         RequestUri = new Uri(uriString: url)
                                     };
                    var csResponse = _httpClient.SendAsync(request: delRequest).Result;
                    if (csResponse.IsSuccessStatusCode)
                    {
                        _mappingService.Delete(item: mapping);
                        await _mappingService.SaveChangesAsync();
                        return Ok();
                    }
                }
            }

            return BadRequest();
        }

        #endregion

        #region Private Methods

        private HttpResponseMessage GetFileDataFromDocumentManagement(string documentLoanApplicationId,
                                                                      string requestId,
                                                                      string documentId,
                                                                      string fileId,
                                                                      string tenantId)
        {
            _logger.LogInformation(message: $"request.DocumentLoanApplicationId = {documentLoanApplicationId}");
            _logger.LogInformation(message: $"request.RequestId = {requestId}");
            _logger.LogInformation(message: $"request.DocumentId = {documentId}");
            _logger.LogInformation(message: $"request.FileId = {fileId}");

            var documentResponse = _httpClient
                                   .GetAsync(requestUri:
                                             $"{_configuration[key: "ServiceAddress:DocumentManagement:Url"]}/api/DocumentManagement/BytePro/view?id={documentLoanApplicationId}&requestId={requestId}&docId={documentId}&fileId={fileId}&tenantId={tenantId}")
                                   .Result;
            _logger.LogInformation(message:
                                   $"DocSync GetFileDataFromDocumentManagement :documentResponse {documentResponse} ");

            if (!documentResponse.IsSuccessStatusCode)
                throw new LosIntegrationException(message: "Unable to load Document from Document Management");

            return documentResponse;
        }


        private async Task<DocumentResponse> SendDocumentToExternalOriginator(SendDocumentRequest sendDocumentRequest)
        {
            
                var externalOriginatorSendDocumentResponse =
                    _httpClient.PostAsync(requestUri:
                                          $"{_configuration[key: "ServiceAddress:ByteWebConnector:Url"]}/api/ByteWebConnector/Document/SendDocument",
                                          content: new StringContent(content: sendDocumentRequest.ToJsonString(),
                                                                     encoding: Encoding.UTF8,
                                                                     mediaType: "application/json")).Result;

                _logger.LogInformation(message:
                                       $"externalOriginatorSendDocumentResponse = {externalOriginatorSendDocumentResponse}");

                if (!externalOriginatorSendDocumentResponse.IsSuccessStatusCode)
                {
                    await _rainmakerService.SendEmailSupportTeam(loanApplicationId: sendDocumentRequest.LoanApplicationId,
                                                                TenantId: sendDocumentRequest.TenantId,
                                                                ErrorDate: DateTime.Now.ToString(),
                                                                EmailBody: externalOriginatorSendDocumentResponse
                                                                    .ReasonPhrase,
                                                                ErrorCode: (int)HttpStatusCode.InternalServerError,
                                                                DocumentCategory: sendDocumentRequest.DocumentCategory,
                                                                DocumentName: sendDocumentRequest.DocumentName,
                                                                DocumentExension: sendDocumentRequest.DocumentExension,
                                                                authHeader: Request
                                                                            .Headers[key: "Authorization"]
                                                                            .Select(selector: x => x.ToString()));

                    throw new LosIntegrationException(message: "Unable to Upload Document to External Originator");
                }

                _logger.LogInformation(message:
                                       $"externalOriginatorSendDocumentResponse.IsSuccessStatusCode = {externalOriginatorSendDocumentResponse.IsSuccessStatusCode}");

                var result = externalOriginatorSendDocumentResponse.Content.ReadAsStringAsync().Result;

                _logger.LogInformation(message:
                                       $"DocSync SendDocumentToExternalOriginator :externalOriginatorSendDocumentResponse {result} ");
                
                if (!externalOriginatorSendDocumentResponse.IsSuccessStatusCode)
                {
                    #region SendEmail in case of sync fail

                    await _rainmakerService.SendEmailSupportTeam(loanApplicationId: sendDocumentRequest.LoanApplicationId,
                                                                TenantId: sendDocumentRequest.TenantId,
                                                                ErrorDate: DateTime.Now.ToString(),
                                                                EmailBody: result,
                                                                ErrorCode: (int)HttpStatusCode.InternalServerError,
                                                                DocumentCategory: sendDocumentRequest.DocumentCategory,
                                                                DocumentName: sendDocumentRequest.DocumentName,
                                                                DocumentExension: sendDocumentRequest.DocumentExension,
                                                                authHeader: Request
                                                                            .Headers[key: "Authorization"]
                                                                            .Select(selector: x => x.ToString()));

                    #endregion

                   
                    throw new LosIntegrationException(message: "Unable to deserialize External Originator document ");
                }
            var apiResponse = JsonConvert.DeserializeObject<DocumentResponse>(value: result);
            _logger.LogInformation(message:
                                   $"DocSync SendDocumentToExternalOriginator :Deserialize apiResponse {apiResponse} ");
            _logger.LogInformation(message: "Deserialize Successfully");
            return apiResponse;

        }

        #endregion
    }
}