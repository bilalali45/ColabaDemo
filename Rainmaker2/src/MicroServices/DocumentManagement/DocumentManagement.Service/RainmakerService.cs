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
        public RainmakerService(HttpClient _httpClient, IConfiguration _configuration, IMongoService mongoService)
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
    }
}
