using ByteWebConnector.Data;
using ByteWebConnector.Entity.Models;
using Extensions.ExtensionClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using ByteWebConnector.Model.Models.OwnModels.Settings;
using URF.Core.Abstractions;

namespace ByteWebConnector.Service.DbServices
{
    public partial class SettingService : ServiceBase<BwcContext, ByteWebConnector.Entity.Models.Setting>, ISettingService
    {
        public SettingService(IUnitOfWork<BwcContext> previousUow,
                              IServiceProvider services) : base(previousUow: previousUow,
                                                                services: services)
        {
        }


        public List<Entity.Models.Setting> GetSettingWithDetails(int? id = null,
                                                                                  string tag = null)
        {
            var mappings = Repository.Query().AsQueryable();

            // @formatter:off 
            if (id.HasValue()) mappings = mappings.Where(predicate: setting => setting.Id == id);
            if (tag.HasValue()) mappings = mappings.Where(predicate: setting => setting.Tag == tag);
            // @formatter:on 

            return mappings.ToList();
        }


        public ByteProSettings GetByteProSettings()
        {
            var settingWithDetails = GetSettingWithDetails(tag: "BytePro");

            return new ByteProSettings(settings: settingWithDetails);
        }
    }
}