using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace Identity.Models
{
    public class GenerateTokenRequest
    {
        [Required]
        [StringLength(256,ErrorMessage = "Validation failed")]
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Employee { get; set; }
        
    }
}
