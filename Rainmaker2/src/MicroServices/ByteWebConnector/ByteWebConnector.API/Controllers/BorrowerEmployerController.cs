using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LosIntegration.API.ExtensionMethods;
using LosIntegration.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LosIntegration.API.Controllers
{
    [Route(template: "api/ByteWebConnector/[controller]")]
    [ApiController]
    public class BorrowerEmployerController : ControllerBase
    {
        #region Constructors

        public BorrowerEmployerController(IHttpClientFactory clientFactory,
                                           IConfiguration configuration
                                  )
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
            _httpClient = _clientFactory.CreateClient(name: "clientWithCorrelationId");
            //_tokenService = tokenService;
            //_logger = logger;
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
        public async Task<IActionResult> PostAsync(ByteEmployer byteEmployer)
        {
            var rainmakerBorrowerEmployer = byteEmployer.GetRainmakerBorrowerEmployer();

            var content = rainmakerBorrowerEmployer.ToJsonString();

            var callResponse =
                await _httpClient.PostAsync(requestUri:
                                           $"{_configuration[key: "RainMaker:Url"]}/api/rainmaker/BorrowerEmployer/AddOrUpdate",
                                           content: new StringContent(content: content,
                                                                      encoding: Encoding.UTF8,
                                                                      mediaType: "application/json"));
            if (callResponse.IsSuccessStatusCode)
                return Ok();
            return BadRequest();

            //return null;
        }

        #endregion

        #region Put

        // PUT api/<BorrowerController>/5
        [HttpPut(template: "{id}")]
        public void Put(int id,
                        [FromBody] string value)
        {
        }

        #endregion

        #region Delete

        // DELETE api/<BorrowerController>/5
        [HttpDelete(template: "{id}")]
        public void Delete(int id)
        {
        }

        #endregion

        #endregion

        #region Private Variables

        private readonly IHttpClientFactory _clientFactory;

        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        #endregion
    }
}