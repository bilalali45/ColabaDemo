using Rainmaker.Model;
using RainMaker.Entity.Models;
using RainMaker.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Rainmaker.Service
{
    public interface ILoanApplicationService : IServiceBase<LoanApplication>
    {
        Task<LoanSummary> GetLoanSummary(int loanApplicationId, int userProfileId);
        Task<LoanOfficer> GetLOInfo(int loanApplicationId, int businessUnitId, int userProfileId);
        Task<LoanOfficer> GetDbaInfo(int businessUnitId);
        Task<AdminLoanSummary> GetAdminLoanSummary(int loanApplicationId);
        Task<PostModel> PostLoanApplication(int loanApplicationId, bool isDraft, int userProfileId, IOpportunityService opportunityService);
        Task<LoanApplicationModel> GetByLoanApplicationId(int loanId);
    }
}
