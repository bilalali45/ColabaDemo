using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Service
{
   public interface IRainmakerService
   {
       Task<string> PostLoanApplication(int loanApplicationId, bool isDraft, IEnumerable<string> authHeader);
   }
}
