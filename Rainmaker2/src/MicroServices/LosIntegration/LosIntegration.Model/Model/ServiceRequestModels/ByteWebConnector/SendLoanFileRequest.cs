using System;
using System.Collections.Generic;
using System.Text;
using LosIntegration.Model.Model.ServiceResponseModels.Rainmaker;

namespace LosIntegration.Model.Model.ServiceRequestModels.ByteWebConnector
{
    public class SendLoanFileRequest
    {
        public LoanApplication LoanApplication { get; set; }
        public LoanRequest LoanRequest { get; set; }
        public ThirdPartyCodeList ThirdPartyCodeList { get; set; }
    }
}
