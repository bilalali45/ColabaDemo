using System;

namespace RainMaker.Common.Caching
{
    /// <summary>
    /// Extensions
    /// </summary>
    public static class CacheExtensions
    {
        public static T Get<T>(this ICacheManager cacheManager, string key, Func<T> acquire, int cacheTime = 60)
        {
            return Get(cacheManager, key, cacheTime, acquire);
        }

        public static T Get<T>(this ICacheManager cacheManager, string key, int cacheTime, Func<T> acquire) 
        {
            if (cacheManager.IsSet(key))
            {
                return cacheManager.Get<T>(key);
            }
            var result = acquire();
            cacheManager.Set(key, result, cacheTime);
            return result;
        }
    }
}
