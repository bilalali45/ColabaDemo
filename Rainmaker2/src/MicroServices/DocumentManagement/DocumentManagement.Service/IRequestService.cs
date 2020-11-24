using DocumentManagement.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocumentManagement.Service
{
    public interface IRequestService
    {
        Task<bool> Save(Model.LoanApplication loanApplication, bool isDraft, IEnumerable<string> authHeader);
        
        Task<RequestDraftModel> GetDraft(int loanApplicationId, int tenantId);
        Task<string> GetEmailTemplate(int tenantId);
        Task<string> UploadFile(int userProfileId, string userName, int tenantId, int custUserId, string custUserName, UploadFileModel model, IEnumerable<string> authHeader);
    }
}
