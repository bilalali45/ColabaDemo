using Milestone.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Milestone.Service
{
    public interface ISettingService
    {
        Task<bool> SendEmailReminderLog(int loanApplicationId,int toStatus, int tenantId, IEnumerable<string> authHeader);
        Task<EmailStatusConfiguration> GetEmailStatusConfiguration(int fromStatus, int toStatus, int tenantId);
        Task<StatusConfigurationModel> GetLoanStatuses(int tenantId, IEnumerable<string> authHeader);
        Task<bool> UpdateLoanStatuses(StatusConfigurationModel statusConfigurationModel,int tenantId);
        Task<List<EmailConfigurationModel>> GetEmailConfigurations(List<int> emailIds);
        Task<bool> EnableDisableEmailReminder(int id, bool isActive, int userProfileId, IEnumerable<string> authHeader);
        Task EnableDisableAllEmailReminders(bool isActive, int userProfileId, IEnumerable<string> authHeader);
    }
}
