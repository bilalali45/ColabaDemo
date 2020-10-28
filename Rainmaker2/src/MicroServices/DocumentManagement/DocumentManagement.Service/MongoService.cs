using DnsClient.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;
using System;
using System.Net.Http;

namespace DocumentManagement.Service
{
    public class MongoService : IMongoService
    {
        private static object lockObject = new object();
        private static volatile string connectionString = string.Empty;
        public IMongoDatabase db { get; set; }
        private static IMongoClient client { get; set; }
        public MongoService(IConfiguration config, ILogger<MongoService> logger, HttpClient httpClient)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                lock (lockObject)
                {
                    if (string.IsNullOrEmpty(connectionString))
                    {
                        var csResponse = httpClient
                            .GetAsync($"{config["KeyStore:Url"]}/api/keystore/keystore?key=DocumentManagementCS")
                            .Result;
                        csResponse.EnsureSuccessStatusCode();
                        connectionString = csResponse.Content.ReadAsStringAsync().Result;

                        var mongoConnectionUrl = new MongoUrl(connectionString);
                        var mongoClientSettings = MongoClientSettings.FromUrl(mongoConnectionUrl);
                        mongoClientSettings.ClusterConfigurator = cb =>
                        {
                            cb.Subscribe<CommandStartedEvent>(e =>
                            {
                                logger.LogInformation($"{e.CommandName} - {e.Command.ToJson()}");
                            });
                        };
                        mongoClientSettings.MaxConnectionIdleTime = TimeSpan.FromMinutes(3);
                        client = new MongoClient(mongoClientSettings);
                    }
                }
            }

            db = client.GetDatabase(config["Mongo:Database"]);
        }
    }
}
