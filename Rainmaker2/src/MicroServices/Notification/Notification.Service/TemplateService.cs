using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Notification.Common;
using Notification.Data;
using Notification.Entity.Models;
using TrackableEntities.Common.Core;

namespace Notification.Service
{
    public class TemplateService : ITemplateService
    {
        private readonly INotificationService notificationService;

        public TemplateService(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }
        public async Task PopulateTemplate(long notificationId)
        {
            NotificationObject notificationObject = await notificationService.GetByIdForTemplate(notificationId);
            switch ((NotificationTypeEnum)notificationObject.NotificationTypeId)
            {
                case NotificationTypeEnum.DocumentSubmission:
                    await DocumentSubmissionTemplate(notificationObject);
                    break;
            }
        }

        private async Task DocumentSubmissionTemplate(NotificationObject notificationObject)
        {
            foreach (var template in notificationObject.NotificationType.NotificationTemplates)
            {
                switch ((NotificationMediumEnum)template.NotificationMediumId)
                {
                    case NotificationMediumEnum.InApp:
                        await DocumentSubmissionTemplate_InApp(notificationObject, template);
                        break;
                }
            }
        }

        private async Task DocumentSubmissionTemplate_InApp(NotificationObject notificationObject,
            NotificationTemplate notificationTemplate)
        {
            // populate template
            string templateJson = notificationTemplate.TemplateJson.Replace("{{NotificationType}}",notificationObject.NotificationType.Name);
            templateJson = templateJson.Replace("{{NotificationDateTime}}",notificationObject.CreatedOn.Value.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));
            string metaJson = notificationTemplate.MediumMetaDataJson;

            List<NotificationRecepientMedium> list = notificationObject.NotificationRecepients.SelectMany(x => x.NotificationRecepientMediums.Where(y=>y.NotificationMediumid==notificationTemplate.NotificationMediumId)).ToList();
            foreach (var item in list)
            {
                JObject jObject = new JObject();
                jObject.Add("text",JObject.Parse(templateJson));
                jObject.Add("metaData", JObject.Parse(metaJson));
                item.SentTextJson = jObject.ToString();
                item.TrackingState = TrackingState.Modified;
                notificationService.Update(notificationObject);
                await notificationService.SaveChangesAsync();
            }
        }
    }
}
