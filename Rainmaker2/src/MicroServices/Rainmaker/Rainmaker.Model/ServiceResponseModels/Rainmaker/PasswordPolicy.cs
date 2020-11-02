namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class PasswordPolicy
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int MinimumLength { get; set; }
        public int MaximumLength { get; set; }
        public int PasswordExpiryInDays { get; set; }
        public int PasswordHistoryCount { get; set; }
        public bool ViewPassword { get; set; }
        public bool RecaptchaEnabled { get; set; }
        public string RecaptchaClientKey { get; set; }
        public string RecaptchaServerKey { get; set; }
        public int IncorrectPasswordCount { get; set; }
        public bool Enable2Fa { get; set; }
        public int? AccountLockDurationInMinutes { get; set; }
        public int? ForgotTokenExpiryInMinutes { get; set; }
    }
}