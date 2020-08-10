using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using LosIntegration.API.ExtensionMethods;
using LosIntegration.API.Models;
using LosIntegration.API.Models.ClientModels.Document;
using LosIntegration.API.Models.ClientModels.LoanApplication;
using LosIntegration.API.Models.Document;
using LosIntegration.API.Models.LoanApplication;
using LosIntegration.Entity.Models;
using LosIntegration.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
                                  IMappingService mappingService)
        {
            _configuration = configuration;
            _mappingService = mappingService;
            _httpClient = clientFactory.CreateClient(name: "clientWithCorrelationId");
            //_tokenService = tokenService;
            //_logger = logger;
        }

        #endregion

        #region Private Variables

        private readonly IConfiguration _configuration;
        private readonly IMappingService _mappingService;
        private readonly HttpClient _httpClient;

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
        public IActionResult SendToExternalOriginator([FromBody] SendToExternalOriginatorRequest request)
        {
            var tenantId = "1";
            goto jump;
            _httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue(scheme: "Bearer",
                                                parameter: Request
                                                           .Headers[key: "Authorization"].ToString()
                                                           .Replace(oldValue: "Bearer ",
                                                                    newValue: ""));

            //var csResponse = _httpClient.GetAsync($"{_configuration["ServiceAddress:DocumentManagement:Url"]}/api/DocumentManagement/document/view?id={request.DocumentLoanApplicationId}&requestId={request.RequestId}&docId={request.DocumentId}&fileId={request.FileId}&tenantId={tenantId}").Result;
            var documentRequestUri =
                $"https://alphamaingateway.rainsoftfn.com/api/documentmanagement/document/view?id={request.DocumentLoanApplicationId}&requestId={request.RequestId}&docId={request.DocumentId}&fileId={request.FileId}&tenantId={tenantId}";
            var documentResponse = _httpClient.GetAsync(requestUri: documentRequestUri).Result;

            if (!documentResponse.IsSuccessStatusCode)
                throw new Exception(message: "Unable to load Document from Document Management");

            //var fileData = documentResponse.Content.ReadAsByteArrayAsync().Result;
            jump:
            var fileData = System.IO.File.ReadAllBytes(path: @"C:\Users\H)P\Desktop\LOAN INFO.docx");
            var sendDocumentResponse = new SendDocumentResponse
            {
                LoanApplicationId = request.LoanApplicationId,
                FileData = fileData
            };
            var callResponse =
                _httpClient.PostAsync(requestUri:
                                      $"{_configuration[key: "ServiceAddress:ByteWebConnector:Url"]}/api/ByteWebConnector/Document/SendDocument",
                                      content: new StringContent(content: sendDocumentResponse.ToJsonString(),
                                                                 encoding: Encoding.UTF8,
                                                                 mediaType: "application/json")).Result;
            var result = callResponse.Content.ReadAsStringAsync().Result;
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(value: result);
            if (apiResponse.Status == ApiResponse.ApiResponseStatus.Success)
            {
                DocumentResponse response = JsonConvert.DeserializeObject<DocumentResponse>(apiResponse.Data);
                var mapping = new Mapping
                {
                    RMEnittyId = request.FileId,
                    RMEntityName = "File",
                    ExtOriginatorEntityId = response.DocumentId.ToString(),
                    ExtOriginatorEntityName = "Document",
                    ExtOriginatorId = response.ExtOriginatorId
                };
                _mappingService.Insert(item: mapping);
                _mappingService.SaveChangesAsync();

                var url =
                    "https://alphamaingateway.rainsoftfn.com/api/Documentmanagement/document/UpdateByteProStatus";
                var csResponse = _httpClient.PostAsync(requestUri: url,
                                                       content: new StringContent(content: request.ToJsonString(),
                                                                                  encoding: Encoding.UTF8,
                                                                                  mediaType: "application/json"))
                                            .Result;
                return Ok();
            }

            return BadRequest();
        }


        // POST api/<DocumentController>
        [Route(template: "[action]")]
        [HttpPost]
        public IActionResult AddDocument([FromBody] AddDocumentRequest request)
        {
            List<string> fileIds = new List<string>();
            //--Get LoanApplication Id from rm by externalLoan Application Id
            var loanApplicationRequestContent = new GeLoanApplicationRequest
            {
                EncompassNumber = request.FileDataId.ToString()
            }.ToJsonString();

            HttpResponseMessage loanApplicationHttpResponseMessage = _httpClient.PostAsync(requestUri:
                                                                                                 $"{_configuration[key: "ServiceAddress:RainMaker:Url"]}/api/rainmaker/LoanApplication/GetLoanApplication",
                                                                                                 content: new StringContent(content: loanApplicationRequestContent.ToJsonString(),
                                                                                                                            encoding: Encoding.UTF8,
                                                                                                                            mediaType: "application/json")).Result;
            if (loanApplicationHttpResponseMessage.IsSuccessStatusCode)
            {
                string loanApplicationResult = loanApplicationHttpResponseMessage.Content.ReadAsStringAsync().Result;
                LoanApplicationResponse loanApplicationResponseModel = JsonConvert.DeserializeObject<LoanApplicationResponse>(value: loanApplicationResult);

                // get all files from document mang by loanapplication Id
                if (loanApplicationResponseModel != null)
                {
                    int loanId = loanApplicationResponseModel.Id;
                    var getDocumentRequestContent = new GetDocumentsRequest()
                    {
                        LoanApplicationId = loanId
                    }.ToJsonString();

                    var getDocumentsUrl = $"{_configuration[key: "ServiceAddress:DocumentManagement:Url"]}/api/DocumentManagement/admindashboard/GetDocuments";
                    HttpRequestMessage getDocumentRequestMessage = new HttpRequestMessage
                                                                   {
                                                                       Content = new StringContent(getDocumentRequestContent, Encoding.UTF8, "application/json"),
                                                                       Method = HttpMethod.Get,
                                                                       RequestUri = new Uri(getDocumentsUrl)
                                                                   };
                    var getDocumentResponse = _httpClient.SendAsync(getDocumentRequestMessage).Result;
                    if (getDocumentResponse != null)
                    {
                        string documentResponseResult = getDocumentResponse.Content.ReadAsStringAsync().Result;
                        List<DocumentManagementDocument> documentManagementDocument = JsonConvert.DeserializeObject<List<DocumentManagementDocument>>(value: documentResponseResult);
                        if (documentManagementDocument != null)
                            fileIds = documentManagementDocument
                                      .SelectMany(document => document.Files).Select(file => file.Id).ToList();
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
                            DocumentId = Convert.ToInt32(fileIdAbsentOnRm)
                        };
                        var documentDataResult =
                            _httpClient.PostAsync(requestUri:
                                                  $"{_configuration[key: "ServiceAddress:ByteWebConnector:Url"]}/api/ByteWebConnector/Document/GetDocumentDataFromByte",
                                                  content: new StringContent(content: documentDataRequest.ToJsonString(),
                                                                             encoding: Encoding.UTF8,
                                                                             mediaType: "application/json")).Result;
                        if (documentDataResult.IsSuccessStatusCode)
                        {
                            string documentResult = documentDataResult.Content.ReadAsStringAsync().Result;
                            EmbeddedDoc embeddedDocModel = JsonConvert.DeserializeObject<EmbeddedDoc>(value: documentResult);

                            // push newly added file to DOC management
                            var uploadFileRequestContent = new UploadFileRequest()
                            {
                                LoanApplicationId = loanId,
                                DocumentType = embeddedDocModel.DocumentType,
                                FileName = embeddedDocModel.DocumentName +"."+ embeddedDocModel.DocumentExension,
                                FileData =  embeddedDocModel.DocumentData
                            }.ToJsonString();

                            var url = $"{_configuration[key: "ServiceAddress:DocumentManagement:Url"]}/api/Documentmanagement/request/UploadFile";
                            HttpRequestMessage uploadFileRequestMessage = new HttpRequestMessage
                            {
                                Content = new StringContent(uploadFileRequestContent, Encoding.UTF8, "application/json"),
                                Method = HttpMethod.Get,
                                RequestUri = new Uri(url)
                            };
                            HttpResponseMessage uploadFileHttpResponse = _httpClient.SendAsync(uploadFileRequestMessage).Result;
                            if (uploadFileHttpResponse.IsSuccessStatusCode)
                            {
                                string uploadFileResponseResult = uploadFileHttpResponse.Content.ReadAsStringAsync().Result;
                                UploadFileResponse uploadFileResponseModel = JsonConvert.DeserializeObject<UploadFileResponse>(value: uploadFileResponseResult);

                                //-- update mapping
                                if (uploadFileResponseModel != null)
                                {
                                    var mapping = new Mapping
                                    {
                                        RMEnittyId = uploadFileResponseModel.FileId,// comes from doc management after upload
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

                    _mappingService.SaveChangesAsync();

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
        }


        // DELETE api/<DocumentController>/5
        [HttpPost]
        [Route(template: "[action]")]
        public void Delete(DeleteRequest request)
        {
            var mapping = _mappingService
                          .GetMappingWithDetails(extOriginatorEntityId: request.ExtOriginatorFileId.ToString(),
                                                 extOriginatorEntityName: "Document",
                                                 rmEntityName: "File",
                                                 extOriginatorId: request.ExtOriginatorId).SingleOrDefault();

            if (mapping != null)
            {
                //var loanApplicationRequestContent = new GeLoanApplicationRequest
                //                                    {
                //                                        EncompassNumber =
                //                                            request.ExtOriginatorLoanApplicationId.ToString()
                //                                    }.ToJsonString();

                //var callResponse =
                //    _httpClient.PostAsync(requestUri:
                //                          $"{_configuration[key: "ServiceAddress:RainMaker:Url"]}/api/rainmaker/LoanApplication/GetLoanApplication",
                //                          content: new StringContent(content: loanApplicationRequestContent,
                //                                                     encoding: Encoding.UTF8,
                //                                                     mediaType: "application/json")).Result;


                var token = Request
                            .Headers[key: "Authorization"].ToString()
                            .Replace(oldValue: "Bearer ",
                                     newValue: "");

                _httpClient.DefaultRequestHeaders.Authorization
                    = new AuthenticationHeaderValue(scheme: "Bearer",
                                                    parameter: token);

                string uri =
                    $"{_configuration[key: "ServiceAddress:RainMaker:Url"]}/api/rainmaker/LoanApplication/GetLoanApplication?encompassNumber={request.ExtOriginatorLoanApplicationId.ToString()}";
                HttpResponseMessage httpResponseMessage = _httpClient.GetAsync(requestUri: uri).Result;

                //var apiResponse = JsonConvert.DeserializeObject(value: result);
                var loanApplicationId = JObject.Parse(json: httpResponseMessage.Content.ReadAsStringAsync().Result)
                                               .SelectToken(path: "id")
                                               .Value<int>();

                var content = new DeleteFileRequest
                {
                    LoanApplicationId = loanApplicationId,
                    FileId = mapping.RMEnittyId
                }.ToJsonString();

                var url = $"{_configuration[key: "ServiceAddress:DocumentManagement:Url"]}/api/DocumentManagement/document/DeleteFile";
                HttpRequestMessage delRequest = new HttpRequestMessage
                {
                    Content = new StringContent(content, Encoding.UTF8, "application/json"),
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri(url)
                };
                var csResponse = _httpClient.SendAsync(delRequest).Result;
                if (csResponse.IsSuccessStatusCode)
                {
                    _mappingService.Delete(item: mapping);
                    _mappingService.SaveChangesAsync();
                }


            }
        }

        #endregion

        #region Private Methods

        #endregion
    }
}