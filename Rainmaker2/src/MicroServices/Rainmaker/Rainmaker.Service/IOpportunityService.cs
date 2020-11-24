using System.Threading.Tasks;
using RainMaker.Entity.Models;
using RainMaker.Service;

namespace Rainmaker.Service
{
    public interface IOpportunityService : IServiceBase<Opportunity>
    {
        void InsertOpportunityStatusLog(OpportunityStatusLog opportunityStatusLog);
        void InsertOpportunityLockStatusLog(OpportunityLockStatusLog opportunityLockStatusLog);
        Task<Opportunity> GetSingleOpportunity(int? opportunityId);
    }
}
