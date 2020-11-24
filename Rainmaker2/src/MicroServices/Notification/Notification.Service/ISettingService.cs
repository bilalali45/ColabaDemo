using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Notification.Entity.Models;
using Notification.Model;

namespace Notification.Service
{
   public interface ISettingService : IServiceBase<TenantSetting>
   {
       Task<List<SettingModel>> GetSettings(int tenantId,
                                      int userId);


       Task UpdateSettings(int tenantId,
                           int userId,
                           int notificationTypeId,
                           short deliveryModeId,
                           short? delayedInterval);
   }
}
