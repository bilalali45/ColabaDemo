using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace LoanApplication.Service
{
    public  class EncryptionService : IEncryptionService
    {
        public string Decrypt(string input, string key, string IV)
        {
            try
            {
                // AesCryptoServiceProvider
                AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
                aes.BlockSize = 128;
                aes.KeySize = 256;
                aes.IV = Convert.FromBase64String(IV);
                aes.Key = Convert.FromBase64String(key);
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                // Convert Base64 strings to byte array
                byte[] src = System.Convert.FromBase64String(input);

                // decryption
                using ICryptoTransform decrypt = aes.CreateDecryptor();

                byte[] dest = decrypt.TransformFinalBlock(src, 0, src.Length);
                return Encoding.UTF8.GetString(dest);
            }
            catch (Exception ex)
            {
                return input;
            }
        }

        public (string, string, string) Encrypt(string input)
        {
            try
            {
                using AesCryptoServiceProvider aesEncryption = new AesCryptoServiceProvider();

                aesEncryption.KeySize = 256;
                aesEncryption.BlockSize = 128;
                aesEncryption.Mode = CipherMode.CBC;
                aesEncryption.Padding = PaddingMode.PKCS7;
                aesEncryption.GenerateIV();
                aesEncryption.GenerateKey();

                using ICryptoTransform crypto = aesEncryption.CreateEncryptor();

                byte[] byteinpute = Encoding.UTF8.GetBytes(input);
                byte[] cipherText = crypto.TransformFinalBlock(byteinpute, 0, byteinpute.Length);
                return (Convert.ToBase64String(cipherText), Convert.ToBase64String(aesEncryption.Key), Convert.ToBase64String(aesEncryption.IV));
            }
            catch (Exception ex)
            {
                return (input, "", "");
            }
          
        }
    }
}
