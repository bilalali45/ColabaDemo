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
        private readonly IActivityLogService activityLogService;
        public RequestService(IMongoService mongoService, IActivityLogService activityLogService)
        {
            this.mongoService = mongoService;
            this.activityLogService = activityLogService;
        }

        public async Task<bool> Save(Model.LoanApplication loanApplication, bool isDraft)
        {
            // get document upload status
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
            // check whether loan application already exists
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
            // if loan application does not exists create loan application
            Entity.Request request = new Entity.Request();
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
                    IMongoCollection<Entity.LoanApplication> collectionLoanApplication =
                        mongoService.db.GetCollection<Entity.LoanApplication>("Request");
                    Entity.LoanApplication loanApplicationModel = new Entity.LoanApplication()
                    {
                        id = ObjectId.GenerateNewId().ToString(),
                        loanApplicationId = loanApplication.loanApplicationId,
                        tenantId = loanApplication.tenantId,
                        status = loanApplication.status,
                        userId = loanApplication.userId,
                        userName = loanApplication.userName,
                        requests = new List<Entity.Request>() { }
                    };
                    loanApplication.id = loanApplicationModel.id;
                    await collectionLoanApplication.InsertOneAsync(loanApplicationModel);
                }
            }

            request.id = ObjectId.GenerateNewId().ToString();
            request.userId = loanApplication.requests[0].userId;
            request.userName = loanApplication.requests[0].userName;
            request.createdOn = DateTime.UtcNow;
            request.status = isDraft ? RequestStatus.Draft : RequestStatus.Active;
            request.message = loanApplication.requests[0].message;
            request.documents = new List<Entity.RequestDocument>() { };

            IMongoCollection<Entity.Request> collectionInsertRequest = mongoService.db.GetCollection<Entity.Request>("Request");

            BsonArray documentBsonArray = new BsonArray();

            foreach (var item in loanApplication.requests[0].documents)
            {
                if (!string.IsNullOrEmpty(item.requestId) && !string.IsNullOrEmpty(item.docId))
                {
                    IMongoCollection<Entity.Request> collectionDraft = mongoService.db.GetCollection<Entity.Request>("Request");

                    //update document message

                    UpdateResult resultDraft = await collectionDraft.UpdateOneAsync(new BsonDocument()
                    {
                        { "_id", BsonObjectId.Create(loanApplication.id) }
                    }, new BsonDocument()
                    {
                        { "$set", new BsonDocument()
                            {
                                { "requests.$[request].documents.$[document].message", item.message}
                            }
                        }
                    }, new UpdateOptions()
                    {
                        ArrayFilters = new List<ArrayFilterDefinition>()
                        {
                            new JsonArrayFilterDefinition<Entity.Request>("{ \"request.id\": "+new ObjectId(item.requestId).ToJson()+"}"),
                            new JsonArrayFilterDefinition<Entity.Request>("{ \"document.id\": "+new ObjectId(item.docId).ToJson()+"}")
                        }
                    });

                    //isDraft == false update status

                    if (!isDraft)
                    {
                        IMongoCollection<Entity.Request> collectionDraftStatus = mongoService.db.GetCollection<Entity.Request>("Request");

                        UpdateResult resultDraftStatus = await collectionDraftStatus.UpdateOneAsync(new BsonDocument()
                         {
                             { "_id", BsonObjectId.Create(loanApplication.id) }
                         }, new BsonDocument()
                         {
                             { "$set", new BsonDocument()
                                 {
                                     { "requests.$[request].documents.$[document].status", DocumentStatus.BorrowerTodo}
                                 }
                             }
                         }, new UpdateOptions()
                         {
                             ArrayFilters = new List<ArrayFilterDefinition>()
                             {
                                 new JsonArrayFilterDefinition<Entity.Request>("{ \"request.id\": "+new ObjectId(item.requestId).ToJson()+"}"),
                                 new JsonArrayFilterDefinition<Entity.Request>("{ \"document.id\": "+new ObjectId(item.docId).ToJson()+"}")
                             }
                         });

                        //insert activity log
                        IMongoCollection<ActivityLog> collectionInsertActivityLog = mongoService.db.GetCollection<ActivityLog>("ActivityLog");

                        ActivityLog activityLog = new ActivityLog()
                        {
                            id = ObjectId.GenerateNewId().ToString(),
                            requestId = item.requestId,
                            userId = request.userId,
                            userName = request.userName,
                            dateTime = DateTime.UtcNow,
                            activity = string.Format(ActivityStatus.RerequestedBy, request.userName),
                            typeId = item.typeId,
                            docId = item.docId,
                            docName = item.displayName,
                            loanId = loanApplication.id,
                            message = item.message,
                            log = new List<Log>() { }
                        };
                        await collectionInsertActivityLog.InsertOneAsync(activityLog);

                        await activityLogService.InsertLog(activityLog.id, string.Format(ActivityStatus.StatusChanged, DocumentStatus.BorrowerTodo));
                    }
                }
                else
                {
                    BsonDocument bsonDocument = new BsonDocument();

                    item.id = ObjectId.GenerateNewId().ToString();
                    item.status = DocumentStatus.BorrowerTodo;

                    bsonDocument.Add("id", new ObjectId(item.id));
                    bsonDocument.Add("status", item.status);
                    bsonDocument.Add("typeId", item.typeId == null ? (BsonValue)BsonNull.Value : new BsonObjectId(new ObjectId(item.typeId)));
                    bsonDocument.Add("displayName", item.displayName);
                    bsonDocument.Add("message", item.message);
                    bsonDocument.Add("files", new BsonArray());

                    //Add document
                    documentBsonArray.Add(bsonDocument);
                    if (!isDraft)
                    {
                        string activityLogId = String.Empty;

                        if (!string.IsNullOrEmpty(item.typeId))
                        {
                            IMongoCollection<ActivityLog> collectionActivityLog =
                                mongoService.db.GetCollection<ActivityLog>("ActivityLog");

                            using var asyncCursorActivityLog = collectionActivityLog.Aggregate(
                                PipelineDefinition<ActivityLog, BsonDocument>.Create(
                                    @"{""$match"": {
                                        ""loanId"": " + new ObjectId(loanApplication.id).ToJson() + @",
                                        ""typeId"": " + new ObjectId(item.typeId).ToJson() + @"
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
                            IMongoCollection<ActivityLog> collectionActivityLog =
                                mongoService.db.GetCollection<ActivityLog>("ActivityLog");

                            using var asyncCursorActivityLog = collectionActivityLog.Aggregate(
                                PipelineDefinition<ActivityLog, BsonDocument>.Create(
                                    @"{""$match"": {
                                        ""loanId"": " + new ObjectId(loanApplication.id).ToJson() + @",
                                        ""docName"": """ + item.displayName.Replace("\"", "\\\"") + @"""
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

                        IMongoCollection<ActivityLog> collectionInsertActivityLog =
                            mongoService.db.GetCollection<ActivityLog>("ActivityLog");

                        ActivityLog activityLog = new ActivityLog()
                        {
                            id = ObjectId.GenerateNewId().ToString(),
                            requestId = request.id,
                            userId = request.userId,
                            userName = request.userName,
                            dateTime = DateTime.UtcNow,
                            activity = !String.IsNullOrEmpty(activityLogId)
                                ? string.Format(ActivityStatus.RerequestedBy, request.userName)
                                : string.Format(ActivityStatus.RequestedBy, request.userName),
                            typeId = item.typeId,
                            docId = item.id,
                            docName = item.displayName,
                            loanId = loanApplication.id,
                            message = item.message,
                            log = new List<Log>() { }
                        };
                        await collectionInsertActivityLog.InsertOneAsync(activityLog);

                        await activityLogService.InsertLog(activityLog.id, string.Format(ActivityStatus.StatusChanged, DocumentStatus.BorrowerTodo));
                    }
                }
            }

            IMongoCollection<Entity.Request> collectionDeleteDraftRequest = mongoService.db.GetCollection<Entity.Request>("Request");

            UpdateResult resultDeleteDraftRequest = await collectionDeleteDraftRequest.UpdateOneAsync(new BsonDocument()
            {
                { "loanApplicationId", loanApplication.loanApplicationId}
            }
                , new BsonDocument()
                {
                { "$pull", new BsonDocument()
                    {
                        { "requests",
                           new BsonDocument(){{ "status", RequestStatus.Draft}}
                        }
                    }
                }
                });

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
            }
            //Get In draft documents

            IMongoCollection<Entity.Request> collectionDocumentDraft = mongoService.db.GetCollection<Entity.Request>("Request");

            using var asyncCursorDocumentDraft = collectionDocumentDraft.Aggregate(PipelineDefinition<Entity.Request, BsonDocument>.Create(
             @"{""$match"": {
                  ""loanApplicationId"": " + loanApplication.loanApplicationId + @" 
                            }
                        }",
                       @"{
                            ""$unwind"": ""$requests""
                        }",
                        @"{
                            ""$unwind"": ""$requests.documents""
                        }",
                        @"{
                            ""$match"": {
                                ""requests.documents.status"": """ + DocumentStatus.Draft + @""",
                            }
                        }",
                       @"{
                            ""$project"": {
                                ""_id"": 1,                               
                                ""docId"": ""$requests.documents.id"",
                                ""requestId"": ""$requests.id""
                                }
                         } "

               ));

            while (await asyncCursorDocumentDraft.MoveNextAsync())
            {

                foreach (var current in asyncCursorDocumentDraft.Current)
                {
                    InDraftDocumentQuery query = BsonSerializer.Deserialize<InDraftDocumentQuery>(current);
                    if(!loanApplication.requests[0].documents.Any(x => x.docId == query.docId && x.requestId == query.requestId))
                    {
                        IMongoCollection<Entity.Request> collectionDraftStatus = mongoService.db.GetCollection<Entity.Request>("Request");

                        UpdateResult resultDraftStatus = await collectionDraftStatus.UpdateOneAsync(new BsonDocument()
                         {
                             { "_id", BsonObjectId.Create(query._id) }
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
                                 new JsonArrayFilterDefinition<Entity.Request>("{ \"request.id\": "+new ObjectId(query.requestId).ToJson()+"}"),
                                 new JsonArrayFilterDefinition<Entity.Request>("{ \"document.id\": "+new ObjectId(query.docId).ToJson()+"}")
                             }
                         });

                        string activityLogId = await activityLogService.GetActivityLogId(query._id, query.requestId, query.docId);

                        await activityLogService.InsertLog(activityLogId, string.Format(ActivityStatus.StatusChanged, DocumentStatus.PendingReview));
                    }
                }
            }
            return true;
        }

        public async Task<List<DraftDocumentDTO>> GetDraft(int loanApplicationId, int tenantId)
        {
            IMongoCollection<Entity.Request> collectionRequest = mongoService.db.GetCollection<Entity.Request>("Request");
            IMongoCollection<Entity.Request> collectionDocumentDraft = mongoService.db.GetCollection<Entity.Request>("Request");
            List<DraftDocumentDTO> result = new List<DraftDocumentDTO>();

            using var asyncCursorDocumentDraft = collectionDocumentDraft.Aggregate(PipelineDefinition<Entity.Request, BsonDocument>.Create(
             @"{""$match"": {
                  ""loanApplicationId"": " + loanApplicationId + @" 
                            }
                        }",
                       @"{
                            ""$unwind"": ""$requests""
                        }",
                        @"{
                            ""$unwind"": ""$requests.documents""
                        }",
                        @"{
                            ""$match"": {
                                ""requests.documents.status"": """ + DocumentStatus.Draft + @""",
                            }
                        }",
                        @"{
                            ""$lookup"": {
                                ""from"": ""DocumentType"",
                                ""localField"": ""requests.documents.typeId"",
                                ""foreignField"": ""_id"",
                                ""as"": ""documentObjects""
                            }
                        }", @"{
                            ""$unwind"": {
                                ""path"": ""$documentObjects"",
                                ""preserveNullAndEmptyArrays"": true
                            }
                        }",
                       @"{
                            ""$project"": {
                                ""_id"": 0,                               
                                ""message"": ""$requests.message"",
                                ""typeId"": ""$requests.documents.typeId"",
                                ""docId"": ""$requests.documents.id"",
                                ""requestId"": ""$requests.id"",
                                ""docName"": ""$requests.documents.displayName"",
                                ""docMessage"": ""$requests.documents.message"",
                                ""typeName"": ""$documentObjects.name"",
                                ""typeMessage"": ""$documentObjects.message"",
                                ""messages"": ""$documentObjects.messages""
                                }
                         } "

               ));

            while (await asyncCursorDocumentDraft.MoveNextAsync())
            {

                foreach (var current in asyncCursorDocumentDraft.Current)
                {
                    DraftDocumentQuery query = BsonSerializer.Deserialize<DraftDocumentQuery>(current);
                    DraftDocumentDTO dto = new DraftDocumentDTO();
                    dto.message = query.message;
                    dto.typeId = query.typeId;
                    dto.docId = query.docId;
                    dto.requestId = query.requestId;
                    dto.docName = string.IsNullOrEmpty(query.docName) ? query.typeName : query.docName;
                    if (string.IsNullOrEmpty(query.docMessage))
                    {
                        if (query.messages?.Any(x => x.tenantId == tenantId) == true)
                        {
                            dto.docMessage = query.messages.Where(x => x.tenantId == tenantId).First().message;
                        }
                        else
                        {
                            dto.docMessage = query.typeMessage;
                        }
                    }
                    else
                    {
                        dto.docMessage = query.docMessage;
                    }
                    result.Add(dto);
                }
            }

            using var asyncCursor = collectionRequest.Aggregate(PipelineDefinition<Entity.Request, BsonDocument>.Create(
              @"{""$match"": {
                  ""loanApplicationId"": " + loanApplicationId + @" 
                            }
                        }",
                        @"{
                            ""$unwind"": ""$requests""
                        }",
                        @"{
                            ""$match"": {
                                ""requests.status"": """ + RequestStatus.Draft + @""",
                            }
                        }",
                         @"{
                            ""$unwind"": ""$requests.documents""
                        }",
                         @"{
                            ""$lookup"": {
                                ""from"": ""DocumentType"",
                                ""localField"": ""requests.documents.typeId"",
                                ""foreignField"": ""_id"",
                                ""as"": ""documentObjects""
                            }
                        }", @"{
                            ""$unwind"": {
                                ""path"": ""$documentObjects"",
                                ""preserveNullAndEmptyArrays"": true
                            }
                        }",
                        @"{
                            ""$project"": {
                                ""_id"": 0,                               
                                ""message"": ""$requests.message"",
                                ""typeId"": ""$requests.documents.typeId"",
                                ""docName"": ""$requests.documents.displayName"",
                                ""docMessage"": ""$requests.documents.message"",
                                ""typeName"": ""$documentObjects.name"",
                                ""typeMessage"": ""$documentObjects.message"",
                                ""messages"": ""$documentObjects.messages""
                                }
                         } "

                ));

            while (await asyncCursor.MoveNextAsync())
            {

                foreach (var current in asyncCursor.Current)
                {
                    DraftDocumentQuery query = BsonSerializer.Deserialize<DraftDocumentQuery>(current);
                    DraftDocumentDTO dto = new DraftDocumentDTO();
                    dto.message = query.message;
                    dto.typeId = query.typeId;
                    dto.docName = string.IsNullOrEmpty(query.docName) ? query.typeName : query.docName;
                    if (string.IsNullOrEmpty(query.docMessage))
                    {
                        if (query.messages?.Any(x => x.tenantId == tenantId) == true)
                        {
                            dto.docMessage = query.messages.Where(x => x.tenantId == tenantId).First().message;
                        }
                        else
                        {
                            dto.docMessage = query.typeMessage;
                        }
                    }
                    else
                    {
                        dto.docMessage = query.docMessage;
                    }
                    result.Add(dto);
                }


            }

            return result;
        }

        public async Task<string> GetEmailTemplate(int tenantId)
        {
            IMongoCollection<Tenant> collection = mongoService.db.GetCollection<Tenant>("Tenant");

            using var asyncCursor = collection.Aggregate(PipelineDefinition<Tenant, BsonDocument>.Create(
                @"{""$match"": {
                  ""tenantId"": " + tenantId + @"}
                        }", @"{
                            ""$project"": {
                                ""_id"": 0,
                                ""emailTemplate"": ""$emailTemplate""
                            }
                        }"
                ));

            while (await asyncCursor.MoveNextAsync())
            {
                string emailTemplate = string.Empty;
                if (asyncCursor.Current.Count() > 0)
                {
                    EmailTemplateQuery query = BsonSerializer.Deserialize<EmailTemplateQuery>(asyncCursor.Current.First());
                    emailTemplate = query.emailTemplate;
                }
                return emailTemplate;
            }
            return string.Empty;
        }
    }
}
