using Microsoft.EntityFrameworkCore;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantConfig.Data;
using URF.Core.Abstractions;

namespace TenantConfig.Common
{
    public static class StringResources
    {
        public const string ColabaFooter = "ColabaFooter";
    }
    public interface IStringResourceService
    {
        Task<string> GetString(string name, int? tenantId = null, int? branch = null, string defaultValue = default, bool readFresh = false);
    }
    public class StringResourceService : IStringResourceService
    {
        public static string StringResourcePrefix = "STRINGRESOURCE#";
        public static string TenantPrefix = "TENANT#";
        public static string BranchPrefix = "BRANCH#";
        private readonly IRedisCacheClient _client;
        private readonly IUnitOfWork<TenantConfigContext> _uow;
        public StringResourceService(IRedisCacheClient client, IUnitOfWork<TenantConfigContext> uow)
        {
            _client = client;
            _uow = uow;
        }
        public async Task<string> GetString(string name, int? tenantId = null, int? branchId = null, string defaultValue = default, bool readFresh = false)
        {
            string value;
            if (branchId!=null)
            {
                value = await GetBranchStringResource(name, branchId.Value, readFresh);
                if(value!=null)
                {
                    return value;
                }
            }
            if (tenantId != null)
            {
                value = await GetTenantStringResource(name, tenantId.Value, readFresh);
                if (value != null)
                {
                    return value;
                }
            }
            value = await GetGlobalStringResource(name, readFresh);
            if (value != null)
            {
                return value;
            }
            return defaultValue;
        }

        private async Task<string> GetGlobalStringResource(string name, bool readFresh)
        {
            string value = null;
            if(!readFresh)
            {
                value = await _client.Db0.HashGetAsync<string>(StringResourcePrefix, name);
            }
            if(value==null)
            {
                value = await _uow.Repository<TenantConfig.Entity.Models.StringResource>().Query(x => x.IsActive == true && x.Name == name && x.TenantId == null && x.BranchId == null).Select(x=>x.Value).FirstOrDefaultAsync();
            }
            return value;
        }

        private async Task<string> GetTenantStringResource(string name, int tenantId, bool readFresh)
        {
            string value = null;
            if (!readFresh)
            {
                value = await _client.Db0.HashGetAsync<string>(StringResourcePrefix + TenantPrefix+tenantId, name);
            }
            if (value == null)
            {
                value = await _uow.Repository<TenantConfig.Entity.Models.StringResource>().Query(x => x.IsActive == true && x.Name == name && x.TenantId == tenantId && x.BranchId == null).Select(x => x.Value).FirstOrDefaultAsync();
            }
            return value;
        }

        private async Task<string> GetBranchStringResource(string name,int branchId, bool readFresh)
        {
            string value = null;
            if (!readFresh)
            {
                value = await _client.Db0.HashGetAsync<string>(StringResourcePrefix + BranchPrefix+branchId, name);
            }
            if (value == null)
            {
                value = await _uow.Repository<TenantConfig.Entity.Models.StringResource>().Query(x => x.IsActive == true && x.Name == name && x.BranchId == branchId).Select(x => x.Value).FirstOrDefaultAsync();
            }
            return value;
        }
    }
}
