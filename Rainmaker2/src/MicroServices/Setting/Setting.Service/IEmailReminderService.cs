using Setting.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Setting.Service
{
   public interface IEmailReminderService
    {
        Task<List<EmailReminderLogModel>> GetEmailreminderLogByDate(DateTime recurringTime, int tenantId, int jobTypeId);
        Task<bool> InsertEmailReminderLog(int tenantId, int jobTypeId, int loanApplicationId, IEnumerable<string> authHeader);
        void JobTrigger();
        Task DeleteEmailReminder(string id);
        Task EnableDisableEmailReminders(List<string> id,bool isActive);
        Task EnableDisableEmailReminders(List<int> id, bool isActive);
        Task EnableDisableAllEmailReminders(bool isActive, int jobTypeId);
        Task<int> InsertEmailReminderLogResponse(int emailReminderLogId, DateTime modifiedDate, bool isEmailSent, string response);
        Task UpdateEmailReminder(string id,int noOfDays, DateTime recurringTime);
        Task EnableDisableEmailRemindersbyLAId(List<int> laIds, bool isActive);
        Task<JobTypeModel> GetJobType(int id,int tenantId);
        Task<bool> InsertLoanStatusLog(int tenantId, int jobTypeId, int loanApplicationId, int? statusId, IEnumerable<string> authHeader);
    }
}
