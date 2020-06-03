// <auto-generated>
// ReSharper disable ConvertPropertyToExpressionBody
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable EmptyNamespace
// ReSharper disable InconsistentNaming
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantOverridenMember
// ReSharper disable UseNameofExpression
// TargetFrameworkVersion = 2.1
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning


namespace RainMaker.Entity.Models
{
    using System;
    using System.Collections.Generic;

    // ScheduleActivity
    
    public partial class ScheduleActivity : URF.Core.EF.Trackable.Entity
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