













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // PasswordPolicy

    public partial class PasswordPolicy 
    {
        public int Id { get; set; } // Id (Primary key)
        public int TenantId { get; set; } // TenantId
        public int MinimumLength { get; set; } // MinimumLength
        public int MaximumLength { get; set; } // MaximumLength
        public int PasswordExpiryInDays { get; set; } // PasswordExpiryInDays
        public int PasswordHistoryCount { get; set; } // PasswordHistoryCount
        public bool ViewPassword { get; set; } // ViewPassword
        public bool RecaptchaEnabled { get; set; } // RecaptchaEnabled
        public string RecaptchaClientKey { get; set; } // RecaptchaClientKey (length: 100)
        public string RecaptchaServerKey { get; set; } // RecaptchaServerKey (length: 100)
        public int IncorrectPasswordCount { get; set; } // IncorrectPasswordCount
        public bool Enable2Fa { get; set; } // Enable2FA
        public int? AccountLockDurationInMinutes { get; set; } // AccountLockDurationInMinutes
        public int? ForgotTokenExpiryInMinutes { get; set; } // ForgotTokenExpiryInMinutes

        public PasswordPolicy()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
