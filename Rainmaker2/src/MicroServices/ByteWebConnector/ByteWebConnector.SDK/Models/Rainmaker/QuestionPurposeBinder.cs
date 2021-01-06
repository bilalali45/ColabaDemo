namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class QuestionPurposeBinder
    {
        public int QuestionId { get; set; }
        public int LoanPurposeId { get; set; }

        public LoanPurpose LoanPurpose { get; set; }

        public Question Question { get; set; }
    }
}