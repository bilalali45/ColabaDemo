using System;
using System.Text.RegularExpressions;

namespace Extensions.ExtensionClasses
{
    public static class IntExtensions
    {
        public static bool ToBoolean(this int value)
        {
            if (value == 0)
                return false;
            return true;
        }

        public static int ToInt(this int? value)
        {
            if (!value.HasValue)
                return 0;
            return value.Value;
        }
        public static int ToInt(this object value)
        {
            if (value == null)
                return 0;
            return Convert.ToInt32(value);
        }
        public static int ToIntWithNull(this int? value)
        {
            return value == null ? 0 : value.Value;
        }
        public static bool IsInt(this string value)
        {
            var retVale = false;
            if (!string.IsNullOrEmpty(value))
            {
                retVale = Regex.IsMatch(value, @"\d");                
            }
            return retVale;
        }
    }
}
