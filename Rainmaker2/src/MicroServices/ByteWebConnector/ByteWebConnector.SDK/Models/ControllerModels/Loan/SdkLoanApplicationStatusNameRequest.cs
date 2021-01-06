using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ByteWebConnector.SDK.Models.ControllerModels.Loan
{
    public class SdkLoanApplicationStatusNameRequest
    {
        public ByteProSettings ByteProSettings { get; set; }
        public string ByteFileName { get; set; }
    }
}
