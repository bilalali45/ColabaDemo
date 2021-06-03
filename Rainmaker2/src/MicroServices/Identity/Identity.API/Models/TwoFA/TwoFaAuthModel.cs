using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Models.TwoFA
{
    public class TwoFaAuthModel
    {
        public string Status { get; set; }
        public string TokenData { get; set; }
        public bool RequirsTwoFa { get; set; }
        public bool PhoneNoMissing { get; set; }
        public string VerificationSid { get; set; }
    }
}
