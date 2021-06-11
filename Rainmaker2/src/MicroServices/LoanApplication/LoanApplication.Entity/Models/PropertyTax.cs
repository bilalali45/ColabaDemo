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

    // PropertyTax
    
    public partial class PropertyTax : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 200)
        public string Description { get; set; } // Description (length: 500)
        public string FeeNumber { get; set; } // FeeNumber (length: 50)
        public int? FeeTypeId { get; set; } // FeeTypeId
        public int? PaidById { get; set; } // PaidById
        public int? FeeBlockId { get; set; } // FeeBlockId
        public int? StateId { get; set; } // StateId
        public int EscrowEntityTypeId { get; set; } // EscrowEntityTypeId
        public int RoundTypeId { get; set; } // RoundTypeId
        public int CalcTypeId { get; set; } // CalcTypeId
        public int? FormulaId { get; set; } // FormulaId
        public decimal Value { get; set; } // Value
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
        public int? CalcBaseOnId { get; set; } // CalcBaseOnId
        public int? RangSetId { get; set; } // RangSetId
        public int TenantId { get; set; } // TenantId

        // Foreign keys

        /// <summary>
        /// Parent EscrowEntityType pointed by [PropertyTax].([EscrowEntityTypeId]) (FK_PropertyTax_EscrowEntityType)
        /// </summary>
        public virtual EscrowEntityType EscrowEntityType { get; set; } // FK_PropertyTax_EscrowEntityType

        /// <summary>
        /// Parent PaidBy pointed by [PropertyTax].([PaidById]) (FK_PropertyTax_PaidBy)
        /// </summary>
        public virtual PaidBy PaidBy { get; set; } // FK_PropertyTax_PaidBy

        /// <summary>
        /// Parent State pointed by [PropertyTax].([StateId]) (FK_PropertyTax_State)
        /// </summary>
        public virtual State State { get; set; } // FK_PropertyTax_State

        public PropertyTax()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 104;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>