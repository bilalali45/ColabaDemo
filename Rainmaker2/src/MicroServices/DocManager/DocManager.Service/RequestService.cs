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
    public class RequestService : IRequestService
    {
        private readonly IMongoService mongoService;
        private readonly IRainmakerService rainmakerService;
        public RequestService(IMongoService mongoService, IRainmakerService rainmakerService)
        {
            this.mongoService = mongoService;
            this.rainmakerService = rainmakerService;
        }
        public async Task<bool> Save(SaveModel loanApplication, IEnumerable<string> authHeader)
        {
            // get document upload status
            IMongoCollection<StatusList> collection =
                mongoService.db.GetCollection<StatusList>("StatusList");

            using var asyncCursorStatus = collection.Aggregate(
                PipelineDefinition<StatusList, BsonDocument>.Create(
                    @"{""$match"": {
                  ""order"": " + 3 + @"
                            }
                        }", @"{
                            ""$project"": {
                                ""_id"": 1
                            }
                        }"
                ));
            while (await asyncCursorStatus.MoveNextAsync())
            {
                foreach (var current in asyncCursorStatus.Current)
                {
                    StatusNameQuery query = BsonSerializer.Deserialize<StatusNameQuery>(current);
                    loanApplication.status = query._id;
                }
            }
            // check whether loan application already exists
            IMongoCollection<Request> collectionRequest =
                mongoService.db.GetCollection<Request>("Request");

            using var asyncCursorRequest = collectionRequest.Aggregate(
                PipelineDefinition<Request, BsonDocument>.Create(
                    @"{""$match"": {
                  ""loanApplicationId"": " + loanApplication.loanApplicationId + @"
                            }
                        }", @"{
                            ""$project"": {
                                ""loanApplicationId"": 1
                            }
                        }"
                ));
            // if loan application does not exists create loan application
            Request request = new Request();
            if (await asyncCursorRequest.MoveNextAsync())
            {
                int loanApplicationId = -1;
                foreach (var current in asyncCursorRequest.Current)
                {
                    LoanApplicationIdQuery query = BsonSerializer.Deserialize<LoanApplicationIdQuery>(current);
                    loanApplicationId = query.loanApplicationId;
                    loanApplication.id = query._id;
                }

                if (loanApplicationId != loanApplication.loanApplicationId)
                {
                    IMongoCollection<LoanApplication> collectionLoanApplication =
                        mongoService.db.GetCollection<LoanApplication>("Request");
                    LoanApplication loanApplicationModel = new LoanApplication()
                    {
                        id = ObjectId.GenerateNewId().ToString(),
                        loanApplicationId = loanApplication.loanApplicationId,
                        tenantId = loanApplication.tenantId,
                        status = loanApplication.status,
                        userId = loanApplication.userId,
                        userName = loanApplication.userName,
                        requests = new List<Request>() { }
                    };
                    loanApplication.id = loanApplicationModel.id;
                    await collectionLoanApplication.InsertOneAsync(loanApplicationModel);
                }
            }

            request.id = ObjectId.GenerateNewId().ToString();
            request.userId = loanApplication.request.userId;
            request.userName = loanApplication.request.userName;
            request.createdOn = DateTime.UtcNow;
            request.status = RequestStatus.Active;
            request.message = "";
            request.document = new RequestDocument() { };

            IMongoCollection<Request> collectionInsertRequest = mongoService.db.GetCollection<Request>("Request");

            BsonArray documentBsonArray = new BsonArray();

            var item = loanApplication.request.document;

            BsonDocument bsonDocument = new BsonDocument();

            item.id = ObjectId.GenerateNewId().ToString();
            item.status = DocumentStatus.ManuallyAdded;
            item.message = string.Empty;
            bsonDocument.Add("id", new ObjectId(item.id));
            bsonDocument.Add("status", item.status);
            bsonDocument.Add("typeId", string.IsNullOrEmpty(item.typeId) ? (BsonValue)BsonNull.Value : new BsonObjectId(new ObjectId(item.typeId)));
            bsonDocument.Add("displayName", item.displayName);
            bsonDocument.Add("message", item.message);
            bsonDocument.Add("isRejected", false);
            bsonDocument.Add("files", new BsonArray());

            //Add document
            documentBsonArray.Add(bsonDocument);
            /*
            IMongoCollection<ActivityLog> collectionInsertActivityLog =
                mongoService.db.GetCollection<ActivityLog>("ActivityLog");

            ActivityLog activityLog = new ActivityLog()
            {
                id = ObjectId.GenerateNewId().ToString(),
                requestId = request.id,
                userId = request.userId,
                userName = request.userName,
                dateTime = DateTime.UtcNow,
                activity = ActivityStatus.AddedBy,
                typeId = string.IsNullOrEmpty(item.typeId) ? null : item.typeId,
                docId = item.id,
                docName = item.displayName,
                loanId = loanApplication.id,
                message = item.message,
                log = new List<Log>() { }
            };
            await collectionInsertActivityLog.InsertOneAsync(activityLog);

            await activityLogService.InsertLog(activityLog.id, string.Format(ActivityStatus.AddBy, request.userName));

            IMongoCollection<Request> collectionDeleteDraftRequest = mongoService.db.GetCollection<Request>("Request");
            */
            BsonDocument bsonElements = new BsonDocument();
            bsonElements.Add("id", new ObjectId(request.id));
            bsonElements.Add("userId", request.userId);
            bsonElements.Add("userName", request.userName);
            bsonElements.Add("createdOn", request.createdOn);
            bsonElements.Add("status", request.status);
            bsonElements.Add("message", request.message);
            bsonElements.Add("documents", documentBsonArray);

            if (documentBsonArray.Count > 0)
            {
                await collectionInsertRequest.UpdateOneAsync(new BsonDocument()
                    {
                        { "loanApplicationId", loanApplication.loanApplicationId}
                    }, new BsonDocument()
                    {
                        { "$push", new BsonDocument()
                            {
                                { "requests", bsonElements  }
                            }
                        },
                    }
                );
            }

            await rainmakerService.UpdateLoanInfo(loanApplication.loanApplicationId,"", authHeader);
            return true;
        }
        public async Task<string> Submit(string contentType, string id, string requestId, string docId,string mcuName, string serverName, int size, string encryptionKey, string encryptionAlgorithm, int tenantId,int userId, string userName, IEnumerable<string> authHeader)
        {
            IMongoCollection<Request> collection = mongoService.db.GetCollection<Request>("Request");
            var fileId = ObjectId.GenerateNewId();
            UpdateResult result = await collection.UpdateOneAsync(new BsonDocument()
            {
                { "_id", BsonObjectId.Create(id) },
                { "tenantId", tenantId},
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
                        { "requests.$[request].documents.$[document].mcuFiles", new BsonDocument() { { "id", fileId }, { "clientName", BsonString.Empty } , { "serverName", serverName }, { "fileUploadedOn", BsonDateTime.Create(DateTime.UtcNow) }, { "size", size }, { "encryptionKey", encryptionKey }, { "encryptionAlgorithm", encryptionAlgorithm }, { "order" , 0 }, { "mcuName", mcuName }, { "contentType", contentType }, { "status", FileStatus.SubmittedToMcu },{ "byteProStatus", ByteProStatus.NotSynchronized}, { "isRead", false }, { "userId", userId }, { "userName", userName } }   }
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
                /*
                string activityLogId = await activityLogService.GetActivityLogId(id, requestId, docId);

                await activityLogService.InsertLog(activityLogId, string.Format(ActivityStatus.FileSubmitted, mcuName));
                */
            }
            
            await rainmakerService.UpdateLoanInfo(null, id, authHeader);
            if (result.ModifiedCount == 1)
            {
                return fileId.ToString();
            }


            return null;
        }
        public async Task<List<FileViewDto>> GetFileByDocId(FileViewModel model, string ipAddress, int tenantId)
        {
            IMongoCollection<Request> collection = mongoService.db.GetCollection<Request>("Request");

            using var asyncCursor = collection.Aggregate(PipelineDefinition<Request, BsonDocument>.Create(
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
        }

    }
}