using System;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class LoanDocument
    {
        public int Id { get; set; }
        public string ClientFileName { get; set; }
        public string ServerFileName { get; set; }
        public int? LoanApplicationId { get; set; }
        public int? LoanDocumentTypeId { get; set; }
        public int? LoanDocumentSubTypeId { get; set; }
        public int? LoanDocumentStatusId { get; set; }
        public string MessageForCustomer { get; set; }
        public int EntityTypeId { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }

        public LoanApplication LoanApplication { get; set; }

        public LoanDocumentStatusList LoanDocumentStatusList { get; set; }

        public LoanDocumentSubType LoanDocumentSubType { get; set; }

        public LoanDocumentType LoanDocumentType { get; set; }
    }
}