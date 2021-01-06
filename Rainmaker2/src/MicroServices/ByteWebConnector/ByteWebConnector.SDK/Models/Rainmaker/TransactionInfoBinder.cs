namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class TransactionInfoBinder
    {
        public int LoanApplicationId { get; set; }
        public int TransactionInfoId { get; set; }

        public LoanApplication LoanApplication { get; set; }

        public TransactionInfo TransactionInfo { get; set; }
    }
}