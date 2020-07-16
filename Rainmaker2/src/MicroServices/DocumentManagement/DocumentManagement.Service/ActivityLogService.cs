using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DocumentManagement.Entity;
using DocumentManagement.Model;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace DocumentManagement.Service
{
    public class ActivityLogService : IActivityLogService
    {
        private readonly IMongoService mongoService;

        public ActivityLogService(IMongoService mongoService)
        {
            this.mongoService = mongoService;
        }

        public async Task InsertLog(string activityId, string activity)
        {
            IMongoCollection<ActivityLog> collection = mongoService.db.GetCollection<ActivityLog>("ActivityLog");

            BsonDocument bsonElements = new BsonDocument();
            bsonElements.Add("_id", ObjectId.GenerateNewId());
            bsonElements.Add("dateTime", DateTime.UtcNow);
            bsonElements.Add("activity", activity);

            UpdateResult result = await collection.UpdateOneAsync(new BsonDocument()
                {
                    { "_id", new ObjectId(activityId)}
                }, new BsonDocument()
                {
                    { "$push", new BsonDocument()
                        {
                            { "log", bsonElements  }
                        }
                    },
                }
            );
        }

        public async Task<string> GetActivityLogId(string loanId, string requestId, string docId)
        {
            string activityLogId = string.Empty;

            IMongoCollection<Entity.ActivityLog> collection = mongoService.db.GetCollection<Entity.ActivityLog>("ActivityLog");

            using var asyncCursor = collection.Aggregate(
                PipelineDefinition<Entity.ActivityLog, BsonDocument>.Create(
                    @"{""$match"": {
                    ""loanId"": " + new ObjectId(loanId).ToJson() + @",
                    ""requestId"": " + new ObjectId(requestId).ToJson() + @",
                    ""docId"": " + new ObjectId(docId).ToJson() + @"
                            }
                        }", @"{
                            ""$sort"": {
                                 ""dateTime"": -1
                            }
                        }", @"{
                            ""$limit"":1
                        }", @"{
                            ""$project"": {
                                 ""_id"": 1
                            }
                        }"
                ));

            if (await asyncCursor.MoveNextAsync())
            {
                foreach (var current in asyncCursor.Current)
                {
                    ActivityLogIdQuery query = BsonSerializer.Deserialize<ActivityLogIdQuery>(current);
                    activityLogId = query._id;
                }
            }
            return activityLogId;
        }
    }
}
