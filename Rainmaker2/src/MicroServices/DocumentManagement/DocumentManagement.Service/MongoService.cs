using DnsClient.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace DocumentManagement.Service
{
    public class MongoService : IMongoService
    {
        public IMongoDatabase db { get; set; }
        public IMongoClient client { get; set; }
        public MongoService(IConfiguration config, ILogger<MongoService> logger, HttpClient httpClient)
        {
            var csResponse = httpClient.GetAsync($"{config["KeyStore:Url"]}/api/keystore/keystore?key=DocumentManagementCS").Result;
            csResponse.EnsureSuccessStatusCode();
            var mongoConnectionUrl = new MongoUrl(csResponse.Content.ReadAsStringAsync().Result);
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
