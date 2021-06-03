using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Identity.Data;
using Identity.Entity.Models;
using Identity.Model;
using Identity.Model.TwoFA;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RestSharp.Extensions;
using TenantConfig.Common;
using TenantConfig.Common.DistributedCache;
using URF.Core.Abstractions;

namespace Identity.Service
{
    public interface ITwoFaHelper
    {
        int DontAsk2FaCookieDays { get; }
        int TwoFaRecycleMinutes { get; }
        int Resend2FaIntervalSeconds { get; }
        int SendDigits { get; }

        void CreateDontAskCookie(string email, bool dontAskFlag, string tenantCode, int userId);
        string CreateCookieName(string tenantCode, int userId);
        T Read2FaConfig<T>(string key, T defaultValue);
        Task<CanSend2FaModel> CanSendOtpAsync(string phoneNumber, string sId);

        ApiResponse ValidatePhoneNumber(string phoneNumber);
        Task<ApiResponse> CreateDontAsk2FaCookieFromToken(string token, int tenantId, string tenantCode);
    }
    public class TwoFaHelper : ITwoFaHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly IOtpTracingService _otpTracingService;
        private readonly IKeyStoreService _keyStoreService;
        private readonly IUnitOfWork<IdentityContext> _uow;

        public int DontAsk2FaCookieDays => Read2FaConfig<int>("DontAsk2FaCookieDays", 180);
        public int TwoFaRecycleMinutes => Read2FaConfig<int>("TwoFaRecycleMinutes", 10);
        public int Resend2FaIntervalSeconds => Read2FaConfig<int>("Resend2FaIntervalSeconds", 120);
        public int SendDigits => Read2FaConfig<int>("SendDigits", 6);


