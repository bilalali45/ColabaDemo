













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // PaidBy

    public partial class PaidBy 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
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

        // Reverse navigation

        /// <summary>
        /// Child FeeDetails where [FeeDetail].[CreditedById] point to this entity (FK_FeeDetail_PaidBy)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<FeeDetail> FeeDetails { get; set; } // FeeDetail.FK_FeeDetail_PaidBy
        /// <summary>
        /// Child FeeDetailPaidBies where [FeeDetailPaidBy].[PaidById] point to this entity (FK_FeeDetailPaidBy_PaidBy)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<FeeDetailPaidBy> FeeDetailPaidBies { get; set; } // FeeDetailPaidBy.FK_FeeDetailPaidBy_PaidBy
        /// <summary>
        /// Child LoanApplicationFees where [LoanApplicationFee].[PaidById] point to this entity (FK_LoanApplicationFee_PaidBy)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanApplicationFee> LoanApplicationFees { get; set; } // LoanApplicationFee.FK_LoanApplicationFee_PaidBy
        /// <summary>
        /// Child OpportunityFees where [OpportunityFee].[CreditedById] point to this entity (FK_OpportunityFee_PaidBy)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<OpportunityFee> OpportunityFees { get; set; } // OpportunityFee.FK_OpportunityFee_PaidBy
        /// <summary>
        /// Child OpportunityFeeDetails where [OpportunityFeeDetail].[CreditedById] point to this entity (FK_OpportunityFeeDetail_PaidBy)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<OpportunityFeeDetail> OpportunityFeeDetails { get; set; } // OpportunityFeeDetail.FK_OpportunityFeeDetail_PaidBy
        /// <summary>
        /// Child OpportunityFeeDetailPaidBies where [OpportunityFeeDetailPaidBy].[PaidById] point to this entity (FK_OpportunityFeeDetailPaidBy_PaidBy)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<OpportunityFeeDetailPaidBy> OpportunityFeeDetailPaidBies { get; set; } // OpportunityFeeDetailPaidBy.FK_OpportunityFeeDetailPaidBy_PaidBy
        /// <summary>
        /// Child OpportunityFeePaidBies where [OpportunityFeePaidBy].[PaidById] point to this entity (FK_OpportunityFeePaidBy_PaidBy)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<OpportunityFeePaidBy> OpportunityFeePaidBies { get; set; } // OpportunityFeePaidBy.FK_OpportunityFeePaidBy_PaidBy
        /// <summary>
        /// Child OpportunityPropertyTaxes where [OpportunityPropertyTax].[PaidById] point to this entity (FK_OpportunityPropertyTax_PaidBy)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<OpportunityPropertyTax> OpportunityPropertyTaxes { get; set; } // OpportunityPropertyTax.FK_OpportunityPropertyTax_PaidBy
        /// <summary>
        /// Child PropertyTaxes where [PropertyTax].[PaidById] point to this entity (FK_PropertyTax_PaidBy)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<PropertyTax> PropertyTaxes { get; set; } // PropertyTax.FK_PropertyTax_PaidBy
        /// <summary>
        /// Child PropertyTaxEscrows where [PropertyTaxEscrow].[PaidById] point to this entity (FK_PropertyTaxEscrow_PaidBy)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<PropertyTaxEscrow> PropertyTaxEscrows { get; set; } // PropertyTaxEscrow.FK_PropertyTaxEscrow_PaidBy

        public PaidBy()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 12;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            FeeDetails = new System.Collections.Generic.HashSet<FeeDetail>();
            FeeDetailPaidBies = new System.Collections.Generic.HashSet<FeeDetailPaidBy>();
            LoanApplicationFees = new System.Collections.Generic.HashSet<LoanApplicationFee>();
            OpportunityFees = new System.Collections.Generic.HashSet<OpportunityFee>();
            OpportunityFeeDetails = new System.Collections.Generic.HashSet<OpportunityFeeDetail>();
            OpportunityFeeDetailPaidBies = new System.Collections.Generic.HashSet<OpportunityFeeDetailPaidBy>();
            OpportunityFeePaidBies = new System.Collections.Generic.HashSet<OpportunityFeePaidBy>();
            OpportunityPropertyTaxes = new System.Collections.Generic.HashSet<OpportunityPropertyTax>();
            PropertyTaxes = new System.Collections.Generic.HashSet<PropertyTax>();
            PropertyTaxEscrows = new System.Collections.Generic.HashSet<PropertyTaxEscrow>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
