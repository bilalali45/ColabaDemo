using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenantConfig.Common.DistributedCache;
using TenantConfig.Data;
using TenantConfig.Entity.Models;
using URF.Core.Abstractions;

namespace MainGateway.Services
{
    public class TenantResolver : RainsoftGateway.Core.Services.TenantResolver
    {
        public TenantResolver(IUnitOfWork<TenantConfigContext> uow, IConnectionMultiplexer connection) : base(uow, connection)
        {
        }
    }
}
