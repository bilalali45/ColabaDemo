using DocumentManagement.Entity;
using DocumentManagement.Model;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace DocumentManagement.Service
{
    public class FileService : IFileService
    {
        private readonly IMongoService mongoService;
        private readonly IActivityLogService activityLogService;
        private readonly IRainmakerService rainmakerService;
        public FileService(IMongoService mongoService, IActivityLogService activityLogService, IRainmakerService rainmakerService)
        {
            this.mongoService = mongoService;
            this.activityLogService = activityLogService;
            this.rainmakerService = rainmakerService;
        }
        public async Task<bool> Rename(FileRenameModel model, int userProfileId, int tenantId)
        {
            IMongoCollection<Entity.Request> collection = mongoService.db.GetCollection<Entity.Request>("Request");

            UpdateResult result = await collection.UpdateOneAsync(new BsonDocument()
            {
                { "_id", BsonObjectId.Create(model.id) },
                { "tenantId", tenantId},
                { "userId", userProfileId}
            }, new BsonDocument()
            {
                { "$set", new BsonDocument()
                    {
                        { "requests.$[request].documents.$[document].files.$[file].clientName", model.fileName }
                    }
                }
            }, new UpdateOptions()
            {
                ArrayFilters = new List<ArrayFilterDefinition>()
                {
                    new JsonArrayFilterDefinition<Entity.Request>("{ \"request.id\": "+new ObjectId(model.requestId).ToJson()+"}"),
                    new JsonArrayFilterDefinition<Entity.Request>("{ \"document.id\": "+new ObjectId(model.docId).ToJson()+"}"),
                    new JsonArrayFilterDefinition<Entity.Request>("{ \"file.id\": "+new ObjectId(model.fileId).ToJson()+"}")
                }
            });

            return result.ModifiedCount == 1;
        }
        public async Task<bool> Done(DoneModel model, int userProfileId, int tenantId, IEnumerable<string> authHeader)
        {
            IMongoCollection<Entity.Request> collection = mongoService.db.GetCollection<Entity.Request>("Request");
            UpdateResult result = await collection.UpdateOneAsync(new BsonDocument()
            {
                { "_id", BsonObjectId.Create(model.id) },
                { "tenantId", tenantId},
                { "userId", userProfileId}
            }, new BsonDocument()
            {
                { "$set", new BsonDocument()
                    {
                        { "requests.$[request].documents.$[document].status", DocumentStatus.PendingReview}

                    }
                }
            }, new UpdateOptions()
            {
                ArrayFilters = new List<ArrayFilterDefinition>()
                {
                    new JsonArrayFilterDefinition<Entity.Request>("{ \"request.id\": "+new ObjectId(model.requestId).ToJson()+"}"),
                    new JsonArrayFilterDefinition<Entity.Request>("{ \"document.id\": "+new ObjectId(model.docId).ToJson()+"}"),
                }
            });

            if (result.ModifiedCount == 1)
            {
                string activityLogId = await activityLogService.GetActivityLogId(model.id, model.requestId, model.docId);

                await activityLogService.InsertLog(activityLogId, string.Format(ActivityStatus.StatusChanged, DocumentStatus.PendingReview));
            }

            await rainmakerService.UpdateLoanInfo(null, model.id, authHeader);

            return result.ModifiedCount == 1;
        }

        public async Task Order(FileOrderModel model, int userProfileId, int tenantId)
        {
            IMongoCollection<Entity.Request> collection = mongoService.db.GetCollection<Entity.Request>("Request");

            foreach (var item in model.files)
            {
                await collection.UpdateOneAsync(new BsonDocument()
                {
                    { "_id", BsonObjectId.Create(model.id) },
                    { "tenantId", tenantId},
                    { "userId", userProfileId}
                }, new BsonDocument()
                {
                    { "$set", new BsonDocument()
                        {
                            { "requests.$[request].documents.$[document].files.$[file].order", item.order }
                        }
                    }
                }, new UpdateOptions()
                {
                    ArrayFilters = new List<ArrayFilterDefinition>()
                    {
                        new JsonArrayFilterDefinition<Entity.Request>("{ \"request.id\": "+new ObjectId(model.requestId).ToJson()+"}"),
                        new JsonArrayFilterDefinition<Entity.Request>("{ \"document.id\": "+new ObjectId(model.docId).ToJson()+"}"),
                        new JsonArrayFilterDefinition<Entity.Request>("{ \"file.clientName\": \""+item.fileName.Replace("\"","\\\"")+"\"}")
                    }
                });
            }
        }

        public async Task<string> Submit(string contentType, string id, string requestId, string docId, string clientName, string serverName, int size, string encryptionKey, string encryptionAlgorithm, int tenantId, int userProfileId, IEnumerable<string> authHeader,string salt)
        {
            bool isStarted = false;

            IMongoCollection<Entity.Request> collectionRequst = mongoService.db.GetCollection<Entity.Request>("Request");

            using var asyncCursor = collectionRequst.Aggregate(
                PipelineDefinition<Entity.Request, BsonDocument>.Create(
                    @"{""$match"": {
                    ""_id"": " + new ObjectId(id).ToJson() + @"
                            }
                        }",
                    @"{
                            ""$unwind"": ""$requests""
                        }", @"{
                            ""$match"": {
                                ""requests.id"": " + new ObjectId(requestId).ToJson() + @"
                            }
                        }",
                    @"{
                            ""$unwind"": ""$requests.documents""
                        }",
                    @"{
                            ""$match"": {
                                ""requests.documents.id"": " + new ObjectId(docId).ToJson() + @",
                                ""requests.documents.status"": """ + DocumentStatus.Started + @""",
                            }
                        }", @"{
                            ""$project"": {
                                 ""_id"": 1
                            }
                        }"
                ));

            if (await asyncCursor.MoveNextAsync())
            {
                foreach (var current in asyncCursor.Current)
                {
                    isStarted = true;
                }
            }

            IMongoCollection<Entity.Request> collection = mongoService.db.GetCollection<Entity.Request>("Request");
            var fileId = ObjectId.GenerateNewId();
            UpdateResult result = await collection.UpdateOneAsync(new BsonDocument()
            {
                { "_id", BsonObjectId.Create(id) },
                { "tenantId", tenantId},
                { "userId", userProfileId},
                {
                    "requests" , new BsonDocument()
                    {
                        {
                            "$elemMatch" , new BsonDocument()
                            {
                                {
                                    "$and",new BsonArray()
                                    {
                                        new BsonDocument()
                                        {
                                            { "id", BsonObjectId.Create(requestId) }
                                        },
                                        new BsonDocument()
                                        {
                                            {
                                                "documents",new BsonDocument()
                                                {
                                                    {
                                                        "$elemMatch", new BsonDocument()
                                                        {
                                                            {
                                                                "$and", new BsonArray()
                                                                {
                                                                    new BsonDocument()
                                                                    {
                                                                        { "id", BsonObjectId.Create(docId) }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new BsonDocument()
            {
                { "$push", new BsonDocument()
                    {   
                        { "requests.$[request].documents.$[document].files", new BsonDocument() { { "id", fileId }, { "clientName", clientName } , { "serverName", serverName }, { "fileUploadedOn", BsonDateTime.Create(DateTime.UtcNow) }, { "size", size }, { "encryptionKey", encryptionKey }, { "encryptionAlgorithm", encryptionAlgorithm }, { "order" , 0 }, { "mcuName", BsonString.Empty }, { "contentType", contentType }, { "status", FileStatus.SubmittedToMcu },{ "byteProStatus", ByteProStatus.NotSynchronized}, { "isRead", false },{ "userId",BsonNull.Value}, { "userName", BsonNull.Value } , { "salt",salt} }   }
                    }
                },
                { "$set", new BsonDocument()
                    {
                        { "requests.$[request].documents.$[document].status", DocumentStatus.Started},
                        { "requests.$[request].documents.$[document].isMcuVisible", true}
                    }
                }
            }, new UpdateOptions()
            {
                ArrayFilters = new List<ArrayFilterDefinition>()
                {
                    new JsonArrayFilterDefinition<Entity.Request>("{ \"request.id\": "+new ObjectId(requestId).ToJson()+"}"),
                    new JsonArrayFilterDefinition<Entity.Request>("{ \"document.id\": "+new ObjectId(docId).ToJson()+"}")
                }
            });

            if (result.ModifiedCount == 1)
            {
                string activityLogId = await activityLogService.GetActivityLogId(id, requestId, docId);

                await activityLogService.InsertLog(activityLogId, string.Format(ActivityStatus.FileSubmitted, clientName));
            }
            if (result.ModifiedCount == 1 && !isStarted)
            {
                string activityLogId = await activityLogService.GetActivityLogId(id, requestId, docId);

                await activityLogService.InsertLog(activityLogId, string.Format(ActivityStatus.StatusChanged, DocumentStatus.Started));
            }
            await rainmakerService.UpdateLoanInfo(null, id, authHeader);
            if (result.ModifiedCount==1)
            {
                return fileId.ToString();
            }

               
            return null;
        }
        public async Task<string> SubmitByBorrower(string contentType, string id, string requestId, string docId, string clientName, string serverName, int size, string encryptionKey, string encryptionAlgorithm, int tenantId, int userProfileId, IEnumerable<string> authHeader, string salt)
        {
            bool isStarted = false;

            IMongoCollection<Entity.Request> collectionRequst = mongoService.db.GetCollection<Entity.Request>("Request");

            using var asyncCursor = collectionRequst.Aggregate(
                PipelineDefinition<Entity.Request, BsonDocument>.Create(
                    @"{""$match"": {
                    ""_id"": " + new ObjectId(id).ToJson() + @"
                            }
                        }",
                    @"{
                            ""$unwind"": ""$requests""
                        }", @"{
                            ""$match"": {
                                ""requests.id"": " + new ObjectId(requestId).ToJson() + @"
                            }
                        }",
                    @"{
                            ""$unwind"": ""$requests.documents""
                        }",
                    @"{
                            ""$match"": {
                                ""requests.documents.id"": " + new ObjectId(docId).ToJson() + @",
                                ""requests.documents.status"": """ + DocumentStatus.PendingReview + @""",
                            }
                        }", @"{
                            ""$project"": {
                                 ""_id"": 1
                            }
                        }"
                ));

            if (await asyncCursor.MoveNextAsync())
            {
                foreach (var current in asyncCursor.Current)
                {
                    isStarted = true;
                }
            }

            IMongoCollection<Entity.Request> collection = mongoService.db.GetCollection<Entity.Request>("Request");
            var fileId = ObjectId.GenerateNewId();
            UpdateResult result = await collection.UpdateOneAsync(new BsonDocument()
            {
                { "_id", BsonObjectId.Create(id) },
                { "tenantId", tenantId},
                { "userId", userProfileId},
                {
                    "requests" , new BsonDocument()
                    {
                        {
                            "$elemMatch" , new BsonDocument()
                            {
                                {
                                    "$and",new BsonArray()
                                    {
                                        new BsonDocument()
                                        {
                                            { "id", BsonObjectId.Create(requestId) }
                                        },
                                        new BsonDocument()
                                        {
                                            {
                                                "documents",new BsonDocument()
                                                {
                                                    {
                                                        "$elemMatch", new BsonDocument()
                                                        {
                                                            {
                                                                "$and", new BsonArray()
                                                                {
                                                                    new BsonDocument()
                                                                    {
                                                                        { "id", BsonObjectId.Create(docId) }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new BsonDocument()
            {
                { "$push", new BsonDocument()
                    {
                        { "requests.$[request].documents.$[document].files", new BsonDocument() { { "id", fileId }, { "clientName", clientName } , { "serverName", serverName }, { "fileUploadedOn", BsonDateTime.Create(DateTime.UtcNow) }, { "size", size }, { "encryptionKey", encryptionKey }, { "encryptionAlgorithm", encryptionAlgorithm }, { "order" , 0 }, { "mcuName", BsonString.Empty }, { "contentType", contentType }, { "status", FileStatus.SubmittedToMcu },{ "byteProStatus", ByteProStatus.NotSynchronized}, { "isRead", false },{ "userId",BsonNull.Value}, { "userName", BsonNull.Value }, {"salt",salt } } }
                    }
                }
            }, new UpdateOptions()
            {
                ArrayFilters = new List<ArrayFilterDefinition>()
                {
                    new JsonArrayFilterDefinition<Entity.Request>("{ \"request.id\": "+new ObjectId(requestId).ToJson()+"}"),
                    new JsonArrayFilterDefinition<Entity.Request>("{ \"document.id\": "+new ObjectId(docId).ToJson()+"}")
                }
            });

            if (result.ModifiedCount == 1)
            {
                string activityLogId = await activityLogService.GetActivityLogId(id, requestId, docId);

                await activityLogService.InsertLog(activityLogId, string.Format(ActivityStatus.FileSubmitted, clientName));
            }
            if (result.ModifiedCount == 1 && !isStarted)
            {
                string activityLogId = await activityLogService.GetActivityLogId(id, requestId, docId);

                await activityLogService.InsertLog(activityLogId, string.Format(ActivityStatus.StatusChanged, DocumentStatus.Started));
            }
            await rainmakerService.UpdateLoanInfo(null, id, authHeader);
            if (result.ModifiedCount == 1)
            {
                return fileId.ToString();
            }


            return null;
        }

        public async Task<FileViewDto> View(FileViewModel model, int userProfileId, string ipAddress, int tenantId)
        {
            IMongoCollection<Entity.Request> collection = mongoService.db.GetCollection<Entity.Request>("Request");

            using var asyncCursor = collection.Aggregate(PipelineDefinition<Entity.Request, BsonDocument>.Create(
              @"{""$match"": {

                  ""_id"": " + new ObjectId(model.id).ToJson() + @" ,
                  ""tenantId"": " + tenantId + @",
                  ""userId"": " + userProfileId + @"
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
                                ""contentType"": ""$requests.documents.files.contentType"",
                                ""salt"": ""$requests.documents.files.salt""
                            }
                             } "

));


            await asyncCursor.MoveNextAsync();
            FileViewDto fileViewDTO = BsonSerializer.Deserialize<FileViewDto>(asyncCursor.Current.FirstOrDefault());

            IMongoCollection<ViewLog> viewLogCollection = mongoService.db.GetCollection<ViewLog>("ViewLog");

            ViewLog viewLog = new ViewLog() { userProfileId = userProfileId, createdOn = DateTime.UtcNow, ipAddress = ipAddress, loanApplicationId = model.id, requestId = model.requestId, documentId = model.docId, fileId = model.fileId };
            await viewLogCollection.InsertOneAsync(viewLog);

            return fileViewDTO;
        }

        /*
        public async Task<List<FileViewDto>> GetFileByDocId(FileViewModel model, int userProfileId, string ipAddress, int tenantId)
        {
            IMongoCollection<Entity.Request> collection = mongoService.db.GetCollection<Entity.Request>("Request");

            using var asyncCursor = collection.Aggregate(PipelineDefinition<Entity.Request, BsonDocument>.Create(
              @"{""$match"": {

                  ""_id"": " + new ObjectId(model.id).ToJson() + @" ,
                  ""tenantId"": " + tenantId + @",
                  ""userId"": " + userProfileId + @"
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
                                ""_id"": ""$requests.documents.files.id""  ,
                                ""loanApplicationId"":1
                                 
                            }
                             } "

));
            List<FileViewDto> fileViewDTO = new List<FileViewDto>();
            if (await asyncCursor.MoveNextAsync())
            {
                foreach (var current in asyncCursor.Current)
                {
                   
                    FileViewDto query = BsonSerializer.Deserialize<FileViewDto>(current.ToJson());

                    
                    fileViewDTO.Add(new FileViewDto
                    {
                        id = query.id,
                        loanApplicationId = query.loanApplicationId
                    });
                }
            }


            return fileViewDTO;
        }*/

    }
}