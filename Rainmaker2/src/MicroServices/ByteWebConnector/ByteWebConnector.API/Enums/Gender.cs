using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ByteWebConnector.API.Enums
{
    public enum Gender
    {
        [Description("Female")]
        Female = 1,
        [Description("Male")]
        Male = 2,
        [Description("Do Not Wish to Provide")]
        Do_Not_Wish = 3

    }
}
