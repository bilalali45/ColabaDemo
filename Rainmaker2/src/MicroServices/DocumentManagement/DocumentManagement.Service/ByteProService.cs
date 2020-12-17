using DocumentManagement.Entity;
using DocumentManagement.Model;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Service
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

        public async Task<FileViewDto> View(AdminFileViewModel model, int tenantId)
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
            FileViewDto fileViewDTO = null;
            if (await asyncCursor.MoveNextAsync())
            {
                if(asyncCursor.Current.FirstOrDefault()!=null)
                    fileViewDTO = BsonSerializer.Deserialize<FileViewDto>(asyncCursor.Current.FirstOrDefault());
            }
            if(fileViewDTO==null)
            {
                fileViewDTO = await mcuView(model,tenantId);
            }
            return fileViewDTO;
        }
        public async Task<FileViewDto> mcuView(AdminFileViewModel model, int tenantId)
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
                            ""$unwind"": ""$requests.documents.mcuFiles""
                        }",

                        @"{
                            ""$match"": {
                                ""requests.documents.mcuFiles.id"": " + new ObjectId(model.fileId).ToJson() + @"
                            }
                        }",

                        @"{
                            ""$project"": {
                                ""_id"": 0,                               
                                ""serverName"": ""$requests.documents.mcuFiles.serverName"",
                                ""encryptionKey"": ""$requests.documents.mcuFiles.encryptionKey"",
                                ""encryptionAlgorithm"": ""$requests.documents.mcuFiles.encryptionAlgorithm"",
                                ""clientName"": ""$requests.documents.mcuFiles.clientName"",
                                ""contentType"": ""$requests.documents.mcuFiles.contentType""
                            }
                             } "

                ));
            FileViewDto fileViewDTO = null;
            if (await asyncCursor.MoveNextAsync())
            {
                if (asyncCursor.Current.FirstOrDefault() != null)
                    fileViewDTO = BsonSerializer.Deserialize<FileViewDto>(asyncCursor.Current.FirstOrDefault());
            }
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

        public async Task UploadFiles(string id, string requestId, string docId, List<string> auth)
        {
            int loanApplicationId = await rainmakerService.GetLoanApplicationId(id);
            IMongoCollection<Entity.Request> collection = mongoService.db.GetCollection<Entity.Request>("Request");

            using var asyncCursor = collection.Aggregate(PipelineDefinition<Entity.Request, BsonDocument>.Create(
              @"{""$match"": {

                  ""_id"": " + new ObjectId(id).ToJson() + @" 
                            }
                        }",
                        @"{
                            ""$unwind"": ""$requests""
                        }",
                        @"{
                            ""$match"": {
                                ""requests.id"": " + new ObjectId(requestId).ToJson() + @"
                            }
                        }",
                        @"{
                            ""$unwind"": ""$requests.documents""
                        }",
                        @"{
                            ""$match"": {
                                ""requests.documents.id"": " + new ObjectId(docId).ToJson() + @"
                            }
                        }",
                        @"{
                            ""$unwind"": ""$requests.documents.files""
                        }",

                        @"{
                            ""$match"": {
                                ""requests.documents.files.byteProStatus"": """ + ByteProStatus.NotSynchronized + @"""
                            }
                        }",

                        @"{
                            ""$project"": {
                                ""_id"": 0,                               
                                ""fileId"": ""$requests.documents.files.id"",
                            }
                             } "

                ));
            while (await asyncCursor.MoveNextAsync())
            {
                foreach (var current in asyncCursor.Current)
                {
                    FileIdModel model = BsonSerializer.Deserialize<FileIdModel>(current);
                    await losIntegrationService.SendFilesToBytePro(loanApplicationId,
                        id,
                        requestId,
                        docId,
                        model.fileId,
                        auth);
                }
            }
            // upload mcu files
            using var asyncCursor1 = collection.Aggregate(PipelineDefinition<Entity.Request, BsonDocument>.Create(
              @"{""$match"": {

                  ""_id"": " + new ObjectId(id).ToJson() + @" 
                            }
                        }",
                        @"{
                            ""$unwind"": ""$requests""
                        }",
                        @"{
                            ""$match"": {
                                ""requests.id"": " + new ObjectId(requestId).ToJson() + @"
                            }
                        }",
                        @"{
                            ""$unwind"": ""$requests.documents""
                        }",
                        @"{
                            ""$match"": {
                                ""requests.documents.id"": " + new ObjectId(docId).ToJson() + @"
                            }
                        }",
                        @"{
                            ""$unwind"": ""$requests.documents.mcuFiles""
                        }",

                        @"{
                            ""$match"": {
                                ""requests.documents.mcuFiles.byteProStatus"": """ + ByteProStatus.NotSynchronized + @"""
                            }
                        }",

                        @"{
                            ""$project"": {
                                ""_id"": 0,                               
                                ""fileId"": ""$requests.documents.mcuFiles.id"",
                            }
                             } "

                ));
            while (await asyncCursor1.MoveNextAsync())
            {
                foreach (var current in asyncCursor1.Current)
                {
                    FileIdModel model = BsonSerializer.Deserialize<FileIdModel>(current);
                    await losIntegrationService.SendFilesToBytePro(loanApplicationId,
                        id,
                        requestId,
                        docId,
                        model.fileId,
                        auth);
                }
            }
        }
    }
}
