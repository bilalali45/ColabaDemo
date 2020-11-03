using System;
using System.Collections.Generic;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class Campaign
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? StartDateUtc { get; set; }
        public DateTime? EndDateUtc { get; set; }
        public int SchedulerId { get; set; }
        public int RuleId { get; set; }
        public DateTime? LastRunOnUtc { get; set; }
        public DateTime? NextRunOnUtc { get; set; }
        public int? NextRunOffset { get; set; }
        public int? DependOnTypeId { get; set; }
        public int? DependOnId { get; set; }
        public int? OffsetFromMinute { get; set; }
        public int? OffsetToMinute { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public int EntityTypeId { get; set; }
        public bool IsDefault { get; set; }
        public bool IsSystem { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsTriggerBasedOnly { get; set; }
        public bool IsRecurring { get; set; }
        public string Remarks { get; set; }
        public string Utm { get; set; }

        public ICollection<CampaignActivityBinder> CampaignActivityBinders { get; set; }

        public ICollection<CampaignQueue> CampaignQueues { get; set; }

        public ICollection<CampaignTriggerBinder> CampaignTriggerBinders { get; set; }

        //        public System.Collections.Generic.ICollection<WorkQueue> WorkQueues { get; set; }

        public Rule Rule { get; set; }

        public Scheduler Scheduler { get; set; }
    }
}