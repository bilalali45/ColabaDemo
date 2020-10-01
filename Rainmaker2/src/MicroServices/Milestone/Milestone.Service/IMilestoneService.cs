using Milestone.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Milestone.Service
{
    public interface IMilestoneService : IServiceBase<Entity.Models.Milestone>
    {
        Task<List<MilestoneModel>> GetAllMilestones(int tenantId);
        Task<MilestoneForBorrowerDashboard> GetMilestoneForBorrowerDashboard(int milestoneId, int tenantId);
        Task<string> GetMilestoneForMcuDashboard(int milestone, int tenantId);
    }
}
