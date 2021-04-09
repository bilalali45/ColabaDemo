using System;
using System.Collections.Generic;
using System.Text;

namespace TokenCacheHelper.Models
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
        public DateTime RefreshTokenValidTo { get; set; }
    }
}
