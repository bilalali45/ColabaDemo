













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // MbsRate

    public partial class MbsRate 
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
        /// Parent MbsSecurity pointed by [MbsRate].([MbsSecurityId]) (FK_MbsRate_MbsSecurity)
        /// </summary>
        public virtual MbsSecurity MbsSecurity { get; set; } // FK_MbsRate_MbsSecurity

        public MbsRate()
        {
            EntityTypeId = 198;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
