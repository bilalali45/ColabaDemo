using LoanApplication.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TenantConfig.Common.DistributedCache;

namespace LoanApplication.Service
{
    public interface ILoanGoalService : IServiceBase<LoanApplicationDb.Entity.Models.LoanGoal>
    {
        Task<int> AddOrUpdate(TenantModel tenant, int userId, AddOrUpdateLoanGoalModel model);
        Task<GetLoanGoalModel> GetLoanGoal(TenantModel tenant, int loanApplicationId, int userId);
    }
}
