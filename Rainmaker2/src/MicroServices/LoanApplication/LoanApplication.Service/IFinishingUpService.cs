using LoanApplication.Model;
using LoanApplicationDb.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TenantConfig.Common.DistributedCache;

namespace LoanApplication.Service
{
    public interface IFinishingUpService : IServiceBase<BorrowerResidence>
    {
        Task<BorrowerPrimaryAddressDetailModel> GetBorrowerPrimaryAddressDetail(TenantModel tenant, int userId, int loanApplicationId, int borrowerId);
        Task<int> AddOrUpdateBorrowerCurrentResidenceMoveInDate(TenantModel tenant,int userId, CurrentResidenceRequestModel model);

        Task<CurrentResidenceResponseModel> GetBorrowerCurrentResidenceMoveInDate(TenantModel tenant, int userId, int borrowerId, int loanApplicationId);

        Task<CoApplicantDetails> GetCoborrowerResidence(TenantModel tenant, int userId, int loanApplicationId);
        Task<ResidenceHistoryModel> GetResidenceHistory(TenantModel tenant, int userId, int loanApplicationId);

        Task<List<BorrowersDetail>> GetResidenceDetails(TenantModel tenant, int userId, int borrowerId, int loanApplicationId);

        Task<int> AddOrUpdateBorrowerCitizenship(TenantModel Tenant, int userId, BorrowerCitizenshipRequestModel model);

        Task<BorrowerCitizenshipResponseModel> GetBorrowerCitizenship(TenantModel Tenant, int userId, int borrowerId, int loanApplicationId);

        Task<int> AddOrUpdateSecondaryAddress(TenantModel tenant, int userId, BorrowerSecondaryAddressRequestModel model);

        //Task<CurrentResidenceResponseModel> GetPrimaryBorrowerCurrentResidenceMoveInDate(TenantModel tenant, int userId, int borrowerResidenceId, int loanApplicationId);

        Task<int> AddOrUpdateDependentinfo(TenantModel tenant, int userId, DependentModel model);
        Task<DependentModel> GetDependentinfo(TenantModel tenant, int userId, int loanApplicationId, int borrowerId);
        Task<BorrowerSecondaryAddressResponseModel> GetSecondaryAddress(TenantModel Tenant, int userId, int borrowerResidenceId, int loanApplicationId);
        Task DeleteSecondaryAddress(TenantModel tenant, int userId, int borrowerResidenceId, int loanApplicationId);

        Task<List<SpouseInfo>> GetAllSpouseInfo(TenantModel tenant,int userId,int loanApplicationId);
        Task<int> AddOrUpdateSpouseInfo(TenantModel tenant, int userId, SpouseInfoRequestModel model);
        Task<SpouseInfoResponseModel> GetSpouseInfo(TenantModel tenant, int userId, int borrowerId, int spouseLoanContactIdint, int loanApplicationId);
        Task<List<ReviewSpouseInfo>> ReviewBorrowerAndAllCoBorrowersInfo(TenantModel tenant, int userId, int loanApplicationId);
    }
}
