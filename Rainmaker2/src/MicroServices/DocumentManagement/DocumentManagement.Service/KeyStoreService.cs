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
        private readonly IHttpClientFactory clientFactory;
        private readonly IConfiguration config;
        public KeyStoreService(IHttpClientFactory clientFactory, IConfiguration config)
        {
            this.clientFactory = clientFactory;
            this.config = config;
        }
        public async Task<string> GetFtpKey()
        {
            var httpClient = clientFactory.CreateClient();
            var ftpKey = config["File:FtpKey"];
            var ftpKeyResponse = await httpClient.GetAsync($"{config["KeyStore:Url"]}/api/keystore/keystore?key={ftpKey}");
            if (!ftpKeyResponse.IsSuccessStatusCode)
            {
                throw new Exception("Unable to load key from key store");
            }
            return await ftpKeyResponse.Content.ReadAsStringAsync();
        }
        public async Task<string> GetFileKey()
        {
            var httpClient = clientFactory.CreateClient();
            var key = config["File:Key"];
            var csResponse = await httpClient.GetAsync($"{config["KeyStore:Url"]}/api/keystore/keystore?key={key}");
            if (!csResponse.IsSuccessStatusCode)
            {
                throw new Exception("Unable to load key from key store");
            }
            return await csResponse.Content.ReadAsStringAsync();
        }
    }
}
