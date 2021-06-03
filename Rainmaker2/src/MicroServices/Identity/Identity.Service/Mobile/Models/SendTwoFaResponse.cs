// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 

using System;
using System.Collections.Generic;

namespace  Identity.Service.Mobile.Models
{
    public class SendCodeAttempt
    {
        public string attempt_sid { get; set; }
        public string channel { get; set; }
        public DateTime time { get; set; }
    }

    public class Carrier
    {
        public string mobile_country_code { get; set; }
        public string type { get; set; }
        public object error_code { get; set; }
        public string mobile_network_code { get; set; }
        public string name { get; set; }
    }

    public class Lookup
    {
        public Carrier carrier { get; set; }
    }

    public class SendTwoFaResponse
    {
        public string status { get; set; }
        public object payee { get; set; }
        public DateTime date_updated { get; set; }
        public List<SendCodeAttempt> send_code_attempts { get; set; }
        public string account_sid { get; set; }
        public string to { get; set; }
        public object amount { get; set; }
        public bool valid { get; set; }
        public Lookup lookup { get; set; }
        public string url { get; set; }
        public string sid { get; set; }
        public DateTime? date_created { get; set; }
        public string service_sid { get; set; }
        public string channel { get; set; }
    }
}