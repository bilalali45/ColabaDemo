using System;
using System.Collections.Generic;
using System.Text;
using ByteWebConnector.Model.Models.OwnModels.Settings;

namespace ByteWebConnector.Model.Models.ServiceRequestModels
{
    public class LoanApplicationStatusNameRequest
    {
        public ByteProSettings ByteProSettings { get; set; }
        public string ByteFileName { get; set; }
    }
}
