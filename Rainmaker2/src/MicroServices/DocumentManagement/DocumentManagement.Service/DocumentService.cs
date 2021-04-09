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
    public class DocumentService : IDocumentService
    {
        private readonly IMongoService mongoService;
        private readonly IActivityLogService activityLogService;
        private readonly IRainmakerService rainmakerService;
        public DocumentService(IMongoService mongoService, IActivityLogService activityLogService, IRainmakerService rainmakerService)
        {
            this.mongoService = mongoService;
            this.activityLogService = activityLogService;
            this.rainmakerService = rainmakerService;
        }
        public async Task<string> CreateLoanApplication(int loanApplicationId, int tenantId, int userId, string userName)
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
            string status=null;
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
                  ""loanApplicationId"": " + loanApplicationId + @"
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
                foreach (var current in asyncCursorRequest.Current)
                {
                    LoanApplicationIdQuery query = BsonSerializer.Deserialize<LoanApplicationIdQuery>(current);
                    return query._id;
                }
            }
            IMongoCollection<Entity.LoanApplication> collectionLoanApplication =
                mongoService.db.GetCollection<Entity.LoanApplication>("Request");
            Entity.LoanApplication loanApplicationModel = new Entity.LoanApplication()
            {
                id = ObjectId.GenerateNewId().ToString(),
                loanApplicationId = loanApplicationId,
                tenantId = tenantId,
                status = status,
                userId = userId,
                userName = userName,
                requests = new List<Entity.Request>() { }
            };
            await collectionLoanApplication.InsertOneAsync(loanApplicationModel);
            return loanApplicationModel.id;
        }
        public async Task<List<DocumentDto>> GetFiles(string id, string requestId, string docId)
        {
            IMongoCollection<Entity.Request> collection = mongoService.db.GetCollection<Entity.Request>("Request");

            using var asyncCursor = collection.Aggregate(PipelineDefinition<Entity.Request, BsonDocument>.Create(
              @"{""$match"": {

                  ""_id"": " + new ObjectId(id).ToJson() + @" 
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
                            ""$unwind"": {
                                ""path"": ""$documentObjects"",
                                ""preserveNullAndEmptyArrays"": true
                            }
                        }", @"{
                            ""$project"": {
                                ""_id"": 1,   
                               ""requestId"": ""$requests.id"",
                                ""docId"": ""$requests.documents.id"",
                                ""typeId"": ""$requests.documents.typeId"",
                                ""docName"": ""$requests.documents.displayName"",
                                ""files"": ""$requests.documents.files"",
                                ""mcuFiles"": ""$requests.documents.mcuFiles"",
                                ""userName"": 1,
                            }
                             } "

                ));


            List<DocumentDto> result = new List<DocumentDto>();
            while (await asyncCursor.MoveNextAsync())
            {
                foreach (var current in asyncCursor.Current)
                {
                    DocumentDetailQuery query = BsonSerializer.Deserialize<DocumentDetailQuery>(current);

                    DocumentDto dto = new DocumentDto();
                    dto.files = new List<DocumentFileDto>();
                    dto.id = query.id;
                    dto.docId = query.docId;
                    dto.typeId = query.typeId;
                    dto.docName = query.docName;
                    dto.requestId = query.requestId;
                    dto.userName = query.userName;
                    dto.docName = string.IsNullOrEmpty(query.docName) ? query.typeName : query.docName;
                    dto.files = query.files?.Where(x => x.status != FileStatus.RejectedByMcu && x.status != FileStatus.Deleted && (x.isMcuVisible == null || x.isMcuVisible == true)).Select(x => new DocumentFileDto()
                    {
                        isRead = x.isRead.HasValue ? x.isRead.Value : false,
                        fileId = x.id,
                        clientName = x.clientName,
                        fileUploadedOn = DateTime.SpecifyKind(x.fileUploadedOn, DateTimeKind.Utc),
                        mcuName = x.mcuName,
                        userId = x.userId,
                        userName = x.userName,
                        fileModifiedOn = x.fileModifiedOn == null ? null : (DateTime?)DateTime.SpecifyKind(x.fileModifiedOn.Value, DateTimeKind.Utc)
                    }).ToList();
                    if (query.mcuFiles != null)
                    {
                        var mcufiles = query.mcuFiles.Where(x => x.status != FileStatus.RejectedByMcu && x.status != FileStatus.Deleted).Select(x => new DocumentFileDto()
                        {
                            isRead = x.isRead.HasValue ? x.isRead.Value : false,
                            fileId = x.id,
                            clientName = x.clientName,
                            fileUploadedOn = DateTime.SpecifyKind(x.fileUploadedOn, DateTimeKind.Utc),
                            mcuName = x.mcuName,
                            userId = x.userId,
                            userName = x.userName,
                            fileModifiedOn = x.fileModifiedOn == null ? null : (DateTime?)DateTime.SpecifyKind(x.fileModifiedOn.Value, DateTimeKind.Utc)
                        }).ToList();
                        dto.files.AddRange(mcufiles);
                    }
                    dto.files= dto.files.OrderByDescending(x => (x.fileUploadedOn > (x.fileModifiedOn ?? DateTime.MinValue)) ? x.fileUploadedOn : x.fileModifiedOn).ToList();
                    result.Add(dto);
                }
            }

            return result;
        }
        public async Task<List<ActivityLogDto>> GetActivityLog(string id, string requestId, string docId)
        {
            IMongoCollection<Entity.ActivityLog> collection = mongoService.db.GetCollection<Entity.ActivityLog>("ActivityLog");
            string match = @"{""$match"": {
                  ""loanId"": " + new ObjectId(id).ToJson() + @", 
                  ""requestId"": " + new ObjectId(requestId).ToJson() + @", 
                  ""docId"": " + new ObjectId(docId).ToJson() + @", 
                            }
                        }";

            using var asyncCursor = collection.Aggregate(PipelineDefinition<Entity.ActivityLog, BsonDocument>.Create(
              match
                        , @"{
                            ""$project"": {
                                ""userId"": 1,                               
                                ""userName"":1,
                                ""dateTime"": 1,
                                ""_id"": 1 ,
                                ""typeId"": 1 ,
                                ""docId"": 1 ,
                                ""activity"": 1, 
                                ""loanId"": 1,
                                ""message"":1,
                                ""log"":1
                            }
                             } "

                ));


            List<ActivityLogDto> result = new List<ActivityLogDto>();
            while (await asyncCursor.MoveNextAsync())
            {
                foreach (var current in asyncCursor.Current)
                {
                    ActivityLogDto dto = new ActivityLogDto();
                    ActivityLogQuery query = BsonSerializer.Deserialize<ActivityLogQuery>(current);
                    dto.userId = query.userId;
                    dto.userName = query.userName;
                    dto.dateTime = DateTime.SpecifyKind(query.dateTime, DateTimeKind.Utc);
                    dto.activity = query.activity;
                    dto.id = query.id;
                    dto.typeId = query.typeId;
                    dto.docId = query.docId;
                    dto.loanId = query.loanId;
                    dto.message = query.message;
                    dto.log = query.log;
                    result.Add(dto);
                }
            }

            return result.OrderByDescending(x => x.dateTime).ToList();
        }
        public async Task<List<GetTemplateModel>> GetDocumentsByTemplateIds(List<string> id, int tenantId)
        {
            IMongoCollection<Entity.Template> collection = mongoService.db.GetCollection<Entity.Template>("Template");
            using var asyncCursor = collection.Aggregate(PipelineDefinition<Entity.Template, BsonDocument>.Create(
                @"{""$match"": { ""_id"": { ""$in"": " + new BsonArray(id.Select(x => new ObjectId(x))).ToJson() + @"
                              }}
                        }", @"{
                            ""$unwind"":{ ""path"": ""$documentTypes"",
                                ""preserveNullAndEmptyArrays"": true
                            }
                        }", @"{
                            ""$lookup"": {
                                ""from"": ""DocumentType"",
                                ""localField"": ""documentTypes.typeId"",
                                ""foreignField"": ""_id"",
                                ""as"": ""documents""
                            }
                        }", @"{
                            ""$unwind"": {
                                ""path"": ""$documents"",
                                ""preserveNullAndEmptyArrays"": true
                            }
                        }", @"{
                            ""$project"": {
                                ""_id"": 1,
                                ""name"":1,
                                ""typeId"": ""$documentTypes.typeId"",
                                ""typeName"": ""$documents.name"",
                                ""docMessage"": ""$documents.message"",
                                ""messages"": ""$documents.messages"",
                                ""docName"":""$documentTypes.docName""
                            }
                        }"
                ));

            List<GetTemplateModel> result = new List<GetTemplateModel>();
            while (await asyncCursor.MoveNextAsync())
            {
                foreach (var current in asyncCursor.Current)
                {
                    TempDocumentQuery query = BsonSerializer.Deserialize<TempDocumentQuery>(current);
                    GetTemplateModel dto;
                    if (result.Any(x => x.id == query.id))
                    {
                        dto = result.First(x => x.id == query.id);
                    }
                    else
                    {
                        dto = new GetTemplateModel();
                        dto.id = query.id;
                        dto.name = query.name;
                        dto.docs = new List<TemplateDocumentModel>();
                        result.Add(dto);
                    }
                    if (query.typeId == null && query.docName == null)
                        continue;
                    TemplateDocumentModel dto1 = new TemplateDocumentModel();
                    dto1.typeId = query.typeId;
                    dto1.docName = string.IsNullOrEmpty(query.docName) ? query.typeName : query.docName;
                    if (query.messages?.Any(x => x.tenantId == tenantId) == true)
                    {
                        dto1.docMessage = query.messages.First(x => x.tenantId == tenantId).message;
                    }
                    else
                    {
                        dto1.docMessage = query.docMessage;
                    }
                    dto.docs.Add(dto1);
                }
            }
            return result;
        }
        public async Task<List<EmailLogDto>> GetEmailLog(string id, string requestId, string docId)
        {
            IMongoCollection<Entity.EmailLog> collection = mongoService.db.GetCollection<Entity.EmailLog>("EmailLog");

            string match = @"{""$match"": {
                  ""loanId"": " + new ObjectId(id).ToJson() + @", 
                  ""requestId"": " + new ObjectId(requestId).ToJson() + @",
                  ""docId"": " + new ObjectId(docId).ToJson() + @"
                            }
                        }";

            using var asyncCursor = collection.Aggregate(PipelineDefinition<Entity.EmailLog, BsonDocument>.Create(
              match
                        , @"{
                            ""$project"": {
                                ""userId"": 1,                               
                                ""userName"":1,
                                ""dateTime"": 1,
                                ""_id"": 1 ,
                                ""emailText"": 1, 
                                ""loanId"": 1,
                                ""message"": 1,
                                ""fromAddress"": 1,
                                ""toAddress"": 1,
                                ""CCAddress"": 1,
                                ""subject"": 1
                            }
                           } "
                ));

            List<EmailLogDto> result = new List<EmailLogDto>();
            while (await asyncCursor.MoveNextAsync())
            {
                foreach (var current in asyncCursor.Current)
                {
                    EmailLogDto dto = new EmailLogDto();
                    EmailLogQuery query = BsonSerializer.Deserialize<EmailLogQuery>(current);
                    dto.userId = query.userId;
                    dto.userName = query.userName;
                    dto.dateTime = DateTime.SpecifyKind(query.dateTime, DateTimeKind.Utc);
                    dto.emailText = query.emailText;
                    dto.id = query.id;
                    dto.loanId = query.loanId;
                    dto.message = query.message;
                    dto.fromAddress = query.fromAddress;
                    dto.toAddress = query.toAddress;
                    dto.CCAddress = query.CCAddress;
                    dto.subject = query.subject;
                    result.Add(dto);
                }
            }

            return result.OrderByDescending(x => x.dateTime).ToList();
        }
        public async Task<bool> McuRename(string id, string requestId, string docId, string fileId, string newName, string userName)
        {
            IMongoCollection<Entity.Request> collection = mongoService.db.GetCollection<Entity.Request>("Request");

            UpdateResult result = await collection.UpdateOneAsync(new BsonDocument()
            {
                { "_id", BsonObjectId.Create(id) }
            }, new BsonDocument()
            {
                { "$set", new BsonDocument()
                    {
                        { "requests.$[request].documents.$[document].files.$[file].mcuName", newName}
                    }
                }
            }, new UpdateOptions()
            {
                ArrayFilters = new List<ArrayFilterDefinition>()
                {
                    new JsonArrayFilterDefinition<Entity.Request>("{ \"request.id\": "+new ObjectId( requestId).ToJson()+"}"),
                    new JsonArrayFilterDefinition<Entity.Request>("{ \"document.id\": "+new ObjectId( docId).ToJson()+"}"),
                    new JsonArrayFilterDefinition<Entity.Request>("{ \"file.id\": "+new ObjectId( fileId).ToJson()+"}")
                }
            });

            if (result.ModifiedCount == 1)
            {
                string activityLogId = await activityLogService.GetActivityLogId(id, requestId, docId);

                await activityLogService.InsertLog(activityLogId, string.Format(ActivityStatus.RenamedBy, userName, newName));
            }
            else
            {
                result = await McuRenameMcuFile(id, requestId, docId, fileId, newName, userName);
                if(result.ModifiedCount!=1)
                {
                    result = await McuRenameWorkbench(id, requestId, docId, fileId, newName, userName);
                }
            }
            return result.ModifiedCount == 1;
        }
        public async Task<UpdateResult> McuRenameMcuFile(string id, string requestId, string docId, string fileId, string newName, string userName)
        {
            IMongoCollection<Entity.Request> collection = mongoService.db.GetCollection<Entity.Request>("Request");

            UpdateResult result = await collection.UpdateOneAsync(new BsonDocument()
            {
                { "_id", BsonObjectId.Create(id) }
            }, new BsonDocument()
            {
                { "$set", new BsonDocument()
                    {
                        { "requests.$[request].documents.$[document].mcuFiles.$[file].mcuName", newName}
                    }
                }
            }, new UpdateOptions()
            {
                ArrayFilters = new List<ArrayFilterDefinition>()
                {
                    new JsonArrayFilterDefinition<Entity.Request>("{ \"request.id\": "+new ObjectId( requestId).ToJson()+"}"),
                    new JsonArrayFilterDefinition<Entity.Request>("{ \"document.id\": "+new ObjectId( docId).ToJson()+"}"),
                    new JsonArrayFilterDefinition<Entity.Request>("{ \"file.id\": "+new ObjectId( fileId).ToJson()+"}")
                }
            });

            if (result.ModifiedCount == 1)
            {
                string activityLogId = await activityLogService.GetActivityLogId(id, requestId, docId);

                await activityLogService.InsertLog(activityLogId, string.Format(ActivityStatus.RenamedBy, userName, newName));
            }
            
            return result;
        }
        public async Task<UpdateResult> McuRenameWorkbench(string id, string requestId, string docId, string fileId, string newName, string userName)
        {
            IMongoCollection<Entity.Request> collection = mongoService.db.GetCollection<Entity.Request>("Request");

            UpdateResult result = await collection.UpdateOneAsync(new BsonDocument()
            {
                { "_id", BsonObjectId.Create(id) }
            }, new BsonDocument()
            {
                { "$set", new BsonDocument()
                    {
                        { "workbench.$[file].mcuName", newName}
                    }
                }
            }, new UpdateOptions()
            {
                ArrayFilters = new List<ArrayFilterDefinition>()
                {
                    new JsonArrayFilterDefinition<Entity.Request>("{ \"file.id\": "+new ObjectId( fileId).ToJson()+"}")
                }
            });

            if (result.ModifiedCount == 1)
            {
                string activityLogId = await activityLogService.GetActivityLogId(id, requestId, docId);

                await activityLogService.InsertLog(activityLogId, string.Format(ActivityStatus.RenamedBy, userName, newName));
            }

            return result;
        }
        public async Task<bool> AcceptDocument(string id, string requestId, string docId, string userName, IEnumerable<string> authHeader)
        {
            IMongoCollection<Entity.Request> collection = mongoService.db.GetCollection<Entity.Request>("Request");
            UpdateResult result = await collection.UpdateOneAsync(new BsonDocument()
            {
                { "_id", BsonObjectId.Create(id) }
            }, new BsonDocument()
            {
                { "$set", new BsonDocument()
                    {
                        { "requests.$[request].documents.$[document].status", DocumentStatus.Completed},
                        { "requests.$[request].documents.$[document].isRejected", false}

                    }
                }
            }, new UpdateOptions()
            {
                ArrayFilters = new List<ArrayFilterDefinition>()
                {
                    new JsonArrayFilterDefinition<Entity.Request>("{ \"request.id\": "+new ObjectId(requestId).ToJson()+"}"),
                    new JsonArrayFilterDefinition<Entity.Request>("{ \"document.id\": "+new ObjectId(docId).ToJson()+"}")
                }

            });

            if (result.ModifiedCount == 1)
            {
                string activityLogId = await activityLogService.GetActivityLogId(id, requestId, docId);

                await activityLogService.InsertLog(activityLogId, string.Format(ActivityStatus.AcceptedBy, userName));

                await activityLogService.InsertLog(activityLogId, string.Format(ActivityStatus.StatusChanged, DocumentStatus.Completed));
            }

            await rainmakerService.UpdateLoanInfo(null, id, authHeader);

            return result.ModifiedCount == 1;
        }
        public async Task<bool> RejectDocument(string id, string requestId, string docId, string message, int userId, string userName, IEnumerable<string> authHeader)
        {
            IMongoCollection<Entity.Request> collection = mongoService.db.GetCollection<Entity.Request>("Request");
            UpdateResult result = await collection.UpdateOneAsync(new BsonDocument()
            {
                { "_id", BsonObjectId.Create(id) }
            }, new BsonDocument()
            {
                { "$set", new BsonDocument()
                    {
                        { "requests.$[request].documents.$[document].status", DocumentStatus.Draft},
                        { "requests.$[request].documents.$[document].isRejected", true},
                        { "requests.$[request].documents.$[document].message", message},
                        { "requests.$[request].message", BsonString.Empty}
                    }
                }
            }, new UpdateOptions()
            {
                ArrayFilters = new List<ArrayFilterDefinition>()
                {
                    new JsonArrayFilterDefinition<Entity.Request>("{ \"request.id\": "+new ObjectId(requestId).ToJson()+"}"),
                    new JsonArrayFilterDefinition<Entity.Request>("{ \"document.id\": "+new ObjectId(docId).ToJson()+"}")
                }

            });

            if (result.ModifiedCount == 1)
            {
                string activityLogId = await activityLogService.GetActivityLogId(id, requestId, docId);

                await activityLogService.InsertLog(activityLogId, string.Format(ActivityStatus.RejectedBy, userName));
            }

            await rainmakerService.UpdateLoanInfo(null, id, authHeader);

            return result.ModifiedCount == 1;
        }
        public async Task<FileViewDto> View(AdminFileViewModel model, int userProfileId, string ipAddress, int tenantId)
        {
            IMongoCollection<Entity.Request> collection = mongoService.db.GetCollection<Entity.Request>("Request");

            using var asyncCursor = collection.Aggregate(PipelineDefinition<Entity.Request, BsonDocument>.Create(
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
                                ""_id"": 0,                               
                                ""serverName"": ""$requests.documents.files.serverName"",
                                ""encryptionKey"": ""$requests.documents.files.encryptionKey"",
                                ""encryptionAlgorithm"": ""$requests.documents.files.encryptionAlgorithm"",
                                ""clientName"": ""$requests.documents.files.clientName"",
                                ""mcuName"": ""$requests.documents.files.mcuName"",
                                ""contentType"": ""$requests.documents.files.contentType"",
                                ""salt"": ""$requests.documents.files.salt""
                            }
                             } "

                ));

            FileViewDto fileViewDTO = null;
            if (await asyncCursor.MoveNextAsync())
            {
                if (asyncCursor.Current.FirstOrDefault() != null)
                    fileViewDTO = BsonSerializer.Deserialize<FileViewDto>(asyncCursor.Current.FirstOrDefault());
            }
            if (fileViewDTO != null)
            {
                IMongoCollection<ViewLog> viewLogCollection = mongoService.db.GetCollection<ViewLog>("ViewLog");

                ViewLog viewLog = new ViewLog() { userProfileId = userProfileId, createdOn = DateTime.UtcNow, ipAddress = ipAddress, loanApplicationId = model.id, requestId = model.requestId, documentId = model.docId, fileId = model.fileId };
                await viewLogCollection.InsertOneAsync(viewLog);
                // mark as read
                await collection.UpdateOneAsync(new BsonDocument()
                {
                    { "_id", BsonObjectId.Create(model.id) }
                }, new BsonDocument()
                {
                    { "$set", new BsonDocument()
                        {
                            { "requests.$[request].documents.$[document].files.$[file].isRead", true}
                        }
                    }
                }, new UpdateOptions()
                {
                    ArrayFilters = new List<ArrayFilterDefinition>()
                    {
                        new JsonArrayFilterDefinition<Entity.Request>("{ \"request.id\": "+new ObjectId(model.requestId).ToJson()+"}"),
                        new JsonArrayFilterDefinition<Entity.Request>("{ \"document.id\": "+new ObjectId(model.docId).ToJson()+"}"),
                        new JsonArrayFilterDefinition<Entity.Request>("{ \"file.id\": "+new ObjectId(model.fileId).ToJson()+"}")
                    }
                });
            }
            if (fileViewDTO == null)
            {
               fileViewDTO= await McuFileView(model, userProfileId, ipAddress, tenantId);
            }
            if(fileViewDTO == null)
            {
                fileViewDTO = await WorkbenchFileView(model, userProfileId, ipAddress, tenantId);
            }
            if (fileViewDTO == null)
            {
                fileViewDTO = await TrashFileView(model, userProfileId, ipAddress, tenantId);
            }
            return fileViewDTO;
        }
        public async Task<FileViewDto> WorkbenchFileView(AdminFileViewModel model, int userProfileId, string ipAddress, int tenantId)
        {
            IMongoCollection<Entity.Request> collection = mongoService.db.GetCollection<Entity.Request>("Request");

            using var asyncCursor = collection.Aggregate(PipelineDefinition<Entity.Request, BsonDocument>.Create(
              @"{""$match"": {

                  ""_id"": " + new ObjectId(model.id).ToJson() + @" ,
                  ""tenantId"": " + tenantId + @"
                            }
                        }",
                        @"{
                            ""$unwind"": ""$workbench""
                        }",
                        @"{
                            ""$match"": {
                                ""workbench.id"": " + new ObjectId(model.fileId).ToJson() + @"
                            }
                        }",
                        @"{
                            ""$project"": {
                                ""_id"": 0,                               
                                ""serverName"": ""$workbench.serverName"",
                                ""encryptionKey"": ""$workbench.encryptionKey"",
                                ""encryptionAlgorithm"": ""$workbench.encryptionAlgorithm"",
                                ""clientName"": ""$workbench.clientName"",
                                ""contentType"": ""$workbench.contentType"",
                                ""salt"": ""$workbench.salt"",
                                ""mcuName"": ""$workbench.mcuName""
                            }
                             } "

                ));


            FileViewDto fileViewDTO = null;
            if (await asyncCursor.MoveNextAsync())
            {
                if (asyncCursor.Current.FirstOrDefault() != null)
                    fileViewDTO = BsonSerializer.Deserialize<FileViewDto>(asyncCursor.Current.FirstOrDefault());
            }
            
            return fileViewDTO;
        }
        public async Task<FileViewDto> TrashFileView(AdminFileViewModel model, int userProfileId, string ipAddress, int tenantId)
        {
            IMongoCollection<Entity.Request> collection = mongoService.db.GetCollection<Entity.Request>("Request");

            using var asyncCursor = collection.Aggregate(PipelineDefinition<Entity.Request, BsonDocument>.Create(
              @"{""$match"": {

                  ""_id"": " + new ObjectId(model.id).ToJson() + @" ,
                  ""tenantId"": " + tenantId + @"
                            }
                        }",
                        @"{
                            ""$unwind"": ""$trash""
                        }",
                        @"{
                            ""$match"": {
                                ""trash.id"": " + new ObjectId(model.fileId).ToJson() + @"
                            }
                        }",
                        @"{
                            ""$project"": {
                                ""_id"": 0,                               
                                ""serverName"": ""$trash.serverName"",
                                ""encryptionKey"": ""$trash.encryptionKey"",
                                ""encryptionAlgorithm"": ""$trash.encryptionAlgorithm"",
                                ""clientName"": ""$trash.clientName"",
                                ""mcuName"": ""$trash.mcuName"",
                                ""contentType"": ""$trash.contentType"",
                                ""salt"": ""$trash.salt""
                            }
                             } "

                ));


            FileViewDto fileViewDTO = null;
            if (await asyncCursor.MoveNextAsync())
            {
                if (asyncCursor.Current.FirstOrDefault() != null)
                    fileViewDTO = BsonSerializer.Deserialize<FileViewDto>(asyncCursor.Current.FirstOrDefault());
            }

            return fileViewDTO;
        }
        public async Task<FileViewDto> McuFileView(AdminFileViewModel model, int userProfileId, string ipAddress, int tenantId)
        {
            IMongoCollection<Entity.Request> collection = mongoService.db.GetCollection<Entity.Request>("Request");

            using var asyncCursor = collection.Aggregate(PipelineDefinition<Entity.Request, BsonDocument>.Create(
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
                            ""$unwind"": ""$requests.documents.mcuFiles""
                        }",

                        @"{
                            ""$match"": {
                                ""requests.documents.mcuFiles.id"": " + new ObjectId(model.fileId).ToJson() + @"
                            }
                        }",

                        @"{
                            ""$project"": {
                                ""_id"": 0,                               
                                ""serverName"": ""$requests.documents.mcuFiles.serverName"",
                                ""encryptionKey"": ""$requests.documents.mcuFiles.encryptionKey"",
                                ""encryptionAlgorithm"": ""$requests.documents.mcuFiles.encryptionAlgorithm"",
                                ""clientName"": ""$requests.documents.mcuFiles.clientName"",
                                ""mcuName"": ""$requests.documents.mcuFiles.mcuName"",
                                ""contentType"": ""$requests.documents.mcuFiles.contentType"",
                                ""salt"": ""$requests.documents.mcuFiles.salt""
                            }
                             } "

                ));


            FileViewDto fileViewDTO = null;
            if (await asyncCursor.MoveNextAsync())
            {
                if (asyncCursor.Current.FirstOrDefault() != null)
                    fileViewDTO = BsonSerializer.Deserialize<FileViewDto>(asyncCursor.Current.FirstOrDefault());
            }
            if (fileViewDTO != null)
            {
                IMongoCollection<ViewLog> viewLogCollection = mongoService.db.GetCollection<ViewLog>("ViewLog");

                ViewLog viewLog = new ViewLog() { userProfileId = userProfileId, createdOn = DateTime.UtcNow, ipAddress = ipAddress, loanApplicationId = model.id, requestId = model.requestId, documentId = model.docId, fileId = model.fileId };
                await viewLogCollection.InsertOneAsync(viewLog);
                // mark as read
                await collection.UpdateOneAsync(new BsonDocument()
                {
                    { "_id", BsonObjectId.Create(model.id) }
                }, new BsonDocument()
                {
                    { "$set", new BsonDocument()
                        {
                            { "requests.$[request].documents.$[document].mcuFiles.$[file].isRead", true}
                        }
                    }
                }, new UpdateOptions()
                {
                    ArrayFilters = new List<ArrayFilterDefinition>()
                    {
                        new JsonArrayFilterDefinition<Entity.Request>("{ \"request.id\": "+new ObjectId(model.requestId).ToJson()+"}"),
                        new JsonArrayFilterDefinition<Entity.Request>("{ \"document.id\": "+new ObjectId(model.docId).ToJson()+"}"),
                        new JsonArrayFilterDefinition<Entity.Request>("{ \"file.id\": "+new ObjectId(model.fileId).ToJson()+"}")
                    }
                });
            }
            return fileViewDTO;
        }



        public async Task<bool> UpdateByteProStatus(string id, string requestId, string docId, string fileId, bool isUploaded, int userId, int tenantId)
        {
            var status = isUploaded ? ByteProStatus.Synchronized : ByteProStatus.Error;
            IMongoCollection<Entity.Request> collection = mongoService.db.GetCollection<Entity.Request>("Request");
            UpdateResult result = await collection.UpdateOneAsync(new BsonDocument()
            {
                { "_id", BsonObjectId.Create(id) }
            }, new BsonDocument()
            {
                { "$set", new BsonDocument()
                    {
                        { "requests.$[request].documents.$[document].files.$[file].byteProStatus", status}

                    }
                }
            }, new UpdateOptions()
            {
                ArrayFilters = new List<ArrayFilterDefinition>()
                {
                    new JsonArrayFilterDefinition<Entity.Request>("{ \"request.id\": "+new ObjectId(requestId).ToJson()+"}"),
                    new JsonArrayFilterDefinition<Entity.Request>("{ \"document.id\": "+new ObjectId(docId).ToJson()+"}"),
                    new JsonArrayFilterDefinition<Entity.Request>("{ \"file.id\": "+new ObjectId(fileId).ToJson()+"}")
                }

            });
             
            IMongoCollection<Entity.ByteProLog> collectionBytePro = mongoService.db.GetCollection<Entity.ByteProLog>("ByteProLog");
            await collectionBytePro.InsertOneAsync(new ByteProLog() { id = ObjectId.GenerateNewId().ToString(), loanId = id, requestId = requestId, docId = docId, fileId = fileId, status = status, tenantId = tenantId, userId = userId, updatedOn = DateTime.UtcNow });
            if(result.ModifiedCount!=1)
            {
                result = await UpdateByteProStatusMCU(id,requestId,docId,fileId,isUploaded,userId,tenantId);
            }

            return result.ModifiedCount == 1;
        }
        public async Task<UpdateResult> UpdateByteProStatusMCU(string id, string requestId, string docId, string fileId, bool isUploaded, int userId, int tenantId)
        {
            var status = isUploaded ? ByteProStatus.Synchronized : ByteProStatus.Error;
            IMongoCollection<Entity.Request> collection = mongoService.db.GetCollection<Entity.Request>("Request");
            UpdateResult result = await collection.UpdateOneAsync(new BsonDocument()
            {
                { "_id", BsonObjectId.Create(id) }
            }, new BsonDocument()
            {
                { "$set", new BsonDocument()
                    {
                        { "requests.$[request].documents.$[document].mcuFiles.$[file].byteProStatus", status}

                    }
                }
            }, new UpdateOptions()
            {
                ArrayFilters = new List<ArrayFilterDefinition>()
                {
                    new JsonArrayFilterDefinition<Entity.Request>("{ \"request.id\": "+new ObjectId(requestId).ToJson()+"}"),
                    new JsonArrayFilterDefinition<Entity.Request>("{ \"document.id\": "+new ObjectId(docId).ToJson()+"}"),
                    new JsonArrayFilterDefinition<Entity.Request>("{ \"file.id\": "+new ObjectId(fileId).ToJson()+"}")
                }

            });

            IMongoCollection<Entity.ByteProLog> collectionBytePro = mongoService.db.GetCollection<Entity.ByteProLog>("ByteProLog");
            await collectionBytePro.InsertOneAsync(new ByteProLog() { id = ObjectId.GenerateNewId().ToString(), loanId = id, requestId = requestId, docId = docId, fileId = fileId, status = status, tenantId = tenantId, userId = userId, updatedOn = DateTime.UtcNow });

            return result;
        }
        public async Task<bool> DeleteFile(int loanApplicationId, string fileId)
        {
            IMongoCollection<Entity.Request> collection = mongoService.db.GetCollection<Entity.Request>("Request");
            UpdateResult result = await collection.UpdateOneAsync(new BsonDocument()
            {
                { "loanApplicationId", loanApplicationId }
            }, new BsonDocument()
            {
                { "$set", new BsonDocument()
                    {
                        { "requests.$[].documents.$[].files.$[file].status", FileStatus.Deleted}

                    }
                }
            }, new UpdateOptions()
            {
                ArrayFilters = new List<ArrayFilterDefinition>()
                {
                    new JsonArrayFilterDefinition<Entity.Request>("{ \"file.id\": "+new ObjectId(fileId).ToJson()+"}")
                }

            });
            if(result.ModifiedCount!=1)
            {
                result = await collection.UpdateOneAsync(new BsonDocument()
                {
                    { "loanApplicationId", loanApplicationId }
                }, new BsonDocument()
                {
                    { "$set", new BsonDocument()
                        {
                            { "requests.$[].documents.$[].mcuFiles.$[file].status", FileStatus.Deleted}

                        }
                    }
                }, new UpdateOptions()
                {
                    ArrayFilters = new List<ArrayFilterDefinition>()
                    {
                        new JsonArrayFilterDefinition<Entity.Request>("{ \"file.id\": "+new ObjectId(fileId).ToJson()+"}")
                    }

                });
            }
            return result.ModifiedCount == 1;
        }
    }
}
