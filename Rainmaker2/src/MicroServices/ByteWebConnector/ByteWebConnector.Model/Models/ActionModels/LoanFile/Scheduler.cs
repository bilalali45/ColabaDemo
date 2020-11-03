













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // Scheduler

    public partial class Scheduler 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string CronText { get; set; } // CronText (length: 250)
        public string CronControlsXml { get; set; } // CronControlsXml (length: 1000)
        public System.TimeSpan? StartTimeUtc { get; set; } // StartTimeUtc
        public System.TimeSpan? EndTimeUtc { get; set; } // EndTimeUtc
        public int? MinIntervalTime { get; set; } // MinIntervalTime
        public bool? ShowCronBuilder { get; set; } // ShowCronBuilder
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
        /// Child Campaigns where [Campaign].[SchedulerId] point to this entity (FK_Campaign_Scheduler)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Campaign> Campaigns { get; set; } // Campaign.FK_Campaign_Scheduler
        /// <summary>
        /// Child ScheduleActivities where [ScheduleActivity].[SchedulerId] point to this entity (FK_ScheduleActivity_Scheduler)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<ScheduleActivity> ScheduleActivities { get; set; } // ScheduleActivity.FK_ScheduleActivity_Scheduler

        public Scheduler()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 44;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            Campaigns = new System.Collections.Generic.HashSet<Campaign>();
            ScheduleActivities = new System.Collections.Generic.HashSet<ScheduleActivity>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
