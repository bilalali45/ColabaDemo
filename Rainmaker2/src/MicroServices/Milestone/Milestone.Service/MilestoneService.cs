using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Milestone.Data;
using Milestone.Entity.Models;
using Milestone.Model;
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

        public async Task<MilestoneForBorrowerDashboard> GetMilestoneForBorrowerDashboard(int milestoneId, int tenantId)
        {
            MilestoneSetting milestoneSetting = await Uow.Repository<MilestoneSetting>()
                .Query(x => x.TenantId == tenantId).FirstOrDefaultAsync();
            if (milestoneSetting == null || milestoneSetting.ShowMilestone.Value)
            {
                var milestones = await Repository.Query().Include(x => x.TenantMilestones).OrderBy(x => x.Order)
                    .ToListAsync();
                var milestone = milestones.First(x => x.Id == milestoneId);
                if (milestone.MilestoneTypeId == (int) MilestoneType.Special)
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
                else
                {
                    var visible = milestone.TenantMilestones.FirstOrDefault(x => x.TenantId == tenantId)?.Visibility;
                    if (visible != null && visible.Value == false)
                    {
                        // get next visible
                        if(milestones.Any(x=>x.Order>milestone.Order && x.MilestoneTypeId==(int)MilestoneType.Timeline 
                            && (x.TenantMilestones.FirstOrDefault(y=>y.TenantId==tenantId)?.Visibility==null 
                                || x.TenantMilestones.FirstOrDefault(y => y.TenantId == tenantId)?.Visibility.Value == true)))
                        {
                            milestone = milestones.First(x =>
                                x.Order > milestone.Order && x.MilestoneTypeId == (int) MilestoneType.Timeline
                                                          && (x.TenantMilestones
                                                                  .FirstOrDefault(y => y.TenantId == tenantId)
                                                                  ?.Visibility == null
                                                              || x.TenantMilestones
                                                                  .FirstOrDefault(y => y.TenantId == tenantId)
                                                                  ?.Visibility.Value == true));
                        }
                        // get previous visible
                        else if (milestones.OrderByDescending(x=>x.Order).Any(x => x.Order < milestone.Order && x.MilestoneTypeId == (int)MilestoneType.Timeline
                                                                               && (x.TenantMilestones.FirstOrDefault(y => y.TenantId == tenantId)?.Visibility == null
                                                                                   || x.TenantMilestones.FirstOrDefault(y => y.TenantId == tenantId)?.Visibility.Value == true)))
                        {
                            milestone = milestones.OrderByDescending(x => x.Order).First(x =>
                                x.Order < milestone.Order && x.MilestoneTypeId == (int)MilestoneType.Timeline
                                                          && (x.TenantMilestones
                                                                  .FirstOrDefault(y => y.TenantId == tenantId)
                                                                  ?.Visibility == null
                                                              || x.TenantMilestones
                                                                  .FirstOrDefault(y => y.TenantId == tenantId)
                                                                  ?.Visibility.Value == true));
                        }
                        else
                        {
                            milestone = null;
                        }
                    }
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
                }
            }

            return new MilestoneForBorrowerDashboard();
        }
    }
}
