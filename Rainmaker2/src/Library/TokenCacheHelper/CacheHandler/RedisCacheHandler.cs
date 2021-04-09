using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace TokenCacheHelper.CacheHandler
{
    public class RedisCacheHandler : ICacheHandler
    {
        private readonly ILogger<RedisCacheHandler> _logger;
        private readonly IDistributedCache _distributedCache;

        public RedisCacheHandler(ILogger<RedisCacheHandler> logger, IDistributedCache distributedCache)
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
