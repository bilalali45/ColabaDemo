using DocManager.Model;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DocManager.Service
{
    public class DocumentService : IDocumentService
    {
        private readonly IMongoService mongoService;
        public DocumentService(IMongoService mongoService)
        {
            this.mongoService = mongoService;
        }
        /*
        public async Task<bool> Delete(DeleteModel deleteModel, int tenantId)
        {
            IMongoCollection<Request> collection = mongoService.db.GetCollection<Request>("Request");
            using var asyncCursor = collection.Aggregate(PipelineDefinition<Request, BsonDocument>.Create(
             @"{""$match"": {

                  ""_id"": " + new ObjectId(deleteModel.id).ToJson() + @",
                  ""tenantId"": " + tenantId + @"
                            }
                        }",
                     @"{
                            ""$unwind"": ""$requests""
                        }",
                     @"{
                            ""$match"": {
                                ""requests.id"": " + new ObjectId(deleteModel.requestId).ToJson() + @"
                            }
                        }",
                     @"{
                            ""$unwind"": ""$requests.documents""
                        }",
                      @"{
                            ""$match"": {
                                ""requests.documents.id"": " + new ObjectId(deleteModel.docId).ToJson() + @"
                            }
                        }",

                     @"{
                            ""$project"": {
                                ""_id"": 0,
                                ""status"": ""$requests.documents.status""
                            }
                        }"
             ));
            while (await asyncCursor.MoveNextAsync())
            {
                foreach (var current in asyncCursor.Current)
                {
                    var query = BsonSerializer.Deserialize<DocumentStatusQuery>((BsonDocument)current);

                    if (query.status == DocumentStatus.BorrowerTodo)
                    {
                        UpdateResult statusresult = await collection.UpdateOneAsync(new BsonDocument()
                        {
                            { "_id", BsonObjectId.Create(deleteModel.id) },
                            { "tenantId", tenantId}
                        }, new BsonDocument()
                        {
                            { "$set", new BsonDocument()
                                {
                                    { "requests.$[request].documents.$[document].status", DocumentStatus.Deleted}
                                }
                            }
                        }, new UpdateOptions()
                        {
                            ArrayFilters = new List<ArrayFilterDefinition>()
                            {
                                new JsonArrayFilterDefinition<Request>("{ \"request.id\": "+new ObjectId(deleteModel.requestId).ToJson()+"}"),
                                new JsonArrayFilterDefinition<Request>("{ \"document.id\": "+new ObjectId(deleteModel.docId).ToJson()+"}")
                            }
                        });
                        return statusresult.ModifiedCount == 1;

                    }
                    else
                    {
                        UpdateResult statusresult = await collection.UpdateOneAsync(new BsonDocument()
                        {
                            { "_id", BsonObjectId.Create(deleteModel.id) },
                            { "tenantId", tenantId}
                        }, new BsonDocument()
                        {
                            { "$set", new BsonDocument()
                                {
                                    { "requests.$[request].documents.$[document].isMcuVisible", false}
                                }
                            }
                        }, new UpdateOptions()
                        {
                            ArrayFilters = new List<ArrayFilterDefinition>()
                            {
                                new JsonArrayFilterDefinition<Request>("{ \"request.id\": "+new ObjectId(deleteModel.requestId).ToJson()+"}"),
                                new JsonArrayFilterDefinition<Request>("{ \"document.id\": "+new ObjectId(deleteModel.docId).ToJson()+"}")
                            }
                        });
                        return statusresult.ModifiedCount == 1;

                    }
                }

            }

            return false;
        }*/
        public async Task<bool> MoveFromCategoryToTrash(MoveFromCategoryToTrash moveFromCategoryToTrash, int tenantId)
        {
            IMongoCollection<Request> collection = mongoService.db.GetCollection<Request>("Request");
            bool IsFileExist = false;
            using var asyncCursor = collection.Aggregate(PipelineDefinition<Request, BsonDocument>.Create(
                @"{""$match"": {

                  ""_id"": " + new ObjectId(moveFromCategoryToTrash.id).ToJson() + @",
                  ""tenantId"": " + tenantId + @"
                            }
                        }",
                        @"{
                            ""$unwind"": ""$requests""
                        }",
                        @"{
                            ""$match"": {
                                ""requests.id"": " + new ObjectId(moveFromCategoryToTrash.fromRequestId).ToJson() + @"
                            }
                        }",
                        @"{
                            ""$unwind"": ""$requests.documents""
                        }",
                         @"{
                            ""$match"": {
                                ""requests.documents.id"": " + new ObjectId(moveFromCategoryToTrash.fromDocId).ToJson() + @"
                            }
                        }",
                         @"{
                            ""$unwind"": ""$requests.documents.files""
                        }",
                         @"{
                            ""$match"": {
                                ""requests.documents.files.id"": " + new ObjectId(moveFromCategoryToTrash.fromFileId).ToJson() + @"
                            }
                        }",
                        @"{
                            ""$project"": {
                                ""_id"": 0,
                               ""files"": ""$requests.documents.files""
                            }
                        }"
                ));
            while (await asyncCursor.MoveNextAsync())
            {

                foreach (var current in asyncCursor.Current)
                {
                    IsFileExist = true;
                    RequestFile query = BsonSerializer.Deserialize<RequestFile>((BsonDocument)current.GetValue("files"));
                    query.mcuName = !string.IsNullOrEmpty(query.mcuName) ? query.mcuName : query.clientName;
                    query.id = ObjectId.GenerateNewId().ToString();
                    bool deleted = await DeleteCategoryFile(moveFromCategoryToTrash.id, tenantId, moveFromCategoryToTrash.fromRequestId, moveFromCategoryToTrash.fromDocId, moveFromCategoryToTrash.fromFileId);
                    if (deleted)
                    {
                        IMongoCollection<Request> collectionInsertRequest = mongoService.db.GetCollection<Request>("Request");

                        UpdateResult result = await collectionInsertRequest.UpdateOneAsync(new BsonDocument()
                        {
                            { "_id",new ObjectId(moveFromCategoryToTrash.id)}
                        }, new BsonDocument()
                        {
                            { "$push", new BsonDocument()
                                {
                                    { "trash", query.ToBsonDocument()  }
                                }
                            },
                        }
                             );
                        return result.ModifiedCount == 1;
                    }

                }

            }
            if (!IsFileExist)//mcuFile
            {
                using var asyncCursor1 = collection.Aggregate(PipelineDefinition<Request, BsonDocument>.Create(
                               @"{""$match"": {

                  ""_id"": " + new ObjectId(moveFromCategoryToTrash.id).ToJson() + @",
                  ""tenantId"": " + tenantId + @"
                            }
                        }",
                                       @"{
                            ""$unwind"": ""$requests""
                        }",
                                       @"{
                            ""$match"": {
                                ""requests.id"": " + new ObjectId(moveFromCategoryToTrash.fromRequestId).ToJson() + @"
                            }
                        }",
                                       @"{
                            ""$unwind"": ""$requests.documents""
                        }",
                                        @"{
                            ""$match"": {
                                ""requests.documents.id"": " + new ObjectId(moveFromCategoryToTrash.fromDocId).ToJson() + @"
                            }
                        }",
                                        @"{
                            ""$unwind"": ""$requests.documents.mcuFiles""
                        }",
                                        @"{
                            ""$match"": {
                                ""requests.documents.mcuFiles.id"": " + new ObjectId(moveFromCategoryToTrash.fromFileId).ToJson() + @"
                            }
                        }",
                                       @"{
                            ""$project"": {
                                ""_id"": 0,
                               ""files"": ""$requests.documents.mcuFiles"",
                            }
                        }"
                               ));
                while (await asyncCursor1.MoveNextAsync())
                {

                    foreach (var current in asyncCursor1.Current)
                    {
                        RequestFile query = BsonSerializer.Deserialize<RequestFile>((BsonDocument)current.GetValue("files"));
                        query.mcuName = !string.IsNullOrEmpty(query.mcuName) ? query.mcuName : query.clientName;
                        bool deleted = await DeleteCategoryMcuFile(moveFromCategoryToTrash.id, tenantId, moveFromCategoryToTrash.fromRequestId, moveFromCategoryToTrash.fromDocId, moveFromCategoryToTrash.fromFileId);
                        if (deleted)
                        {
                            IMongoCollection<Request> collectionInsertRequest = mongoService.db.GetCollection<Request>("Request");

                            var result = await collectionInsertRequest.UpdateOneAsync(new BsonDocument()
                        {
                            { "_id",new ObjectId(moveFromCategoryToTrash.id)}
                        }, new BsonDocument()
                        {
                            { "$push", new BsonDocument()
                                {
                                    { "trash", query.ToBsonDocument()  }
                                }
                            },
                        }
                               );
                            return result.ModifiedCount == 1;
                        }

                    }

                }
            }
            return false;
        }
        public async Task<bool> MoveFromCategoryToWorkBench(MoveFromCategoryToWorkBench moveFromCategoryToworkbench, int tenantId)
        {
            IMongoCollection<Request> collection = mongoService.db.GetCollection<Request>("Request");
            bool IsFileExist = false;
            using var asyncCursor = collection.Aggregate(PipelineDefinition<Request, BsonDocument>.Create(
                @"{""$match"": {

                  ""_id"": " + new ObjectId(moveFromCategoryToworkbench.id).ToJson() + @",
                  ""tenantId"": " + tenantId + @"
                            }
                        }",
                        @"{
                            ""$unwind"": ""$requests""
                        }",
                        @"{
                            ""$match"": {
                                ""requests.id"": " + new ObjectId(moveFromCategoryToworkbench.fromRequestId).ToJson() + @"
                            }
                        }",
                        @"{
                            ""$unwind"": ""$requests.documents""
                        }",
                         @"{
                            ""$match"": {
                                ""requests.documents.id"": " + new ObjectId(moveFromCategoryToworkbench.fromDocId).ToJson() + @"
                            }
                        }",
                         @"{
                            ""$unwind"": ""$requests.documents.files""
                        }",
                         @"{
                            ""$match"": {
                                ""requests.documents.files.id"": " + new ObjectId(moveFromCategoryToworkbench.fromFileId).ToJson() + @"
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

                foreach (var current in asyncCursor.Current)
                {
                    IsFileExist = true;
                    RequestFile query = BsonSerializer.Deserialize<RequestFile>((BsonDocument)current.GetValue("files"));
                    query.id = ObjectId.GenerateNewId().ToString();
                    query.mcuName = !string.IsNullOrEmpty(query.mcuName) ? query.mcuName : query.clientName;
                    bool deleted = await DeleteCategoryFile(moveFromCategoryToworkbench.id, tenantId, moveFromCategoryToworkbench.fromRequestId, moveFromCategoryToworkbench.fromDocId, moveFromCategoryToworkbench.fromFileId);
                    if (deleted)
                    {
                        IMongoCollection<Request> collectionInsertRequest = mongoService.db.GetCollection<Request>("Request");

                        UpdateResult result = await collectionInsertRequest.UpdateOneAsync(new BsonDocument()
                        {
                            { "_id",new ObjectId(moveFromCategoryToworkbench.id)}
                        }, new BsonDocument()
                        {
                            { "$push", new BsonDocument()
                                {
                                    { "workbench", query.ToBsonDocument()  }
                                }
                            },
                        }
                             );
                        return result.ModifiedCount == 1;
                    }

                }

            }
            if (!IsFileExist)//mcuFile
            {
                using var asyncCursor1 = collection.Aggregate(PipelineDefinition<Request, BsonDocument>.Create(
                               @"{""$match"": {

                  ""_id"": " + new ObjectId(moveFromCategoryToworkbench.id).ToJson() + @",
                  ""tenantId"": " + tenantId + @"
                            }
                        }",
                                       @"{
                            ""$unwind"": ""$requests""
                        }",
                                       @"{
                            ""$match"": {
                                ""requests.id"": " + new ObjectId(moveFromCategoryToworkbench.fromRequestId).ToJson() + @"
                            }
                        }",
                                       @"{
                            ""$unwind"": ""$requests.documents""
                        }",
                                        @"{
                            ""$match"": {
                                ""requests.documents.id"": " + new ObjectId(moveFromCategoryToworkbench.fromDocId).ToJson() + @"
                            }
                        }",
                                        @"{
                            ""$unwind"": ""$requests.documents.mcuFiles""
                        }",
                                        @"{
                            ""$match"": {
                                ""requests.documents.mcuFiles.id"": " + new ObjectId(moveFromCategoryToworkbench.fromFileId).ToJson() + @"
                            }
                        }",
                                       @"{
                            ""$project"": {
                                ""_id"": 0,
                               ""files"": ""$requests.documents.mcuFiles"",
                            }
                        }"
                               ));
                while (await asyncCursor1.MoveNextAsync())
                {

                    foreach (var current in asyncCursor1.Current)
                    {
                        RequestFile query = BsonSerializer.Deserialize<RequestFile>((BsonDocument)current.GetValue("files"));
                        query.mcuName = !string.IsNullOrEmpty(query.mcuName) ? query.mcuName : query.clientName;
                        bool deleted = await DeleteCategoryMcuFile(moveFromCategoryToworkbench.id, tenantId, moveFromCategoryToworkbench.fromRequestId, moveFromCategoryToworkbench.fromDocId, moveFromCategoryToworkbench.fromFileId);
                        if (deleted)
                        {
                            IMongoCollection<Request> collectionInsertRequest = mongoService.db.GetCollection<Request>("Request");

                            var result = await collectionInsertRequest.UpdateOneAsync(new BsonDocument()
                        {
                            { "_id",new ObjectId(moveFromCategoryToworkbench.id)}
                        }, new BsonDocument()
                        {
                            { "$push", new BsonDocument()
                                {
                                    { "workbench", query.ToBsonDocument()  }
                                }
                            },
                        }
                               );
                            return result.ModifiedCount == 1;
                        }

                    }

                }
            }
            return false;
        }
        public async Task<bool> DeleteCategoryFile(string id, int tenantid, string fromRequestId, string fromDocId, string fromFileId)
        {
            IMongoCollection<Request> collection = mongoService.db.GetCollection<Request>("Request");
            UpdateResult result = await collection.UpdateOneAsync(new BsonDocument()
            {
                { "_id", BsonObjectId.Create(id) }
              ,  { "tenantId", tenantid}

            }
             , new BsonDocument()
            {
                 { "$set", new BsonDocument()
                    {
                        { "requests.$[request].documents.$[document].files.$[file].isMcuVisible", false}
                    }
                }

                //{ "$pull", new BsonDocument()
                //{

                //              { "requests.$[request].documents.$[document].files",new BsonDocument()
                //              {
                //                  { "id",BsonObjectId.Create(fromFileId)}
                //              }
                //         }
                //    }
                //}
            }, new UpdateOptions()
            {
                ArrayFilters = new List<ArrayFilterDefinition>()
                {
                    new JsonArrayFilterDefinition<Request>("{ \"request.id\": "+new ObjectId(fromRequestId).ToJson()+"}"),
                    new JsonArrayFilterDefinition<Request>("{ \"document.id\": "+new ObjectId(fromDocId).ToJson()+"}"),
                    new JsonArrayFilterDefinition<Request>("{ \"file.id\": "+new ObjectId(fromFileId).ToJson()+"}")
                }
            });
            return result.ModifiedCount == 1;
        }
        public async Task<bool> DeleteCategoryMcuFile(string id, int tenantid, string fromRequestId, string fromDocId, string fromFileId)
        {
            IMongoCollection<Request> collection = mongoService.db.GetCollection<Request>("Request");
            UpdateResult result = await collection.UpdateOneAsync(new BsonDocument()
            {
                { "_id", BsonObjectId.Create(id) }
              ,  { "tenantId", tenantid}

            }
             , new BsonDocument()
            {

                { "$pull", new BsonDocument()
                {

                              { "requests.$[request].documents.$[document].mcuFiles",new BsonDocument(){
                                  { "id",BsonObjectId.Create(fromFileId)}
                              }
                              }


                    }
                }
            }, new UpdateOptions()
            {
                ArrayFilters = new List<ArrayFilterDefinition>()
                {
                    new JsonArrayFilterDefinition<Request>("{ \"request.id\": "+new ObjectId(fromRequestId).ToJson()+"}"),
                    new JsonArrayFilterDefinition<Request>("{ \"document.id\": "+new ObjectId(fromDocId).ToJson()+"}")
                }
            });
            return result.ModifiedCount == 1;
        }
        public async Task<bool> MoveFromoneCategoryToAnotherCategory(MoveFromOneCategoryToAnotherCategory moveFromoneCategoryToAnotherCategory, int tenantId)
        {
            IMongoCollection<Request> collection = mongoService.db.GetCollection<Request>("Request");
            bool IsFileExist = false;
            using var asyncCursor = collection.Aggregate(PipelineDefinition<Request, BsonDocument>.Create(
                @"{""$match"": {

                  ""_id"": " + new ObjectId(moveFromoneCategoryToAnotherCategory.id).ToJson() + @",
                  ""tenantId"": " + tenantId + @"
                            }
                        }",
                        @"{
                            ""$unwind"": ""$requests""
                        }",
                        @"{
                            ""$match"": {
                                ""requests.id"": " + new ObjectId(moveFromoneCategoryToAnotherCategory.fromRequestId).ToJson() + @"
                            }
                        }",
                        @"{
                            ""$unwind"": ""$requests.documents""
                        }",
                         @"{
                            ""$match"": {
                                ""requests.documents.id"": " + new ObjectId(moveFromoneCategoryToAnotherCategory.fromDocId).ToJson() + @"
                            }
                        }",
                         @"{
                            ""$unwind"": ""$requests.documents.files""
                        }",
                         @"{
                            ""$match"": {
                                ""requests.documents.files.id"": " + new ObjectId(moveFromoneCategoryToAnotherCategory.fromFileId).ToJson() + @"
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

                foreach (var current in asyncCursor.Current)
                {
                    IsFileExist = true;
                    RequestFile query = BsonSerializer.Deserialize<RequestFile>((BsonDocument)current.GetValue("files"));
                    query.mcuName = !string.IsNullOrEmpty(query.mcuName) ? query.mcuName : query.clientName;
                    query.id = ObjectId.GenerateNewId().ToString();
                    bool deleted = await DeleteCategoryFile(moveFromoneCategoryToAnotherCategory.id, tenantId, moveFromoneCategoryToAnotherCategory.fromRequestId, moveFromoneCategoryToAnotherCategory.fromDocId, moveFromoneCategoryToAnotherCategory.fromFileId);
                    if (deleted)
                    {
                        IMongoCollection<Request> collectionInsertRequest = mongoService.db.GetCollection<Request>("Request");

                        UpdateResult result = await collection.UpdateOneAsync(new BsonDocument()
            {
                { "_id", BsonObjectId.Create(moveFromoneCategoryToAnotherCategory.id) },
                { "tenantId", tenantId}
            }, new BsonDocument()
            {

                { "$push", new BsonDocument()
                    {
                        { "requests.$[request].documents.$[document].mcuFiles",query.ToBsonDocument() }

                    }
                }
            }, new UpdateOptions()
            {
                ArrayFilters = new List<ArrayFilterDefinition>()
                {
                    new JsonArrayFilterDefinition<Request>("{ \"request.id\": "+new ObjectId(moveFromoneCategoryToAnotherCategory.toRequestId).ToJson()+"}"),
                    new JsonArrayFilterDefinition<Request>("{ \"document.id\": "+new ObjectId(moveFromoneCategoryToAnotherCategory.toDocId).ToJson()+"}")
                }
            });
                        return result.ModifiedCount == 1;
                    }

                }

            }
            if (!IsFileExist)//mcuFile
            {
                using var asyncCursor1 = collection.Aggregate(PipelineDefinition<Request, BsonDocument>.Create(
                               @"{""$match"": {

                  ""_id"": " + new ObjectId(moveFromoneCategoryToAnotherCategory.id).ToJson() + @",
                  ""tenantId"": " + tenantId + @"
                            }
                        }",
                                       @"{
                            ""$unwind"": ""$requests""
                        }",
                                       @"{
                            ""$match"": {
                                ""requests.id"": " + new ObjectId(moveFromoneCategoryToAnotherCategory.fromRequestId).ToJson() + @"
                            }
                        }",
                                       @"{
                            ""$unwind"": ""$requests.documents""
                        }",
                                        @"{
                            ""$match"": {
                                ""requests.documents.id"": " + new ObjectId(moveFromoneCategoryToAnotherCategory.fromDocId).ToJson() + @"
                            }
                        }",
                                        @"{
                            ""$unwind"": ""$requests.documents.mcuFiles""
                        }",
                                        @"{
                            ""$match"": {
                                ""requests.documents.mcuFiles.id"": " + new ObjectId(moveFromoneCategoryToAnotherCategory.fromFileId).ToJson() + @"
                            }
                        }",
                                       @"{
                            ""$project"": {
                                ""_id"": 0,
                               ""files"": ""$requests.documents.mcuFiles"",
                            }
                        }"
                               ));
                while (await asyncCursor1.MoveNextAsync())
                {

                    foreach (var current in asyncCursor1.Current)
                    {
                        RequestFile query = BsonSerializer.Deserialize<RequestFile>((BsonDocument)current.GetValue("files"));
                        bool deleted = await DeleteCategoryMcuFile(moveFromoneCategoryToAnotherCategory.id, tenantId, moveFromoneCategoryToAnotherCategory.fromRequestId, moveFromoneCategoryToAnotherCategory.fromDocId, moveFromoneCategoryToAnotherCategory.fromFileId);
                        if (deleted)
                        {
                            IMongoCollection<Request> collectionInsertRequest = mongoService.db.GetCollection<Request>("Request");

                            UpdateResult result = await collection.UpdateOneAsync(new BsonDocument()
            {
                { "_id", BsonObjectId.Create(moveFromoneCategoryToAnotherCategory.id) },
                { "tenantId", tenantId}
            }, new BsonDocument()
            {

                { "$push", new BsonDocument()
                    {
                        { "requests.$[request].documents.$[document].mcuFiles",query.ToBsonDocument() }

                    }
                }
            }, new UpdateOptions()
            {
                ArrayFilters = new List<ArrayFilterDefinition>()
                {
                    new JsonArrayFilterDefinition<Request>("{ \"request.id\": "+new ObjectId(moveFromoneCategoryToAnotherCategory.toRequestId).ToJson()+"}"),
                    new JsonArrayFilterDefinition<Request>("{ \"document.id\": "+new ObjectId(moveFromoneCategoryToAnotherCategory.toDocId).ToJson()+"}")
                }
            });
                            return result.ModifiedCount == 1;
                        }

                    }

                }
            }
            return false;
        }
        public async Task<string> ViewCategoryAnnotations(ViewCategoryAnnotations viewCategoryAnnotations, int tenantId)
        {
            IMongoCollection<Request> collection = mongoService.db.GetCollection<Request>("Request");

            using var asyncCursor = collection.Aggregate(PipelineDefinition<Request, BsonDocument>.Create(
                @"{""$match"": {

                  ""_id"": " + new ObjectId(viewCategoryAnnotations.id).ToJson() + @",
                  ""tenantId"": " + tenantId + @"
                            }
                        }",
                        @"{
                            ""$unwind"": ""$requests""
                        }",
                        @"{
                            ""$match"": {
                                ""requests.id"": " + new ObjectId(viewCategoryAnnotations.fromRequestId).ToJson() + @"
                            }
                        }",
                        @"{
                            ""$unwind"": ""$requests.documents""
                        }",
                         @"{
                            ""$match"": {
                                ""requests.documents.id"": " + new ObjectId(viewCategoryAnnotations.fromDocId).ToJson() + @"
                            }
                        }",
                         @"{
                            ""$unwind"": ""$requests.documents.mcuFiles""
                        }",
                         @"{
                            ""$match"": {
                                ""requests.documents.mcuFiles.id"": " + new ObjectId(viewCategoryAnnotations.fromFileId).ToJson() + @"
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

                foreach (var current in asyncCursor.Current)
                {
                    RequestFile query = BsonSerializer.Deserialize<RequestFile>((BsonDocument)current.GetValue("files"));
                    return query.annotations;
                }
            }
            return null;
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
                foreach (var current in asyncCursor.Current)
                {
                    return true;
                }
            }
            return false;
        }
        public async Task<bool> SaveCategoryAnnotations(SaveCategoryAnnotations saveCategoryAnnotations, int tenantId)
        {
            if (await IsExistfile(saveCategoryAnnotations.id, saveCategoryAnnotations.requestId, saveCategoryAnnotations.docId, saveCategoryAnnotations.fileId, tenantId))
            {
                IMongoCollection<Request> collection = mongoService.db.GetCollection<Request>("Request");
                UpdateResult result = await collection.UpdateOneAsync(new BsonDocument()
                {
                    { "_id", BsonObjectId.Create(saveCategoryAnnotations.id) },
                    { "tenantId", tenantId}

                }, new BsonDocument()
                {
                    { "$set", new BsonDocument()
                        {
                        { "requests.$[request].documents.$[document].files.$[file].annotations", saveCategoryAnnotations.annotations},
                        { "requests.$[request].documents.$[document].files.$[file].fileModifiedOn", DateTime.UtcNow}
                        }
                    }
                }, new UpdateOptions()
                {
                    ArrayFilters = new List<ArrayFilterDefinition>()
                    {
                        new JsonArrayFilterDefinition< Request>("{ \"request.id\": "+new ObjectId( saveCategoryAnnotations.requestId).ToJson()+"}"),
                        new JsonArrayFilterDefinition< Request>("{ \"document.id\": "+new ObjectId( saveCategoryAnnotations.docId).ToJson()+"}"),
                        new JsonArrayFilterDefinition< Request>("{ \"file.id\": "+new ObjectId( saveCategoryAnnotations.fileId).ToJson()+"}")
                    }
                });
                return result.ModifiedCount == 1;
            }
            else
            {
                IMongoCollection<Request> collection = mongoService.db.GetCollection<Request>("Request");
                UpdateResult result = await collection.UpdateOneAsync(new BsonDocument()
                {
                    { "_id", BsonObjectId.Create(saveCategoryAnnotations.id) },
                    { "tenantId", tenantId}

                }, new BsonDocument()
                {
                    { "$set", new BsonDocument()
                        {
                        { "requests.$[request].documents.$[document].mcuFiles.$[mcuFile].annotations", saveCategoryAnnotations.annotations},
                        { "requests.$[request].documents.$[document].mcuFiles.$[mcuFile].fileModifiedOn", DateTime.UtcNow}
                        }
                    }
                }, new UpdateOptions()
                {
                    ArrayFilters = new List<ArrayFilterDefinition>()
                    {
                        new JsonArrayFilterDefinition< Request>("{ \"request.id\": "+new ObjectId( saveCategoryAnnotations.requestId).ToJson()+"}"),
                        new JsonArrayFilterDefinition< Request>("{ \"document.id\": "+new ObjectId( saveCategoryAnnotations.docId).ToJson()+"}"),
                        new JsonArrayFilterDefinition< Request>("{ \"mcuFile.id\": "+new ObjectId( saveCategoryAnnotations.fileId).ToJson()+"}")
                    }
                });

                return result.ModifiedCount == 1;
            }
        }

    }
}
