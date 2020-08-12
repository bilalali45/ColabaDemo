﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
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
        public async Task<IActionResult> SendToExternalOriginator([FromBody] SendToExternalOriginatorRequest request)
        {
            var tenantId = "1";
            _httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue(scheme: "Bearer",
                                                parameter: Request
                                                           .Headers[key: "Authorization"].ToString()
                                                           .Replace(oldValue: "Bearer ",
                                                                    newValue: ""));

            var documentResponse = _httpClient.GetAsync($"{_configuration["ServiceAddress:DocumentManagement:Url"]}/api/DocumentManagement/document/view?id={request.DocumentLoanApplicationId}&requestId={request.RequestId}&docId={request.DocumentId}&fileId={request.FileId}&tenantId={tenantId}").Result;
            if (!documentResponse.IsSuccessStatusCode)
                throw new Exception(message: "Unable to load Document from Document Management");

            var fileData = documentResponse.Content.ReadAsByteArrayAsync().Result;

            var sendDocumentResponse = new SendDocumentResponse
            {
                LoanApplicationId = request.LoanApplicationId,
                FileData = fileData
            };
            var externalOriginatorSendDocumentResponse =
                _httpClient.PostAsync(requestUri:
                                      $"{_configuration[key: "ServiceAddress:ByteWebConnector:Url"]}/api/ByteWebConnector/Document/SendDocument",
                                      content: new StringContent(content: sendDocumentResponse.ToJsonString(),
                                                                 encoding: Encoding.UTF8,
                                                                 mediaType: "application/json")).Result;
            if (!externalOriginatorSendDocumentResponse.IsSuccessStatusCode)
                throw new Exception(message: "Unable to Upload Document to External Originator");
            var result = externalOriginatorSendDocumentResponse.Content.ReadAsStringAsync().Result;
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
                await _mappingService.SaveChangesAsync();



                var url = $"{_configuration[key: "ServiceAddress:DocumentManagement:Url"]}/api/Documentmanagement/document/UpdateByteProStatus";
                var updateByteProStatusResponse = _httpClient.PostAsync(requestUri: url,
                                                                content: new StringContent(content: request.ToJsonString(),
                                                                                           encoding: Encoding.UTF8,
                                                                                           mediaType: "application/json"))
                                                     .Result;
                if (!updateByteProStatusResponse.IsSuccessStatusCode)
                    throw new Exception(message: "Unable to Update Status in Document Management");
                return Ok();
            }

            return BadRequest();
        }


        // POST api/<DocumentController>
        [Route(template: "[action]")]
        [HttpPost]
        public async Task<IActionResult> AddDocument([FromBody] AddDocumentRequest request)
        {
            List<string> fileIds = new List<string>();
            //--Get LoanApplication Id from rm by externalLoan Application Id
            var loanApplicationRequestContent = new GeLoanApplicationRequest
            {
                EncompassNumber = request.FileDataId.ToString()
            }.ToJsonString();
            var token = Request
                        .Headers[key: "Authorization"].ToString()
                        .Replace(oldValue: "Bearer ",
                                 newValue: "");

            _httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue(scheme: "Bearer",
                                                parameter: token);

            string uri =
                $"{_configuration[key: "ServiceAddress:RainMaker:Url"]}/api/rainmaker/LoanApplication/GetLoanApplication?encompassNumber={request.FileDataId.ToString()}";
            HttpResponseMessage loanApplicationHttpResponseMessage = _httpClient.GetAsync(requestUri: uri).Result;

            if (loanApplicationHttpResponseMessage.IsSuccessStatusCode)
            {
                string loanApplicationResult = loanApplicationHttpResponseMessage.Content.ReadAsStringAsync().Result;
                LoanApplicationResponse loanApplicationResponseModel = JsonConvert.DeserializeObject<LoanApplicationResponse>(value: loanApplicationResult);

                // get all files from document mang by loanapplication Id
                if (loanApplicationResponseModel != null)
                {
                    int loanApplicationId = loanApplicationResponseModel.Id;
                    var getDocumentRequestContent = new GetDocumentsRequest()
                    {
                        LoanApplicationId = loanApplicationId
                    }.ToJsonString();

                    var getDocumentsUrl = $"{_configuration[key: "ServiceAddress:DocumentManagement:Url"]}/api/DocumentManagement/admindashboard/GetDocuments?loanApplicationId={loanApplicationResponseModel.Id}&pending={false}";
                    var getDocumentResponse = _httpClient.GetAsync(requestUri: getDocumentsUrl).Result;

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
                                LoanApplicationId = loanApplicationId,
                                DocumentType = embeddedDocModel.DocumentType,
                                FileName = embeddedDocModel.DocumentName +"."+ embeddedDocModel.DocumentExension,
                                FileData =  embeddedDocModel.DocumentData
                            }.ToJsonString();

                            var url = $"{_configuration[key: "ServiceAddress:DocumentManagement:Url"]}/api/Documentmanagement/request/UploadFile";
                            HttpRequestMessage uploadFileRequestMessage = new HttpRequestMessage
                            {
                                Content = new StringContent(uploadFileRequestContent, Encoding.UTF8, "application/json"),
                                Method = HttpMethod.Post,
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
                  await  _mappingService.SaveChangesAsync();

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
                    await _mappingService.SaveChangesAsync();
                    return Ok();
                }
            }

            return BadRequest();
        }

        #endregion

        #region Private Methods

        #endregion
    }
}