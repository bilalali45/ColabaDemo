using System;
using System.Linq;
using System.Net;
using Identity.Model;
using Identity.Models.TwoFA;
using Identity.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Identity.Service;
using TenantConfig.Common;
using TenantConfig.Common.DistributedCache;

namespace Identity.Controllers
{
    [Route(template: "api/Identity/[controller]")]
    [ApiController]
    public class CustomerAccountController : Controller
    {
        private readonly ICustomerAccountService _accountService;
        private readonly ICustomerService _customerService;
        private readonly ITwoFaHelper _twoFaHelper;
        private readonly ITenantConfigService _tenantConfigService;
        private readonly IOtpTracingService _otpTracingService;

        public CustomerAccountController(ICustomerAccountService accountService, ITwoFaHelper twoFaHelper, ICustomerService customerService, ITenantConfigService tenantConfigService, IOtpTracingService otpTracingService)
        {
            _accountService = accountService;
            _twoFaHelper = twoFaHelper;
            _customerService = customerService;
            _tenantConfigService = tenantConfigService;
            _otpTracingService = otpTracingService;
        }
        [AllowAnonymous]
        [HttpPost("[action]")]
        [ResolveWebTenant]
        [ServiceFilter(typeof(ValidateRecaptchaAttribute))]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            TenantModel tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            if(model.Phone!=null && !PhoneHelper.IsValidUnmasked(model.Phone))
            {
                return BadRequest(new ApiResponse() { Code = "400", Message = "Invalid phone number." });
            }
            //TODO: check whether tenant has 2FA enabled
            if (await this._tenantConfigService.TenantRequiresCustomerTwoFa(tenant.Id)   )
            {
                if (string.IsNullOrEmpty(model.Phone))
                {
                    return BadRequest(new ApiResponse() { Code = "400", Message = "Phone number is required." });
                }
                
                if (await this._otpTracingService.OtpVerificationExists(model.Phone) == false)
                {
                    return BadRequest(new ApiResponse() { Code = "400", Message = $"No OTP verification found." });
                }
            }
            //TODO: check whether phone has been verified via 2FA
            bool is2faVerified = false;
            if (await _accountService.DoesCustomerAccountExist(model.Email, tenant.Id))
            {
                return BadRequest(new ApiResponse() { Code = "400", Message = "Account already exists against provided email address." });
            }
            int userId= await _accountService.Register(model, tenant.Id, is2faVerified);
            
            this._twoFaHelper.CreateDontAskCookie(model.Email, model.DontAsk2Fa, tenant.Code, userId);
            await this._accountService.Set2FaForUserAsync(tenant.Id, userId, !model.Skipped2Fa);
            if (model.MapPhoneNumber)
            {
                await this._customerService.MapPhoneNumberFromOtpTracingAsync(userId
                    , tenant.Id, model.Email, model.RequestSid, true);
            }

            var response = await _accountService.GenerateNewAccessToken(userId, tenant.Id, tenant.Branches[0].Code);
            if (response.Code != "200")
                return BadRequest(response);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("[action]")]
        [ResolveWebTenant]
        [ServiceFilter(typeof(ValidateRecaptchaAttribute))]
        public async Task<IActionResult> DoesCustomerAccountExist(string email)
        {
            TenantModel tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            if (await _accountService.DoesCustomerAccountExist(email, tenant.Id))
            {
                return Ok(new ApiResponse() { Code = "200", Data = new { Exists = true } });
            }
            return Ok(new ApiResponse() { Code = "200", Data = new { Exists = false } });
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        [ResolveWebTenant]
        [ServiceFilter(typeof(ValidateRecaptchaAttribute))]
        public async Task<IActionResult> Signin(SigninModel model)
        {
            TenantModel tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            var response = await _accountService.Signin(model, tenant);
            if (response.Code != "200")
                return BadRequest(response);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        [ResolveWebTenant]
        [ServiceFilter(typeof(ValidateRecaptchaAttribute))]
        public async Task<IActionResult> ForgotPasswordRequest(ForgotPasswordRequestModel model)
        {
            TenantModel tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            var response = await _accountService.ForgotPasswordRequest(model, tenant);
            if (response.Code != "200")
                return BadRequest(response);
            return Ok(response);
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost("[action]")]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel changePasswordModel)
        {
            var userId = int.Parse(s: User.FindFirst(type: "UserId").Value);
            TenantModel tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            var response = await _accountService.ChangePassword(userId, changePasswordModel.oldPassword, changePasswordModel.newPassword, tenant.Id);
            if (response.Code != "200")
                return BadRequest(response);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        [ResolveWebTenant]
        [ServiceFilter(typeof(ValidateRecaptchaAttribute))]
        public async Task<IActionResult> ForgotPasswordResponse(ForgotPasswordResponseModel model)
        {
            TenantModel tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            var response = await _accountService.ForgotPasswordResponse(model, tenant);
            if (response.Code != "200")
                return BadRequest(response);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("[action]")]
        [ResolveWebTenant]
        [ServiceFilter(typeof(ValidateRecaptchaAttribute))]
        public async Task<IActionResult> IsPasswordLinkExpired(int userId, string key)
        {
            TenantModel tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            var response = await _accountService.IsPasswordLinkExpired(userId, key, tenant);
            return Ok(response);
        }

        //[Authorize(Roles = "Customer")]
        //[ResolveWebTenant]
        //[HttpDelete("[action]")]
        //public async Task<IActionResult> DeleteUser(DeleteUserModel model)
        //{
        //    TenantModel tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
        //    if (! await _accountService.DoesCustomerAccountExist(model.Email, tenant.Id))
        //    {
        //        return BadRequest(new ApiResponse() { Code = "400", Message = "Account does not exist." });
        //    }
        //    return Ok(await _accountService.DeleteUser(model.Email, tenant.Id));
        //}
    }
}