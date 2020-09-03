using DocumentManagement.Entity;
using DocumentManagement.Model;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DocumentManagement.Model.Template;
using TemplateDocument = DocumentManagement.Entity.TemplateDocument;

namespace DocumentManagement.Service
{
    public class TemplateService : ITemplateService
    {
        private readonly IMongoService mongoService;

        public TemplateService(IMongoService mongoService)
        {
            this.mongoService = mongoService;
        }
        public async Task<List<TemplateModel>> GetTemplates(int tenantId, int userProfileId)
        {
            IMongoCollection<Entity.Template> collection = mongoService.db.GetCollection<Entity.Template>("Template");
            List<TemplateModel> templateModels = new List<TemplateModel>();

            //MCU template
            using var asyncCursorMCU = collection.Aggregate(PipelineDefinition<Entity.Template, BsonDocument>.Create(
           @"{""$match"": {
                  ""tenantId"": " + tenantId + @",
                  ""userId"": " + userProfileId + @",
                  ""isActive"":true
                            }
                        }", @"{
                            ""$project"": {
                                ""_id"": 1,
                                ""name"": ""$name"",
                                ""type"":""" + TemplateType.MCUTemplate + @"""
                            }
                        }"
           ));
            while (await asyncCursorMCU.MoveNextAsync())
            {
                foreach (var current in asyncCursorMCU.Current)
                {
                    TemplateQuery query = BsonSerializer.Deserialize<TemplateQuery>(current);
                    TemplateModel dto = new TemplateModel();
                    dto.id = query.id;
                    dto.name = query.name;
                    dto.type = query.type;
                    templateModels.Add(dto);
                }
            }

