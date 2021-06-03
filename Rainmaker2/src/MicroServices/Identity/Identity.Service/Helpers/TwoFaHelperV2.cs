using Identity.Service.Helpers.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using Identity.Data;
using Identity.Entity.Models;
using Microsoft.AspNetCore.Http;
using TenantConfig.Common.DistributedCache;
using URF.Core.Abstractions;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Identity.Model;
using RestSharp.Extensions;
using TenantConfig.Common;

namespace Identity.Service.Helpers
{
    public class  TwoFaHelperV2 : ITwoFaHelperV2
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork<IdentityContext> _identityUow;

        public TwoFaHelperV2(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IUnitOfWork<IdentityContext> identityUow)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _identityUow = identityUow;
        }

        public int DoNotAsk2FaCookieDays => Read2FaConfig<int>("DontAsk2FaCookieDays", 180);
        public int Resend2FaIntervalSeconds => Read2FaConfig<int>("Resend2FaIntervalSeconds", 120);
        public int OtpLength => Read2FaConfig<int>("OtpLength", 6);
        public string TwilioEndPoint => Read2FaConfig<string>("TwilioEndPoint", "https://verify.twilio.com/v2");

        public T Read2FaConfig<T>(string key, T defaultValue)
        {
            string configVal = _configuration[$"TwoFaSettings:{key}"];
            if (string.IsNullOrEmpty(configVal))
            {
                return defaultValue;
            }
            return (T)Convert.ChangeType(configVal, typeof(T));
        }

        public bool TwoFaCookieExists(string tenantCode, int userId)
        {
            string twoFaCookieName = this.CreateCookieName(tenantCode, userId);
            var cookie = this._httpContextAccessor.HttpContext.Request.Cookies[twoFaCookieName];
            return cookie != null;
        }

        public string CreateCookieName(string tenantCode, int userId)
        {
            var cookieName = $"{Constants.DONT_ASK_COOKIE_NAME}_{tenantCode}_{userId}";
            var partToHash = $"{tenantCode}_{userId}";
            var userProfile = _identityUow.Repository<User>().Query(u => u.Id == userId)
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

        public ApiResponse ValidatePhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return new ApiResponse()
                {
                    Message = "Phone number is required to verify 2FA.",
                    Code = Convert.ToString((int)HttpStatusCode.BadRequest),
                    Status = HttpStatusCode.BadRequest.ToString()
                };
            }

            if (!PhoneHelper.IsValidUnmasked(phoneNumber.Replace("+1", string.Empty)))
            {
                return new ApiResponse()
                {
                    Message = "Invalid phone number",
                    Code = Convert.ToString((int)HttpStatusCode.BadRequest),
                    Status = HttpStatusCode.BadRequest.ToString()
                };
            }

            return null;
        }
    }
}
