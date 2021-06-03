using System.Security.Claims;
using Identity.Models.TwoFA;
using System.Threading.Tasks;
using Identity.Model;
using Identity.Model.TwoFA;
using TenantConfig.Common.DistributedCache;
using TenantConfig.Entity.Models;

namespace Identity.Service
{
    public interface ITwoFactorAuth
    {
        Task<TwilioTwoFaResponseModel> Create2FaRequestAsync(string to, string existingVerificationId, int sendDigits = 6);
        Task<TwilioTwoFaResponseModel> Verify2FaRequestAsync(string code, string requestId);
        //Task<TwoFaBase> Create2FaServiceForTenant(string userFriendlyName);
        void SetServiceSid(string sid);
        //Task<ClaimsPrincipal> Validate2FaTokenAsync(string token);
        //Task<ApiResponse> Verify2FaAsync(Verify2FaModel verifyData, TenantModel contextTenant, bool validate2FaToken);
    }
}
