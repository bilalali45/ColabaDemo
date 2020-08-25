using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LosIntegration.API.Models
{
    public class SendDocumentRequest
    {
        
        public byte[] FileData { get; set; }
        public int LoanApplicationId { get; set; }
     
        public string DocumentCategory { get; set; }
        public string DocumentExension { get; set; }
        public string DocumentName { get; set; }
        public string DocumentStatus { get; set; }
        public string DocumentType { get; set; }
        public string MediaType { get; set; }
    }
}
