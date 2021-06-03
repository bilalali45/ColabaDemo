using LoanApplication.Model;
using LoanApplicationDb.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantConfig.Common;
using TenantConfig.Common.DistributedCache;
using TenantConfig.Data;
using TenantConfig.Entity.Models;
using URF.Core.Abstractions;

namespace LoanApplication.Service
{
    public class LoanGoalService : ServiceBase<LoanApplicationDb.Data.LoanApplicationContext, LoanApplicationDb.Entity.Models.LoanGoal>, ILoanGoalService
    {
        private readonly IUnitOfWork<TenantConfigContext> _tenantConfigUow;
        public LoanGoalService(IUnitOfWork<LoanApplicationContext> previousUow, IServiceProvider services, IUnitOfWork<TenantConfigContext> tenantConfigUow) : base(previousUow, services)
        {
            _tenantConfigUow = tenantConfigUow;
        }
        public async Task<int> AddOrUpdate(TenantModel tenant, int userId, AddOrUpdateLoanGoalModel model)
        {
            LoanApplicationDb.Entity.Models.LoanApplication application;
            if (model.LoanApplicationId.HasValue)
            {
                application = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(x => x.Id == model.LoanApplicationId.Value && x.IsActive == true && x.UserId == userId && x.TenantId == tenant.Id).SingleAsync();
                Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(application);
            }
            else
            {
                List<TenantSetting> settings = await Uow.Repository<LoanApplicationDb.Entity.Models.Config>().Query()
                    .Include(x => x.ConfigSelections).OrderBy(x => x.Name).Select(x => new TenantSetting
                    {
                        Name = x.Name,
                        Value = x.ConfigSelections.Any(y => y.TenantId == tenant.Id) ? (int)x.ConfigSelections.First(y => y.TenantId == tenant.Id).SelectionId : 1
                    }).ToListAsync();
                var customer = await _tenantConfigUow.Repository<Customer>().Query(x => x.TenantId == tenant.Id && x.UserId == userId).FirstAsync();
                string settingHash = Newtonsoft.Json.JsonConvert.SerializeObject(settings).ToSha(1);
                application = new LoanApplicationDb.Entity.Models.LoanApplication
                {
                    IsActive = true,
                    BranchId = tenant.Branches[0].Id,
                    TenantId = tenant.Id,
                    CreatedOnUtc = DateTime.UtcNow,
                    CreatedBy = userId,
                    IsDeleted = false,
                    MilestoneId = 1,
                    UserId = userId,
                    CustomerId = customer.Id,
                    SettingHash = settingHash,
                    LoanOfficerId = tenant.Branches[0].LoanOfficers.Count == 1 ? (int?)tenant.Branches[0].LoanOfficers[0].Id : null,
                };
                Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Insert(application);
            }
            application.LoanGoalId = model.LoanGoal;
            application.LoanPurposeId = model.LoanPurpose;
            application.State = model.State;
            await Uow.SaveChangesAsync();
            return application.Id;
        }

        public async Task<GetLoanGoalModel> GetLoanGoal(TenantModel tenant, int loanApplicationId, int userId)
        {
            return await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(x => x.Id == loanApplicationId && x.IsActive == true && x.UserId == userId && x.TenantId == tenant.Id).Select(x => new GetLoanGoalModel { LoanGoal = x.LoanGoalId.Value, LoanPurpose = x.LoanPurposeId }).SingleAsync();
        }
    }
}
