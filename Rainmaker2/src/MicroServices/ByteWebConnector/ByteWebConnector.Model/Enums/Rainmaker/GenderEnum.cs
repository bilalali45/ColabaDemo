using System.ComponentModel;

namespace ByteWebConnector.Model.Enums.Rainmaker
{
    public enum GenderEnum
    {
        [Description(description: "Female")] Female = 1,
        [Description(description: "Male")] Male = 2,

        [Description(description: "Do Not Wish to Provide")]
        Do_Not_Wish = 3
    }
}