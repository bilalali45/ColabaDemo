using DocumentManagement.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocumentManagement.Service
{
    public interface IAdminDashboardService
    {
        Task<List<AdminDashboardDTO>> GetDocument(int loanApplicationId, int tenantId, bool pending);
        Task<bool> Delete(AdminDeleteModel model, int tenantId, IEnumerable<string> authHeader);
        Task<RequestIdQuery> IsDocumentDraft(int loanApplicationId, int userId);
    }
}
