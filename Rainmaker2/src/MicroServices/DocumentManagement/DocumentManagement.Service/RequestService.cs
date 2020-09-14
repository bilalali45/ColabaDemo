using DocumentManagement.Entity;
using DocumentManagement.Model;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Service
{
    public class RequestService : IRequestService
    {
        private readonly IMongoService mongoService;
        private readonly IActivityLogService activityLogService;
        private readonly IConfiguration config;
        private readonly ISettingService settingService;
        private readonly IFtpClient ftpClient;
        private readonly IKeyStoreService keyStoreService;
        private readonly IFileEncryptionFactory fileEncryptionFactory;
        private readonly IRainmakerService rainmakerService;
        public RequestService(IMongoService mongoService,
            IActivityLogService activityLogService,
            IConfiguration config,
            ISettingService settingService,
            IFtpClient ftpClient,
            IKeyStoreService keyStoreService,
            IFileEncryptionFactory fileEncryptionFactory,
            IRainmakerService rainmakerService)
        {
            this.mongoService = mongoService;
            this.activityLogService = activityLogService;
            this.config = config;
            this.settingService = settingService;
            this.ftpClient = ftpClient;
            this.keyStoreService = keyStoreService;
            this.fileEncryptionFactory = fileEncryptionFactory;
            this.rainmakerService = rainmakerService;
        }
        public async Task<string> UploadFile(int userProfileId, string userName, int tenantId, int custUserId, string custUserName, UploadFileModel model, IEnumerable<string> authHeader)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(model.fileName, out contentType))
            {
                contentType = "application/octet-stream";
            }

            using MemoryStream memoryStream = new MemoryStream();
            byte[] bytes = Convert.FromBase64String(model.fileData);
            memoryStream.Write(bytes,0,bytes.Length);
            memoryStream.Position = 0;

            var algo = config[key: "File:Algo"];
            var key = config[key: "File:Key"];
            var setting = await settingService.GetSetting();

            ftpClient.Setup(hostIp: setting.ftpServer,
                            userName: setting.ftpUser,
                            password: AesCryptography.Decrypt(text: setting.ftpPassword,
                                                              key: await keyStoreService.GetFtpKey()));
            var filePath = fileEncryptionFactory.GetEncryptor(name: algo).EncryptFile(inputFile: memoryStream,
                                                                                              password: await keyStoreService.GetFileKey());
            // upload to ftp
            await ftpClient.UploadAsync(remoteFile: Path.GetFileName(path: filePath),
                                        localFile: filePath);
            // get document upload status
            string status=string.Empty;
            IMongoCollection<Entity.StatusList> collection =
                mongoService.db.GetCollection<Entity.StatusList>("StatusList");

            using var asyncCursorStatus = collection.Aggregate(
                PipelineDefinition<Entity.StatusList, BsonDocument>.Create(
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
                    status = query._id;
                }
            }

            // check whether loan application already exists
            IMongoCollection<Entity.Request> collectionRequest =
                mongoService.db.GetCollection<Entity.Request>("Request");

            using var asyncCursorRequest = collectionRequest.Aggregate(
                PipelineDefinition<Entity.Request, BsonDocument>.Create(
                    @"{""$match"": {
                  ""loanApplicationId"": " + model.loanApplicationId + @"
                            }
                        }", @"{
                            ""$project"": {
                                ""loanApplicationId"": 1
                            }
                        }"
                ));
            // if loan application does not exists create loan application
          
            if (await asyncCursorRequest.MoveNextAsync())
            {
                int loanApplicationId = -1;
                foreach (var current in asyncCursorRequest.Current)
                {
                    LoanApplicationIdQuery query = BsonSerializer.Deserialize<LoanApplicationIdQuery>(current);
                    loanApplicationId = query.loanApplicationId;
                }

                if (loanApplicationId != model.loanApplicationId)
                {
                    IMongoCollection<Entity.LoanApplication> collectionLoanApplication =
                        mongoService.db.GetCollection<Entity.LoanApplication>("Request");
                    Entity.LoanApplication loanApplicationModel = new Entity.LoanApplication()
                    {
                        id = ObjectId.GenerateNewId().ToString(),
                        loanApplicationId = model.loanApplicationId,
                        tenantId = tenantId,
                        status = status,
                        userId = custUserId,
                        userName = custUserName,
                        requests = new List<Entity.Request>() { }
                    };
                    await collectionLoanApplication.InsertOneAsync(loanApplicationModel);
                }
            }

            IMongoCollection<Entity.Request> collectionInsertRequest = mongoService.db.GetCollection<Entity.Request>("Request");

            BsonArray documentBsonArray = new BsonArray();
            BsonArray filesArray = new BsonArray();
            ObjectId fileId = ObjectId.GenerateNewId();
            BsonDocument fileDocument = new BsonDocument() { 
                { "id", fileId },
                { "clientName", model.fileName },
                { "serverName", Path.GetFileName(path: filePath) },
                { "fileUploadedOn", BsonDateTime.Create(DateTime.UtcNow) },
                { "size", memoryStream.Length },
                { "encryptionKey", key },
                { "encryptionAlgorithm", algo },
                { "order", 0 },
                { "mcuName", BsonString.Empty },
                { "contentType", contentType },
                { "status", FileStatus.SubmittedToMcu },
                { "byteProStatus", ByteProStatus.Synchronized },
                { "isRead", true }
            };

            filesArray.Add(fileDocument);

            BsonDocument bsonDocument = new BsonDocument();
            bsonDocument.Add("id", ObjectId.GenerateNewId());
            bsonDocument.Add("status", DocumentStatus.Completed);
            bsonDocument.Add("typeId", BsonNull.Value);
            bsonDocument.Add("displayName", model.documentType);
            bsonDocument.Add("message", BsonString.Empty);
            bsonDocument.Add("isRejected",false);
            bsonDocument.Add("files", filesArray);
            documentBsonArray.Add(bsonDocument);

            BsonDocument bsonElements = new BsonDocument();
            bsonElements.Add("id", ObjectId.GenerateNewId());
            bsonElements.Add("userId", userProfileId);
            bsonElements.Add("userName", userName);
            bsonElements.Add("createdOn", DateTime.UtcNow);
            bsonElements.Add("status", RequestStatus.Active);
            bsonElements.Add("message", "");
            bsonElements.Add("documents", documentBsonArray);

            UpdateResult result = await collectionInsertRequest.UpdateOneAsync(new BsonDocument()
                    {
                        { "loanApplicationId", model.loanApplicationId}
                    }, new BsonDocument()
                    {
                        { "$push", new BsonDocument()
                            {
                                { "requests", bsonElements  }
                            }
                        },
                    }
            );
            await rainmakerService.UpdateLoanInfo(model.loanApplicationId,"", authHeader);
            if (result.ModifiedCount > 0)
                return fileId.ToString();
            return null;
        }
        public async Task<bool> Save(Model.LoanApplication loanApplication, bool isDraft, IEnumerable<string> authHeader)
        {
            // get document upload status
            IMongoCollection<Entity.StatusList> collection =
                mongoService.db.GetCollection<Entity.StatusList>("StatusList");

            using var asyncCursorStatus = collection.Aggregate(
                PipelineDefinition<Entity.StatusList, BsonDocument>.Create(
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

                    await collectionDraft.UpdateOneAsync(new BsonDocument()
                    {
                        { "_id", BsonObjectId.Create(loanApplication.id) }
                    }, new BsonDocument()
                    {
                        { "$set", new BsonDocument()
                            {
                                { "requests.$[request].documents.$[document].message", item.message},
                                { "requests.$[request].message", loanApplication.requests[0].message}
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

                         await collectionDraftStatus.UpdateOneAsync(new BsonDocument()
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
                            activity = ActivityStatus.RerequestedBy,
                            typeId = string.IsNullOrEmpty(item.typeId)?null:item.typeId,
                            docId = item.docId,
                            docName = item.displayName,
                            loanId = loanApplication.id,
                            message = item.message,
                            log = new List<Log>() { }
                        };
                        await collectionInsertActivityLog.InsertOneAsync(activityLog);

                        await activityLogService.InsertLog(activityLog.id, string.Format(ActivityStatus.StatusChanged, DocumentStatus.BorrowerTodo));

                        IMongoCollection<Entity.EmailLog> collectionEmail = mongoService.db.GetCollection<Entity.EmailLog>("EmailLog");

                        Entity.EmailLog emailLog = new Entity.EmailLog() { id = ObjectId.GenerateNewId().ToString(),requestId = item.requestId,docId = item.docId, userId = request.userId, userName = request.userName, dateTime = DateTime.UtcNow, emailText = request.message, loanId = loanApplication.id, message = ActivityStatus.RerequestedBy,typeId = string.IsNullOrEmpty(item.typeId) ? null : item.typeId,docName = item.displayName };
                        await collectionEmail.InsertOneAsync(emailLog);
                    }
                }
                else
                {
                    BsonDocument bsonDocument = new BsonDocument();

                    item.id = ObjectId.GenerateNewId().ToString();
                    item.status = DocumentStatus.BorrowerTodo;

                    bsonDocument.Add("id", new ObjectId(item.id));
                    bsonDocument.Add("status", item.status);
                    bsonDocument.Add("typeId", string.IsNullOrEmpty(item.typeId) ? (BsonValue)BsonNull.Value : new BsonObjectId(new ObjectId(item.typeId)));
                    bsonDocument.Add("displayName", item.displayName);
                    bsonDocument.Add("message", item.message);
                    bsonDocument.Add("isRejected", false);
                    bsonDocument.Add("files", new BsonArray());

                    //Add document
                    documentBsonArray.Add(bsonDocument);
                    if (!isDraft)
                    {
                        IMongoCollection<ActivityLog> collectionInsertActivityLog =
                            mongoService.db.GetCollection<ActivityLog>("ActivityLog");

                        ActivityLog activityLog = new ActivityLog()
                        {
                            id = ObjectId.GenerateNewId().ToString(),
                            requestId = request.id,
                            userId = request.userId,
                            userName = request.userName,
                            dateTime = DateTime.UtcNow,
                            activity = ActivityStatus.RequestedBy,
                            typeId = string.IsNullOrEmpty(item.typeId) ? null : item.typeId,
                            docId = item.id,
                            docName = item.displayName,
                            loanId = loanApplication.id,
                            message = item.message,
                            log = new List<Log>() { }
                        };
                        await collectionInsertActivityLog.InsertOneAsync(activityLog);

                        await activityLogService.InsertLog(activityLog.id, string.Format(ActivityStatus.StatusChanged, DocumentStatus.BorrowerTodo));

                        IMongoCollection<Entity.EmailLog> collectionEmail = mongoService.db.GetCollection<Entity.EmailLog>("EmailLog");

                        Entity.EmailLog emailLog = new Entity.EmailLog() { id = ObjectId.GenerateNewId().ToString(), requestId = request.id,docId = item.id , userId = request.userId, userName = request.userName, dateTime = DateTime.UtcNow, emailText = request.message, loanId = loanApplication.id, message = ActivityStatus.RequestedBy, typeId = string.IsNullOrEmpty(item.typeId) ? null : item.typeId, docName = item.displayName };
                        await collectionEmail.InsertOneAsync(emailLog);
                    }
                }
            }

            IMongoCollection<Entity.Request> collectionDeleteDraftRequest = mongoService.db.GetCollection<Entity.Request>("Request");

            await collectionDeleteDraftRequest.UpdateOneAsync(new BsonDocument()
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

                        await collectionDraftStatus.UpdateOneAsync(new BsonDocument()
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
            if(!isDraft)
            {
                await rainmakerService.UpdateLoanInfo(loanApplication.loanApplicationId, "", authHeader);
            }
            return true;
        }

        public async Task<List<DraftDocumentDto>> GetDraft(int loanApplicationId, int tenantId)
        {
            IMongoCollection<Entity.Request> collectionRequest = mongoService.db.GetCollection<Entity.Request>("Request");
            IMongoCollection<Entity.Request> collectionDocumentDraft = mongoService.db.GetCollection<Entity.Request>("Request");
            List<DraftDocumentDto> result = new List<DraftDocumentDto>();
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
                    DraftDocumentDto dto = new DraftDocumentDto();
                    dto.message = query.message;
                    dto.typeId = query.typeId;
                    dto.docName = string.IsNullOrEmpty(query.docName) ? query.typeName : query.docName;
                    if (string.IsNullOrEmpty(query.docMessage))
                    {
                        if (query.messages?.Any(x => x.tenantId == tenantId) == true)
                        {
                            dto.docMessage = query.messages.First(x => x.tenantId == tenantId).message;
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
                    DraftDocumentDto dto = new DraftDocumentDto();
                    dto.message = query.message;
                    dto.typeId = query.typeId;
                    dto.docId = query.docId;
                    dto.requestId = query.requestId;
                    dto.docName = string.IsNullOrEmpty(query.docName) ? query.typeName : query.docName;
                    if (string.IsNullOrEmpty(query.docMessage))
                    {
                        if (query.messages?.Any(x => x.tenantId == tenantId) == true)
                        {
                            dto.docMessage = query.messages.First(x => x.tenantId == tenantId).message;
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

            if (await asyncCursor.MoveNextAsync())
            {
                string emailTemplate = string.Empty;
                if (asyncCursor.Current.Any())
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
