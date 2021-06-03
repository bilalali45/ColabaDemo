using Identity.Entity.Models;
using Identity.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TenantConfig.Common.DistributedCache;

namespace Identity.Service
{
    public interface ICustomerAccountService : IServiceBase<User>
    {
        Task<bool> DoesCustomerAccountExist(string email, int tenantId);
        Task<int> Register(RegisterModel model, int tenantId, bool is2FaVerified);
        Task<ApiResponse> Signin(SigninModel model, TenantModel tenant);
        Task<ApiResponse> ForgotPasswordRequest(ForgotPasswordRequestModel model, TenantModel tenant);
        Task<ApiResponse> ChangePassword(int userId, string oldPassword, string newPassword,int tenantId);
        Task<ApiResponse> ForgotPasswordResponse(ForgotPasswordResponseModel model, TenantModel tenant);
        Task<User> GetUserById(int userId, int tenantId);
        Task<ApiResponse> GenerateNewAccessToken(int userId, int tenantId, string branchCode);
        Task<ApiResponse> Set2FaForUserAsync(int tenantId, int userId, bool userVerified2Fa);
        Task<ApiResponse> IsPasswordLinkExpired(int userId, string key, TenantModel tenant);
        //Task<ApiResponse> DeleteUser(string email, int tenantId);
    }
}
