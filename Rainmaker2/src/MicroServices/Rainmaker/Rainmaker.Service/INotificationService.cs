using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RainMaker.Entity.Models;
using RainMaker.Service;

namespace Rainmaker.Service
{
    public interface INotificationService : IServiceBase<LoanApplication>
    {
        Task<List<int>> GetAssignedUsers(int loanApplicationId, int userId);
    }
}
