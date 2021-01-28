using DocManager.Model;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocManager.Service
{
    public class ByteProService : IByteProService
    {
        private readonly IMongoService mongoService;
        private readonly IRainmakerService rainmakerService;
        private readonly ILosIntegrationService losIntegrationService;
        public ByteProService(IMongoService mongoService,
            IRainmakerService rainmakerService,
            ILosIntegrationService losIntegrationService)
        {
            this.mongoService = mongoService;
            this.rainmakerService = rainmakerService;
            this.losIntegrationService = losIntegrationService;
        }

        public async Task<Tenant> GetTenantSetting(int tenantId)
        {
            Tenant tenant = null;
            IMongoCollection<Tenant> collection = mongoService.db.GetCollection<Tenant>("Tenant");
            using var asyncCursor = await collection.FindAsync(new BsonDocument() {
                { "tenantId",tenantId }
            });
            if (await asyncCursor.MoveNextAsync() && asyncCursor.Current?.Count() > 0)
            {
                tenant = asyncCursor.Current.First();
            }
            return tenant;
        }
    }
}
