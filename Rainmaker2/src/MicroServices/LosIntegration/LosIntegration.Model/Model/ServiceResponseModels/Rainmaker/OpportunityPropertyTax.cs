













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // OpportunityPropertyTax

    public partial class OpportunityPropertyTax 
    {
        public int Id { get; set; } // Id (Primary key)
        public int PaidById { get; set; } // PaidById
        public int OpportunityId { get; set; } // OpportunityId
        public int? LoanRequestId { get; set; } // LoanRequestId
        public decimal Value { get; set; } // Value
        public int? EscrowMonth { get; set; } // EscrowMonth
        public int? EscrowEntityTypeId { get; set; } // EscrowEntityTypeId
        public int? PrePaidMonth { get; set; } // PrePaidMonth
        public decimal? PrePaid { get; set; } // PrePaid

        // Reverse navigation

        /// <summary>
        /// Child OpportunityTaxOns where [OpportunityTaxOn].[OpportunityTaxId] point to this entity (FK_OpportunityTaxOn_OpportunityPropertyTax)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<OpportunityTaxOn> OpportunityTaxOns { get; set; } // OpportunityTaxOn.FK_OpportunityTaxOn_OpportunityPropertyTax

        // Foreign keys

        /// <summary>
        /// Parent EscrowEntityType pointed by [OpportunityPropertyTax].([EscrowEntityTypeId]) (FK_OpportunityPropertyTax_EscrowEntityType)
        /// </summary>
        public virtual EscrowEntityType EscrowEntityType { get; set; } // FK_OpportunityPropertyTax_EscrowEntityType

        /// <summary>
        /// Parent LoanRequest pointed by [OpportunityPropertyTax].([LoanRequestId]) (FK_OpportunityPropertyTax_LoanRequest)
        /// </summary>
        public virtual LoanRequest LoanRequest { get; set; } // FK_OpportunityPropertyTax_LoanRequest

        /// <summary>
        /// Parent Opportunity pointed by [OpportunityPropertyTax].([OpportunityId]) (FK_OpportunityPropertyTax_Opportunity)
        /// </summary>
        public virtual Opportunity Opportunity { get; set; } // FK_OpportunityPropertyTax_Opportunity

        /// <summary>
        /// Parent PaidBy pointed by [OpportunityPropertyTax].([PaidById]) (FK_OpportunityPropertyTax_PaidBy)
        /// </summary>
        public virtual PaidBy PaidBy { get; set; } // FK_OpportunityPropertyTax_PaidBy

        public OpportunityPropertyTax()
        {
            OpportunityTaxOns = new System.Collections.Generic.HashSet<OpportunityTaxOn>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
