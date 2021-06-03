using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TenantConfig.Common;
using TenantConfig.Common.DistributedCache;
using TenantConfig.Data;
using TenantConfig.Entity.Models;
using URF.Core.Abstractions;

namespace TenantConfig.Service
{
    public class TenantConfigService : ITenantConfigService
    {
        private readonly IServiceProvider _services;
        public TenantConfigService(IServiceProvider services)
        {
            _services = services;
        }
        //public async Task Run()
        //{
        //    while (true)
        //    {
        //        await UpdateTenantBranchCache();
        //        await UpdateSettingCache();
        //        await UpdateStringResourceCache();
        //        Thread.Sleep(5 * 60 * 1000);
        //    }
        //}
    }
}
