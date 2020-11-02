using System;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class LoanDocumentPipeLine
    {
        public int LoanApplicationId { get; set; }
        public DateTime? DocumentUploadDateUtc { get; set; }
        public DateTime? DocumentRequestSentDateUtc { get; set; }
        public int? DocumentRemaining { get; set; }
        public int? DocumentOutstanding { get; set; }
        public int? DocumentCompleted { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }

        public LoanApplication LoanApplication { get; set; }
    }
}