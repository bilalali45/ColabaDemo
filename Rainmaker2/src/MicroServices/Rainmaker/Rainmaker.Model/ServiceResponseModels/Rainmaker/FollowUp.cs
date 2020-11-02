using System;
using System.Collections.Generic;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class FollowUp
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public bool IsAnytime { get; set; }
        public int? FollowUpPurposeId { get; set; }
        public int? OpportunityId { get; set; }
        public int? ContactId { get; set; }
        public int? ContactPhoneId { get; set; }
        public int? ContactEmailId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime? FollowUpStartDateUtc { get; set; }
        public DateTime? FollowUpEndDateUtc { get; set; }
        public DateTime? RemindOnUtc { get; set; }
        public DateTime? FollowedUpOn { get; set; }
        public string ActivityMessage { get; set; }
        public int StatusId { get; set; }
        public int? FollowUpPriorityId { get; set; }
        public int EntityTypeId { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<FollowUpActivityBinder> FollowUpActivityBinders { get; set; }

        public ICollection<FollowUpReminderVia> FollowUpReminderVias { get; set; }

        public Contact Contact { get; set; }

        public ContactEmailInfo ContactEmailInfo { get; set; }

        public ContactPhoneInfo ContactPhoneInfo { get; set; }

        public Employee Employee { get; set; }

        public FollowUpPriority FollowUpPriority { get; set; }

        public FollowUpPurpose FollowUpPurpose { get; set; }

        public Opportunity Opportunity { get; set; }
    }
}