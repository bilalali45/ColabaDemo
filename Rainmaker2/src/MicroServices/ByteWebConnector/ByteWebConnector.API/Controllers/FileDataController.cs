using ByteWebConnector.API.ExtensionMethods;
using ByteWebConnector.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ByteWebConnector.API.Controllers
{
    [Route(template: "api/ByteWebConnector/[controller]")]
    [ApiController]
    public class FileDataController : ControllerBase
    {
        #region Constructors

        public FileDataController(IHttpClientFactory clientFactory,
                                     IConfiguration configuration
                                  )
        {
            IHttpClientFactory _clientFactory = clientFactory;
            _configuration = configuration;
            _httpClient = _clientFactory.CreateClient(name: "clientWithCorrelationId");
        }

        #endregion

        #region Action Methods

        #region Get

        // GET: api/<BorrowerController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new[]
                   {
                       "value1",
                       "value2"
                   };
        }


        // GET api/<BorrowerController>/5
        [HttpGet(template: "{id}")]
        public string Get(int id)
        {
            return "request";
        }

        #endregion

        #region Post

        // POST api/<BorrowerController>
        [Route(template: "update")]
        [HttpPost]
        public async Task<IActionResult> PostAsync(ByteFileData byteFileData)
        {
            var fileData = byteFileData.GetFileData();

            var content = fileData.ToJsonString();

            var callResponse =
                await _httpClient.PostAsync(requestUri:
                                           $"{_configuration[key: "LosIntegration:Url"]}/api/LosIntegration/FileData/AddOrUpdate",
                                           content: new StringContent(content: content,
                                                                      encoding: Encoding.UTF8,
                                                                      mediaType: "application/json"));
            if (callResponse.IsSuccessStatusCode)
                return Ok();
            return BadRequest();
        }

        #endregion

        #region Put

        // PUT api/<BorrowerController>/5
        [HttpPut(template: "{id}")]
        public void Put(int id,
                        [FromBody] string value)
        {
            throw new NotSupportedException();
        }

        #endregion

        #region Delete

        // DELETE api/<BorrowerController>/5
        [HttpDelete(template: "{id}")]
        public void Delete(int id)
        {
            throw new NotSupportedException();
        }

        #endregion

        #endregion

        #region Private Variables

        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        #endregion
    }

}