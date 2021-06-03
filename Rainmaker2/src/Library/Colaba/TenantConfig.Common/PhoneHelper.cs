using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TenantConfig.Common
{
    public enum PasswordEncryptionFormat { PlainText = 0, Encrypted = 1, Hashed = 2, ShaHashed = 3 }

    public enum PasswordChangeType
    {
        ChangePassword = 1,
        ForgotPassword = 2
    }
    public enum UserType
    {
        Customer=1,
        Employee=2,
        Support=3
    }

    public enum EmailType
    {
        Primary=1,
        Secondary=2,
        AutoReply=3
    }

    public enum PhoneType
    {
        Mobile=1,
        Home=2,
        Work=3
    }
    public static class PhoneHelper
    {
        public static string UnMask(string text)
        {
            if (text != null)
            {
                text = text.Replace("+1", "");
                text = Regex.Replace(text, "[^0-9]", String.Empty).Replace("+1", "");
            }

            return text;
        }
        public static string Mask(string text)
        {
            if (text != null)
                text = Regex.Replace(text, @"(\d{3})(\d{3})(\d{4})", "($1) $2-$3");

            return text;
        }

        public static bool IsValidUnmasked(string value) => value.All(char.IsNumber) && value.Length==10;
    }
}
