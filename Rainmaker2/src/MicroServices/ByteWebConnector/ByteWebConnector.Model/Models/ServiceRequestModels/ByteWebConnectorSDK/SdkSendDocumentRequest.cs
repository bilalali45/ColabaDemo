using ByteWebConnector.Model.Models.OwnModels.Settings;

namespace ByteWebConnector.Model.Models.ServiceRequestModels.ByteWebConnectorSDK
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