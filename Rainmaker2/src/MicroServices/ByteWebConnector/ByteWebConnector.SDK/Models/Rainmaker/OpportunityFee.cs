using System;
using System.Collections.Generic;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class OpportunityFee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int FeeBlockId { get; set; }
        public int? FeeId { get; set; }
        public int OpportunityId { get; set; }
        public bool IsNegativeFee { get; set; }
        public bool IsIncludeInApr { get; set; }
        public bool IsIncludeInClosingCost { get; set; }
        public bool IsFinancedToLoan { get; set; }
        public bool IsOriginationFee { get; set; }
        public decimal Value { get; set; }
        public int? FeeTypeId { get; set; }
        public string FeeNumber { get; set; }
        public DateTime StartDateUtc { get; set; }
        public DateTime? EndDateUtc { get; set; }
        public string Note { get; set; }
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
        public int? CreditedById { get; set; }

        public ICollection<LoanApplicationFee> LoanApplicationFees { get; set; }

        public ICollection<OpportunityFeePaidBy> OpportunityFeePaidBies { get; set; }

        public Fee Fee { get; set; }

        public FeeBlock FeeBlock { get; set; }

        public FeeType FeeType { get; set; }

        public Opportunity Opportunity { get; set; }

        public PaidBy PaidBy { get; set; }
    }
}