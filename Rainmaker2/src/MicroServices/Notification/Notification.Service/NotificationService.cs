using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Notification.Data;
using Notification.Entity.Models;
using Notification.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Notification.Common;
using TrackableEntities.Common.Core;
using URF.Core.Abstractions;

namespace Notification.Service
{
    public class NotificationService : ServiceBase<NotificationContext, NotificationObject>, INotificationService
    {
        private readonly IRainmakerService rainmakerService;
        private readonly ITemplateService templateService;
        public NotificationService(IUnitOfWork<NotificationContext> previousUow,
            IServiceProvider services,
            IRainmakerService rainmakerService,
            ITemplateService templateService) : base(previousUow: previousUow,
            services: services)
        {
            this.rainmakerService = rainmakerService;
            this.templateService = templateService;
        }

        public async Task<int> GetCount(int userProfileId)
        {
            return await Uow.Repository<NotificationRecepient>().Query(x =>
                    x.RecipientId == userProfileId && x.StatusId == (byte) Notification.Common.StatusList.Unseen)
                .CountAsync();
        }

        public async Task<TenantSetting> GetTenantSetting(int tenantId, int notificationType)
        {
            return await Uow.Repository<TenantSetting>().Query(x => x.TenantId == tenantId && x.NotificationTypeId == notificationType).FirstOrDefaultAsync();
        }

        public async Task<TenantSettingModel> GetTenantSetting(int tenantId)
        {
            var tenantSetting = await Uow.Repository<TenantSetting>().Query(x => x.TenantId == tenantId).FirstAsync();
            return new TenantSettingModel() { deliveryModeId = tenantSetting.DeliveryModeId, queueTimeout = tenantSetting.DelayedInterval };
        }

        public async Task<TenantSetting> GetSetting(int tenantId)
        {
            return await Uow.Repository<TenantSetting>().Query(x => x.TenantId == tenantId).FirstOrDefaultAsync();
        }
        public async Task<long> Add(NotificationModel model)
        {
            NotificationObject notificationObject = new NotificationObject();
            notificationObject.CreatedOn = model.DateTime;
            notificationObject.CustomTextJson = model.CustomTextJson;
            notificationObject.EntityId = model.EntityId;
            notificationObject.NotificationTypeId = model.NotificationType;
            notificationObject.TenantId = model.tenantId.Value;
            notificationObject.StatusId = (byte)Notification.Common.StatusList.Created;
            notificationObject.TrackingState = TrackingState.Added;

            notificationObject.NotificationActor = new NotificationActor();
            notificationObject.NotificationActor.TrackingState = TrackingState.Added;
            notificationObject.NotificationActor.ActorId = model.userId.Value;
            
            model.UsersToSendList = await rainmakerService.GetAssignedUsers(model.EntityId);

            Uow.Repository<NotificationObject>().Insert(notificationObject);
            await Uow.SaveChangesAsync();

            return notificationObject.Id;
        }


        public async Task<NotificationRecepient> GetRecepient(long recepientId)
        {
            return await Uow.Repository<NotificationRecepient>().Query(x => x.Id == recepientId).Include(x => x.StatusListEnum).FirstAsync();
        }
        public async Task<long> AddUserNotificationMedium(int userId, long notificationObjectId, short deliveryModeId, int notificationMediumId, int notificationTypeId)
        {
            NotificationRecepient notificationRecepient = new NotificationRecepient();
            notificationRecepient.RecipientId = userId;
            notificationRecepient.StatusId = (byte)Notification.Common.StatusList.Unseen;
            notificationRecepient.NotificationObjectId = notificationObjectId;
            notificationRecepient.TrackingState = TrackingState.Added;
            notificationRecepient.NotificationRecepientMediums = new List<NotificationRecepientMedium>();

            NotificationRecepientMedium notificationRecepientMedium = new NotificationRecepientMedium();
            notificationRecepientMedium.DeliveryModeId = deliveryModeId;
            notificationRecepientMedium.NotificationMediumid = notificationMediumId;
            notificationRecepientMedium.StatusId = (byte)Notification.Common.StatusList.Created;
            notificationRecepientMedium.SentTextJson = await templateService.PopulateTemplate(notificationObjectId,notificationTypeId,notificationMediumId);
            notificationRecepientMedium.TrackingState = TrackingState.Added;

            notificationRecepient.NotificationRecepientMediums.Add(notificationRecepientMedium);
            Uow.Repository<NotificationRecepient>().Insert(notificationRecepient);
            await Uow.SaveChangesAsync();
            return notificationRecepient.Id;
        }

