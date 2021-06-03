using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TenantConfig.Common
{
    public interface IKeyStoreService
    {
        Task<string> GetFtpKey(string url);
    }
    public class KeyStoreService : IKeyStoreService
    {
        private readonly HttpClient httpClient;
        private static object lockObject = new object();
        private static volatile string FtpKey = string.Empty;
        public KeyStoreService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<string> GetFtpKey(string url)
        {
            if (!string.IsNullOrEmpty(FtpKey))
                return FtpKey;
            var ftpKey = "FtpKey";
            var ftpKeyResponse = await httpClient.GetAsync($"{url}/api/keystore/keystore?key={ftpKey}");
            ftpKeyResponse.EnsureSuccessStatusCode();
            ftpKey = await ftpKeyResponse.Content.ReadAsStringAsync();
            lock (lockObject)
            {
                FtpKey = ftpKey;
            }
            return ftpKey;
        }
    }
}
