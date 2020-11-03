













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // BorrowerQuestionResponse

    public partial class BorrowerQuestionResponse 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? BorrowerId { get; set; } // BorrowerId
        public int? QuestionId { get; set; } // QuestionId
        public int? ResponseId { get; set; } // ResponseId
        public string Detail { get; set; } // Detail (length: 500)

        // Foreign keys

        /// <summary>
        /// Parent Borrower pointed by [BorrowerQuestionResponse].([BorrowerId]) (FK_BorrowerQuestionResponse_Borrower)
        /// </summary>
        public virtual Borrower Borrower { get; set; } // FK_BorrowerQuestionResponse_Borrower

        /// <summary>
        /// Parent Question pointed by [BorrowerQuestionResponse].([QuestionId]) (FK_BorrowerQuestionResponse_Question)
        /// </summary>
        public virtual Question Question { get; set; } // FK_BorrowerQuestionResponse_Question

        /// <summary>
        /// Parent QuestionResponse pointed by [BorrowerQuestionResponse].([ResponseId]) (FK_BorrowerQuestionResponse_QuestionResponse)
        /// </summary>
        public virtual QuestionResponse QuestionResponse { get; set; } // FK_BorrowerQuestionResponse_QuestionResponse

        public BorrowerQuestionResponse()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
