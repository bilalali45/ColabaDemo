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
    // OpportunityFee

    public partial class OpportunityFee : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public int FeeBlockId { get; set; } // FeeBlockId
        public int? FeeId { get; set; } // FeeId
        public int OpportunityId { get; set; } // OpportunityId
        public bool IsNegativeFee { get; set; } // IsNegativeFee
        public bool IsIncludeInApr { get; set; } // IsIncludeInApr
        public bool IsIncludeInClosingCost { get; set; } // IsIncludeInClosingCost
        public bool IsFinancedToLoan { get; set; } // IsFinancedToLoan
        public bool IsOriginationFee { get; set; } // IsOriginationFee
        public decimal Value { get; set; } // Value
        public int? FeeTypeId { get; set; } // FeeTypeId
        public string FeeNumber { get; set; } // FeeNumber (length: 150)
        public System.DateTime StartDateUtc { get; set; } // StartDateUtc
        public System.DateTime? EndDateUtc { get; set; } // EndDateUtc
        public string Note { get; set; } // Note (length: 500)
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
        public int? CreditedById { get; set; } // CreditedById

        // Reverse navigation

        /// <summary>
        /// Child LoanApplicationFees where [LoanApplicationFee].[OpportunityFeeId] point to this entity (FK_LoanApplicationFee_OpportunityFee)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanApplicationFee> LoanApplicationFees { get; set; } // LoanApplicationFee.FK_LoanApplicationFee_OpportunityFee
        /// <summary>
        /// Child OpportunityFeePaidBies where [OpportunityFeePaidBy].[OpportunityFeeId] point to this entity (FK_OpportunityFeePaidBy_OpportunityFee)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<OpportunityFeePaidBy> OpportunityFeePaidBies { get; set; } // OpportunityFeePaidBy.FK_OpportunityFeePaidBy_OpportunityFee

        // Foreign keys

        /// <summary>
        /// Parent Fee pointed by [OpportunityFee].([FeeId]) (FK_OpportunityFee_Fee)
        /// </summary>
        public virtual Fee Fee { get; set; } // FK_OpportunityFee_Fee

        /// <summary>
        /// Parent FeeBlock pointed by [OpportunityFee].([FeeBlockId]) (FK_OpportunityFee_FeeBlock)
        /// </summary>
        public virtual FeeBlock FeeBlock { get; set; } // FK_OpportunityFee_FeeBlock

        /// <summary>
        /// Parent FeeType pointed by [OpportunityFee].([FeeTypeId]) (FK_OpportunityFee_FeeType)
        /// </summary>
        public virtual FeeType FeeType { get; set; } // FK_OpportunityFee_FeeType

        /// <summary>
        /// Parent Opportunity pointed by [OpportunityFee].([OpportunityId]) (FK_OpportunityFee_Opportunity)
        /// </summary>
        public virtual Opportunity Opportunity { get; set; } // FK_OpportunityFee_Opportunity

        /// <summary>
        /// Parent PaidBy pointed by [OpportunityFee].([CreditedById]) (FK_OpportunityFee_PaidBy)
        /// </summary>
        public virtual PaidBy PaidBy { get; set; } // FK_OpportunityFee_PaidBy

        public OpportunityFee()
        {
            IsNegativeFee = false;
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 73;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            LoanApplicationFees = new System.Collections.Generic.HashSet<LoanApplicationFee>();
            OpportunityFeePaidBies = new System.Collections.Generic.HashSet<OpportunityFeePaidBy>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
