using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Service
{
    public  interface ILossIntegrationService
    {
        Task<string> SendDocumentToBytePro(int LoanApplicationId, string DocumentLoanApplicationId, string RequestId, string DocumentId, string FileId, IEnumerable<string> authHeader);

    }
}
