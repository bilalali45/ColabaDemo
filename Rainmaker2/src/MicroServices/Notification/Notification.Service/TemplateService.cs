using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using Notification.Common;
using Notification.Data;
using Notification.Entity.Models;
using Notification.Model;
using TrackableEntities.Common.Core;

namespace Notification.Service
{
    public class TemplateService : ITemplateService
    {
        private readonly IServiceProvider serviceProvider;

        public TemplateService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public async Task PopulateTemplate(long notificationId, IEnumerable<string> authHeader)
        {
            INotificationService notificationService = serviceProvider.GetRequiredService<INotificationService>();
            NotificationObject notificationObject = await notificationService.GetByIdForTemplate(notificationId);
            switch ((NotificationTypeEnum)notificationObject.NotificationTypeId)
            {
                case NotificationTypeEnum.DocumentSubmission:
                    await DocumentSubmissionTemplate(notificationObject,authHeader);
                    break;
            }
        }

        private async Task DocumentSubmissionTemplate(NotificationObject notificationObject, IEnumerable<string> authHeader)
        {
            foreach (var template in notificationObject.NotificationType.NotificationTemplates)
            {
                switch ((NotificationMediumEnum)template.NotificationMediumId)
                {
                    case NotificationMediumEnum.InApp:
                        await DocumentSubmissionTemplate_InApp(notificationObject, template,authHeader);
                        break;
                }
            }
        }

        private async Task DocumentSubmissionTemplate_InApp(NotificationObject notificationObject,
            NotificationTemplate notificationTemplate, IEnumerable<string> authHeader)
        {
            INotificationService notificationService = serviceProvider.GetRequiredService<INotificationService>();
            IRainmakerService rainmakerService = serviceProvider.GetRequiredService<IRainmakerService>();
            LoanSummary summary = await rainmakerService.GetLoanSummary(notificationObject.EntityId.Value, authHeader);
            // populate template
            string templateJson = notificationTemplate.TemplateJson.Replace("{{NotificationType}}",notificationObject.NotificationType.Name);
            templateJson = templateJson.Replace("{{NotificationDateTime}}",notificationObject.CreatedOn.Value.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));
            templateJson = templateJson.Replace("{{PrimaryName}}", summary.Name);
            templateJson = templateJson.Replace("{{Address}}", summary.StreetAddress);
            templateJson = templateJson.Replace("{{UnitNumber}}", summary.UnitNumber);
            templateJson = templateJson.Replace("{{City}}", summary.CityName);
            templateJson = templateJson.Replace("{{State}}",summary.StateName);
            templateJson = templateJson.Replace("{{ZipCode}}",summary.ZipCode);
            string metaJson = notificationTemplate.MediumMetaDataJson.Replace("{{Link}}", $"/admin/loanapplication/documentmanagement?loanApplicationId={notificationObject.EntityId}");

            List<NotificationRecepientMedium> list = notificationObject.NotificationRecepients.SelectMany(x => x.NotificationRecepientMediums.Where(y=>y.NotificationMediumid==notificationTemplate.NotificationMediumId)).ToList();
            foreach (var item in list)
            {
                JObject jObject = new JObject();
                jObject.Add("data",JObject.Parse(templateJson));
                jObject.Add("meta", JObject.Parse(metaJson));
                item.SentTextJson = jObject.ToString();
                item.TrackingState = TrackingState.Modified;
                notificationService.Update(notificationObject);
                await notificationService.SaveChangesAsync();
            }
        }
    }
}
