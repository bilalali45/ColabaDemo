using System;
using System.ComponentModel.DataAnnotations;

namespace Identity.Models
{
    public class AuthorizePermanentRequest
    {
        [Required]
        [StringLength(256,ErrorMessage = "Validation failed")]
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Employee { get; set; }
        public DateTime ExpiryDate { get; set; }
        
    }
}
