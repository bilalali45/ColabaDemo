namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class OpportunityTaxOn
    {
        public int Id { get; set; }
        public int? Month { get; set; }
        public int? OpportunityTaxId { get; set; }
        public decimal? Amount { get; set; }
        public decimal? TotalAmountPercent { get; set; }

        public OpportunityPropertyTax OpportunityPropertyTax { get; set; }
    }
}