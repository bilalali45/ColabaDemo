using System.Collections.Generic;
using ByteWebConnector.API.Models.Document;

namespace ByteWebConnector.API.Models.ClientModels.Document
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