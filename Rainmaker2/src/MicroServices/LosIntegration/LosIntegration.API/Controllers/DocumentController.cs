using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using LosIntegration.API.ExtensionMethods;
using LosIntegration.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LosIntegration.API.Controllers
{
    [Route("api/LosIntegration/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        #region Private Variables

        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        #endregion

        #region Constructors

        public DocumentController(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = clientFactory.CreateClient(name: "clientWithCorrelationId");
            //_tokenService = tokenService;
            //_logger = logger;
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
        public IActionResult SendToExternalOriginator([FromBody] SendToExternalOriginatorRequest request)
        {
            string tenantId = "1";

            _httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", Request.Headers["Authorization"].ToString().Replace("Bearer ", ""));

            //var csResponse = _httpClient.GetAsync($"{_configuration["ServiceAddress:DocumentManagement:Url"]}/api/DocumentManagement/document/view?id={request.DocumentLoanApplicationId}&requestId={request.RequestId}&docId={request.DocumentId}&fileId={request.FileId}&tenantId={tenantId}").Result;
            var url =
                $"https://alphamaingateway.rainsoftfn.com/api/documentmanagement/document/view?id={request.DocumentLoanApplicationId}&requestId={request.RequestId}&docId={request.DocumentId}&fileId={request.FileId}&tenantId={tenantId}";
            var csResponse = _httpClient.GetAsync(url).Result;


            var fileData = csResponse.Content.ReadAsByteArrayAsync().Result;

            if (!csResponse.IsSuccessStatusCode)
            {
                throw new Exception("Unable to load Document from Document Management");
            }
            else
            {
                SendDocumentResponse sendDocumentResponse = new SendDocumentResponse()
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
                if (callResponse.IsSuccessStatusCode)
                    return Ok();
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
    }

}
