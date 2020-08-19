using System;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
 

namespace Extensions.ExtensionClasses
{
    public static class StringExtensions
    {
        public static string TrimToLength(this string text, int length)
        {
            string result = text;

            if (!string.IsNullOrEmpty(result) && result.Length > length)
            {
                result = result.Substring(0, length - 3) + "...";
            }
            return result;
        }
      
    

      
        public static int ToInt(this string value)
        {
            try
            {
                if (string.IsNullOrEmpty(value))
                    return 0;
                return Convert.ToInt32(Convert.ToDecimal(value));
            }
            catch (Exception)
            {

                return 0;
            }
        }

        public static decimal ToDecimal(this string value, int digits = 18)
        {
            try
            {
                if (string.IsNullOrEmpty(value))
                    return 0;

                if (value.Length > digits)
                    value = value.Substring(0, digits);

                return Convert.ToDecimal(value);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static string ToMd5Base64(this string text, int rounds = 11)
        {
            return text.ToMd5(rounds).ToBase64();
        }
        public static string ToMd5(this string text, int rounds = 11)
        {
            string temp = text;
            for (int i = 0; i < rounds; i++)
            {
                temp = temp.ComputeHash(new MD5CryptoServiceProvider());
            }
            return temp;
        }

        public static string ComputeHash(this string input, HashAlgorithm algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }

        public static string FromBase64(this string textbase64)
        {
            var bytes = Convert.FromBase64String(textbase64);
            return bytes.GetString();
        }

        public static string ToBase64(this string text)
        {
            return Convert.ToBase64String(text.GetBytes());
        }

        public static byte[] GetBytes(this string str)
        {
            return Encoding.Unicode.GetBytes(str);
        }

        public static string GetString(this byte[] bytes)
        {
            return Encoding.Unicode.GetString(bytes);
        }



        public static string Encrypt(this string text, string key)
        {
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(text);

            var hashMd5 = new MD5CryptoServiceProvider();
            byte[] keyArray = hashMd5.ComputeHash(Encoding.UTF8.GetBytes(key));


            var tripleDes = new TripleDESCryptoServiceProvider
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            //set the secret key for the tripleDES algorithm
            //mode of operation. there are other 4 modes. We choose ECB(Electronic code Book)
            //padding mode(if any extra byte added)

            ICryptoTransform cTransform = tripleDes.CreateEncryptor();
            //transform the specified region of bytes array to resultArray
            byte[] resultArray = cTransform.TransformFinalBlock
                    (toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tripleDes.Clear();
            //Return the encrypted data into unreadable string format
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        public static string Decrypt(this string cipherString, string key)
        {

            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            var hashMd5 = new MD5CryptoServiceProvider();
            byte[] keyArray = hashMd5.ComputeHash(Encoding.UTF8.GetBytes(key));

            hashMd5.Clear();

            var tripleDes = new TripleDESCryptoServiceProvider
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            //set the secret key for the tripleDES algorithm
            //mode of operation. there are other 4 modes.
            //We choose ECB(Electronic code Book)

            //padding mode(if any extra byte added)

            ICryptoTransform cTransform = tripleDes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock
                    (toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tripleDes.Clear();
            //return the Clear decrypted TEXT
            return Encoding.UTF8.GetString(resultArray);
        }

        

        public static string RemoveLast(this string text, string removetext)
        {
            if (text != null && text.Contains(removetext))
                text = text.Remove(text.LastIndexOf(removetext, StringComparison.Ordinal));

            return text;
        }


        public static bool IsEmptyOrNullWithTrim(this string text)
        {
            return (text == null || text.Trim().Length == 0);
        }


        
        /// <summary>
        /// Convert Pascal for front-end string
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns>Converted string</returns>
        public static string PascalToDisplayString(this string str)
        {
            var result = new StringBuilder();
            //string result = string.Empty;
            char[] letters = str.ToCharArray();
            foreach (char c in letters)
                if (c.ToString(CultureInfo.InvariantCulture) != c.ToString(CultureInfo.InvariantCulture).ToLower())
                {
                    result.Append(" ");
                    result.Append(c.ToString(CultureInfo.InvariantCulture));
                }
                else
                {
                    result.Append(c.ToString(CultureInfo.InvariantCulture));
                }
            return result.ToString();
        }



   
        public static string GetNumberSuffix(string num)
        {
            var suffix = "th";

            if (int.Parse(num) >= 11 && int.Parse(num) <= 20) return suffix;

            num = num.ToCharArray()[num.ToCharArray().Length - 1].ToString(CultureInfo.InvariantCulture);
            switch (num)
            {
                case "1":
                    suffix = "st";
                    break;
                case "2":
                    suffix = "nd";
                    break;
                case "3":
                    suffix = "rd";
                    break;
            }
            return suffix;
        }

        /// <summary>
        /// return time like 1:00 PM
        /// </summary>
        public static string GetFomattedTimeSpan(this TimeSpan value)
        {
            var time = DateTime.Today.Add(value);
            return time.ToShortTimeString();
        }

    

    
        public static string TruncateWithEllipsis(this string text, int maxLength)
        {
            if (string.IsNullOrEmpty(text)) return text;
            return text.Length <= maxLength ? text : text.Substring(0, maxLength).Trim() + " ...";
        }

        public static string MinifyHtml(this string html)
        {
            var minifyRegexLineBreak = new Regex(@"\s*[\n\r]+\s*", RegexOptions.Compiled | RegexOptions.CultureInvariant);
            var minifyRegexInline = new Regex(@"\s{2,}", RegexOptions.Compiled | RegexOptions.CultureInvariant);

            html = minifyRegexLineBreak.Replace(html, "\n");
            html = minifyRegexInline.Replace(html, " ");

            return html;
        }

        /// <summary>
        /// Converting string To Title Case
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ToTitleCase(this string text)
        {
            return text != null ? CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text.Trim().ToLower()) : text;
        }

        public static string NameAbbreviation(this string text)
        {
            if (string.IsNullOrWhiteSpace(value: text)) return "";

            text = Regex.Replace(input: text.Trim(),
                                 pattern: " {2,}",
                                 replacement: " "); //remove extra spaces

            var wordsArray = text.Split();

            var wordItems = wordsArray.Length == 1 ? wordsArray[0] : wordsArray[0] + ' ' + wordsArray[1];

            var splitedWords = wordItems.Split(' ').ToList();
            var name = "";

            foreach (var wr in splitedWords)
                if (splitedWords.Count == 1)
                {
                    name += wr[index: 0].ToString().ToUpper();
                    if (wr.Length >= 2)
                        name += wr[index: 1].ToString().ToUpper();
                }
                else
                {
                    name += wr[index: 0].ToString().ToUpper();
                }

            return name;
        }
    }
}
