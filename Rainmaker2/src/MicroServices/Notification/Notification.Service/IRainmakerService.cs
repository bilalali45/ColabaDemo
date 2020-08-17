using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Service
{
    public interface IRainmakerService
    {
        Task<List<int>> GetAssignedUsers(int loanApplicationId, IEnumerable<string> authHeader);
    }
}
