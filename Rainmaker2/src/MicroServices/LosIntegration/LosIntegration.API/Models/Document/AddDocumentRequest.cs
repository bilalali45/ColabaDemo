using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace LosIntegration.API.Models.Document
{
    public class AddDocumentRequest
    {
        [FromBody]
        public int FileDataId { get; set; }
        [FromBody]
        public List<EmbeddedDoc> EmbeddedDocs { get; set; }


      
    }
}