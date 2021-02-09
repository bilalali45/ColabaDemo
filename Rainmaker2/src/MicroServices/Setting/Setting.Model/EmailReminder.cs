using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Setting.Model
{
    public static class JobType
    {
        public const int EmailReminder = 1;
        public const int LoanStatusUpdate = 2;
    }
    public enum ActivityForType
    {
        LoanApplicationDocumentRequestActivity = 19,
        LoanApplicationDocumentRejectActivity = 20
    }
    public class EmailReminderLogModel
    {
        public int id { get; set; }
        public int tenantId { get; set; }
        public int loanApplicationId { get; set; }
        public int jobTypeId { get; set; }
        public DateTime requestDate { get; set; }
        public DateTime? RecurringDate { get; set; }
        public string ReminderId { get; set; }
        public int? loanStatusId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
    }
    public class EmailReminderModel
    {
        public bool isActive { get; set; }
        public List<EmailReminder> emailReminders { get; set; }
    }

    public class EmailReminder
    {
        public string id { get; set; }
        public int noOfDays { get; set; }
        public DateTime recurringTime { get; set; }
        public bool isActive { get; set; }
        public EmailDetail email { get; set; }
        public int LoanApplicationId { get; set; }
        public int EmailReminderLogId { get; set; }
    }
    public class EmailDetail
    {
        public string id { get; set; }
        public string fromAddress { get; set; }
        public string ccAddress { get; set; }
        public string subject { get; set; }
        public string emailBody { get; set; }

    }
    public class RemainingDocuments
    {
        public int loanApplicationId { get; set; }
        public bool isDocumentRemaining { get; set; }
    }
    public class RemainingDocumentsModel
    {
        public List<RemainingDocuments> remainingDocuments { get; set; }

        public static implicit operator List<object>(RemainingDocumentsModel v)
        {
            throw new NotImplementedException();
        }
    }
    public class DeleteEmailReminderLogModel
    {
        public string id { get; set; }
    }
    public class RequestEmailRemidersByIds
    {
        public string[] id { get; set; }
    }
    public class RequestLoanStatusByIds
    {
        public int[] id { get; set; }
    }
    public class ResponseLoanStatus
    {
        public int id { get; set; }
        public int statusUpdateId { get; set; }
        public string fromAddress { get; set; }
        public string ccAddress { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
    }
    public class EnableDisableReminderModel
    {
        public string[] id { get; set; }
        public bool isActive { get; set; }
    }
    public class EnableDisableAllRemindersModel
    {
        public bool isActive { get; set; }
        public int jobTypeId { get; set; }
    }
    public class EnableDisableLoanStatusReminderModel
    {
        public int[] id { get; set; }
        public bool isActive { get; set; }
    }
    public class UpdateEmailReminderLogModel
    {
        public string id { get; set; }
        public int noOfDays { get; set; }
        public DateTime recurringTime { get; set; } 
    }

    public class JobTypeModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool isActive { get; set; }
    }
    public class GetJobTypeModel
    {
        [Required(ErrorMessage = ValidationMessages.ValidationFieldEmpty)]
        public int jobTypeId { get; set; }
    }
    public class DashboardDTO
    {
        public string id { get; set; }
        public string requestId { get; set; }
        public string docId { get; set; }
        public string docName { get; set; }
        public string docMessage { get; set; }
        public bool isRejected { get; set; }
        public List<FileDto> files { get; set; }
    }

    public class FileDto
    {
        public string id { get; set; }
        public string clientName { get; set; }
        public DateTime fileUploadedOn { get; set; }
        public int size { get; set; }
        public int order { get; set; }
    }
}
