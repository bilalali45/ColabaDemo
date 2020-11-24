using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using Notification.Common;
using Notification.Entity.Models;
using Notification.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<string> PopulateTemplate(long notificationId, int notificationTypeId, int notificationMediumId)
        {
            INotificationService notificationService = serviceProvider.GetRequiredService<INotificationService>();
            NotificationObject notificationObject = await notificationService.GetByIdForTemplate(notificationId);
            switch ((Common.NotificationType)notificationTypeId)
            {
                case Common.NotificationType.DocumentSubmission:
                    return await DocumentSubmissionTemplate(notificationObject,notificationMediumId);
            }

            return "";
        }

        private async Task<string> DocumentSubmissionTemplate(NotificationObject notificationObject, int notificationMediumId)
        {
            foreach (var template in notificationObject.NotificationType.NotificationTemplates)
            {
                switch ((Common.NotificationMedium)notificationMediumId)
                {
                    case Common.NotificationMedium.InApp:
                        return await DocumentSubmissionTemplate_InApp(notificationObject, template);
                }
            }

            return "";
        }

        private async Task<string> DocumentSubmissionTemplate_InApp(NotificationObject notificationObject,
            NotificationTemplate notificationTemplate)
        {
            IRainmakerService rainmakerService = serviceProvider.GetRequiredService<IRainmakerService>();
            LoanSummary summary = await rainmakerService.GetLoanSummary(notificationObject.EntityId.Value);
            // populate template  
            string templateJson = notificationTemplate.TemplateJson.Replace("{{NotificationType}}",notificationObject.NotificationType.Name);
            templateJson = templateJson.Replace("{{NotificationDateTime}}",notificationObject.CreatedOn.Value.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));
            templateJson = templateJson.Replace("{{PrimaryName}}", summary.Name ?? "");
            templateJson = templateJson.Replace("{{Address}}", summary.StreetAddress ?? "");
            templateJson = templateJson.Replace("{{UnitNumber}}", summary.UnitNumber ?? "");
            templateJson = templateJson.Replace("{{City}}", summary.CityName ?? "");
            templateJson = templateJson.Replace("{{State}}",summary.StateName ?? "");
            templateJson = templateJson.Replace("{{ZipCode}}",summary.ZipCode ?? "");
            templateJson = templateJson.Replace("{{LoanApplicationId}}", notificationObject.EntityId.Value.ToString());
            string metaJson = notificationTemplate.MediumMetaDataJson.Replace("{{Link}}", $"/admin/loanapplication/documentmanagement?loanApplicationId={notificationObject.EntityId}");
            JObject jObject = new JObject();
            jObject.Add("data",JObject.Parse(templateJson));
            jObject.Add("meta", JObject.Parse(metaJson));
            return jObject.ToString();
        }
    }
}
