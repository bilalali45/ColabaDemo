namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class LoanRequestMessageBinder
    {
        public int LoanRequestId { get; set; }
        public int MessageOnRuleId { get; set; }

        public LoanRequest LoanRequest { get; set; }

        public MessageOnRule MessageOnRule { get; set; }
    }
}