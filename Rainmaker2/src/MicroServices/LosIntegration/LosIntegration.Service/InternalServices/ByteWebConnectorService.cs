
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ServiceCallHelper;

namespace LosIntegration.Service.InternalServices
{
    public class ByteWebConnectorService : IByteWebConnectorService
    {
        #region Constructors

        public ByteWebConnectorService(ILogger<ByteWebConnectorService> logger,
                                       HttpClient httpClient,
                                       IConfiguration configuration)
        {
            _logger = logger;
            _httpClient = httpClient;
            _configuration = configuration;
            _baseUrl = _configuration[key: "ServiceAddress:ByteWebConnector:Url"];
        }

        #endregion

        #region Methods 

        public async Task<short> GetLoanStatusAsync(string fileDataId)
        {
            short currentLoanStatus = 0;
            var response = await this._httpClient.EasyGetAsync<response>(requestUri: $"{_baseUrl}/api/ByteWebConnector/LoanFile/GetLoanStatus?fileDataId={fileDataId}");

            return response.ResponseObject.loanStatusId;

            
        }


        public class response
        {
            public short loanStatusId { get; set; }
        }


        #endregion

        #region Private Variables

        private readonly ILogger<ByteWebConnectorService> _logger;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl;

        #endregion
    }
}