using System;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class RateArchive
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public int? RateTypeId { get; set; }
        public DateTime? PriceDateUtc { get; set; }
        public int? InvestorId { get; set; }
        public decimal? WholesalePrice { get; set; }
        public decimal? WholesaleRate { get; set; }
        public decimal? Price { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Apr { get; set; }
        public decimal? LenderFees { get; set; }
        public decimal? DiscountCharges { get; set; }
        public decimal? ClosingCost { get; set; }
        public decimal? Mi { get; set; }
        public decimal? Piti { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? ModifiedBy { get; set; }
        public int EntityTypeId { get; set; }

        public int? LoanRequestId { get; set; }

        //        public int? QuoteResultId { get; set; }
        public int? BusinessUnitId { get; set; }
        public int? RateParameterId { get; set; }

        public BusinessUnit BusinessUnit { get; set; }

        public Investor Investor { get; set; }

        public Product Product { get; set; }

        public RateServiceParameter RateServiceParameter { get; set; }
    }
}