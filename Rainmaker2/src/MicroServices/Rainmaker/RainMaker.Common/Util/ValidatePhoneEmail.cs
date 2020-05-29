using System;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RainMaker.Common.Util
{
    /// <summary>
    /// http://www.email-validator.net/
    //  http://www.phone-validator.net/
    /// </summary>
    public static class ValidatePhoneEmail
    {
        private const string EmailApiUrl = "http://api1.email-validator.net/api/verify?APIKey={0}&EmailAddress={1}";
        private const string PhoneApiUrl = "http://api1.phone-validator.net/api/v2/verify?APIKey={0}&PhoneNumber={1}&CountryCode={2}";
        //private const string ApiUsername = "username";
        //private const string ApiPassword = "password";
        private const string ApiKey = "";

        /// <summary>
        /// Verify single phone number
        /// </summary>
        public static async Task<int> ValidatePhoneNumberAsync(string phoneNumber, string countryCode)
        {
            return await GetVerificationStatusAsync(string.Format(PhoneApiUrl, ApiKey, phoneNumber, countryCode));
        }

        /// <summary>
        /// Verify single email address
        /// </summary>
        public static async Task<int> ValidateEmailAddressAsync(string emailAddress)
        {
            return await GetVerificationStatusAsync(string.Format(EmailApiUrl, ApiKey, emailAddress));
        }

        private static async Task<int> GetVerificationStatusAsync(string url)
        {
            try
            {
                var webClient = new WebClient();
                var responseJson = await webClient.DownloadStringTaskAsync(url);

                var result = JsonConvert.DeserializeObject<ApiResult>(responseJson);

                switch (result.Status.ToLower())
                {
                    case "200":
                    case "207":
                    case "215":
                        return (int) ValidityId.Yes;
                    case "114": // Validation Delayed
                    case "118": // Rate Limit Exceeded
                    case "119": // API Key Invalid or Depleted
                    case "121": // Task Accepted
                        return (int) ValidityId.Unknown;
                    case "401": // Bad Address
                    case "404": // Domain Not Fully Qualified
                    case "406": // MX Lookup Error
                    case "409": // No-Reply Address
                    case "410": // Address Rejected
                    case "413": // Server Unavailable
                    case "414": // Address Unavailable	
                    case "420": // Domain Name Misspelled
                        return (int) ValidityId.No;
                    case "302": // Local Address 
                    case "303": // IP Address Literal
                    case "305": // Disposable Address
                    case "308": // Role Address
                    case "313": // Server Unavailable
                    case "314": // Address Unavailable
                    case "316": // Duplicate Address
                    case "317": // Server Reject
                        return (int) ValidityId.Unknown;
                    case "valid_confirmed":
                        return (int) ValidityId.Yes;
                    case "valid_unconfirmed":
                        return (int) ValidityId.Unknown;
                    case "invalid":
                        return (int) ValidityId.No;
                    default:
                        return (int) ValidityId.Unknown;
                }
            }
            catch (WebException)
            {
                return (int) ValidityId.Unknown;
            }
        }
    }

    internal class ApiResult
    {
        public string Status { get; set; }
    }
}

