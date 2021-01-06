using System.Collections.Generic;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class QuestionResponse
    {
        public int Id { get; set; }
        public int? SelectedOptionId { get; set; }
        public string AnswerText { get; set; }
        public string Description { get; set; }

        //public System.Collections.Generic.ICollection<BorrowerQuestionResponse> BorrowerQuestionResponses { get; set; }

        public ICollection<LoanRequestQuestionBinding> LoanRequestQuestionBindings { get; set; }

        public QuestionOption QuestionOption { get; set; }

    }
}