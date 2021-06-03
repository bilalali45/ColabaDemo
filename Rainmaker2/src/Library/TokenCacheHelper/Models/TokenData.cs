using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TokenCacheHelper.Models
{
    public enum TokenType : byte
    {
        AccessToken = 0,
        IntermediateToken = 1
    }

    [Serializable]
    public class TokenData
    {
        public string Token { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string RefreshToken { get; set; }
        public long UserProfileId { get; set; }
        public string UserName { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? RefreshTokenValidTo { get; set; }
        public TokenType TokenType { get; set; }

        public string TokenTypeName
        {
            get
            {
                return Convert.ToString(TokenType);
            }
        }
    }
}
