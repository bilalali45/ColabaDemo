using DocumentManagement.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Service
{
    public interface IDashboardService
    {
        Task<List<DashboardDTO>> GetPendingDocuments(int loanApplicationId, int tenantId);
    }
}
