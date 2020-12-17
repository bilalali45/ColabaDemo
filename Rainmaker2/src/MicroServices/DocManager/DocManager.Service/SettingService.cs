using DocManager.Model;
using MongoDB.Driver;
using System.Linq;
using System.Threading.Tasks;

namespace DocManager.Service
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
            using IAsyncCursor<Setting> setting = await collection.FindAsync(FilterDefinition<Setting>.Empty);
            await setting.MoveNextAsync();
            return setting.Current.FirstOrDefault();
        }
        public async Task<LockSetting> GetLockSetting()
        {
            IMongoCollection<LockSetting> collection = mongoService.db.GetCollection<LockSetting>("LockSetting");
            using IAsyncCursor<LockSetting> setting = await collection.FindAsync(FilterDefinition<LockSetting>.Empty);
            await setting.MoveNextAsync();
            return setting.Current.FirstOrDefault();
        }
    }
}
