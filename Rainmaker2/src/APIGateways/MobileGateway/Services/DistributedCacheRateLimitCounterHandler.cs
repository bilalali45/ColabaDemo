using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Ocelot.RateLimit;

namespace MobileGateway.Services
{
    public class DistributedCacheRateLimitCounterHandler : IRateLimitCounterHandler
    {
        private readonly IDistributedCache _distributedCache;

        public DistributedCacheRateLimitCounterHandler(IDistributedCache memoryCache)
        {
            _distributedCache = memoryCache;
        }

        public void Set(string id, RateLimitCounter counter, TimeSpan expirationTime)
        {
            _distributedCache.Set(id, System.Text.Encoding.Unicode.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(counter)), new DistributedCacheEntryOptions().SetAbsoluteExpiration(expirationTime));
        }

        public bool Exists(string id)
        {
            return _distributedCache.Get(id) != null;
        }

        public RateLimitCounter? Get(string id)
        {
            byte[] bytes = _distributedCache.Get(id);
            if (bytes == null)
                return null;
            return Newtonsoft.Json.JsonConvert.DeserializeObject<RateLimitCounter>(System.Text.Encoding.Unicode.GetString(bytes));
        }

        public void Remove(string id)
        {
            _distributedCache.Remove(id);
        }
    }
}
