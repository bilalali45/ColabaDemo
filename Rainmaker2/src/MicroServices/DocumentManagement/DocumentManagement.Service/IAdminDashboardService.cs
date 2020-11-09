using DocumentManagement.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocumentManagement.Service
{
    public interface IAdminDashboardService
    {
        Task<List<AdminDashboardDto>> GetDocument(int loanApplicationId, int tenantId, bool pending, int userId);
        Task<bool> Delete(AdminDeleteModel model, int tenantId, IEnumerable<string> authHeader);
        Task<RequestIdQuery> IsDocumentDraft(int loanApplicationId, int userId);
        Task<DashboardSettingModel> GetDashboardSetting(int userProfileId);
    }
}
