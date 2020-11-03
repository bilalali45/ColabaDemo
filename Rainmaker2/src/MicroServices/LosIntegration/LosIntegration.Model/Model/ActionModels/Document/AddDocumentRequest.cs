using System.Collections.Generic;
using LosIntegration.Model.Model.ServiceRequestModels.Document;
using Microsoft.AspNetCore.Mvc;

namespace LosIntegration.Model.Model.ActionModels.Document
{
    public class AddDocumentRequest
    {
        [FromBody]
        public int FileDataId { get; set; }
        [FromBody]
        public List<EmbeddedDoc> EmbeddedDocs { get; set; }


      
    }
}