













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // QuestionPurposeBinder

    public partial class QuestionPurposeBinder 
    {
        public int QuestionId { get; set; } // QuestionId (Primary key)
        public int LoanPurposeId { get; set; } // LoanPurposeId (Primary key)

        // Foreign keys

        /// <summary>
        /// Parent LoanPurpose pointed by [QuestionPurposeBinder].([LoanPurposeId]) (FK_QuestionPurposeBinder_LoanPurpose)
        /// </summary>
        public virtual LoanPurpose LoanPurpose { get; set; } // FK_QuestionPurposeBinder_LoanPurpose

        /// <summary>
        /// Parent Question pointed by [QuestionPurposeBinder].([QuestionId]) (FK_QuestionPurposeBinder_Question)
        /// </summary>
        public virtual Question Question { get; set; } // FK_QuestionPurposeBinder_Question

        public QuestionPurposeBinder()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
