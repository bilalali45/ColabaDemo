﻿using Milestone.Model;
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
        Task<List<MilestoneSettingModel>> GetMilestoneSetting(int tenantId);
        Task SetMilestoneSetting(int tenantId, List<MilestoneSettingModel> model);
        Task<List<MilestoneMappingModel>> GetMilestoneMapping(int tenantId, short losId);
        Task SetMilestoneMapping(int tenantId, List<MilestoneMappingModel> model);
    }
}
