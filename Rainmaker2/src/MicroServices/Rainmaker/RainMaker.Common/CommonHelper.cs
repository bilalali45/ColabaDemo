using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace RainMaker.Common
{
    /// <summary>
    /// Represents a common helper
    /// </summary>
    public partial class CommonHelper
    {
        /// <summary>
        /// Ensures the subscriber email or throw.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        public static string EnsureSubscriberEmailOrThrow(string email) {
            string output = EnsureNotNull(email);
            output = output.Trim();
            output = EnsureMaximumLength(output, 255);

            if (!IsValidEmail(output)) {
                throw new Exception("Email is not valid.");
            }

            return output;
        }

        /// <summary>
        /// Verifies that a string is in valid e-mail format
        /// </summary>
        /// <param name="email">Email to verify</param>
        /// <returns>true if the string is a valid e-mail address and false if it's not</returns>
        public static bool IsValidEmail(string email)
        {
            if (String.IsNullOrEmpty(email))
                return false;

            email = email.Trim();
            var result = Regex.IsMatch(email, "^(?:[\\w\\!\\#\\$\\%\\&\\'\\*\\+\\-\\/\\=\\?\\^\\`\\{\\|\\}\\~]+\\.)*[\\w\\!\\#\\$\\%\\&\\'\\*\\+\\-\\/\\=\\?\\^\\`\\{\\|\\}\\~]+@(?:(?:(?:[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!\\.)){0,61}[a-zA-Z0-9]?\\.)+[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!$)){0,61}[a-zA-Z0-9]?)|(?:\\[(?:(?:[01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\.){3}(?:[01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\]))$", RegexOptions.IgnoreCase);
            return result;
        }

        /// <summary>
        /// Generate random digit code
        /// </summary>
        /// <param name="length">Length</param>
        /// <returns>Result string</returns>
        public static string GenerateRandomDigitCode(int length)
        {
            var random = new Random();
            string str = string.Empty;
            for (int i = 0; i < length; i++)
                str = String.Concat(str, random.Next(10).ToString(CultureInfo.InvariantCulture));
            return str;
        }

        /// <summary>
        /// Returns an random interger number within a specified rage
        /// </summary>
        /// <param name="min">Minimum number</param>
        /// <param name="max">Maximum number</param>
        /// <returns>Result</returns>
        public static int GenerateRandomInteger(int min = 0, int max = int.MaxValue)
        {
            var randomNumberBuffer = new byte[10];
            new RNGCryptoServiceProvider().GetBytes(randomNumberBuffer);
            return new Random(BitConverter.ToInt32(randomNumberBuffer, 0)).Next(min, max);
        }

        /// <summary>
        /// Ensure that a string doesn't exceed maximum allowed length
        /// </summary>
        /// <param name="str">Input string</param>
        /// <param name="maxLength">Maximum length</param>
        /// <param name="postfix">A string to add to the end if the original string was shorten</param>
        /// <returns>Input string if its length is OK; otherwise, truncated input string</returns>
        public static string EnsureMaximumLength(string str, int maxLength, string postfix = null)
        {
            if (String.IsNullOrEmpty(str))
                return str;

            if (str.Length > maxLength)
            {
                var result = str.Substring(0, maxLength);
                if (!String.IsNullOrEmpty(postfix))
                {
                    result += postfix;
                }
                return result;
            }
            return str;
        }

        /// <summary>
        /// Ensures that a string only contains numeric values
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns>Input string with only numeric values, empty string if input is null/empty</returns>
        public static string EnsureNumericOnly(string str)
        {
            if (String.IsNullOrEmpty(str))
                return string.Empty;

            var result = new StringBuilder();
            foreach (char c in str)
            {
                if (Char.IsDigit(c))
                    result.Append(c);
            }
            return result.ToString();
        }

        /// <summary>
        /// Ensure that a string is not null
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns>Result</returns>
        public static string EnsureNotNull(string str)
        {
            if (str == null)
                return string.Empty;

            return str;
        }

        /// <summary>
        /// Indicates whether the specified strings are null or empty strings
        /// </summary>
        /// <param name="stringsToValidate">Array of strings to validate</param>
        /// <returns>Boolean</returns>
        public static bool AreNullOrEmpty(params string[] stringsToValidate) {
            bool result = false;
            Array.ForEach(stringsToValidate, str => {
                if (string.IsNullOrEmpty(str)) result = true;
            });
            return result;
        }


        private static AspNetHostingPermissionLevel? _trustLevel;
        /// <summary>
        /// Finds the trust level of the running application (http://blogs.msdn.com/dmitryr/archive/2007/01/23/finding-out-the-current-trust-level-in-asp-net.aspx)
        /// </summary>
        /// <returns>The current trust level.</returns>
        public static AspNetHostingPermissionLevel GetTrustLevel()
        {
            if (!_trustLevel.HasValue)
            {
                //set minimum
                _trustLevel = AspNetHostingPermissionLevel.None;

                //determine maximum
                foreach (AspNetHostingPermissionLevel trustLevel in
                        new[] {
                                AspNetHostingPermissionLevel.Unrestricted,
                                AspNetHostingPermissionLevel.High,
                                AspNetHostingPermissionLevel.Medium,
                                AspNetHostingPermissionLevel.Low,
                                AspNetHostingPermissionLevel.Minimal
                            })
                {
                    try
                    {
                        new AspNetHostingPermission(trustLevel).Demand();
                        _trustLevel = trustLevel;
                        break; //we've set the highest permission we can
                    }
                    catch (System.Security.SecurityException)
                    {
                    }
                }
            }
            return _trustLevel.Value;
        }

        public static string SetCompleteAddress(string StreetAddress, string CityName, string CountyName, string StateName, string ZipCode)
        {
            List<string> strArray = new List<string>();
            if (!string.IsNullOrEmpty(StreetAddress))
            {
                strArray.Add(StreetAddress);
            }

            if (!string.IsNullOrEmpty(CityName))
            {
                strArray.Add(CityName);
            }

            if (!string.IsNullOrEmpty(CountyName))
            {
                strArray.Add(CountyName);
            }

            if (!string.IsNullOrEmpty(StateName))
            {
                strArray.Add(StateName);
            }

            if (!string.IsNullOrEmpty(ZipCode))
            {
                strArray.Add(ZipCode);
            }

            return string.Join(", ", strArray);
        }


        public static TypeConverter GetCustomTypeConverter(Type type)
        {

            return TypeDescriptor.GetConverter(type);
        }

        /// <summary>
        /// Converts a value to a destination type.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="destinationType">The type to convert the value to.</param>
        /// <returns>The converted value.</returns>
        public static object To(object value, Type destinationType)
        {
            return To(value, destinationType, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts a value to a destination type.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="destinationType">The type to convert the value to.</param>
        /// <param name="culture">Culture</param>
        /// <returns>The converted value.</returns>
        public static object To(object value, Type destinationType, CultureInfo culture)
        {
            if (value != null)
            {
                var sourceType = value.GetType();

                TypeConverter destinationConverter = GetCustomTypeConverter(destinationType);
                TypeConverter sourceConverter = GetCustomTypeConverter(sourceType);
                if (destinationConverter != null && destinationConverter.CanConvertFrom(value.GetType()))
                    return destinationConverter.ConvertFrom(null, culture, value);
                if (sourceConverter != null && sourceConverter.CanConvertTo(destinationType))
                    return sourceConverter.ConvertTo(null, culture, value, destinationType);
                if (destinationType.IsEnum && value is int)
                    return Enum.ToObject(destinationType, (int)value);
                if (!destinationType.IsInstanceOfType(value))
                    return Convert.ChangeType(value, destinationType, culture);
            }
            return value;
        }

        /// <summary>
        /// Converts a value to a destination type.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <typeparam name="T">The type to convert the value to.</typeparam>
        /// <returns>The converted value.</returns>
        public static T To<T>(object value)
        {
            //return (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);

            return (T)To(value, typeof(T));



        }

        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
        /// <summary>
        /// Compare equality of double values. 
        /// </summary>
        /// <param name="x">The value 1 to compare.</param>
        /// <param name="y">The value 2 to compare.</param>
        /// <returns>The boolen compare result.</returns>
        public static bool AboutEqual(double x, double y)
        {
            double epsilon = Math.Max(Math.Abs(x), Math.Abs(y)) * 1E-15;
            return Math.Abs(x - y) <= epsilon;
        }
        public static string DateToString(DateTime? date)
        {
            if (date == null)
                return "";
            return date.ToString();
        }
        public static string UnMaskPhone(string text)
        {
            if (text != null)
            {
                text = text.Replace("+1", "");
                text = Regex.Replace(text, "[^0-9]", String.Empty).Replace("+1", "");
            }

            //text=text.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "");


            return text;
        }
        public static string MaskPhone(string text)
        {
            if (text != null)
                text = Regex.Replace(text, @"(\d{3})(\d{3})(\d{4})", "($1) $2-$3");

            return text;
        }

        public static string MaskPhonePlain(string text)
        {
            if (text != null)
                text = Regex.Replace(text, @"(\d{3})(\d{3})(\d{4})", "$1-$2-$3");

            return text;
        }

        public static string IntToIp(uint ipAddress)
        {
            return new IPAddress(BitConverter.GetBytes(ipAddress).Reverse().ToArray()).ToString();
        }

        public static uint IpToInt(string ipAddress)
        {
            return BitConverter.ToUInt32(IPAddress.Parse(ipAddress).GetAddressBytes().Reverse().ToArray(), 0);
        }

        public static bool IsNumeric(string val)
        {
            if (val == null)
                return false;
            long number;
            val = val.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "");
            return long.TryParse(val, out number);
        }

        public static bool IsValidPhoneString(string strPhone)
        {
            string regExPattern = @"^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$";
            return MatchStringFromRegex(strPhone, regExPattern);
        }

        private static bool MatchStringFromRegex(string str, string regexstr)
        {
            str = str.Trim();
            Regex pattern = new Regex(regexstr);
            return pattern.IsMatch(str);
        }

        public static int? GetMapNoofUnit(int? propertyTypeId)
        {
            int? noOfUnit;
            // Depends on PropertyType
            switch (propertyTypeId)
            {
                case 1: // Single Family Detached
                case 2: // Condominium
                case 3: // Townhome
                    noOfUnit = 1;
                    break;
                case 4: // 2-Unit Building
                    noOfUnit = 2;
                    break;
                case 5: // 3-Unit Building
                    noOfUnit = 3;
                    break;
                case 6: // 4-Unit Building
                    noOfUnit = 4;
                    break;
                default:
                    noOfUnit = (int?)null;
                    break;
            }

            return noOfUnit;
        }


        public static string DateFormat(string dateFormate )
            {
        
            DateTime date = Convert.ToDateTime(dateFormate);
            var _date= date.ToString("MMM") + " " + date.ToString("dd") + ", " + date.ToString("yyyy") + " AT " + date.ToString("hh:mm");
            return _date;
        }
    }
}
