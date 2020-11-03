













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // Activity

    public partial class Activity 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public int? ActivityTypeId { get; set; } // ActivityTypeId
        public int? TemplateId { get; set; } // TemplateId
        public int DisplayOrder { get; set; } // DisplayOrder
        public bool IsRecurring { get; set; } // IsRecurring
        public int? RecurDuration { get; set; } // RecurDuration
        public int? RecurCount { get; set; } // RecurCount
        public int ActivityForId { get; set; } // ActivityForId
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDefault { get; set; } // IsDefault
        public bool IsSystem { get; set; } // IsSystem
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public bool IsDeleted { get; set; } // IsDeleted
        public int? EntityRefTypeId { get; set; } // EntityRefTypeId
        public bool RequiredSubscription { get; set; } // RequiredSubscription
        public int? BusinessUnitId { get; set; } // BusinessUnitId
        public bool? IsCustomerDefault { get; set; } // IsCustomerDefault
        public string Utm { get; set; } // Utm (length: 500)
        public int? MinimumInterval { get; set; } // MinimumInterval

        // Reverse navigation

        /// <summary>
        /// Child ActivitySubscriptionBinders where [ActivitySubscriptionBinder].[ActivityId] point to this entity (FK_ActivitySubscriptionBinder_Activity)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<ActivitySubscriptionBinder> ActivitySubscriptionBinders { get; set; } // ActivitySubscriptionBinder.FK_ActivitySubscriptionBinder_Activity
        /// <summary>
        /// Child CampaignActivityBinders where [CampaignActivityBinder].[ActivityId] point to this entity (FK_CampaignActivityBinder_Activity)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<CampaignActivityBinder> CampaignActivityBinders { get; set; } // CampaignActivityBinder.FK_CampaignActivityBinder_Activity
        /// <summary>
        /// Child WorkQueues where [WorkQueue].[ActivityId] point to this entity (FK_WorkQueue_Activity)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<WorkQueue> WorkQueues { get; set; } // WorkQueue.FK_WorkQueue_Activity

        // Foreign keys

        /// <summary>
        /// Parent ActivityType pointed by [Activity].([ActivityTypeId]) (FK_ActivityLog_ActivityType)
        /// </summary>
        public virtual ActivityType ActivityType { get; set; } // FK_ActivityLog_ActivityType

        /// <summary>
        /// Parent BusinessUnit pointed by [Activity].([BusinessUnitId]) (FK_Activity_BusinessUnit)
        /// </summary>
        public virtual BusinessUnit BusinessUnit { get; set; } // FK_Activity_BusinessUnit

        /// <summary>
        /// Parent Template pointed by [Activity].([TemplateId]) (FK_Activity_Template)
        /// </summary>
        public virtual Template Template { get; set; } // FK_Activity_Template

        public Activity()
        {
            DisplayOrder = 0;
            IsRecurring = true;
            ActivityForId = 1;
            IsActive = true;
            EntityTypeId = 88;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            RequiredSubscription = false;
            IsCustomerDefault = false;
            ActivitySubscriptionBinders = new System.Collections.Generic.HashSet<ActivitySubscriptionBinder>();
            CampaignActivityBinders = new System.Collections.Generic.HashSet<CampaignActivityBinder>();
            WorkQueues = new System.Collections.Generic.HashSet<WorkQueue>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
