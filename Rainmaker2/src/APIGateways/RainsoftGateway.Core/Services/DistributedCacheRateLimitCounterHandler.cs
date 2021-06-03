using System;
using Microsoft.Extensions.Caching.Distributed;
using Ocelot.RateLimit;

namespace RainsoftGateway.Core.Services
{
    public class DistributedCacheRateLimitCounterHandler : IRateLimitCounterHandler
    {
        private readonly IDistributedCache _distributedCache;

        public DistributedCacheRateLimitCounterHandler(IDistributedCache memoryCache)
        {
            _distributedCache = memoryCache;
        }

        public virtual void Set(string id, RateLimitCounter counter, TimeSpan expirationTime)
        {
            _distributedCache.Set(id, System.Text.Encoding.Unicode.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(counter)), new DistributedCacheEntryOptions().SetAbsoluteExpiration(expirationTime));
        }

        public virtual bool Exists(string id)
        {
            return _distributedCache.Get(id) != null;
        }

        public virtual RateLimitCounter? Get(string id)
        {
            byte[] bytes = _distributedCache.Get(id);
            if (bytes == null)
                return null;
            return Newtonsoft.Json.JsonConvert.DeserializeObject<RateLimitCounter>(System.Text.Encoding.Unicode.GetString(bytes));
        }

        public virtual void Remove(string id)
        {
            _distributedCache.Remove(id);
        }
    }
}
