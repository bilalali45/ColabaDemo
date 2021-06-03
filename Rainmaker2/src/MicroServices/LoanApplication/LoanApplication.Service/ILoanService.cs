using LoanApplication.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TenantConfig.Common.DistributedCache;

namespace LoanApplication.Service
{
    public interface ILoanService : IServiceBase<LoanApplicationDb.Entity.Models.LoanApplication>
    {
        Task<PendingLoanApplicationModel> GetPendingLoanApplication(TenantModel tenant, int userId, int? loanApplicationId);


        Task<LoanApplicationReview> GetLoanApplicationForBorrowersInfoSectionReview(TenantModel tenant,
                                                                        int userId,
                                                                        int? loanApplicationId);

        Task<LoanApplicationFirstReview> GetLoanApplicationForFirstReview(TenantModel tenant, int userId,
            int? loanApplicationId);
        Task<LoanApplicationSecondReview> GetLoanApplicationForSecondReview(TenantModel tenant, int userId,
            int? loanApplicationId);

        Task<int> UpdateState(TenantModel tenant, int userId, UpdateStateModel model);
        Task<List<LoanSummary>> GetDashboardLoanInfo(TenantModel tenant, int userId);
        Task<object> SubmitLoanApplication(TenantModel tenant, int userId, LoanCommentsModel model);
    }
}
