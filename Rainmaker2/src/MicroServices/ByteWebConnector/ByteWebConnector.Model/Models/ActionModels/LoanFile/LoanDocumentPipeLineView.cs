













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // LoanDocumentPipeLineView

    public partial class LoanDocumentPipeLineView 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? OpportunityId { get; set; } // OpportunityId
        public int? StatusId { get; set; } // StatusId
        public string ByteLoanNumber { get; set; } // ByteLoanNumber (length: 50)
        public string ApplicationStatus { get; set; } // ApplicationStatus (Primary key) (length: 150)
        public string CustomerName { get; set; } // CustomerName (Primary key) (length: 601)
        public int? BusinessUnitId { get; set; } // BusinessUnitId
        public System.DateTime? DocumentUploadDateUtc { get; set; } // DocumentUploadDateUtc
        public System.DateTime? DocumentRequestSentDateUtc { get; set; } // DocumentRequestSentDateUtc
        public int? DocumentRemaining { get; set; } // DocumentRemaining
        public int? DocumentOutstanding { get; set; } // DocumentOutstanding
        public int? DocumentCompleted { get; set; } // DocumentCompleted

        public LoanDocumentPipeLineView()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
