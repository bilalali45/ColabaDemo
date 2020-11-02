using ByteWebConnector.Model.Models.ServiceRequestModels.ByteWebConnectorSDK;
using ByteWebConnector.Model.Models.ServiceResponseModels.ByteWebConnectorSDK;
using ServiceCallHelper;

namespace ByteWebConnector.Service.InternalServices
{
    public interface IByteWebConnectorSdkService
    {
        CallResponse<SendSdkDocumentResponse> SendDocumentToByte(DocumentUploadRequest documentUploadRequest);
    }
}