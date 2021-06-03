using System.ComponentModel.DataAnnotations;

namespace Identity.Service.Mobile.Models
{
    public class SigninModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
