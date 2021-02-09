using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Milestone.Model
{
    public static class JobType
    {
        public const int EmailReminder = 1;
        public const int LoanStatusUpdate = 2;
    }
    public class LoanStatus
    {
        public int id { get; set; }
        public string mcuName { get; set; }
        public int statusId { get; set; }
        public int tenantId { get; set; }
        public int fromStatusId { get; set; }
        public string fromStatus { get; set; }
        public int toStatusId { get; set; }
        public string toStatus { get; set; }
        public short noofDays { get; set; }
        public DateTime recurringTime { get; set; }
        public bool isActive { get; set; }
        public int emailId { get; set; }
        public string fromAddress { get; set; }
        public string ccAddress { get; set; }
        public string subject { get; set; }
        public string body { get; set; }

        public LoanStatus()
        {
            this.isActive = true; 
        }
    }

    public class StatusConfigurationModel
    {
        public bool isActive { get; set; }
        public List<LoanStatus> loanStatuses { get; set; }
    }

    public class JobTypeModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool isActive { get; set; }
    }
    public class GetJobTypeModel
    {
        public int jobTypeId { get; set; }
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
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? loanStatusId { get; set; }

    }

    public class EmailConfigurationModel
    {
        public int id { get; set; }
        public int statusUpdateId { get; set; }
        public string fromAddress { get; set; }
        public string ccAddress { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
    }

    public class GetEmailConfigurationByIds
    {
        [Required(ErrorMessage = ValidationMessages.ValidationFailed)]
        
        public int[] id { get; set; }
    }
    public class EnableDisableEmailReminderModel
    {
        [Required(ErrorMessage = ValidationMessages.ValidationFieldEmpty)]
        public int id { get; set; }
        [Required(ErrorMessage = ValidationMessages.ValidationFieldEmpty)]
        public bool isActive { get; set; }
    }
    public class EnableDisableAllEmailReminderModel
    {
        [Required(ErrorMessage = ValidationMessages.ValidationFieldEmpty)]
        public bool isActive { get; set; }
    }
}
