using DocumentManagement.Entity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static DocumentManagement.Model.Template;

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
            IMongoCollection<Template> collection = mongoService.db.GetCollection<Template>("Template");
            List<TemplateModel> templateModels = new List<TemplateModel>();

            //MCU template
            using var asyncCursorMCU = collection.Aggregate(PipelineDefinition<Template, BsonDocument>.Create(
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
            using var asyncCursorTenant = collection.Aggregate(PipelineDefinition<Template, BsonDocument>.Create(
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
            using var asyncCursor = collection.Aggregate(PipelineDefinition<Template, BsonDocument>.Create(
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

        public async Task<bool> DeleteTemplate(string templateId, int tenantId ,int userProfileId)
        {
            IMongoCollection<Template> collection = mongoService.db.GetCollection<Template>("Template");
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

        public async Task<string> InsertTemplate(int tenantId, int userProfileId, string name)
        {
            IMongoCollection<Template> collection = mongoService.db.GetCollection<Template>("Template");

            Template template = new Template() { employeeId = userProfileId, createdOn = DateTime.UtcNow, tenantId = tenantId, name = name, isActive = true };
            await collection.InsertOneAsync(template);

            return template.id;
        }
    }
}
