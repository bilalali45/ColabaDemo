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

    // Milestone
    
    public partial class Milestone : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public int Order { get; set; } // Order
        public string Icon { get; set; } // Icon
        public string BorrowerName { get; set; } // BorrowerName (length: 50)
        public string McuName { get; set; } // McuName (length: 50)
        public string Description { get; set; } // Description (length: 500)
        public int MilestoneTypeId { get; set; } // MilestoneTypeId

        // Reverse navigation

        /// <summary>
        /// Child MilestoneLogs where [MilestoneLog].[MilestoneId] point to this entity (FK_MilestoneLog_Milestone)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<MilestoneLog> MilestoneLogs { get; set; } // MilestoneLog.FK_MilestoneLog_Milestone
        /// <summary>
        /// Child MilestoneMappings where [MilestoneMapping].[MilestoneId] point to this entity (FK_MilestoneMapping_Milestone)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<MilestoneMapping> MilestoneMappings { get; set; } // MilestoneMapping.FK_MilestoneMapping_Milestone
        /// <summary>
        /// Child MilestoneStatusConfigurations where [MilestoneStatusConfiguration].[FromStatus] point to this entity (FK_MilestoneStatusUpdate_MilestoneStatusUpdate)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<MilestoneStatusConfiguration> MilestoneStatusConfigurations { get; set; } // MilestoneStatusConfiguration.FK_MilestoneStatusUpdate_MilestoneStatusUpdate
        /// <summary>
        /// Child TenantMilestones where [TenantMilestone].[MilestoneId] point to this entity (FK_TenantMilestone_Milestone)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<TenantMilestone> TenantMilestones { get; set; } // TenantMilestone.FK_TenantMilestone_Milestone

        // Foreign keys

        /// <summary>
        /// Parent MilestoneType pointed by [Milestone].([MilestoneTypeId]) (FK_Milestone_MilestoneType)
        /// </summary>
        public virtual MilestoneType MilestoneType { get; set; } // FK_Milestone_MilestoneType

        public Milestone()
        {
            MilestoneLogs = new System.Collections.Generic.HashSet<MilestoneLog>();
            MilestoneMappings = new System.Collections.Generic.HashSet<MilestoneMapping>();
            MilestoneStatusConfigurations = new System.Collections.Generic.HashSet<MilestoneStatusConfiguration>();
            TenantMilestones = new System.Collections.Generic.HashSet<TenantMilestone>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
