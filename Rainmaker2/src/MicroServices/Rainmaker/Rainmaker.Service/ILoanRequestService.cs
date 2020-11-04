using System.Collections.Generic;
using RainMaker.Entity.Models;
using RainMaker.Service;

namespace Rainmaker.Service
{
    public interface ILoanRequestService : IServiceBase<LoanRequest>
    {
        List<LoanRequest> GetLoanRequestWithDetails(int? id = null,
                                                    int? loanApplicationId = null,
                                                    int? opportunityId = null,
                                                    LoanRequestService.RelatedEntities? includes = null);
    }
}