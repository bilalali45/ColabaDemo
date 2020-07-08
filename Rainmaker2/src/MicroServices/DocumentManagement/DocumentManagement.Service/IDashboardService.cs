using DocumentManagement.Entity;
using DocumentManagement.Model;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
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
