using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace DocManager.Service
{
    public class KeyStoreService : IKeyStoreService
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration config;
        private static object lockObject = new object();
        private static volatile string FtpKey=string.Empty;
        private static volatile string FileKey=string.Empty;
        public KeyStoreService(HttpClient httpClient, IConfiguration config)
        {
            this.httpClient = httpClient;
            this.config = config;
        }
        public async Task<string> GetFtpKey()
        {
            if (!string.IsNullOrEmpty(FtpKey))
                return FtpKey;
            var ftpKey = config["File:FtpKey"];
            var ftpKeyResponse = await httpClient.GetAsync($"{config["KeyStore:Url"]}/api/keystore/keystore?key={ftpKey}");
            ftpKeyResponse.EnsureSuccessStatusCode();
            ftpKey = await ftpKeyResponse.Content.ReadAsStringAsync();
            lock (lockObject)
            {
                FtpKey = ftpKey;
            }
            return ftpKey;
        }
        public async Task<string> GetFileKey()
        {
            if (!string.IsNullOrEmpty(FileKey))
                return FileKey;
            var key = config["File:Key"];
            var csResponse = await httpClient.GetAsync($"{config["KeyStore:Url"]}/api/keystore/keystore?key={key}");
            csResponse.EnsureSuccessStatusCode();
            key = await csResponse.Content.ReadAsStringAsync();
            lock (lockObject)
            {
                FileKey = key;
            }
            return key;
        }
    }
}
