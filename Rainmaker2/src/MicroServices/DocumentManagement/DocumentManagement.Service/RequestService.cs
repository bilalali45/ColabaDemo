using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentManagement.Entity;
using DocumentManagement.Model;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Template = DocumentManagement.Model.Template;

namespace DocumentManagement.Service
{
    public class RequestService : IRequestService
    {
        private readonly IMongoService mongoService;
        public RequestService(IMongoService mongoService)
        {
            this.mongoService = mongoService;
        }

        public async Task<bool> SaveDraft(LoanApplication loanApplication)
        {
            IMongoCollection<Entity.StatusList> collection =
                mongoService.db.GetCollection<Entity.StatusList>("StatusList");

            using var asyncCursorStatus = collection.Aggregate(
                PipelineDefinition<Entity.StatusList, BsonDocument>.Create(
                    @"{""$match"": {
                  ""order"": " + 4 + @"
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

            IMongoCollection<Entity.Request> collectionRequest =
                mongoService.db.GetCollection<Entity.Request>("Request");

            using var asyncCursorRequest = collectionRequest.Aggregate(
                PipelineDefinition<Entity.Request, BsonDocument>.Create(
                    @"{""$match"": {
                  ""loanApplicationId"": " + loanApplication.loanApplicationId + @"
                            }
                        }", @"{
                            ""$project"": {
                                ""loanApplicationId"": 1
                            }
                        }"
                ));
            Request request = new Request();
            if (await asyncCursorRequest.MoveNextAsync())
            {
                int loanApplicationId = 0;
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
            request.userId = loanApplication.requests[0].userId;
            request.userName = loanApplication.requests[0].userName;
            request.createdOn = DateTime.UtcNow;
            request.status = DocumentStatus.Draft;
            request.message = loanApplication.requests[0].message;
            request.documents = new List<RequestDocument>() { };

            IMongoCollection<Request> collectionInsertRequest = mongoService.db.GetCollection<Request>("Request");

            BsonArray documentBsonArray = new BsonArray();
            BsonArray activityLogBsonArray = new BsonArray();

            foreach (var item in loanApplication.requests[0].documents)
            {
                BsonDocument bsonDocument = new BsonDocument();

                item.id = ObjectId.GenerateNewId().ToString();
                item.activityId = ObjectId.GenerateNewId().ToString();
                item.status = DocumentStatus.BorrowerTodo;

                bsonDocument.Add("id", item.id);
                bsonDocument.Add("activityId", item.activityId);
                bsonDocument.Add("status", item.status);
                bsonDocument.Add("typeId", item.typeId);
                bsonDocument.Add("displayName", item.displayName);
                bsonDocument.Add("message", item.message);
                bsonDocument.Add("files", new BsonArray());

                //Add document
                documentBsonArray.Add(bsonDocument);

                string activityLogId = String.Empty;

                if (!string.IsNullOrEmpty(item.typeId))
                {
                    IMongoCollection<ActivityLog> collectionActivityLog = mongoService.db.GetCollection<ActivityLog>("ActivityLog");

                    using var asyncCursorActivityLog = collectionActivityLog.Aggregate(
                        PipelineDefinition<ActivityLog, BsonDocument>.Create(
                            @"{""$match"": {
                                        ""loanId"": " + new ObjectId(loanApplication.id).ToJson() + @",
                                        ""typeId"": " + new ObjectId(item.typeId).ToJson() + @",
                                        ""activity"":""" + ActivityStatus.RequestedBy + @"""
                            }
                        }", @"{
                            ""$project"": {
                                ""_id"": 1
                            }
                        }"
                        ));

                    if (await asyncCursorActivityLog.MoveNextAsync())
                    {
                        foreach (var current in asyncCursorActivityLog.Current)
                        {
                            ActivityLogIdQuery query = BsonSerializer.Deserialize<ActivityLogIdQuery>(current);
                            activityLogId = query._id;
                        }

                    }
                }
                else if (!string.IsNullOrEmpty(item.displayName))
                {
                    IMongoCollection<ActivityLog> collectionActivityLog = mongoService.db.GetCollection<ActivityLog>("ActivityLog");

                    using var asyncCursorActivityLog = collectionActivityLog.Aggregate(
                        PipelineDefinition<ActivityLog, BsonDocument>.Create(
                            @"{""$match"": {
                                        ""loanId"": " + new ObjectId(loanApplication.id).ToJson() + @",
                                        ""activity"":""" + ActivityStatus.RequestedBy + @""",
                                        ""docName"": " + item.displayName + @"
                            }
                        }", @"{
                            ""$project"": {
                                ""_id"": 1
                            }
                        }"
                        ));

