using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ByteWebConnector.Model.Models;
using ByteWebConnector.Model.Models.Document;
using ByteWebConnector.Model.Models.ServiceRequestModels.BytePro;
using ByteWebConnector.Model.Models.ServiceResponseModels.BytePro;
using ByteWebConnector.Service.DbServices;
using Extensions.ExtensionClasses;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ByteWebConnector.Service.ExternalServices
{
    public class ByteProService : IByteProService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ByteProService> _logger;
        private readonly ISettingService _settingService;
        private string _baseUrl;


        public ByteProService(ILogger<ByteProService> logger,
                              string baseUrl,
                              HttpClient httpClient,
                              ISettingService settingService)
        {
            _logger = logger;
            _baseUrl = baseUrl;
            _httpClient = httpClient;
            _settingService = settingService;
        }


        public string GetByteProSession()
        {
            try
            {
                var byteProSettings = _settingService.GetByteProSettings();

                _logger.LogInformation(message: $"byteProSettings = {byteProSettings.ToJson()}");

                _baseUrl = byteProSettings.ByteApiUrl;
                _logger.LogInformation(message: $"_apiUrl = {_baseUrl}");
                ServicePointManager.ServerCertificateValidationCallback += (sender,
                                                                            certificate,
                                                                            chain,
                                                                            sslPolicyErrors) => true;

                var request = (HttpWebRequest) WebRequest.Create(requestUriString: _baseUrl + "auth/ ");
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
                using (var response = (HttpWebResponse) request.GetResponse())
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


        public ApiResponse SendDocumentToByte(DocumentUploadRequest documentUploadRequest,
                                              string session)
        {
            var respone = new ApiResponse();
            try
            {
                var input = JsonConvert.SerializeObject(value: documentUploadRequest,
                                                         formatting: Formatting.None,
                                                         settings: new JsonSerializerSettings
                                                                   {
                                                                       NullValueHandling = NullValueHandling.Ignore
                                                                   });
                //string output = JsonConvert.SerializeObject(loanInfo);
                var documentResponse = Send(output: input,
                                            session: session);
                var settings = new JsonSerializerSettings
                               {
                                   NullValueHandling = NullValueHandling.Ignore,
                                   MissingMemberHandling = MissingMemberHandling.Ignore
                               };

                _logger.LogInformation(message: $"byteDocumentResponse= {documentResponse.Result}");
                var document =
                    JsonConvert.DeserializeObject<DocumentUploadResponse>(value: documentResponse.Result,
                                                                          settings: settings);
                if (document != null) document.ExtOriginatorId = 1;

                respone.Status = ApiResponse.ApiResponseStatus.Success;
                respone.Data = document.ToJson();
            }
            catch (Exception ex)
            {
                respone.Status = ApiResponse.ApiResponseStatus.Fail;
                respone.Message = ex.Message;
            }

            return respone;
        }


        public List<EmbeddedDoc> GetAllByteDocuments(string session,
                                                     int fileDataId)
        {
            var request = (HttpWebRequest) WebRequest.Create(requestUriString: _baseUrl + "Document/" + fileDataId);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Headers.Add(name: "Session",
                                value: session);
            request.Accept = "application/json";
            var test = string.Empty;
            using (var response = (HttpWebResponse) request.GetResponse())
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

            return null;
        }


        public async Task<string> Send(string output,
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
            using (var client = new HttpClient(handler: clientHandler))
            {
                var request = new HttpRequestMessage
                              {
                                  RequestUri = new Uri(uriString: _baseUrl + "Document/"),
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
                var response = await client.SendAsync(request: request)
                                           .ConfigureAwait(continueOnCapturedContext: false);
                response.EnsureSuccessStatusCode();
                var resp = await response.Content.ReadAsStringAsync();
                return resp;
            }
        }


        public EmbeddedDoc GetEmbeddedDocData(string byteProSession,
                                              int documentId,
                                              int fileDataId)
        {
            var request =
                (HttpWebRequest) WebRequest.Create(requestUriString: $"{_baseUrl}Document/{fileDataId}/{documentId}");
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Headers.Add(name: "Session",
                                value: byteProSession);
            request.Accept = "application/json";
            var test = string.Empty;
            using (var response = (HttpWebResponse) request.GetResponse())
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

            return null;
        }
    }
}