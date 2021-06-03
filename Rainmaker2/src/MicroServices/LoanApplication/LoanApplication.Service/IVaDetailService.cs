using System.Collections.Generic;
using System.Threading.Tasks;
using LoanApplication.Model;
using TenantConfig.Common.DistributedCache;

namespace LoanApplication.Service
{
    public interface IVaDetailService
    {
        Task<int> AddOrUpdate(TenantModel tenant,
            int userId,
            VaDetailAddOrUpdate model);


        Task<BorrowerVaDetailGetModel> GetVaDetails(TenantModel tenant,
                                                    int userId,
                                                    int borrowerId);

        Task<int> SetBorrowerVaStatus(TenantModel tenant, int userId, BorrowerVaStatusModel model);
        Task<bool?> GetBorrowerVaStatus(TenantModel tenant, int userId, int borrowerId);
    }
}