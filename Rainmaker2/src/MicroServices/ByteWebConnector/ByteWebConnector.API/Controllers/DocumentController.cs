using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ByteWebConnector.API.ExtensionMethods;
using ByteWebConnector.API.Models;
using ByteWebConnector.API.Models.ByteApi;
using ByteWebConnector.API.Models.ClientModels.Document;
using ByteWebConnector.API.Models.Document;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RainMaker.Common;
using RainMaker.Common.Extensions;
using Rainmaker.Service;
using RainMaker.Service;
using DeleteRequest = ByteWebConnector.API.Models.Document.DeleteRequest;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ByteWebConnector.API.Controllers
{
    [Route("api/ByteWebConnector/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        #region Constructor

        public DocumentController(ILoanApplicationService loanApplicationService,
                                  ICommonService commonService,
                                  HttpClient httpClient,
                                  IConfiguration configuration)
        {
            _loanApplicationService = loanApplicationService;
            _commonService = commonService;
            _httpClient = httpClient;
            _configuration = configuration;
        }

        #endregion

        #region Private Fields

        private readonly ILoanApplicationService _loanApplicationService;
        private readonly ICommonService _commonService;
        private string _apiUrl;

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
            var loanApplication = _loanApplicationService
                                  .GetLoanApplicationWithDetails(id: request.LoanApplicationId,
                                                                 includes: null)
                                  .SingleOrDefault();

            if (loanApplication != null)
            {
                var documentUploadModel = new DocumentUploadRequest
                                          {
                                              FileDataId = Convert.ToInt64(loanApplication.EncompassNumber),
                                              DocumentCategory = "PROP", //request.DocumentCategory,
                                              DocumentExension = "DOCX", //request.DocumentExension,
                                              DocumentName = "Bank Doc", //request.DocumentName,
                                              DocumentStatus = "0", //request.DocumentStatus,
                                              DocumentType = "PurchaseAgr", //request.DocumentType,
                                              DocumentData = request.FileData.ToBase64String(0,
                                                                                             request.FileData.Length)
                                          };

                #region BytePro API Call

                string byteProSession = GetByteProSession();
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
        // POST api/<DocumentController>
        //[Route(template: "[action]")]
        //[HttpPost]
        //public IActionResult SendDocumentToManagement([FromBody] SendDocumentRequest request)
        //{


        //}
        // PUT api/<DocumentController>/5
        [HttpPut("{id}")]
        public void Put(int id,
                        [FromBody] string value)
        {
        }


        // DELETE api/<DocumentController>/5
        [Route(template: "[action]")]
        [HttpPost]
        public async Task Delete(DeleteRequest request)
        {
            var losModel = request.GetLosModel();

            var content = losModel.ToJsonString();

            var callResponse =
                await _httpClient.PostAsync(requestUri:
                                            $"{_configuration[key: "LosIntegration:Url"]}/api/LosIntegration/Document/Delete",
                                            content: new StringContent(content: content,
                                                                       encoding: Encoding.UTF8,
                                                                       mediaType: "application/json"));
        }


        
        [Route(template: "[action]")]
        [HttpPost]
        public async Task DocumentAdded(DocumentAddedRequest request)
        {
           // System.Threading.Thread.Sleep();
           
            #region Byte API Call

            var byteProSession=  GetByteProSession();

            var embeddedDocs =     GetAllByteDocuments(byteProSession,
                                   request.FileDataId);

            var content = new AddDocumentRequest(request.FileDataId,
                                   embeddedDocs).ToJson();
            var callResponse =
                await _httpClient.PostAsync(requestUri:
                                            $"{_configuration[key: "LosIntegration:Url"]}/api/LosIntegration/Document/AddDocument",
                                            content: new StringContent(content: content,
                                                                       encoding: Encoding.UTF8,
                                                                       mediaType: "application/json"));



            var embeddedDocWithData = GetEmbeddedDocData(byteProSession, embeddedDocs[0].DocumentId, embeddedDocs[0].FileDataId);



            #endregion


        }

        [Route(template: "[action]")]
        [HttpPost]
        public EmbeddedDoc GetDocumentDataFromByte(DocumentDataRequest request)
        {

            #region Byte API Call
            
            var byteProSession = GetByteProSession();

            var embeddedDocWithData = GetEmbeddedDocData(byteProSession, request.DocumentId, request.FileDataId);

            return embeddedDocWithData;

            #endregion


        }


        private EmbeddedDoc GetEmbeddedDocData(string byteProSession,
                                               int documentId,
                                               int fileDataId)
        {

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(_apiUrl + "Document/" + fileDataId + "/" + documentId);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Headers.Add("Session", byteProSession);
            request.Accept = "application/json";
            String test = String.Empty;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
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

            return null;
        }

        #endregion

        #region Private Methods

        private string GetByteProSession()
        {
            try
            {
                var userName = _commonService.GetSettingValueByKeyAsync<string>(SystemSettingKeys.ByteProApiUserName)
                                             .Result;
                var password = _commonService.GetSettingValueByKeyAsync<string>(SystemSettingKeys.ByteProApiPassword)
                                             .Result;
                var authKey = _commonService.GetSettingValueByKeyAsync<string>(SystemSettingKeys.ByteApiAuthKey).Result;
                _apiUrl = _commonService.GetSettingValueByKeyAsync<string>(SystemSettingKeys.ByteApiUrl).Result;
                ServicePointManager.ServerCertificateValidationCallback += (sender,
                                                                            certificate,
                                                                            chain,
                                                                            sslPolicyErrors) => true;

                HttpWebRequest request = (HttpWebRequest) HttpWebRequest.Create(_apiUrl + "auth/ ");
                request.Method = "GET";
                request.ContentType = "application/json";
                request.Headers.Add("authorizationKey",
                                    authKey);
                request.Headers.Add("username",
                                    userName);
                request.Headers.Add("password",
                                    password);
                request.Accept = "application/json";
                String test = String.Empty;
                using (HttpWebResponse response = (HttpWebResponse) request.GetResponse())
                {
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    test = reader.ReadToEnd();
                    reader.Close();
                    dataStream.Close();
                }

                return test.Replace("\"",
                                    "");
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.InnerException != null
                                                ? ex.InnerException.Message
                                                : "Error in BytePro Connection, Please try again later.");
            }
        }

        [NonAction]
        private ApiResponse SendDocumentToByte(DocumentUploadRequest documentUploadRequest,
                                               string session)
        {
            var respone = new ApiResponse();
            try
            {
                string output = JsonConvert.SerializeObject(documentUploadRequest,
                                                            Formatting.None,
                                                            new JsonSerializerSettings
                                                            {
                                                                NullValueHandling = NullValueHandling.Ignore
                                                            });
                //string output = JsonConvert.SerializeObject(loanInfo);
                Task<string> documentResponse = Send(output,
                                                     session);
                var settings = new JsonSerializerSettings
                               {
                                   NullValueHandling = NullValueHandling.Ignore,
                                   MissingMemberHandling = MissingMemberHandling.Ignore
                               };
                DocumentUploadResponse document =
                    JsonConvert.DeserializeObject<DocumentUploadResponse>(documentResponse.Result,
                                                                          settings);
                if (document != null)
                {
                    document.ExtOriginatorId = 1;
                }

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


        private List<EmbeddedDoc> GetAllByteDocuments(string session,int fileDataId)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(_apiUrl + "Document/" + fileDataId);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Headers.Add("Session", session);
            request.Accept = "application/json";
            String test = String.Empty;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
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

            return null;
        }

        private async Task<string> Send(string output,
                                        string session)
        {
            try
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
                    HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
                    response.EnsureSuccessStatusCode();
                    var resp = await response.Content.ReadAsStringAsync();
                    return resp;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion
    }

    public static class Extensions
    {
        /// <summary>
        ///     Converts an array of 8-bit unsigned integers to its equivalent string representation that is encoded with
        ///     base-64 digits.
        /// </summary>
        /// <param name="inArray">An array of 8-bit unsigned integers.</param>
        /// <returns>The string representation, in base 64, of the contents of .</returns>
        public static String ToBase64String(this Byte[] inArray)
        {
            return Convert.ToBase64String(inArray);
        }


        /// <summary>
        ///     Converts an array of 8-bit unsigned integers to its equivalent string representation that is encoded with
        ///     base-64 digits. A parameter specifies whether to insert line breaks in the return value.
        /// </summary>
        /// <param name="inArray">An array of 8-bit unsigned integers.</param>
        /// <param name="options">to insert a line break every 76 characters, or  to not insert line breaks.</param>
        /// <returns>The string representation in base 64 of the elements in .</returns>
        public static String ToBase64String(this Byte[] inArray,
                                            Base64FormattingOptions options)
        {
            return Convert.ToBase64String(inArray,
                                          options);
        }


        /// <summary>
        ///     Converts a subset of an array of 8-bit unsigned integers to its equivalent string representation that is
        ///     encoded with base-64 digits. Parameters specify the subset as an offset in the input array, and the number of
        ///     elements in the array to convert.
        /// </summary>
        /// <param name="inArray">An array of 8-bit unsigned integers.</param>
        /// <param name="offset">An offset in .</param>
        /// <param name="length">The number of elements of  to convert.</param>
        /// <returns>The string representation in base 64 of  elements of , starting at position .</returns>
        public static String ToBase64String(this Byte[] inArray,
                                            Int32 offset,
                                            Int32 length)
        {
            return Convert.ToBase64String(inArray,
                                          offset,
                                          length);
        }


        /// <summary>
        ///     Converts a subset of an array of 8-bit unsigned integers to its equivalent string representation that is
        ///     encoded with base-64 digits. Parameters specify the subset as an offset in the input array, the number of
        ///     elements in the array to convert, and whether to insert line breaks in the return value.
        /// </summary>
        /// <param name="inArray">An array of 8-bit unsigned integers.</param>
        /// <param name="offset">An offset in .</param>
        /// <param name="length">The number of elements of  to convert.</param>
        /// <param name="options">to insert a line break every 76 characters, or  to not insert line breaks.</param>
        /// <returns>The string representation in base 64 of  elements of , starting at position .</returns>
        public static String ToBase64String(this Byte[] inArray,
                                            Int32 offset,
                                            Int32 length,
                                            Base64FormattingOptions options)
        {
            return Convert.ToBase64String(inArray,
                                          offset,
                                          length,
                                          options);
        }
    }
}