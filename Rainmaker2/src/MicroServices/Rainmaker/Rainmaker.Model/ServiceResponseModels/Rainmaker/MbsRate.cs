using System;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class MbsRate
    {
        public int Id { get; set; }
        public DateTime UpdateOnUtc { get; set; }
        public int MbsSecurityId { get; set; }
        public decimal? CouponRate { get; set; }
        public DateTime? SettlementMonth { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public int EntityTypeId { get; set; }
        public decimal? BidBasis { get; set; }
        public string Bid32Nds { get; set; }
        public string TMaturity { get; set; }
        public decimal? TPrice { get; set; }
        public decimal? TPriceChange { get; set; }
        public decimal? TYield { get; set; }
        public decimal? TYieldChange { get; set; }

        public MbsSecurity MbsSecurity { get; set; }
    }
}