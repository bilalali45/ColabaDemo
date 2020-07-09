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

        public DocumentService(IMongoService mongoService)
        {
            this.mongoService = mongoService;
        }
        public async Task<List<DocumendDTO>> GetFiles(string id, string requestId, string docId)
        {
            IMongoCollection<Request> collection = mongoService.db.GetCollection<Request>("Request");

            using var asyncCursor = collection.Aggregate(PipelineDefinition<Request, BsonDocument>.Create(
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
                                ""files"": ""$requests.documents.files"" 
                            }
                             } "

                ));


            List<DocumendDTO> result = new List<DocumendDTO>();
            while (await asyncCursor.MoveNextAsync())
            {
                foreach (var current in asyncCursor.Current)
                {
                    var c = current.ToJson();
                    DocumentDetailQuery query = BsonSerializer.Deserialize<DocumentDetailQuery>(current);

                    DocumendDTO dto = new DocumendDTO();
                    dto.files = new List<DocumentFileDTO>();
                    dto.id = query.id;
                    dto.docId = query.docId;
                    dto.typeId = query.typeId;
                    dto.docName = query.docName;
                    dto.requestId = query.requestId;
                    dto.docName = string.IsNullOrEmpty(query.docName) ? query.typeName : query.docName;
                    dto.files = query.files?.Select(x => new DocumentFileDTO()
                    {
                        fileId = x.id,
                        clientName = x.clientName,
                        fileUploadedOn = DateTime.SpecifyKind(x.fileUploadedOn, DateTimeKind.Utc),
                        mcuName = x.mcuName
                    }).ToList();
                    result.Add(dto);
                }
            }

            return result;
        }


        public async Task<List<ActivityLogDTO>> GetActivityLog(string id, string typeId, string docId)
        {
            IMongoCollection<Entity.ActivityLog> collection = mongoService.db.GetCollection<Entity.ActivityLog>("ActivityLog");
            string match = "";
            if(!string.IsNullOrEmpty(typeId))
            {
                match = @"{""$match"": {

                  ""loanId"": " + new ObjectId(id).ToJson() + @" 
                  ""typeId"": " + new ObjectId(typeId).ToJson() + @"
                            }
                        }";
            }
            else
            {
                match = @"{""$match"": {

                  ""loanId"": " + new ObjectId(id).ToJson() + @" 
                  ""docId"": " + new ObjectId(docId).ToJson() + @"
                            }
                        }";
            }
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


            List<ActivityLogDTO> result = new List<ActivityLogDTO>();
            while (await asyncCursor.MoveNextAsync())
            {
                foreach (var current in asyncCursor.Current)
                {
                    ActivityLogDTO dto = new ActivityLogDTO();
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
        public async Task<List<DocumentModel>> GetDocumentsByTemplateIds(TemplateIdModel templateIdsModel)
        {
            IMongoCollection<Entity.Template> collection = mongoService.db.GetCollection<Entity.Template>("Template");

            using var asyncCursor = collection.Aggregate(PipelineDefinition<Entity.Template, BsonDocument>.Create(
                @"{""$match"": { ""_id"": { ""$in"": " + new BsonArray(templateIdsModel.id.Select(x => new ObjectId(x))).ToJson() + @"
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
                                ""_id"": 0,
                                ""docId"": ""$documents._id"",
                                ""typeName"": ""$documents.name"",
                                ""docMessage"": ""$documents.message"",
                                ""messages"": ""$documents.messages"",
                                ""docName"":""$documentTypes.docName""
                            }
                        }"
                ));

            List<DocumentModel> result = new List<DocumentModel>();
            while (await asyncCursor.MoveNextAsync())
            {
                foreach (var current in asyncCursor.Current)
                {
                    DocumentQuery query = BsonSerializer.Deserialize<DocumentQuery>(current);
                    DocumentModel dto = new DocumentModel();
                    dto.docId = query.docId;
                    dto.docName = string.IsNullOrEmpty(query.docName) ? query.typeName : query.docName;
                    if (query.messages?.Any(x => x.tenantId == templateIdsModel.tenantId) == true)
                    {
                        dto.docMessage = query.messages.Where(x => x.tenantId == templateIdsModel.tenantId).First().message;
                    }
                    else
                    {
                        dto.docMessage = query.docMessage;
                    }
                    result.Add(dto);
                }
            }
            return result.GroupBy(x => new { x.docId, x.docName }).Select(x => x.First()).ToList();
        }
        public async Task<List<EmailLogDTO>> GetEmailLog(string id)
        {
            IMongoCollection<Entity.EmailLog> collection = mongoService.db.GetCollection<Entity.EmailLog>("ActivityLog");

            using var asyncCursor = collection.Aggregate(PipelineDefinition<Entity.EmailLog, BsonDocument>.Create(
              @"{""$match"": {

                  ""loanId"": " + new ObjectId(id).ToJson() + @"
                            }
                        }"
                        , @"{
                            ""$project"": {
                                ""userId"": 1,                               
                                ""userName"":1,
                                ""dateTime"": 1,
                                ""_id"": 1 ,
                                ""emailText"": 1, 
                                ""loanId"": 1  
                            }
                             } "

                ));


            List<EmailLogDTO> result = new List<EmailLogDTO>();
            while (await asyncCursor.MoveNextAsync())
            {
                foreach (var current in asyncCursor.Current)
                {
                    EmailLogDTO dto = new EmailLogDTO();
                    EmailLogQuery query = BsonSerializer.Deserialize<EmailLogQuery>(current);
                    dto.userId = query.userId;
                    dto.userName = query.userName;
                    dto.dateTime = DateTime.SpecifyKind(query.dateTime, DateTimeKind.Utc);
                    dto.emailText = query.emailText;
                    dto.id = query.id;
                    dto.loanId = query.loanId;

                    result.Add(dto);
                }
            }

            return result.OrderByDescending(x=>x.dateTime).ToList();
        }
        public async Task<bool> mcuRename(string id, string requestId, string docId, string fileId, string newName)
        {
            IMongoCollection<Request> collection = mongoService.db.GetCollection<Request>("Request");

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
                    new JsonArrayFilterDefinition<Request>("{ \"request.id\": "+new ObjectId( requestId).ToJson()+"}"),
                    new JsonArrayFilterDefinition<Request>("{ \"document.id\": "+new ObjectId( docId).ToJson()+"}"),
                    new JsonArrayFilterDefinition<Request>("{ \"file.id\": "+new ObjectId( fileId).ToJson()+"}")
                }
            });

            return result.ModifiedCount == 1;
        }
        public async Task<bool> AcceptDocument(string id, string requestId, string docId)
        {
            IMongoCollection<Entity.Request> collection = mongoService.db.GetCollection<Entity.Request>("Request");
            UpdateResult result = await collection.UpdateOneAsync(new BsonDocument()
            {
                { "_id", BsonObjectId.Create(id) }
            }, new BsonDocument()
            {
                { "$set", new BsonDocument()
                    {
                        { "requests.$[request].documents.$[document].status", DocumentStatus.Completed}

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
            return result.ModifiedCount == 1;
        }

        public async Task<bool> RejectDocument(string id, string requestId, string docId, string message)
        {
            IMongoCollection<Entity.Request> collection = mongoService.db.GetCollection<Entity.Request>("Request");
            UpdateResult result = await collection.UpdateOneAsync(new BsonDocument()
            {
                { "_id", BsonObjectId.Create(id) }
            }, new BsonDocument()
            {
                { "$set", new BsonDocument()
                    {
                        { "requests.$[request].documents.$[document].status", DocumentStatus.Started},
                        { "requests.$[request].documents.$[document].message", message}

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
            return result.ModifiedCount == 1;
        }

    }
}
