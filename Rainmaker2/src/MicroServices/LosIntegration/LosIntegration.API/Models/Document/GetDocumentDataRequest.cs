using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LosIntegration.API.Models.Document
{
    public class GetDocumentDataRequest
    {
        public int FileDataId { get; set; }
        public int DocumentId { get; set; }
    }
}
