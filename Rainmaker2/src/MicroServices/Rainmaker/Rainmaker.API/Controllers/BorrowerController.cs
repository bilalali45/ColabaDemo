﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Rainmaker.Model.Borrower;
using Rainmaker.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Rainmaker.API.Controllers
{
    [Route(template: "api/[controller]")]
    [ApiController]
    public class BorrowerController : ControllerBase
    {
        private readonly IBorrowerService _borrowerService;


        public BorrowerController(IBorrowerService borrowerService)
        {
            _borrowerService = borrowerService;
        }


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
            return "value";
        }


        // POST api/<BorrowerController>
        [HttpPost]
        public IActionResult AddOrUpdate(RainmakerBorrower rainmakerBorrowerModel,bool addIfNotExists = false)
        {
            var borrowerEntity = _borrowerService.GetBorrowerWithDetails(encompassId:rainmakerBorrowerModel.FileDataId,
                                                                         firstName: rainmakerBorrowerModel.FirstName,
                                                                                    email: rainmakerBorrowerModel.EmailAddress,
                                                                                     includes: BorrowerService.RelatedEntity.LoanContact_Ethnicity).SingleOrDefault();

            if (borrowerEntity == null)
            {
                if (addIfNotExists)
                {
                    // insertLogic
                }
            }

            rainmakerBorrowerModel.PopulateEntity(borrowerEntity);

            _borrowerService.Update(item: borrowerEntity);
            _borrowerService.SaveChangesAsync();
            return Ok();
        }


        // PUT api/<BorrowerController>/5
        [HttpPut(template: "{id}")]
        public void Put(int id,
                        [FromBody] string value)
        {
        }


        // DELETE api/<BorrowerController>/5
        [HttpDelete(template: "{id}")]
        public void Delete(int id)
        {
        }
    }
}