using DocManager.Model;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DocManager.Service
{
    public class LockService : ILockService
    {
        private readonly IMongoService mongoService;
        private readonly ISettingService settingService;
        public LockService(IMongoService mongoService, ISettingService settingService)
        {
            this.mongoService = mongoService;
            this.settingService = settingService;
        }
        public async Task<Lock> AcquireLock(LockModel lockModel, int UserId, string UserName)
        {
            IMongoCollection<Lock> lockCollection = mongoService.db.GetCollection<Lock>("Lock");
            Lock l = null;
            LockSetting setting = await settingService.GetLockSetting();
            using var asyncCursor = lockCollection.Aggregate(
               PipelineDefinition<Lock, BsonDocument>.Create(
                   @"{""$match"": {
                    ""loanApplicationId"": " + lockModel.loanApplicationId + @"
                            }
                        }",
                      @"{
                            ""$project"": {
                                 ""_id"": 1,
                                ""lockDateTime"": ""$lockDateTime"",
                                ""lockUserId"": ""$lockUserId"",
                                ""lockUserName"": ""$lockUserName"",
                                ""loanApplicationId"": ""$loanApplicationId""
                            }
                        }"
               ));

            if (await asyncCursor.MoveNextAsync())
            {
                foreach (var current in asyncCursor.Current)
                {
                    l = BsonSerializer.Deserialize<Lock>(current);
                }
            }
            if (l == null)
            {
                // no lock exists, thus acquire lock
                l = new Lock()
                {
                    loanApplicationId = lockModel.loanApplicationId,
                    lockUserId = UserId,
                    lockUserName = UserName,
                    lockDateTime = DateTime.UtcNow
                };
                await lockCollection.InsertOneAsync(l);
                return l;
            }
            else if (l.lockUserId == UserId)
            {
                // same user has lock, thus update lock
                l.lockDateTime = DateTime.UtcNow;
                UpdateResult result = await lockCollection.UpdateOneAsync(new BsonDocument()
                        {
                            { "loanApplicationId", lockModel.loanApplicationId},
                        }, new BsonDocument()
                        {
                            { "$set", new BsonDocument(){
                                {"lockDateTime",l.lockDateTime }
                            }
                            }

                        });
                if (result.ModifiedCount == 1)
                    return l;
            }
            else if ((DateTime.UtcNow - l.lockDateTime).TotalMinutes > setting.lockTimeInMinutes)
            {
                // lock has expired, thus acquire lock
                l.lockDateTime = DateTime.UtcNow;
                l.lockUserId = UserId;
                l.lockUserName = UserName;
                UpdateResult result = await lockCollection.UpdateOneAsync(new BsonDocument()
                        {
                            { "loanApplicationId", lockModel.loanApplicationId},
                        }, new BsonDocument()
                        {
                            { "$set", new BsonDocument(){
                                {"lockDateTime",l.lockDateTime },
                                {"lockUserId",l.lockUserId },
                                {"lockUserName",l.lockUserName }
                            }
                            }

                        });
                if (result.ModifiedCount == 1)
                    return l;
            }
            else
            {
                // other user has the lock
                return l;
            }
            return null;
        }
        public async Task<Lock> RetainLock(LockModel lockModel, int UserId, string UserName)
        {
            IMongoCollection<Lock> lockCollection = mongoService.db.GetCollection<Lock>("Lock");
            Lock l = null;
            LockSetting setting = await settingService.GetLockSetting();
            using var asyncCursor = lockCollection.Aggregate(
               PipelineDefinition<Lock, BsonDocument>.Create(
                   @"{""$match"": {
                    ""loanApplicationId"": " + lockModel.loanApplicationId + @"
                            }
                        }",
                      @"{
                            ""$project"": {
                                 ""_id"": 1,
                                ""lockDateTime"": ""$lockDateTime"",
                                ""lockUserId"": ""$lockUserId"",
                                ""lockUserName"": ""$lockUserName"",
                                ""loanApplicationId"": ""$loanApplicationId""
                            }
                        }"
               ));

            if (await asyncCursor.MoveNextAsync())
            {
                foreach (var current in asyncCursor.Current)
                {
                    l = BsonSerializer.Deserialize<Lock>(current);
                }
            }
            if (l?.lockUserId==UserId)
            {
                // lock has expired, thus acquire lock
                l.lockDateTime = DateTime.UtcNow;
                UpdateResult result = await lockCollection.UpdateOneAsync(new BsonDocument()
                        {
                            { "loanApplicationId", lockModel.loanApplicationId},
                        }, new BsonDocument()
                        {
                            { "$set", new BsonDocument(){
                                {"lockDateTime",l.lockDateTime }
                            }
                            }

                        });
                if (result.ModifiedCount == 1)
                    return l;
            }
            return null;
        }
    }
}
