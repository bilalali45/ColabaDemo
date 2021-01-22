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
    public class AdminDashboardService : IAdminDashboardService
    {
        private readonly IMongoService mongoService;
        private readonly IActivityLogService activityLogService;
        private readonly IRainmakerService rainmakerService;
        public AdminDashboardService(IMongoService mongoService, IActivityLogService activityLogService, IRainmakerService rainmakerService)
        {
            this.mongoService = mongoService;
            this.activityLogService = activityLogService;
            this.rainmakerService = rainmakerService;
        }
        public async Task<List<AdminDashboardDto>> GetDocument(int loanApplicationId, int tenantId, bool pending, int userId)
        {
            await SaveDashboardSetting(userId,pending);
            IMongoCollection<Entity.Request> collection = mongoService.db.GetCollection<Entity.Request>("Request");

            using var asyncCursor = collection.Aggregate(PipelineDefinition<Entity.Request, BsonDocument>.Create(
                @"{""$match"": {

                  ""loanApplicationId"": " + loanApplicationId + @",
                  ""tenantId"": " + tenantId + @"
                            }
                        }", @"{
                            ""$unwind"": ""$requests""
                        }", @"{
                            ""$match"": {""requests.status"": """ + RequestStatus.Active + @"""}
                        }", @"{
                            ""$unwind"": ""$requests.documents""
                        }",
                             @"{
                            ""$match"": {""requests.documents.status"":{""$ne"": """ + DocumentStatus.Deleted + @"""}}
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
                        }", @"{
                            ""$project"": {
                                ""_id"": 1,
                               ""requestId"": ""$requests.id"",
                                ""docId"": ""$requests.documents.id"",
                                ""docName"": ""$requests.documents.displayName"",
                                ""isMcuVisible"": ""$requests.documents.isMcuVisible"",
                                 ""typeName"": ""$documentObjects.name"",
                                ""status"": ""$requests.documents.status"",
                                ""files"": ""$requests.documents.files"",
                                ""mcuFiles"": ""$requests.documents.mcuFiles"",
                                ""typeId"": ""$requests.documents.typeId"",
                                ""userName"": 1,
                                ""createdOn"": ""$requests.createdOn""
                            }
                        }"
                ));

            List<AdminDashboardDto> result = new List<AdminDashboardDto>();
            while (await asyncCursor.MoveNextAsync())
            {
                foreach (var current in asyncCursor.Current)
                {
                    AdminDashboardQuery query = BsonSerializer.Deserialize<AdminDashboardQuery>(current);
                    if (query.isMcuVisible != false)
                    {
                        AdminDashboardDto dto = new AdminDashboardDto();
                        dto.id = query.id;
                        dto.docId = query.docId;
                        dto.requestId = query.requestId;
                        dto.userName = query.userName;
                        dto.typeId = query.typeId;
                        dto.docName = string.IsNullOrEmpty(query.docName) ? query.typeName : query.docName;
                        dto.status = query.status;
                        dto.createdOn = query.createdOn.HasValue ? (DateTime?)DateTime.SpecifyKind(query.createdOn.Value, DateTimeKind.Utc) : null;
                        dto.files = query.files?.Where(x => x.status != FileStatus.RejectedByMcu && x.status != FileStatus.Deleted && (x.isMcuVisible == null || x.isMcuVisible == true)).Select(x => new AdminFileDto()
                        {
                            isRead = x.isRead.HasValue ? x.isRead.Value : false,
                            id = x.id,
                            clientName = x.clientName,
                            fileUploadedOn = DateTime.SpecifyKind(x.fileUploadedOn, DateTimeKind.Utc),
                            mcuName = x.mcuName,
                            byteProStatus = String.IsNullOrEmpty(x.byteProStatus) ? ByteProStatus.NotSynchronized : x.byteProStatus,
                            userId = x.userId,
                            userName = x.userName,
                            fileModifiedOn = x.fileModifiedOn == null ? null : (DateTime?)DateTime.SpecifyKind(x.fileModifiedOn.Value, DateTimeKind.Utc)
                        }).ToList();
                        if (query.mcuFiles != null)
                        {
                            var mcufiles = query.mcuFiles.Where(x => x.status != FileStatus.RejectedByMcu && x.status != FileStatus.Deleted).Select(x => new AdminFileDto()
                            {
                                isRead = x.isRead.HasValue ? x.isRead.Value : false,
                                id = x.id,
                                clientName = x.clientName,
                                fileUploadedOn = DateTime.SpecifyKind(x.fileUploadedOn, DateTimeKind.Utc),
                                mcuName = x.mcuName,
                                byteProStatus = String.IsNullOrEmpty(x.byteProStatus) ? ByteProStatus.NotSynchronized : x.byteProStatus,
                                userId = x.userId,
                                userName = x.userName,
                                fileModifiedOn = x.fileModifiedOn == null ? null : (DateTime?)DateTime.SpecifyKind(x.fileModifiedOn.Value, DateTimeKind.Utc)
                            }).ToList();

                            dto.files.AddRange(mcufiles);
                        }
                        dto.files = dto.files.OrderByDescending(x => (x.fileUploadedOn>(x.fileModifiedOn??DateTime.MinValue))?x.fileUploadedOn:x.fileModifiedOn).ToList();
                        result.Add(dto);
                    }
                }
            }
            if (pending)
            {
                result = result.Where(x=>x.status!=DocumentStatus.Completed).Select(x => new { order = x.status == DocumentStatus.PendingReview ? 0 : 1, x })
                    .OrderBy(x => x.order).ThenByDescending(x=>x.x.createdOn).Select(x => x.x).ToList();
            }
            else
            {
                result = result.OrderByDescending(x => x.createdOn).ToList();
            }
            return result;
        }

        private async Task SaveDashboardSetting(int userId, bool pending)
        {
            IMongoCollection<Entity.Request> collection = mongoService.db.GetCollection<Entity.Request>("DashboardSetting");
            await collection.UpdateOneAsync(new BsonDocument() { {"userId",userId } }, new BsonDocument()
            {

                { "$set", new BsonDocument()
                    {
                        { "pending", pending}
                    }
                }
            },new UpdateOptions() { IsUpsert=true});
        }

        public async Task<bool> Delete(AdminDeleteModel model, int tenantId, IEnumerable<string> authHeader)
        {
            /*
            IMongoCollection<Entity.Request> collection = mongoService.db.GetCollection<Entity.Request>("Request");

            UpdateResult result = await collection.UpdateOneAsync(new BsonDocument()
            {
                { "_id", BsonObjectId.Create(model.id) },
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
                                            { "id", BsonObjectId.Create(model.requestId) }
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
                                                                        { "id", BsonObjectId.Create(model.docId) }
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

                { "$set", new BsonDocument()
                    {
                        { "requests.$[request].documents.$[document].status", DocumentStatus.Deleted}

                    }
                }
            }, new UpdateOptions()
            {
                ArrayFilters = new List<ArrayFilterDefinition>()
                {
                    new JsonArrayFilterDefinition<Entity.Request>("{ \"request.id\": "+new ObjectId(model.requestId).ToJson()+"}"),
                    new JsonArrayFilterDefinition<Entity.Request>("{ \"document.id\": "+new ObjectId(model.docId).ToJson()+"}")
                }
            });
            */
            IMongoCollection<Request> collection = mongoService.db.GetCollection<Request>("Request");
            using var asyncCursor = collection.Aggregate(PipelineDefinition<Request, BsonDocument>.Create(
             @"{""$match"": {

                  ""_id"": " + new ObjectId(model.id).ToJson() + @",
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
                            ""$project"": {
                                ""_id"": 0,
                                ""status"": ""$requests.documents.status""
                            }
                        }"
             ));
            UpdateResult result = new UpdateResult.Acknowledged(0,0,BsonNull.Value);
            while (await asyncCursor.MoveNextAsync())
            {
                foreach (var current in asyncCursor.Current)
                {
                    var query = BsonSerializer.Deserialize<DocumentStatusQuery>((BsonDocument)current);

                    if (query.status == DocumentStatus.BorrowerTodo)
                    {
                        result = await collection.UpdateOneAsync(new BsonDocument()
                        {
                            { "_id", BsonObjectId.Create(model.id) },
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
                                new JsonArrayFilterDefinition<Request>("{ \"request.id\": "+new ObjectId(model.requestId).ToJson()+"}"),
                                new JsonArrayFilterDefinition<Request>("{ \"document.id\": "+new ObjectId(model.docId).ToJson()+"}")
                            }
                        });
                    }
                    else if(query.status==DocumentStatus.Completed || query.status == DocumentStatus.Draft || query.status == DocumentStatus.ManuallyAdded || query.status == DocumentStatus.PendingReview)
                    {
                        result = await collection.UpdateOneAsync(new BsonDocument()
                        {
                            { "_id", BsonObjectId.Create(model.id) },
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
                                new JsonArrayFilterDefinition<Request>("{ \"request.id\": "+new ObjectId(model.requestId).ToJson()+"}"),
                                new JsonArrayFilterDefinition<Request>("{ \"document.id\": "+new ObjectId(model.docId).ToJson()+"}")
                            }
                        });
                    }
                    else if (query.status == DocumentStatus.Started)
                    {
                        result = await collection.UpdateOneAsync(new BsonDocument()
                        {
                            { "_id", BsonObjectId.Create(model.id) },
                            { "tenantId", tenantId}
                        }, new BsonDocument()
                        {
                            { "$set", new BsonDocument()
                                {
                                    { "requests.$[request].documents.$[document].isMcuVisible", false},
                                    { "requests.$[request].documents.$[document].status", DocumentStatus.Completed}
                                }
                            }
                        }, new UpdateOptions()
                        {
                            ArrayFilters = new List<ArrayFilterDefinition>()
                            {
                                new JsonArrayFilterDefinition<Request>("{ \"request.id\": "+new ObjectId(model.requestId).ToJson()+"}"),
                                new JsonArrayFilterDefinition<Request>("{ \"document.id\": "+new ObjectId(model.docId).ToJson()+"}")
                            }
                        });
                    }
                }
            }

            if (result.ModifiedCount == 1)
            {
                string activityLogId = await activityLogService.GetActivityLogId(model.id, model.requestId, model.docId);

                await activityLogService.InsertLog(activityLogId, string.Format(ActivityStatus.StatusChanged, DocumentStatus.Deleted));
            }

            await rainmakerService.UpdateLoanInfo(null, model.id, authHeader);

            return result.ModifiedCount == 1;
        }

        public async Task<RequestIdQuery> IsDocumentDraft(int loanApplicationId, int userId)
        {
            IMongoCollection<Entity.Request> collection = mongoService.db.GetCollection<Entity.Request>("Request");
            using var asyncCursor = collection.Aggregate(PipelineDefinition<Entity.Request, BsonDocument>.Create(
                @"{""$match"": {
                  ""loanApplicationId"": " + loanApplicationId + @"
                            }
                        }",
                        @"{
                            ""$unwind"": {
                                ""path"": ""$requests"",
                                ""preserveNullAndEmptyArrays"": true}
                        }", @"{""$match"": {
                                ""requests.status"": """ + RequestStatus.Draft + @"""
                            }
                        }", @"{
                            ""$project"": {
                                ""_id"": 0,
                                ""requestId"": ""$requests.id""
                            }
                        }"
            ));
            RequestIdQuery query = new RequestIdQuery();
            while (await asyncCursor.MoveNextAsync())
            {
                foreach (var current in asyncCursor.Current)
                {
                    query = BsonSerializer.Deserialize<RequestIdQuery>(current);
                }
            }

            if (string.IsNullOrEmpty(query.requestId))
            {
                IMongoCollection<Entity.Request> collectionDocumentDraft = mongoService.db.GetCollection<Entity.Request>("Request");

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
                            ""$project"": {
                                ""_id"": 0,
                                ""requestId"": ""$requests.id"",
                                ""isMcuVisible"": ""$requests.documents.isMcuVisible""
                                }
                         } "

                   ));

                while (await asyncCursorDocumentDraft.MoveNextAsync())
                {
                    foreach (var current in asyncCursorDocumentDraft.Current)
                    {
                        query = BsonSerializer.Deserialize<RequestIdQuery>(current);
                        if (query.isMcuVisible == false)
                        {
                            query = new RequestIdQuery();
                        }
                    }
                }
            }
            return query;
        }

        public async Task<DashboardSettingModel> GetDashboardSetting(int userProfileId)
        {
            IMongoCollection<Entity.Request> collection = mongoService.db.GetCollection<Entity.Request>("DashboardSetting");
            using var asyncCursor = collection.Aggregate(PipelineDefinition<Entity.Request, BsonDocument>.Create(
                @"{""$match"": {
                  ""userId"": " + userProfileId + @"
                            }
                        }", @"{
                            ""$project"": {
                                ""_id"": 0,
                                ""userId"": 1,
                                ""pending"": 1
                            }
                        }"
            ));
            DashboardSettingModel query = new DashboardSettingModel();
            while (await asyncCursor.MoveNextAsync())
            {
                foreach (var current in asyncCursor.Current)
                {
                    query = BsonSerializer.Deserialize<DashboardSettingModel>(current);
                    return query;
                }
            }
            return new DashboardSettingModel() { userId=userProfileId,pending=true};
        }
    }
}

