using Microsoft.EntityFrameworkCore;
using Milestone.Data;
using Milestone.Data.Mapping;
using Milestone.Entity.Models;
using Milestone.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            //MilestoneSetting milestoneSetting = await Uow.Repository<MilestoneSetting>()
            //   .Query(x => x.TenantId == tenantId).FirstOrDefaultAsync();
            //if (milestoneSetting == null || milestoneSetting.ShowMilestone)
            //{
                if (milestone != null)
                {
                    if (milestone.MilestoneTypeId == (int)MilestoneType.Special)
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
                        return await Query(x => x.MilestoneTypeId == (int)MilestoneType.Timeline &&
                                              (x.TenantMilestones.FirstOrDefault(y => y.TenantId == tenantId) == null
                                               || x.TenantMilestones.FirstOrDefault(y => y.TenantId == tenantId).Visibility)
                                              ).Include(x => x.TenantMilestones).OrderBy(x => x.Order).Select(x => new MilestoneForLoanCenter()
                                              {
                                                  Name = (!x.TenantMilestones.Any(y => y.TenantId == tenantId) ||
                                        string.IsNullOrEmpty(x.TenantMilestones.First(y => y.TenantId == tenantId)
                                            .BorrowerName))
                                    ? x.BorrowerName
                                    : x.TenantMilestones.First(y => y.TenantId == tenantId).BorrowerName,
                                                  Icon = x.Icon,
                                                  MilestoneType = x.MilestoneTypeId,
                                                  IsCurrent = (x.Id == milestone.Id),
                                                  Description = (!x.TenantMilestones.Any(y => y.TenantId == tenantId) ||
                                               string.IsNullOrEmpty(x.TenantMilestones.First(y => y.TenantId == tenantId)
                                                   .Description))
                                    ? x.Description
                                    : x.TenantMilestones.First(y => y.TenantId == tenantId).Description
                                              }).ToListAsync();
                    }
                }
            //}
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
        public async Task InsertMilestoneLog(int loanApplicationId, int milestoneId)
        {
            var milestoneLog = new MilestoneLog()
            {
                MilestoneId = milestoneId,
                CreatedDateUtc = DateTime.UtcNow,
                LoanApplicationId = loanApplicationId,
                TrackingState = TrackingState.Added
            };
            Uow.Repository<MilestoneLog>().Insert(milestoneLog);
            await Uow.SaveChangesAsync();
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
            Entity.Models.Milestone milestone = null;
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
                            || x.TenantMilestones.First(y => y.TenantId == tenantId).Visibility)))
                    {
                        milestone = milestones.First(x =>
                            x.Order > milestone.Order && x.MilestoneTypeId == (int) MilestoneType.Timeline
                                                        && (x.TenantMilestones
                                                                .FirstOrDefault(y => y.TenantId == tenantId) == null
                                                            || x.TenantMilestones
                                                                .First(y => y.TenantId == tenantId)
                                                                .Visibility));
                    }
                    // get previous visible from timeline
                    else if (milestones.OrderByDescending(x=>x.Order).Any(x => x.Order < milestone.Order && x.MilestoneTypeId == (int)MilestoneType.Timeline
                                                                            && (x.TenantMilestones.FirstOrDefault(y => y.TenantId == tenantId) == null
                                                                                || x.TenantMilestones.First(y => y.TenantId == tenantId).Visibility)))
                    {
                        milestone = milestones.OrderByDescending(x => x.Order).First(x =>
                            x.Order < milestone.Order && x.MilestoneTypeId == (int)MilestoneType.Timeline
                                                        && (x.TenantMilestones
                                                                .FirstOrDefault(y => y.TenantId == tenantId) == null
                                                            || x.TenantMilestones
                                                                .First(y => y.TenantId == tenantId)
                                                                .Visibility));
                    }
                    else
                    {
                        milestone = null;
                    }
                }
            }
            //MilestoneSetting milestoneSetting = await Uow.Repository<MilestoneSetting>()
            //    .Query(x => x.TenantId == tenantId).FirstOrDefaultAsync();
            //if (milestoneSetting != null && !milestoneSetting.ShowMilestone && milestone!=null && milestone.Id!=1 && milestone.Id!=2)
            //{
            //    milestone = null;
            //}
            return milestone;
        }

        public async Task<int> GetLosMilestone(int tenantId, string milestone, short losId)
        {
            var m = await Uow.Repository<LosTenantMilestone>().Query(x => x.TenantId == tenantId && x.ExternalOriginatorId == losId && x.Name.ToLower() == milestone.ToLower())
                .Include(x => x.MilestoneMappings).FirstOrDefaultAsync();
            if (m == null)
                return -1;
            var n = m.MilestoneMappings.FirstOrDefault();
            if (n == null)
                return -1;
            return n.MilestoneId;
        }

        //Start
        public async Task<GlobalMilestoneSettingModel> GetGlobalMilestoneSetting(int tenantId)
        {
            var setting = await Uow.Repository<MilestoneSetting>().Query(x => x.TenantId == tenantId).Select(x => new GlobalMilestoneSettingModel() { ShowMilestone = x.ShowMilestone,TenantId=x.TenantId }).FirstOrDefaultAsync();
            if (setting == null)
                setting = new GlobalMilestoneSettingModel() { ShowMilestone = true,TenantId=tenantId };
            return setting;
        }
        public async Task SetGlobalMilestoneSetting(GlobalMilestoneSettingModel model)
        {
            var setting = await Uow.Repository<MilestoneSetting>().Query(x => x.TenantId == model.TenantId).FirstOrDefaultAsync();
            if(setting==null)
            {
                setting = new MilestoneSetting()
                { 
                    ShowMilestone = model.ShowMilestone,
                    TenantId = model.TenantId,
                    TrackingState = TrackingState.Added
                };
                Uow.Repository<MilestoneSetting>().Insert(setting);
            }
            else
            {
                setting.ShowMilestone = model.ShowMilestone;
                setting.TrackingState = TrackingState.Modified;
                Uow.Repository<MilestoneSetting>().Update(setting);
            }
            await Uow.SaveChangesAsync();
        }
        
        public async Task<List<MilestoneSettingModel>> GetMilestoneSettingList(int tenantId)
        {
            return await Query().Include(x => x.TenantMilestones).Select(x=>new MilestoneSettingModel() { 
                TenantId = tenantId,
                Icon = x.Icon,
                Id = x.Id,
                Name = x.McuName,
                Visible = x.TenantMilestones.Any(y=>y.TenantId==tenantId) ? x.TenantMilestones.First(y => y.TenantId == tenantId).Visibility : true,
                McuName = x.TenantMilestones.Any(y => y.TenantId == tenantId) && !string.IsNullOrEmpty(x.TenantMilestones.First(y => y.TenantId == tenantId).McuName) ? x.TenantMilestones.First(y => y.TenantId == tenantId).McuName : x.McuName,
                BorrowerName = x.TenantMilestones.Any(y => y.TenantId == tenantId) && !string.IsNullOrEmpty(x.TenantMilestones.First(y => y.TenantId == tenantId).BorrowerName) ? x.TenantMilestones.First(y => y.TenantId == tenantId).BorrowerName : x.BorrowerName,
                Description = x.TenantMilestones.Any(y => y.TenantId == tenantId) && !string.IsNullOrEmpty(x.TenantMilestones.First(y => y.TenantId == tenantId).Description) ? x.TenantMilestones.First(y => y.TenantId == tenantId).Description : x.Description
            }).ToListAsync();
        }


        public async Task<MilestoneSettingModel> GetMilestoneSetting(int tenantId, int milestoneId)
        {
            return await Query(x=>x.Id==milestoneId).Include(x => x.TenantMilestones).Select(x => new MilestoneSettingModel()
            {
                TenantId = tenantId,
                Icon = x.Icon,
                Id = x.Id,
                Name = x.McuName,
                Visible = x.TenantMilestones.Any(y => y.TenantId == tenantId) ? x.TenantMilestones.First(y => y.TenantId == tenantId).Visibility : true,
                McuName = x.TenantMilestones.Any(y => y.TenantId == tenantId) && !string.IsNullOrEmpty(x.TenantMilestones.First(y => y.TenantId == tenantId).McuName) ? x.TenantMilestones.First(y => y.TenantId == tenantId).McuName : x.McuName,
                BorrowerName = x.TenantMilestones.Any(y => y.TenantId == tenantId) && !string.IsNullOrEmpty(x.TenantMilestones.First(y => y.TenantId == tenantId).BorrowerName) ? x.TenantMilestones.First(y => y.TenantId == tenantId).BorrowerName : x.BorrowerName,
                Description = x.TenantMilestones.Any(y => y.TenantId == tenantId) && !string.IsNullOrEmpty(x.TenantMilestones.First(y => y.TenantId == tenantId).Description) ? x.TenantMilestones.First(y => y.TenantId == tenantId).Description : x.Description
            }).FirstOrDefaultAsync();
        }
        
        public async Task SetMilestoneSetting(MilestoneSettingModel model)
        {
            await Uow.BeginTransactionAsync();
            try
            {
                await Uow.DataContext.Database.ExecuteSqlCommandAsync($"delete from tenantmilestone where tenantId={model.TenantId} and milestoneId={model.Id}");
                TenantMilestone m = new TenantMilestone()
                {
                    BorrowerName = model.BorrowerName,
                    Description = model.Description,
                    McuName = model.McuName,
                    MilestoneId = model.Id,
                    TenantId = model.TenantId,
                    Visibility = model.Visible,
                    TrackingState = TrackingState.Added
                };
                Uow.Repository<TenantMilestone>().Insert(m);
                await Uow.SaveChangesAsync();
                await Uow.CommitAsync();
            }
            catch
            {
                await Uow.RollbackAsync();
                throw;
            }
        }
        public async Task<List<LosModel>> GetLosAll()
        {
            return await Uow.Repository<ExternalOriginator>().Query().Select(x=>new LosModel() { Id=x.Id,Name=x.Name}).ToListAsync();
        }
        public async Task<List<MappingModel>> GetMappingAll(int tenantId, short losId)
        {
            return await Uow.Repository<LosTenantMilestone>().Query(x => x.ExternalOriginatorId == losId && x.TenantId == tenantId).Select(x => new MappingModel() { Id = x.Id, Name = x.Name }).ToListAsync();
        }
        public async Task<MilestoneMappingModel> GetMapping(int tenantId, int milestoneId)
        {
            return new MilestoneMappingModel() { Id=milestoneId,Mapping= await Uow.Repository<MilestoneMapping>().Query(x => x.MilestoneId == milestoneId && x.LosTenantMilestone.TenantId == tenantId).Select(x => x.LosMilestoneId).ToListAsync() };
        }
        public async Task SetMapping(MilestoneMappingModel model)
        {
            await Uow.BeginTransactionAsync();
            try
            {
                await Uow.DataContext.Database.ExecuteSqlCommandAsync($"delete from milestonemapping where milestoneId={model.Id}");
                foreach (var item in model.Mapping)
                {
                    MilestoneMapping m = new MilestoneMapping()
                    {
                        MilestoneId = model.Id,
                        LosMilestoneId = item,
                        TrackingState = TrackingState.Added
                    };
                    Uow.Repository<MilestoneMapping>().Insert(m);
                }
                await Uow.SaveChangesAsync();
                await Uow.CommitAsync();
            }
            catch
            {
                await Uow.RollbackAsync();
                throw;
            }
        }
        public async Task AddMapping(MilestoneAddMappingModel model)
        {
            LosTenantMilestone milestone = new LosTenantMilestone()
            { 
                TenantId = model.TenantId,
                ExternalOriginatorId = model.LosId,
                Name = model.Name,
                TrackingState = TrackingState.Added
            };
            Uow.Repository<LosTenantMilestone>().Insert(milestone);
            await Uow.SaveChangesAsync();
        }
        public async Task DeleteMapping(MilestoneAddMappingModel model)
        {
            await Uow.BeginTransactionAsync();
            try
            {
                await Uow.DataContext.Database.ExecuteSqlCommandAsync($"delete from milestonemapping where losmilestoneId={model.Id}");
                await Uow.DataContext.Database.ExecuteSqlCommandAsync($"delete from lostenantmilestone where Id={model.Id}");
                await Uow.CommitAsync();
            }
            catch
            {
                await Uow.RollbackAsync();
                throw;
            }
        }
        public async Task EditMapping(MilestoneAddMappingModel model)
        {
            var mapping = await Uow.Repository<LosTenantMilestone>().Query(x => x.Id == model.Id).FirstAsync();
            mapping.Name = model.Name;
            mapping.TrackingState = TrackingState.Modified;
            Uow.Repository<LosTenantMilestone>().Update(mapping);
            await Uow.SaveChangesAsync();
        }
    }
}
