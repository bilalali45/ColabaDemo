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
    // LockStatusList

    public partial class LockStatusList : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
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
        public int LockTypeId { get; set; } // LockTypeId
        public string CustomerDisplay { get; set; } // CustomerDisplay (length: 150)
        public string EmployeeDisplay { get; set; } // EmployeeDisplay (length: 150)

        // Reverse navigation

        /// <summary>
        /// Child LockStatusCauses where [LockStatusCause].[LockStatusId] point to this entity (FK_LockStatusCause_LockStatusList)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LockStatusCause> LockStatusCauses { get; set; } // LockStatusCause.FK_LockStatusCause_LockStatusList
        /// <summary>
        /// Child Opportunities where [Opportunity].[LockStatusId] point to this entity (FK_Opportunity_LockStatusList)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Opportunity> Opportunities { get; set; } // Opportunity.FK_Opportunity_LockStatusList
        /// <summary>
        /// Child OpportunityLockStatusLogs where [OpportunityLockStatusLog].[LockStatusId] point to this entity (FK_OpportunityLockStatusLog_LockStatusList)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<OpportunityLockStatusLog> OpportunityLockStatusLogs { get; set; } // OpportunityLockStatusLog.FK_OpportunityLockStatusLog_LockStatusList

        public LockStatusList()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 167;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            LockStatusCauses = new System.Collections.Generic.HashSet<LockStatusCause>();
            Opportunities = new System.Collections.Generic.HashSet<Opportunity>();
            OpportunityLockStatusLogs = new System.Collections.Generic.HashSet<OpportunityLockStatusLog>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
