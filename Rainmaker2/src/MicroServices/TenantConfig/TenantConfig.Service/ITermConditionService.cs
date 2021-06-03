using System.Threading.Tasks;
using TenantConfig.Common.DistributedCache;
using TenantConfig.Entity.Models;
using TenantConfig.Model;
using BranchModel = TenantConfig.Model.BranchModel;
namespace TenantConfig.Service
{
    public interface ITermConditionService : IServiceBase<TermsCondition>
    {
        Task<string> GetTermsConditions(int type, int tenantId, int branchId);
        Task<BranchModel> GetSetting(TenantModel tenant, int branchId, string branchCode);
        Task PutSetting(TenantModel tenant, SettingModel model);
    }
}
