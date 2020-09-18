using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ByteWebConnector.API.ExtensionMethods;
using ByteWebConnector.API.Models;
using ByteWebConnector.API.Models.ClientModels.Document;
using ByteWebConnector.API.Models.Document;
using ByteWebConnector.API.Utility;
using ByteWebConnector.Model.Models.ByteApi;
using ByteWebConnector.Model.Models.ServiceResponseModels.BytePro;
using ByteWebConnector.Model.Models.Settings;
using ByteWebConnector.Service.DbServices;
using ByteWebConnector.Service.InternalServices;
using Extensions.ExtensionClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using DeleteRequest = ByteWebConnector.API.Models.Document.DeleteRequest;
using DocumentUploadResponse = ByteWebConnector.Model.Models.ByteApi.DocumentUploadResponse;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ByteWebConnector.API.Controllers
{
    [Route(template: "api/ByteWebConnector/[controller]")]
    [ApiController]
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
                                  IByteWebConnectorSdkService byteWebConnectorSdkService)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
            ISettingService _settingService = settingService;
            _rainmakerService = rainmakerService;
            _byteWebConnectorSdkService = byteWebConnectorSdkService;
            ByteProSettings = _settingService.GetByteProSettings();
        }

        #endregion

        #region Properties

        public ByteProSettings ByteProSettings { get; set; }

        #endregion

        #region Private Fields

        //private string _apiUrl;
        private readonly ILogger<DocumentController> _logger;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly IRainmakerService _rainmakerService;
        private readonly IByteWebConnectorSdkService _byteWebConnectorSdkService;

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
        public ApiResponse SendDocument([FromBody] SendDocumentRequest request)
        {
            _logger.LogInformation(message: "Start");
            _logger.LogInformation(message: "DocSync  ByteWebConnector  SendDocument uploadEmbeddedDoc start ");

            var getLoanApplicationResponse =
                _rainmakerService.GetLoanApplication(loanApplicationId: request.LoanApplicationId);
            var loanApplication = getLoanApplicationResponse.ResponseObject;

            _logger.LogInformation(message:
                                   $"DocSync  getLoanApplicationResponse.Content = {getLoanApplicationResponse.HttpResponseMessage.Content.ReadAsStringAsync().Result} ");
            _logger.LogInformation(message: $"DocSync  loanApplication = {loanApplication.ToJson()} ");

            _logger.LogInformation(message: $"loanApplication found= {loanApplication.HasValue()}");
            if (loanApplication != null)
            {
                _logger.LogInformation(message: $"request.MediaType={request.MediaType}");
                if (_configuration.GetSection(key: "MediaTypesToBeWrappedInPdf").Get<string[]>()
                                  .Contains(value: request.MediaType))
                {
                    _logger.LogInformation(message: "Wrapping In PDF");
                    var intput = new List<byte[]>();
                    intput.Add(item: request.FileData);
                    request.FileData = Helper.WrapImagesInPdf(intput: intput).Single();
                    request.DocumentExension = "pdf";
                }

                _logger.LogInformation(message: $"loanApplication.Id = {loanApplication.Id}");
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

                _logger.LogInformation(message: "Start GetByteProSession();");
                //if (string.IsNullOrEmpty(ByteSession))
                //{
                //    ByteSession = GetByteProSession();
                //}
                //else
                //{
                //    bool isValid = ValidateByteSession(ByteSession).Result;
                //    if (!isValid)
                //    {
                //        ByteSession = GetByteProSession();
                //    }
                //}

                ByteSession = ByteSession.HasValue() ? ByteSession : GetByteProSession();
                if (!ValidateByteSession(byteSession: ByteSession)) ByteSession = GetByteProSession();
                _logger.LogInformation(message: $"byteProSession = {ByteSession}");
                FileDataResponse fileData = GetFileData(ByteSession,
                                                loanApplication.ByteLoanNumber);
                documentUploadModel.FileName = fileData.FileName;
                //var documentResponse = SendDocumentToByte(documentUploadRequest: documentUploadModel,
                //                                          session: ByteSession);

                var sdkDocumentResponse = _byteWebConnectorSdkService.SendDocumentToByte(documentUploadModel).ResponseObject;

                var apiResponse = new ApiResponse() { Status = sdkDocumentResponse.Status,Data = sdkDocumentResponse.Data};

                return apiResponse;

                #endregion
            }

            return null;
        }


        // PUT api/<DocumentController>/5
        [HttpPut(template: "{id}")]
        public void Put(int id,
                        [FromBody] string value)
        {
            throw new NotSupportedException();
        }


        // DELETE api/<DocumentController>/5
        [Route(template: "[action]")]
        [HttpPost]
        public async Task<IActionResult> Delete(DeleteRequest request)
        {
            var losModel = request.GetLosModel();

            var content = losModel.ToJsonString();

            var token = Request.Headers[key: "Authorization"].ToString().Replace(oldValue: "Bearer ",
                                                                                 newValue: "");
            _httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue(scheme: "Bearer",
                                                parameter: token);

            var callResponse =
                await _httpClient.PostAsync(requestUri:
                                            $"{_configuration[key: "ServiceAddress:LosIntegration:Url"]}/api/LosIntegration/Document/Delete",
                                            content: new StringContent(content: content,
                                                                       encoding: Encoding.UTF8,
                                                                       mediaType: "application/json"));
            if (callResponse.IsSuccessStatusCode) return Ok();

            return BadRequest();
        }


        [Route(template: "[action]")]
        [HttpPost]
        public async Task<IActionResult> DocumentAdded(DocumentAddedRequest request)
        {
            #region Byte API Call

            Thread.Sleep(millisecondsTimeout: 5000);
            var byteProSession = GetByteProSession();

            var embeddedDocs = GetAllByteDocuments(session: byteProSession,
                                                   fileDataId: request.FileDataId);

            _logger.LogInformation(message: $"T====== Total embeddedDocs = {embeddedDocs.Count}");
            var content = new AddDocumentRequest(fileDataId: request.FileDataId,
                                                 embeddedDocs: embeddedDocs).ToJson();
            var token = Request.Headers[key: "Authorization"].ToString().Replace(oldValue: "Bearer ",
                                                                                 newValue: "");
            _httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue(scheme: "Bearer",
                                                parameter: token);
            var callResponse =
                await _httpClient.PostAsync(requestUri:
                                            $"{_configuration[key: "ServiceAddress:LosIntegration:Url"]}/api/LosIntegration/Document/AddDocument",
                                            content: new StringContent(content: content,
                                                                       encoding: Encoding.UTF8,
                                                                       mediaType: "application/json"));

            if (callResponse.IsSuccessStatusCode) return Ok();

            return BadRequest();

            #endregion
        }


        [Route(template: "[action]")]
        [HttpPost]
        public EmbeddedDoc GetDocumentDataFromByte(DocumentDataRequest request)
        {
            #region Byte API Call

            var byteProSession = GetByteProSession();

            var embeddedDocWithData = GetEmbeddedDocData(byteProSession: byteProSession,
                                                         documentId: request.DocumentId,
                                                         fileDataId: request.FileDataId);

            return embeddedDocWithData;

            #endregion
        }



        #endregion

        #region Private Methods

        private string GetByteProSession()
        {
            try
            {
                //var byteProSettings = _settingService.GetByteProSettings();

                _logger.LogInformation(message: $"byteProSettings = {ByteProSettings.ToJson()}");

                var baseUrl = ByteProSettings.ByteApiUrl;
                _logger.LogInformation(message: $"ByteApiUrl = {baseUrl}");
                ServicePointManager.ServerCertificateValidationCallback += (sender,
                                                                            certificate,
                                                                            chain,
                                                                            sslPolicyErrors) => true;
                //_apiUrl = baseUrl;
                var request = (HttpWebRequest)WebRequest.Create(requestUriString: baseUrl + "auth/ ");
                request.Method = "GET";
                request.ContentType = "application/json";
                request.Headers.Add(name: "authorizationKey",
                                    value: ByteProSettings.ByteApiAuthKey);
                request.Headers.Add(name: "username",
                                    value: ByteProSettings.ByteApiUserName);
                request.Headers.Add(name: "password",
                                    value: ByteProSettings.ByteApiPassword);
                request.Accept = "application/json";
                var test = string.Empty;
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    var dataStream = response.GetResponseStream();
                    var reader = new StreamReader(stream: dataStream);
                    test = reader.ReadToEnd();
                    reader.Close();
                    dataStream.Close();
                }

                return test.Replace(oldValue: "\"",
                                    newValue: "");
            }
            catch (Exception ex)
            {
                throw new ArgumentException(message: ex.InnerException != null
                                                ? ex.InnerException.Message
                                                : "Error in BytePro Connection, Please try again later.");
            }
        }


        [NonAction]
        private ApiResponse SendDocumentToByte(DocumentUploadRequest documentUploadRequest,
                                               string session)
        {
            _logger.LogInformation(message:
                                   $"DocSync SendDocumentToByte :DocumentCategory {documentUploadRequest.DocumentCategory} ");
            _logger.LogInformation(message:
                                   $"DocSync SendDocumentToByte :FileDataId {documentUploadRequest.FileDataId} ");
            _logger.LogInformation(message:
                                   $"DocSync SendDocumentToByte :DocumentType {documentUploadRequest.DocumentType} ");
            _logger.LogInformation(message:
                                   $"DocSync SendDocumentToByte :DocumentCategory {documentUploadRequest.DocumentCategory} ");
            _logger.LogInformation(message:
                                   $"DocSync SendDocumentToByte :DocumentStatus {documentUploadRequest.DocumentStatus} ");
            _logger.LogInformation(message:
                                   $"DocSync SendDocumentToByte :DocumentExension {documentUploadRequest.DocumentExension} ");
            _logger.LogInformation(message:
                                   $"DocSync SendDocumentToByte uploadEmbeddedDoc :DocumentName {documentUploadRequest.DocumentName} ");
            _logger.LogInformation(message:
                                   $"DocSync SendDocumentToByte :DocumentData {documentUploadRequest.DocumentData} ");
            var respone = new ApiResponse();
            try
            {
                var output = JsonConvert.SerializeObject(value: documentUploadRequest,
                                                         formatting: Formatting.None,
                                                         settings: new JsonSerializerSettings
                                                         {
                                                             NullValueHandling = NullValueHandling.Ignore
                                                         });

                var documentResponse = Send(output: output,
                                            session: session);
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };

                var response = documentResponse.Result;
                _logger.LogInformation(message: $"DocSync SendDocumentToByte uploadEmbeddedDoc Resposne = {response}");
                var document =
                    JsonConvert.DeserializeObject<DocumentUploadResponse>(value: response,
                                                                          settings: settings);
                _logger.LogInformation(message: $"DocSync  SendDocumentToByte Deserialize    {document}");
                if (document != null)
                {
                    _logger.LogInformation(message: "byteDocumentResponse Deserialized");
                    document.ExtOriginatorId = 1;
                }

                respone.Status = ApiResponse.ApiResponseStatus.Success;
                respone.Data = document.ToJson();
                _logger.LogInformation(message: $"DocSync  SendDocumentToByte respone      {respone.Data}");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(message: $"DocSync SendDocumentToByte Exception    {ex.Message}");
                _logger.LogInformation(message: "byteDocumentResponse Deserialized");
                respone.Status = ApiResponse.ApiResponseStatus.Fail;
                respone.Message = ex.Message;
            }

            _logger.LogInformation(message: $"DocSync SendDocumentToByte finished :  {respone} ");
            return respone;
        }



        private List<EmbeddedDoc> GetAllByteDocuments(string session,
                                                      int fileDataId)
        {
            var request =
                (HttpWebRequest)WebRequest.Create(requestUriString: ByteProSettings.ByteApiUrl + "Document/" +
                                                                     fileDataId);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Headers.Add(name: "Session",
                                value: session);
            request.Accept = "application/json";

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                var dataStream = response.GetResponseStream();
                var reader = new StreamReader(stream: dataStream);
                var responseString = reader.ReadToEnd();

                var embeddedDocList =
                    JsonConvert.DeserializeObject<List<EmbeddedDoc>>(value: responseString);
                reader.Close();
                dataStream.Close();
                return embeddedDocList;
            }
        }


        private async Task<string> Send(string output,
                                        string session)
        {
            var clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender,
                                                                       cert,
                                                                       chain,
                                                                       sslPolicyErrors) =>
            {
                return true;
            };

            _logger.LogInformation(message: $"DocSync ByteApiDocument Content = {output.ToJson()}");

            using (var client = new HttpClient(handler: clientHandler))
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri(uriString: ByteProSettings.ByteApiUrl + "Document/"),
                    Method = HttpMethod.Post,
                    Content = new StringContent(content: output,
                                                              encoding: Encoding.UTF8,
                                                              mediaType: "application/json")
                };
                request.Content.Headers.ContentType = new MediaTypeHeaderValue(mediaType: "application/json");
                request.Headers.Add(name: "Session",
                                    value: session);
                request.Headers.Accept.Clear();
                request.Headers.Accept.Add(item: new MediaTypeWithQualityHeaderValue(mediaType: "application/json"));

                _logger.LogInformation(message:
                                       $"DocSync Send    :DocumentData {ByteProSettings.ByteApiUrl + "Document/"} ");
                var response = await client.SendAsync(request: request)
                                           .ConfigureAwait(continueOnCapturedContext: false);
                response.EnsureSuccessStatusCode();
                var resp = await response.Content.ReadAsStringAsync();
                return resp;
            }

        }


        private bool ValidateByteSession(string byteSession)
        {
            _logger.LogInformation(message: $"DocSync byteSession = {byteSession}");

            var request =
                (HttpWebRequest)WebRequest.Create(requestUriString: ByteProSettings.ByteApiUrl + "organization/");
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Headers.Add(name: "Session",
                                value: byteSession);
            request.Accept = "application/json";

            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    var dataStream = response.GetResponseStream();
                    var reader = new StreamReader(stream: dataStream);
                    reader.ReadToEnd();
                    reader.Close();
                    dataStream.Close();
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        _logger.LogInformation(message: $"DocSync Byte request failed :{response.StatusCode} ");
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogInformation(message: $"DocSync Byte request failed");
                return false;
            }

            return true;
        }


        private EmbeddedDoc GetEmbeddedDocData(string byteProSession,
                                               int documentId,
                                               int fileDataId)
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


        private FileDataResponse GetFileData(string byteSession, string fileDataId)
        {
            try
            {
                _logger.LogInformation($"GetFileData Start");
                var request =
                    (HttpWebRequest)WebRequest.Create(requestUriString: ByteProSettings.ByteApiUrl + "FileData/" +
                                                                        fileDataId);
                request.Method = "GET";
                request.ContentType = "application/json";
                request.Headers.Add(name: "Session",
                                    value: byteSession);
                request.Accept = "application/json";

                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    var dataStream = response.GetResponseStream();
                    var reader = new StreamReader(stream: dataStream);
                    var responseString = reader.ReadToEnd();
                    var fileData =
                        JsonConvert.DeserializeObject<FileDataResponse>(value: responseString);
                    reader.Close();
                    dataStream.Close();
                    return fileData;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.InnerException.Message);
            }
            _logger.LogInformation($"GetFileData return null");
            return null;
        }
        #endregion
    }
}