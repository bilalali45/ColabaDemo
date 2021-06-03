using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;
using System.Text;

namespace Identity.Model.TwoFA
{
    public class CanSend2FaModel
    {
        public bool CanSend2Fa { get; set; }
        public double Next2FaInSeconds { get; set; }
        public int TwoFaRecycleMinutes { get; set; }
        [IgnoreDataMember()]
        public DateTime? LastOtpAttempt { get; set; }
        [IgnoreDataMember()]
        public string ErrorMessage { get; set; }
        [IgnoreDataMember()]
        public DateTime? OtpValidity { get; set; }
    }
}
