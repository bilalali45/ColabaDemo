using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using LosIntegration.Model.Model.ActionModels.Document;
using LosIntegration.Model.Model.ServiceRequestModels.Document;

namespace LosIntegration.Service.InternalServices
{
    public interface IDocumentManagementService
    {
        Task<HttpResponseMessage> GetFileDataFromDocumentManagement(string documentLoanApplicationId,
                                                                    string requestId,
                                                                    string documentId,
                                                                    string fileId,
                                                                    string tenantId);
        Task<List<DocumentManagementDocument>> GetDocuments(int loanApplicationId);
        Task<List<DocumentCategory>> GetDocumentCategories();
        Task<bool> UpdateByteStatusInDocumentManagement(UpdateByteStatusRequest updateByteStatusRequest);


        Task<HttpResponseMessage> GetDocuments(int loanApplicationId,
                                               bool pending);
    }
}
