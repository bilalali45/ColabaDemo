﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using MainGateway.Helpers;
using Microsoft.Extensions.Configuration;

namespace MainGateway.Services
{
   

    public class KeyStoreService : IKeyStoreService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;


        public KeyStoreService(IHttpClientFactory clientFactory,
                               IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
        }


        public async Task<string> GetJwtSecurityKeyAsync()
        {
            var httpClient = _clientFactory.CreateClient();
            var jwtKeyResponse = await httpClient.GetAsync($"{_configuration["KeyStore:Url"]}/api/keystore/keystore?key=JWT");
            if (!jwtKeyResponse.IsSuccessStatusCode)
            {
                throw new Exception("jwt key not found");
            }
            return await jwtKeyResponse.Content.ReadAsStringAsync();
        }

        public string GetJwtSecurityKey()
        {
            var httpClient = _clientFactory.CreateClient();
            var jwtKeyResponse = AsyncHelper.RunSync(() =>  httpClient.GetAsync($"{_configuration["KeyStore:Url"]}/api/keystore/keystore?key=JWT"));

            if (!jwtKeyResponse.IsSuccessStatusCode)
            {
                throw new Exception("jwt key not found");
            }

            var result = AsyncHelper.RunSync(()=> jwtKeyResponse.Content.ReadAsStringAsync());

            return result;
        }
    }
}