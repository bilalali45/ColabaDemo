using System;
using System.Collections.Generic;
using System.Linq;
using ByteWebConnector.API.Models;
using ByteWebConnector.API.Models.ClientModels;
using ByteWebConnector.Service.DbServices;
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
        private readonly ISettingService _settingService;


        public ValuesController(ISettingService settingService)
        {
            _settingService = settingService;
        }


        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        [Route("[action]")]
        public void DBConnTest()
        {
            _settingService.GetByteProSettings();
        }

        [HttpGet]
        [Route("TestImageWrap")]
        public void TestImageWrap()
        {
            var imageBytes = System.IO.File.ReadAllBytes(@"C:\Users\user\Downloads\Foo.jpg");

           var pdfBytes =  Utility.Helper.WrapImagesInPdf(new List<byte[]> { imageBytes });

            System.IO.File.WriteAllBytes(@"C:\Users\user\Downloads\Foo.pdf", pdfBytes.First());


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
            throw new NotSupportedException();
        }

        // POST api/<ValuesController>
        [Route("subprop")]
        [HttpPost]
        public void Post(ByteSubProperty request)
        {
            throw new NotSupportedException();
        }
        [Route("loan")]
        [HttpPost]
        public void Post(Models.ByteLoanInfo request)
        {
            throw new NotSupportedException();
        }
        [Route("application")]
        [HttpPost]
        public void Post(ByteApplication request)
        {
            throw new NotSupportedException();
        }
        [Route("party")]
        [HttpPost]
        public void Post(ByteParties request)
        {
            throw new NotSupportedException();
        }
        [Route("prepaid")]
        [HttpPost]
        public void Post(BytePrepaidItem request)
        {
            throw new NotSupportedException();
        }
        [Route("custom")]
        [HttpPost]
        public void Post(ByteCustomValue request)
        {
            throw new NotSupportedException();
        }
        [Route("status")]
        [HttpPost]
        public void Post(ByteStatus request)
        {
            throw new NotSupportedException();
        }
        [Route("filedata")]
        [HttpPost]
        public void Post(ByteFileData request)
        {
            throw new NotSupportedException();
        }
        [Route("debt")]
        [HttpPost]
        public void Post(ByteLiability request)
        {
            throw new NotSupportedException();
        }
        [Route("residence")]
        [HttpPost]
        public void Post(ByteResidence request)
        {
            throw new NotSupportedException();
        }
        [Route("asset")]
        [HttpPost]
        public void Post(ByteAsset request)
        {
            throw new NotSupportedException();
        }
        [Route("employer")]
        [HttpPost]
        public void Post(ByteEmployer request)
        {
            throw new NotSupportedException();
        }
        [Route("income")]
        [HttpPost]
        public void Post(ByteIncome request)
        {
            throw new NotSupportedException();
        }
        [Route("reo")]
        [HttpPost]
        public void Post(ByteREO request)
        {
            throw new NotSupportedException();
        }
        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            throw new NotSupportedException();
        } 

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            throw new NotSupportedException();
        }
    }

    public class RequestModel
    {
        public string Value { get; set; }
    }
}
