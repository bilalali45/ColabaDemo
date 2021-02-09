using DocumentManagement.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Service
{
    public interface IEmailReminderService
    {
        Task<string> AddEmailReminder(int tenantId, int noOfDays, DateTime recurringTime, string fromAddress, string ccAddress, string subject, string emailBody,int userProfileId);
        Task<Model.EmailReminderModel> GetEmailReminders(int tenantId, IEnumerable<string> authHeader);
        Task<bool> UpdateEmailReminder(string id, int noOfDays, DateTime recurringTime, string fromAddress, string ccAddress, string subject, string emailBody, int userProfileId,IEnumerable<string> authHeader);
        Task<bool> DeleteEmailReminder(string id, int userProfileId,IEnumerable<string> authHeader);
        Task<bool> EnableDisableEmailReminder(string id,bool isActive, int userProfileId, IEnumerable<string> authHeader);
        Task EnableDisableAllEmailReminders(bool isActive,int userProfileId,IEnumerable<string> authHeader);
        Task<List<RemainingDocuments>> GetDocumentStatusByLoanIds(List<RemainingDocuments> remainingDocuments);
        Task<Model.EmailReminder> GetEmailReminderById(string id);
        Task<List<Model.EmailReminder>> GetEmailReminderByIds(List<string> emailReminderIds,int tenantId);
    }
}
