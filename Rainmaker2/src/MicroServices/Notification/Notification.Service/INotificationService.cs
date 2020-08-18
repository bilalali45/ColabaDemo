using Notification.Entity.Models;
using Notification.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Service
{
    public interface INotificationService : IServiceBase<NotificationObject>
    {
        Task<long> Add(NotificationModel model, int userId, int tenantId, IEnumerable<string> authHeader);
        Task<NotificationObject> GetByIdForTemplate(long notificationId);
        Task<List<NotificationMediumModel>> GetPaged(int pageSize, long lastId, int mediumId,int userId);
        Task Read(long id);
        Task Delete(long id);
        Task DeleteAll();
        Task<NotificationMediumModel> Undelete(long id);
    }
}
