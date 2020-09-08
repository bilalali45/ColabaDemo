using Notification.Model;
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
