using System.ComponentModel.DataAnnotations;

namespace Identity.Models
{
    public class RefreshTokenRequest
    {
        [RegularExpression(pattern: @"^[A-Za-z0-9-_=]+\.[A-Za-z0-9-_=]+\.?[A-Za-z0-9-_.+/=]*$",ErrorMessage = "Validation failed") ]
        public string Token { get; set; }

        [RegularExpression(pattern: @"^(?:[A-Za-z0-9+/]{4})*(?:[A-Za-z0-9+/]{2}==|[A-Za-z0-9+/]{3}=)?$", ErrorMessage = "Validation failed")]
        public string RefreshToken { get; set; }
    }
}