using System.ComponentModel.DataAnnotations;
using TenantConfig.Common;

namespace Identity.Model
{
    public class RegisterModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [RegularExpression(@"^[A-Za-z0-9._-]+@[A-Za-z0-9-]+\.[A-Za-z]{2,}$")]
        public string Email { get; set; }
        public string Phone { get; set; }
        [Required]
        [PasswordValidator]
        public string Password { get; set; }
        public bool DontAsk2Fa { get; set; }
        public bool MapPhoneNumber { get; set; }
        public string RequestSid { get; set; }
        public bool Skipped2Fa { get; set; }
    }

    public class SigninModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public bool IsDevMode { get; set; }
    }

    public class ForgotPasswordRequestModel
    {
        [Required]
        public string Email { get; set; }
    }
}
