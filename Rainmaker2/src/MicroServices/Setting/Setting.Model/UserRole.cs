using System.ComponentModel.DataAnnotations;

namespace Setting.Model
{
    public class UserRole
    {
        [Required(ErrorMessage = ValidationMessages.ValidationFailed)]
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = ValidationMessages.ValidationFailed)]
        public int RoleId { get; set; }
        [Required(ErrorMessage = ValidationMessages.ValidationFailed)]
        public string RoleName { get; set; }
        [Required(ErrorMessage = ValidationMessages.ValidationFailed)]
        public bool IsRoleAssigned { get; set; }
    }
}
