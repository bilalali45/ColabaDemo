using DocumentManagement.Entity;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Service
{

    public class SettingService : ISettingService
    {
        private readonly IMongoService mongoService;

        public SettingService(IMongoService mongoService)
        {
            this.mongoService = mongoService;
        }
        public async Task<Setting> GetSetting()
        {
            IMongoCollection<Setting> collection = mongoService.db.GetCollection<Setting>("Setting");
            IAsyncCursor<Setting> setting = await collection.FindAsync(FilterDefinition<Setting>.Empty);
            setting.MoveNext();
            return setting.Current.FirstOrDefault();
        }
    }
}
