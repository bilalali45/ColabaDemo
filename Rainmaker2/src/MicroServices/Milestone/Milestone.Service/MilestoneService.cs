using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Milestone.Data;
using Milestone.Entity.Models;
using Milestone.Model;
using TrackableEntities.Common.Core;
using URF.Core.Abstractions;
using MilestoneType = Milestone.Model.MilestoneType;

namespace Milestone.Service
{
    public class MilestoneService : ServiceBase<MilestoneContext,Entity.Models.Milestone>,IMilestoneService
    {
        public MilestoneService(IUnitOfWork<MilestoneContext> previousUow,
            IServiceProvider services) : base(previousUow: previousUow,
            services: services)
        {
        }

        public async Task<List<MilestoneForLoanCenter>> GetMilestoneForLoanCenter(int loanApplicationId, int milestoneId,
            int tenantId)
        {
            var milestone = await GetEligibleMilestone(loanApplicationId, milestoneId, tenantId);
            if (milestone != null)
            {
                if (milestone.MilestoneTypeId == (int) MilestoneType.Special)
                {
                    return new List<MilestoneForLoanCenter>()
                    {
                        new MilestoneForLoanCenter()
                        {
                            Name = (!milestone.TenantMilestones.Any(x => x.TenantId == tenantId) ||
                                    string.IsNullOrEmpty(milestone.TenantMilestones.First(x => x.TenantId == tenantId)
                                        .BorrowerName))
                                ? milestone.BorrowerName
                                : milestone.TenantMilestones.First(x => x.TenantId == tenantId).BorrowerName,
                            Icon = milestone.Icon,
                            MilestoneType = milestone.MilestoneTypeId,
                            IsCurrent = true,
                            Description =  (!milestone.TenantMilestones.Any(x => x.TenantId == tenantId) ||
                                            string.IsNullOrEmpty(milestone.TenantMilestones.First(x => x.TenantId == tenantId)
                                                .Description))
                                ? milestone.Description
                                : milestone.TenantMilestones.First(x => x.TenantId == tenantId).Description
                        }
                    };
                }
                else
                {
                    return await Query(x=>x.MilestoneTypeId==(int)MilestoneType.Timeline &&
                                          (x.TenantMilestones.FirstOrDefault(y => y.TenantId == tenantId) == null 
                                           || x.TenantMilestones.FirstOrDefault(y => y.TenantId == tenantId).Visibility)
                                          ).Include(x=>x.TenantMilestones).OrderBy(x=>x.Order).Select(x=>new MilestoneForLoanCenter()
                    {
                        Name = (!x.TenantMilestones.Any(y => y.TenantId == tenantId) ||
                                string.IsNullOrEmpty(x.TenantMilestones.First(y => y.TenantId == tenantId)
                                    .BorrowerName))
                            ? x.BorrowerName
                            : x.TenantMilestones.First(y => y.TenantId == tenantId).BorrowerName,
                        Icon = x.Icon,
                        MilestoneType = x.MilestoneTypeId,
                        IsCurrent = (x.Id==milestone.Id),
                        Description = (!x.TenantMilestones.Any(y => y.TenantId == tenantId) ||
                                       string.IsNullOrEmpty(x.TenantMilestones.First(y => y.TenantId == tenantId)
                                           .Description))
                            ? x.Description
                            : x.TenantMilestones.First(y => y.TenantId == tenantId).Description
                    }).ToListAsync();
                }
            }
            return null;
        }
        public async Task UpdateMilestoneLog(int loanApplicationId, int milestoneId)
        {
            MilestoneLog milestoneLog = await Uow.Repository<MilestoneLog>()
                .Query(x => x.LoanApplicationId == loanApplicationId)
                .OrderByDescending(x => x.CreatedDateUtc).FirstOrDefaultAsync();
            if (milestoneLog == null || milestoneLog.MilestoneId != milestoneId)
            {
                milestoneLog = new MilestoneLog()
                {
                    MilestoneId = milestoneId,
                    CreatedDateUtc = DateTime.UtcNow,
                    LoanApplicationId = loanApplicationId,
                    TrackingState = TrackingState.Added
                };
                Uow.Repository<MilestoneLog>().Insert(milestoneLog);
                await Uow.SaveChangesAsync();
            }
        }
        public async Task<string> GetMilestoneForMcuDashboard(int milestone, int tenantId)
        {
            return await Repository.Query(x=>x.Id==milestone).Include(x => x.TenantMilestones)
                .Select(x => 
                    (!x.TenantMilestones.Any(y => y.TenantId == tenantId) 
                     || string.IsNullOrEmpty(x.TenantMilestones.First(y => y.TenantId == tenantId).McuName)) ? 
                        x.McuName : x.TenantMilestones.First(y => y.TenantId == tenantId).McuName
                ).FirstOrDefaultAsync();
        }
        public async Task<List<MilestoneModel>> GetAllMilestones(int tenantId)
        {
            return await Repository.Query().Include(x => x.TenantMilestones).OrderBy(x=>x.Order)
                .Select(x => new MilestoneModel()
                {
                    Id = x.Id,
                    Name = (!x.TenantMilestones.Any(y=>y.TenantId==tenantId) || string.IsNullOrEmpty(x.TenantMilestones.First(y=>y.TenantId==tenantId).McuName)) ? x.McuName : x.TenantMilestones.First(y=>y.TenantId==tenantId).McuName
                }).ToListAsync();
        }
        public async Task<MilestoneForBorrowerDashboard> GetMilestoneForBorrowerDashboard(int loanApplicationId,
            int milestoneId, int tenantId)
        {
            var milestone = await GetEligibleMilestone(loanApplicationId, milestoneId, tenantId);
            if (milestone != null)
            {
                return new MilestoneForBorrowerDashboard()
                {
                    Name = (!milestone.TenantMilestones.Any(x => x.TenantId == tenantId) ||
                            string.IsNullOrEmpty(milestone.TenantMilestones.First(x => x.TenantId == tenantId)
                                .BorrowerName))
                        ? milestone.BorrowerName
                        : milestone.TenantMilestones.First(x => x.TenantId == tenantId).BorrowerName,
                    Icon = milestone.Icon
                };
            }
            return null;
        }
        private async Task<Entity.Models.Milestone> GetEligibleMilestone(int loanApplicationId,
            int milestoneId, int tenantId)
        {

            MilestoneSetting milestoneSetting = await Uow.Repository<MilestoneSetting>()
                .Query(x => x.TenantId == tenantId).FirstOrDefaultAsync();
            Entity.Models.Milestone milestone = null;
            if (milestoneSetting == null || milestoneSetting.ShowMilestone)
            {
                var milestones = await Repository.Query().Include(x => x.TenantMilestones).OrderBy(x => x.Order)
                    .ToListAsync();
                milestone = milestones.First(x => x.Id == milestoneId);
                if (milestone.MilestoneTypeId == (int) MilestoneType.Special)
                {
                    //check current visible
                    var visible = milestone.TenantMilestones.FirstOrDefault(x => x.TenantId == tenantId)?.Visibility;
                    if (visible != null && !visible.Value)
                    {
                        milestone = null;
                        // find visible from log
                        List<MilestoneLog> list = await Uow.Repository<MilestoneLog>()
                            .Query(x => x.LoanApplicationId == loanApplicationId)
                            .Include(x => x.Milestone).ThenInclude(x => x.TenantMilestones)
                            .OrderByDescending(x => x.CreatedDateUtc).ToListAsync();
                        foreach (var item in list)
                        {
                            visible = item.Milestone.TenantMilestones.FirstOrDefault(x => x.TenantId == tenantId)
                                ?.Visibility;
                            if (visible == null || visible.Value)
                            {
                                milestone = item.Milestone;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    var visible = milestone.TenantMilestones.FirstOrDefault(x => x.TenantId == tenantId)?.Visibility;
                    if (visible != null && !visible.Value)
                    {
                        // get next visible from timeline
                        if(milestones.Any(x=>x.Order>milestone.Order && x.MilestoneTypeId==(int)MilestoneType.Timeline 
                            && (x.TenantMilestones.FirstOrDefault(y=>y.TenantId==tenantId)==null 
                                || x.TenantMilestones.FirstOrDefault(y => y.TenantId == tenantId).Visibility)))
                        {
                            milestone = milestones.First(x =>
                                x.Order > milestone.Order && x.MilestoneTypeId == (int) MilestoneType.Timeline
                                                          && (x.TenantMilestones
                                                                  .FirstOrDefault(y => y.TenantId == tenantId) == null
                                                              || x.TenantMilestones
                                                                  .FirstOrDefault(y => y.TenantId == tenantId)
                                                                  .Visibility));
                        }
                        // get previous visible from timeline
                        else if (milestones.OrderByDescending(x=>x.Order).Any(x => x.Order < milestone.Order && x.MilestoneTypeId == (int)MilestoneType.Timeline
                                                                               && (x.TenantMilestones.FirstOrDefault(y => y.TenantId == tenantId) == null
                                                                                   || x.TenantMilestones.FirstOrDefault(y => y.TenantId == tenantId).Visibility)))
                        {
                            milestone = milestones.OrderByDescending(x => x.Order).First(x =>
                                x.Order < milestone.Order && x.MilestoneTypeId == (int)MilestoneType.Timeline
                                                          && (x.TenantMilestones
                                                                  .FirstOrDefault(y => y.TenantId == tenantId) == null
                                                              || x.TenantMilestones
                                                                  .FirstOrDefault(y => y.TenantId == tenantId)
                                                                  .Visibility));
                        }
                        else
                        {
                            milestone = null;
                        }
                    }
                }
            }
            return milestone;
        }
    }
}
