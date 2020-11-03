













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // FeeDetail

    public partial class FeeDetail 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public int? FeeId { get; set; } // FeeId
        public int? RuleId { get; set; } // RuleId
        public int? CalcBaseOnId { get; set; } // CalcBaseOnId
        public int RoundTypeId { get; set; } // RoundTypeId
        public int CalcTypeId { get; set; } // CalcTypeId
        public int? FormulaId { get; set; } // FormulaId
        public decimal Value { get; set; } // Value
        public int? RangeSetId { get; set; } // RangeSetId
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
        public int? VendorId { get; set; } // VendorId
        public bool IsNegativeFee { get; set; } // IsNegativeFee
        public bool IsIncludeInApr { get; set; } // IsIncludeInApr
        public bool IsIncludeInClosingCost { get; set; } // IsIncludeInClosingCost
        public bool IsFinancedToLoan { get; set; } // IsFinancedToLoan
        public bool IsOriginationFee { get; set; } // IsOriginationFee
        public bool IsDisplayZero { get; set; } // IsDisplayZero
        public int? CreditedById { get; set; } // CreditedById
        public int? CalcTypeIdPublished { get; set; } // CalcTypeIdPublished
        public int? FormulaIdPublished { get; set; } // FormulaIdPublished
        public decimal? ValuePublished { get; set; } // ValuePublished
        public int? RangeSetIdPublished { get; set; } // RangeSetIdPublished

        // Reverse navigation

        /// <summary>
        /// Child FeeDetailPaidBies where [FeeDetailPaidBy].[FeeDetailId] point to this entity (FK_FeeDetailPaidBy_FeeDetail)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<FeeDetailPaidBy> FeeDetailPaidBies { get; set; } // FeeDetailPaidBy.FK_FeeDetailPaidBy_FeeDetail
        /// <summary>
        /// Child LoanApplicationFees where [LoanApplicationFee].[FeeDetailId] point to this entity (FK_LoanApplicationFee_FeeDetail)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanApplicationFee> LoanApplicationFees { get; set; } // LoanApplicationFee.FK_LoanApplicationFee_FeeDetail
        /// <summary>
        /// Child OpportunityFeeDetails where [OpportunityFeeDetail].[FeeDetailId] point to this entity (FK_OpportunityFeeDetail_FeeDetail)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<OpportunityFeeDetail> OpportunityFeeDetails { get; set; } // OpportunityFeeDetail.FK_OpportunityFeeDetail_FeeDetail

        // Foreign keys

        /// <summary>
        /// Parent Fee pointed by [FeeDetail].([FeeId]) (FK_FeeDetail_Fee)
        /// </summary>
        public virtual Fee Fee { get; set; } // FK_FeeDetail_Fee

        /// <summary>
        /// Parent Formula pointed by [FeeDetail].([FormulaId]) (FK_FeeDetail_Formula)
        /// </summary>
        public virtual Formula Formula { get; set; } // FK_FeeDetail_Formula

        /// <summary>
        /// Parent PaidBy pointed by [FeeDetail].([CreditedById]) (FK_FeeDetail_PaidBy)
        /// </summary>
        public virtual PaidBy PaidBy { get; set; } // FK_FeeDetail_PaidBy

        /// <summary>
        /// Parent RangeSet pointed by [FeeDetail].([RangeSetId]) (FK_FeeDetail_RangeSet)
        /// </summary>
        public virtual RangeSet RangeSet { get; set; } // FK_FeeDetail_RangeSet

        /// <summary>
        /// Parent Rule pointed by [FeeDetail].([RuleId]) (FK_FeeDetail_Rule)
        /// </summary>
        public virtual Rule Rule { get; set; } // FK_FeeDetail_Rule

        public FeeDetail()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 134;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            IsDisplayZero = false;
            FeeDetailPaidBies = new System.Collections.Generic.HashSet<FeeDetailPaidBy>();
            LoanApplicationFees = new System.Collections.Generic.HashSet<LoanApplicationFee>();
            OpportunityFeeDetails = new System.Collections.Generic.HashSet<OpportunityFeeDetail>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
