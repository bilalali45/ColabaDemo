using System;
using System.Collections.Generic;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class Notification
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public bool IsEmployeeNotification { get; set; }
        public int? OpportunityId { get; set; }
        public int? EmployeeId { get; set; }
        public int? CustomerId { get; set; }
        public string Url { get; set; }
        public int? NotificationTypeId { get; set; }
        public int? CriticalId { get; set; }
        public int EntityTypeId { get; set; }
        public bool IsPosted { get; set; }
        public bool IsPitched { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; }

        public ICollection<NotificationTo> NotificationToes { get; set; }

        public Customer Customer { get; set; }

        public Employee Employee { get; set; }

        public Opportunity Opportunity { get; set; }
    }
}