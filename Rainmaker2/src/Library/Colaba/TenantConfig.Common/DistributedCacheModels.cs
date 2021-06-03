using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TenantConfig.Common.DistributedCache
{
    public enum OwnType
    {
        Primary=1,
        Secondary=2
    }
    public class VerifyModel
    {
        public string Url { get; set; }
        public string Path { get; set; }
    }
    public static class Constants
    {
        public const string TENANTS = "TenantConfig#Tenants";
        public const string URL_PREFIX = "TenantConfig#";
        public const string COLABA_WEB_URL_HEADER = "ColabaWebUrl";
        public const string COLABA_TENANT = "ColabaTenant";
        public const string COOKIE_NAME = "verifiedUrl";
        public const string SPA_PATH = "app";
        public const string CDN_PATH = "colabacdn";
        public const string RECAPTCHA_CODE = "RecaptchaCode";
        public const string DONT_ASK_COOKIE_NAME = "__Secure-DontAsk2Fa";

        public const string MAX_VERIFICATION_MESSAGE = "Max verification attempt(s) reached. Please try again after {0} minute(s).";
        public const string MAX_RESEND_MESSAGE = "Max resend attempt(s) reached. Please try again after {0} minute(s).";
    }
    public enum TenantUrlType
    {
        Customer=1,
        Admin=2
    }
    public class TenantModel
    {
        [JsonRequired]
        public int Id { get; set; }
        [JsonRequired]
        public string Code { get; set; }
        [JsonRequired]
        public List<BranchModel> Branches { get; set; }
        [JsonRequired]
        public List<UrlModel> Urls { get; set; }
    }
   
    public class UrlModel
    {
        [JsonRequired]
        public string Url { get; set; }
        [JsonRequired]
        public TenantUrlType Type { get; set; }
    }
    public class BranchModel
    {
        [JsonRequired]
        public int Id { get; set; }
        [JsonRequired]
        public string Code { get; set; }
        [JsonRequired]
        public List<LoanOfficerModel> LoanOfficers { get; set; }
        [JsonRequired]
        public bool IsCorporate { get; set; }
    }

    public class LoanOfficerModel
    {
        [JsonRequired]
        public int Id { get; set; }
        [JsonRequired]
        public string Code { get; set; }
    }
}
