namespace LosIntegration.Model.Model.ServiceRequestModels.ByteWebConnector
{
    public class UploadFileRequest
    {
        public int LoanApplicationId { get; set; }
        public string DocumentType { get; set; }
        public string FileName { get; set; }
        public string FileData { get; set; }
    }
}