using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Identity.Model.TwoFA
{
    //public class TwoFaBase
    //{

    //    public string Sid { get; set; }
    //}

    ////public interface ITwoFaResponseModel
    ////{
    ////    string Sid { get; set; }
    ////    string Status { get; set; }
    ////    int AttemptsCount { get; set; }
    ////}

    //public class SendCodeAttempt
    //{
    //    [JsonPropertyName("attempt_sid")]
    //    public string AttemptSid { get; set; }

    //    [JsonPropertyName("channel")]
    //    public string Channel { get; set; }

    //    [JsonPropertyName("time")]
    //    public DateTime Time { get; set; }
    //}

    //public class Carrier
    //{
    //    [JsonPropertyName("mobile_country_code")]
    //    public string MobileCountryCode { get; set; }

    //    [JsonPropertyName("type")]
    //    public string Type { get; set; }

    //    [JsonPropertyName("error_code")]
    //    public object ErrorCode { get; set; }

    //    [JsonPropertyName("mobile_network_code")]
    //    public string MobileNetworkCode { get; set; }

    //    [JsonPropertyName("name")]
    //    public string Name { get; set; }
    //}

    //public class Lookup
    //{
    //    [JsonPropertyName("carrier")]
    //    public Carrier Carrier { get; set; }
    //}

    //public class TwilioTwoFaResponseModel : TwoFaBase//, ITwoFaResponseModel
    //{

    //    [JsonPropertyName("status")]
    //    public string Status { get; set; }


    //    public int StatusCode { get; set; }

    //    [JsonPropertyName("payee")]

    //    public object Payee { get; set; }

    //    [JsonPropertyName("date_updated")]

    //    public DateTime DateUpdated { get; set; }

    //    [JsonPropertyName("send_code_attempts")]

    //    public List<SendCodeAttempt> SendCodeAttempts { get; set; }


    //    //public int AttemptsCount
    //    //{
    //    //    get
    //    //    {
    //    //        return this.SendCodeAttempts == null ? 0 : this.SendCodeAttempts.Count;
    //    //    }
    //    //    set { }
    //    //}

    //    [JsonPropertyName("account_sid")]
    //    public string AccountSid { get; set; }

    //    [JsonPropertyName("to")]

    //    public string To { get; set; }

    //    [JsonPropertyName("amount")]
    //    public object Amount { get; set; }

    //    [JsonPropertyName("valid")]
    //    public bool Valid { get; set; }

    //    [JsonPropertyName("lookup")]
    //    public Lookup Lookup { get; set; }

    //    [JsonPropertyName("url")]
    //    public string Url { get; set; }


    //    [JsonPropertyName("sid")]
    //    public string Sid { get; set; }


    //    [JsonPropertyName("date_created")]
    //    public DateTime DateCreated { get; set; }

    //    [JsonPropertyName("service_sid")]
    //    public string ServiceSid { get; set; }

    //    [JsonPropertyName("channel")]
    //    public string Channel { get; set; }


    //}

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class SendCodeAttempt
    {
        [JsonProperty("attempt_sid")]
        public string AttemptSid { get; set; }

        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("time")]
        public DateTime Time { get; set; }
    }

    public class Carrier
    {
        [JsonProperty("mobile_country_code")]
        public string MobileCountryCode { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("error_code")]
        public object ErrorCode { get; set; }

        [JsonProperty("mobile_network_code")]
        public string MobileNetworkCode { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Lookup
    {
        [JsonProperty("carrier")]
        public Carrier Carrier { get; set; }
    }

    public class TwilioTwoFaResponseModel : CanSend2FaModel
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("payee")]
        [IgnoreDataMember()]
        public object Payee { get; set; }

        [JsonProperty("date_updated")]
        public DateTime? DateUpdated { get; set; }

        [JsonProperty("send_code_attempts")]
        public List<SendCodeAttempt> SendCodeAttempts { get; set; }

        [JsonProperty("verify_attempts_count")]
        public int VerifyAttemptsCount { get; set; }

        [IgnoreDataMember()]
        [JsonProperty("account_sid")]
        public string AccountSid { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }

        [IgnoreDataMember()]
        [JsonProperty("amount")]
        public object Amount { get; set; }

        [JsonProperty("valid")]
        public bool Valid { get; set; }

        [DataMember()]
        [JsonProperty("lookup")]
        public Lookup Lookup { get; set; }

        [IgnoreDataMember()]
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("sid")]
        public string Sid { get; set; }
        
        [JsonProperty("date_created")]
        public DateTime DateCreated { get; set; }

        [JsonProperty("otp_valid_till")]
        public DateTime OtpValidTill { get; set; }

        [JsonProperty("twoFaRecycleSeconds")]
        public int? TwoFaRecycleSeconds { get; set; }

        [IgnoreDataMember()]
        [JsonProperty("service_sid")]
        public string ServiceSid { get; set; }

        [IgnoreDataMember()]
        [JsonProperty("channel")]
        public string Channel { get; set; }


        public int StatusCode { get; set; }


    }


}
