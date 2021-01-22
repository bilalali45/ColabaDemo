using DocumentManagement.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocumentManagement.Service
{
    public  interface IDocumentService
    {
        Task<List<DocumentDto>> GetFiles(string id, string requestId, string docId);
        Task<List<ActivityLogDto>> GetActivityLog(string id, string requestId, string docId);
        Task<List<GetTemplateModel>> GetDocumentsByTemplateIds(List<string> id, int tenantId);
        Task<List<EmailLogDto>> GetEmailLog(string id, string requestId, string docId);

        Task<bool> McuRename(string id, string requestId, string docId, string fileId, string newName, string userName);
        Task<bool> AcceptDocument(string id, string requestId, string docId, string userName, IEnumerable<string> authHeader);
        Task<bool> RejectDocument(string id, string requestId, string docId,string message,int userId, string userName, IEnumerable<string> authHeader);
        Task<FileViewDto> View(AdminFileViewModel model, int userProfileId, string ipAddress, int tenantId);

        Task<bool> UpdateByteProStatus(string id, string requestId, string docId, string fileId, bool isUploaded, int userId, int tenantId);

        Task<bool> DeleteFile(int loanApplicationId, string fileId);
        Task<string> CreateLoanApplication(int loanApplicationId, int tenantId, int userId, string userName);
    }
}
