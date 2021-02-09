using System;
using System.Collections.Generic;
using System.Text;

namespace Setting.Model
{
    public class AuthorizeRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Employee { get; set; }
    }
    public class Data
    {
        public string token { get; set; }
        public string refreshToken { get; set; }
        public int userProfileId { get; set; }
        public string userName { get; set; }
        public DateTime validFrom { get; set; }
        public DateTime validTo { get; set; }
    }

    public class AuthorizeResponse
    {
        public string status { get; set; }
        public Data data { get; set; }
        public string message { get; set; }
        public string code { get; set; }
    }
}
