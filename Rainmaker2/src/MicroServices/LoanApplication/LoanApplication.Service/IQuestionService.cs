using LoanApplication.Model;
using LoanApplicationDb.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TenantConfig.Common.DistributedCache;

namespace LoanApplication.Service
{
    public interface IQuestionService : IServiceBase<Question>
    {
        Task<List<QuestionModel>> GetSection2PrimaryQuestions(TenantModel tenant, int userId, int loanApplicationId, int borrowerId);
        Task<List<QuestionModel>> GetSection2SecondaryQuestion(TenantModel tenant, int userId, int loanApplicationId, int borrowerId);
        Task<List<QuestionModel>> GetSectionOneForPrimaryBorrower(TenantModel Tenant, int userId, int borrowerId, int loanApplicationId);
        Task<List<QuestionModel>> GetSectionOneForSecondaryBorrower(TenantModel Tenant, int userId, int borrowerId, int loanApplicationId);
        Task<bool> AddOrUpdateSectionOne(TenantModel tenant, int userId, QuestionRequestModel model);
        Task<bool> AddOrUpdateSection2(TenantModel tenant, int userId, QuestionRequestModel model);
        List<DropDownModel> GetAllPropertyUsageDropDown(TenantModel tenant);
        List<DropDownModel> GetAllTitleHeldWithDropDown(TenantModel tenant);
        List<DropDownModel> GetAllBankruptcy(TenantModel tenant);

        List<DropDownModel> GetAllLiablilityType(TenantModel tenant);

        Task<List<QuestionModel>> GetSection3ForSecondaryBorrower(TenantModel tenant, int userId, int borrowerId, int loanApplicationId);
        Task<List<QuestionModel>> GetSection3ForPrimaryBorrower(TenantModel tenant, int userId, int borrowerId, int loanApplicationId);

        Task<bool> AddOrUpdateSection3(TenantModel tenant, int userId, QuestionRequestModel model);

        List<RaceModel> GetAllRaceList(TenantModel tenant);

        List<DropDownModel> GetGenderList(TenantModel tenant); 

        List<EthnicityModel> GetAllEthnicityList(TenantModel tenant);

        Task<DemographicInfoResponseModel> GetDemographicInformation(TenantModel tenant, int UserId, int borrowerId, int loanApplicationId);

        Task<bool> AddOrUpdateDemogrhphicInfo(TenantModel tenant, int UserId, DemographicInfoResponseModel model);
        Task<PrimaryBorrowerSubjectPropertyModel> CheckPrimaryBorrowerSubjectProperty(TenantModel tenant, int userId, int borrowerId, int loanApplicationId);
        Task<SecondaryBorrowerSubjectPropertyModel> CheckSecondaryBorrowerSubjectProperty(TenantModel tenant, int userId, int borrowerId, int loanApplicationId);
        Task<object> GetGovernmentQuestionReview(TenantModel tenant, int userId, int loanApplicationId);
        Task<object> GetDemographicInformationReview(TenantModel tenant, int userId , int loanApplicationId);
      
    }
}
