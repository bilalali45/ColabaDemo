using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Notification.Model;
using StackExchange.Redis;

namespace Notification.Service
{
    public class RedisService : IRedisService
    {
        private readonly IServiceProvider serviceProvider;
        private static readonly string NotificationKey = "NotificationQueue";
        public RedisService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public async Task Run()
        {
            using (IServiceScope scope = serviceProvider.CreateScope())
            {
                while (true)
                {
                    INotificationService notificationService =
                        scope.ServiceProvider.GetRequiredService<INotificationService>();
                    IConfiguration configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
                    IConnectionMultiplexer connection = await ConnectionMultiplexer.ConnectAsync(ConfigurationOptions.Parse(configuration["Redis:ConnectionString"]));
                    IDatabaseAsync database = connection.GetDatabase();
                    long count = await database.ListLengthAsync(NotificationKey);

                    for (int i = 0; i < count; i++)
                    {
                        RedisValue value = await database.ListGetByIndexAsync(NotificationKey, i);
                        NotificationModel m = JsonConvert.DeserializeObject<NotificationModel>(value.ToString());
                    }
                    Thread.Sleep(60000);
                }
            }
        }

        public async Task InsertInCache(NotificationModel model)
        {
            using (IServiceScope scope = serviceProvider.CreateScope())
            {
                IConfiguration configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
                IConnectionMultiplexer connection = await ConnectionMultiplexer.ConnectAsync(ConfigurationOptions.Parse(configuration["Redis:ConnectionString"]));
                IDatabaseAsync database = connection.GetDatabase();
                long count = await database.ListLengthAsync(NotificationKey);
                
                for (int i = 0; i < count; i++)
                {
                    RedisValue value = await database.ListGetByIndexAsync(NotificationKey,i);
                    NotificationModel m = JsonConvert.DeserializeObject<NotificationModel>(value.ToString());
                    if (m.EntityId == model.EntityId)
                        return;
                }
                await database.ListRightPushAsync(NotificationKey, JsonConvert.SerializeObject(model));
            }
        }
    }
}
