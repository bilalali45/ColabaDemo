using ByteWebConnector.API.Models.ClientModels;

namespace ByteWebConnector.API.Models
{
    public class ByteLoanInfo
    {
        public long LoanId { get; set; }

        public string LoanPurpose { get; set; }
        public string AmortizationType { get; set; }
        public string AmortizationTypeDescOv { get; set; }
        public string LoanProgramName { get; set; }
        public decimal? PurPrice { get; set; }
        public decimal? BaseLoan { get; set; }
        public double LoanWith { get; set; }
        public string MortgageType { get; set; }
        public decimal? SubFiBaseLoan { get; set; }
        public long FileDataId { get; set; }
        public double? RefinanceCashOutAmount { get; internal set; }
        public string LoanGuid { get; set; }


        public LoanInfoEntity GetRainmakerLoanInfo()
        {
            var loanInfoEntity = new LoanInfoEntity();
            loanInfoEntity.FileDataId = this.FileDataId;
            loanInfoEntity.AmortizationTypeId = GetRainMakerAmortizationId(this.AmortizationType);
            loanInfoEntity.MortgageTypeId = GetRainMakerMortgageId(this.MortgageType);
            loanInfoEntity.CashOutAmount = this.RefinanceCashOutAmount;
            loanInfoEntity.LoanAmount = this.BaseLoan;
            loanInfoEntity.PropertyValue = this.PurPrice;
            loanInfoEntity.LoanPurpose = GetRainMakerLoanPurposeId(this.LoanPurpose);
            loanInfoEntity.MortgageToBeSubordinate = this.SubFiBaseLoan;
            return loanInfoEntity;
        }


        private int? GetRainMakerLoanPurposeId(string loanPurpose)
        {
            throw new System.NotImplementedException();
        }


        private int? GetRainMakerMortgageId(string mortgageType)
        {
            throw new System.NotImplementedException();
        }


        private int? GetRainMakerAmortizationId(string amortizationType)
        {
            throw new System.NotImplementedException();
        }
    }
}
