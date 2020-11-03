













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // RateArchive

    public partial class RateArchive 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? ProductId { get; set; } // ProductId
        public int? RateTypeId { get; set; } // RateTypeId
        public System.DateTime? PriceDateUtc { get; set; } // PriceDateUtc
        public int? InvestorId { get; set; } // InvestorId
        public decimal? WholesalePrice { get; set; } // WholesalePrice
        public decimal? WholesaleRate { get; set; } // WholesaleRate
        public decimal? Price { get; set; } // Price
        public decimal? Rate { get; set; } // Rate
        public decimal? Apr { get; set; } // Apr
        public decimal? LenderFees { get; set; } // LenderFees
        public decimal? DiscountCharges { get; set; } // DiscountCharges
        public decimal? ClosingCost { get; set; } // ClosingCost
        public decimal? Mi { get; set; } // Mi
        public decimal? Piti { get; set; } // Piti
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? ModifiedBy { get; set; } // ModifiedBy
        public int EntityTypeId { get; set; } // EntityTypeId
        public int? LoanRequestId { get; set; } // LoanRequestId
        public int? QuoteResultId { get; set; } // QuoteResultId
        public int? BusinessUnitId { get; set; } // BusinessUnitId
        public int? RateParameterId { get; set; } // RateParameterId

        // Foreign keys

        /// <summary>
        /// Parent BusinessUnit pointed by [RateArchive].([BusinessUnitId]) (FK_RateArchive_BusinessUnit)
        /// </summary>
        public virtual BusinessUnit BusinessUnit { get; set; } // FK_RateArchive_BusinessUnit

        /// <summary>
        /// Parent Investor pointed by [RateArchive].([InvestorId]) (FK_RateArchive_Investor)
        /// </summary>
        public virtual Investor Investor { get; set; } // FK_RateArchive_Investor

        /// <summary>
        /// Parent Product pointed by [RateArchive].([ProductId]) (FK_RateArchive_Product)
        /// </summary>
        public virtual Product Product { get; set; } // FK_RateArchive_Product

        /// <summary>
        /// Parent RateServiceParameter pointed by [RateArchive].([RateParameterId]) (FK_RateArchive_RateServiceParameter)
        /// </summary>
        public virtual RateServiceParameter RateServiceParameter { get; set; } // FK_RateArchive_RateServiceParameter

        public RateArchive()
        {
            EntityTypeId = 24;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
