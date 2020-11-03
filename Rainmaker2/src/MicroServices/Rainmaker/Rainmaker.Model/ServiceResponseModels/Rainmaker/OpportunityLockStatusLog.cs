using System;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class OpportunityLockStatusLog
    {
        public int Id { get; set; }
        public int? LockStatusId { get; set; }
        public int? LockCauseId { get; set; }
        public DateTime? DatetimeUtc { get; set; }
        public int? OpportunityId { get; set; }
        public bool IsActive { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public int EntityTypeId { get; set; }

        public LockStatusCause LockStatusCause { get; set; }

        public LockStatusList LockStatusList { get; set; }

        public Opportunity Opportunity { get; set; }
    }
}