using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace Identity.Service
{


    public class KeyStoreService : IKeyStoreService
    {
        private const string TWILIO_ACCOUNT_SID_KEY = "TwilioAccountSid";
        private const string TWILIO_AUTH_TOKEN_KEY = "TwilioAuthToken";

        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;


        public KeyStoreService(IHttpClientFactory clientFactory,
                               IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
            this._httpClient = _clientFactory.CreateClient("clientWithCorrelationId");
        }


        public async Task<string> GetJwtSecurityKeyAsync()
        {
            var httpClient = _clientFactory.CreateClient(name: "clientWithCorrelationId");
            var jwtKeyResponse = await httpClient.GetAsync($"{_configuration["KeyStore:Url"]}/api/keystore/keystore?key=JWT");
            jwtKeyResponse.EnsureSuccessStatusCode();
            return await jwtKeyResponse.Content.ReadAsStringAsync();
        }

        public async Task<string> GetTwilioAccountSidAsync()
        {
            return await this.GetValueFromKeyStoreAsync(TWILIO_ACCOUNT_SID_KEY);
        }

        public async Task<string> GetTwilioAuthTokenAsync()
        {
            return await this.GetValueFromKeyStoreAsync(TWILIO_AUTH_TOKEN_KEY);
        }

     

        private async Task<string> GetValueFromKeyStoreAsync(string key)
        {
            var response = await this._httpClient.GetAsync($"{_configuration["KeyStore:Url"]}/api/keystore/keystore?key={key}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

      
    }
}
