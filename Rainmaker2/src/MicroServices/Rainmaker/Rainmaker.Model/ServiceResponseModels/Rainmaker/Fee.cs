using System;
using System.Collections.Generic;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class Fee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Number { get; set; }
        public int? FeeTypeId { get; set; }
        public int? FeeBlockId { get; set; }
        public bool IsActive { get; set; }
        public int EntityTypeId { get; set; }
        public bool IsDefault { get; set; }
        public bool IsSystem { get; set; }
        public int DisplayOrder { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public string TpId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsDisplayZero { get; set; }
        public int? FeeCategoryId { get; set; }

        public ICollection<FeeDetail> FeeDetails { get; set; }

        public ICollection<LoanApplicationFee> LoanApplicationFees { get; set; }

        public ICollection<OpportunityFee> OpportunityFees { get; set; }

        //public System.Collections.Generic.ICollection<TemplateFormPlotDetail> TemplateFormPlotDetails { get; set; }

        public FeeBlock FeeBlock { get; set; }

        public FeeCategory FeeCategory { get; set; }

        public FeeType FeeType { get; set; }
    }
}