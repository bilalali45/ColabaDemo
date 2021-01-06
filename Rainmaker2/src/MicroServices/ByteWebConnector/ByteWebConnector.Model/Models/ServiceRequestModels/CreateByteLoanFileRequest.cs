using ByteWebConnector.Model.Models.ActionModels.LoanFile;
using ByteWebConnector.Model.Models.OwnModels.Settings;

namespace ByteWebConnector.Model.Models.ServiceRequestModels
{
    public class CreateByteLoanFileRequest
    {
        public LoanApplication LoanApplication { get; set; }
        public ByteProSettings ByteProSettings { get; set; }
    }
}