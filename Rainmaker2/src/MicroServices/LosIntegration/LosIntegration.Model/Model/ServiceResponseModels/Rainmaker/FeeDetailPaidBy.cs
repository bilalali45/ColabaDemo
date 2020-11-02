













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // FeeDetailPaidBy

    public partial class FeeDetailPaidBy 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? FeeDetailId { get; set; } // FeeDetailId
        public int? PaidById { get; set; } // PaidById
        public decimal? PaymentPercent { get; set; } // PaymentPercent
        public int EntityTypeId { get; set; } // EntityTypeId
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc

        // Foreign keys

        /// <summary>
        /// Parent FeeDetail pointed by [FeeDetailPaidBy].([FeeDetailId]) (FK_FeeDetailPaidBy_FeeDetail)
        /// </summary>
        public virtual FeeDetail FeeDetail { get; set; } // FK_FeeDetailPaidBy_FeeDetail

        /// <summary>
        /// Parent PaidBy pointed by [FeeDetailPaidBy].([PaidById]) (FK_FeeDetailPaidBy_PaidBy)
        /// </summary>
        public virtual PaidBy PaidBy { get; set; } // FK_FeeDetailPaidBy_PaidBy

        public FeeDetailPaidBy()
        {
            EntityTypeId = 34;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
