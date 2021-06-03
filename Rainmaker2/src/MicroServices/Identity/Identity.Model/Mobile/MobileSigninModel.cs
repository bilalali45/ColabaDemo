using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Identity.Model.Mobile
{
    public class MobileSigninModel
    {
        [Required(ErrorMessage = "Username is required.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
