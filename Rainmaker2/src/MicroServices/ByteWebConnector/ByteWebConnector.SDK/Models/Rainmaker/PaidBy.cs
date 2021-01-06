using System;
using System.Collections.Generic;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class PaidBy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public int EntityTypeId { get; set; }
        public bool IsDefault { get; set; }
        public bool IsSystem { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? ModifiedBy { get; set; }
        public string TpId { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<FeeDetail> FeeDetails { get; set; }

        public ICollection<FeeDetailPaidBy> FeeDetailPaidBies { get; set; }

        public ICollection<LoanApplicationFee> LoanApplicationFees { get; set; }

        public ICollection<OpportunityFee> OpportunityFees { get; set; }

        public ICollection<OpportunityFeeDetail> OpportunityFeeDetails { get; set; }

        public ICollection<OpportunityFeeDetailPaidBy> OpportunityFeeDetailPaidBies { get; set; }

        public ICollection<OpportunityFeePaidBy> OpportunityFeePaidBies { get; set; }

        public ICollection<OpportunityPropertyTax> OpportunityPropertyTaxes { get; set; }

        public ICollection<PropertyTax> PropertyTaxes { get; set; }

        public ICollection<PropertyTaxEscrow> PropertyTaxEscrows { get; set; }
    }
}