using Notification.Model;
using System.Threading.Tasks;

namespace Notification.Service
{
    public interface IRedisService
    {
        Task Run();
        Task InsertInCache(NotificationModel model);
        Task<bool> SendNotification(NotificationModel model);
    }
}
