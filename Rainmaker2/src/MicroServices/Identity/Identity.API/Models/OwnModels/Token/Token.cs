using System;

namespace Identity.Models.OwnModels.Token
{
    public class xToken
    {
        public long Id { get; set; } // Id (Primary key)
        public int? UserId { get; set; } // UserId
        public string AccessToken { get; set; } // AccessToken
        public string RefreshToken { get; set; } // RefreshToken (length: 50)
        public DateTime? RefreshTokenExpiry { get; set; } // RefreshTokenExpiry
    }
}