namespace ByteWebConnector.API.Models.ClientModels
{
    public class LoanInfoEntity
    {
        public long FileDataId { get; set; }
        public double? SubFiBaseLoan { get; set; }
        public int? LoanPurpose { get; set; }
        public int? AmortizationTypeId { get; set; }
        public int? MortgageTypeId { get; set; }
        public double? RefinanceCashOutAmount { get; set; }
        public double? BaseLoan { get; set; }
        public double? PurPrice { get; set; }
    }
}