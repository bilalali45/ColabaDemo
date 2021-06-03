using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Notification.Model;
using SettingModel = Setting.Model.SettingModel;

namespace Setting.Service
{
    public interface INotificationService
    {
        Task<List<SettingModel>> GetSettings(IEnumerable<string> authHeader);


        Task<bool> UpdateSettings(int notificationTypeId,
                                  short deliveryModeId,
                                  short? delayedInterval,
                                  IEnumerable<string> authHeader);

        Task<bool> SendNotification(NotificationModel model);
    }
}
