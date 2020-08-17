using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Notification.Data;
using Notification.Entity.Models;
using Notification.Model;
using TrackableEntities.Common.Core;
using URF.Core.Abstractions;

namespace Notification.Service
{
    public class NotificationService : ServiceBase<NotificationContext,NotificationObject>,INotificationService
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
        public async Task<long> Add(NotificationModel model, int userId, int tenantId, IEnumerable<string> authHeader)
        {
            List<TenantSetting> tenantSetting = await Uow.Repository<TenantSetting>().Query(x=>x.TenantId == tenantId && x.NotificationTypeId == model.NotificationType).ToListAsync();
            NotificationObject notificationObject = new NotificationObject();
            notificationObject.CreatedOn = DateTime.UtcNow;
            notificationObject.CustomTextJson = model.CustomTextJson;
            notificationObject.EntityId = model.EntityId;
            notificationObject.NotificationTypeId = model.NotificationType;
            notificationObject.TenantId = tenantId;
            notificationObject.StatusId = (byte)Notification.Common.StatusListEnum.Created;
            notificationObject.TrackingState = TrackingState.Added;

            notificationObject.NotificationActor = new NotificationActor();
            notificationObject.NotificationActor.TrackingState = TrackingState.Added;
            notificationObject.NotificationActor.ActorId = userId;

            List<int> reciepient = await rainmakerService.GetAssignedUsers(model.EntityId, authHeader);
            if(reciepient != null)
            {
                foreach(var item in reciepient)
                {
                    NotificationRecepient notificationRecepient = new NotificationRecepient();
                    notificationRecepient.RecipientId = item;
                    notificationRecepient.StatusId = (byte)Notification.Common.StatusListEnum.Unread;
                    notificationRecepient.TrackingState = TrackingState.Added;
                    notificationObject.NotificationRecepients.Add(notificationRecepient);
                    notificationRecepient.NotificationRecepientMediums = new List<NotificationRecepientMedium>();
                    foreach(var setting in tenantSetting)
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
            return await Repository.Query(x => x.Id == notificationId).Include(x => x.NotificationType).ThenInclude(x=>x.NotificationTemplates)
                .Include(x=>x.NotificationRecepients).ThenInclude(x=>x.NotificationRecepientMediums).FirstAsync();
        }
    }
}
