using System;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class LoanDocumentPipeLineView
    {
        public int Id { get; set; }
        public int? OpportunityId { get; set; }
        public int? StatusId { get; set; }
        public string ByteLoanNumber { get; set; }
        public string ApplicationStatus { get; set; }
        public string CustomerName { get; set; }
        public int? BusinessUnitId { get; set; }
        public DateTime? DocumentUploadDateUtc { get; set; }
        public DateTime? DocumentRequestSentDateUtc { get; set; }
        public int? DocumentRemaining { get; set; }
        public int? DocumentOutstanding { get; set; }
        public int? DocumentCompleted { get; set; }
    }
}