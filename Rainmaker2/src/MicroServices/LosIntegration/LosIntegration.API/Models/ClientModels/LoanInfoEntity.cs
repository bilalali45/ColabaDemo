namespace LosIntegration.API.Models.ClientModels
{
    public class LoanInfoEntity
    {
        public long FileDataId { get; set; }
        public decimal? MortgageToBeSubordinate { get; set; }
        public int? LoanPurpose { get; set; }
        public int? AmortizationTypeId { get; set; }
        public int? MortgageTypeId { get; set; }
        public double? CashOutAmount { get; set; }
        public decimal? LoanAmount { get; set; }
        public decimal? PropertyValue { get; set; }
    }
}