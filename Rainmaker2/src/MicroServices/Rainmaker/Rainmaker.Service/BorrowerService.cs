using Microsoft.EntityFrameworkCore;
using RainMaker.Common.Extensions;
using RainMaker.Data;
using RainMaker.Entity.Models;
using RainMaker.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using URF.Core.Abstractions;

namespace Rainmaker.Service
{
    public class BorrowerService : ServiceBase<RainMakerContext, Borrower>, IBorrowerService
    {
        [Flags]
        public enum RelatedEntities
        {
            LoanContact = 1 << 0,
            LoanContact_Ethnicity = 1 << 1,
            LoanApplication = 1 << 2,
            LoanContact_Race = 1 << 3,
            BorrowerQuestionResponses = 1 << 4,
            BorrowerQuestionResponses_QuestionResponse = 1 << 5,
        }


        public BorrowerService(IUnitOfWork<RainMakerContext> previousUow,
                               IServiceProvider services) : base(previousUow: previousUow,
                                                                 services: services)
        {
        }


        public List<Borrower> GetBorrowerWithDetails(string firstName = "",
                                                     string lastName = "",
                                                     string email = "",
                                                     int? loanApplicationId = null,
                                                     string encompassId = "",
                                                     RelatedEntities? includes = null)
        {
            var borrowers = Repository.Query().AsQueryable();

            // @formatter:off 
            if (firstName.HasValue())                       borrowers = borrowers.Where(predicate:b => b.LoanContact.FirstName == firstName);
            if (lastName.HasValue())                        borrowers = borrowers.Where(predicate:b => b.LoanContact.LastName == lastName);
            if (email.HasValue())                           borrowers = borrowers.Where(predicate:b => b.LoanContact.EmailAddress == email);
            if (loanApplicationId.HasValue())               borrowers = borrowers.Where(predicate:b => b.LoanApplicationId == loanApplicationId);
            if (encompassId.HasValue())                     borrowers = borrowers.Where(predicate:b => b.LoanApplication.EncompassNumber == encompassId);
            // @formatter:on 

            if (includes.HasValue)
                borrowers = ProcessIncludes(query: borrowers,
                                            includes: includes.Value);

            return borrowers.ToList();
        }


        private IQueryable<Borrower> ProcessIncludes(IQueryable<Borrower> query,
                                                     RelatedEntities includes)
        {
            // @formatter:off 
            if (includes.HasFlag(flag:RelatedEntities.LoanContact))                         query = query.Include(navigationPropertyPath:borrower => borrower.LoanContact);
            if (includes.HasFlag(flag:RelatedEntities.LoanContact_Ethnicity))               query = query.Include(navigationPropertyPath:borrower => borrower.LoanContact).ThenInclude(navigationPropertyPath:loanContact => loanContact.LoanContactEthnicityBinders).ThenInclude(navigationPropertyPath:ethnicityBinder => ethnicityBinder.Ethnicity);
            if (includes.HasFlag(flag:RelatedEntities.LoanApplication))                     query = query.Include(navigationPropertyPath:borrower => borrower.LoanApplication);
            if (includes.HasFlag(flag:RelatedEntities.LoanContact_Race))                    query = query.Include(navigationPropertyPath:borrower => borrower.LoanContact).ThenInclude(navigationPropertyPath:loanContact => loanContact.LoanContactRaceBinders).ThenInclude(navigationPropertyPath:raceBinder => raceBinder.Race);
            if (includes.HasFlag(flag: RelatedEntities.BorrowerQuestionResponses))          query = query.Include(navigationPropertyPath: borrower => borrower.BorrowerQuestionResponses);
            if (includes.HasFlag(flag: RelatedEntities.BorrowerQuestionResponses_QuestionResponse))          query = query.Include(navigationPropertyPath: borrower => borrower.BorrowerQuestionResponses).ThenInclude(borrowerQuestionResponse=>borrowerQuestionResponse.QuestionResponse);
            // @formatter:on 
            return query;
        }
    }
}