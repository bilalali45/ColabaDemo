using RainMaker.Data;
using RainMaker.Entity.Models;
using RainMaker.Service;
using System;
using URF.Core.Abstractions;

namespace Rainmaker.Service
{
    public class OpportunityService : ServiceBase<RainMakerContext,Opportunity>, IOpportunityService
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
    }
}
