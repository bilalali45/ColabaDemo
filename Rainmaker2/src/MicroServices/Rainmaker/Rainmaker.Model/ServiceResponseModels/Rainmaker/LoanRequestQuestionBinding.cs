namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class LoanRequestQuestionBinding
    {
        public int Id { get; set; }
        public int? LoanRequestId { get; set; }
        public int? QuestionId { get; set; }
        public int? ResponseId { get; set; }

        public LoanRequest LoanRequest { get; set; }

        public Question Question { get; set; }

        public QuestionResponse QuestionResponse { get; set; }
    }
}