using System;
using System.Collections.Generic;
using System.Text;
using Identity.Model;

namespace Identity.Service.Helpers.Interfaces
{
    public  interface ITwoFaHelperV2
    {
        int DoNotAsk2FaCookieDays { get; }
        int Resend2FaIntervalSeconds { get; }
        int OtpLength { get; }
        string TwilioEndPoint { get; }

        T Read2FaConfig<T>(string key, T defaultValue);
        string CreateCookieName(string tenantCode, int userId);
        bool TwoFaCookieExists(string tenantCode, int userId);
        ApiResponse ValidatePhoneNumber(string phoneNumber);
    }
}
