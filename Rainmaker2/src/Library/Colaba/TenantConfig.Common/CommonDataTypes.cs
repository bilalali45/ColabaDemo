using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TenantConfig.Common
{
    public static class EmailTokens
    {
        public const string FROM_EMAIL = "###FromEmail###";
        public const string FROM_EMAIL_DISPLAY = "###FromEmailDisplay###";
        public const string FORGOT_PASSWORD_LINK = "###ForgotPasswordLink###";
        public const string BRANCH_URL = "###BranchUrl###";
        public const string BRANCH_LOGO_URL = "###BranchLogoUrl###";
        public const string BRANCH_NAME = "###BranchName###";
        public const string PRIMARY_EMAIL_ADDRESS = "###PrimaryEmailAddress###";
        public const string PRIMARY_COLOR = "###PrimaryColor###";
        public const string FORGOT_YOUR_PASSWORD = "###ForgotYourPassword###";
    }
    public enum ActivityType
    {
        Email = 1
    }

    public enum Activity
    {
        ForgotPassword=1,
        McuForgotPassword = 2
    }

    public enum TwoFaStatus : byte
    {
        RequiredForAll = 1,
        InactiveForAll = 2,
        UserPreference = 3
    }

    [Flags]
    public enum CustomerEntities 
    {
        Contact = 1,
        Tenant = 1 << 1,
        Contact_PhoneInfo = 1 << 2
    }

    [Flags]
    public enum TenantEntities
    {
        [Description("TwoFAConfig")]
        TwoFaConfig = 1
    }

    public enum TwoFaConfigEntities
    {
        [Description("Tenant")]
        Tenant = 1
    }

    public enum CustomerRelatedEntities
    {
        [Description("Contact")]
        Contact,
        [Description("Contact.ContactPhoneInfoes")]
        ContactPhoneInfo,
        [Description("Contact.ContactEmailInfo")]
        ContactEmailInfo,
        [Description("Tenant")]
        Tenant,
        [Description("Tenant.Branches")]
        TenantBranches
    }

    public enum EmployeeRelatedEntities
    {
        [Description("Contact")]
        Contact,
        [Description("Contact.ContactPhoneInfoes")]
        ContactPhoneInfo,
        [Description("Contact.ContactEmailInfo")]
        ContactEmailInfo,
        [Description("Tenant")]
        Tenant,
        [Description("Tenant.Branches")]
        TenantBranches
    }
}
