using DocumentManagement.Entity;
using DocumentManagement.Model;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Service
{
    public class ByteProService : IByteProService
    {
        private readonly IMongoService mongoService;
        public ByteProService(IMongoService mongoService)
        {
            this.mongoService = mongoService;
        }

        public async Task<FileViewDTO> View(AdminFileViewModel model, int tenantId)
        {
            IMongoCollection<Entity.Request> collection = mongoService.db.GetCollection<Entity.Request>("Request");

            using var asyncCursor = collection.Aggregate(PipelineDefinition<Entity.Request, BsonDocument>.Create(
              @"{""$match"": {

                  ""_id"": " + new ObjectId(model.id).ToJson() + @" ,
                  ""tenantId"": " + tenantId + @"
                            }
                        }",
                        @"{
                            ""$unwind"": ""$requests""
                        }",
                        @"{
                            ""$match"": {
                                ""requests.id"": " + new ObjectId(model.requestId).ToJson() + @"
                            }
                        }",
                        @"{
                            ""$unwind"": ""$requests.documents""
                        }",
                        @"{
                            ""$match"": {
                                ""requests.documents.id"": " + new ObjectId(model.docId).ToJson() + @"
                            }
                        }",
                        @"{
                            ""$unwind"": ""$requests.documents.files""
                        }",

                        @"{
                            ""$match"": {
                                ""requests.documents.files.id"": " + new ObjectId(model.fileId).ToJson() + @"
                            }
                        }",

                        @"{
                            ""$project"": {
                                ""_id"": 0,                               
                                ""serverName"": ""$requests.documents.files.serverName"",
                                ""encryptionKey"": ""$requests.documents.files.encryptionKey"",
                                ""encryptionAlgorithm"": ""$requests.documents.files.encryptionAlgorithm"",
                                ""clientName"": ""$requests.documents.files.clientName"",
                                ""contentType"": ""$requests.documents.files.contentType""
                            }
                             } "

                ));

            await asyncCursor.MoveNextAsync();
            FileViewDTO fileViewDTO = BsonSerializer.Deserialize<FileViewDTO>(asyncCursor.Current.FirstOrDefault());

            return fileViewDTO;
        }
        public async Task<Tenant> GetTenantSetting(int tenantId)
        {
            Tenant tenant = null;
            IMongoCollection<Entity.Tenant> collection = mongoService.db.GetCollection<Entity.Tenant>("Tenant");
            using var asyncCursor = await collection.FindAsync(new BsonDocument() {
                { "tenantId",tenantId }
            });
            if (await asyncCursor.MoveNextAsync() && asyncCursor.Current?.Count() > 0)
            {
                tenant = asyncCursor.Current.First();
            }
            return tenant;
        }
        public async Task SetTenantSetting(int tenantId, TenantSetting setting)
        {
            IMongoCollection<Entity.Tenant> collection = mongoService.db.GetCollection<Entity.Tenant>("Tenant");
            UpdateResult result = await collection.UpdateOneAsync(new BsonDocument() {
                { "tenantId",tenantId }
            }, new BsonDocument()
            {
                { "$set", new BsonDocument()
                    {
                        { "syncToBytePro", setting.syncToBytePro },
                        { "autoSyncToBytePro", setting.autoSyncToBytePro }
                    }
                }
            });
            if (result.ModifiedCount <= 0)
                throw new DocumentManagementException("Unable to update settings");
        }

    }
}
