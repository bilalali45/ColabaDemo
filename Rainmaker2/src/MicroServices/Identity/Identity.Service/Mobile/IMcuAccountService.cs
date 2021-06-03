using System.Collections.Generic;
using Identity.Entity.Models;
using Identity.Model;
using Identity.Model.Mobile;
using System.Threading.Tasks;
using Identity.Service.Mobile.Models;
using TenantConfig.Common;
using TenantConfig.Common.DistributedCache;
using TenantConfig.Entity.Models;

namespace Identity.Service.Mobile
{
    public interface IMcuAccountService :IServiceBase<User>
    {
     
        Task<ApiResponse> Signin(MobileSigninModel model);
        Task<ApiResponse> ForgotPasswordRequest(ForgotPasswordRequestModel model);

        Task<ApiResponse> ForgotPasswordResponse(ForgotPasswordResponseModel model);


        Task<ApiResponse> IsPasswordLinkExpired(int userId,
                                                string key);

        Task<Employee> GetEmployeeByUserIdAsync(long userId, int? tenantId,
            List<EmployeeRelatedEntities> includes = null);

        Task<Employee> GetEmployeeByUserIdAsync(long userId, List<EmployeeRelatedEntities> includes = null);
        Task<string> GetVerifiedMobileNumber(int userId, TenantModel tenant);

        Task<ApiResponse> SendTwoFaToNumber(string phoneNumber, int userId, TenantModel tenant,
            string ipAddress);

        Task<ApiResponse> VerifyTwoFa(VerifyTwoFaModel model, int userId, TenantModel tenant, string ipAddress);
        Task<ApiResponse> GetTwoFaValuesToSkip(int userId, TenantModel tenant);
        Task<ApiResponse> SkipTwoFa(int userId, TenantModel tenant);
        Task<ApiResponse> DontAskTwoFa(string token, int tenantId, string tenantCode);
        void CreateDontAskTwoFa(string email, string tenantCode, int userId);
    }
}
