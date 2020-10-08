using DocumentManagement.Entity;
using MongoDB.Driver;
using System.Linq;
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
            using IAsyncCursor<Setting> setting = await collection.FindAsync(FilterDefinition<Setting>.Empty);
            await setting.MoveNextAsync();
            return setting.Current.FirstOrDefault();
        }
    }
}
