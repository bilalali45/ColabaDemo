using System.ComponentModel;

namespace ByteWebConnector.Model.Enums.Rainmaker
{
    public enum ResidencyStateEnum
    {
        [Description(description: "US Citizen")]
        UsCitizen = 1,

        [Description(description: "Permanent Resident")]
        PermanentResident = 2,

        [Description(description: "Valid work VISA (H1, L1 etc.)")]
        ValidworkVisa = 3,

        [Description(description: "Temporary workers (H -2A)")]
        Temporaryworkers = 4,
        [Description(description: "Other")] Other = 5
    }
}