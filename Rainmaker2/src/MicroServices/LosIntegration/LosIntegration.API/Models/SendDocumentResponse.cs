using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LosIntegration.API.Models
{
    public class SendDocumentResponse
    {
        
        public byte[] FileData { get; set; }
        public int LoanApplicationId { get; set; }
    }
}
