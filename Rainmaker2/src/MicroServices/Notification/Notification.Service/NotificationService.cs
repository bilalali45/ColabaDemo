using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Notification.Data;
using Notification.Entity.Models;
using Notification.Model;
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
                    x.RecipientId == userProfileId && x.StatusId == (byte) Notification.Common.StatusListEnum.Unseen)
                .CountAsync();
        }

        public async Task<TenantSetting> GetTenantSetting(int tenantId, int notificationType)
        {
            return await Uow.Repository<TenantSetting>().Query(x => x.TenantId == tenantId && x.NotificationTypeId == notificationType).FirstOrDefaultAsync();
        }

        public async Task<Setting> GetSetting(int tenantId)
        {
            return await Uow.Repository<Setting>().Query(x => x.TenantId == tenantId).FirstOrDefaultAsync();
        }
        public async Task<long> Add(NotificationModel model, int userId, int tenantId, TenantSetting setting)
        {
            NotificationObject notificationObject = new NotificationObject();
            notificationObject.CreatedOn = model.DateTime;
            notificationObject.CustomTextJson = model.CustomTextJson;
            notificationObject.EntityId = model.EntityId;
            notificationObject.NotificationTypeId = model.NotificationType;
            notificationObject.TenantId = tenantId;
            notificationObject.StatusId = (byte)Notification.Common.StatusListEnum.Created;
            notificationObject.TrackingState = TrackingState.Added;

            notificationObject.NotificationActor = new NotificationActor();
            notificationObject.NotificationActor.TrackingState = TrackingState.Added;
            notificationObject.NotificationActor.ActorId = userId;

            List<int> reciepient = await rainmakerService.GetAssignedUsers(model.EntityId);
            if (reciepient != null)
            {
                foreach (var item in reciepient)
                {
                    NotificationRecepient notificationRecepient = new NotificationRecepient();
                    notificationRecepient.RecipientId = item;
                    notificationRecepient.StatusId = (byte)Notification.Common.StatusListEnum.Unseen;
                    notificationRecepient.TrackingState = TrackingState.Added;
                    notificationObject.NotificationRecepients.Add(notificationRecepient);
                    notificationRecepient.NotificationRecepientMediums = new List<NotificationRecepientMedium>();
                    if (await IsUserSubscribedToMedium(userId, tenantId, setting.NotificationMediumId))
                    {
                        NotificationRecepientMedium notificationRecepientMedium = new NotificationRecepientMedium();
                        notificationRecepientMedium.DeliveryModeId = setting.DeliveryModeId;
                        notificationRecepientMedium.NotificationMediumid = setting.NotificationMediumId;
                        notificationRecepientMedium.StatusId = (byte)Notification.Common.StatusListEnum.Created;
                        notificationRecepientMedium.SentTextJson = String.Empty;

                        notificationRecepient.NotificationRecepientMediums.Add(notificationRecepientMedium);
                    }
                }
            }

            Uow.Repository<NotificationObject>().Insert(notificationObject);
            await Uow.SaveChangesAsync();

            await templateService.PopulateTemplate(notificationObject.Id);

            return notificationObject.Id;
        }

        public async Task<NotificationObject> GetByIdForTemplate(long notificationId)
        {
            return await Repository.Query(x => x.Id == notificationId).Include(x => x.NotificationType).ThenInclude(x => x.NotificationTemplates)
                .Include(x=>x.NotificationRecepients).ThenInclude(x=>x.StatusListEnum)
                .Include(x => x.NotificationRecepients).ThenInclude(x => x.NotificationRecepientMediums).FirstAsync();
        }

        private async Task<bool> IsUserSubscribedToMedium(int userId, int tenantId, int mediumId)
        {
            var result = await Uow.Repository<UserNotificationMedium>().Query(x => x.UserId == userId && x.TenantId == tenantId && x.NotificationMediumId == mediumId).FirstOrDefaultAsync();
            if (result == null)
            {
                return true;
            }
            else
            {
                return result.IsActive.Value;
            }
        }

        public async Task<List<NotificationMediumModel>> GetPaged(int pageSize, long lastId, int mediumId, int userId)
        {
            return await Uow.Repository<NotificationRecepientMedium>().Query(x => x.Id < lastId
            && x.NotificationRecepient.RecipientId == userId
            && x.NotificationMediumid == mediumId
            && x.NotificationRecepient.StatusId != (byte)Notification.Common.StatusListEnum.Deleted)
                .Include(x => x.NotificationRecepient).ThenInclude(x => x.StatusListEnum).OrderByDescending(x => x.Id).Take(pageSize)
                .Select(x => new NotificationMediumModel()
                {
                    id = x.Id,
                    payload = !String.IsNullOrEmpty(x.SentTextJson) ? JObject.Parse(x.SentTextJson) : new JObject(),
                    status = x.NotificationRecepient.StatusListEnum.Name
                }).ToListAsync();
        }

        public async Task Read(List<long> ids, int userId)
        {
            foreach (var id in ids)
            {
                var result = await Uow.Repository<NotificationRecepientMedium>().Query(x => x.Id == id)
                    .Include(x => x.NotificationRecepient).ThenInclude(x=>x.NotificationObject).FirstOrDefaultAsync();

                result.NotificationRecepient.StatusId = (byte) Notification.Common.StatusListEnum.Read;

                result.NotificationRecepient.TrackingState = TrackingState.Modified;

                Uow.Repository<NotificationRecepientMedium>().Update(result);
                await Uow.SaveChangesAsync();
                int loanApplicationId = result.NotificationRecepient.NotificationObject.EntityId.Value;
                var records = await Uow.Repository<NotificationRecepientMedium>().Query(x => x.NotificationRecepient.RecipientId==userId && x.NotificationRecepient.NotificationObject.EntityId==loanApplicationId &&
                                                                                             (x.NotificationRecepient.StatusId==(byte)Notification.Common.StatusListEnum.Unread || x.NotificationRecepient.StatusId == (byte)Notification.Common.StatusListEnum.Unseen))
                    .Include(x => x.NotificationRecepient).ThenInclude(x => x.NotificationObject).ToListAsync();
                foreach (var record in records)
                {
                    record.NotificationRecepient.StatusId = (byte)Notification.Common.StatusListEnum.Read;

                    record.NotificationRecepient.TrackingState = TrackingState.Modified;

                    Uow.Repository<NotificationRecepientMedium>().Update(record);
                }
            }
            await Uow.SaveChangesAsync();
        }
        public async Task Seen(List<long> ids)
        {
            foreach (var id in ids)
            {
                var result = await Uow.Repository<NotificationRecepientMedium>().Query(x => x.Id == id)
                    .Include(x => x.NotificationRecepient).ThenInclude(x=>x.NotificationRecepientStatusLogs).FirstOrDefaultAsync();

                result.NotificationRecepient.StatusId = result.NotificationRecepient.NotificationRecepientStatusLogs
                    .Where(x => x.StatusId != (byte)Notification.Common.StatusListEnum.Deleted 
                                && x.StatusId != (byte)Notification.Common.StatusListEnum.Unseen).Count() >= 1 ? 
                    result.NotificationRecepient.NotificationRecepientStatusLogs
                        .Where(x => x.StatusId != (byte)Notification.Common.StatusListEnum.Deleted
                                    && x.StatusId != (byte)Notification.Common.StatusListEnum.Unseen).OrderByDescending(x => x.UpdatedOn)
                        .First().StatusId : (byte)Notification.Common.StatusListEnum.Unread;

                result.NotificationRecepient.TrackingState = TrackingState.Modified;

                Uow.Repository<NotificationRecepientMedium>().Update(result);
            }
            await Uow.SaveChangesAsync();
        }
        public async Task Delete(long id)
        {
            var result = await Uow.Repository<NotificationRecepientMedium>().Query(x => x.Id == id).Include(x => x.NotificationRecepient).FirstOrDefaultAsync();

            result.NotificationRecepient.StatusId = (byte)Notification.Common.StatusListEnum.Deleted;

            result.NotificationRecepient.TrackingState = TrackingState.Modified;

            Uow.Repository<NotificationRecepientMedium>().Update(result);
            await Uow.SaveChangesAsync();
        }
        public async Task DeleteAll()
        {
            var results = await Uow.Repository<NotificationRecepientMedium>().Query(x=>x.NotificationRecepient.StatusId != (byte)Notification.Common.StatusListEnum.Deleted).Include(x => x.NotificationRecepient).ToListAsync();

            foreach(var result in results)
            {
                result.NotificationRecepient.StatusId = (byte)Notification.Common.StatusListEnum.Deleted;

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
                .Where(x=>x.StatusId!= (byte)Notification.Common.StatusListEnum.Deleted
                          && x.StatusId != (byte)Notification.Common.StatusListEnum.Unseen).Count()>=1 ? 
                result.NotificationRecepient.NotificationRecepientStatusLogs
                    .Where(x => x.StatusId != (byte)Notification.Common.StatusListEnum.Deleted
                                && x.StatusId != (byte)Notification.Common.StatusListEnum.Unseen).OrderByDescending(x=>x.UpdatedOn)
                    .First().StatusId : (byte)Notification.Common.StatusListEnum.Unread;

            result.NotificationRecepient.TrackingState = TrackingState.Modified;

            Uow.Repository<NotificationRecepientMedium>().Update(result);
            await Uow.SaveChangesAsync();
            result = await Uow.Repository<NotificationRecepientMedium>().Query(x => x.Id == id)
                .Include(x => x.NotificationRecepient)
                .ThenInclude(x => x.StatusListEnum)
                .FirstOrDefaultAsync();

            return new NotificationMediumModel() { id = result.Id, payload = !String.IsNullOrEmpty(result.SentTextJson) ? JObject.Parse(result.SentTextJson) : new JObject(), status = result.NotificationRecepient.StatusListEnum.Name };
        }
    }
}
