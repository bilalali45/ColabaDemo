using System.Collections.Generic;
using System.Threading.Tasks;
using ByteWebConnector.Model.Models;
using ByteWebConnector.Model.Models.Document;
using ByteWebConnector.Model.Models.ServiceRequestModels.BytePro;

namespace ByteWebConnector.Service.ExternalServices
{
    public interface IByteProService
    {
        string GetByteProSession();


        ApiResponse SendDocumentToByte(DocumentUploadRequest documentUploadRequest,
                                       string session);


        List<EmbeddedDoc> GetAllByteDocuments(string session,
                                              int fileDataId);


        Task<string> Send(string output,
                          string session);


        EmbeddedDoc GetEmbeddedDocData(string byteProSession,
                                       int documentId,
                                       int fileDataId);
    }
}