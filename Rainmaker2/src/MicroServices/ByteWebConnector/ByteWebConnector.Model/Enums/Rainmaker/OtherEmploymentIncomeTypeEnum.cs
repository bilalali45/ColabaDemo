using System.ComponentModel;

namespace ByteWebConnector.Model.Enums.Rainmaker
{
    public enum OtherEmploymentIncomeTypeEnum
    {
        [Description("Overtime")]
        Overtime = 1,
        [Description("Bonus")]
        Bonus = 2,
        [Description("Commission")]
        Commission = 3,
        [Description("Military Entitlements")]
        MilitaryEntitlements = 4,
        [Description("Other")]
        Other = 5,
    }
}