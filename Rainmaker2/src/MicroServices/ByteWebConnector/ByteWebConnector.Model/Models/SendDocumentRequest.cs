namespace ByteWebConnector.Model.Models
{
    public class SendDocumentRequest
    {
        public int LoanApplicationId { get; set; }
        public byte[] FileData { get; set; }
        public string DocumentCategory { get; set; }
        public string DocumentExension { get; set; }
        public string DocumentName { get; set; }
        public string DocumentStatus { get; set; }
        public string DocumentType { get; set; }
        public string MediaType { get; set; }
    }
}
