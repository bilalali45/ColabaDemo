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

    // MilestoneMapping
    
    public partial class MilestoneMapping : URF.Core.EF.Trackable.Entity
    {
        public int MilestoneId { get; set; } // MilestoneId (Primary key)
        public int LosMilestoneId { get; set; } // LosMilestoneId (Primary key)

        // Foreign keys

        /// <summary>
        /// Parent LosTenantMilestone pointed by [MilestoneMapping].([LosMilestoneId]) (FK_MilestoneMapping_LosTenantMilestone_Id)
        /// </summary>
        public virtual LosTenantMilestone LosTenantMilestone { get; set; } // FK_MilestoneMapping_LosTenantMilestone_Id

        /// <summary>
        /// Parent Milestone pointed by [MilestoneMapping].([MilestoneId]) (FK_MilestoneMapping_Milestone_Id)
        /// </summary>
        public virtual Milestone Milestone { get; set; } // FK_MilestoneMapping_Milestone_Id

        public MilestoneMapping()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
