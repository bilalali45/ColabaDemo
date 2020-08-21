using Notification.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Service
{
    public interface IRedisService
    {
        Task Run();
        Task InsertInCache(NotificationModel model);
        Task SendNotification(long id);
    }
}
