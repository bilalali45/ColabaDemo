using System;
using System.Collections.Generic;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class FeeDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? FeeId { get; set; }
        public int? RuleId { get; set; }
        public int? CalcBaseOnId { get; set; }
        public int RoundTypeId { get; set; }
        public int CalcTypeId { get; set; }
        public int? FormulaId { get; set; }
        public decimal Value { get; set; }
        public int? RangeSetId { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public int EntityTypeId { get; set; }
        public bool IsDefault { get; set; }
        public bool IsSystem { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public bool IsDeleted { get; set; }
        public int? VendorId { get; set; }
        public bool IsNegativeFee { get; set; }
        public bool IsIncludeInApr { get; set; }
        public bool IsIncludeInClosingCost { get; set; }
        public bool IsFinancedToLoan { get; set; }
        public bool IsOriginationFee { get; set; }
        public bool IsDisplayZero { get; set; }
        public int? CreditedById { get; set; }
        public int? CalcTypeIdPublished { get; set; }
        public int? FormulaIdPublished { get; set; }
        public decimal? ValuePublished { get; set; }
        public int? RangeSetIdPublished { get; set; }

        public ICollection<FeeDetailPaidBy> FeeDetailPaidBies { get; set; }

        public ICollection<LoanApplicationFee> LoanApplicationFees { get; set; }

        public ICollection<OpportunityFeeDetail> OpportunityFeeDetails { get; set; }

        public Fee Fee { get; set; }

        public Formula Formula { get; set; }

        public PaidBy PaidBy { get; set; }

        public RangeSet RangeSet { get; set; }

        public Rule Rule { get; set; }
    }
}