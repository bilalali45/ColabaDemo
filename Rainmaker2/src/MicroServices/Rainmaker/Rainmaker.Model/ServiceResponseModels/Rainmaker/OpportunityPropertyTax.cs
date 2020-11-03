using System.Collections.Generic;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class OpportunityPropertyTax
    {
        public int Id { get; set; }
        public int PaidById { get; set; }
        public int OpportunityId { get; set; }
        public int? LoanRequestId { get; set; }
        public decimal Value { get; set; }
        public int? EscrowMonth { get; set; }
        public int? EscrowEntityTypeId { get; set; }
        public int? PrePaidMonth { get; set; }
        public decimal? PrePaid { get; set; }

        public ICollection<OpportunityTaxOn> OpportunityTaxOns { get; set; }

        public EscrowEntityType EscrowEntityType { get; set; }

        public LoanRequest LoanRequest { get; set; }

        public Opportunity Opportunity { get; set; }

        public PaidBy PaidBy { get; set; }
    }
}