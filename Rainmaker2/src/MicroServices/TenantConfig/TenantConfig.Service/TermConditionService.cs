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
using TenantConfig.Model;
using URF.Core.Abstractions;
using BranchModel = TenantConfig.Model.BranchModel;

namespace TenantConfig.Service
{
    public class TermConditionService : ServiceBase<TenantConfigContext, TermsCondition>, ITermConditionService
    {
        public TermConditionService(IUnitOfWork<TenantConfigContext> previousUow, IServiceProvider services) : base(previousUow, services)
        {
        }

        public async Task<string> GetTermsConditions(int type, int tenantId, int branchId)
        {
            return await Uow.Repository<TermsCondition>().Query(x => x.TenantId == tenantId && x.BranchId == branchId && x.IsActive == true && x.TermTypeId == type).Select(x => x.TermsContent).FirstOrDefaultAsync();
        }
        public async Task<BranchModel> GetSetting(TenantModel tenant, int branchId,string branchCode)
        {
            return await Uow.Repository<Branch>().Query(x => x.Id == branchId && x.IsActive == true).Select(x => new
              BranchModel
            {
                Color = x.PrimaryColor,
                Footer = x.Footer,
                Logo = CommonHelper.GenerateCdnUrl(tenant, x.Logo),
                Favicon = CommonHelper.GenerateCdnUrl(tenant, x.FavIcon),
                CookiePath = $"/{branchCode}/",
                DbaName = x.DbaName
            }).SingleAsync();
        }
        public async Task PutSetting(TenantModel tenant, SettingModel model)
        {
            var branch = await Uow.Repository<Branch>().Query(x => x.Id == tenant.Branches[0].Id && x.IsActive == true).SingleAsync();
            branch.PrimaryColor = model.Color;
            Uow.Repository<Branch>().Update(branch);
            await Uow.SaveChangesAsync();
        }
    }
}
