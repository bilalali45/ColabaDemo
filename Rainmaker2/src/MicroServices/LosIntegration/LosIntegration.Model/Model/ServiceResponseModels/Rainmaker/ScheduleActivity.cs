













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // ScheduleActivity

    public partial class ScheduleActivity 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 500)
        public int SchedulerId { get; set; } // SchedulerId
        public int SystemActivityId { get; set; } // SystemActivityId
        public int? QueueStatusId { get; set; } // QueueStatusId
        public bool IsActive { get; set; } // IsActive
        public bool StopOnError { get; set; } // StopOnError
        public System.DateTime? LastStartUtc { get; set; } // LastStartUtc
        public System.DateTime? LastEndUtc { get; set; } // LastEndUtc
        public System.DateTime? LastSuccessUtc { get; set; } // LastSuccessUtc
        public System.DateTime? NextRunUtc { get; set; } // NextRunUtc
        public int? NextRunOffset { get; set; } // NextRunOffset
        public int EntityTypeId { get; set; } // EntityTypeId
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public bool IsSystem { get; set; } // IsSystem

        // Reverse navigation

        /// <summary>
        /// Child ScheduleActivityLogs where [ScheduleActivityLog].[ScheduleActivityId] point to this entity (FK_ScheduleActivityLog_ScheduleActivity)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<ScheduleActivityLog> ScheduleActivityLogs { get; set; } // ScheduleActivityLog.FK_ScheduleActivityLog_ScheduleActivity

        // Foreign keys

        /// <summary>
        /// Parent Scheduler pointed by [ScheduleActivity].([SchedulerId]) (FK_ScheduleActivity_Scheduler)
        /// </summary>
        public virtual Scheduler Scheduler { get; set; } // FK_ScheduleActivity_Scheduler

        public ScheduleActivity()
        {
            EntityTypeId = 199;
            IsSystem = false;
            ScheduleActivityLogs = new System.Collections.Generic.HashSet<ScheduleActivityLog>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
