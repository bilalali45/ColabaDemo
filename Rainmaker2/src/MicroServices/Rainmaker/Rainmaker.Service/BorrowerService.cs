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
            LoanApplication = 1 << 2,
            LoanContact_Race = 1 << 3,
        }


        public BorrowerService(IUnitOfWork<RainMakerContext> previousUow,
            IServiceProvider services) : base(previousUow,
            services)
        {
        }


        public List<Borrower> GetBorrowerWithDetails(
            string firstName = null,
            string lastName = null,
            string email = "",
            int? loanApplicationId = null,
            string encompassId = "",
            RelatedEntity? includes = null)
        {
            var borrowers = Repository.Query().AsQueryable();
            // ReSharper disable formatting
            if (firstName.HasValue())                       borrowers = borrowers.Where(b => b.LoanContact.FirstName == firstName);
            if (lastName.HasValue())                        borrowers = borrowers.Where(b => b.LoanContact.LastName == lastName);
            if (email.HasValue())                           borrowers = borrowers.Where(b => b.LoanContact.EmailAddress == email);
            if (loanApplicationId.HasValue())               borrowers = borrowers.Where(b => b.LoanApplicationId == loanApplicationId);
            if (encompassId.HasValue())                     borrowers = borrowers.Where(b => b.LoanApplication.EncompassNumber == encompassId);
            // ReSharper enable formatting

            if (includes.HasValue)
                borrowers = ProcessIncludes(borrowers,
                    includes.Value);

            return borrowers.ToList();
        }


        private IQueryable<Borrower> ProcessIncludes(IQueryable<Borrower> query,
            RelatedEntity includes)
        {
            // ReSharper disable formatting
            if (includes.HasFlag(RelatedEntity.LoanContact))                query = query.Include(borrower => borrower.LoanContact);
            if (includes.HasFlag(RelatedEntity.LoanContact_Ethnicity))      query = query.Include(borrower => borrower.LoanContact).ThenInclude(loanContact => loanContact.LoanContactEthnicityBinders).ThenInclude(ethnicityBinder => ethnicityBinder.Ethnicity);
            if (includes.HasFlag(RelatedEntity.LoanApplication))            query = query.Include(borrower => borrower.LoanApplication);
            if (includes.HasFlag(RelatedEntity.LoanContact_Race))           query = query.Include(borrower => borrower.LoanContact).ThenInclude(loanContact => loanContact.LoanContactRaceBinders).ThenInclude(raceBinder => raceBinder.Race);
            // ReSharper enable formatting
            return query;
        }
    }
}