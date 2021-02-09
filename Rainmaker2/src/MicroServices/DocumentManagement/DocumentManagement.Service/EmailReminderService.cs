using DocumentManagement.Model;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Service
{
    public class EmailReminderService : IEmailReminderService
    {
        private readonly IMongoService mongoService;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public EmailReminderService(IMongoService mongoService, HttpClient _httpClient, IConfiguration _configuration)
        {
            this.mongoService = mongoService;
            this._httpClient = _httpClient;
            this._configuration = _configuration;
        }
        public async Task<string> AddEmailReminder(int tenantId, int noOfDays, DateTime recurringTime, string fromAddress, string ccAddress, string subject, string emailBody, int userProfileId)
        {
            Entity.Email emailModel = new Entity.Email();
            emailModel.id = ObjectId.GenerateNewId().ToString();
            emailModel.fromAddress = fromAddress;
            emailModel.ccAddress = ccAddress;
            emailModel.subject = subject;
            emailModel.emailBody = emailBody;

            IMongoCollection<Entity.EmailReminder> collectionEmailReminder =
                        mongoService.db.GetCollection<Entity.EmailReminder>("EmailReminder");
            Entity.EmailReminder emailReminderModel = new Entity.EmailReminder()
            {
                id = ObjectId.GenerateNewId().ToString(),
                tenantId = tenantId,
                noOfDays = noOfDays,
                isActive = true,
                isDeleted = false,
                createdBy = userProfileId,
                createdOn = DateTime.UtcNow,
                recurringTime = recurringTime,
                email = emailModel
            };

            await collectionEmailReminder.InsertOneAsync(emailReminderModel);
            return emailReminderModel.id;
        }

        public async Task<Model.EmailReminderModel> GetEmailReminders(int tenantId, IEnumerable<string> authHeader)
        {
            IMongoCollection<Entity.EmailReminder> collection = mongoService.db.GetCollection<Entity.EmailReminder>("EmailReminder");

            Model.EmailReminderModel emailReminderModel = new Model.EmailReminderModel();
            emailReminderModel.emailReminders = new List<EmailReminder>();
            using var asyncCursorEmailTemplates = collection.Aggregate(PipelineDefinition<Entity.EmailReminder, BsonDocument>.Create(
                                                                                                                                     @"{""$match"": {
                  ""tenantId"": " + tenantId + @"
                  ""isDeleted"":false
                            }
                        }", @"{
                            ""$sort"": {""recurringTime"": 1}}",
                                                                                                                                     @"{
                            ""$project"": {
                                ""_id"": 1,                               
                                ""noOfDays"": ""$noOfDays"",
                                ""recurringTime"": ""$recurringTime"",
                                ""isActive"": ""$isActive"",
                                ""email"": ""$email""
                                }
                         } "
                        ));
            while (await asyncCursorEmailTemplates.MoveNextAsync())
            {
                foreach (var current in asyncCursorEmailTemplates.Current)
                {
                    Model.EmailReminder dto = BsonSerializer.Deserialize<Model.EmailReminder>(current);
                    emailReminderModel.emailReminders.Add(dto);
                }
            }
            GetJobTypeModel getJobTypeModel = new GetJobTypeModel();
            getJobTypeModel.jobTypeId = JobType.EmailReminder;

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(_configuration[key: "Setting:Url"] + "/api/Setting/EmailReminder/GetJobType"),
                Method = HttpMethod.Get,
                Content = new StringContent(content: getJobTypeModel.ToJson(),
                                                         encoding: Encoding.UTF8,
                                                         mediaType: "application/json")
            };

            request.Headers.Add("Authorization", authHeader);
            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var jobTypeModel = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.JobTypeModel>(await response.Content.ReadAsStringAsync());

            emailReminderModel.isActive = jobTypeModel.isActive;

            return emailReminderModel;
        }

        public async Task<bool> UpdateEmailReminder(string id, int noOfDays, DateTime recurringTime, string fromAddress, string ccAddress, string subject, string emailBody, int userProfileId, IEnumerable<string> authHeader)
        {
            IMongoCollection<Entity.EmailReminder> collection = mongoService.db.GetCollection<Entity.EmailReminder>("EmailReminder");

            BsonDocument bsonElements = new BsonDocument();
            bsonElements.Add("fromAddress", fromAddress);
            bsonElements.Add("ccAddress", ccAddress);
            bsonElements.Add("subject", subject);
            bsonElements.Add("emailBody", emailBody);

            UpdateResult result = await collection.UpdateOneAsync(new BsonDocument()
                                                                  {
                                                                      {"_id", BsonObjectId.Create(id)}
                                                                  },
                                                                  new BsonDocument()
                                                                  {
                                                                      {
                                                                          "$set", new BsonDocument()
                                                                                  {
                                                                                      {"noOfDays", noOfDays},
                                                                                      {"recurringTime", recurringTime},
                                                                                      {"email", bsonElements},
                                                                                      {"modifiedBy", userProfileId},
                                                                                      {"modifiedOn", DateTime.UtcNow}
                                                                                  }
                                                                      }
                                                                  });
            if (result.ModifiedCount == 1)
            {
                var model = new
                {
                    id,
                    noOfDays,
                    recurringTime
                };

                var content = JsonConvert.SerializeObject(model);
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(_configuration[key: "Setting:Url"] + "/api/Setting/EmailReminder/UpdateEmailReminder"),
                    Method = HttpMethod.Post,
                    Content = new StringContent(content: content,
                                                             encoding: Encoding.UTF8,
                                                             mediaType: "application/json")
                };
                request.Headers.Add("Authorization", authHeader);
                var response = await _httpClient.SendAsync(request);

                response.EnsureSuccessStatusCode();
            }
            return result.ModifiedCount == 1;
        }
        public async Task<bool> DeleteEmailReminder(string id,
                                                   int userProfileId,
                                                   IEnumerable<string> authHeader)
        {
            var model = new
            {
                id
            };

            IMongoCollection<Entity.EmailReminder> collection = mongoService.db.GetCollection<Entity.EmailReminder>("EmailReminder");
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
            if (result.ModifiedCount == 1)
            {

                var content = JsonConvert.SerializeObject(model);
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(_configuration[key: "Setting:Url"] + "/api/Setting/EmailReminder/DeleteEmailReminder"),
                    Method = HttpMethod.Delete,
                    Content = new StringContent(content: content,
                                                             encoding: Encoding.UTF8,
                                                             mediaType: "application/json")
                };
                request.Headers.Add("Authorization", authHeader);
                var response = await _httpClient.SendAsync(request);

                response.EnsureSuccessStatusCode();
            }

            return result.ModifiedCount == 1;
        }
        public async Task<bool> EnableDisableEmailReminder(string id, bool isActive,
                                                   int userProfileId, IEnumerable<string> authHeader)
        {
            IMongoCollection<Entity.EmailReminder> collection = mongoService.db.GetCollection<Entity.EmailReminder>("EmailReminder");
            UpdateResult result = await collection.UpdateOneAsync(new BsonDocument()
                                                                  {
                                                                      {"_id", BsonObjectId.Create(id)}
                                                                  },
                                                                  new BsonDocument()
                                                                  {
                                                                      {
                                                                          "$set", new BsonDocument()
                                                                                  {
                                                                                      {"isActive", isActive},
                                                                                      {"modifiedBy", userProfileId},
                                                                                      {"modifiedOn", DateTime.UtcNow}
                                                                                  }
                                                                      }
                                                                  });
            if (result.ModifiedCount == 1)
            {
                var model = new
                {
                    id = new[] { id },
                    isActive
                };

                var content = JsonConvert.SerializeObject(model);

                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(_configuration[key: "Setting:Url"] + "/api/Setting/emailreminder/EnableDisableEmailReminders"),
                    Method = HttpMethod.Post,
                    Content = new StringContent(content: content,
                                                            encoding: Encoding.UTF8,
                                                            mediaType: "application/json")
                };

                request.Headers.Add("Authorization", authHeader);
                var response = await _httpClient.SendAsync(request);

                response.EnsureSuccessStatusCode();
            }
            return result.ModifiedCount == 1;
        }
        public async Task EnableDisableAllEmailReminders(bool isActive, int userProfileId, IEnumerable<string> authHeader)
        {
            int jobTypeId = JobType.EmailReminder;

            var model = new
            {
                isActive,
                jobTypeId
            };
            var content = JsonConvert.SerializeObject(model);
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(_configuration[key: "Setting:Url"] + "/api/Setting/emailreminder/EnableDisableAllEmailReminders"),
                Method = HttpMethod.Post,
                Content = new StringContent(content: content,
                                                        encoding: Encoding.UTF8,
                                                        mediaType: "application/json")
            };

            request.Headers.Add("Authorization", authHeader);
            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();
        }

        public async Task<List<RemainingDocuments>> GetDocumentStatusByLoanIds(List<RemainingDocuments> remainingDocumentsModel)
        {
            foreach (var item in remainingDocumentsModel)
            {
                IMongoCollection<Entity.Request> collectionRemainingDocuments = mongoService.db.GetCollection<Entity.Request>("Request");

                using var asyncCursorRemainingDocuments = collectionRemainingDocuments.Aggregate(PipelineDefinition<Entity.Request, BsonDocument>.Create(
                    @"{""$match"": {
                  ""loanApplicationId"": " + item.loanApplicationId + @"
                            }
                        }", @"{
                            ""$unwind"": ""$requests""
                        }", @"{""$match"": {
                                ""requests.status"": """ + RequestStatus.Active + @"""
                            }
                        }", @"{
                            ""$unwind"": ""$requests.documents""
                        }", @"{
                            ""$match"": { ""$or"":[
                                {""requests.documents.status"": """ + DocumentStatus.BorrowerTodo + @"""},
                                {""requests.documents.status"": """ + DocumentStatus.Started + @"""}
                            ]}
                        }", @"{
                            ""$project"": {
                                            ""_id"":1
                                            }
                        }"
                    ));

                int? remainingDocuments = null;
                if (await asyncCursorRemainingDocuments.MoveNextAsync())
                {
                    foreach (var current in asyncCursorRemainingDocuments.Current)
                    {
                        RemainingDocumentQuery query = BsonSerializer.Deserialize<RemainingDocumentQuery>(current);
                        if (query != null && query.id != null)
                            remainingDocuments = (remainingDocuments ?? 0) + 1;
                    }
                }
                if (remainingDocuments != null && remainingDocuments > 0)
                    item.isDocumentRemaining = true;
                else
                    item.isDocumentRemaining = false;
            }

            return remainingDocumentsModel;
        }
        public async Task<Model.EmailReminder> GetEmailReminderById(string id)
        {
            IMongoCollection<Entity.EmailReminder> collection = mongoService.db.GetCollection<Entity.EmailReminder>("EmailReminder");

            Model.EmailReminder emailReminder = new Model.EmailReminder();

            using var asyncCursorEmailTemplates = collection.Aggregate(PipelineDefinition<Entity.EmailReminder, BsonDocument>.Create(
                                                                                                                                     @"{""$match"": {
                  ""_id"": " + new ObjectId(id).ToJson() + @"
                            }
                        }", @"{
                            ""$project"": {
                                ""_id"": 1,                               
                                ""noOfDays"": ""$noOfDays"",
                                ""recurringTime"": ""$recurringTime"",
                                ""isActive"": ""$isActive"",
                                ""email"": ""$email""
                                }
                         } "
                        ));
            while (await asyncCursorEmailTemplates.MoveNextAsync())
            {
                foreach (var current in asyncCursorEmailTemplates.Current)
                {
                    Model.EmailReminder dto = BsonSerializer.Deserialize<Model.EmailReminder>(current);
                    emailReminder = dto;
                }
            }

            return emailReminder;
        }

        public async Task<List<Model.EmailReminder>> GetEmailReminderByIds(List<string> emailReminderIds, int tenantId)
        {
            IMongoCollection<Entity.EmailReminder> collection = mongoService.db.GetCollection<Entity.EmailReminder>("EmailReminder");

            List<Model.EmailReminder> emailReminders = new List<EmailReminder>();

            using var asyncCursorEmailTemplates = collection.Aggregate(PipelineDefinition<Entity.EmailReminder, BsonDocument>.Create(
            @"{""$match"": { ""_id"": { ""$in"": " + new BsonArray(emailReminderIds.Select(x => new ObjectId(x))).ToJson() + @"
                              }}
                        }", @"{
                            ""$project"": {
                                ""_id"": 1,                               
                                ""noOfDays"": ""$noOfDays"",
                                ""recurringTime"": ""$recurringTime"",
                                ""isActive"": ""$isActive"",
                                ""email"": ""$email""
                                }
                         } "
                        ));
            while (await asyncCursorEmailTemplates.MoveNextAsync())
            {
                foreach (var current in asyncCursorEmailTemplates.Current)
                {
                    Model.EmailReminder dto = BsonSerializer.Deserialize<Model.EmailReminder>(current);
                    emailReminders.Add(dto);
                }
            }
            return emailReminders;
        }
    }
}
