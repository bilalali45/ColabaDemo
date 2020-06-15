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
        public MongoService(IConfiguration config, ILogger<MongoService> logger, IHttpClientFactory clientFactory)
        {
            var httpClient = clientFactory.CreateClient();
            var csResponse = httpClient.GetAsync($"{config["KeyStore:Url"]}/api/keystore/keystore?key=DocumentManagementCS").Result;
            if (!csResponse.IsSuccessStatusCode)
            {
                throw new Exception("Unable to load key from key store");
            }
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
