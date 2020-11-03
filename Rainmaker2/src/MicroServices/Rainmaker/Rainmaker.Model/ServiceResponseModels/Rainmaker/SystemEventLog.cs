using System;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class SystemEventLog
    {
        public int Id { get; set; }
        public int? VisitorId { get; set; }
        public int? UserId { get; set; }
        public int? SessionId { get; set; }
        public int? EventTypeId { get; set; }
        public string ResourceName { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public string Remarks { get; set; }
        public int EntityTypeId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int? LoanRequestId { get; set; }
        public int? OpportunityId { get; set; }

        public SystemEvent SystemEvent { get; set; }

        public UserProfile UserProfile { get; set; }

        public Visitor Visitor { get; set; }
    }
}