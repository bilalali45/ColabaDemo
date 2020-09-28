using System.Collections.Generic;
using System.Threading.Tasks;

namespace LosIntegration.Service
{
    public interface IRainmakerService
   {
      
       Task SendBorrowerEmail(int loanApplicationId, string emailBody, int activityForId, int userId, string userName, IEnumerable<string> authHeader);

        Task SendEmailSupportTeam(int loanApplicationId, int TenantId, string ErrorDate, string EmailBody, int ErrorCode, string DocumentCategory, string DocumentName, string DocumentExension, IEnumerable<string> authHeader);
       }
}
