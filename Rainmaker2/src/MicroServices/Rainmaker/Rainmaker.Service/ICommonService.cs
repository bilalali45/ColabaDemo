using System.Collections.Generic;
using System.Threading.Tasks;

namespace RainMaker.Service
{
    public interface ICommonService
    {
        //Task<Dictionary<string, KeyValuePair<int, string>>> GetAllResourceValueAsync(int languageId = 1,
        //    int? businessUnit = null);
        //void Copy<T>(T from, T to);
        //Task<Dictionary<string, KeyValuePair<int, string>>> GetAllSettingValueAsync(int? businessUnit = null);
        //Task<string> GetResourceByNameAsync(string key, params string[] parmas);
        //Task<string> GetResourceByNameAsync(string key, int languageId = 1, int? businessUnit = null, params string[] parmas);
        
        //Task<T> GetSettingValueByKeyExcludeExceptionsAsync<T>(string settingKey, int? businessUnit = null,
        //    T defaultValue = default(T));
        
        //Task<Dictionary<string, KeyValuePair<int, string>>> GetAllFreshSettingValueAsync(string keyName, int? businessUnit = null);
        
        Task<T> GetSettingValueByKeyAsync<T>(string settingKey, int? businessUnit = null, T defaultValue = default(T));
        Task<T> GetSettingFreshValueByKeyAsync<T>(string settingKey, int? businessUnit = null, T defaultValue = default(T));
    }
}
