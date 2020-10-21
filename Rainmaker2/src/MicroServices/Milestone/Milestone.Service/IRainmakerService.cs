using System.Collections.Generic;
using System.Threading.Tasks;

namespace Milestone.Service
{
    public interface IRainmakerService
    {
        Task SetMilestoneId(int loanApplicationId,int milestoneId, IEnumerable<string> auth);
        Task<int> GetMilestoneId(int loanApplicationId, IEnumerable<string> auth);
        Task<int> GetLoanApplicationId(string loanId, short losId, IEnumerable<string> auth);
        Task SendEmailToSupport(int tenantId, string milestone, string loanId, int rainmakerLosId, IEnumerable<string> auth);
    }
}
