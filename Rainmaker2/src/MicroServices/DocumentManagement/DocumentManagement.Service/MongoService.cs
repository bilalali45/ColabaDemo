using DnsClient.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagement.Service
{
    public class MongoService : IMongoService
    {
        public IMongoDatabase db { get; set; }
        public IMongoClient client { get; set; }
        public MongoService(IConfiguration config, ILogger<MongoService> logger)
        {
            var mongoConnectionUrl = new MongoUrl(config["Mongo:ConnectionString"]);
            var mongoClientSettings = MongoClientSettings.FromUrl(mongoConnectionUrl);
            mongoClientSettings.ClusterConfigurator = cb =>
            {
                cb.Subscribe<CommandStartedEvent>(e =>
                {
                    logger.LogInformation($"{e.CommandName} - {e.Command.ToJson()}");
                });
            };
            client = new MongoClient(mongoClientSettings);
            db = client.GetDatabase(config["Mongo:Database"]);
        }
    }

    public class MongoAggregateService<T> : IMongoAggregateService<T>
    {
        public IAggregateFluent<BsonDocument> Unwind(IAggregateFluent<T> aggregateFluent,FieldDefinition<T> field,AggregateUnwindOptions<BsonDocument> options=null)
        {
            return aggregateFluent.Unwind(field,options);
        }
        public IAggregateFluent<BsonDocument> Lookup(IAggregateFluent<T> aggregateFluent,string foreignCollection, FieldDefinition<T> localField, FieldDefinition<BsonDocument> foreignField, FieldDefinition<BsonDocument> @as)
        {
            return aggregateFluent.Lookup(foreignCollection,localField,foreignField,@as);
        }
        public IAggregateFluent<BsonDocument> Project(IAggregateFluent<T> aggregateFluent,ProjectionDefinition<T, BsonDocument> projection)
        {
            return aggregateFluent.Project(projection);
        }
    }
}
