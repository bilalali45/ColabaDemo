using Milestone.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Milestone.Service
{
    public interface IMilestoneService : IServiceBase<Entity.Models.Milestone>
    {
        Task<List<MilestoneModel>> GetAllMilestones(int tenantId);
        Task<MilestoneForBorrowerDashboard> GetMilestoneForBorrowerDashboard(int loanApplicationId,int milestoneId, int tenantId);
        Task<string> GetMilestoneForMcuDashboard(int milestone, int tenantId);
        Task UpdateMilestoneLog(int loanApplicationId, int milestoneId);
        Task<List<MilestoneForLoanCenter>> GetMilestoneForLoanCenter(int loanApplicationId, int milestoneId, int tenantId);
        Task<int> GetLosMilestone(int tenantId, string milestone, short losId);
        Task<GlobalMilestoneSettingModel> GetGlobalMilestoneSetting(int tenantId);
        Task SetGlobalMilestoneSetting(GlobalMilestoneSettingModel model);
        Task<List<MilestoneSettingModel>> GetMilestoneSettingList(int tenantId);
        Task<MilestoneSettingModel> GetMilestoneSetting(int tenantId, int milestoneId);
        Task SetMilestoneSetting(MilestoneSettingModel model);
        Task<List<LosModel>> GetLosAll();
        Task<List<MappingModel>> GetMappingAll(int tenantId, short losId);
        Task<MilestoneMappingModel> GetMapping(int tenantId, int milestoneId);
        Task SetMapping(MilestoneMappingModel model);
        Task AddMapping(MilestoneAddMappingModel model);
        Task DeleteMapping(MilestoneAddMappingModel model);
        Task EditMapping(MilestoneAddMappingModel model);
        Task InsertMilestoneLog(int loanApplicationId, int milestoneId);
    }
}
