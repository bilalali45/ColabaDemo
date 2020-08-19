using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DocumentManagement.Entity;
using DocumentManagement.Model;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace DocumentManagement.Service
{
    public class RainmakerService : IRainmakerService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IMongoService mongoService;
        private readonly IFileService fileService;
        public RainmakerService(HttpClient _httpClient, IConfiguration _configuration, IMongoService mongoService,IFileService fileService)
        {
            this._httpClient = _httpClient;
            this._configuration = _configuration;
            this.mongoService = mongoService;
            this.fileService = fileService;
        }

        public async Task<string> PostLoanApplication(int loanApplicationId,bool isDraft,IEnumerable<string> authHeader )
        {
            var content = new
            {
                loanApplicationId,
                isDraft
            };

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(_configuration[key: "RainMaker:Url"] + "/api/rainmaker/LoanApplication/PostLoanApplication"),
                Method = HttpMethod.Post,
                Content = new StringContent(content: content.ToJson(),
                    encoding: Encoding.UTF8,
                    mediaType: "application/json")
            };
            request.Headers.Add("Authorization",authHeader);
            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsStringAsync();
            else
                return null;
        }

        public async Task SendBorrowerEmail(int loanApplicationId, string emailBody, int activityForId,int userId ,string userName, IEnumerable<string> authHeader )
        {
            var content = new
            {
                loanApplicationId,
                emailBody,
                activityForId
            };

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(_configuration[key: "RainMaker:Url"] + "/api/rainmaker/LoanApplication/SendBorrowerEmail"),
                Method = HttpMethod.Post,
                Content = new StringContent(content: content.ToJson(),
                    encoding: Encoding.UTF8,
                    mediaType: "application/json")
            };
            request.Headers.Add("Authorization", authHeader);
            var resp = await _httpClient.SendAsync(request);
            if (resp.IsSuccessStatusCode)
            {
                IMongoCollection<Entity.Request> collectionRequest = mongoService.db.GetCollection<Entity.Request>("Request");

                using var asyncCursorRequest = collectionRequest.Aggregate(
                    PipelineDefinition<Entity.Request, BsonDocument>.Create(
                        @"{""$match"": {
                  ""loanApplicationId"": " + loanApplicationId + @"
                            }
                        }", @"{
                            ""$project"": {
                                ""_id"": 1
                            }
                        }"
                    ));
              
                string loanId = string.Empty;
                if (await asyncCursorRequest.MoveNextAsync())
                {
                   
                    foreach (var current in asyncCursorRequest.Current)
                    {
                        LoanApplicationIdQuery query = BsonSerializer.Deserialize<LoanApplicationIdQuery>(current);
                        loanId = query._id;
                    }

                    //insert emaillog

                    //IMongoCollection<Entity.EmailLog> collection = mongoService.db.GetCollection<Entity.EmailLog>("EmailLog");

                    //using var asyncCursorEmailLog = collection.Aggregate(
                    //PipelineDefinition<Entity.EmailLog, BsonDocument>.Create(
                    //    @"{""$match"": {
                    //    ""loanId"": " + new ObjectId(loanId).ToJson() + @"
                    //        }
                    //    }", @"{
                    //        ""$project"": {
                    //            ""_id"": 1
                    //        }
                    //    }"
                    //));

                    //string message = ActivityStatus.RequestedBy;

                    //if (await asyncCursorEmailLog.MoveNextAsync())
                    //{
                    //    foreach (var current in asyncCursorEmailLog.Current)
                    //    {
                    //        message = ActivityStatus.RerequestedBy;
                    //    }
                    //}

                    //Entity.EmailLog emailLog = new Entity.EmailLog() { id = ObjectId.GenerateNewId().ToString(), userId = userId, userName = userName, dateTime = DateTime.UtcNow, emailText = emailBody, loanId = loanId, message = message };
                    //await collection.InsertOneAsync(emailLog);
                }
            }
        }
        public async Task<LoanApplicationModel> GetByLoanApplicationId(int loanApplicationId,
            IEnumerable<string> authHeader)
        {
            var content = new
            {
                loanApplicationId
            };

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(_configuration[key: "RainMaker:Url"] + "/api/rainmaker/LoanApplication/GetByLoanApplicationId"),
                Method = HttpMethod.Post,
                Content = new StringContent(content: content.ToJson(),
                    encoding: Encoding.UTF8,
                    mediaType: "application/json")
            };
            request.Headers.Add("Authorization", authHeader);
            var resp = await _httpClient.SendAsync(request);
            if (resp.IsSuccessStatusCode)
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<LoanApplicationModel>(await resp.Content.ReadAsStringAsync());
            }
            throw new Exception("Unable to get loan info");
        }

        public async Task UpdateLoanInfo(int? loanApplicationId,string id, IEnumerable<string> authHeader)
        {
            if(loanApplicationId == null)
                 loanApplicationId = await fileService.GetLoanApplicationId(id);

            IMongoCollection<Entity.Request> collectionLastDocUploadDate = mongoService.db.GetCollection<Entity.Request>("Request");

            using var asyncCursor = collectionLastDocUploadDate.Aggregate(PipelineDefinition<Entity.Request, BsonDocument>.Create(
                @"{""$match"": {
                  ""loanApplicationId"": " + loanApplicationId + @"
                            }
                        }", @"{
                            ""$unwind"": ""$requests"",
                            ""preserveNullAndEmptyArrays"": false
                        }", @"{
                            ""$unwind"": ""$requests.documents"",
                            ""preserveNullAndEmptyArrays"": false
                        }", @"{
                            ""$unwind"": ""$requests.documents.files"",
                            ""preserveNullAndEmptyArrays"": false
                        }", @"{
                            ""$sort"": {
                                 ""requests.documents.files.fileUploadedOn"": -1
                            }
                        }", @"{
                            ""$limit"":1
                        }", @"{
                            ""$project"": {
                                ""_id"": 0,
                               ""LastDocUploadDate"": ""$requests.documents.files.fileUploadedOn""
                            }
                        }"
                ));

            DateTime lastDocUploadDate;
            if (await asyncCursor.MoveNextAsync())
            {
                foreach (var current in asyncCursor.Current)
                {
                    LastDocUploadQuery query = BsonSerializer.Deserialize<LastDocUploadQuery>(current);
                    lastDocUploadDate = query.LastDocUploadDate;
                }
            }

            IMongoCollection<Entity.Request> collectionLastDocRequestSentDate = mongoService.db.GetCollection<Entity.Request>("Request");

            using var asyncCursorLastDocRequestSentDate = collectionLastDocRequestSentDate.Aggregate(PipelineDefinition<Entity.Request, BsonDocument>.Create(
                @"{""$match"": {
                  ""loanApplicationId"": " + loanApplicationId + @"
                            }
                        }", @"{
                            ""$unwind"": ""$requests"",
                            ""preserveNullAndEmptyArrays"": false
                        }", @"{""$match"": {
                                ""requests.status"": """ + RequestStatus.Active + @"""
                            }
                        }", @"{
                            ""$sort"": {
                                 ""requests.createdOn"": -1
                            }
                        }", @"{
                            ""$limit"":1
                        }", @"{
                            ""$project"": {
                                ""_id"": 0,
                               ""LastDocRequestSentDate"": ""$requests.createdOn""
                            }
                        }"
                ));

            DateTime lastDocRequestSentDate;
            if (await asyncCursorLastDocRequestSentDate.MoveNextAsync())
            {
                foreach (var current in asyncCursorLastDocRequestSentDate.Current)
                {
                    LastDocRequestSentDateQuery query = BsonSerializer.Deserialize<LastDocRequestSentDateQuery>(current);
                    lastDocRequestSentDate = query.LastDocRequestSentDate;
                }
            }

            IMongoCollection<Entity.Request> collectionRemainingDocuments = mongoService.db.GetCollection<Entity.Request>("Request");

            using var asyncCursorRemainingDocuments = collectionRemainingDocuments.Aggregate(PipelineDefinition<Entity.Request, BsonDocument>.Create(
                @"{""$match"": {
                  ""loanApplicationId"": " + loanApplicationId + @"
                            }
                        }", @"{
                            ""$unwind"": ""$requests"",
                            ""preserveNullAndEmptyArrays"": false
                        }", @"{""$match"": {
                                ""requests.status"": """ + RequestStatus.Active + @"""
                            }
                        }", @"{
                            ""$unwind"": ""$requests.documents"",
                            ""preserveNullAndEmptyArrays"": false
                        }", @"{
                            ""$match"": { ""$or"":[
                                {""requests.documents.status"": """ + DocumentStatus.BorrowerTodo + @"""},
                                {""requests.documents.status"": """ + DocumentStatus.Started + @"""}
                            ]}
                        }", @"{
                            ""$count"": ""RemainingDocuments""
                        }"
                ));

            int remainingDocuments = 0;
            if (await asyncCursorRemainingDocuments.MoveNextAsync())
            {
                foreach (var current in asyncCursorRemainingDocuments.Current)
                {
                    RemainingDocumentsQuery query = BsonSerializer.Deserialize<RemainingDocumentsQuery>(current);
                    remainingDocuments = query.RemainingDocuments;
                }
            }

            IMongoCollection<Entity.Request> collectionOutstandingDocuments = mongoService.db.GetCollection<Entity.Request>("Request");

            using var asyncCursorOutstandingDocuments = collectionOutstandingDocuments.Aggregate(PipelineDefinition<Entity.Request, BsonDocument>.Create(
                @"{""$match"": {
                  ""loanApplicationId"": " + loanApplicationId + @"
                            }
                        }", @"{
                            ""$unwind"": ""$requests"",
                            ""preserveNullAndEmptyArrays"": false
                        }", @"{""$match"": {
                                ""requests.status"": """ + RequestStatus.Active + @"""
                            }
                        }", @"{
                            ""$unwind"": ""$requests.documents"",
                            ""preserveNullAndEmptyArrays"": false
                        }", @"{
                            ""$match"": { ""$or"":[
                                {""requests.documents.status"": """ + DocumentStatus.Draft + @"""},
                                {""requests.documents.status"": """ + DocumentStatus.PendingReview + @"""}
                            ]}
                        }", @"{
                            ""$count"": ""RemainingDocuments""
                        }"
                ));

            int outstandingDocuments = 0;
            if (await asyncCursorOutstandingDocuments.MoveNextAsync())
            {
                foreach (var current in asyncCursorOutstandingDocuments.Current)
                {
                    OutstandingDocumentsQuery query = BsonSerializer.Deserialize<OutstandingDocumentsQuery>(current);
                    outstandingDocuments = query.OutstandingDocuments;
                }
            }

            IMongoCollection<Entity.Request> collectionCompletedDocuments = mongoService.db.GetCollection<Entity.Request>("Request");

            using var asyncCursorCompletedDocuments = collectionCompletedDocuments.Aggregate(PipelineDefinition<Entity.Request, BsonDocument>.Create(
                @"{""$match"": {
                  ""loanApplicationId"": " + loanApplicationId + @"
                            }
                        }", @"{
                            ""$unwind"": ""$requests"",
                            ""preserveNullAndEmptyArrays"": false
                        }", @"{""$match"": {
                                ""requests.status"": """ + RequestStatus.Active + @"""
                            }
                        }", @"{
                            ""$unwind"": ""$requests.documents"",
                            ""preserveNullAndEmptyArrays"": false
                        }", @"{
                            ""$match"": { ""$or"":[
                                {""requests.documents.status"": """ + DocumentStatus.Completed + @"""}
                            ]}
                        }", @"{
                            ""$count"": ""RemainingDocuments""
                        }"
                ));

            int completedDocuments = 0;
            if (await asyncCursorCompletedDocuments.MoveNextAsync())
            {
                foreach (var current in asyncCursorCompletedDocuments.Current)
                {
                    CompletedDocumentsQuery query = BsonSerializer.Deserialize<CompletedDocumentsQuery>(current);
                    completedDocuments = query.CompletedDocuments;
                }
            }
        }
    }
}
