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
using ByteWebConnector.API.Models.ByteApi;
using ByteWebConnector.API.Models.ClientModels.Document;
using ByteWebConnector.API.Models.Document;
using ByteWebConnector.Service.DbServices;
using ByteWebConnector.Service.InternalServices;
using Extensions.ExtensionClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;



using DeleteRequest = ByteWebConnector.API.Models.Document.DeleteRequest;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ByteWebConnector.API.Controllers
{
    [Route("api/ByteWebConnector/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        

        #region Constructor

        public DocumentController(
                                  HttpClient httpClient,
                                  IConfiguration configuration,
                                  ILogger<DocumentController> logger,
                                  ISettingService settingService,
                                  IRainmakerService rainmakerService)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
            _settingService = settingService;
            _rainmakerService = rainmakerService;
        }

        #endregion

        #region Private Fields

        
        private string _apiUrl;
        private readonly ILogger<DocumentController> _logger;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly ISettingService _settingService;
        private readonly IRainmakerService _rainmakerService;

        #endregion

        #region Action Methods

        // GET: api/<DocumentController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[]
                   {
                       "value1",
                       "value2"
                   };
        }


        // GET api/<DocumentController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }


        // POST api/<DocumentController>
        [Route(template: "[action]")]
        [HttpPost]
        public ApiResponse SendDocument([FromBody] SendDocumentRequest request)
        {
            _logger.LogInformation($"Start");
            _logger.LogInformation(message: $"DocSync  ByteWebConnector  SendDocument uploadEmbeddedDoc start ");
            _logger.LogInformation(message: $"DocSync  SendDocument uploadEmbeddedDoc request  {request.FileData} ");

            var getLoanApplicationResponse = _rainmakerService.GetLoanApplication(loanApplicationId: request.LoanApplicationId);
            var loanApplication = getLoanApplicationResponse.ResponseObject;

            _logger.LogInformation($"loanApplication found= {loanApplication.HasValue()}");
            if (loanApplication != null)
            {
                _logger.LogInformation($"request.MediaType={request.MediaType}");
                if (_configuration.GetSection($"MediaTypesToBeWrappedInPdf").Get<string[]>().Contains(request.MediaType))
                {
                    _logger.LogInformation("Wrapping In PDF");
                    List<byte[]> intput = new List<byte[]>();
                    intput.Add(request.FileData);
                    request.FileData = Utility.Helper.WrapImagesInPdf(intput).Single();
                    request.DocumentExension = "pdf";
                }
                _logger.LogInformation($"loanApplication.Id = {loanApplication.Id}");
                var documentUploadModel = new DocumentUploadRequest
                                          {
                                              FileDataId = Convert.ToInt64(loanApplication.EncompassNumber),
                                              DocumentCategory = request.DocumentCategory,
                                              DocumentExension = request.DocumentExension,
                                              DocumentName = request.DocumentName,
                                              DocumentStatus = request.DocumentStatus,
                                              DocumentType = request.DocumentType,
                                              DocumentData = request.FileData.ToBase64String(0,
                                                                                             request.FileData.Length)
                                          };

                #region BytePro API Call
                _logger.LogInformation($"Start GetByteProSession();");
                string byteProSession = GetByteProSession();
                _logger.LogInformation($"byteProSession = {byteProSession}");
                ApiResponse documentResponse = SendDocumentToByte(documentUploadModel,
                                                                  byteProSession);
                return documentResponse;

                #endregion
            }
            else
            {
                return null;
            }
        }
       
        // PUT api/<DocumentController>/5
        [HttpPut("{id}")]
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
            if (callResponse.IsSuccessStatusCode)
            {
                return Ok();
            }

            return BadRequest();
        }


        [Route(template: "[action]")]
        [HttpPost]
        public async Task<IActionResult> DocumentAdded(DocumentAddedRequest request)
        {
            #region Byte API Call
            Thread.Sleep(5000);
            var byteProSession = GetByteProSession();

            var embeddedDocs = GetAllByteDocuments(byteProSession,
                                                   request.FileDataId);

            _logger.LogInformation($"T====== Total embeddedDocs = {embeddedDocs.Count}");
            var content = new AddDocumentRequest(request.FileDataId,
                                                 embeddedDocs).ToJson();
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

            if (callResponse.IsSuccessStatusCode)
            {
                return Ok();
            }

            return BadRequest();

            #endregion
        }


        [Route(template: "[action]")]
        [HttpPost]
        public EmbeddedDoc GetDocumentDataFromByte(DocumentDataRequest request)
        {
            #region Byte API Call

            var byteProSession = GetByteProSession();

            var embeddedDocWithData = GetEmbeddedDocData(byteProSession,
                                                         request.DocumentId,
                                                         request.FileDataId);

            return embeddedDocWithData;

            #endregion
        }


        private EmbeddedDoc GetEmbeddedDocData(string byteProSession,
                                               int documentId,
                                               int fileDataId)
        {
            HttpWebRequest request =
                (HttpWebRequest) HttpWebRequest.Create(_apiUrl + "Document/" + fileDataId + "/" + documentId);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Headers.Add("Session",
                                byteProSession);
            request.Accept = "application/json";
            String test = String.Empty;
            using (HttpWebResponse response = (HttpWebResponse) request.GetResponse())
            {
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                var responseString = reader.ReadToEnd();

                var embeddedDoc =
                    JsonConvert.DeserializeObject<EmbeddedDoc>(responseString);
                reader.Close();
                dataStream.Close();
                return embeddedDoc;
            }

        }

        #endregion

        #region Private Methods

        private string GetByteProSession()
        {
            try
            {
                var byteProSettings = _settingService.GetByteProSettings();

                _logger.LogInformation(message: $"byteProSettings = {byteProSettings.ToJson()}");

                var baseUrl = byteProSettings.ByteApiUrl;
                _logger.LogInformation(message: $"_apiUrl = {baseUrl}");
                ServicePointManager.ServerCertificateValidationCallback += (sender,
                                                                            certificate,
                                                                            chain,
                                                                            sslPolicyErrors) => true;
                _apiUrl = baseUrl;
                var request = (HttpWebRequest)WebRequest.Create(requestUriString: baseUrl + "auth/ ");
                request.Method = "GET";
                request.ContentType = "application/json";
                request.Headers.Add(name: "authorizationKey",
                                    value: byteProSettings.ByteApiAuthKey);
                request.Headers.Add(name: "username",
                                    value: byteProSettings.ByteApiUserName);
                request.Headers.Add(name: "password",
                                    value: byteProSettings.ByteApiPassword);
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
            _logger.LogInformation(message: $"DocSync SendDocumentToByte :DocumentCategory {documentUploadRequest.DocumentCategory} ");
            _logger.LogInformation(message: $"DocSync SendDocumentToByte :FileDataId {documentUploadRequest.FileDataId} ");
            _logger.LogInformation(message: $"DocSync SendDocumentToByte :DocumentType {documentUploadRequest.DocumentType} ");
            _logger.LogInformation(message: $"DocSync SendDocumentToByte :DocumentCategory {documentUploadRequest.DocumentCategory} ");
            _logger.LogInformation(message: $"DocSync SendDocumentToByte :DocumentStatus {documentUploadRequest.DocumentStatus} ");
            _logger.LogInformation(message: $"DocSync SendDocumentToByte :DocumentExension {documentUploadRequest.DocumentExension} ");
            _logger.LogInformation(message: $"DocSync SendDocumentToByte uploadEmbeddedDoc :DocumentName {documentUploadRequest.DocumentName} ");
            _logger.LogInformation(message: $"DocSync SendDocumentToByte :DocumentData {documentUploadRequest.DocumentData} ");
            var respone = new ApiResponse();
            try
            {
                string output = JsonConvert.SerializeObject(documentUploadRequest,
                                                            Formatting.None,
                                                            new JsonSerializerSettings
                                                            {
                                                                NullValueHandling = NullValueHandling.Ignore
                                                            });
                
                Task<string> documentResponse = Send(output,
                                                     session);
                var settings = new JsonSerializerSettings
                               {
                                   NullValueHandling = NullValueHandling.Ignore,
                                   MissingMemberHandling = MissingMemberHandling.Ignore
                               };



              var response   = documentResponse.Result;
                _logger.LogInformation(message: $"DocSync SendDocumentToByte uploadEmbeddedDoc Resposne = { response  }");
                DocumentUploadResponse document =
                    JsonConvert.DeserializeObject<DocumentUploadResponse>(response,
                                                                          settings);
                _logger.LogInformation(message: $"DocSync  SendDocumentToByte Deserialize    { document}");
                if (document != null)
                {
                    _logger.LogInformation($"byteDocumentResponse Deserialized");
                    document.ExtOriginatorId = 1;
                }

                respone.Status = ApiResponse.ApiResponseStatus.Success;
                respone.Data = document.ToJson();
                _logger.LogInformation(message: $"DocSync  SendDocumentToByte respone      { respone.Data}");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(message: $"DocSync SendDocumentToByte Exception    { ex.Message}");
                _logger.LogInformation($"byteDocumentResponse Deserialized");
                respone.Status = ApiResponse.ApiResponseStatus.Fail;
                respone.Message = ex.Message;
            }
            _logger.LogInformation(message: $"DocSync SendDocumentToByte finished :  {respone} ");
            return respone;
        }


        private List<EmbeddedDoc> GetAllByteDocuments(string session,
                                                      int fileDataId)
        {
            HttpWebRequest request = (HttpWebRequest) HttpWebRequest.Create(_apiUrl + "Document/" + fileDataId);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Headers.Add("Session",
                                session);
            request.Accept = "application/json";
            String test = String.Empty;
            using (HttpWebResponse response = (HttpWebResponse) request.GetResponse())
            {
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                var responseString = reader.ReadToEnd();

                var embeddedDocList =
                    JsonConvert.DeserializeObject<List<EmbeddedDoc>>(responseString);
                reader.Close();
                dataStream.Close();
                return embeddedDocList;
            }

        }


        private async Task<string> Send(string output,
                                        string session)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender,
                                                                       cert,
                                                                       chain,
                                                                       sslPolicyErrors) =>
            {
                return true;
            };
            using (var client = new HttpClient(clientHandler))
            {
                var request = new HttpRequestMessage()
                              {
                                  RequestUri = new Uri(_apiUrl + "Document/"),
                                  Method = HttpMethod.Post,
                                  Content = new StringContent(output,
                                                              Encoding.UTF8,
                                                              "application/json")
                              };
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                request.Headers.Add("Session",
                                    session);
                request.Headers.Accept.Clear();
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                _logger.LogInformation(message: $"DocSync Send    :DocumentData {_apiUrl + "Document/"} ");
                HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                var resp = await response.Content.ReadAsStringAsync();
                return resp;
            }
        }

        #endregion
    }
}