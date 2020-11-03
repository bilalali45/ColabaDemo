using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ByteWebConnector.API.ExtensionMethods;
using ByteWebConnector.API.Utility;
using ByteWebConnector.Model.Models;
using ByteWebConnector.Model.Models.ActionModels.Document;
using ByteWebConnector.Model.Models.OwnModels;
using ByteWebConnector.Model.Models.OwnModels.Settings;
using ByteWebConnector.Model.Models.ServiceRequestModels.ByteWebConnectorSDK;
using ByteWebConnector.Model.Models.ServiceResponseModels.ByteApi;
using ByteWebConnector.Model.Models.ServiceResponseModels.ByteWebConnectorSDK;
using ByteWebConnector.Service.DbServices;
using ByteWebConnector.Service.ExternalServices;
using ByteWebConnector.Service.InternalServices;
using Extensions.ExtensionClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ByteWebConnector.API.Controllers
{
    [Route(template: "api/ByteWebConnector/[controller]")]
    [ApiController]
    [Authorize(Roles = "MCU,Customer")]
    public class DocumentController : ControllerBase
    {
        #region StaticFields

        public static string ByteSession = string.Empty;

        #endregion

        #region Constructor

        public DocumentController(HttpClient httpClient,
                                  IConfiguration configuration,
                                  ILogger<DocumentController> logger,
                                  ISettingService settingService,
                                  IRainmakerService rainmakerService,
                                  IByteWebConnectorSdkService byteWebConnectorSdkService,
                                  IByteProService byteProService,
                                  ILosIntegrationService losIntegrationService)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
            var _settingService = settingService;
            _rainmakerService = rainmakerService;
            _byteWebConnectorSdkService = byteWebConnectorSdkService;
            ByteProSettings = _settingService.GetByteProSettings();
            _byteProService = byteProService;
            _losIntegrationService = losIntegrationService;
        }

        #endregion

        #region Properties

        public ByteProSettings ByteProSettings { get; set; }

        #endregion

        #region Private Methods

        private EmbeddedDoc GetEmbeddedDocData(string byteProSession,
                                               int documentId,
                                               int fileDataId)  //todo talha move thid method to service
        {
            var request =
                (HttpWebRequest)WebRequest.Create(requestUriString: ByteProSettings.ByteApiUrl + "Document/" +
                                                                     fileDataId + "/" + documentId);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Headers.Add(name: "Session",
                                value: byteProSession);
            request.Accept = "application/json";

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                var dataStream = response.GetResponseStream();
                var reader = new StreamReader(stream: dataStream);
                var responseString = reader.ReadToEnd();

                var embeddedDoc =
                    JsonConvert.DeserializeObject<EmbeddedDoc>(value: responseString);
                reader.Close();
                dataStream.Close();
                return embeddedDoc;
            }
        }

        #endregion

        #region Private Fields

        private readonly ILogger<DocumentController> _logger;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly IRainmakerService _rainmakerService;
        private readonly IByteWebConnectorSdkService _byteWebConnectorSdkService;
        private readonly IByteProService _byteProService;
        private readonly ILosIntegrationService _losIntegrationService;

        #endregion

        #region Action Methods



        [Route(template: "[action]")]
        [HttpPost]
        public ApiResponse<SendSdkDocumentResponse> SendDocument([FromBody] SendDocumentRequest request)
        {
            var loanApplication =
                _rainmakerService.GetLoanApplication(loanApplicationId: request.LoanApplicationId);

            if (loanApplication != null)
            {
                if (_configuration.GetSection(key: "MediaTypesToBeWrappedInPdf").Get<string[]>()
                                  .Contains(value: request.MediaType))
                {
                    var intput = new List<byte[]>();
                    intput.Add(item: request.FileData);
                    request.FileData = Helper.WrapImagesInPdf(input: intput).Single();
                    request.DocumentExension = "pdf";
                }

                var documentUploadModel = new DocumentUploadRequest
                {
                    FileDataId = Convert.ToInt64(value: loanApplication.ByteLoanNumber),
                    DocumentCategory = request.DocumentCategory,
                    DocumentExension = request.DocumentExension,
                    DocumentName = request.DocumentName,
                    DocumentStatus = request.DocumentStatus,
                    DocumentType = request.DocumentType,
                    DocumentData = request.FileData.ToBase64String(offset: 0,
                                                                                             length: request
                                                                                                     .FileData.Length)
                };

                #region BytePro API Call

                ByteSession = ByteSession.HasValue() ? ByteSession : _byteProService.GetByteProSession();
                if (!_byteProService.ValidateByteSession(byteSession: ByteSession)) ByteSession = _byteProService.GetByteProSession();

                var fileData = _byteProService.GetFileData(byteSession: ByteSession,
                                                           fileDataId: loanApplication.ByteLoanNumber);
                documentUploadModel.FileName = fileData.FileName;

                var sdkDocumentResponse = _byteWebConnectorSdkService.SendDocumentToByte(documentUploadRequest: documentUploadModel).ResponseObject;

                var apiResponse = new ApiResponse<SendSdkDocumentResponse>
                {
                    Status = sdkDocumentResponse.Status,
                    Data = sdkDocumentResponse.Data
                };

                return apiResponse;

                #endregion
            }

            return null;
        }



        [Route(template: "[action]")]
        [HttpPost]
        public async Task<IActionResult> Delete(DeleteRequest request)
        {
            var losModel = request.GetLosModel();

            var content = losModel.ToJson();

            var callResponse = _losIntegrationService.DocumentDelete(content: content);

            if (await callResponse) return Ok();

            return BadRequest();
        }


        [Route(template: "[action]")]
        [HttpPost]
        public async Task<IActionResult> DocumentAdded(DocumentAddedRequest request)
        {
            #region Byte API Call

            Thread.Sleep(millisecondsTimeout: 5000);
            var byteProSession = _byteProService.GetByteProSession();

            var embeddedDocs = _byteProService.GetAllByteDocuments(session: byteProSession,
                                                                   fileDataId: request.FileDataId);

            _logger.LogInformation(message: $"T====== Total embeddedDocs = {embeddedDocs.Count}");
            //var content = new AddDocumentRequest(fileDataId: request.FileDataId,
            //                                     embeddedDocs: embeddedDocs).ToJson();
            //var token = Request.Headers[key: "Authorization"].ToString().Replace(oldValue: "Bearer ",
            //                                                                     newValue: "");
            //_httpClient.DefaultRequestHeaders.Authorization
            //    = new AuthenticationHeaderValue(scheme: "Bearer",
            //                                    parameter: token);
            //var callResponse =
            //    await _httpClient.PostAsync(requestUri:
            //                                $"{_configuration[key: "ServiceAddress:LosIntegration:Url"]}/api/LosIntegration/Document/AddDocument",
            //                                content: new StringContent(content: content,
            //                                                           encoding: Encoding.UTF8,
            //                                                           mediaType: "application/json"));
            var callResponse = _losIntegrationService.DocumentAddDocument(fileDataId: request.FileDataId,
                                                                          embeddedDocs: embeddedDocs);

            if (await callResponse) return Ok();

            return BadRequest();

            #endregion
        }


        [Route(template: "[action]")]
        [HttpPost]
        public EmbeddedDoc GetDocumentDataFromByte(DocumentDataRequest request)
        {
            #region Byte API Call

            var byteProSession = _byteProService.GetByteProSession();

            var embeddedDocWithData = GetEmbeddedDocData(byteProSession: byteProSession,
                                                         documentId: request.DocumentId,
                                                         fileDataId: request.FileDataId);

            return embeddedDocWithData;

            #endregion
        }

        #endregion
    }
}