using System.Collections.Generic;
using LosIntegration.API.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LosIntegration.API.Controllers
{
    [Route(template: "api/LosIntegration/[controller]")]
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
        public void Post(Borrower request)
        {
           
        }

        // POST api/<ValuesController>
        [Route("subprop")]
        [HttpPost]
        public void Post(SubProperty request)
        {
            

        }
        [Route("loan")]
        [HttpPost]
        public void Post(Models.LoanInfo request)
        {

        }
        [Route("application")]
        [HttpPost]
        public void Post(ByteApplication request)
        {

        }
        [Route("party")]
        [HttpPost]
        public void Post(Parties request)
        {

        }
        [Route("prepaid")]
        [HttpPost]
        public void Post(PrepaidItem request)
        {

        }
        [Route("custom")]
        [HttpPost]
        public void Post(ByteCustomValue request)
        {

        }
        [Route("status")]
        [HttpPost]
        public void Post(Status request)
        {

        }
        [Route("filedata")]
        [HttpPost]
        public void Post(FileData request)
        {

        }
        [Route("debt")]
        [HttpPost]
        public void Post(Liability request)
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
