using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RainMaker.Common.Extensions;
using RainMaker.Data;
using RainMaker.Entity.Models;
using RainMaker.Service;
using URF.Core.Abstractions;

namespace Rainmaker.Service
{
    public class BorrowerService : ServiceBase<RainMakerContext, Borrower>, IBorrowerService
    {
        [Flags]
        public enum RelatedEntity
        {
            LoanContact = 1 << 0,
            LoanContact_Ethnicity = 1 << 1,
        }


        public BorrowerService(IUnitOfWork<RainMakerContext> previousUow,
                               IServiceProvider services) : base(previousUow: previousUow,
                                                                 services: services)
        {
        }


        public List<Borrower> GetBorrowerWithDetails(
                                                     string firstName = "",
                                                     string lastName = "",
                                                     string email = "",
                                                     int? loanApplicationId = null,
                                                     string encompassId = "",
                                                     RelatedEntity? includes = null)
        {
            var borrowers = Repository.Query().AsQueryable();

            if (firstName.HasValue()) borrowers = borrowers.Where(predicate: b => b.LoanContact.FirstName == firstName);
            if (lastName.HasValue()) borrowers = borrowers.Where(predicate: b => b.LoanContact.LastName == lastName);
            if (email.HasValue()) borrowers = borrowers.Where(predicate: b => b.LoanContact.EmailAddress == email);
            if (loanApplicationId.HasValue()) borrowers = borrowers.Where(predicate: b => b.LoanApplicationId == loanApplicationId);
            if (encompassId.HasValue()) borrowers = borrowers.Where(predicate: b => b.LoanApplication.EncompassId== encompassId);

            if (includes.HasValue)
                ProcessIncludes(query: borrowers,
                                includes: includes.Value);

            return borrowers.ToList();
        }


        private void ProcessIncludes(IQueryable<Borrower> query,
                                     RelatedEntity includes)
        {
            if (includes.HasFlag(flag: RelatedEntity.LoanContact))
                query = query.Include(navigationPropertyPath: b => b.LoanContact);
            if (includes.HasFlag(flag: RelatedEntity.LoanContact_Ethnicity))
                query = query.Include(navigationPropertyPath: b =>
                                          b.LoanContact.LoanContactEthnicityBinders.Select(d => d.Ethnicity));
        }
    }
}