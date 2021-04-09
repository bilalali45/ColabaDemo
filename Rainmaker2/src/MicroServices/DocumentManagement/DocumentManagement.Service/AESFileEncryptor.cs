using System;
using System.IO;
using System.Security.Cryptography;

namespace DocumentManagement.Service
{
    public class AesFileEncryptor : IFileEncryptor
    {
        public Stream DecrypeFile(MemoryStream fsCrypt, string password, string originalFileName, string saltString)
        {
            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
            byte[] salt = new byte[32];

            if (string.IsNullOrEmpty(saltString))
            {
                fsCrypt.Read(salt, 0, salt.Length);
            }
            else
            {
                salt = Convert.FromBase64String(saltString);
            }

            using RijndaelManaged AES = new RijndaelManaged();
            AES.KeySize = 256;
            AES.BlockSize = 128;
            var key = new Rfc2898DeriveBytes(passwordBytes, salt, 50000);
            AES.Key = key.GetBytes(AES.KeySize / 8);
            AES.IV = key.GetBytes(AES.BlockSize / 8);
            AES.Padding = PaddingMode.PKCS7;
            AES.Mode = CipherMode.ECB;

            using CryptoStream cs = new CryptoStream(fsCrypt, AES.CreateDecryptor(), CryptoStreamMode.Read, true);
            //var filePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + Path.GetExtension(originalFileName));
            //FileStream fsOut = new FileStream(filePath, FileMode.Create);
            MemoryStream fsOut = new MemoryStream();
            int read;
            byte[] buffer = new byte[100000];

            while ((read = cs.Read(buffer, 0, buffer.Length)) > 0)
            {
                fsOut.Write(buffer, 0, read);
            }
            fsOut.Position = 0;
            fsCrypt.Position = 0;
            return fsOut;
        }

        public (MemoryStream,string) EncryptFile(Stream inputFile, string password)
        {
            byte[] salt = GenerateRandomSalt();

            //create output file name
            //var filePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".enc");
            MemoryStream fsCrypt = new MemoryStream();

            //convert password string to byte arrray
            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);

            //Set Rijndael symmetric encryption algorithm
            using RijndaelManaged AES = new RijndaelManaged();
            AES.KeySize = 256;
            AES.BlockSize = 128;
            AES.Padding = PaddingMode.PKCS7;

            //http://stackoverflow.com/questions/2659214/why-do-i-need-to-use-the-rfc2898derivebytes-class-in-net-instead-of-directly
            //"What it does is repeatedly hash the user password along with the salt." High iteration counts.
            var key = new Rfc2898DeriveBytes(passwordBytes, salt, 50000);
            AES.Key = key.GetBytes(AES.KeySize / 8);
            AES.IV = key.GetBytes(AES.BlockSize / 8);

            //Cipher modes: http://security.stackexchange.com/questions/52665/which-is-the-best-cipher-mode-and-padding-mode-for-aes-encryption
            AES.Mode = CipherMode.ECB;

            //write salt to the begining of the output file, so in this case can be random every time
            //fsCrypt.Write(salt, 0, salt.Length);
            string saltString = Convert.ToBase64String(salt);

            using CryptoStream cs = new CryptoStream(fsCrypt, AES.CreateEncryptor(), CryptoStreamMode.Write, true);

            //create a buffer (100kb) so only this amount will allocate in the memory and not the whole file
            byte[] buffer = new byte[100000];
            int read;

            while ((read = inputFile.Read(buffer, 0, buffer.Length)) > 0)
            {
                cs.Write(buffer, 0, read);
            }
            cs.FlushFinalBlock();
            fsCrypt.Position = 0;
            return (fsCrypt,saltString);
        }

        private static byte[] GenerateRandomSalt()
        {
            byte[] data = new byte[32];

            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(data);
            }
            return data;
        }
    }
}
