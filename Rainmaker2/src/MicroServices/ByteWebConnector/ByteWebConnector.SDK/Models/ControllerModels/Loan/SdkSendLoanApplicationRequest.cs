using ByteWebConnector.SDK.Models.Rainmaker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ByteWebConnector.SDK.Models.ControllerModels.Loan
{
    public class SdkSendLoanApplicationRequest
    {
        //public LoanApplication LoanApplication { get; set; }
        public LoanFileRequest LoanFileRequest { get; set; }
        public ByteProSettings ByteProSettings { get; set; }
    }

    public class LoanFileRequest
    {
        public LoanApplication LoanApplication { get; set; }
        public LoanRequest LoanRequest { get; set; }
        public ThirdPartyCodeList ThirdPartyCodeList { get; set; }
    }
}
