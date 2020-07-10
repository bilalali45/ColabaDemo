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
                                ""_id"": 0,
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
                        requests = new List<Request>(){}
                    };

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
            

            foreach (var item in loanApplication.requests[0].documents)
            {
                BsonDocument bsonDocument = new BsonDocument();

                item.id = ObjectId.GenerateNewId().ToString();
                item.activityId = ObjectId.GenerateNewId().ToString();
                item.status = DocumentStatus.BorrowerTodo;
                //item.files = new List<RequestFile>() { };

                bsonDocument.Add("id", item.id);
                bsonDocument.Add("activityId", item.activityId);
                bsonDocument.Add("status", item.status);
                bsonDocument.Add("typeId", item.typeId);
                bsonDocument.Add("displayName", item.displayName);
                bsonDocument.Add("message", item.message);
                bsonDocument.Add("files", new BsonArray());

                documentBsonArray.Add(bsonDocument);
            }

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
    }
}
