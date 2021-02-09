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
    public class WorkbenchService : IWorkbenchService
    {
        private readonly IMongoService mongoService;
        public WorkbenchService(IMongoService mongoService)
        {
            this.mongoService = mongoService;
        }
        public async Task<List<WorkbenchFile>> GetWorkbenchFiles(int loanApplicationId, int tenantId)
        {
            IMongoCollection<Request> collection = mongoService.db.GetCollection<Request>("Request");

            using var asyncCursor = collection.Aggregate(PipelineDefinition<Request, BsonDocument>.Create(
                @"{""$match"": {

                  ""loanApplicationId"": " + loanApplicationId + @",
                  ""tenantId"": " + tenantId + @"
                            }
                        }", @"{
                            ""$project"": {
                                ""_id"": 1,
                               ""files"": ""$workbench"",
                            }
                        }"
                ));

            List<WorkbenchFile> result = new List<WorkbenchFile>();
            while (await asyncCursor.MoveNextAsync())
            {
                foreach (var current in asyncCursor.Current)
                {
                    WorkbenchQuery query = BsonSerializer.Deserialize<WorkbenchQuery>(current);
                    if (query.files != null)
                    {
                        foreach (var x in query.files)
                        {
                            result.Add(new WorkbenchFile()
                            {
                                id=query.id,
                                fileId = x.id,
                                fileUploadedOn = DateTime.SpecifyKind(x.fileUploadedOn, DateTimeKind.Utc),
                                mcuName = x.mcuName,
                                userId = x.userId,
                                userName = x.userName,
                                fileModifiedOn = x.fileModifiedOn == null ? null : (DateTime?)DateTime.SpecifyKind(x.fileModifiedOn.Value, DateTimeKind.Utc)
                            });
                        }
                    }
                }
            }
            result = result.OrderByDescending(x => (x.fileUploadedOn > (x.fileModifiedOn ?? DateTime.MinValue)) ? x.fileUploadedOn : x.fileModifiedOn).ToList();

            return result;
        }

        public async Task<bool> MoveFromWorkBenchToTrash(MoveFromWorkBenchToTrash moveFromWorkBenchToTrash, int tenantId)
        {
            IMongoCollection<Request> collection = mongoService.db.GetCollection<Request>("Request");

            using var asyncCursor = collection.Aggregate(PipelineDefinition<Request, BsonDocument>.Create(
                @"{""$match"": {

                  ""_id"": " + new ObjectId(moveFromWorkBenchToTrash.id).ToJson() + @",
                  ""tenantId"": " + tenantId + @"
                            }
                        }", @"{
                            ""$unwind"": ""$workbench""
                        }",
                        @"{
                            ""$match"": {
                                ""workbench.id"": " + new ObjectId(moveFromWorkBenchToTrash.fromFileId).ToJson() + @"
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
                foreach (var current in asyncCursor.Current)
                {
                    bool deleted = await DeleteWorkbenchFile(moveFromWorkBenchToTrash.id, tenantId, moveFromWorkBenchToTrash.fromFileId);
                    if (deleted)
                    {
                        IMongoCollection<Request> collectionInsertRequest = mongoService.db.GetCollection<Request>("Request");

                        UpdateResult result = await collectionInsertRequest.UpdateOneAsync(new BsonDocument()
                        {
                            { "_id",new ObjectId(moveFromWorkBenchToTrash.id)}
                        }, new BsonDocument()
                        {
                            { "$push", new BsonDocument()
                                {
                                    { "trash", current.GetValue("files")  }
                                }
                            },
                        }
                        );
                        return result.ModifiedCount == 1;
                    }

                }
            }
            return false;
        }
        public async Task<bool> DeleteWorkbenchFile(string id, int tenantid, string fromFileId)
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
                          { "workbench", new BsonDocument(){

                              { "id",BsonObjectId.Create(fromFileId)
                              }
                          }
                       }
                    }
                }
            });
            return result.ModifiedCount == 1;
        }

        public async Task<bool> MoveFromWorkBenchToCategory(MoveFromWorkBenchToCategory moveFromWorkBenchToCategory , int tenantId)
        {
            IMongoCollection<Request> collection = mongoService.db.GetCollection<Request>("Request");

            using var asyncCursor = collection.Aggregate(PipelineDefinition<Request, BsonDocument>.Create(
                @"{""$match"": {

                  ""_id"": " + new ObjectId(moveFromWorkBenchToCategory.id).ToJson() + @",
                  ""tenantId"": " + tenantId + @"
                            }
                        }", @"{
                            ""$unwind"": ""$workbench""
                        }",
                        @"{
                            ""$match"": {
                                ""workbench.id"": " + new ObjectId(moveFromWorkBenchToCategory.fromFileId).ToJson() + @"
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
                foreach (var current in asyncCursor.Current)
                {
                    bool deleted = await DeleteWorkbenchFile(moveFromWorkBenchToCategory.id, tenantId, moveFromWorkBenchToCategory.fromFileId);
                    if (deleted)
                    {
                        IMongoCollection<Request> collectionInsertRequest = mongoService.db.GetCollection<Request>("Request");

                        UpdateResult result = await collection.UpdateOneAsync(new BsonDocument()
            {
                { "_id", BsonObjectId.Create(moveFromWorkBenchToCategory.id) },
                { "tenantId", tenantId}
            }, new BsonDocument()
            {

                { "$push", new BsonDocument()
                    {
                        { "requests.$[request].documents.$[document].mcuFiles",current.GetValue("files") }

                    }
                }
            }, new UpdateOptions()
            {
                ArrayFilters = new List<ArrayFilterDefinition>()
                {
                    new JsonArrayFilterDefinition<Request>("{ \"request.id\": "+new ObjectId(moveFromWorkBenchToCategory.toRequestId).ToJson()+"}"),
                    new JsonArrayFilterDefinition<Request>("{ \"document.id\": "+new ObjectId(moveFromWorkBenchToCategory.toDocId).ToJson()+"}")
                }
            });
                        return result.ModifiedCount == 1;
                    }

                }
            }
            return false;
        }
        public async Task<string> ViewWorkbenchAnnotations(ViewWorkbenchAnnotations  viewWorkbenchAnnotations, int tenantId)
        {
            IMongoCollection<Request> collection = mongoService.db.GetCollection<Request>("Request");

            using var asyncCursor = collection.Aggregate(PipelineDefinition<Request, BsonDocument>.Create(
                @"{""$match"": {

                  ""_id"": " + new ObjectId(viewWorkbenchAnnotations.id).ToJson() + @",
                  ""tenantId"": " + tenantId + @"
                            }
                        }", @"{
                            ""$unwind"": ""$workbench""
                        }",
                        @"{
                            ""$match"": {
                                ""workbench.id"": " + new ObjectId(viewWorkbenchAnnotations.fromFileId).ToJson() + @"
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
                    return query.annotations;
                }
            }
            return null;
        }
        public async Task<bool> SaveWorkbenchAnnotations(SaveWorkbenchAnnotations  saveWorkbenchAnnotations, int tenantId)
        {
            IMongoCollection<Request> collection = mongoService.db.GetCollection<Request>("Request");
            UpdateResult result = await collection.UpdateOneAsync(new BsonDocument()
            {
                { "_id", BsonObjectId.Create(saveWorkbenchAnnotations.id) },
                { "tenantId", tenantId}
            }, new BsonDocument()
            {
                { "$set", new BsonDocument()
                    {
                        { "workbench.$[workbenchs].annotations", saveWorkbenchAnnotations.annotations},
                        { "workbench.$[workbenchs].fileModifiedOn", DateTime.UtcNow}
                    }
                }
            }, new UpdateOptions()
            {
                ArrayFilters = new List<ArrayFilterDefinition>()
                {
            
                    new JsonArrayFilterDefinition< Request>("{ \"workbenchs.id\": "+new ObjectId( saveWorkbenchAnnotations.fileId).ToJson()+"}")
                }
            });

            return result.ModifiedCount == 1;
            
        }

    }
}
