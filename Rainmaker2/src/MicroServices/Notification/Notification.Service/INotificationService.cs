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
        Task<long> Add(NotificationModel model, int userId, int tenantId);
    }
}
