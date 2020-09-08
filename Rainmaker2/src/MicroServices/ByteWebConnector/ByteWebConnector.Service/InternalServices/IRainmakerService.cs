using ByteWebConnector.Model.Models.Document;
using ByteWebConnector.Model.Models.ServiceResponseModels.Rainmaker.LoanApplication;
using ServiceCallHelper;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ByteWebConnector.Service.InternalServices
{
    public interface IRainmakerService
    {
        CallResponse<GetLoanApplicationResponse> GetLoanApplication(int loanApplicationId);


        Task<HttpResponseMessage> DocumentAddDocument(int fileDataId,
                                                      List<EmbeddedDoc> embeddedDocs);
    }
}