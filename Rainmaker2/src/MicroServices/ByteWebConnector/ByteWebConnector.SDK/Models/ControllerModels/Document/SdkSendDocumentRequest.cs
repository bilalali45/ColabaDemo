namespace ByteWebConnector.SDK.Models
{
    public class SdkSendDocumentRequest
    {
        public ByteProSettings ByteProSettings { get; }
        public DocumentUploadRequest DocumentUploadRequest { get; }


        public SdkSendDocumentRequest(ByteProSettings byteProSettings,
                                      DocumentUploadRequest documentUploadRequest)
        {
            this.ByteProSettings = byteProSettings;
            this.DocumentUploadRequest = documentUploadRequest;
        }
    }
}