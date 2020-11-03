using System;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class FeeDetailPaidBy
    {
        public int Id { get; set; }
        public int? FeeDetailId { get; set; }
        public int? PaidById { get; set; }
        public decimal? PaymentPercent { get; set; }
        public int EntityTypeId { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; }

        public FeeDetail FeeDetail { get; set; }

        public PaidBy PaidBy { get; set; }
    }
}