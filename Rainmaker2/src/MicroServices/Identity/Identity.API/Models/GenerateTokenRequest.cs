using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace Identity.Models
{
    public class GenerateTokenRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Employee { get; set; }
        
    }
}
