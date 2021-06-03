using LoanApplication.Model;
using LoanApplicationDb.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TenantConfig.Common.DistributedCache;

namespace LoanApplication.Service
{
    public interface IPropertyService : IServiceBase<PropertyInfo>
    {
        Task<PropertyTypeModel> GetPropertyType(TenantModel tenant, int loanApplicationId, int userId);
        Task<bool> AddOrUpdatePropertyType(TenantModel tenant, AddPropertyTypeModel model, int userId);
        Task<GetPropertyUsageModel> GetPropertyUsage(TenantModel tenant, int loanApplicationId, int userId);
        Task<bool> AddOrUpdatePropertyUsage(TenantModel tenant, AddPropertyUsageModel model, int userId);
        Task<GetPropertyAddressModel> GetPropertyAddress(TenantModel tenant, int userId, int loanApplicationId);
        Task<bool> AddOrUpdatePropertyAddress(TenantModel tenant, AddPropertyAddressModel model, int userId);
    }
}
