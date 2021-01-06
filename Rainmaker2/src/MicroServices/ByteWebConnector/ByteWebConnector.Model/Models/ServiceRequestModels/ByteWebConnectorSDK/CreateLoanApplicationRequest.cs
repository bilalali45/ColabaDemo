using ByteWebConnector.Model.Models.ActionModels.LoanFile;
using ByteWebConnector.Model.Models.OwnModels.Settings;

namespace ByteWebConnector.Model.Models.ServiceRequestModels.ByteWebConnectorSDK
{
    public class CreateLoanApplicationRequest
    {
        //public LoanApplication LoanApplication { get; set; }
        public LoanFileRequest LoanFileRequest { get; set; }
        public ByteProSettings ByteProSettings { get; set; }
    }
}