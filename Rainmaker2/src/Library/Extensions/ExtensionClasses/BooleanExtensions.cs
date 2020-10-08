using System;

namespace Extensions.ExtensionClasses
{
    public static class BooleanExtensions
    {
        public static Boolean ToBoolean(this bool value)
        {
            return Convert.ToBoolean(value);
        }
        public static Boolean ToBoolean(this bool? value)
        {
            return Convert.ToBoolean(value);
        } 

        public static string ToYesNoString(this bool value)
        {
            return value ? "Yes" : "No";
        }

    }
}
