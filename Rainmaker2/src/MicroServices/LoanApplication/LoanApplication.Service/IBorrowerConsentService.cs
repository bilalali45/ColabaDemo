using System.Collections.Generic;
using System.Threading.Tasks;
using LoanApplication.Model;
using TenantConfig.Common.DistributedCache;

namespace LoanApplication.Service
{
    public interface IBorrowerConsentService
    {
        Task<int> AddOrUpdate(TenantModel tenant, BorrowerConsentModel model, string ipAddress, int userId);

        Task<int> AddOrUpdateMultipleConsents(TenantModel tenant, BorrowerMultipleConsentsModel model, string ipAddress,
            int userId);
        ConsentTypeGetModel GetAllConsentType(TenantModel tenant);
        string ComputeConsentHash(List<string> consentList);

        Task<BorrowerAcceptedConsentsModel> GetBorrowerAcceptedConsents(TenantModel tenant, int userId, int borrowerId,
            int loanApplicationId);

        Task<ConsentTypeGetModel> GetBorrowerConsent(TenantModel tenant, int userId, int borrowerId);
    }
}