using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Models.TwoFA
{
    public class Verify2FaModel
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string RequestSid { get; set; }
        [Required]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public bool DontAsk2Fa { get; set; }
        public bool MapPhoneNumber { get; set; }
    }
}
