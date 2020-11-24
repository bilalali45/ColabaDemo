using System.ComponentModel.DataAnnotations;

namespace Setting.Model
{
    public class SettingModel
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int? UserId { get; set; }
        public short DeliveryModeId { get; set; }
        public int NotificationMediumId { get; set; }
        public int NotificationTypeId { get; set; }
        public short? DelayedInterval { get; set; }
        public string NotificationType { get; set; }
    }
    public class UpdateSettingModel
    {
        [Required(ErrorMessage = ValidationMessages.ValidationFailed)]
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = ValidationMessages.ValidationFailed)]
        public int notificationTypeId { get; set; }
        [Required(ErrorMessage = ValidationMessages.ValidationFailed)]
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = ValidationMessages.ValidationFailed)]
        public short deliveryModeId { get; set; }
        public short? delayedInterval { get; set; }
    }
}
