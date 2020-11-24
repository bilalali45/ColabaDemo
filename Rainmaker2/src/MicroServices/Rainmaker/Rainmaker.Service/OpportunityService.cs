using RainMaker.Data;
using RainMaker.Entity.Models;
using RainMaker.Service;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using URF.Core.Abstractions;

namespace Rainmaker.Service
{
    public class OpportunityService : ServiceBase<RainMakerContext, Opportunity>, IOpportunityService
    {
        public OpportunityService(IUnitOfWork<RainMakerContext> previousUow, IServiceProvider services) : base(previousUow, services)
        {
        }
        public void InsertOpportunityLockStatusLog(OpportunityLockStatusLog opportunityLockStatusLog)
        {
            Uow.Repository<OpportunityLockStatusLog>().Insert(opportunityLockStatusLog);
        }
        public void InsertOpportunityStatusLog(OpportunityStatusLog opportunityStatusLog)
        {
            Uow.Repository<OpportunityStatusLog>().Insert(opportunityStatusLog);
        }
        public async Task<Opportunity> GetSingleOpportunity(int? opportunityId)
        {
            var query = Uow.Repository<Opportunity>()
                           .Query(x => x.IsDeleted != true && x.Id == opportunityId)
                           .Include(x => x.Owner)
                           .Include(x => x.Owner.EmployeeBusinessUnitEmails)
                           .ThenInclude(x => x.EmailAccount)
                           .Include(x => x.Owner.Contact)
                           .Include(x => x.Owner.Contact.ContactEmailInfoes)
                           .Include(x => x.OpportunityLeadBinders)
                           .ThenInclude(x => x.Customer)
                           .ThenInclude(x => x.Contact)
                           .ThenInclude(x => x.ContactEmailInfoes);

            return await query.FirstOrDefaultAsync();
        }
    }
}
