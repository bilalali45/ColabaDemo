using DocManager.Model;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
namespace DocManager.Service
{
    public class TrashService : ITrashService
    {
        private readonly IMongoService mongoService;
        public TrashService(IMongoService mongoService)
        {
            this.mongoService = mongoService;
        }
        public async Task<List<WorkbenchFile>> GetTrashFiles(int loanApplicationId, int tenantId)
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
                               ""files"": ""$trash"",
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
                                id = query.id,
                                fileId = x.id,
                                fileUploadedOn = DateTime.SpecifyKind(x.fileUploadedOn, DateTimeKind.Utc),
                                mcuName = x.mcuName,
                                userId = x.userId,
                                userName = x.userName,
                                fileModifiedOn = x.fileModifiedOn==null ? null : (DateTime?)DateTime.SpecifyKind(x.fileModifiedOn.Value,DateTimeKind.Utc)
                            });
                        }
                    }
                }
            }
            result.Reverse();
            return result;
        }

        public async Task<bool> MoveFromTrashToWorkBench(MoveFromTrashToWorkBench moveFromTrashToWorkBench, int tenantId)
        {
            IMongoCollection<Request> collection = mongoService.db.GetCollection<Request>("Request");

            using var asyncCursor = collection.Aggregate(PipelineDefinition<Request, BsonDocument>.Create(
                @"{""$match"": {

                  ""_id"": " + new ObjectId(moveFromTrashToWorkBench.id).ToJson() + @",
                  ""tenantId"": " + tenantId + @"
                            }
                        }", @"{
                            ""$unwind"": ""$trash""
                        }",
                        @"{
                            ""$match"": {
                                ""trash.id"": " + new ObjectId(moveFromTrashToWorkBench.fromFileId).ToJson() + @"
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
                foreach (var current in asyncCursor.Current)
                {
                    bool deleted = await DeleteTrashFile(moveFromTrashToWorkBench.id, tenantId, moveFromTrashToWorkBench.fromFileId);
                    if (deleted)
                    {
                        IMongoCollection<Request> collectionInsertRequest = mongoService.db.GetCollection<Request>("Request");

                        UpdateResult result = await collectionInsertRequest.UpdateOneAsync(new BsonDocument()
                        {
                            { "_id",new ObjectId(moveFromTrashToWorkBench.id)}
                        }, new BsonDocument()
                        {
                            { "$push", new BsonDocument()
                                {
                                    { "workbench", current.GetValue("files")  }
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
        private async Task<bool> DeleteTrashFile(string id, int tenantid, string fromFileId)
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
                          { "trash", new BsonDocument(){

                              { "id",BsonObjectId.Create(fromFileId)
                              }
                          }
                       }
                    }
                }
            });
            return result.ModifiedCount == 1;
        }
        public async Task<bool> SaveTrashAnnotations(SaveTrashAnnotations saveTrashAnnotations, int tenantId)
        {
            IMongoCollection<Request> collection = mongoService.db.GetCollection<Request>("Request");
            UpdateResult result = await collection.UpdateOneAsync(new BsonDocument()
            {
                { "_id", BsonObjectId.Create(saveTrashAnnotations.id) },
                { "tenantId", tenantId}

            }, new BsonDocument()
            {
                { "$set", new BsonDocument()
                    {
                     { "trash.$[trashs].annotations", saveTrashAnnotations.annotations}  
                    }
                }
            }, new UpdateOptions()
            {
                ArrayFilters = new List<ArrayFilterDefinition>()
                {
                    new JsonArrayFilterDefinition< Request>("{ \"trashs.id\": "+new ObjectId( saveTrashAnnotations.fileId).ToJson()+"}")
                }
            });

            return result.ModifiedCount == 1;

        }

    }
}
