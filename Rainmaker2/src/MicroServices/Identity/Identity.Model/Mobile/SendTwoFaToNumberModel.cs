using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Identity.Model.Mobile
{
    public class SendTwoFaToNumberModel
    {
        [Required(ErrorMessage = "Phone number is required.")]
        public string PhoneNumber { get; set; }
    }
}
