
using LosIntegration.Model.Model.ServiceRequestModels.ByteWebConnector;
using LosIntegration.Model.Model.ServiceRequestModels.Document;
using LosIntegration.Model.Model.ServiceResponseModels;
using LosIntegration.Model.Model.ServiceResponseModels.ByteWebConnector;
using LosIntegration.Model.Model.ServiceResponseModels.Rainmaker;
using ServiceCallHelper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LosIntegration.Service.InternalServices
{
    public interface IByteWebConnectorService
    {


        Task<CallResponse<SendSdkDocumentResponse>> SendDocument(SendDocumentRequest sendDocumentRequest);
        Task<EmbeddedDoc> GetDocumentDataFromByte(GetDocumentDataRequest documentDataRequest);
        Task<UploadFileResponse> UploadFile(string uploadFileRequest);
        Task<CallResponse<ApiResponse<LoanFileInfo>>> SendLoanApplication(LoanApplication loanApplication,
                                                                          LoanRequest loanRequest,
                                                                          List<ThirdPartyCode> byteProCodeList);

        Task<CallResponse<ApiResponse<LoanFileInfo>>> SendLoanApplicationViaSDK(LoanApplication loanApplication,
                                                                                List<ThirdPartyCode> byteProCodeList);


        Task<CallResponse<ApiResponse<string>>> GetByteLoanStatusNameViaSDK(string byteFileName);
    }
}
