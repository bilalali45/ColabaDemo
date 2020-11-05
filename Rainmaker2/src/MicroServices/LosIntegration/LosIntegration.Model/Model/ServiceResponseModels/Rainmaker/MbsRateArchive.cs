













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // MbsRateArchive

    public partial class MbsRateArchive 
    {
        public int Id { get; set; } // Id (Primary key)
        public System.DateTime UpdateOnUtc { get; set; } // UpdateOnUtc
        public int MbsSecurityId { get; set; } // MbsSecurityId
        public decimal? CouponRate { get; set; } // CouponRate
        public System.DateTime? SettlementMonth { get; set; } // SettlementMonth
        public System.DateTime CreatedOnUtc { get; set; } // CreatedOnUtc
        public int EntityTypeId { get; set; } // EntityTypeId
        public decimal? BidBasis { get; set; } // BidBasis
        public string Bid32Nds { get; set; } // Bid32nds (length: 20)
        public string TMaturity { get; set; } // TMaturity (length: 20)
        public decimal? TPrice { get; set; } // TPrice
        public decimal? TPriceChange { get; set; } // TPriceChange
        public decimal? TYield { get; set; } // TYield
        public decimal? TYieldChange { get; set; } // TYieldChange

        // Foreign keys

        /// <summary>
        /// Parent MbsSecurity pointed by [MbsRateArchive].([MbsSecurityId]) (FK_MbsRateArchive_MbsSecurity)
        /// </summary>
        public virtual MbsSecurity MbsSecurity { get; set; } // FK_MbsRateArchive_MbsSecurity

        public MbsRateArchive()
        {
            EntityTypeId = 197;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>