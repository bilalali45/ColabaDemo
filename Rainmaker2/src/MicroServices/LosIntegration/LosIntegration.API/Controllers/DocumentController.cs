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
            jump :
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
            //--Get LoanApplication Id from rm by externalLoan Application Id
            var loanApplicationRequestContent = new GeLoanApplicationRequest
                                                {
                                                    EncompassNumber = request.FileDataId.ToString()
                                                }.ToJsonString();
            var loanlApplicationResult =
                _httpClient.PostAsync(requestUri:
                                      $"{_configuration[key: "ServiceAddress:RainMaker:Url"]}/api/rainmaker/LoanApplication/GetLoanApplication",
                                      content: new StringContent(content: loanApplicationRequestContent.ToJsonString(),
                                                                 encoding: Encoding.UTF8,
                                                                 mediaType: "application/json")).Result;

            // get all files from document mang by loanapplication Id

            List<string> fileIds = null; // from Doc Managment;

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
                var documentDaResult =
                    _httpClient.PostAsync(requestUri:
                                          $"{_configuration[key: "ServiceAddress:ByteWebConnector:Url"]}/api/ByteWebConnector/Document/GetDocumentDataFromByte",
                                          content: new StringContent(content: documentDataRequest.ToJsonString(),
                                                                     encoding: Encoding.UTF8,
                                                                     mediaType: "application/json")).Result;

                // push newly added file to DOC management
                string fileId = null;// comes from doc management after upload

                //-- update mapping
                var mapping = new Mapping
                              {
                                  RMEnittyId = fileId,
                                  RMEntityName = "File",
                                  ExtOriginatorEntityId = fileIdAbsentOnRm,
                                  ExtOriginatorEntityName = "Document",
                                  ExtOriginatorId = 1
                              };
                _mappingService.Insert(item: mapping);
            }

            _mappingService.SaveChangesAsync();

            return Ok();
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
                var loanApplicationRequestContent = new GeLoanApplicationRequest
                                                    {
                                                        EncompassNumber =
                                                            request.ExtOriginatorLoanApplicationId.ToString()
                                                    }.ToJsonString();

                var callResponse =
                    _httpClient.PostAsync(requestUri:
                                          $"{_configuration[key: "RainMaker:Url"]}/api/rainmaker/LoanApplication/GetLoanApplication",
                                          content: new StringContent(content: loanApplicationRequestContent,
                                                                     encoding: Encoding.UTF8,
                                                                     mediaType: "application/json")).Result;
                //var apiResponse = JsonConvert.DeserializeObject(value: result);
                var loanApplicationId = JObject.Parse(json: callResponse.Content.ReadAsStringAsync().Result)
                                               .SelectToken(path: "id")
                                               .Value<int>();

                var content = new DeleteFileRequest
                              {
                                  LoanApplicationId = loanApplicationId,
                                  FileId = mapping.RMEnittyId
                              }.ToJsonString();

                var url =
                    "https://alphamaingateway.rainsoftfn.com/api/Documentmanagement/document/delete";
                var csResponse = _httpClient.PostAsync(requestUri: url,
                                                       content: new StringContent(content: content,
                                                                                  encoding: Encoding.UTF8,
                                                                                  mediaType: "application/json"))
                                            .Result;

                _mappingService.Delete(item: mapping);
                _mappingService.SaveChangesAsync();
            }
        }

        #endregion

        #region Private Methods

        #endregion
    }
}