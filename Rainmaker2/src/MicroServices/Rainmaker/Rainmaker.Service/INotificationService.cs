using Rainmaker.Model;
using RainMaker.Entity.Models;
using RainMaker.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rainmaker.Service
{
    public interface INotificationService : IServiceBase<LoanApplication>
    {
        Task<List<int>> GetAssignedUsers(int loanApplicationId);
        Task<LoanSummary> GetLoanSummary(int loanApplicationId);
    }
}
