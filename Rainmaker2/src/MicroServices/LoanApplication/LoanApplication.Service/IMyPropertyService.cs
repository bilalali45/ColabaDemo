using LoanApplication.Model;
using LoanApplicationDb.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TenantConfig.Common.DistributedCache;

namespace LoanApplication.Service
{
    public interface IMyPropertyService : IServiceBase<BorrowerProperty>
    {

        Task<PrimaryBorrowerDetailModel> GetPrimaryBorrowerAddressDetail(TenantModel tenant, int userId, int loanApplicationId);
        Task<int> AddOrUpdatePrimaryPropertyType(TenantModel tenant, int userId, BorrowerPropertyRequestModel model);
        Task<int> AddOrUpdateAdditionalPropertyType(TenantModel tenant, int userId, BorrowerAdditionalPropertyRequestModel model);
        Task<BorrowerPropertyResponseModel> GetBorrowerPrimaryPropertyType(TenantModel tenant, int userId, int borrowerPropertyId, int loanApplicationId);
        Task<BorrowerAdditionalPropertyResponseModel> GetBorrowerAdditionalPropertyType(TenantModel tenant, int userId, int borrowerPropertyId, int loanApplicationId);
        Task<List<MyPropertyModel>> GetPropertyList(TenantModel tenant, int userId, int loanApplicationId, int borrowerId);
        Task<HasFirstMortgageModel> DoYouHaveFirstMortgage(TenantModel tenant, int userId, int loanApplicationId, int borrowerPropertyId);
        Task<bool?> DoYouHaveSecondMortgage(TenantModel tenant, int userId, int loanApplicationId, int borrowerPropertyId);
        Task<int> AddOrUpdatePropertyValue(TenantModel tenant, int userId, CurrentResidenceModel model);
        Task<CurrentResidenceModel> GetPropertyValue(TenantModel tenant, int userId, int borrowerPropertyId, int loanApplicationId);
        Task<int> AddOrUpdateHasSecondMortgage(TenantModel tenant, int userId, HasSecondMortgageModel model);
        Task<int> AddOrUpdateFirstMortgageValue(TenantModel tenant, int userId, FirstMortgageModel model);
        Task<FirstMortgageModel> GetFirstMortgageValue(TenantModel tenant, int userId, int borrowerPropertyId, int loanApplicationId);
        Task<int> AddOrUpdateHasFirstMortgage(TenantModel tenant, int userId, HasFirstMortgageModel model);

        Task<int> AddOrUpdateAdditionalPropertyInfo(TenantModel tenant, int userId, BorrowerAdditionalPropertyInfoRequestModel model);
        Task<BorrowerAdditionalPropertyInfoResponseModel> GetBorrowerAdditionalPropertyInfo(TenantModel tenant, int userId, int borrowerPropertyId, int loanApplicationId);


        Task<int> AddOrUpdatBorrowerAdditionalPropertyAddress(TenantModel tenant, int userId, BorrowerAdditionalPropertyAddressRequestModel model);
        Task<BorrowerAdditionalPropertyAddressRsponseModel> GetBorrowerAdditionalPropertyAddress(TenantModel tenant, int userId, int loanApplicationId, int borrowerPropertyId);
        Task<int> AddOrUpdateSecondMortgageValue(TenantModel tenant, int userId, SecondMortgageModel model);
        Task<SecondMortgageModel> GetSecondMortgageValue(TenantModel tenant, int userId, int borrowerPropertyId, int loanApplicationId);

        Task<List<MyPropertyModel>> GetFinalScreenReview(TenantModel tenant, int userId, int loanApplicationId, int borrowerId);
        Task<bool> DeleteProperty(TenantModel tenant, int userId, int loanApplicationId, int borrowerPropertyId);
        Task DeleteProperty(int borrowerPropertyId);
        Task<bool> DoYouOwnAdditionalProperty(TenantModel tenant, int userId, int loanApplicationId, int borrowerId);
      
    }
}
