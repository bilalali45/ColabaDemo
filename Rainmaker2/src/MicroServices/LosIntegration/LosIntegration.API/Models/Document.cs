using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LosIntegration.API.Models
{
    public class SendToExternalOriginatorRequest
    {
        public int LoanApplicationId { get; set; }
        public string DocumentLoanApplicationId { get; set; }
        public string RequestId { get; set; }
        public string DocumentId { get; set; }
        public string FileId { get; set; }

        
    }
}
