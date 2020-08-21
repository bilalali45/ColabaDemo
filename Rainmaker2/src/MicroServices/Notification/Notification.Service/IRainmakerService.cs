using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Notification.Model;

namespace Notification.Service
{
    public interface IRainmakerService
    {
        Task<List<int>> GetAssignedUsers(int loanApplicationId);
        Task<LoanSummary> GetLoanSummary(int loanApplicationId);
    }
}
