using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ByteWebConnector.Model.Enums.Rainmaker
{
    public enum LoanPurposeEnum
    {
        [Description("Geo Location")]
        GeoLocation = 0,
        [Description("Buying a Home")]
        Purchase = 1,
        [Description("Looking to Refinance")]
        Refinance = 2,
        CashOut = 3
    }
}
