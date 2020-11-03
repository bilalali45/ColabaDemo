













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // OpportunityFeeDetail

    public partial class OpportunityFeeDetail 
    {
        public int Id { get; set; } // Id (Primary key)
        public int OpportunityId { get; set; } // OpportunityId
        public bool IsFeeOverride { get; set; } // IsFeeOverride
        public int FeeDetailId { get; set; } // FeeDetailId
        public decimal? Value { get; set; } // Value
        public System.DateTime StartDateUtc { get; set; } // StartDateUtc
        public System.DateTime? EndDateUtc { get; set; } // EndDateUtc
        public string Note { get; set; } // Note (length: 500)
        public int EntityTypeId { get; set; } // EntityTypeId
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public int? CreditedById { get; set; } // CreditedById

        // Reverse navigation

        /// <summary>
        /// Child OpportunityFeeDetailPaidBies where [OpportunityFeeDetailPaidBy].[OpportunityFeeDetailId] point to this entity (FK_OpportunityFeeDetailPaidBy_OpportunityFeeDetail)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<OpportunityFeeDetailPaidBy> OpportunityFeeDetailPaidBies { get; set; } // OpportunityFeeDetailPaidBy.FK_OpportunityFeeDetailPaidBy_OpportunityFeeDetail

        // Foreign keys

        /// <summary>
        /// Parent FeeDetail pointed by [OpportunityFeeDetail].([FeeDetailId]) (FK_OpportunityFeeDetail_FeeDetail)
        /// </summary>
        public virtual FeeDetail FeeDetail { get; set; } // FK_OpportunityFeeDetail_FeeDetail

        /// <summary>
        /// Parent Opportunity pointed by [OpportunityFeeDetail].([OpportunityId]) (FK_OpportunityFeeDetail_Opportunity)
        /// </summary>
        public virtual Opportunity Opportunity { get; set; } // FK_OpportunityFeeDetail_Opportunity

        /// <summary>
        /// Parent PaidBy pointed by [OpportunityFeeDetail].([CreditedById]) (FK_OpportunityFeeDetail_PaidBy)
        /// </summary>
        public virtual PaidBy PaidBy { get; set; } // FK_OpportunityFeeDetail_PaidBy

        public OpportunityFeeDetail()
        {
            EntityTypeId = 143;
            OpportunityFeeDetailPaidBies = new System.Collections.Generic.HashSet<OpportunityFeeDetailPaidBy>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
