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


namespace Milestone.Entity.Models
{
    using System;
    using System.Collections.Generic;

    // MilestoneSetting
    
    public partial class MilestoneSetting : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public int? TenantId { get; set; } // TenantId
        public bool? ShowMilestone { get; set; } // ShowMilestone

        public MilestoneSetting()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
