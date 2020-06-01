using System.Collections.Generic;
using RainMaker.Entity.Models;
using RainMaker.Common;
using System.Threading.Tasks;

namespace RainMaker.Service
{
    public interface IStringResourceService : IServiceBase<LocaleStringResource>
    {
        Task<(IEnumerable<LocaleStringResource> collection, int totalRecords)> GetPagedListAsync(int pageNumber, int pageSize, DynamicLinQFilter gridfilter, int resourceTypeId);
        Task<string> GetByNameAsync(string name, int languageid = 1);

        Task<LocaleStringResource> GetByKeyAsync(string key, int languageid = 1, int? businessUnitId = null);

        Task<IEnumerable<LocaleStringResource>> GetAllAdSourceMessagesAsync(int resourceTypeId);
        Task<bool> IsUniqueSettingAsync(string name, int? businessUnitId, int id);
        Task<IList<LocaleStringResource>> GetByLanguageAsync(int languageid = 1, int? businessUnitId = null);
    }
}
