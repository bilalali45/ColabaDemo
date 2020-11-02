













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // Campaign

    public partial class Campaign 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public System.DateTime? StartDateUtc { get; set; } // StartDateUtc
        public System.DateTime? EndDateUtc { get; set; } // EndDateUtc
        public int SchedulerId { get; set; } // SchedulerId
        public int RuleId { get; set; } // RuleId
        public System.DateTime? LastRunOnUtc { get; set; } // LastRunOnUtc
        public System.DateTime? NextRunOnUtc { get; set; } // NextRunOnUtc
        public int? NextRunOffset { get; set; } // NextRunOffset
        public int? DependOnTypeId { get; set; } // DependOnTypeId
        public int? DependOnId { get; set; } // DependOnId
        public int? OffsetFromMinute { get; set; } // OffsetFromMinute
        public int? OffsetToMinute { get; set; } // OffsetToMinute
        public int DisplayOrder { get; set; } // DisplayOrder
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDefault { get; set; } // IsDefault
        public bool IsSystem { get; set; } // IsSystem
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public bool IsDeleted { get; set; } // IsDeleted
        public bool IsTriggerBasedOnly { get; set; } // IsTriggerBasedOnly
        public bool IsRecurring { get; set; } // IsRecurring
        public string Remarks { get; set; } // Remarks (length: 4000)
        public string Utm { get; set; } // Utm (length: 500)

        // Reverse navigation

        /// <summary>
        /// Child CampaignActivityBinders where [CampaignActivityBinder].[CampaignId] point to this entity (FK_CampaignActivityBinder_Campaign)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<CampaignActivityBinder> CampaignActivityBinders { get; set; } // CampaignActivityBinder.FK_CampaignActivityBinder_Campaign
        /// <summary>
        /// Child CampaignQueues where [CampaignQueue].[CampaignId] point to this entity (FK_CampaignQueue_Campaign)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<CampaignQueue> CampaignQueues { get; set; } // CampaignQueue.FK_CampaignQueue_Campaign
        /// <summary>
        /// Child CampaignTriggerBinders where [CampaignTriggerBinder].[CampaignId] point to this entity (FK_CampaignTriggerBinder_Campaign)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<CampaignTriggerBinder> CampaignTriggerBinders { get; set; } // CampaignTriggerBinder.FK_CampaignTriggerBinder_Campaign
        /// <summary>
        /// Child WorkQueues where [WorkQueue].[CampaignId] point to this entity (FK_WorkQueue_Campaign)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<WorkQueue> WorkQueues { get; set; } // WorkQueue.FK_WorkQueue_Campaign

        // Foreign keys

        /// <summary>
        /// Parent Rule pointed by [Campaign].([RuleId]) (FK_Campaign_Rule)
        /// </summary>
        public virtual Rule Rule { get; set; } // FK_Campaign_Rule

        /// <summary>
        /// Parent Scheduler pointed by [Campaign].([SchedulerId]) (FK_Campaign_Scheduler)
        /// </summary>
        public virtual Scheduler Scheduler { get; set; } // FK_Campaign_Scheduler

        public Campaign()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 96;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            IsTriggerBasedOnly = false;
            IsRecurring = true;
            CampaignActivityBinders = new System.Collections.Generic.HashSet<CampaignActivityBinder>();
            CampaignQueues = new System.Collections.Generic.HashSet<CampaignQueue>();
            CampaignTriggerBinders = new System.Collections.Generic.HashSet<CampaignTriggerBinder>();
            WorkQueues = new System.Collections.Generic.HashSet<WorkQueue>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
