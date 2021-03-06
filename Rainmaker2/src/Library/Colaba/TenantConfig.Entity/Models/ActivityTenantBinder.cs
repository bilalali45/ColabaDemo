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

    // ActivityTenantBinder
    
    public partial class ActivityTenantBinder : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public int? TenantId { get; set; } // TenantId
        public int? ActivityId { get; set; } // ActivityId

        // Foreign keys

        /// <summary>
        /// Parent Activity pointed by [ActivityTenantBinder].([ActivityId]) (FK_ActivityTenantBinder_Activity_Id)
        /// </summary>
        public virtual Activity Activity { get; set; } // FK_ActivityTenantBinder_Activity_Id

        /// <summary>
        /// Parent Tenant pointed by [ActivityTenantBinder].([TenantId]) (FK_ActivityTenantBinder_Tenant_Id)
        /// </summary>
        public virtual Tenant Tenant { get; set; } // FK_ActivityTenantBinder_Tenant_Id

        public ActivityTenantBinder()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
