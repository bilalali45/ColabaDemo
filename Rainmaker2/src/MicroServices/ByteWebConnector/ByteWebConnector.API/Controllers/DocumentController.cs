using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ByteWebConnector.API.Models;
using ByteWebConnector.API.Models.ByteApi;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RainMaker.Common;
using Rainmaker.Service;
using RainMaker.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ByteWebConnector.API.Controllers
{
    [Route("api/ByteWebConnector/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        #region Private Fields

        private readonly ILoanApplicationService _loanApplicationService;
        private readonly ICommonService _commonService;
        private string _apiUrl;
        #endregion
        #region Constructor

        public DocumentController(ILoanApplicationService loanApplicationService, ICommonService commonService)
        {
            _loanApplicationService = loanApplicationService;
            _commonService = commonService;
        }

        #endregion
        #region Action Methods

        // GET: api/<DocumentController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
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
        public IActionResult SendDocument([FromBody] SendDocumentRequest request
                                 )
        {

            var loanApplication = _loanApplicationService
                                  .GetLoanApplicationWithDetails(id: request.LoanApplicationId,
                                                                 includes: null)
                                  .SingleOrDefault();

            if (loanApplication != null)
            {
                var documentUploadModel = new DocumentUploadModel
                                          {
                                              FileDataId = Convert.ToInt64(loanApplication.EncompassNumber),
                                              DocumentCategory = "PROP",//request.DocumentCategory,
                                              DocumentExtenstion = "jpg",//request.DocumentExension,
                                              DocumentName = "Senior React Engineer",//request.DocumentName,
                                              DocumentStatus = "0",//request.DocumentStatus,
                                              DocumentType = "PurchaseAgr",//request.DocumentType,
                                              DocumentData = request.FileData.ToBase64String()
                                          };
                #region BytePro API Call

                string byteProSession = GetByteProSession();
                IActionResult documentResponse = SendDocumentToByte(documentUploadModel,
                                                            byteProSession);
                return documentResponse;

                #endregion
            }
            else
            {
                return BadRequest();
            }



        }

        // PUT api/<DocumentController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DocumentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        #endregion

        #region Private Methods

        private string GetByteProSession()
        {
            try
            {
                var userName      = _commonService.GetSettingValueByKeyAsync<string>(SystemSettingKeys.ByteProApiUserName).Result;
                var password      = _commonService.GetSettingValueByKeyAsync<string>(SystemSettingKeys.ByteProApiPassword).Result;
                var authKey       = _commonService.GetSettingValueByKeyAsync<string>(SystemSettingKeys.ByteApiAuthKey).Result;
                _apiUrl                 = _commonService.GetSettingValueByKeyAsync<string>(SystemSettingKeys.ByteApiUrl).Result;
                System.Net.ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(_apiUrl + "auth/ ");
                request.Method = "GET";
                request.ContentType = "application/json";
                request.Headers.Add("authorizationKey", authKey);
                request.Headers.Add("username", userName);
                request.Headers.Add("password", password);
                request.Accept = "application/json";
                String test = String.Empty;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    test = reader.ReadToEnd();
                    reader.Close();
                    dataStream.Close();

                }
                return test.Replace("\"", "");



            }
            catch (Exception ex)
            {
                throw new System.ArgumentException(ex.InnerException != null ? ex.InnerException.Message : "Error in BytePro Connection, Please try again later.");
            }

        }
        private IActionResult SendDocumentToByte(DocumentUploadModel documentUploadModel,string session)
        {
            try
            {
                string output = JsonConvert.SerializeObject(documentUploadModel, Newtonsoft.Json.Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                //string output = JsonConvert.SerializeObject(loanInfo);
                Task<string> response = Send(output,session);
                var settings = new JsonSerializerSettings
                               {
                                   NullValueHandling = NullValueHandling.Ignore,
                                   MissingMemberHandling = MissingMemberHandling.Ignore
                               };
                DocumentUploadModel document = JsonConvert.DeserializeObject<DocumentUploadModel>(response.Result, settings);
                if (document != null && document.FileDataId != 0)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                throw new System.ArgumentException(ex.InnerException != null ? ex.InnerException.Message : "BytePro Error in API Update File Call");
            }
            return null;
        }
        private async Task<string> Send(string output,string session)
        {
            try
            {
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                using (var client = new HttpClient(clientHandler))
                {
                    var request = new HttpRequestMessage()
                    {
                        RequestUri = new Uri(_apiUrl + "Document/"),
                        Method = HttpMethod.Post,
                        Content = new StringContent(output, System.Text.Encoding.UTF8, "application/json")

                    };
                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    request.Headers.Add("Session", session);
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
        public static String ToBase64String(this Byte[] inArray, Base64FormattingOptions options)
        {
            return Convert.ToBase64String(inArray, options);
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
        public static String ToBase64String(this Byte[] inArray, Int32 offset, Int32 length)
        {
            return Convert.ToBase64String(inArray, offset, length);
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
        public static String ToBase64String(this Byte[] inArray, Int32 offset, Int32 length, Base64FormattingOptions options)
        {
            return Convert.ToBase64String(inArray, offset, length, options);
        }
    }
}
