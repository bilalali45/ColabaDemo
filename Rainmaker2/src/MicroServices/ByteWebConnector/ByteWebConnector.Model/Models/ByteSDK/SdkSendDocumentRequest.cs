using ByteWebConnector.Model.Models.ByteApi;
using ByteWebConnector.Model.Models.Settings;

namespace ByteWebConnector.Model.Models.ByteSDK
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