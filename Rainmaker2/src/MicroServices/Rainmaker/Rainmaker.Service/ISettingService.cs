using System.Collections.Generic;
using RainMaker.Entity.Models;
using RainMaker.Common;
using System.Threading.Tasks;

namespace RainMaker.Service
{
    public interface ISettingService : IServiceBase<Setting>
    {
        Task<(IEnumerable<Setting> collection,int totalRecords)> GetPagedListAsync(int pageNumber, int pageSize, DynamicLinQFilter gridfilter);
        Task<Setting> GetSettingAsync(string keyName);
        Task<string> GetSettingValueAsync(string keyName);
        Task<bool> IsExistsAsync(string name);

        Task<IEnumerable<Setting>> GetGroupSettingsAsync(int gId, int buId);
        Task<bool> IsUniqueSettingAsync(string name, int? businessUnitId, int id);

        Task<IList<Setting>> GetSettingsForAllBusinessUnitsAsync();

        Task<Setting> GetSettingByKeyNameAsync(string keyName, int? businessUnitId);
        Task<IList<Setting>> GetAllSettingAsync(int? businessUnit = null);
        Task<Setting> GetSettingByKeyAsync(string settingKey, int? businessUnit = null);
    }
}
