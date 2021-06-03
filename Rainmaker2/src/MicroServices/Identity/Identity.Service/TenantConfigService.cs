using Identity.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TenantConfig.Common;
using TenantConfig.Data;
using TenantConfig.Entity.Models;
using URF.Core.Abstractions;

namespace Identity.Service
{
    public interface ITenantConfigService
    {
        Task<TwoFaConfig> GetTenant2FaConfigAsync(int tenantId, List<TwoFaConfigEntities> include = null);

        Task<Tenant> GetTenantById(int? tenantId);
        //Task<Customer> GetCustomerByUserIdAsync(int userId, CustomerEntities include);

        Task<bool> TenantRequiresCustomerTwoFa(int tenantId);
    }

    public class TenantConfigService :  ServiceBase<TenantConfigContext, Tenant>, ITenantConfigService
    {
        public TenantConfigService(IUnitOfWork<TenantConfigContext> previousUow, IServiceProvider services) : base(previousUow, services)
        {
        }

        public async Task<TwoFaConfig> GetTenant2FaConfigAsync(int tenantId, List<TwoFaConfigEntities> includes = null)
        {
            var query = Uow.Repository<TwoFaConfig>()
                .Query(x => x.TenantId == tenantId && x.IsActive);

            if (includes != null)
            {
                query = base.ProcessIncludes<TwoFaConfig, TwoFaConfigEntities>(query, includes);
            }
            var results = await query.FirstOrDefaultAsync();
            return results;
        }

        public async Task<Tenant> GetTenantById(int? tenantId)
        {
            var query = Uow.Repository<Tenant>()
                .Query(x => x.Id == tenantId && (x.IsActive == true));
            var results = await query.FirstOrDefaultAsync();
            return results;
        }

        public async Task<bool> TenantRequiresCustomerTwoFa(int tenantId)
        {
            var tenantConfig = await this.GetTenant2FaConfigAsync(tenantId, null);
            return tenantConfig != null && tenantConfig.BorrowerTwoFaModeId == 1 && (!string.IsNullOrEmpty(tenantConfig.TwilioVerifyServiceId));
        }
    }
}