        public async Task<NotificationObject> GetByIdForTemplate(long notificationId)
        {
            return await Repository.Query(x => x.Id == notificationId).Include(x => x.NotificationType).ThenInclude(x => x.NotificationTemplates)
                .FirstAsync();
        }

        public async Task<List<NotificationMediumModel>> GetPaged(int pageSize, long lastId, int mediumId, int userId)
        {
            return await Uow.Repository<NotificationRecepientMedium>().Query(x => x.Id < lastId
            && x.NotificationRecepient.RecipientId == userId
            && x.NotificationMediumid == mediumId
            && x.NotificationRecepient.StatusId != (byte)Notification.Common.StatusList.Deleted)
                .Include(x => x.NotificationRecepient).ThenInclude(x => x.StatusListEnum).OrderByDescending(x => x.Id).Take(pageSize)
                .Select(x => new NotificationMediumModel()
                {
                    id = x.Id,
                    payload = !String.IsNullOrEmpty(x.SentTextJson) ? JObject.Parse(x.SentTextJson) : new JObject(),
                    status = x.NotificationRecepient.StatusListEnum.Name
                }).ToListAsync();
        }

        public async Task<List<long>> Read(List<long> ids, int userId)
        {
            List<long> readList = new List<long>(ids);
            foreach (var id in ids)
            {
                var result = await Uow.Repository<NotificationRecepientMedium>().Query(x => x.Id == id)
                    .Include(x => x.NotificationRecepient).ThenInclude(x=>x.NotificationObject).FirstOrDefaultAsync();

                result.NotificationRecepient.StatusId = (byte) Notification.Common.StatusList.Read;

                result.NotificationRecepient.TrackingState = TrackingState.Modified;

                Uow.Repository<NotificationRecepientMedium>().Update(result);
                await Uow.SaveChangesAsync();
                int loanApplicationId = result.NotificationRecepient.NotificationObject.EntityId.Value;
                var records = await Uow.Repository<NotificationRecepientMedium>().Query(x => x.NotificationRecepient.RecipientId==userId && x.NotificationRecepient.NotificationObject.EntityId==loanApplicationId &&
                                                                                             (x.NotificationRecepient.StatusId==(byte)Notification.Common.StatusList.Unread || x.NotificationRecepient.StatusId == (byte)Notification.Common.StatusList.Unseen))
                    .Include(x => x.NotificationRecepient).ThenInclude(x => x.NotificationObject).ToListAsync();
                foreach (var record in records)
                {
                    readList.Add(record.Id);
                    record.NotificationRecepient.StatusId = (byte)Notification.Common.StatusList.Read;

                    record.NotificationRecepient.TrackingState = TrackingState.Modified;

                    Uow.Repository<NotificationRecepientMedium>().Update(record);
                }
            }
            await Uow.SaveChangesAsync();
            return readList.Distinct().ToList();
        }
        public async Task Seen(List<long> ids)
        {
            foreach (var id in ids)
            {
                var result = await Uow.Repository<NotificationRecepientMedium>().Query(x => x.Id == id)
                    .Include(x => x.NotificationRecepient).ThenInclude(x=>x.NotificationRecepientStatusLogs).FirstOrDefaultAsync();

                result.NotificationRecepient.StatusId = result.NotificationRecepient.NotificationRecepientStatusLogs
                    .Any(x => x.StatusId != (byte)Notification.Common.StatusList.Deleted 
                                && x.StatusId != (byte)Notification.Common.StatusList.Unseen) ? 
                    result.NotificationRecepient.NotificationRecepientStatusLogs
                        .Where(x => x.StatusId != (byte)Notification.Common.StatusList.Deleted
                                    && x.StatusId != (byte)Notification.Common.StatusList.Unseen).OrderByDescending(x => x.UpdatedOn)
                        .First().StatusId : (byte)Notification.Common.StatusList.Unread;

                result.NotificationRecepient.TrackingState = TrackingState.Modified;

                Uow.Repository<NotificationRecepientMedium>().Update(result);
            }
            await Uow.SaveChangesAsync();
        }
        public async Task Delete(long id)
        {
            var result = await Uow.Repository<NotificationRecepientMedium>().Query(x => x.Id == id).Include(x => x.NotificationRecepient).FirstOrDefaultAsync();

            result.NotificationRecepient.StatusId = (byte)Notification.Common.StatusList.Deleted;

            result.NotificationRecepient.TrackingState = TrackingState.Modified;

            Uow.Repository<NotificationRecepientMedium>().Update(result);
            await Uow.SaveChangesAsync();
        }
        public async Task DeleteAll()
        {
            var results = await Uow.Repository<NotificationRecepientMedium>().Query(x=>x.NotificationRecepient.StatusId != (byte)Notification.Common.StatusList.Deleted).Include(x => x.NotificationRecepient).ToListAsync();

            foreach(var result in results)
            {
                result.NotificationRecepient.StatusId = (byte)Notification.Common.StatusList.Deleted;

                result.NotificationRecepient.TrackingState = TrackingState.Modified;

                Uow.Repository<NotificationRecepientMedium>().Update(result);
            }
            await Uow.SaveChangesAsync();
        }

