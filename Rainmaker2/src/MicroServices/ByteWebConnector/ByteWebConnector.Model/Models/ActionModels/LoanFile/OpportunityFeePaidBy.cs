













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // OpportunityFeePaidBy

    public partial class OpportunityFeePaidBy 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? PaidById { get; set; } // PaidById
        public int? EntityTypeId { get; set; } // EntityTypeId
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public int? OpportunityFeeId { get; set; } // OpportunityFeeId
        public decimal? PaymentPercent { get; set; } // PaymentPercent

        // Foreign keys

        /// <summary>
        /// Parent OpportunityFee pointed by [OpportunityFeePaidBy].([OpportunityFeeId]) (FK_OpportunityFeePaidBy_OpportunityFee)
        /// </summary>
        public virtual OpportunityFee OpportunityFee { get; set; } // FK_OpportunityFeePaidBy_OpportunityFee

        /// <summary>
        /// Parent PaidBy pointed by [OpportunityFeePaidBy].([PaidById]) (FK_OpportunityFeePaidBy_PaidBy)
        /// </summary>
        public virtual PaidBy PaidBy { get; set; } // FK_OpportunityFeePaidBy_PaidBy

        public OpportunityFeePaidBy()
        {
            EntityTypeId = 122;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
