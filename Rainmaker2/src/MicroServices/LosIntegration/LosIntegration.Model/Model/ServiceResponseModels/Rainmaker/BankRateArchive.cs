
















namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // BankRateArchive

    public partial class BankRateArchive 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? BankRequestId { get; set; } // BankRequestId
        public int? BankRateParameterId { get; set; } // BankRateParameterId
        public int? BankRatePriceRequestId { get; set; } // BankRatePriceRequestId
        public decimal? Rate { get; set; } // Rate
        public decimal? Apr { get; set; } // Apr
        public decimal? DiscountPoint { get; set; } // DiscountPoint
        public decimal? Charges { get; set; } // Charges
        public decimal? Price { get; set; } // Price
        public decimal? LenderFee { get; set; } // LenderFee
        public decimal? InitialRateCap { get; set; } // InitialRateCap
        public decimal? PeriodicRateCap { get; set; } // PeriodicRateCap
        public decimal? LifeTimeCap { get; set; } // LifeTimeCap
        public string BankRateName { get; set; } // BankRateName (length: 255)
        public int? InstanceId { get; set; } // InstanceId
        public string InstanceNo { get; set; } // InstanceNo (length: 255)
        public string InstanceName { get; set; } // InstanceName (length: 500)
        public int? PointNo { get; set; } // PointNo
        public int? TierNo { get; set; } // TierNo
        public int? ProductId { get; set; } // ProductId
        public string BankRateProductId { get; set; } // BankRateProductId (length: 50)
        public decimal? MinLoan { get; set; } // MinLoan
        public decimal? MaxLoan { get; set; } // MaxLoan
        public System.DateTime? DateUtc { get; set; } // DateUtc
        public System.DateTime? InsertDateTimeUtc { get; set; } // InsertDateTimeUtc
        public bool? IsZeroRate { get; set; } // IsZeroRate
        public int EntityTypeId { get; set; } // EntityTypeId

        // Foreign keys

        /// <summary>
        /// Parent BankRateParameter pointed by [BankRateArchive].([BankRateParameterId]) (FK_BankRateArchive_BankRateParameter)
        /// </summary>
        public virtual BankRateParameter BankRateParameter { get; set; } // FK_BankRateArchive_BankRateParameter

        /// <summary>
        /// Parent BankRatePricingRequest pointed by [BankRateArchive].([BankRatePriceRequestId]) (FK_BankRateArchive_BankRatePricingRequest)
        /// </summary>
        public virtual BankRatePricingRequest BankRatePricingRequest { get; set; } // FK_BankRateArchive_BankRatePricingRequest

        /// <summary>
        /// Parent BankRateRequest pointed by [BankRateArchive].([BankRequestId]) (FK_BankRateArchive_BankRateRequest)
        /// </summary>
        public virtual BankRateRequest BankRateRequest { get; set; } // FK_BankRateArchive_BankRateRequest

        public BankRateArchive()
        {
            EntityTypeId = 15;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
