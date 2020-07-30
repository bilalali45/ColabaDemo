using System.Collections.Generic;
using ByteWebConnector.API.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ByteWebConnector.API.Controllers
{
    [Route(template: "api/ByteWebConnector/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        [Route("borrower")]
        // POST api/<ValuesController>
        [HttpPost]
        public void Post(ByteBorrower request)
        {
           
        }

        // POST api/<ValuesController>
        [Route("subprop")]
        [HttpPost]
        public void Post(ByteSubProperty request)
        {
            

        }
        [Route("loan")]
        [HttpPost]
        public void Post(Models.ByteLoanInfo request)
        {

        }
        [Route("application")]
        [HttpPost]
        public void Post(ByteApplication request)
        {

        }
        [Route("party")]
        [HttpPost]
        public void Post(Party request)
        {

        }
        [Route("prepaid")]
        [HttpPost]
        public void Post(PrepaidItem request)
        {

        }
        [Route("custom")]
        [HttpPost]
        public void Post(CustomFields request)
        {

        }
        [Route("status")]
        [HttpPost]
        public void Post(ByteStatus request)
        {

        }
        [Route("filedata")]
        [HttpPost]
        public void Post(ByteFileData request)
        {

        }
        [Route("debt")]
        [HttpPost]
        public void Post(Debt request)
        {

        }
        [Route("residence")]
        [HttpPost]
        public void Post(Residence request)
        {

        }
        [Route("asset")]
        [HttpPost]
        public void Post(Asset request)
        {

        }
        [Route("employer")]
        [HttpPost]
        public void Post(Employer request)
        {

        }
        [Route("income")]
        [HttpPost]
        public void Post(Income request)
        {

        }
        [Route("reo")]
        [HttpPost]
        public void Post(REO request)
        {

        }
        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    public class RequestModel
    {
        public string Value { get; set; }
    }
}
