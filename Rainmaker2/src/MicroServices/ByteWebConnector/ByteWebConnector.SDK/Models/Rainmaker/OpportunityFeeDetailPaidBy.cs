using System;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class OpportunityFeeDetailPaidBy
    {
        public int Id { get; set; }
        public int OpportunityFeeDetailId { get; set; }
        public int PaidById { get; set; }
        public decimal? PaymentPercent { get; set; }
        public int EntityTypeId { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; }

        public OpportunityFeeDetail OpportunityFeeDetail { get; set; }

        public PaidBy PaidBy { get; set; }
    }
}