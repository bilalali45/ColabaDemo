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

    // LosTenantMilestone
    
    public partial class LosTenantMilestone : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public int? TenantId { get; set; } // TenantId
        public string Name { get; set; } // Name (length: 50)
        public short? ExternalOriginatorId { get; set; } // ExternalOriginatorId

        // Reverse navigation

        /// <summary>
        /// Child MilestoneMappings where [MilestoneMapping].[LosMilestoneId] point to this entity (FK_MilestoneMapping_LosTenantMilestone_Id)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<MilestoneMapping> MilestoneMappings { get; set; } // MilestoneMapping.FK_MilestoneMapping_LosTenantMilestone_Id

        // Foreign keys

        /// <summary>
        /// Parent ExternalOriginator pointed by [LosTenantMilestone].([ExternalOriginatorId]) (FK_LosTenantMilestone_ExternalOriginator_Id)
        /// </summary>
        public virtual ExternalOriginator ExternalOriginator { get; set; } // FK_LosTenantMilestone_ExternalOriginator_Id

        public LosTenantMilestone()
        {
            MilestoneMappings = new System.Collections.Generic.HashSet<MilestoneMapping>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
