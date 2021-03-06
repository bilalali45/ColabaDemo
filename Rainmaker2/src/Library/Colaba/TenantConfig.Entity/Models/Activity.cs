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


namespace TenantConfig.Entity.Models
{
    using System;
    using System.Collections.Generic;

    // Activity
    
    public partial class Activity : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public int? ActivityTypeId { get; set; } // ActivityTypeId
        public int? TemplateId { get; set; } // TemplateId
        public bool? IsActive { get; set; } // IsActive
        public string Name { get; set; } // Name (length: 50)

        // Reverse navigation

        /// <summary>
        /// Child ActivityTenantBinders where [ActivityTenantBinder].[ActivityId] point to this entity (FK_ActivityTenantBinder_Activity_Id)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<ActivityTenantBinder> ActivityTenantBinders { get; set; } // ActivityTenantBinder.FK_ActivityTenantBinder_Activity_Id
        /// <summary>
        /// Child WorkQueues where [WorkQueue].[ActivityId] point to this entity (FK_WorkQueue_Activity_Id)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<WorkQueue> WorkQueues { get; set; } // WorkQueue.FK_WorkQueue_Activity_Id

        // Foreign keys

        /// <summary>
        /// Parent ActivityType pointed by [Activity].([ActivityTypeId]) (FK_Activity_ActivityType_Id)
        /// </summary>
        public virtual ActivityType ActivityType { get; set; } // FK_Activity_ActivityType_Id

        /// <summary>
        /// Parent Template pointed by [Activity].([TemplateId]) (FK_Activity_Template_Id)
        /// </summary>
        public virtual Template Template { get; set; } // FK_Activity_Template_Id

        public Activity()
        {
            ActivityTenantBinders = new System.Collections.Generic.HashSet<ActivityTenantBinder>();
            WorkQueues = new System.Collections.Generic.HashSet<WorkQueue>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
