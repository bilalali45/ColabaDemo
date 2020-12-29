using DocumentManagement.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocumentManagement.Service
{
    public interface IRequestService
    {
        Task<bool> Save(Model.LoanApplication loanApplication, bool isDraft,bool isFromBorrower, IEnumerable<string> authHeader);
        Task<RequestResponseModel> SaveByBorrower(Model.LoanApplication loanApplication, bool isDraft, bool isFromBorrower, IEnumerable<string> authHeader);

        Task<RequestDraftModel> GetDraft(int loanApplicationId, int tenantId);
        Task<string> GetEmailTemplate(int tenantId);
        Task<string> UploadFile(int userProfileId, string userName, int tenantId, int custUserId, string custUserName, UploadFileModel model, IEnumerable<string> authHeader);
        Task<RequestResponseModel> GetDocumentRequest(int loanApplicationId, int tenantId,string docId); 
    }
}
