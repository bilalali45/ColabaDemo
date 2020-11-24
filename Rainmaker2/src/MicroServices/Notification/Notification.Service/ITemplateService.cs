using System.Threading.Tasks;

namespace Notification.Service
{
    public interface ITemplateService
    {
        Task<string> PopulateTemplate(long notificationId, int notificationTypeId, int notificationMediumId);
    }
}
