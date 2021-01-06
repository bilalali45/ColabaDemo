namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class BorrowerQuestionResponse
    {
        public int Id { get; set; }
        public int? BorrowerId { get; set; }
        public int? QuestionId { get; set; }
        public int? ResponseId { get; set; }
        public string Detail { get; set; }

        //public Borrower Borrower { get; set; }

        public Question Question { get; set; }

        public QuestionResponse QuestionResponse { get; set; }

    }
}