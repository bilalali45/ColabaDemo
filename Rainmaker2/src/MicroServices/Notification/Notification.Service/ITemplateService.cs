using System.Threading.Tasks;

namespace Notification.Service
{
    public interface ITemplateService
    {
        Task PopulateTemplate(long notificationId);
    }
}
