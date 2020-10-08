using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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