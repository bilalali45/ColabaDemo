using DocManager.Model;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DocManager.Service
{
    public class RainmakerService : IRainmakerService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IMongoService mongoService;
        public RainmakerService(HttpClient _httpClient, IConfiguration _configuration, IMongoService mongoService )
        {
            this._httpClient = _httpClient;
            this._configuration = _configuration;
            this.mongoService = mongoService;
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
        public async Task<int> GetLoanApplicationId(string loanApplicationId)
        {
            IMongoCollection<Request> collectionRequest = mongoService.db.GetCollection<Request>("Request");

            using var asyncCursorRequest = collectionRequest.Aggregate(
                PipelineDefinition<Request, BsonDocument>.Create(
                    @"{""$match"": {
                    ""_id"": " + new ObjectId(loanApplicationId).ToJson() + @"
                            }
                        }", @"{
                            ""$project"": {
                                ""loanApplicationId"": 1
                            }
                        }"
                ));

            int loanId = -1;
            if (await asyncCursorRequest.MoveNextAsync())
            {
                foreach (var current in asyncCursorRequest.Current)
                {
                    LoanApplicationIdQuery query = BsonSerializer.Deserialize<LoanApplicationIdQuery>(current);
                    loanId = query.loanApplicationId;
                }


            }
            return loanId;
        }

        public async Task UpdateLoanInfo(int? loanApplicationId, string id, IEnumerable<string> authHeader)
        {
            if (loanApplicationId == null)
                loanApplicationId = await this.GetLoanApplicationId(id);

            IMongoCollection<Request> collectionLastDocUploadDate = mongoService.db.GetCollection<Request>("Request");

            using var asyncCursor = collectionLastDocUploadDate.Aggregate(PipelineDefinition<Request, BsonDocument>.Create(
                @"{""$match"": {
                  ""loanApplicationId"": " + loanApplicationId + @"
                            }
                        }", @"{
                            ""$unwind"": ""$requests""
                        }", @"{
                            ""$unwind"": ""$requests.documents""
                        }", @"{
                            ""$unwind"": ""$requests.documents.files""
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

            DateTime? lastDocUploadDate = null;
            DateTime? lastFileDocUploadDate = null;
            DateTime? lastMcuFileDocUploadDate = null;
            if (await asyncCursor.MoveNextAsync())
            {
                foreach (var current in asyncCursor.Current)
                {
                    LastDocUploadQuery query = BsonSerializer.Deserialize<LastDocUploadQuery>(current);
                    lastFileDocUploadDate = query.LastDocUploadDate;
                }
            }
            IMongoCollection<Request> collectionLastDocUploadDateMcu = mongoService.db.GetCollection<Request>("Request");

            using var asyncCursorMcu = collectionLastDocUploadDateMcu.Aggregate(PipelineDefinition<Request, BsonDocument>.Create(
                @"{""$match"": {
                  ""loanApplicationId"": " + loanApplicationId + @"
                            }
                        }", @"{
                            ""$unwind"": ""$requests""
                        }", @"{
                            ""$unwind"": ""$requests.documents""
                        }", @"{
                            ""$unwind"": ""$requests.documents.mcuFiles""
                        }", @"{
                            ""$sort"": {
                                 ""requests.documents.mcuFiles.fileUploadedOn"": -1
                            }
                        }", @"{
                            ""$limit"":1
                        }", @"{
                            ""$project"": {
                                ""_id"": 0,
                               ""LastDocUploadDate"": ""$requests.documents.mcuFiles.fileUploadedOn""
                            }
                        }"
                ));
            if (await asyncCursorMcu.MoveNextAsync())
            {
                foreach (var current in asyncCursorMcu.Current)
                {
                    LastDocUploadQuery query = BsonSerializer.Deserialize<LastDocUploadQuery>(current);
                    lastMcuFileDocUploadDate = query.LastDocUploadDate;
                }
            }
            if ((lastMcuFileDocUploadDate ?? DateTime.MinValue) > (lastFileDocUploadDate ?? DateTime.MinValue))
            {
                lastDocUploadDate = lastMcuFileDocUploadDate;
            }
            else
            {
                lastDocUploadDate = lastFileDocUploadDate;
            }

            IMongoCollection<Request> collectionLastDocRequestSentDate = mongoService.db.GetCollection<Request>("Request");

            using var asyncCursorLastDocRequestSentDate = collectionLastDocRequestSentDate.Aggregate(PipelineDefinition<Request, BsonDocument>.Create(
                @"{""$match"": {
                  ""loanApplicationId"": " + loanApplicationId + @"
                            }
                        }", @"{
                            ""$unwind"": ""$requests""
                        }", @"{""$match"": {
                                ""requests.status"": """ + RequestStatus.Active + @"""
                            }
                        }",
                @"{""$match"":{""$or"":[{""requests.isFromBorrower"":{""$exists"":false}},{""requests.isFromBorrower"":false}]}}"
                , @"{
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

            DateTime? lastDocRequestSentDate = null;
            if (await asyncCursorLastDocRequestSentDate.MoveNextAsync())
            {
                foreach (var current in asyncCursorLastDocRequestSentDate.Current)
                {
                    LastDocRequestSentDateQuery query = BsonSerializer.Deserialize<LastDocRequestSentDateQuery>(current);
                    lastDocRequestSentDate = query.LastDocRequestSentDate;
                }
            }

            IMongoCollection<Request> collectionRemainingDocuments = mongoService.db.GetCollection<Request>("Request");

            using var asyncCursorRemainingDocuments = collectionRemainingDocuments.Aggregate(PipelineDefinition<Request, BsonDocument>.Create(
                @"{""$match"": {
                  ""loanApplicationId"": " + loanApplicationId + @"
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
                                            ""_id"":0,
                                            ""isMcuVisible"":""$requests.documents.isMcuVisible""
                                            }
                        }"
                ));

            int? remainingDocuments = null;
            if (await asyncCursorRemainingDocuments.MoveNextAsync())
            {
                foreach (var current in asyncCursorRemainingDocuments.Current)
                {
                    RemainingDocumentsQuery query = BsonSerializer.Deserialize<RemainingDocumentsQuery>(current);
                    if (query.isMcuVisible == null || query.isMcuVisible == true)
                        remainingDocuments = (remainingDocuments ?? 0) + 1;
                }
            }

            IMongoCollection<Request> collectionOutstandingDocuments = mongoService.db.GetCollection<Request>("Request");

            using var asyncCursorOutstandingDocuments = collectionOutstandingDocuments.Aggregate(PipelineDefinition<Request, BsonDocument>.Create(
                @"{""$match"": {
                  ""loanApplicationId"": " + loanApplicationId + @"
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
                                {""requests.documents.status"": """ + DocumentStatus.Draft + @"""},
                                {""requests.documents.status"": """ + DocumentStatus.PendingReview + @"""}
                            ]}
                        }", @"{
                            ""$project"": {
                                            ""_id"":0,
                                            ""isMcuVisible"":""$requests.documents.isMcuVisible""
                                            }
                        }"
                ));

            int? outstandingDocuments = null;
            if (await asyncCursorOutstandingDocuments.MoveNextAsync())
            {
                foreach (var current in asyncCursorOutstandingDocuments.Current)
                {
                    OutstandingDocumentsQuery query = BsonSerializer.Deserialize<OutstandingDocumentsQuery>(current);
                    if (query.isMcuVisible == null || query.isMcuVisible == true)
                        outstandingDocuments = (outstandingDocuments ?? 0) + 1;
                }
            }

            IMongoCollection<Request> collectionCompletedDocuments = mongoService.db.GetCollection<Request>("Request");

            using var asyncCursorCompletedDocuments = collectionCompletedDocuments.Aggregate(PipelineDefinition<Request, BsonDocument>.Create(
                @"{""$match"": {
                  ""loanApplicationId"": " + loanApplicationId + @"
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
                                {""requests.documents.status"": """ + DocumentStatus.Completed + @"""},
                                {""requests.documents.status"": """ + DocumentStatus.ManuallyAdded + @"""}
                            ]}
                        }", @"{
                            ""$project"": {
                                            ""_id"":0,
                                            ""isMcuVisible"":""$requests.documents.isMcuVisible""
                                            }
                        }"
                ));

            int? completedDocuments = null;
            if (await asyncCursorCompletedDocuments.MoveNextAsync())
            {
                foreach (var current in asyncCursorCompletedDocuments.Current)
                {
                    CompletedDocumentsQuery query = BsonSerializer.Deserialize<CompletedDocumentsQuery>(current);
                    if (query.isMcuVisible == null || query.isMcuVisible == true)
                        completedDocuments = (completedDocuments ?? 0) + 1;
                }
            }

            var content = new
            {
                loanApplicationId,
                lastDocUploadDate,
                lastDocRequestSentDate,
                remainingDocuments,
                outstandingDocuments,
                completedDocuments
            };
            var c = JsonConvert.SerializeObject(content);
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(_configuration[key: "RainMaker:Url"] + "/api/rainmaker/LoanApplication/UpdateLoanInfo"),
                Method = HttpMethod.Post,
                Content = new StringContent(content: c,
                    encoding: Encoding.UTF8,
                    mediaType: "application/json")
            };
            request.Headers.Add("Authorization", authHeader);
            await _httpClient.SendAsync(request);
        }

    }
}