            //Tenant template
            using var asyncCursorTenant = collection.Aggregate(PipelineDefinition<Entity.Template, BsonDocument>.Create(
           @"{""$match"": {
                  ""tenantId"": " + tenantId + @",
                  ""userId"": null,
                  ""isActive"":true
                            }
                        }", @"{
                            ""$project"": {
                                ""_id"": 1,
                                ""name"": ""$name"",
                                ""type"":""" + TemplateType.TenantTemplate + @"""
                            }
                        }"
           ));
            while (await asyncCursorTenant.MoveNextAsync())
            {
                foreach (var current in asyncCursorTenant.Current)
                {
                    TemplateQuery query = BsonSerializer.Deserialize<TemplateQuery>(current);
                    TemplateModel dto = new TemplateModel();
                    dto.id = query.id;
                    dto.name = query.name;
                    dto.type = query.type;
                    templateModels.Add(dto);
                }
            }

            //System template
            using var asyncCursor = collection.Aggregate(PipelineDefinition<Entity.Template, BsonDocument>.Create(
          @"{""$match"": {
                  ""tenantId"": null,
                  ""userId"": null,
                  ""isActive"":true
                            }
                        }", @"{
                            ""$project"": {
                                ""_id"": 1,
                                ""name"": ""$name"",
                                ""type"":""" + TemplateType.SystemTemplate + @"""
                            }
                        }"
          ));
            while (await asyncCursor.MoveNextAsync())
            {
                foreach (var current in asyncCursor.Current)
                {
                    TemplateQuery query = BsonSerializer.Deserialize<TemplateQuery>(current);
                    TemplateModel dto = new TemplateModel();
                    dto.id = query.id;
                    dto.name = query.name;
                    dto.type = query.type;
                    templateModels.Add(dto);
                }
            }

            return templateModels;
        }
        public async Task<List<TemplateDTO>> GetDocument(string id)
        {
            IMongoCollection<Entity.Template> collection = mongoService.db.GetCollection<Entity.Template>("Template");

            using var asyncCursor = collection.Aggregate(PipelineDefinition<Entity.Template, BsonDocument>.Create(
                @"{""$match"": {

                  ""_id"": " + new ObjectId(id).ToJson() + @"
                            }
                        }",
                        @"{
                            ""$unwind"": ""$documentTypes""
                        }",
                             @"{
                            ""$lookup"": {
                                ""from"": ""DocumentType"",
                                ""localField"": ""documentTypes.typeId"",
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
                                ""docId"": ""$documentTypes.id"",
                                ""docName"": ""$documentTypes.docName"",
                                ""typeName"": ""$documentObjects.name"",
                               ""typeId"": ""$documentTypes.typeId""
                            }
                        }"
                ));

            List<TemplateDTO> result = new List<TemplateDTO>();
            while (await asyncCursor.MoveNextAsync())
            {
                foreach (var current in asyncCursor.Current)
                {
                    TemplateDocumentQuery query = BsonSerializer.Deserialize<TemplateDocumentQuery>(current);
                    TemplateDTO dto = new TemplateDTO();
                    dto.docId = query.docId;
                    dto.docName = string.IsNullOrEmpty(query.docName) ? query.typeName : query.docName;
                    dto.typeId = query.typeId;
                    result.Add(dto);
                }
            }
            return result;
        }
        public async Task<bool> RenameTemplate(string id, int tenantid, string newname, int userProfileId)
        {
            IMongoCollection<Entity.Template> collection = mongoService.db.GetCollection<Entity.Template>("Template");

            UpdateResult result = await collection.UpdateOneAsync(new BsonDocument()
            {
                { "_id", BsonObjectId.Create(id) },
                { "tenantId", tenantid}
                ,{ "userId", userProfileId}
            }, new BsonDocument()
            {
                { "$set", new BsonDocument()
                    {
                        { "name", newname}
                    }
                }
            });

            return result.ModifiedCount == 1;
        }
        public async Task<bool> DeleteDocument(string id, int tenantid, string documentid, int userProfileId)
        {
            IMongoCollection<Entity.Template> collection = mongoService.db.GetCollection<Entity.Template>("Template");
            UpdateResult result = await collection.UpdateOneAsync(new BsonDocument()
            {
                { "_id", BsonObjectId.Create(id) }
              ,  { "tenantId", tenantid}  
              , { "userId", userProfileId}
            }
             , new BsonDocument()
            {

                { "$pull", new BsonDocument()
                    {
                          { "documentTypes", new BsonDocument(){

                              { "id",BsonObjectId.Create(documentid)
                              }
                          }
                       }
                    }
                }
            });
            return result.ModifiedCount == 1;
        }
        public async Task<string> InsertTemplate(int tenantId, int userProfileId, string name)
        {
            IMongoCollection<Entity.Template> collection = mongoService.db.GetCollection<Entity.Template>("Template");

            Entity.Template template = new Entity.Template() { userId = userProfileId, createdOn = DateTime.UtcNow, tenantId = tenantId, name = name, isActive = true,documentTypes=new List<Entity.TemplateDocument>() };
            await collection.InsertOneAsync(template);

            return template.name;
        }

        public async Task<List<CategoryDocumentTypeModel>> GetCategoryDocument(int tenantId)
        {
            IMongoCollection<Category> collection = mongoService.db.GetCollection<Category>("Category");
            List<CategoryDocumentTypeModel> CategoryDocumentTypeModel = new List<CategoryDocumentTypeModel>();

            using var asyncCursor = collection.Aggregate(PipelineDefinition<Category, BsonDocument>.Create(
            @"{""$lookup"": {
                                ""from"": ""DocumentType"",
                                ""localField"": ""_id"",
                                ""foreignField"": ""categoryId"",
                                ""as"": ""categoryDocument""
                            }
                        }", @"{
                            ""$unwind"": {
                                ""path"": ""$categoryDocument"",
                                ""preserveNullAndEmptyArrays"": true
                            }
                        }", @"{
                            ""$project"": {
                                ""_id"": 1,
                                ""name"": 1,
                                ""docTypeId"": ""$categoryDocument._id"",
                                ""docType"": ""$categoryDocument.name"",
                                ""docMessage"": ""$categoryDocument.message"",
                                ""messages"": ""$categoryDocument.messages"",
                                ""isCommonlyUsed"": ""$categoryDocument.isCommonlyUsed""
                            }
                        }"
           ));
            while (await asyncCursor.MoveNextAsync())
            {
                foreach (var current in asyncCursor.Current)
                {
                    CategoryDocumentQuery query = BsonSerializer.Deserialize<CategoryDocumentQuery>(current);
                    var cdtm = CategoryDocumentTypeModel.Where(x => x.catId == query.id).FirstOrDefault();
                    if(cdtm==null)
                    {
                        cdtm = new CategoryDocumentTypeModel();
                        cdtm.catId = query.id;
                        cdtm.catName = query.name;
                        cdtm.documents = new List<DocumentTypeModel>();
                        CategoryDocumentTypeModel.Add(cdtm);
                    }

                    if (!string.IsNullOrEmpty(query.docTypeId))
                    {
                        string docMessage = String.Empty;
                        if (query.messages?.Any(x => x.tenantId == tenantId) == true)
                        {
                            docMessage = query.messages.Where(x => x.tenantId == tenantId).First().message;
                        }
                        else
                        {
                            docMessage = query.docMessage;
                        }

                        cdtm.documents.Add(new DocumentTypeModel()
                            {docTypeId = query.docTypeId, docType = query.docType,docMessage = docMessage,isCommonlyUsed = query.isCommonlyUsed ?? false});
                    }
                   
                }
            }

            return CategoryDocumentTypeModel;
        }

        public async Task<bool> DeleteTemplate(string templateId, int tenantId, int userProfileId)
        {
            IMongoCollection<Entity.Template> collection = mongoService.db.GetCollection<Entity.Template>("Template");
            UpdateResult result = await collection.UpdateOneAsync(new BsonDocument()
            {
                { "_id", BsonObjectId.Create(templateId) },
                { "tenantId", tenantId},
                { "userId", userProfileId}
            }, new BsonDocument()
            {
                { "$set", new BsonDocument()
                    {
                        { "isActive", false}

                    }
                }
            });
            return result.ModifiedCount == 1;

        }

        public async Task<bool> AddDocument(string templateId, int tenantId, int userProfileId, string typeId, string docName)
        {
            IMongoCollection<Entity.Template> collection = mongoService.db.GetCollection<Entity.Template>("Template");

            BsonDocument bsonElements = new BsonDocument();
            bsonElements.Add("id", ObjectId.GenerateNewId());
            if (!string.IsNullOrEmpty(typeId))
            {
                bsonElements.Add("typeId", BsonObjectId.Create(typeId));
                bsonElements.Add("docName", BsonNull.Value);
            }
            else if (!string.IsNullOrEmpty(docName))
            {
                bsonElements.Add("typeId", BsonNull.Value);
                bsonElements.Add("docName", docName);
            }
            else {
                throw new Exception("typeId and docName both can't be null");
            }

            UpdateResult result = await collection.UpdateOneAsync(new BsonDocument()
            {
                { "_id", BsonObjectId.Create(templateId) },
                { "tenantId", tenantId},
                { "userId", userProfileId}
            }, new BsonDocument()
            {
                { "$push", new BsonDocument()
                    {
                        { "documentTypes", bsonElements  }
                    }
                },
            }
            );
            return result.ModifiedCount == 1;
        }



        public async Task<string> SaveTemplate(AddTemplateModel model, int userProfileId, int tenantId)
        {
            IMongoCollection<Entity.Template> collection = mongoService.db.GetCollection<Entity.Template>("Template");
            List<Entity.TemplateDocument> mTemplateDocumentsList= new List<TemplateDocument>();
           
            foreach (var item in model.documentTypes)
            {
                Entity.TemplateDocument mTemplateDocuments = new TemplateDocument();
                mTemplateDocuments.id = ObjectId.GenerateNewId().ToString();
                mTemplateDocuments.typeId = item.typeId;
                mTemplateDocuments.docName = item.docName;
                mTemplateDocumentsList.Add(mTemplateDocuments);
            }
            Entity.Template template = new Entity.Template() {id= ObjectId.GenerateNewId().ToString(),
                userId = userProfileId, createdOn = DateTime.UtcNow, tenantId = tenantId, name = model.name, isActive = true, documentTypes = mTemplateDocumentsList};
            await collection.InsertOneAsync(template);

            return template.id;
        }
    }
}
