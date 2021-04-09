using System.Threading.Tasks;

namespace TokenCacheHelper.CacheHandler
{
    public interface ICacheHandler
    {

        void SetCacheItemAsync<T>(string key,
                                  T itemData);
        Task<T> GetCacheItemAsync<T>(string key);
        Task<bool> RemoveCacheItemAsync(string key);
    }
}
