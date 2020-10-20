using System.Collections.Generic;
using System.Threading.Tasks;

namespace Milestone.Service
{
    public interface IRainmakerService
    {
        Task SetMilestoneId(int loanApplicationId,int milestoneId);
        Task<int> GetMilestoneId(int loanApplicationId, IEnumerable<string> auth);
        Task<int> GetLoanApplicationId(string loanId, short losId);
        Task SendEmailToSupport(int tenantId, string milestone, string loanId, int rainmakerLosId);
    }
}
