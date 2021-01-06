using System.Collections.Generic;
using System.Threading.Tasks;
using ByteWebConnector.Model.Models;
using ByteWebConnector.Model.Models.ActionModels.LoanFile;
using ByteWebConnector.Model.Models.OwnModels;
using ByteWebConnector.Model.Models.ServiceRequestModels.ByteApi;
using ByteWebConnector.Model.Models.ServiceRequestModels.Los;
using ByteWebConnector.Model.Models.ServiceResponseModels.ByteApi;

namespace ByteWebConnector.Service.ExternalServices
{
    public interface IByteProService
    {
        string GetByteProSession();


        ApiResponse<DocumentUploadResponse> SendDocumentToByte(DocumentUploadRequest documentUploadRequest,
                                                               string session);


        List<EmbeddedDoc> GetAllByteDocuments(string session,
                                              int fileDataId);


        Task<string> Send(string output,
                          string session);


        EmbeddedDoc GetEmbeddedDocData(string byteProSession,
                                       int documentId,
                                       int fileDataId);


        bool ValidateByteSession(string byteSession);


        FileDataResponse GetFileData(string byteSession,
                                     string fileDataId);


        ByteFile GetByteLoanFile(string loanApplicationByteLoanNumber);
        ByteFile SendFile(LoanFileRequest loanFileRequest);
        Task<short> GetLoanStatusAsync(int fileDataId);
    }
}