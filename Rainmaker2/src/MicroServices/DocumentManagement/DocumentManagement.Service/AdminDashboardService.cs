﻿using DocumentManagement.Entity;
using DocumentManagement.Model;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public async Task<List<AdminDashboardDTO>> GetDocument(int loanApplicationId, int tenantId, bool pending)
        {
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
                                 ""typeName"": ""$documentObjects.name"",
                                ""status"": ""$requests.documents.status"",
                                ""files"": ""$requests.documents.files"",
                                ""typeId"": ""$requests.documents.typeId"",
                                ""userName"": 1,
                                ""createdOn"": ""$requests.createdOn""
                            }
                        }"
                ));

            List<AdminDashboardDTO> result = new List<AdminDashboardDTO>();
            while (await asyncCursor.MoveNextAsync())
            {
                foreach (var current in asyncCursor.Current)
                {
                    AdminDashboardQuery query = BsonSerializer.Deserialize<AdminDashboardQuery>(current);
                    AdminDashboardDTO dto = new AdminDashboardDTO();
                    dto.id = query.id;
                    dto.docId = query.docId;
                    dto.requestId = query.requestId;
                    dto.userName = query.userName;
                    dto.typeId = query.typeId;
                    dto.docName = string.IsNullOrEmpty(query.docName) ? query.typeName : query.docName;
                    dto.status = query.status;
                    dto.createdOn = query.createdOn.HasValue ? (DateTime?)DateTime.SpecifyKind(query.createdOn.Value,DateTimeKind.Utc) : null;
                    dto.files = query.files?.Where(x => x.status != FileStatus.RejectedByMcu && x.status!=FileStatus.Deleted).Select(x => new AdminFileDTO()
                    {
                        isRead = x.isRead.HasValue ? x.isRead.Value : false,
                        id = x.id,
                        clientName = x.clientName,
                        fileUploadedOn = DateTime.SpecifyKind(x.fileUploadedOn, DateTimeKind.Utc),
                        mcuName = x.mcuName,
                        byteProStatus = String.IsNullOrEmpty(x.byteProStatus) ? ByteProStatus.NotSynchronized : x.byteProStatus
                    }).ToList();
                    result.Add(dto);

                }
            }
            if (pending)
            {
                result = result.Select(x => new { order = x.status == DocumentStatus.PendingReview ? 0 : 1, x })
                    .OrderBy(x => x.order).ThenByDescending(x=>x.x.createdOn).Select(x => x.x).ToList();
            }
            else
            {
                result = result.OrderByDescending(x => x.createdOn).ToList();
            }
            return result;
        }


        public async Task<bool> Delete(AdminDeleteModel model, int tenantId, IEnumerable<string> authHeader)
        {
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
                                                                    },
                                                                    new BsonDocument()
                                                                    {
                                                                      { "status", DocumentStatus.BorrowerTodo}

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
                                ""requestId"": ""$requests.id""
                                }
                         } "

                   ));

                while (await asyncCursorDocumentDraft.MoveNextAsync())
                {
                    foreach (var current in asyncCursorDocumentDraft.Current)
                    {
                        query = BsonSerializer.Deserialize<RequestIdQuery>(current);
                    }
                }
            }
            return query;
        }
    }
}
