using DocumentManagement.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocumentManagement.Service
{
    public interface IRainmakerService
   {
       Task<string> PostLoanApplication(int loanApplicationId, bool isDraft, IEnumerable<string> authHeader);
       Task SendBorrowerEmail(int loanApplicationId,string toAddess,string ccAddress,string fromAddress,string subject, string emailBody, int activityForId, int userId, string userName, IEnumerable<string> authHeader);
       Task<LoanApplicationModel> GetByLoanApplicationId(int loanApplicationId, IEnumerable<string> authHeader);
        Task UpdateLoanInfo(int? loanApplicationId, string id, IEnumerable<string> authHeader);
        Task<int> GetLoanApplicationId(string loanApplicationId);
    }
}
