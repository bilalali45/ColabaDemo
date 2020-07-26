namespace ByteWebConnector.API.Models
{
    public class Loan
    {
        public long LoanID { get; set; }

        public int LoanPurpose { get; set; }
        public int AmortizationType { get; set; }
        public string AmortizationTypeDescOV { get; set; }
        public string LoanProgramName { get; set; }
        public object PurPrice { get; set; }
        public object BaseLoan { get; set; }
        public double LoanWith { get; set; }
        public int MortgageType { get; set; }
        public object SubFiBaseLoan { get; set; }
        public long FileDataID { get; set; }
        public double? RefinanceCashOutAmount { get; internal set; }
        public string LoanGUID { get; set; }
    }
}
