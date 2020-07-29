using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DocumentManagement.Entity;
using DocumentManagement.Model;

namespace DocumentManagement.Service
{
    public interface IRequestService
    {
        Task<bool> Save(Model.LoanApplication loanApplication, bool isDraft);

        Task<List<DraftDocumentDTO>> GetDraft(int loanApplicationId, int tenantId);
    }
}
