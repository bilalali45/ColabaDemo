using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TenantConfig.Common;

namespace Identity.Model
{
    public class DeleteUserModel
    {
        public string Email { get; set; }
    }
    public class ChangePasswordModel
    {
        [Required]
        public string oldPassword { get; set; }
        [Required]
        [PasswordValidator]
        public  string newPassword { get; set; }
    }

    public class ForgotPasswordResponseModel
    {
        [Required]
        [PasswordValidator]
        public string Password { get; set; }
        [Required]
        public string Key { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
