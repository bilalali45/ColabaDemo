using System.Collections.Generic;

namespace LosIntegration.API.Models.Document
{
    public class AddDocumentRequest
    {
        public int FileDataId { get; }
        public List<EmbeddedDoc> EmbeddedDocs { get; }


      
    }
}