using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Notification.API;
using Notification.Entity.Models;
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
                    try
                    {
                        INotificationService notificationService =
                            scope.ServiceProvider.GetRequiredService<INotificationService>();
                        IConfiguration configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
                        IConnectionMultiplexer connection = await ConnectionMultiplexer.ConnectAsync(ConfigurationOptions.Parse(configuration["Redis:ConnectionString"]));
                        IDatabaseAsync database = connection.GetDatabase();
                        long count = await database.ListLengthAsync(NotificationKey);
                        List<NotificationModel> list = new List<NotificationModel>();
                        for (int i = 0; i < count; i++)
                        {
                            RedisValue value = await database.ListGetByIndexAsync(NotificationKey, i);
                            if (!value.IsNullOrEmpty)
                            {
                                NotificationModel m =
                                    JsonConvert.DeserializeObject<NotificationModel>(value.ToString());
                                Setting setting = await notificationService.GetSetting(m.tenantId.Value);
                                if ((DateTime.UtcNow - m.DateTime.Value).TotalMinutes > setting.QueueTimeInMinute)
                                    list.Add(m);
                            }
                        }
                        foreach (var item in list)
                        {
                            TenantSetting tenantSetting = await notificationService.GetTenantSetting(item.tenantId.Value,item.NotificationType);
                            long id = await notificationService.Add(item,item.userId.Value,item.tenantId.Value,tenantSetting);
                            await SendNotification(id);
                            await database.ListRemoveAsync(NotificationKey, JsonConvert.SerializeObject(item));
                        }
                    }
                    catch {
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
                List<NotificationModel> list = new List<NotificationModel>();
                for (int i = 0; i < count; i++)
                {
                    RedisValue value = await database.ListGetByIndexAsync(NotificationKey,i);
                    if (!value.IsNullOrEmpty)
                    {
                        NotificationModel m = JsonConvert.DeserializeObject<NotificationModel>(value.ToString());
                        if (m.EntityId == model.EntityId)
                            list.Add(m);
                    }
                }

                foreach (var item in list)
                {
                    await database.ListRemoveAsync(NotificationKey, JsonConvert.SerializeObject(item));
                }
                await database.ListRightPushAsync(NotificationKey, JsonConvert.SerializeObject(model));
            }
        }

        public async Task SendNotification(long id)
        {
            using (IServiceScope scope = serviceProvider.CreateScope())
            {
                INotificationService notificationService =
                    scope.ServiceProvider.GetRequiredService<INotificationService>();
                IHubContext<ServerHub, IClientHub> context = scope.ServiceProvider.GetRequiredService<IHubContext<ServerHub, IClientHub>>();
                NotificationObject notificationObject = await notificationService.GetByIdForTemplate(id);
                foreach (var recep in notificationObject.NotificationRecepients)
                {
                    foreach (var medium in recep.NotificationRecepientMediums)
                    {
                        if (medium.NotificationMediumid == (int) Notification.Common.NotificationMediumEnum.InApp)
                        {
                            NotificationMediumModel model = new NotificationMediumModel()
                            {
                                id = medium.Id,
                                payload = string.IsNullOrEmpty(medium.SentTextJson)
                                    ? new JObject()
                                    : JObject.Parse(medium.SentTextJson),
                                status = recep.StatusListEnum.Name
                            };
                            await ServerHub.SendNotification(context, recep.RecipientId.Value, model);
                        }
                    }
                }
            }
        }
    }
}
