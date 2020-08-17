using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RainMaker.Common;
using RainMaker.Data;
using RainMaker.Entity.Models;
using RainMaker.Service;
using URF.Core.Abstractions;

namespace Rainmaker.Service
{
    public class NotificationService : ServiceBase<RainMakerContext,LoanApplication>, INotificationService
    {
        public NotificationService(IUnitOfWork<RainMakerContext> previousUow,
            IServiceProvider services) : base(previousUow: previousUow,
            services: services)
        {
        }
        public async Task<List<int>> GetAssignedUsers(int loanApplicationId, int userId)
        {
            List<int> list = new List<int>();
            LoanApplication application = await Repository.Query(x =>
                    x.Id == loanApplicationId && x.Opportunity.OpportunityLeadBinders
                        .Where(y => y.OwnTypeId == (int) OwnTypeEnum.PrimaryContact).FirstOrDefault().Customer
                        .UserId == userId).Include(x => x.Opportunity).ThenInclude(x => x.OpportunityLeadBinders)
                .ThenInclude(x => x.Customer)
                .Include(x=>x.Opportunity).ThenInclude(x=>x.Employee).FirstAsync();
            if(application.Opportunity.Employee!=null)
                list.Add(application.Opportunity.Employee.UserId.Value);
            return list;
        }
    }
}
