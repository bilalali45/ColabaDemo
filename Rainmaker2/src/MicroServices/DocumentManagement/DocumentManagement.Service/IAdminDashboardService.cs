using DocumentManagement.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Service
{
   public interface IAdminDashboardService
    {
        Task<List<AdminDashboardDTO>> GetDocument(int loanApplicationId, int tenantId, bool pending);
        Task<bool> Delete(AdminDeleteModel model);
        Task<RequestIdQuery> IsDocumentDraft(string id, int userId);
    }
}
