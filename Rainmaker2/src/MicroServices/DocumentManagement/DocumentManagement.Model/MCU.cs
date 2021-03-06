using System.ComponentModel.DataAnnotations;

namespace DocumentManagement.Model
{
    public class McuRenameModel
    {
        [Required(ErrorMessage = ValidationMessages.ValidationFailed)]
        [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = ValidationMessages.ValidationFailed)]
        public string id { get; set; }
        [Required(ErrorMessage = ValidationMessages.ValidationFailed)]
        [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = ValidationMessages.ValidationFailed)]
        public string requestId { get; set; }
        [Required(ErrorMessage = ValidationMessages.ValidationFailed)]
        [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = ValidationMessages.ValidationFailed)]
        public string docId { get; set; }
        [Required(ErrorMessage = ValidationMessages.ValidationFailed)]
        [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = ValidationMessages.ValidationFailed)]
        public string fileId { get; set; }
        [Required(ErrorMessage = ValidationMessages.ValidationFailed)]
        public string newName { get; set; }
    }
}
