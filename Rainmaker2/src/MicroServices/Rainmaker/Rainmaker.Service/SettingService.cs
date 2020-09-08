using Microsoft.EntityFrameworkCore;
using RainMaker.Common;
using RainMaker.Data;
using RainMaker.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using URF.Core.Abstractions;
using URF.Core.EF;

namespace RainMaker.Service
{
    public class SettingService : ServiceBase<RainMakerContext,Setting>, ISettingService
    {
        public SettingService(IUnitOfWork<RainMakerContext> previousUow, IServiceProvider services)
            : base(previousUow,services)
        {
        }

        public async Task<(IEnumerable<Setting> collection, int totalRecords)> GetPagedListAsync(int pageNumber, int pageSize, DynamicLinQFilter gridfilter)
        {
            string order = gridfilter.Sort.Split('-')[0];
            string member = gridfilter.Sort.Split('-')[1];


            if (order == "Des")
            {
                member += " descending";
            }
            var query = Uow.Repository<Setting>()
                .Query(gridfilter, x => x.IsDeleted != true)
                .OrderBy(member);


            var settings = await query.SelectPageAsync(pageNumber, pageSize);

            return settings;

        }
        public override async Task<Setting> GetByIdWithDetailAsync(int id)
        {
            return (await Uow.Repository<Setting>().Query(a => a.Id == id && a.IsDeleted != true).ToListAsync()).FirstOrDefault();
        }
        public async Task<Setting> GetSettingAsync(string key)
        {
            return (await Uow.Repository<Setting>().Query(a => a.Name == key && a.IsActive != false && a.IsDeleted != true).ToListAsync()).FirstOrDefault();
        }

        public async Task<IList<Setting>> GetAllSettingAsync(int? businessUnit = null)
        {
            return (await Uow.Repository<Setting>().Query(x => x.BusinessUnitId == null || x.BusinessUnitId == businessUnit).ToListAsync()).OrderByDescending(x => x.BusinessUnitId).ToList();
        }

        public async Task<string> GetSettingValueAsync(string keyName)
        {
            string settingValue = string.Empty;
            var setting = await GetSettingAsync(keyName);
            if (setting != null)
            {
                settingValue = setting.Value;
            }

            return settingValue;
        }

        public async Task<bool> IsExistsAsync(string name)
        {

            return (await Uow.Repository<Setting>()
                      .Query(x => x.IsDeleted != true && x.Name.ToLower().Trim() == name.ToLower().Trim()).ToListAsync())
                      .Any();

        }
        public async Task<IEnumerable<Setting>> GetGroupSettingsAsync(int gId, int buId)
        {

            var dynamicLinQFilter = new DynamicLinQFilter
            {
                Filter = (buId == 0) ? "BusinessUnitId != @0" : "BusinessUnitId = @0"
            };
            dynamicLinQFilter.Predicates.Add(buId);

            if (gId != 0)
            {
                dynamicLinQFilter.Filter += (dynamicLinQFilter.Filter.Length > 10) ? " And " : "";
                dynamicLinQFilter.Filter += "SettingGroupBinders.Any(GroupId= @1)";
                dynamicLinQFilter.Predicates.Add(gId);
            }



            var query = Uow.Repository<Setting>().Query(dynamicLinQFilter, x => x.SettingGroupBinders.Any(gb => gb.GroupId > 0) && x.IsDeleted != true)
                        .Include(x => x.SettingGroupBinders).ThenInclude(c => c.SettingGroup)
                        .Include(x => x.BusinessUnit);

            var ret = await query.ToListAsync();

            return ret;


        }

        public async Task<bool> IsUniqueSettingAsync(string name, int? businessUnitId, int id)
        {
            return (await Uow.Repository<Setting>().Query(x => x.Name == name && x.Id != id && x.BusinessUnitId == businessUnitId && x.IsDeleted != true).ToListAsync()).Any();
        }

        public async Task<Setting> GetSettingByKeyAsync(string settingKey, int? businessUnit = null)
        {
            return (await Uow.Repository<Setting>().Query(x => x.Name == settingKey && (x.BusinessUnitId == null || x.BusinessUnitId == businessUnit)).ToListAsync()).FirstOrDefault();
        }

        public async Task<IList<Setting>> GetSettingsForAllBusinessUnitsAsync()
        {
            return (await Uow.Repository<Setting>().Query(x => x.IsDifferentForBusinessUnit && x.BusinessUnitId == null).ToListAsync()).OrderBy(o => o.Name).ToList();
        }

        public async Task<Setting> GetSettingByKeyNameAsync(string keyName, int? businessUnitId)
        {
            return (await Uow.Repository<Setting>().Query(x => x.Name == keyName && (x.BusinessUnitId == null || x.BusinessUnitId == businessUnitId) && x.IsActive != false && x.IsDeleted != true).ToListAsync()).FirstOrDefault();
        }
    }
}
