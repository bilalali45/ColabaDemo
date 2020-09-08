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
    public class StringResourceService : ServiceBase<RainMakerContext,LocaleStringResource>, IStringResourceService
    {
        public StringResourceService(IUnitOfWork<RainMakerContext> uow, IServiceProvider services) :base(uow,services)
        {
        }
        public async Task<(IEnumerable<LocaleStringResource> collection, int totalRecords)> GetPagedListAsync(int pageNumber, int pageSize, DynamicLinQFilter gridfilter, int resourceTypeId)
        {

            string order = gridfilter.Sort.Split('-')[0];
            string member = gridfilter.Sort.Split('-')[1];

            if (order == "Des")
            {
                member += " descending";
            }


            var stringresource = await Uow.Repository<LocaleStringResource>()
                .Query(gridfilter, x => x.ResourceForId == resourceTypeId)
                .Include(x => x.Language)
                .Include(x => x.BusinessUnit)
                .OrderBy(member)
                .SelectPageAsync(pageNumber, pageSize);

            return stringresource;
        }

        public async Task<LocaleStringResource> GetByKeyAsync(string key, int languageid = 1, int? businessUnitId = null)
        {
            var res = (await Uow.Repository<LocaleStringResource>()
                .Query()
                .ToListAsync()).OrderByDescending(x => x.BusinessUnitId).FirstOrDefault(x => x.ResourceName == key && x.LanguageId == languageid && (x.BusinessUnitId == null || x.BusinessUnitId == businessUnitId));
            return res;
        }

        public async Task<string> GetByNameAsync(string name, int languageid = 1)
        {
            var res = (await Uow.Repository<LocaleStringResource>()
                .Query()
                .ToListAsync()).FirstOrDefault(x => x.ResourceName == name && x.LanguageId == languageid);
            return res != null ? res.ResourceValue : "";
        }

        public async Task<IList<LocaleStringResource>> GetByLanguageAsync(int languageid = 1, int? businessUnitId = null)
        {

            var localeStringResource = (await Uow.Repository<LocaleStringResource>()
                     .Query(x => x.LanguageId == languageid && (x.BusinessUnitId == null || x.BusinessUnitId == businessUnitId))
                     .ToListAsync()).OrderByDescending(x => x.BusinessUnitId).ToList();

            return localeStringResource;

        }

        public override async Task<LocaleStringResource> GetByIdWithDetailAsync(int id)
        {

            var result = await Uow.Repository<LocaleStringResource>().Query(x => x.Id == id)
                .Include(x => x.Language)
                .ToListAsync();

            if (result.Count() > 0)
                return result.FirstOrDefault();
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<LocaleStringResource>> GetAllAdSourceMessagesAsync(int resourceTypeId)
        {
            var localeStringResource = (await Uow.Repository<LocaleStringResource>().Query(x => x.ResourceForId == resourceTypeId && x.BusinessUnitId == null).ToListAsync()).OrderByDescending(x => x.BusinessUnitId);

            return localeStringResource;
        }

        public async Task<bool> IsUniqueSettingAsync(string name, int? businessUnitId, int id)
        {
            return (await Uow.Repository<LocaleStringResource>().Query(x => x.ResourceName == name && x.Id != id && x.BusinessUnitId == businessUnitId).ToListAsync()).Any();
        }
    }
}
