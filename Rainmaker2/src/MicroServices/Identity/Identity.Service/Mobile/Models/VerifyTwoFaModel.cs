using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Identity.Service.Mobile.Models
{
    public class VerifyTwoFaModel
    {
        [Required(ErrorMessage = "Phone number is required.")]
        public string PhoneNumber { get; set; }
        //[Required(ErrorMessage = "Verification Sid is required.")]
        public string VerificationSid { get; set; }
        [Required(ErrorMessage = "Otp is required.")]
        public string Otp { get; set; }
    }
}
