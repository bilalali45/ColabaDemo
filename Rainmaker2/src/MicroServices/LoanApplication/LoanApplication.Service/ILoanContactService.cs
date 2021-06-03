using System.Threading.Tasks;
using LoanApplication.Model;
using TenantConfig.Common.DistributedCache;

namespace LoanApplication.Service
{
    public interface ILoanContactService
    {
        Task<int> UpdateDobSsn(TenantModel tenant,
            int userId,
            BorrowerDobSsnAddOrUpdate model);

        Task<BorrowerDobSsnGet> GetDobSsn(TenantModel tenant,
            int borrowerId,
            int loanApplicaitonId,
            int userId );
    }
}