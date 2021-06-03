using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Identity.Data;
using Identity.Entity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using StackExchange.Redis;
using TenantConfig.Common.DistributedCache;
using TenantConfig.Data;
using TenantConfig.Entity.Models;
using URF.Core.Abstractions;

namespace MobileGateway.Services
{
    public class MobileTenantResolver : RainsoftGateway.Core.Services.MobileTenantResolver
    {
        public MobileTenantResolver(IUnitOfWork<TenantConfigContext> uow, IConnectionMultiplexer connection, IHttpClientFactory clientFactory, IConfiguration configuration, ILogger<MobileTenantResolver> logger)
            : base(uow, connection, clientFactory, configuration, logger)
        {
        }
    }
}