using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Service
{
    public interface INotificationService
    {
        Task<string> DocumentsSubmitted(int loanApplicationId, IEnumerable<string> authHeader);
    }
}
