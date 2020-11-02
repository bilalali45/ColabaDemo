using ByteWebConnector.Model.Models.ServiceResponseModels.Rainmaker.LoanApplication;
using ServiceCallHelper;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ByteWebConnector.Model.Models;
using ByteWebConnector.Model.Models.ServiceResponseModels.ByteApi;

namespace ByteWebConnector.Service.InternalServices
{
    public interface IRainmakerService
    {
        GetLoanApplicationResponse GetLoanApplication(int loanApplicationId);


        Task<HttpResponseMessage> DocumentAddDocument(int fileDataId,
                                                      List<EmbeddedDoc> embeddedDocs);
    }
}