using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Notification.API;
using Notification.Entity.Models;
using Notification.Model;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Notification.Common;

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
            while (true)
            {
                using (IServiceScope scope = serviceProvider.CreateScope())
                {
                    try
                    {
                        INotificationService notificationService =
                            scope.ServiceProvider.GetRequiredService<INotificationService>();
                        IConfiguration configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
                        using IConnectionMultiplexer connection = await ConnectionMultiplexer.ConnectAsync(ConfigurationOptions.Parse(configuration["Redis:ConnectionString"]));
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
                                NotificationModel c =
                                    JsonConvert.DeserializeObject<NotificationModel>(value.ToString());
                                if(await SendNotification(m))
                                    list.Add(c);
                            }
                        }
                        foreach (var item in list)
                        {
                            await database.ListRemoveAsync(NotificationKey, JsonConvert.SerializeObject(item));
                        }
                    }
                    catch
                    {
                        // this exception can be ignored
                    }
                }
                Thread.Sleep(60000);
            }
        }

        public async Task InsertInCache(NotificationModel model)
        {
            using (IServiceScope scope = serviceProvider.CreateScope())
            {
                IConfiguration configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
                using IConnectionMultiplexer connection = await ConnectionMultiplexer.ConnectAsync(ConfigurationOptions.Parse(configuration["Redis:ConnectionString"]));
                IDatabaseAsync database = connection.GetDatabase();
                long count = await database.ListLengthAsync(NotificationKey);
                List<NotificationModel> list = new List<NotificationModel>();
                for (int i = 0; i < count; i++)
                {
                    RedisValue value = await database.ListGetByIndexAsync(NotificationKey, i);
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

        public async Task<bool> SendNotification(NotificationModel model)
        {
            using (IServiceScope scope = serviceProvider.CreateScope())
            {
                INotificationService notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();
                ISettingService settingService = scope.ServiceProvider.GetRequiredService<ISettingService>();
                IHubContext<ServerHub, IClientHub> context = scope.ServiceProvider.GetRequiredService<IHubContext<ServerHub, IClientHub>>();
                List<int> doneUsers = new List<int>();
                foreach (var user in model.UsersToSendList)
                {
                    SettingModel setting = (await settingService.GetSettings(model.tenantId.Value,
                                                                              user)).FirstOrDefault(x=> x.NotificationTypeId == model.NotificationType);
                    if(setting==null)
                        doneUsers.Add(user);
                    else if(setting.DeliveryModeId==(short)DeliveryMode.Off)
                        doneUsers.Add(user);
                    else if (setting.DeliveryModeId == (short) DeliveryMode.Express)
                    {
                        long recepientId = await notificationService.AddUserNotificationMedium(user,
                                                                                                              model.Id,
                                                                                                              setting.DeliveryModeId,
                                                                                                              setting.NotificationMediumId,
                                                                                                              setting.NotificationTypeId);
                        NotificationRecepient recepient = await notificationService.GetRecepient(recepientId);
                        NotificationMediumModel m = new NotificationMediumModel()
                        {
                            id = recepient.NotificationRecepientMediums.First().Id,
                            payload = JObject.Parse(recepient.NotificationRecepientMediums.First().SentTextJson),
                            status = recepient.StatusListEnum.Name
                        };
                        await ServerHub.SendNotification(context, recepient.RecipientId.Value, m);
                        doneUsers.Add(user);
                    }
                    else if (setting.DeliveryModeId == (short) DeliveryMode.Queued && (DateTime.UtcNow - model.DateTime.Value).TotalMinutes >= setting.DelayedInterval.Value)
                    {
                        long recepientId = await notificationService.AddUserNotificationMedium(user,
                                                                                               model.Id,
                                                                                               setting.DeliveryModeId,
                                                                                               setting.NotificationMediumId,
                                                                                               setting.NotificationTypeId);
                        NotificationRecepient recepient = await notificationService.GetRecepient(recepientId);
                        NotificationMediumModel m = new NotificationMediumModel()
                                                    {
                                                        id = recepient.NotificationRecepientMediums.First().Id,
                                                        payload = JObject.Parse(recepient.NotificationRecepientMediums.First().SentTextJson),
                                                        status = recepient.StatusListEnum.Name
                                                    };
                        await ServerHub.SendNotification(context, recepient.RecipientId.Value, m);
                        doneUsers.Add(user);
                    }
                }

                foreach (var item in doneUsers)
                {
                    model.UsersToSendList.Remove(item);
                }

                return model.UsersToSendList.Count <= 0;
            }
        }
    }
}
