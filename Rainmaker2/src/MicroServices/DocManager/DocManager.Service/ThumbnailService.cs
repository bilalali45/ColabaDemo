using DocManager.Model;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocManager.Service
{
    public class ThumbnailService : IThumbnailService
    {
        private readonly IMongoService mongoService;
        public ThumbnailService(IMongoService mongoService)
        {
            this.mongoService = mongoService;
        }
        public async Task<SaveWorkbenchDocument> SaveWorkbenchDocument(string id, string fileId, int tenantId, string serverName, string mcuName, int size, string contentType,
                            int userProfileId, string userName, string encryptionAlgo, string encryptionKey, string salt)
        {
            string oldFile = await IsExistWorkbenchFile(id, fileId, tenantId);
            if (!string.IsNullOrEmpty(oldFile))
            {
                IMongoCollection<Request> collection = mongoService.db.GetCollection<Request>("Request");
                UpdateResult result = await collection.UpdateOneAsync(new BsonDocument()
                {
                    { "_id", BsonObjectId.Create(id) },
                    { "tenantId", tenantId}

                }, new BsonDocument()
                {
                    { "$set", new BsonDocument()
                        {
                          { "workbench.$[workbenchs].serverName",serverName },
                          { "workbench.$[workbenchs].salt",salt },
                          { "workbench.$[workbenchs].mcuName",mcuName },
                        { "workbench.$[workbenchs].size",size },
                        { "workbench.$[workbenchs].encryptionKey", encryptionKey},
                        { "workbench.$[workbenchs].encryptionAlgorithm", encryptionAlgo},
                        { "workbench.$[workbenchs].contentType", contentType},
                        { "workbench.$[workbenchs].userId", userProfileId},
                        { "workbench.$[workbenchs].userName", userName},
                        { "workbench.$[workbenchs].isMcuVisible", true},
                        { "workbench.$[workbenchs].byteProStatus", ByteProStatus.NotSynchronized},
                        { "workbench.$[workbenchs].fileModifiedOn", DateTime.UtcNow}
                        }
                    }
                }, new UpdateOptions()
                {
                    ArrayFilters = new List<ArrayFilterDefinition>()
                    {
                        new JsonArrayFilterDefinition< Request>("{ \"workbenchs.id\": "+new ObjectId(fileId).ToJson()+"}")
                    }
                });
                if (result.ModifiedCount == 1)
                {
                    return new SaveWorkbenchDocument() { fileId = fileId, oldFile = oldFile };
                }
            }
            else
            {
                RequestFile requestFile = new RequestFile()
                {
                    annotations = null,
                    byteProStatus = ByteProStatus.NotSynchronized,
                    clientName = "",
                    contentType = contentType,
                    encryptionAlgorithm = encryptionAlgo,
                    encryptionKey = encryptionKey,
                    fileModifiedOn = DateTime.UtcNow,
                    fileUploadedOn = DateTime.UtcNow,
                    id = ObjectId.GenerateNewId().ToString(),
                    isMcuVisible = true,
                    isRead = false,
                    mcuName = mcuName,
                    order = 0,
                    serverName = serverName,
                    size = size,
                    status = FileStatus.SubmittedToMcu,
                    userId = userProfileId,
                    userName = userName,
                    salt=salt
                };
                IMongoCollection<Request> collectionInsert = mongoService.db.GetCollection<Request>("Request");
                UpdateResult resultInsert = await collectionInsert.UpdateOneAsync(new BsonDocument()
                        {
                            { "_id",new ObjectId(id)}
                        }, new BsonDocument()
                        {
                            { "$push", new BsonDocument()
                                {
                                    { "workbench", requestFile.ToBsonDocument()  }
                                }
                            },
                        }
                        );
                if (resultInsert.ModifiedCount == 1)
                {
                    return new SaveWorkbenchDocument() { fileId = requestFile.id };
                }
            }
            return new SaveWorkbenchDocument() { };
        }

        private async Task<string> IsExistWorkbenchFile(string id, string fileId, int tenantId)
        {
            IMongoCollection<Request> collection = mongoService.db.GetCollection<Request>("Request");

            using var asyncCursor = collection.Aggregate(PipelineDefinition<Request, BsonDocument>.Create(
                @"{""$match"": {

                  ""_id"": " + new ObjectId(id).ToJson() + @",
                  ""tenantId"": " + tenantId + @"
                            }
                        }", @"{
                            ""$unwind"": ""$workbench""
                        }",
                        @"{
                            ""$match"": {
                                ""workbench.id"": " + new ObjectId(fileId).ToJson() + @"
                            }
                        }",
                        @"{
                            ""$project"": {
                                ""_id"": 0,
                               ""files"": ""$workbench"",
                            }
                        }"
                ));

            while (await asyncCursor.MoveNextAsync())
            {
                BsonDocument current = asyncCursor.Current?.FirstOrDefault();
                if (current != null)
                {
                    RequestFile query = BsonSerializer.Deserialize<RequestFile>((BsonDocument)current.GetValue("files"));
                    return query.serverName;
                }
            }
            return null;
        }
        public async Task<SaveWorkbenchDocument> SaveTrashDocument(string id, string fileId, int tenantId, string serverName, string mcuName, int size, string contentType,
                          int userProfileId, string userName, string encryptionAlgo, string encryptionKey, string salt)
        {
            string oldFile = await IsExistTrashFile(id, fileId, tenantId);
            if (!string.IsNullOrEmpty(oldFile))
            {
                IMongoCollection<Request> collection = mongoService.db.GetCollection<Request>("Request");
                UpdateResult result = await collection.UpdateOneAsync(new BsonDocument()
                {
                    { "_id", BsonObjectId.Create(id) },
                    { "tenantId", tenantId}

                }, new BsonDocument()
                {
                    { "$set", new BsonDocument()
                        {
                            { "trash.$[trashs].serverName",serverName },
                            { "trash.$[trashs].salt",salt },
                            { "trash.$[trashs].mcuName",mcuName },
                        { "trash.$[trashs].size",size },
                        { "trash.$[trashs].encryptionKey", encryptionKey},
                        { "trash.$[trashs].encryptionAlgorithm", encryptionAlgo},
                        { "trash.$[trashs].contentType", contentType},
                        { "trash.$[trashs].userId", userProfileId},
                        { "trash.$[trashs].userName", userName},
                        { "trash.$[trashs].isMcuVisible", true},
                        { "trash.$[trashs].byteProStatus", ByteProStatus.NotSynchronized},
                        { "trash.$[trashs].fileModifiedOn", DateTime.UtcNow}
                        }
                    }
                }, new UpdateOptions()
                {
                    ArrayFilters = new List<ArrayFilterDefinition>()
                    {
                        new JsonArrayFilterDefinition< Request>("{ \"trashs.id\": "+new ObjectId(fileId).ToJson()+"}")
                    }
                });
                if (result.ModifiedCount == 1)
                {
                    return new SaveWorkbenchDocument() { fileId = fileId, oldFile = oldFile };
                }
            }
            else
            {
                RequestFile requestFile = new RequestFile()
                {
                    annotations = null,
                    byteProStatus = ByteProStatus.NotSynchronized,
                    clientName = "",
                    contentType = contentType,
                    encryptionAlgorithm = encryptionAlgo,
                    encryptionKey = encryptionKey,
                    fileModifiedOn = DateTime.UtcNow,
                    fileUploadedOn = DateTime.UtcNow,
                    id = ObjectId.GenerateNewId().ToString(),
                    isMcuVisible = true,
                    isRead = false,
                    mcuName = mcuName,
                    order = 0,
                    serverName = serverName,
                    size = size,
                    status = FileStatus.SubmittedToMcu,
                    userId = userProfileId,
                    userName = userName,
                    salt=salt
                };
                IMongoCollection<Request> collectionInsert = mongoService.db.GetCollection<Request>("Request");
                UpdateResult resultInsert = await collectionInsert.UpdateOneAsync(new BsonDocument()
                        {
                            { "_id",new ObjectId(id)}
                        }, new BsonDocument()
                        {
                            { "$push", new BsonDocument()
                                {
                                    { "trash", requestFile.ToBsonDocument()  }
                                }
                            },
                        }
                        );
                if (resultInsert.ModifiedCount == 1)
                {
                    return new SaveWorkbenchDocument() { fileId = requestFile.id };
                }
            }
            return new SaveWorkbenchDocument() { };
        }
        private async Task<string> IsExistTrashFile(string id, string fileId, int tenantId)
        {
            IMongoCollection<Request> collection = mongoService.db.GetCollection<Request>("Request");

            using var asyncCursor = collection.Aggregate(PipelineDefinition<Request, BsonDocument>.Create(
                @"{""$match"": {

                  ""_id"": " + new ObjectId(id).ToJson() + @",
                  ""tenantId"": " + tenantId + @"
                            }
                        }", @"{
                            ""$unwind"": ""$trash""
                        }",
                        @"{
                            ""$match"": {
                                ""trash.id"": " + new ObjectId(fileId).ToJson() + @"
                            }
                        }",
                        @"{
                            ""$project"": {
                                ""_id"": 0,
                               ""files"": ""$trash"",
                            }
                        }"
                ));

            while (await asyncCursor.MoveNextAsync())
            {
                BsonDocument current = asyncCursor.Current?.FirstOrDefault();
                if (current != null)
                {
                    RequestFile query = BsonSerializer.Deserialize<RequestFile>((BsonDocument)current.GetValue("files"));
                    return query.serverName;
                }
                
            }
            return null;
        }



        public async Task<SaveWorkbenchDocument> SaveCategoryDocument(string id, string requestId, string docId, string fileId, int tenantId, string serverName, string mcuName, int size, string contentType,
                         int userProfileId, string userName, string encryptionAlgo, string encryptionKey, string salt)
        {

            if (await IsExistfile(id, requestId, docId, fileId, tenantId))
            {
                var file = await PushFile(id, requestId, docId, fileId, tenantId, mcuName, contentType
                                , encryptionAlgo, encryptionKey, serverName, size, userProfileId, userName,salt);
                if (!string.IsNullOrEmpty(file))
                {
                    return new SaveWorkbenchDocument() { fileId = file };
                }
            }
            else
            {
                string oldFile = await IsExistmcuFile(id, requestId, docId, fileId, tenantId);
                if (!string.IsNullOrEmpty(oldFile))
                {
                    var updatemcuFile = await UpdatemcuFile(id, requestId, docId, fileId, tenantId, mcuName, contentType
                    , encryptionAlgo, encryptionKey, serverName, size, userProfileId, userName,salt);
                    if (updatemcuFile)
                    {
                        return new SaveWorkbenchDocument() { oldFile = oldFile, fileId = fileId };
                    }
                }
                else 
                {
                    var updatemcuFile = await PushmcuFile(id, requestId, docId, fileId, tenantId, mcuName, contentType
                    , encryptionAlgo, encryptionKey, serverName, size, userProfileId, userName,salt);
                    if (!string.IsNullOrEmpty(updatemcuFile))
                    {
                        return new SaveWorkbenchDocument() { fileId = updatemcuFile };
                    }
                }
            }
            return new SaveWorkbenchDocument();
        }


        private async Task<bool> IsExistfile(string id, string requestId, string docId, string fileId, int tenantId)
        {
            IMongoCollection<Request> collection = mongoService.db.GetCollection<Request>("Request");

            using var asyncCursor = collection.Aggregate(PipelineDefinition<Request, BsonDocument>.Create(
                @"{""$match"": {

                  ""_id"": " + new ObjectId(id).ToJson() + @",
                  ""tenantId"": " + tenantId + @"
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
                                ""requests.documents.files.id"": " + new ObjectId(fileId).ToJson() + @"
                            }
                        }",
                        @"{
                            ""$project"": {
                                ""_id"": 0,
                               ""files"": ""$requests.documents.files"",
                            }
                        }"
                ));
            while (await asyncCursor.MoveNextAsync())
            {
                BsonDocument current = asyncCursor.Current?.FirstOrDefault();
                if (current != null)
                {
                    return true;
                }
                
            }
            return false;
        }
        private async Task<string> PushFile(string id, string requestId, string docId, string fileId, int tenantId, string fileName, string contentType
            , string encryptionAlgo, string encryptionKey, string serverName, int size, int userProfileId, string userName, string salt)
        {

            IMongoCollection<Request> collectionUpdate = mongoService.db.GetCollection<Request>("Request");
            UpdateResult result = await collectionUpdate.UpdateOneAsync(new BsonDocument()
                        {
                            { "_id", BsonObjectId.Create(id) },
                            { "tenantId", tenantId}

                        }, new BsonDocument()
                        {
                            { "$set", new BsonDocument()
                                {
                                { "requests.$[request].documents.$[document].files.$[file].isMcuVisible", false}

                                }
                            }
                        }, new UpdateOptions()
                        {
                            ArrayFilters = new List<ArrayFilterDefinition>()
                            {
                                new JsonArrayFilterDefinition< Request>("{ \"request.id\": "+new ObjectId( requestId).ToJson()+"}"),
                                new JsonArrayFilterDefinition< Request>("{ \"document.id\": "+new ObjectId( docId).ToJson()+"}"),
                                new JsonArrayFilterDefinition< Request>("{ \"file.id\": "+new ObjectId( fileId).ToJson()+"}")
                            }
                        });
            if (result.ModifiedCount == 1)
            {
                RequestFile requestFile = new RequestFile()
                {
                    annotations = null,
                    byteProStatus = ByteProStatus.NotSynchronized,
                    clientName = "",
                    contentType = contentType,
                    encryptionAlgorithm = encryptionAlgo,
                    encryptionKey = encryptionKey,
                    fileModifiedOn = DateTime.UtcNow,
                    fileUploadedOn = DateTime.UtcNow,
                    id = ObjectId.GenerateNewId().ToString(),
                    isMcuVisible = true,
                    isRead = false,
                    mcuName = fileName,
                    order = 0,
                    serverName = serverName,
                    size = size,
                    status = FileStatus.SubmittedToMcu,
                    userId = userProfileId,
                    userName = userName,
                    salt=salt
                };
                IMongoCollection<Request> collectionInsert = mongoService.db.GetCollection<Request>("Request");
                UpdateResult resultInsert = await collectionInsert.UpdateOneAsync(new BsonDocument()
                        {
                            { "_id",new ObjectId(id)}
                        }, new BsonDocument()
                        {
                            { "$push", new BsonDocument()
                                {
                                    { "requests.$[request].documents.$[document].mcuFiles", requestFile.ToBsonDocument()  }
                                }
                            },
                        }, new UpdateOptions()
                        {
                            ArrayFilters = new List<ArrayFilterDefinition>()   {
                                new JsonArrayFilterDefinition< Request>("{ \"request.id\": "+new ObjectId(  requestId).ToJson()+"}"),
                                new JsonArrayFilterDefinition< Request>("{ \"document.id\": "+new ObjectId(  docId).ToJson()+"}")
                              }
                        });
                if(resultInsert.ModifiedCount==1)
                    return requestFile.id;
            }
            return null;
        }
        private async Task<string> IsExistmcuFile(string id, string requestId, string docId, string fileId, int tenantId)
        {
            IMongoCollection<Request> collection = mongoService.db.GetCollection<Request>("Request");

            using var asyncCursor = collection.Aggregate(PipelineDefinition<Request, BsonDocument>.Create(
                @"{""$match"": {

                  ""_id"": " + new ObjectId(id).ToJson() + @",
                  ""tenantId"": " + tenantId + @"
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
                                ""requests.documents.mcuFiles.id"": " + new ObjectId(fileId).ToJson() + @"
                            }
                        }",
                        @"{
                            ""$project"": {
                                ""_id"": 0,
                               ""files"": ""$requests.documents.mcuFiles"",
                            }
                        }"
                ));
            while (await asyncCursor.MoveNextAsync())
            {
                BsonDocument current = asyncCursor.Current?.FirstOrDefault();
                if (current != null)
                {
                    RequestFile query = BsonSerializer.Deserialize<RequestFile>((BsonDocument)current.GetValue("files"));
                    return query.serverName;
                }
            }
            return null;
        }
        private async Task<bool> UpdatemcuFile(string id, string requestId, string docId, string fileId, int tenantId, string fileName, string contentType
              , string encryptionAlgo, string encryptionKey, string serverName, int size, int userProfileId, string userName, string salt)
        {

            IMongoCollection<Request> collectionUpdate = mongoService.db.GetCollection<Request>("Request");
            UpdateResult result = await collectionUpdate.UpdateOneAsync(new BsonDocument()
                        {
                            { "_id", BsonObjectId.Create(id) },
                            { "tenantId", tenantId}

                        }, new BsonDocument()
                        {
                            { "$set", new BsonDocument(){

                                    { "requests.$[request].documents.$[document].mcuFiles.$[mcuFile].serverName",serverName },
                                    { "requests.$[request].documents.$[document].mcuFiles.$[mcuFile].salt",salt },
                                    { "requests.$[request].documents.$[document].mcuFiles.$[mcuFile].mcuName",fileName },
                                    { "requests.$[request].documents.$[document].mcuFiles.$[mcuFile].size",size },
                                    { "requests.$[request].documents.$[document].mcuFiles.$[mcuFile].encryptionKey", encryptionKey},
                                    { "requests.$[request].documents.$[document].mcuFiles.$[mcuFile].encryptionAlgorithm", encryptionAlgo},
                                    { "requests.$[request].documents.$[document].mcuFiles.$[mcuFile].contentType", contentType},
                                    { "requests.$[request].documents.$[document].mcuFiles.$[mcuFile].userId", userProfileId},
                                    { "requests.$[request].documents.$[document].mcuFiles.$[mcuFile].userName", userName},
                                    { "requests.$[request].documents.$[document].mcuFiles.$[mcuFile].isMcuVisible", true},
                                    { "requests.$[request].documents.$[document].mcuFiles.$[mcuFile].byteProStatus", ByteProStatus.NotSynchronized},
                                    { "requests.$[request].documents.$[document].mcuFiles.$[mcuFile].fileModifiedOn", DateTime.UtcNow}

                        }
                            } }, new UpdateOptions()
                            {
                                ArrayFilters = new List<ArrayFilterDefinition>()
                            {
                                new JsonArrayFilterDefinition< Request>("{ \"request.id\": "+new ObjectId( requestId).ToJson()+"}"),
                                new JsonArrayFilterDefinition< Request>("{ \"document.id\": "+new ObjectId( docId).ToJson()+"}"),
                                new JsonArrayFilterDefinition< Request>("{ \"mcuFile.id\": "+new ObjectId( fileId).ToJson()+"}")
                            }
                            });
            if (result.ModifiedCount == 1)
            {
                return true;
            }
            return false;
        }

        private async Task<string> PushmcuFile(string id, string requestId, string docId, string fileId, int tenantId, string fileName, string contentType
        , string encryptionAlgo, string encryptionKey, string serverName, int size, int userProfileId, string userName, string salt)
        {
            RequestFile requestFile = new RequestFile()
            {
                annotations = null,
                byteProStatus = ByteProStatus.NotSynchronized,
                clientName = "",
                contentType = contentType,
                encryptionAlgorithm = encryptionAlgo,
                encryptionKey = encryptionKey,
                fileModifiedOn = DateTime.UtcNow,
                fileUploadedOn = DateTime.UtcNow,
                id = ObjectId.GenerateNewId().ToString(),
                isMcuVisible = true,
                isRead = false,
                mcuName = fileName,
                order = 0,
                serverName = serverName,
                size = size,
                status = FileStatus.SubmittedToMcu,
                userId = userProfileId,
                userName = userName,
                salt=salt
            };
            IMongoCollection<Request> collectionInsert = mongoService.db.GetCollection<Request>("Request");
            UpdateResult resultInsert = await collectionInsert.UpdateOneAsync(new BsonDocument()
                    {
                        { "_id",new ObjectId(id)}
                    }, new BsonDocument()
                    {
                        { "$push", new BsonDocument()
                            {
                                { "requests.$[request].documents.$[document].mcuFiles", requestFile.ToBsonDocument()  }
                            }
                        },
                    }, new UpdateOptions()
                    {
                        ArrayFilters = new List<ArrayFilterDefinition>()   {
                            new JsonArrayFilterDefinition< Request>("{ \"request.id\": "+new ObjectId(  requestId).ToJson()+"}"),
                            new JsonArrayFilterDefinition< Request>("{ \"document.id\": "+new ObjectId(  docId).ToJson()+"}")
                            }
                    });
            if(resultInsert.ModifiedCount==1)
                return requestFile.id;
            return null;
        }
    }
}
