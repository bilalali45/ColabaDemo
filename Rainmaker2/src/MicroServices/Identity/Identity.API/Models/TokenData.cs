using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Models
{
    [Serializable]
    public class TokenData
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public long UserProfileId { get; set; }
        public string UserName { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
