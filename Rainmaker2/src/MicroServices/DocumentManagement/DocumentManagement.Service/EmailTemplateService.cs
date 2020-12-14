using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DocumentManagement.Model;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace DocumentManagement.Service
{
    public class EmailTemplateService : IEmailTemplateService
    {
        private readonly IMongoService mongoService;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;


        public EmailTemplateService(IMongoService mongoService,
                                    HttpClient _httpClient,
                                    IConfiguration _configuration)
        {
            this.mongoService = mongoService;
            this._httpClient = _httpClient;
            this._configuration = _configuration;
        }


        public async Task<List<Model.EmailTemplate>> GetEmailTemplates(int tenantId)
        {
            IMongoCollection<Entity.EmailTemplate> collection = mongoService.db.GetCollection<Entity.EmailTemplate>("EmailTemplate");

            List<Model.EmailTemplate> emailTemplates = new List<Model.EmailTemplate>();

            using var asyncCursorEmailTemplates = collection.Aggregate(PipelineDefinition<Entity.EmailTemplate, BsonDocument>.Create(
                                                                                                                                     @"{""$match"": {
                  ""tenantId"": " + tenantId + @"
                  ""isDeleted"":false
                            }
                        }",
                                                                                                                                     @"{
                            ""$project"": {
                                ""_id"": 1,
                                ""tenantId"": ""$tenantId"",
                                ""templateName"": ""$templateName"",
                                ""templateDescription"": ""$templateDescription"",
                                ""fromAddress"":""$fromAddress"",
                                ""toAddress"":""$toAddress"",
                                ""CCAddress"":""$CCAddress"", 
                                ""subject"":""$subject"",
                                ""emailBody"":""$emailBody"",
                                ""sortOrder"":""$sortOrder""
                            }
                        }"));
            while (await asyncCursorEmailTemplates.MoveNextAsync())
            {
                foreach (var current in asyncCursorEmailTemplates.Current)
                {
                    Model.EmailTemplateModelQuery query = BsonSerializer.Deserialize<Model.EmailTemplateModelQuery>(current);
                    Model.EmailTemplate dto = new Model.EmailTemplate();
                    dto.id = query.id;
                    dto.tenantId = query.tenantId;
                    dto.templateName = query.templateName;
                    dto.templateDescription = query.templateDescription;
                    dto.fromAddress = query.fromAddress;
                    dto.toAddress = query.toAddress;
                    dto.CCAddress = query.CCAddress;
                    dto.subject = query.subject;
                    dto.emailBody = query.emailBody;
                    dto.sortOrder = query.sortOrder;
                    emailTemplates.Add(dto);
                }
            }

            return emailTemplates;

        }


        public async Task<string> InsertEmailTemplate(int tennantId,
                                                      string templateName,
                                                      string templateDescription,
                                                      string fromAddress,
                                                      string toAddress,
                                                      string CCAddress,
                                                      string subject,
                                                      string emailBody,
                                                      int userProfileId)
        {


            IMongoCollection<Entity.EmailTemplate> collectionMaxSortOrder = mongoService.db.GetCollection<Entity.EmailTemplate>("EmailTemplate");

            using var asyncCursor = collectionMaxSortOrder.Aggregate(PipelineDefinition<Entity.EmailTemplate, BsonDocument>.Create(
                                                                                                                                   @"{""$sort"": {
                              ""sortOrder"": -1
                            }
                        }",
                                                                                                                                   @"{
                            ""$limit"":1
                        }",
                                                                                                                                   @"{
                            ""$project"": {
                                ""_id"": 0,
                               ""maxSortOrder"": ""$sortOrder""
                            }
                        }"));

            int maxSortOrder = 0;
            if (await asyncCursor.MoveNextAsync())
            {
                foreach (var current in asyncCursor.Current)
                {
                    LastSortOrder query = BsonSerializer.Deserialize<LastSortOrder>(current);
                    maxSortOrder = query.maxSortOrder;
                }
            }

            IMongoCollection<Entity.EmailTemplate> collection = mongoService.db.GetCollection<Entity.EmailTemplate>("EmailTemplate");

            Entity.EmailTemplate emailTemplate = new Entity.EmailTemplate()
            {
                tenantId = tennantId,
                templateName = templateName,
                templateDescription = templateDescription,
                fromAddress = fromAddress,
                toAddress = toAddress,
                CCAddress = CCAddress,
                subject = subject,
                emailBody = emailBody,
                sortOrder = maxSortOrder + 1,
                isDeleted = false,
                createdBy = userProfileId,
                createdOn = DateTime.UtcNow
            };
            await collection.InsertOneAsync(emailTemplate);

            return emailTemplate.id;
        }


        public async Task<List<TokenModel>> GetTokens()
        {
            List<TokenModel> lstTokenModels = new List<TokenModel>();

            IMongoCollection<Entity.TokenParam> collection = mongoService.db.GetCollection<Entity.TokenParam>("TokenParam");

            using var asyncCursorTokens = collection.Aggregate(PipelineDefinition<Entity.TokenParam, BsonDocument>.Create(
                                                                                                                          @"{
                            ""$sort"": {""name"": 1}}", @"{
                            ""$project"": {
                                ""_id"": 1,
                                ""name"": ""$name"",
                                ""symbol"": ""$symbol"",
                                ""description"": ""$description"",
                                ""key"":""$key"",
                                ""fromAddess"":""$fromAddess"",
                                ""ccAddess"":""$ccAddess"",
                                ""emailBody"":""$emailBody"",
                                ""emailSubject"":""$emailSubject"",
                                ""defaultValue"":""$defaultValue""
                            }
                        }"
                                                                                                                         ));
            while (await asyncCursorTokens.MoveNextAsync())
            {
                foreach (var current in asyncCursorTokens.Current)
                {
                    Model.TokenParamQuery query = BsonSerializer.Deserialize<Model.TokenParamQuery>(current);
                    Model.TokenModel dto = new Model.TokenModel();
                    dto.id = query.id;
                    dto.name = query.name;
                    dto.symbol = query.symbol;
                    dto.description = query.description;
                    dto.key = query.key;
                    dto.fromAddess = query.fromAddess;
                    dto.ccAddess = query.ccAddess;
                    dto.emailBody = query.emailBody;
                    dto.emailSubject = query.emailSubject;
                    dto.defaultValue = query.defaultValue;
                    lstTokenModels.Add(dto);
                }
            }

            return lstTokenModels;

        }


        public async Task<Model.EmailTemplate> GetEmailTemplateById(string id)
        {
            IMongoCollection<Entity.EmailTemplate> collection = mongoService.db.GetCollection<Entity.EmailTemplate>("EmailTemplate");

            Model.EmailTemplate emailTemplate = new Model.EmailTemplate();

            using var asyncCursorEmailTemplates = collection.Aggregate(PipelineDefinition<Entity.EmailTemplate, BsonDocument>.Create(
                                                                                                                                     @"{""$match"": {
                  ""_id"": " + new ObjectId(id).ToJson() + @" 
                            }
                        }",
                                                                                                                                     @"{
                            ""$project"": {
                                ""_id"": 1,
                                ""tenantId"": ""$tenantId"",
                                ""templateName"": ""$templateName"",
                                ""templateDescription"": ""$templateDescription"",
                                ""fromAddress"":""$fromAddress"",
                                ""toAddress"":""$toAddress"",
                                ""CCAddress"":""$CCAddress"", 
                                ""subject"":""$subject"",
                                ""emailBody"":""$emailBody"",
                                ""sortOrder"":""$sortOrder""
                            }
                        }"));
            while (await asyncCursorEmailTemplates.MoveNextAsync())
            {
                foreach (var current in asyncCursorEmailTemplates.Current)
                {
                    Model.EmailTemplateModelQuery query = BsonSerializer.Deserialize<Model.EmailTemplateModelQuery>(current);
                    emailTemplate.id = query.id;
                    emailTemplate.tenantId = query.tenantId;
                    emailTemplate.templateName = query.templateName;
                    emailTemplate.templateDescription = query.templateDescription;
                    emailTemplate.fromAddress = query.fromAddress;
                    emailTemplate.toAddress = query.toAddress;
                    emailTemplate.CCAddress = query.CCAddress;
                    emailTemplate.subject = query.subject;
                    emailTemplate.emailBody = query.emailBody;
                    emailTemplate.sortOrder = query.sortOrder;
                }
            }

            return emailTemplate;
        }


        public async Task<Model.EmailTemplate> GetRenderEmailTemplateById(string id,
                                                                          int loanApplicationId,
                                                                          IEnumerable<string> authHeader)
        {
            IMongoCollection<Entity.EmailTemplate> collection = mongoService.db.GetCollection<Entity.EmailTemplate>("EmailTemplate");

            Model.EmailTemplate emailTemplate = new Model.EmailTemplate();

            using var asyncCursorEmailTemplates = collection.Aggregate(PipelineDefinition<Entity.EmailTemplate, BsonDocument>.Create(
                                                                                                                                     @"{""$match"": {
                  ""_id"": " + new ObjectId(id).ToJson() + @" 
                            }
                        }",
                                                                                                                                     @"{
                            ""$project"": {
                                ""_id"": 1,
                                ""tenantId"": ""$tenantId"",
                                ""templateName"": ""$templateName"",
                                ""templateDescription"": ""$templateDescription"",
                                ""fromAddress"":""$fromAddress"",
                                ""toAddress"":""$toAddress"",
                                ""CCAddress"":""$CCAddress"", 
                                ""subject"":""$subject"",
                                ""emailBody"":""$emailBody"",
                                ""sortOrder"":""$sortOrder""
                            }
                        }"));
            while (await asyncCursorEmailTemplates.MoveNextAsync())
            {
                foreach (var current in asyncCursorEmailTemplates.Current)
                {
                    Model.EmailTemplateModelQuery query = BsonSerializer.Deserialize<Model.EmailTemplateModelQuery>(current);
                    emailTemplate.id = query.id;
                    emailTemplate.tenantId = query.tenantId;
                    emailTemplate.templateName = query.templateName;
                    emailTemplate.templateDescription = query.templateDescription;
                    emailTemplate.fromAddress = query.fromAddress;
                    emailTemplate.toAddress = query.toAddress;
                    emailTemplate.CCAddress = query.CCAddress;
                    emailTemplate.subject = query.subject;
                    emailTemplate.emailBody = query.emailBody;
                    emailTemplate.sortOrder = query.sortOrder;
                }
            }

            var tokens = GetTokens();

            EmailTemplateModel emailTemplateModel = new EmailTemplateModel();
            emailTemplateModel.id = id;
            emailTemplateModel.loanApplicationId = loanApplicationId;
            emailTemplateModel.tenantId = emailTemplate.tenantId;
            emailTemplateModel.templateName = emailTemplate.templateName;
            emailTemplateModel.templateDescription = emailTemplate.templateDescription;
            emailTemplateModel.fromAddress = emailTemplate.fromAddress;
            emailTemplateModel.CCAddress = emailTemplate.CCAddress;
            emailTemplateModel.subject = emailTemplate.subject;
            emailTemplateModel.emailBody = emailTemplate.emailBody;
            emailTemplateModel.lstTokens = tokens.Result;

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(_configuration[key: "RainMaker:Url"] + "/api/RainMaker/Setting/RenderEmailTokens"),
                Method = HttpMethod.Get,
                Content = new StringContent(content: emailTemplateModel.ToJson(),
                                                          encoding: Encoding.UTF8,
                                                          mediaType: "application/json")
            };
            request.Headers.Add("Authorization",
                                authHeader);
            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.EmailTemplate>(await response.Content.ReadAsStringAsync());
            result.id = emailTemplateModel.id;
            result.tenantId = emailTemplateModel.tenantId;
            result.templateName = emailTemplateModel.templateName;
            result.templateDescription = emailTemplateModel.templateDescription;
            result.sortOrder = emailTemplate.sortOrder;
            //result.CCAddress = emailTemplateModel.CCAddress;
            return result;
        }


        public async Task<Model.EmailTemplate> GetDraftEmailTemplateById(string id,
                                                                         int loanApplicationId,
                                                                         int tenantId,
                                                                         IEnumerable<string> authHeader)
        {
            Model.EmailTemplate emailTemplate = new EmailTemplate();

            IMongoCollection<Entity.Request> collectionEmailDraft = mongoService.db.GetCollection<Entity.Request>("Request");

            //Get Draft Email
            using var asyncDarftEmailCursor = collectionEmailDraft.Aggregate(PipelineDefinition<Entity.Request, BsonDocument>.Create(
                                                                                                                                     @"{""$match"": {
                  ""loanApplicationId"": " + loanApplicationId + @",
                  ""tenantId"": " + tenantId + @" 
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
                            ""$match"": {
                                ""requests.email.emailTemplateId"":" + new ObjectId(id).ToJson() + @" 
                            }
                        }",
                                                                                                                                     @"{
                            ""$project"": {
                                ""_id"": 0,                               
                                ""emailTemplateId"": ""$requests.email.emailTemplateId"",
                                ""fromAddress"": ""$requests.email.fromAddress"",
                                ""toAddress"": ""$requests.email.toAddress"",
                                ""ccAddress"": ""$requests.email.ccAddress"",
                                ""subject"": ""$requests.email.subject"",
                                ""emailBody"": ""$requests.email.emailBody""
                                }
                         } "));

            if (await asyncDarftEmailCursor.MoveNextAsync())
            {
                foreach (var current in asyncDarftEmailCursor.Current)
                {
                    DraftEmailQuery query = BsonSerializer.Deserialize<DraftEmailQuery>(current);

                    emailTemplate.id = query.emailTemplateId;
                    emailTemplate.fromAddress = query.fromAddress;
                    emailTemplate.toAddress = query.toAddress;
                    emailTemplate.CCAddress = query.ccAddress;
                    emailTemplate.subject = query.subject;
                    emailTemplate.emailBody = query.emailBody;
                    return emailTemplate;
                }
            }

            return emailTemplate = await GetRenderEmailTemplateById(id,
                                                                    loanApplicationId,
                                                                    authHeader);
        }


        public async Task<bool> DeleteEmailTemplate(string id,
                                                    int userProfileId)
        {
            IMongoCollection<Entity.EmailTemplate> collection = mongoService.db.GetCollection<Entity.EmailTemplate>("EmailTemplate");
            UpdateResult result = await collection.UpdateOneAsync(new BsonDocument()
                                                                  {
                                                                      {"_id", BsonObjectId.Create(id)}
                                                                  },
                                                                  new BsonDocument()
                                                                  {
                                                                      {
                                                                          "$set", new BsonDocument()
                                                                                  {
                                                                                      {"isDeleted", true},
                                                                                      {"modifiedBy", userProfileId},
                                                                                      {"modifiedOn", DateTime.UtcNow}
                                                                                  }
                                                                      }
                                                                  });
            return result.ModifiedCount == 1;
        }


        public async Task<bool> UpdateEmailTemplate(string id,
                                                    string templateName,
                                                    string templateDescription,
                                                    string fromAddress,
                                                    string CCAddress,
                                                    string subject,
                                                    string emailBody,
                                                    int userProfileId)
        {
            IMongoCollection<Entity.EmailTemplate> collection = mongoService.db.GetCollection<Entity.EmailTemplate>("EmailTemplate");
            UpdateResult result = await collection.UpdateOneAsync(new BsonDocument()
                                                                  {
                                                                      {"_id", BsonObjectId.Create(id)}
                                                                  },
                                                                  new BsonDocument()
                                                                  {
                                                                      {
                                                                          "$set", new BsonDocument()
                                                                                  {
                                                                                      {"templateName", templateName},
                                                                                      {"templateDescription", templateDescription},
                                                                                      {"fromAddress", fromAddress},
                                                                                      {"CCAddress", CCAddress},
                                                                                      {"subject", subject},
                                                                                      {"emailBody", emailBody},
                                                                                      {"modifiedBy", userProfileId},
                                                                                      {"modifiedOn", DateTime.UtcNow}
                                                                                  }
                                                                      }
                                                                  });
            return result.ModifiedCount == 1;
        }


        public async Task UpdateSortOrder(List<TemplateSortModel> lstEmailTemplates)
        {
            foreach (var item in lstEmailTemplates)
            {
                IMongoCollection<Entity.EmailTemplate> collection = mongoService.db.GetCollection<Entity.EmailTemplate>("EmailTemplate");
                UpdateResult result = await collection.UpdateOneAsync(new BsonDocument()
                                                                      {
                                                                          {"_id", BsonObjectId.Create(item.id)}
                                                                      },
                                                                      new BsonDocument()
                                                                      {
                                                                          {
                                                                              "$set", new BsonDocument()
                                                                                      {
                                                                                          {"sortOrder", item.sortOrder}
                                                                                      }
                                                                          }
                                                                      });
            }
        }
    }

}
