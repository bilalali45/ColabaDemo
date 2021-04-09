using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Identity.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace Identity.CacheHandler
{
    public interface ICacheHandler
    {

        void SetCacheItemAsync<T>(string key,
                                  T itemData);
        Task<T> GetCacheItemAsync<T>(string key);
        Task<bool> RemoveCacheItemAsync(string key);
    }
    

    public class RedisCachehandler : ICacheHandler
    {
        private readonly ILogger<RedisCachehandler> _logger;
        private readonly IDistributedCache _distributedCache;

        public RedisCachehandler(ILogger<RedisCachehandler> logger, IDistributedCache distributedCache)
        {
            this._logger = logger;
            this._distributedCache = distributedCache;
        }

        

        public async void SetCacheItemAsync<T>(string key, T itemData)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                var binFormatter = new BinaryFormatter();
                binFormatter.Serialize(ms, itemData);
                await this._distributedCache.SetAsync(key,
                                                      ms.ToArray());
            }
        }

        public async Task<T> GetCacheItemAsync<T>(string key)
        {
            var itemData = await this._distributedCache.GetAsync(key);
            if (itemData != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    var binFormatter = new BinaryFormatter();
                    ms.Write(itemData, 0, itemData.Length);
                    ms.Position = 0;
                    return (T)Convert.ChangeType(binFormatter.Deserialize(ms),
                                                 typeof(T));
                }
            }
            return default(T);
        }

        public async Task<bool> RemoveCacheItemAsync(string key)
        {
            bool success = false;
            var itemData = await this._distributedCache.GetAsync(key);
            if (itemData != null)
            {
                await this._distributedCache.RemoveAsync(key);
                success = true;
            }
            return success;
        }
    }
}
