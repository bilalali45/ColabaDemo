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
}
