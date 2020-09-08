using DocumentManagement.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocumentManagement.Service
{
    public interface IDashboardService
    {
        Task<List<DashboardDTO>> GetPendingDocuments(int loanApplicationId, int tenantId, int userProfileId);
        Task<List<DashboardDTO>> GetSubmittedDocuments(int loanApplicationId, int tenantId, int userProfileId);
        Task<List<DashboardStatus>> GetDashboardStatus(int loanApplicationId, int tenantId, int userProfileId);
        Task<string> GetFooterText(int tenantId, int businessUnitId);
    }
}
