using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocManager.Service
{
   public interface IRainmakerService
   {
       Task<string> PostLoanApplication(int loanApplicationId, bool isDraft, IEnumerable<string> authHeader);
       Task UpdateLoanInfo(int? loanApplicationId, string id, IEnumerable<string> authHeader);
       Task<int> GetLoanApplicationId(string loanApplicationId);


    }
}
