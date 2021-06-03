using Microsoft.Extensions.Caching.Distributed;
using Ocelot.RateLimit;
using System;

namespace MainGateway.Services
{
    public class DistributedCacheRateLimitCounterHandler : RainsoftGateway.Core.Services.DistributedCacheRateLimitCounterHandler
    {
        public DistributedCacheRateLimitCounterHandler(IDistributedCache memoryCache) : base(memoryCache)
        {
        }
    }
}
