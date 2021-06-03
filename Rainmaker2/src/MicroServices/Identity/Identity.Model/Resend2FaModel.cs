using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Identity.Model
{
    public class Resend2FaModel
    {
        [Required]
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string VerificationSid { get; set; }
    }
}
