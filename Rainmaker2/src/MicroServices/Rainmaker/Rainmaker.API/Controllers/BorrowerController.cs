using Microsoft.AspNetCore.Mvc;
using Rainmaker.Model.Borrower;
using Rainmaker.Service;
using RainMaker.Entity.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackableEntities.Common.Core;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Rainmaker.API.Controllers
{
    [ApiController]
    [Route(template: "/api/rainmaker/[controller]")]
    public class BorrowerController : ControllerBase
    {
        private readonly IBorrowerService _borrowerService;
        private readonly ILoanApplicationService _loanApplicationService;


        public BorrowerController(IBorrowerService borrowerService,
                                  ILoanApplicationService loanApplicationService)
        {
            _borrowerService = borrowerService;
            _loanApplicationService = loanApplicationService;
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
        [HttpPost(template: "[action]")]
        public async Task<IActionResult> AddOrUpdate(RainmakerBorrower rainmakerBorrowerModel,
                                                     bool addIfNotExists = false)
        {
            var firstName = rainmakerBorrowerModel.FirstName;
            var email = rainmakerBorrowerModel.EmailAddress;

            if (!string.IsNullOrEmpty(value: rainmakerBorrowerModel.OldFirstName) &&
                rainmakerBorrowerModel.OldFirstName != rainmakerBorrowerModel.FirstName)
                firstName = rainmakerBorrowerModel.OldFirstName;
            if (!string.IsNullOrEmpty(value: rainmakerBorrowerModel.OldEmailAddress) &&
                rainmakerBorrowerModel.OldEmailAddress != rainmakerBorrowerModel.EmailAddress)
                email = rainmakerBorrowerModel.OldEmailAddress;

            var loanApplication = _loanApplicationService
                                  .GetLoanApplicationWithDetails(encompassNumber: rainmakerBorrowerModel.FileDataId,
                                                                 includes: LoanApplicationService
                                                                           .RelatedEntities.Borrower)
                                  .SingleOrDefault();

            var borrowerEntity = _borrowerService.GetBorrowerWithDetails(encompassId: rainmakerBorrowerModel.FileDataId,
                                                                         firstName: firstName,
                                                                         email: email,
                                                                         // @formatter:off
                                                                         includes: BorrowerService.RelatedEntities.LoanContact_Ethnicity |
                                                                                   BorrowerService.RelatedEntities.LoanContact_Race |
                                                                                   BorrowerService.RelatedEntities.LoanApplication|
                                                                                   BorrowerService.RelatedEntities.BorrowerQuestionResponses_QuestionResponse
                                                                         // @formatter:on
                                                                        )
                                                 .SingleOrDefault();

            if (borrowerEntity == null)
            {
                // insertLogic
                if (rainmakerBorrowerModel.IsAddOrUpdate)
                {
                    borrowerEntity = new Borrower
                                     {
                                         TrackingState = TrackingState.Added,
                                     };
                    borrowerEntity.LoanContact = new LoanContact
                                                 {
                                                     TrackingState = TrackingState.Added
                                                 };
                    rainmakerBorrowerModel.PopulateEntity(entity: borrowerEntity);
                    if (loanApplication != null)
                    {
                        loanApplication.Borrowers.Add(item: borrowerEntity);
                        loanApplication.TrackingState = TrackingState.Modified;
                        _loanApplicationService.Update(item: loanApplication);
                        await _loanApplicationService.SaveChangesAsync();
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
            }
            else
            {
                borrowerEntity.LoanContact.TrackingState = TrackingState.Modified;
                rainmakerBorrowerModel.PopulateEntity(entity: borrowerEntity);
                _borrowerService.Update(item: borrowerEntity);
                await _borrowerService.SaveChangesAsync();
            }

            return Ok();
        }


        // PUT api/<BorrowerController>/5
        [HttpPut(template: "{id}")]
        public void Put(int id,
                        [FromBody] string value)
        {
            throw new System.NotSupportedException();
        }


        // DELETE api/<BorrowerController>/5
        [HttpDelete(template: "{id}")]
        public void Delete(int id)
        {
            throw new System.NotSupportedException();
        }
    }
}