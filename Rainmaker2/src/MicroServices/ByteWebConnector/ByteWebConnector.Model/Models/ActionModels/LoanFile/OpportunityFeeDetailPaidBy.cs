













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // OpportunityFeeDetailPaidBy

    public partial class OpportunityFeeDetailPaidBy 
    {
        public int Id { get; set; } // Id (Primary key)
        public int OpportunityFeeDetailId { get; set; } // OpportunityFeeDetailId
        public int PaidById { get; set; } // PaidById
        public decimal? PaymentPercent { get; set; } // PaymentPercent
        public int EntityTypeId { get; set; } // EntityTypeId
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc

        // Foreign keys

        /// <summary>
        /// Parent OpportunityFeeDetail pointed by [OpportunityFeeDetailPaidBy].([OpportunityFeeDetailId]) (FK_OpportunityFeeDetailPaidBy_OpportunityFeeDetail)
        /// </summary>
        public virtual OpportunityFeeDetail OpportunityFeeDetail { get; set; } // FK_OpportunityFeeDetailPaidBy_OpportunityFeeDetail

        /// <summary>
        /// Parent PaidBy pointed by [OpportunityFeeDetailPaidBy].([PaidById]) (FK_OpportunityFeeDetailPaidBy_PaidBy)
        /// </summary>
        public virtual PaidBy PaidBy { get; set; } // FK_OpportunityFeeDetailPaidBy_PaidBy

        public OpportunityFeeDetailPaidBy()
        {
            EntityTypeId = 118;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
