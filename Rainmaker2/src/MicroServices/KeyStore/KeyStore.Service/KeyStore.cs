using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace KeyStore.Service
{
    public class KeyStore : IKeyStore
    {
        private readonly IConfiguration configuration;
        public KeyStore(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string Get(string key)
        {
            using FileStream fileStream = new FileStream(configuration["FilePath"],FileMode.Open,FileAccess.Read,FileShare.ReadWrite);
            using StreamReader streamReader = new StreamReader(fileStream);
            while(!streamReader.EndOfStream)
            {
                string line = streamReader.ReadLine();
                int pos = line.IndexOf('=');
                if (pos < 0)
                    throw new Exception("Invalid file format");
                string keyString = line.Substring(0,pos);
                string valueString = line.Substring(pos+1);
                if (keyString == key)
                    return AESCryptography.Decrypt(valueString,AESCryptography.AesKey256);
            }
            return null;
        }
    }
}
