using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Notification.Data;
using Notification.Entity.Models;
using Notification.Model;
using TrackableEntities.Common.Core;
using URF.Core.Abstractions;

namespace Notification.Service
{
    public class SettingService : ServiceBase<NotificationContext, TenantSetting>, ISettingService
    {
        public SettingService(IUnitOfWork<NotificationContext> previousUow,
                              IServiceProvider services) : base(previousUow: previousUow,
                                                                services: services)
        {
        }


        public async Task<List<SettingModel>> GetSettings(int tenantId,
                                                          int userId)
        {
            var result = await Uow.Repository<TenantSetting>().Query(query: x => x.UserId == userId && x.TenantId == tenantId)
                                  .Include(x => x.NotificationType)
                                  .Select(selector: x => new SettingModel
                                  {
                                      Id = x.Id,
                                      TenantId = x.TenantId,
                                      UserId = x.UserId,
                                      DeliveryModeId = x.DeliveryModeId,
                                      NotificationMediumId = x.NotificationMediumId,
                                      NotificationTypeId = x.NotificationTypeId,
                                      DelayedInterval = x.DelayedInterval,
                                      NotificationType = x.NotificationType.Name
                                  }).ToListAsync();

            if (result.Count > 0)
            {
                return result;
            }
            else
            {
                return await Uow.Repository<TenantSetting>().Query(query: x => x.TenantId == tenantId)
                                .Include(x => x.NotificationType)
                                .Select(selector: x => new SettingModel
                                {
                                    Id = x.Id,
                                    TenantId = x.TenantId,
                                    UserId = x.UserId,
                                    DeliveryModeId = x.DeliveryModeId,
                                    NotificationMediumId = x.NotificationMediumId,
                                    NotificationTypeId = x.NotificationTypeId,
                                    DelayedInterval = x.DelayedInterval,
                                    NotificationType = x.NotificationType.Name
                                }).ToListAsync();

            }
        }

        public async Task UpdateSettings(int tenantId, int userId, int notificationTypeId, short deliveryModeId, short? delayedInterval)
        {
            TenantSetting tenantSetting = await Uow.Repository<TenantSetting>().Query(x => x.TenantId == tenantId && x.UserId == userId && x.NotificationTypeId == notificationTypeId).FirstOrDefaultAsync();

            if (tenantSetting != null)
            {
                tenantSetting.DeliveryModeId = deliveryModeId;
                tenantSetting.DelayedInterval = delayedInterval;

                tenantSetting.TrackingState = TrackingState.Modified;

                Uow.Repository<TenantSetting>().Update(tenantSetting);
            }
            else
            {
                tenantSetting = new TenantSetting();
                tenantSetting.TrackingState = TrackingState.Added;

                tenantSetting.TenantId = tenantId;
                tenantSetting.UserId = userId;
                tenantSetting.NotificationTypeId = notificationTypeId;
                tenantSetting.DeliveryModeId = deliveryModeId;
                tenantSetting.DelayedInterval = delayedInterval;
                tenantSetting.NotificationMediumId = 1;

                Uow.Repository<TenantSetting>().Insert(tenantSetting);
            }

            await Uow.SaveChangesAsync();
        }
    }
}