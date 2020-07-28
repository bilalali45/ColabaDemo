using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ByteWebConnector.API.Enums
{
    public enum ResidencyStateEnum
    {
        [Description("US Citizen")]
        UsCitizen = 1,
        [Description("Permanent Resident")]
        PermanentResident = 2,
        [Description("Valid work VISA (H1, L1 etc.)")]
        ValidworkVisa = 3,
        [Description("Temporary workers (H -2A)")]
        Temporaryworkers = 4,
        [Description("Other")]
        Other = 5

    }
}
