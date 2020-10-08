using Notification.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notification.Service
{
    public interface IRainmakerService
    {
        Task<List<int>> GetAssignedUsers(int loanApplicationId);
        Task<LoanSummary> GetLoanSummary(int loanApplicationId);
    }
}
