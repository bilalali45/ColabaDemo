using Identity.Helpers;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace Identity.Services
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
            var httpClient = _clientFactory.CreateClient(name: "clientWithCorrelationId");
            var jwtKeyResponse = await httpClient.GetAsync($"{_configuration["KeyStore:Url"]}/api/keystore/keystore?key=JWT");
            jwtKeyResponse.EnsureSuccessStatusCode();
            return await jwtKeyResponse.Content.ReadAsStringAsync();
        }

        public  string GetJwtSecurityKey()
        {
            var httpClient = _clientFactory.CreateClient(name: "clientWithCorrelationId");
            var jwtKeyResponse = AsyncHelper.RunSync(() =>  httpClient.GetAsync($"{_configuration["KeyStore:Url"]}/api/keystore/keystore?key=JWT"));
            jwtKeyResponse.EnsureSuccessStatusCode();

            var result = AsyncHelper.RunSync(()=> jwtKeyResponse.Content.ReadAsStringAsync());

            return result;
        }
    }
}