        public async Task<NotificationMediumModel> Undelete(long id)
        {
            var result = await Uow.Repository<NotificationRecepientMedium>().Query(x => x.Id == id)
                .Include(x => x.NotificationRecepient)
                .ThenInclude(x=>x.NotificationRecepientStatusLogs)
                .FirstOrDefaultAsync();
            result.NotificationRecepient.StatusId = result.NotificationRecepient.NotificationRecepientStatusLogs
                .Any(x=>x.StatusId!= (byte)Notification.Common.StatusList.Deleted
                          && x.StatusId != (byte)Notification.Common.StatusList.Unseen) ? 
                result.NotificationRecepient.NotificationRecepientStatusLogs
                    .Where(x => x.StatusId != (byte)Notification.Common.StatusList.Deleted
                                && x.StatusId != (byte)Notification.Common.StatusList.Unseen).OrderByDescending(x=>x.UpdatedOn)
                    .First().StatusId : (byte)Notification.Common.StatusList.Unread;

            result.NotificationRecepient.TrackingState = TrackingState.Modified;

            Uow.Repository<NotificationRecepientMedium>().Update(result);
            await Uow.SaveChangesAsync();
            result = await Uow.Repository<NotificationRecepientMedium>().Query(x => x.Id == id)
                .Include(x => x.NotificationRecepient)
                .ThenInclude(x => x.StatusListEnum)
                .FirstOrDefaultAsync();

            return new NotificationMediumModel() { id = result.Id, payload = !String.IsNullOrEmpty(result.SentTextJson) ? JObject.Parse(result.SentTextJson) : new JObject(), status = result.NotificationRecepient.StatusListEnum.Name };
        }

        public async Task SetTenantSetting(int tenantId, TenantSettingModel model)
        {
            var tenantSetting = await Uow.Repository<TenantSetting>().Query(x => x.TenantId == tenantId).FirstAsync();
            tenantSetting.TrackingState = TrackingState.Modified;
            tenantSetting.DeliveryModeId = model.deliveryModeId;
            tenantSetting.DelayedInterval = model.queueTimeout;
            Uow.Repository<TenantSetting>().Update(tenantSetting);
            await Uow.SaveChangesAsync();
        }
    }
}
