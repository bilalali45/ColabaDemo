﻿using System.Collections.Generic;
using System.Linq;
using ByteWebConnector.API.Models;
using ByteWebConnector.API.Models.ClientModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.IdentityModel.Tokens;

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

        [HttpGet]
        [Route("TestImageWrap")]
        public IEnumerable<string> TestImageWrap()
        {
            var imageBytes = System.IO.File.ReadAllBytes(@"C:\Users\user\Downloads\Foo.jpg");

           var pdfBytes =  Utility.Helper.WrapImagesInPdf(new List<byte[]> { imageBytes });

            System.IO.File.WriteAllBytes(@"C:\Users\user\Downloads\Foo.pdf", pdfBytes.First());


            return null;
        }

        // GET api/<ValuesController>/5
        [HttpGet("{ id}")]
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
        public void Post(ByteParties request)
        {

        }
        [Route("prepaid")]
        [HttpPost]
        public void Post(BytePrepaidItem request)
        {

        }
        [Route("custom")]
        [HttpPost]
        public void Post(ByteCustomValue request)
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
        public void Post(ByteLiability request)
        {

        }
        [Route("residence")]
        [HttpPost]
        public void Post(ByteResidence request)
        {

        }
        [Route("asset")]
        [HttpPost]
        public void Post(ByteAsset request)
        {

        }
        [Route("employer")]
        [HttpPost]
        public void Post(ByteEmployer request)
        {

        }
        [Route("income")]
        [HttpPost]
        public void Post(ByteIncome request)
        {

        }
        [Route("reo")]
        [HttpPost]
        public void Post(ByteREO request)
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