                    if (await asyncCursorActivityLog.MoveNextAsync())
                    {
                        foreach (var current in asyncCursorActivityLog.Current)
                        {
                            ActivityLogIdQuery query = BsonSerializer.Deserialize<ActivityLogIdQuery>(current);
                            activityLogId = query._id;
                        }
                    }
                }

                //if (!String.IsNullOrEmpty(activityLogId))
                //{
                    IMongoCollection<ActivityLog> collectionInsertActivityLog = mongoService.db.GetCollection<ActivityLog>("ActivityLog");

                    ActivityLog activityLog = new ActivityLog()
                    {
                        id = item.activityId,
                        userId = request.userId,
                        userName = request.userName,
                        dateTime = DateTime.UtcNow,
                        activity = !String.IsNullOrEmpty(activityLogId)  ? ActivityStatus.RerequestedBy : ActivityStatus.RequestedBy,
                        typeId = item.typeId,
                        docId = item.id,
                        docName = item.displayName,
                        loanId = loanApplication.id,
                        message = item.message,
                        log = new List<Log>() {  }
                    };
                    await collectionInsertActivityLog.InsertOneAsync(activityLog);

                    InsertLog(item.activityId, ActivityStatus.StatusChanged + " : " + DocumentStatus.BorrowerTodo);
             
            }

            IMongoCollection<Request> collectionDeleteDraftRequest = mongoService.db.GetCollection<Request>("Request");

            UpdateResult resultDeleteDraftRequest = await collectionDeleteDraftRequest.UpdateOneAsync(new BsonDocument()
            {
                { "loanApplicationId", loanApplication.loanApplicationId}
            }
                , new BsonDocument()
                {
                { "$pull", new BsonDocument()
                    {
                        { "requests", new BsonDocument(){
                            new BsonDocument()
                                {
                                    {
                                        "$and", new BsonArray(){
                                            new BsonDocument(){{ "status",DocumentStatus.Draft}},
                                            new BsonDocument(){{ "userId",request.userId}}
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                });

            BsonDocument bsonElements = new BsonDocument();
            bsonElements.Add("id", ObjectId.GenerateNewId());
            bsonElements.Add("userId", request.userId);
            bsonElements.Add("userName", request.userName);
            bsonElements.Add("createdOn", DateTime.UtcNow);
            bsonElements.Add("status", DocumentStatus.Draft);
            bsonElements.Add("message", request.message);
            bsonElements.Add("documents", documentBsonArray);

            UpdateResult result = await collectionInsertRequest.UpdateOneAsync(new BsonDocument()
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

            return true;
        }

        public async Task InsertLog(string activityId,string activity)
        {
            IMongoCollection<ActivityLog> collection = mongoService.db.GetCollection<ActivityLog>("ActivityLog");

            BsonDocument bsonElements = new BsonDocument();
            bsonElements.Add("_id", ObjectId.GenerateNewId());
            bsonElements.Add("dateTime", DateTime.UtcNow);
            bsonElements.Add("activity", activity);

            UpdateResult result = await collection.UpdateOneAsync(new BsonDocument()
                {
                    { "_id", new ObjectId(activityId)}
                }, new BsonDocument()
                {
                    { "$push", new BsonDocument()
                        {
                            { "log", bsonElements  }
                        }
                    },
                }
            );
        }
    }
}
