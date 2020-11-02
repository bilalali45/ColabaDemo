namespace ByteWebConnector.Model.Models.ServiceRequestModels.ByteApi
{
    public class DocumentUploadRequest
    {

        public long FileDataId { get; set; }
        public string DocumentName { get; set; }
        public string DocumentType { get; set; }
        public string DocumentCategory { get; set; }
        public string DocumentStatus { get; set; }
        public string DocumentExension { get; set; }
        public string DocumentData { get; set; }
    }

    
}
