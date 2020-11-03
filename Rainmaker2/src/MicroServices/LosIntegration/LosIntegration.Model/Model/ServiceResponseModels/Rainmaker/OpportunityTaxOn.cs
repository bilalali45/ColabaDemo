













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // OpportunityTaxOn

    public partial class OpportunityTaxOn 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? Month { get; set; } // Month
        public int? OpportunityTaxId { get; set; } // OpportunityTaxId
        public decimal? Amount { get; set; } // Amount
        public decimal? TotalAmountPercent { get; set; } // TotalAmountPercent

        // Foreign keys

        /// <summary>
        /// Parent OpportunityPropertyTax pointed by [OpportunityTaxOn].([OpportunityTaxId]) (FK_OpportunityTaxOn_OpportunityPropertyTax)
        /// </summary>
        public virtual OpportunityPropertyTax OpportunityPropertyTax { get; set; } // FK_OpportunityTaxOn_OpportunityPropertyTax

        public OpportunityTaxOn()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
