using System;
using System.Collections.Generic;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class OpportunityFeeDetail
    {
        public int Id { get; set; }
        public int OpportunityId { get; set; }
        public bool IsFeeOverride { get; set; }
        public int FeeDetailId { get; set; }
        public decimal? Value { get; set; }
        public DateTime StartDateUtc { get; set; }
        public DateTime? EndDateUtc { get; set; }
        public string Note { get; set; }
        public int EntityTypeId { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public int? CreditedById { get; set; }

        public ICollection<OpportunityFeeDetailPaidBy> OpportunityFeeDetailPaidBies { get; set; }

        public FeeDetail FeeDetail { get; set; }

        public Opportunity Opportunity { get; set; }

        public PaidBy PaidBy { get; set; }
    }
}