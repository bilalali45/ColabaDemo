













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // LoanDocumentPipeLine

    public partial class LoanDocumentPipeLine 
    {
        public int LoanApplicationId { get; set; } // LoanApplicationId (Primary key)
        public System.DateTime? DocumentUploadDateUtc { get; set; } // DocumentUploadDateUtc
        public System.DateTime? DocumentRequestSentDateUtc { get; set; } // DocumentRequestSentDateUtc
        public int? DocumentRemaining { get; set; } // DocumentRemaining
        public int? DocumentOutstanding { get; set; } // DocumentOutstanding
        public int? DocumentCompleted { get; set; } // DocumentCompleted
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc

        // Foreign keys

        /// <summary>
        /// Parent LoanApplication pointed by [LoanDocumentPipeLine].([LoanApplicationId]) (FK_LoanDocumentPipeLine_LoanApplication)
        /// </summary>
        public virtual LoanApplication LoanApplication { get; set; } // FK_LoanDocumentPipeLine_LoanApplication

        public LoanDocumentPipeLine()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
