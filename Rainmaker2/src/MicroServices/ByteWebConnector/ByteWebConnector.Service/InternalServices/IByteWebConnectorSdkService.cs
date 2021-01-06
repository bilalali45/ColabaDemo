using ByteWebConnector.Model.Models.ActionModels.LoanFile;
using ByteWebConnector.Model.Models.OwnModels;
using ByteWebConnector.Model.Models.ServiceRequestModels.ByteWebConnectorSDK;
using ByteWebConnector.Model.Models.ServiceResponseModels.ByteWebConnectorSDK;
using ServiceCallHelper;

namespace ByteWebConnector.Service.InternalServices
{
    public interface IByteWebConnectorSdkService
    {
        CallResponse<SendSdkDocumentResponse> SendDocumentToByte(DocumentUploadRequest documentUploadRequest);
        ApiResponse<SendLoanApplicationResponse> SendLoanApplicationToByteViaSDK(LoanFileRequest loanFileRequest);
        ApiResponse<string> GetLoanApplicationStatusNameViaSDK(string byteFileName);
    }
}