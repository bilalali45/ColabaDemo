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


namespace LoanApplicationDb.Entity.Models
{
    using System;
    using System.Collections.Generic;

    // LoanPurpose
    
    public partial class LoanPurpose : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public bool IsPurchase { get; set; } // IsPurchase
        public int DisplayOrder { get; set; } // DisplayOrder
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDefault { get; set; } // IsDefault
        public bool IsSystem { get; set; } // IsSystem
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? ModifiedBy { get; set; } // ModifiedBy
        public string TpId { get; set; } // TpId (length: 50)
        public bool IsDeleted { get; set; } // IsDeleted
        public string Image { get; set; } // Image (length: 100)

        // Reverse navigation

        /// <summary>
        /// Child LoanApplications where [LoanApplication].[LoanPurposeId] point to this entity (FK_LoanApplication_LoanPurpose)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanApplication> LoanApplications { get; set; } // LoanApplication.FK_LoanApplication_LoanPurpose
        /// <summary>
        /// Child LoanGoals where [LoanGoal].[LoanPurposeId] point to this entity (FK_LoanGoal_LoanPurpose)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanGoal> LoanGoals { get; set; } // LoanGoal.FK_LoanGoal_LoanPurpose

        public LoanPurpose()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 4;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            LoanApplications = new System.Collections.Generic.HashSet<LoanApplication>();
            LoanGoals = new System.Collections.Generic.HashSet<LoanGoal>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
