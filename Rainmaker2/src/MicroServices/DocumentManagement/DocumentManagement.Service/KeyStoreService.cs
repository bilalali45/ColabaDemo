using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Service
{
    public class KeyStoreService : IKeyStoreService
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration config;
        public KeyStoreService(HttpClient httpClient, IConfiguration config)
        {
            this.httpClient = httpClient;
            this.config = config;
        }
        public async Task<string> GetFtpKey()
        {
            var ftpKey = config["File:FtpKey"];
            var ftpKeyResponse = await httpClient.GetAsync($"{config["KeyStore:Url"]}/api/keystore/keystore?key={ftpKey}");
            ftpKeyResponse.EnsureSuccessStatusCode();
            return await ftpKeyResponse.Content.ReadAsStringAsync();
        }
        public async Task<string> GetFileKey()
        {
            var key = config["File:Key"];
            var csResponse = await httpClient.GetAsync($"{config["KeyStore:Url"]}/api/keystore/keystore?key={key}");
            csResponse.EnsureSuccessStatusCode();
            return await csResponse.Content.ReadAsStringAsync();
        }
    }
}
