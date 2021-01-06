using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ByteWebConnector.SDK.Models.Rainmaker;

namespace ByteWebConnector.SDK.Models
{
    public class CreateByteFileRequest
    {
        public LoanApplication loanApplication { get; set; }
        public ByteProSettings byteproCredentials { get; set; }
    }
}
