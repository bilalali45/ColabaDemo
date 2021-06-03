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
    public static class Settings
    {
        public const string EmailHost = "EmailHost";
        public const string EmailPort = "EmailPort";
        public const string EmailEnableSsl = "EmailEnableSsl";
        public const string EmailUser = "EmailUser";
        public const string EmailPassword = "EmailPassword";
    }
    public interface ISettingService
    {
        Task<T> GetSetting<T>(string name, int? tenantId = null, int? branch = null, T defaultValue = default, bool readFresh = false);
    }
    public class SettingService : ISettingService
    {
        public static string SettingPrefix = "SETTING#";
        public static string TenantPrefix = "TENANT#";
        public static string BranchPrefix = "BRANCH#";
        private readonly IRedisCacheClient _client;
        private readonly IUnitOfWork<TenantConfigContext> _uow;
        public SettingService(IRedisCacheClient client, IUnitOfWork<TenantConfigContext> uow)
        {
            _client = client;
            _uow = uow;
        }
        public async Task<T> GetSetting<T>(string name, int? tenantId = null, int? branchId = null, T defaultValue = default, bool readFresh = false)
        {
            string value;
            if (branchId!=null)
            {
                value = await GetBranchSetting(name, branchId.Value, readFresh);
                if(value!=null)
                {
                    return CommonHelper.To<T>(value);
                }
            }
            if (tenantId != null)
            {
                value = await GetTenantSetting(name, tenantId.Value, readFresh);
                if (value != null)
                {
                    return CommonHelper.To<T>(value);
                }
            }
            value = await GetGlobalSetting(name, readFresh);
            if (value != null)
            {
                return CommonHelper.To<T>(value);
            }
            return defaultValue;
        }

        private async Task<string> GetGlobalSetting(string name, bool readFresh)
        {
            string value = null;
            if(!readFresh)
            {
                value = await _client.Db0.HashGetAsync<string>(SettingPrefix,name);
            }
            if(value==null)
            {
                value = await _uow.Repository<TenantConfig.Entity.Models.Setting>().Query(x => x.IsActive == true && x.Name == name && x.TenantId == null && x.BranchId == null).Select(x=>x.Value).FirstOrDefaultAsync();
            }
            return value;
        }

        private async Task<string> GetTenantSetting(string name, int tenantId, bool readFresh)
        {
            string value = null;
            if (!readFresh)
            {
                value = await _client.Db0.HashGetAsync<string>(SettingPrefix+TenantPrefix+tenantId, name);
            }
            if (value == null)
            {
                value = await _uow.Repository<TenantConfig.Entity.Models.Setting>().Query(x => x.IsActive == true && x.Name == name && x.TenantId == tenantId && x.BranchId == null).Select(x => x.Value).FirstOrDefaultAsync();
            }
            return value;
        }

        private async Task<string> GetBranchSetting(string name,int branchId, bool readFresh)
        {
            string value = null;
            if (!readFresh)
            {
                value = await _client.Db0.HashGetAsync<string>(SettingPrefix+BranchPrefix+branchId, name);
            }
            if (value == null)
            {
                value = await _uow.Repository<TenantConfig.Entity.Models.Setting>().Query(x => x.IsActive == true && x.Name == name && x.BranchId == branchId).Select(x => x.Value).FirstOrDefaultAsync();
            }
            return value;
        }
    }
}
