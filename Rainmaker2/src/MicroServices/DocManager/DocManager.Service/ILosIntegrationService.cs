using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocManager.Service
{
    public interface ILosIntegrationService
    {
        Task<string> SendFilesToBytePro(int loanApplicationId,
                                        string documentLoanApplicationId,
                                        string requestId,
                                        string documentId,
                                        string fileid,
                                        IEnumerable<string> authHeader);
    }
}