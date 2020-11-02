using System;
using System.Collections.Generic;
using System.Text;

namespace LosIntegration.Model.Model.ServiceRequestModels.RainMaker
{
    public class UpdateLoanApplicationRequest
    {
        public int Id { get; set; }
        public DateTime BytePostDateUtc { get; set; }
        public string ByteLoanNumber { get; set; }
        public string ByteFileName { get; set; }
    }
}
