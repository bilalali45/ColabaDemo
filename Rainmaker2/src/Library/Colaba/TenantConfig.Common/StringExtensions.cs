using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace TenantConfig.Common
{
    public static class StringExtensions
    {
        public static string ToSha(this string text, int rounds)
        {
            string temp = text;
            for (int i = 0; i < rounds; i++)
            {
                temp = temp.ComputeHash(new SHA256CryptoServiceProvider());
            }
            return temp;
        }

        public static string ComputeHash(this string input, HashAlgorithm algorithm)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }
        public static string ApplyPasswordEncryption(this string password, PasswordEncryptionFormat format, out string key)
        {
            string result = string.Empty;
            key = string.Empty;
            switch (format)
            {
                case PasswordEncryptionFormat.ShaHashed:
                    Random rnd = new Random();
                    int rounds = rnd.Next(1, 100);
                    key = rounds.ToString();
                    result = password.ToSha(rounds);
                    break;
            }
            return result;
        }

        public static string ApplyPasswordEncryptionWithSalt(this string password, PasswordEncryptionFormat format, string key)
        {
            string result = string.Empty;
            switch (format)
            {
                case PasswordEncryptionFormat.ShaHashed:
                    int rounds = int.Parse(key);
                    result = password.ToSha(rounds);
                    break;
            }
            return result;
        }
    }
}
