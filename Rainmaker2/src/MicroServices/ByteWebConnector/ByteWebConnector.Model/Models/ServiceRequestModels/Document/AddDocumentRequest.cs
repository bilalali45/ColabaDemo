using System.Collections.Generic;
using ByteWebConnector.Model.Models.ServiceResponseModels.ByteApi;
namespace ByteWebConnector.Model.Models.ServiceRequestModels.Document
{
    public class AddDocumentRequest
    {
        public int FileDataId { get; }
        public List<EmbeddedDoc> EmbeddedDocs { get; }


        public AddDocumentRequest(int fileDataId,
                                  List<EmbeddedDoc> embeddedDocs)
        {
            FileDataId = fileDataId;
            this.EmbeddedDocs = embeddedDocs;
        }
    }
}