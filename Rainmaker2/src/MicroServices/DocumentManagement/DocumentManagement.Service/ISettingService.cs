using DocumentManagement.Entity;
using DocumentManagement.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocumentManagement.Service
{
    public interface ISettingService
    {
        Task<Setting> GetSetting();
        Task<bool> InsertEmailReminderLog(EmailReminderLogModel emailReminderLogModel, IEnumerable<string> authHeader);
    }
}
