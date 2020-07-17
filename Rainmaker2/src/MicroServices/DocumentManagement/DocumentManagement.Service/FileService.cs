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

        public FileService(IMongoService mongoService, IActivityLogService activityLogService)
        {
            this.mongoService = mongoService;
            this.activityLogService = activityLogService;
        }
        public async Task<bool> Rename(FileRenameModel model,int userProfileId)
        {
            IMongoCollection<Request> collection = mongoService.db.GetCollection<Request>("Request");

            UpdateResult result = await collection.UpdateOneAsync(new BsonDocument()
            {
                { "_id", BsonObjectId.Create(model.id) },
                { "tenantId", model.tenantId},
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
                    new JsonArrayFilterDefinition<Request>("{ \"request.id\": "+new ObjectId(model.requestId).ToJson()+"}"),
                    new JsonArrayFilterDefinition<Request>("{ \"document.id\": "+new ObjectId(model.docId).ToJson()+"}"),
                    new JsonArrayFilterDefinition<Request>("{ \"file.id\": "+new ObjectId(model.fileId).ToJson()+"}")
                }
            });

            return result.ModifiedCount == 1;
        }
        public async Task<bool> Done(DoneModel model, int userProfileId)
        {
            IMongoCollection<Request> collection = mongoService.db.GetCollection<Request>("Request");
            UpdateResult result = await collection.UpdateOneAsync(new BsonDocument()
            {
                { "_id", BsonObjectId.Create(model.id) },
                { "tenantId", model.tenantId},
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
                    new JsonArrayFilterDefinition<Request>("{ \"request.id\": "+new ObjectId(model.requestId).ToJson()+"}"),
                    new JsonArrayFilterDefinition<Request>("{ \"document.id\": "+new ObjectId(model.docId).ToJson()+"}"),
                }
            });

            if (result.ModifiedCount == 1)
            {
                string activityLogId = await activityLogService.GetActivityLogId(model.id, model.requestId, model.docId);

                await activityLogService.InsertLog(activityLogId, string.Format(ActivityStatus.StatusChanged, DocumentStatus.PendingReview));
            }

            return result.ModifiedCount == 1;
        }

        public async Task Order(FileOrderModel model, int userProfileId)
        {
            IMongoCollection<Request> collection = mongoService.db.GetCollection<Request>("Request");

            foreach (var item in model.files)
            {
                UpdateResult result = await collection.UpdateOneAsync(new BsonDocument()
                {
                    { "_id", BsonObjectId.Create(model.id) },
                    { "tenantId", model.tenantId},
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
                        new JsonArrayFilterDefinition<Request>("{ \"request.id\": "+new ObjectId(model.requestId).ToJson()+"}"),
                        new JsonArrayFilterDefinition<Request>("{ \"document.id\": "+new ObjectId(model.docId).ToJson()+"}"),
                        new JsonArrayFilterDefinition<Request>("{ \"file.clientName\": \""+item.fileName.Replace("\"","\\\"")+"\"}")
                    }
                });
            }
        }

        public async Task<bool> Submit(string contentType,string id, string requestId, string docId, string clientName, string serverName, int size, string encryptionKey, string encryptionAlgorithm, int tenantId, int userProfileId)
        {
            bool isStarted = false;

            IMongoCollection<Request> collectionRequst = mongoService.db.GetCollection<Request>("Request");

            using var asyncCursor = collectionRequst.Aggregate(
                PipelineDefinition<Request, BsonDocument>.Create(
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

            IMongoCollection<Request> collection = mongoService.db.GetCollection<Request>("Request");

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
                                                                    },
                                                                    new BsonDocument()
                                                                    {
                                                                        { 
                                                                            "$or",new BsonArray()
                                                                            {
                                                                                new BsonDocument()
                                                                                {
                                                                                    { "status", DocumentStatus.BorrowerTodo}
                                                                                },
                                                                                new BsonDocument()
                                                                                {
                                                                                    { "status", DocumentStatus.Started}
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
                        }
                    } 
                }
            }, new BsonDocument()
            {
                { "$push", new BsonDocument()
                    {
                        { "requests.$[request].documents.$[document].files", new BsonDocument() { { "id", ObjectId.GenerateNewId() }, { "clientName", clientName } , { "serverName", serverName }, { "fileUploadedOn", BsonDateTime.Create(DateTime.UtcNow) }, { "size", size }, { "encryptionKey", encryptionKey }, { "encryptionAlgorithm", encryptionAlgorithm }, { "order" , 0 }, { "mcuName", BsonString.Empty }, { "contentType", contentType }, { "status", FileStatus.SubmittedToMcu },{ "byteProStatus", ByteProStatus.NotSynchronized} }   }
                    }
                },
                { "$set", new BsonDocument()
                    {
                        { "requests.$[request].documents.$[document].status", DocumentStatus.Started}

                    }
                }
            }, new UpdateOptions()
            {
                ArrayFilters = new List<ArrayFilterDefinition>()
                {
                    new JsonArrayFilterDefinition<Request>("{ \"request.id\": "+new ObjectId(requestId).ToJson()+"}"),
                    new JsonArrayFilterDefinition<Request>("{ \"document.id\": "+new ObjectId(docId).ToJson()+"}")
                }
            });

            if (result.ModifiedCount == 1)
            {
                string activityLogId = await activityLogService.GetActivityLogId(id, requestId, docId);

                await activityLogService.InsertLog(activityLogId, string.Format(ActivityStatus.FileSubmitted, clientName));
            }
            if (result.ModifiedCount == 1 && isStarted == false)
            {
                string activityLogId = await activityLogService.GetActivityLogId(id, requestId, docId);

                await activityLogService.InsertLog(activityLogId, string.Format(ActivityStatus.StatusChanged, DocumentStatus.Started));
            }

            return result.ModifiedCount == 1;
        }

        public async Task<FileViewDTO> View(FileViewModel model, int userProfileId, string ipAddress)
        {
            IMongoCollection<Request> collection = mongoService.db.GetCollection<Request>("Request");

            using var asyncCursor = collection.Aggregate(PipelineDefinition<Request, BsonDocument>.Create(
              @"{""$match"": {

                  ""_id"": " + new ObjectId(model.id).ToJson() + @" ,
                  ""tenantId"": " + model.tenantId + @",
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
                                ""contentType"": ""$requests.documents.files.contentType""
                            }
                             } "

));


            await asyncCursor.MoveNextAsync();
            FileViewDTO fileViewDTO = BsonSerializer.Deserialize<FileViewDTO>(asyncCursor.Current.FirstOrDefault());

            IMongoCollection<ViewLog> viewLogCollection = mongoService.db.GetCollection<ViewLog>("ViewLog");

            ViewLog viewLog = new ViewLog() { userProfileId = userProfileId, createdOn = DateTime.UtcNow, ipAddress = ipAddress, loanApplicationId = model.id, requestId = model.requestId, documentId = model.docId, fileId = model.fileId };
            await viewLogCollection.InsertOneAsync(viewLog);

            return fileViewDTO;
        }

    }
}