













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // ActivityType

    public partial class ActivityType 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
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

        // Reverse navigation

        /// <summary>
        /// Child Activities where [Activity].[ActivityTypeId] point to this entity (FK_ActivityLog_ActivityType)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Activity> Activities { get; set; } // Activity.FK_ActivityLog_ActivityType
        /// <summary>
        /// Child WorkQueues where [WorkQueue].[ActivityTypeId] point to this entity (FK_WorkQueue_ActivityType)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<WorkQueue> WorkQueues { get; set; } // WorkQueue.FK_WorkQueue_ActivityType

        public ActivityType()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 101;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            Activities = new System.Collections.Generic.HashSet<Activity>();
            WorkQueues = new System.Collections.Generic.HashSet<WorkQueue>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
