













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // LoanRequestQuestionBinding

    public partial class LoanRequestQuestionBinding 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? LoanRequestId { get; set; } // LoanRequestId
        public int? QuestionId { get; set; } // QuestionId
        public int? ResponseId { get; set; } // ResponseId

        // Foreign keys

        /// <summary>
        /// Parent LoanRequest pointed by [LoanRequestQuestionBinding].([LoanRequestId]) (FK_LoanRequestQuestionBinding_LoanRequest)
        /// </summary>
        public virtual LoanRequest LoanRequest { get; set; } // FK_LoanRequestQuestionBinding_LoanRequest

        /// <summary>
        /// Parent Question pointed by [LoanRequestQuestionBinding].([QuestionId]) (FK_LoanRequestQuestionBinding_Question)
        /// </summary>
        public virtual Question Question { get; set; } // FK_LoanRequestQuestionBinding_Question

        /// <summary>
        /// Parent QuestionResponse pointed by [LoanRequestQuestionBinding].([ResponseId]) (FK_LoanRequestQuestionBinding_QuestionResponse)
        /// </summary>
        public virtual QuestionResponse QuestionResponse { get; set; } // FK_LoanRequestQuestionBinding_QuestionResponse

        public LoanRequestQuestionBinding()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
