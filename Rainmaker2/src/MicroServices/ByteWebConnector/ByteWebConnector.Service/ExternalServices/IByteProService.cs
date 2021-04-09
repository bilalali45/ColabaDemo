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
        Task<string> GetByteProSession();


        ApiResponse<DocumentUploadResponse> SendDocumentToByte(DocumentUploadRequest documentUploadRequest,
                                                               string session);


        List<EmbeddedDoc> GetAllByteDocuments(string session,
                                              int fileDataId);


        Task<string> Send(string output,
                          string session);


        EmbeddedDoc GetEmbeddedDocData(string byteProSession,
                                       int documentId,
                                       int fileDataId);


        Task<bool> ValidateByteSessionAsync(string byteSession);


        Task<FileDataResponse> GetFileDataAsync(string byteSession,
                                string fileDataId);


        ByteFile GetByteLoanFile(string loanApplicationByteLoanNumber);
        ByteFile SendFile(LoanFileRequest loanFileRequest);
        Task<short> GetLoanStatusAsync(int fileDataId);
    }
}