using System;
using System.Collections.Generic;
using System.Text;
using RainMaker.Data;
using RainMaker.Entity.Models;
using RainMaker.Service;

namespace Rainmaker.Service
{
    public interface IOpportunityService : IServiceBase<Opportunity>
    {
        void InsertOpportunityStatusLog(OpportunityStatusLog opportunityStatusLog);
        void InsertOpportunityLockStatusLog(OpportunityLockStatusLog opportunityLockStatusLog);
    }
}