        public TwoFaHelper(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IOtpTracingService otpTracingService, IKeyStoreService keyStoreService, IUnitOfWork<IdentityContext> previousUow)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _otpTracingService = otpTracingService;
            _keyStoreService = keyStoreService;
            this._uow = previousUow;
        }

        public void CreateDontAskCookie(string email, bool dontAskFlag, string tenantCode, int userId)
        {
            if (dontAskFlag)
            {
                CookieOptions cookieOptions = new CookieOptions()
                {
                    //Path = $"/{branchCode}/",
                    Path = "/api/identity/customeraccount/signin",
                    Expires = DateTime.Now.AddYears(this.DontAsk2FaCookieDays),
                    IsEssential = false,
                    HttpOnly = true, // TODO
                    Secure = true // TODO
                };

                this._httpContextAccessor.HttpContext.Response.Cookies.Append(CreateCookieName(tenantCode, userId), email.ComputeHash(SHA256.Create()), cookieOptions);
            }
        }

        public string CreateCookieName(string tenantCode, int userId)
        {
            var cookieName = $"{Constants.DONT_ASK_COOKIE_NAME}_{tenantCode}_{userId}";
            var partToHash = $"{tenantCode}_{userId}";
            var userProfile = _uow.Repository<User>().Query(u => u.Id == userId)
                .FirstOrDefault();
            if (userProfile != null && userProfile.PasswordSalt.HasValue())
            {
                HashAlgorithm algorithm = new SHA256Managed();
                var plainText = Encoding.ASCII.GetBytes(partToHash);
                var salt = Encoding.ASCII.GetBytes(userProfile.PasswordSalt);

                byte[] plainTextWithSaltBytes =
                    new byte[plainText.Length + salt.Length];

                for (int i = 0; i < plainText.Length; i++)
                {
                    plainTextWithSaltBytes[i] = plainText[i];
                }
                for (int i = 0; i < salt.Length; i++)
                {
                    plainTextWithSaltBytes[plainText.Length + i] = salt[i];
                }

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < plainTextWithSaltBytes.Length; i++)
                {
                    sb.Append(plainTextWithSaltBytes[i].ToString("x2"));
                }
                //string hashString = Encoding.ASCII.GetString(algorithm.ComputeHash(plainTextWithSaltBytes));
                string hashString = sb.ToString();
                cookieName = $"{Constants.DONT_ASK_COOKIE_NAME}_{hashString}";
            }
            return cookieName;
        }

        public T Read2FaConfig<T>(string key, T defaultValue)
        {
            string configVal = _configuration[$"TwoFaSettings:{key}"];
            if (string.IsNullOrEmpty(configVal))
            {
                return defaultValue;
            }
            return (T)Convert.ChangeType(configVal, typeof(T));
        }

        public async Task<CanSend2FaModel> CanSendOtpAsync(string phoneNumber, string sId)
        {
            CanSend2FaModel model = new CanSend2FaModel()
            {
                CanSend2Fa = true,
                Next2FaInSeconds = this.Resend2FaIntervalSeconds
            };
            var lastOtpLog = await this._otpTracingService.GetLastSendAttemptAsync(phoneNumber, sId);
            if (lastOtpLog != null)
            {
                var secondsToAdd = this.Read2FaConfig("Resend2FaIntervalSeconds", 120);
                DateTime currentTime = DateTime.UtcNow;
                var diffInSeconds = (currentTime - lastOtpLog.DateUtc.Value).TotalSeconds;
                model.CanSend2Fa = diffInSeconds > this.Resend2FaIntervalSeconds;
                model.TwoFaRecycleMinutes = this.TwoFaRecycleMinutes;
                if (!model.CanSend2Fa)
                {
                    model.LastOtpAttempt = lastOtpLog.OtpCreatedOn;
                    model.OtpValidity = lastOtpLog.OtpCreatedOn.Value.AddMinutes(this.TwoFaRecycleMinutes);
                    model.ErrorMessage = "Frequent OTP attempt found.";
                }
                //DateTime nextResendTime =
                //    lastOtpLog.DateUtc.Value.AddSeconds(secondsToAdd);
                //if (currentTime < nextResendTime)
                //{
                //    model.TwoFaSent = false;
                //    model.Next2FaInSeconds = Math.Round((nextResendTime - currentTime).TotalSeconds);
                //}
            }
            return model;
        }

        public ApiResponse ValidatePhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return new ApiResponse()
                {
                    Message = "Phone number is required to verify 2FA.",
                    Code = Convert.ToString((int) HttpStatusCode.BadRequest),
                    Status = HttpStatusCode.BadRequest.ToString()
                };
            }

            if (!PhoneHelper.IsValidUnmasked(phoneNumber.Replace("+1", string.Empty)))
            {
                return new ApiResponse()
                {
                    Message = "Invalid phone number",
                    Code = Convert.ToString((int) HttpStatusCode.BadRequest),
                    Status = HttpStatusCode.BadRequest.ToString()
                };
            }

            return null;
        }

        public async Task<ApiResponse> CreateDontAsk2FaCookieFromToken(string token, int tenantId, string tenantCode)
        {
            if (string.IsNullOrEmpty(token))
            {
                return new ApiResponse()
                {
                    Code = Convert.ToString((int) HttpStatusCode.Unauthorized),
                    Status = HttpStatusCode.Unauthorized.ToString()
                };
            }

            token = token.Split(' ')[1];
            if (string.IsNullOrEmpty(token))
            {
                return new ApiResponse()
                {
                    Code = Convert.ToString((int)HttpStatusCode.Unauthorized),
                    Status = HttpStatusCode.Unauthorized.ToString()
                };
            }

            //security key
            var securityKey = await _keyStoreService.GetJwtSecurityKeyAsync();
            //symmetric security key
            var symmetricSecurityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key: Encoding.UTF8.GetBytes(s: securityKey));
            //signing credentials
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = symmetricSecurityKey,
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
            {
                return new ApiResponse()
                {
                    Code = Convert.ToString((int)HttpStatusCode.Unauthorized),
                    Status = HttpStatusCode.Unauthorized.ToString(),
                    Message =  "Invalid token."
                };
            }

            var userIdClaim = principal.Claims.FirstOrDefault(c => c.Type == "UserId");
            if (userIdClaim == null)
            {
                return new ApiResponse()
                {
                    Code = Convert.ToString((int)HttpStatusCode.Unauthorized),
                    Status = HttpStatusCode.Unauthorized.ToString(),
                    Message = "Invalid token."
                };
            }

            var userProfile = await _uow.Repository<User>().Query(x =>  x.TenantId == tenantId && x.Id == int.Parse(userIdClaim.Value) && x.UserTypeId == (byte)TenantConfig.Common.UserType.Customer && x.IsActive).FirstOrDefaultAsync();
            if (userProfile == null)
            {
                return new ApiResponse()
                {
                    Code = Convert.ToString((int)HttpStatusCode.Unauthorized),
                    Status = HttpStatusCode.Unauthorized.ToString(),
                    Message = "User does not exist."
                };
            }

            this.CreateDontAskCookie(userProfile.UserName, true, tenantCode, int.Parse(userIdClaim.Value));
            return new ApiResponse()
            {
                Message = "Dont ask 2FA cookie created.",
                Code = Convert.ToString((int) HttpStatusCode.OK),
                Status = HttpStatusCode.OK.ToString()
            };
        }
    }
}
