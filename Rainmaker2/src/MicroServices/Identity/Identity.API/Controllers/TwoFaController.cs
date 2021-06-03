using Identity.Model;
using Identity.Models.TwoFA;
using Identity.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Identity.Model.TwoFA;
using Identity.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using TenantConfig.Common;
using TenantConfig.Common.DistributedCache;
using IKeyStoreService = Identity.Service.IKeyStoreService;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Identity.Controllers
{
    [Route(template: "api/Identity/[controller]")]
    [ApiController]
    public class TwoFaController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly ITwoFactorAuth _twoFactorAuthService;
        private readonly ITenantConfigService _tenantConfigService;
        private readonly ILogger<TwoFaController> _logger;
        private readonly IOtpTracingService _otpTracingService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICustomerService _customerService;
        private readonly ICustomerAccountService _accountService;
        private readonly ITwoFaHelper _twoFaHelper;
        private readonly IConfiguration _config;

        public TwoFaController(ITokenService tokenService, ITwoFactorAuth twoFactorAuthService, ITenantConfigService tenantConfigService, ILogger<TwoFaController> logger, IOtpTracingService otpTracingService, IHttpContextAccessor httpContextAccessor, ICustomerService customerService, ITwoFaHelper twoFaHelper, ICustomerAccountService accountService, IConfiguration config, IKeyStoreService keyStoreService)
        {
            this._tokenService = tokenService;
            this._twoFactorAuthService = twoFactorAuthService;
            this._tenantConfigService = tenantConfigService;
            this._logger = logger;
            this._otpTracingService = otpTracingService;
            this._httpContextAccessor = httpContextAccessor;
            this._customerService = customerService;
            this._twoFaHelper = twoFaHelper;
            _accountService = accountService;
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        [ResolveWebTenant]
        [ServiceFilter(typeof(ValidateRecaptchaAttribute))]
        public async Task<IActionResult> Verify2FaSignInRequest(Verify2FaModel model)
        {
            ApiResponse apiResponse = new ApiResponse()
            {
                Code = Convert.ToString((int) HttpStatusCode.BadRequest),
                Status = Convert.ToString(HttpStatusCode.BadRequest)
            };

            var phoneValidRes = this._twoFaHelper.ValidatePhoneNumber(model.PhoneNumber);
            if (phoneValidRes != null)
            {
                return BadRequest(phoneValidRes);
            }

            if (await this._otpTracingService.VerifyPhoneSidCombination(model.PhoneNumber, model.RequestSid) == false)
            {
                return BadRequest(new ApiResponse()
                {
                    Message = "Verification detail not found.",
                    Code = Convert.ToString((int) HttpStatusCode.BadRequest),
                    Status = HttpStatusCode.BadRequest.ToString()
                });
            }

            TenantModel contextTenant = HttpContext.Items[Constants.COLABA_TENANT] as TenantModel;
            var token = Convert.ToString(Request.Headers["Authorization"]);
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }
            var principal = await this._tokenService.Validate2FaTokenAsync(token.Split(' ')[1]);
            if (principal == null)
            {
                return Unauthorized(apiResponse);
            }

            var userId = principal.Claims.Where(c => c.Type == "UserProfileId").FirstOrDefault().Value;


            var tenant2FaInfo = await this._tenantConfigService.GetTenant2FaConfigAsync(contextTenant.Id);
            this._twoFactorAuthService.SetServiceSid(tenant2FaInfo.TwilioVerifyServiceId);

            var response = await this._twoFactorAuthService.Verify2FaRequestAsync(model.Code, model.RequestSid);

            if (response.StatusCode == (int) HttpStatusCode.NotFound) // When verification SID not found
            {
                apiResponse.Message = "Verification not found.";
            }
            else
            {
                //TODO: insert/update phone number in contactphoneinfo with isValid true
                await this._otpTracingService.Create2FaLogAsync(model.PhoneNumber,
                    model.Code, null, model.Email, response.Status, model.RequestSid, contextTenant, response);
            }

            
            response.VerifyAttemptsCount = await this._otpTracingService.GetVerificationAttemptsCountAsync(model.RequestSid);
            
            //response.OtpValidTill = response.DateCreated.AddMinutes(this._config.GetValue<int>("DontAsk2FaCookieDays"));
            response.OtpValidTill = response.DateCreated.AddMinutes(this._twoFaHelper.Read2FaConfig<int>("OtpValidity", 10));
            if (response.Status == "approved")
            {
                await this._accountService.Set2FaForUserAsync(contextTenant.Id, int.Parse(userId), true);
                if (model.MapPhoneNumber)
                {
                    await this._customerService.MapPhoneNumberFromOtpTracingAsync(int.Parse(userId)
                        , contextTenant.Id, model.Email, model.RequestSid, true);
                }

                apiResponse = await this._accountService.GenerateNewAccessToken(int.Parse(userId), contextTenant.Id,
                    contextTenant.Branches[0].Code);
                this._twoFaHelper.CreateDontAskCookie(model.Email, model.DontAsk2Fa, contextTenant.Code, int.Parse(userId));

                return Ok(apiResponse);
            }

            if (response.Status == "pending")
            {
                apiResponse.Message = "Invalid code";
            }

            apiResponse.Data = response;

            if (response.VerifyAttemptsCount >= 5 && string.IsNullOrEmpty(apiResponse.Message)) // Make sure not to override verification not found message
            {
                apiResponse.Message = string.Format(Constants.MAX_RESEND_MESSAGE, this._twoFaHelper.TwoFaRecycleMinutes);
            }
            return BadRequest(apiResponse);
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        [ResolveWebTenant]
        [ServiceFilter(typeof(ValidateRecaptchaAttribute))]
        public async Task<IActionResult> Send2FaRequest(Resend2FaModel model)
        {
            var phoneValidRes = this._twoFaHelper.ValidatePhoneNumber(model.PhoneNumber);
            if (phoneValidRes != null)
            {
                return BadRequest(phoneValidRes);
            }

            TenantModel contextTenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            var tenant2FaInfo = await this._tenantConfigService.GetTenant2FaConfigAsync(contextTenant.Id);
            this._twoFactorAuthService.SetServiceSid(tenant2FaInfo.TwilioVerifyServiceId);
            var twoFaResponse = await this._twoFactorAuthService.Create2FaRequestAsync(model.PhoneNumber, model.VerificationSid);

            if (!twoFaResponse.CanSend2Fa)
            {
                var lastLog = await this._otpTracingService.GetLastSendAttemptAsync(model.PhoneNumber, null);
                if (lastLog != null)
                {
                    TwilioTwoFaResponseModel previousResponse = JsonConvert.DeserializeObject<TwilioTwoFaResponseModel>(lastLog.ResponseJson);
                    twoFaResponse.SendCodeAttempts = previousResponse.SendCodeAttempts;
                    twoFaResponse.VerifyAttemptsCount = await this._otpTracingService.GetVerificationAttemptsCountAsync(lastLog.OtpRequestId);
                    twoFaResponse.Next2FaInSeconds = this._twoFaHelper.Resend2FaIntervalSeconds;
                    twoFaResponse.TwoFaRecycleMinutes = this._twoFaHelper.TwoFaRecycleMinutes;
                    twoFaResponse.Sid = lastLog.OtpRequestId;
                    twoFaResponse.DateCreated = lastLog.DateUtc.Value;
                }

                return BadRequest(new ApiResponse()
                {
                    Message = "Frequent OTP attempt found.",
                    Code = Convert.ToString((int)HttpStatusCode.BadRequest),
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Data = twoFaResponse
                });
            }

            if (twoFaResponse.StatusCode == (int) HttpStatusCode.NotFound)
            {
                return BadRequest(new ApiResponse()
                {
                    Code = ((int) HttpStatusCode.BadRequest).ToString(),
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Data = twoFaResponse,
                    Message = "Verification not found."
                });
            }

            if (twoFaResponse.StatusCode == (int) HttpStatusCode.BadRequest)
            {
                return BadRequest(new ApiResponse()
                {
                    Code = ((int) HttpStatusCode.BadRequest).ToString(),
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Data = twoFaResponse,
                    Message = twoFaResponse.ErrorMessage
                });
            }
            string message = "OTP Created.";
            string apiMessage = null;
            if ((twoFaResponse.SendCodeAttempts != null))
            {
                if (twoFaResponse.SendCodeAttempts.Count > 1)
                {
                    if (twoFaResponse.Status == "max_attempts_reached")
                    {
                        message = "Max attempts reached";
                        apiMessage = string.Format(Constants.MAX_RESEND_MESSAGE, this._twoFaHelper.TwoFaRecycleMinutes);
                    }
                    else
                    {
                        message = "OTP Resend.";
                    }
                }
            }

            await this._otpTracingService.Create2FaLogAsync(model.PhoneNumber,
                null, null, model.Email, message, twoFaResponse.Sid, contextTenant, twoFaResponse);
            ApiResponse response = new ApiResponse()
            {
                Status = twoFaResponse.Status,
                Data = new
                {
                    TwoFaResponse = twoFaResponse,
                    CookiePath = $"/{contextTenant.Branches[0].Code}/"
                },
                Message = apiMessage
            };

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        [ResolveWebTenant]
        [ServiceFilter(typeof(ValidateRecaptchaAttribute))]
        public async Task<IActionResult> Verify2FaRequest(Verify2FaModel model)
        {
            var phoneValidRes = this._twoFaHelper.ValidatePhoneNumber(model.PhoneNumber);
            if (phoneValidRes != null)
            {
                return BadRequest(phoneValidRes);
            }

            if (await this._otpTracingService.VerifyPhoneSidCombination(model.PhoneNumber, model.RequestSid) == false)
            {
                return BadRequest(new ApiResponse()
                {
                    Message = "Verification detail not found.",
                    Code = Convert.ToString((int)HttpStatusCode.BadRequest),
                    Status = HttpStatusCode.BadRequest.ToString()
                });
            }

            ApiResponse apiResponse = new ApiResponse()
            {
                Status = HttpStatusCode.BadRequest.ToString(),
                Code = ((int) HttpStatusCode.BadRequest).ToString(),
            };
            TenantModel contextTenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            var tenant2FaInfo = await this._tenantConfigService.GetTenant2FaConfigAsync(contextTenant.Id);
            this._twoFactorAuthService.SetServiceSid(tenant2FaInfo.TwilioVerifyServiceId);
            var res = await this._twoFactorAuthService.Verify2FaRequestAsync(model.Code, model.RequestSid);
            //TODO: insert/update phone number in contactphoneinfo with isValid true
            if (res.StatusCode == (int) HttpStatusCode.NotFound)
            {
                apiResponse.Message = "Verification not found.";
            }
            else
            {
                await this._otpTracingService.Create2FaLogAsync(model.PhoneNumber,
                    model.Code, null, model.Email, res.Status, model.RequestSid, contextTenant, res);
            }
            res.VerifyAttemptsCount = await this._otpTracingService.GetVerificationAttemptsCountAsync(model.RequestSid);
            res.OtpValidTill = res.DateCreated.AddMinutes(10);
            if (res.Status == "approved")
            {
                //return Ok(new ApiResponse()
                //{
                //    Status = HttpStatusCode.OK.ToString(),
                //    Code = ((int)HttpStatusCode.OK).ToString(),
                //    Data = res
                //});
                apiResponse.Status = HttpStatusCode.OK.ToString();
                apiResponse.Code = ((int) HttpStatusCode.OK).ToString();
                return Ok(apiResponse);
            }

            return BadRequest(new ApiResponse()
            {
                Status = HttpStatusCode.BadRequest.ToString(),
                Code = ((int)HttpStatusCode.BadRequest).ToString(),
                Data = res
            });
        }

        [AllowAnonymous]
        [HttpGet("[action]")]
        [ResolveWebTenant]
        [ServiceFilter(typeof(ValidateRecaptchaAttribute))]
        public async Task<IActionResult> GetTenant2FaConfig(bool getCustomerConfig)
        {
            TenantModel contextTenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            var tenantConfig = await this._tenantConfigService.GetTenant2FaConfigAsync(contextTenant.Id, new List<TwoFaConfigEntities>()
            {
                TwoFaConfigEntities.Tenant
            });

            ApiResponse apiResponse = new ApiResponse();
            apiResponse.Code = Convert.ToString((int) HttpStatusCode.OK);
            apiResponse.Status = HttpStatusCode.OK.ToString();
            apiResponse.Data = new
            {
                TenantTwoFaStatus = getCustomerConfig == true
                    ? tenantConfig.BorrowerTwoFaModeId
                    : tenantConfig.McuTwoFaModeId
            };

            return Ok(apiResponse);
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        [ResolveWebTenant]
        [ServiceFilter(typeof(ValidateRecaptchaAttribute))]
        public async Task<IActionResult> Skip2FaSignIn()
        {
            ApiResponse apiResponse = new ApiResponse()
            {
                Code = Convert.ToString((int) HttpStatusCode.BadRequest),
                Status = HttpStatusCode.BadRequest.ToString()
            };

            TenantModel contextTenant = HttpContext.Items[Constants.COLABA_TENANT] as TenantModel;
            var token = Convert.ToString(Request.Headers["Authorization"]);
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new ApiResponse()
                {
                    Code = Convert.ToString((int)HttpStatusCode.Unauthorized),
                    Status = HttpStatusCode.Unauthorized.ToString()
                });
            }
            var principal = await this._tokenService.Validate2FaTokenAsync(token.Split(' ')[1]);
            if (principal == null)
            {
                return Unauthorized(new ApiResponse()
                {
                    Code = Convert.ToString((int)HttpStatusCode.Unauthorized),
                    Status = HttpStatusCode.Unauthorized.ToString()
                });
            }
            var userId = principal.Claims.Where(c => c.Type == "UserProfileId").FirstOrDefault().Value;
            await this._accountService.Set2FaForUserAsync(contextTenant.Id, int.Parse(userId), false);
            apiResponse = await this._accountService.GenerateNewAccessToken(int.Parse(userId), contextTenant.Id,
                contextTenant.Branches[0].Code);

            return Ok(apiResponse);
        }

        [AllowAnonymous]
        [HttpGet("[action]")]
        [ResolveWebTenant]
        [ServiceFilter(typeof(ValidateRecaptchaAttribute))]
        public async Task<IActionResult> Get2FaIntervalValue()
        {
            var val = this._twoFaHelper.Resend2FaIntervalSeconds;
            return Ok(new ApiResponse()
            {
                Status = HttpStatusCode.OK.ToString(),
                Code = Convert.ToString((int) HttpStatusCode.OK),
                Data = new
                {
                    Resend2FaIntervalSeconds = val
                }
            });
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        [ResolveWebTenant]
        [ServiceFilter(typeof(ValidateRecaptchaAttribute))]
        public async Task<IActionResult> DontAsk2FA()
        {
            TenantModel contextTenant = HttpContext.Items[Constants.COLABA_TENANT] as TenantModel;
            string token = Convert.ToString(Request.Headers["Authorization"]);
            var apiResponse = await this._twoFaHelper.CreateDontAsk2FaCookieFromToken(token, contextTenant.Id, contextTenant.Code);
            if (apiResponse.Code != Convert.ToString((int)HttpStatusCode.OK))
            {
                return Unauthorized(apiResponse);
            }

            return Ok(apiResponse);
        }
    }
}
