using Notification.Entity.Models;
using Notification.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notification.Service
{
    public interface INotificationService : IServiceBase<NotificationObject>
    {
        Task<long> Add(NotificationModel model);
        Task<NotificationObject> GetByIdForTemplate(long notificationId);
        Task<List<NotificationMediumModel>> GetPaged(int pageSize, long lastId, int mediumId,int userId);
        Task<List<long>> Read(List<long> ids, int userId);
        Task Seen(List<long> ids);
        Task Delete(long id);
        Task DeleteAll();
        Task<NotificationMediumModel> Undelete(long id);
        Task<int> GetCount(int userProfileId);
        Task<TenantSettingModel> GetTenantSetting(int tenantId);
        Task<TenantSetting> GetTenantSetting(int tenantId, int notificationType);
        Task<TenantSetting> GetSetting(int tenantId);
        Task SetTenantSetting(int tenantId, TenantSettingModel model);
        Task<long> AddUserNotificationMedium(int userId,
                                       long notificationObjectId,
                                       short deliveryModeId,
                                       int notificationMediumId,
                                       int notificationTypeId);
        Task<NotificationRecepient> GetRecepient(long recepientId);
    }
}
