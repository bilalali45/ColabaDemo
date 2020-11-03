using System;
using System.Collections.Generic;
using System.Text;

namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    public class LoanFileRequest
    {
        public LoanApplication LoanApplication { get; set; }
        public LoanRequest LoanRequest { get; set; }
        public ThirdPartyCodeList ThirdPartyCodeList { get; set; }
    }
}
