using LoanApplication.Model;
using LoanApplicationDb.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TenantConfig.Common.DistributedCache;
using static LoanApplication.Model.RefinancePropertyModel;

namespace LoanApplication.Service
{
   public interface IRefinancePropertyService : IServiceBase<PropertyInfo>
    {
        Task<BorrowerResidenceStatusModel> GetPrimaryBorrowerResidenceHousingStatus(TenantModel tenant, int userId, int loanApplicationId, int borrowerId);
        Task<PropertyAddressModel> GetBorrowerResidenceAddress(TenantModel tenant, int userId, int borrowerResidenceId, int borrowerId, int loanApplicationId);
        Task<int> AddOrUpdateIsSameAsPropertyAddress(TenantModel tenant, int userId, SameAsPropertyAddress model);
        Task<bool> AddOrUpdatePropertyType(TenantModel tenant, int userId, AddPropertyTypeModel model);
        Task<GetRefinancePropertyUsageModel> GetPropertyUsageRent(TenantModel tenant, int loanApplicationId, int userId);
        Task<bool> AddOrUpdatePropertyUsageRent(TenantModel tenant, int userId, AddPropertyUsageRefinanceModel model);
        Task<PropertyTypeModel> GetPropertyType(TenantModel tenant, int loanApplicationId, int userId);
        Task<bool> AddOrUpdatePropertyUsageOwn(TenantModel tenant, AddPropertyUsagerequestModel model, int userId);
        Task<AddPropertyUsagerequestModel> GetPropertyUsageOwn(TenantModel tenant, int loanApplicationId, int userId);
        Task<GenericAddressModel> GetPropertyAddress(TenantModel tenant, int userId, int loanApplicationId);
        Task<bool> AddOrUpdatePropertyAddress(TenantModel tenant, AddPropertyAddressModel model, int userId);
        Task<SubjectPropertyDetails> GetSubjectPropertyDetails(TenantModel tenant, int userId, int loanApplicationId);
        Task<bool> AddOrUpdateSubjectPropertyDetails(TenantModel tenant, SubjectPropertyDetailsRequestModel model, int userId);

        Task<HasMortgageModel> DoYouHaveFirstMortgage(TenantModel tenant, int userId, int loanApplicationId);
        Task<int> AddOrUpdateHasFirstMortgage(TenantModel tenant, int userId, HasMortgageModel model);
        Task<FirstMortgageModel> GetFirstMortgageValue(TenantModel tenant, int userId, int loanApplicationId);
        Task<int> AddOrUpdateFirstMortgageValue(TenantModel tenant, int userId, FirstMortgageModel model);
    }
}
