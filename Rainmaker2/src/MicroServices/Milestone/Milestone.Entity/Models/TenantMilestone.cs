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
// TargetFrameworkVersion = 2
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning


namespace Milestone.Entity.Models
{
    using System;
    using System.Collections.Generic;

    // TenantMilestone
    
    public partial class TenantMilestone : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public string BorrowerName { get; set; } // BorrowerName (length: 50)
        public string McuName { get; set; } // McuName (length: 50)
        public string Description { get; set; } // Description (length: 500)
        public bool? Visibility { get; set; } // Visibility
        public int? TenantId { get; set; } // TenantId

        // Foreign keys

        /// <summary>
        /// Parent Milestone pointed by [TenantMilestone].([Id]) (FK_TenantMilestone_Milestone_Id)
        /// </summary>
        public virtual Milestone Milestone { get; set; } // FK_TenantMilestone_Milestone_Id

        public TenantMilestone()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
