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
    public class LoanRequestService : ServiceBase<RainMakerContext, LoanRequest>, ILoanRequestService
    {

        public LoanRequestService(IUnitOfWork<RainMakerContext> previousUow,
                                  IServiceProvider services, ICommonService commonService) : base(previousUow: previousUow,
                                                                                                  services: services)
        {
            
        }

        [Flags]
        public enum RelatedEntities
        {
            BusinessUnit = 1 << 0,
        }


        public List<LoanRequest> GetLoanRequestWithDetails(int? id = null,
                                                           int? loanApplicationId = null,
                                                           int? opportunityId = null,
                                                           RelatedEntities? includes = null)
        {
            var loanRequests = Repository.Query().AsQueryable();

            // @formatter:off 

            if (id.HasValue()) loanRequests = loanRequests.Where(predicate: loanRequest => loanRequest.Id == id);
            if (loanApplicationId.HasValue()) loanRequests = loanRequests.Where(predicate: loanRequest => loanRequest.Opportunities.Any(opportunity=>opportunity.LoanApplications.Any(loanApplication=>loanApplication.Id ==loanApplicationId)));
            if (opportunityId.HasValue()) loanRequests = loanRequests.Where(predicate: loanRequest => loanRequest.Opportunities.Any(op=>op.Id ==opportunityId));

            // @formatter:on 

            if (includes.HasValue)
                loanRequests = ProcessIncludes(query: loanRequests,
                                               includes: includes.Value);

            return loanRequests.ToList();
        }


        private IQueryable<LoanRequest> ProcessIncludes(IQueryable<LoanRequest> query,
                                                        RelatedEntities includes)
        {
            // @formatter:off 
            if (includes.HasFlag(flag: RelatedEntities.BusinessUnit)) query = query.Include(navigationPropertyPath: loanRequest => loanRequest.BusinessUnit);
            // @formatter:on 
            return query;
        }
    }
}