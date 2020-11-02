using System;
using System.Net.Http;
using Extensions.ExtensionClasses;
using LosIntegration.Model.Model.ServiceResponseModels.Rainmaker;
using Microsoft.Extensions.Configuration;
using ServiceCallHelper;

namespace LosIntegration.Service.InternalServices.Rainmaker
{
    public class LoanRequestService : ILoanRequestService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl;


        [Flags]
        public enum RelatedEntities
        {
            BusinessUnit = 1 << 0,
        }

        public LoanRequestService(HttpClient _httpClient, IConfiguration _configuration)
        {
            this._httpClient = _httpClient;
            this._configuration = _configuration;
            _baseUrl = _configuration[key: "ServiceAddress:RainMaker:Url"];
        }

        public LoanRequest GetLoanRequestWithDetails(int loanApplicationId,
                                                             RelatedEntities? includes = null)
        {

            _httpClient.EasyGet<LoanRequest>(out var loanRequestCallResponse,
                                                 requestUri: $"{_baseUrl}/api/RainMaker/LoanRequest/GetLoanRequest?loanApplicationId={loanApplicationId}",
                                                 
                                                 attachAuthorizationHeadersFromCurrentRequest: true
                                                );

            string rawResult = loanRequestCallResponse.RawResult;

            if (!loanRequestCallResponse.HttpResponseMessage.IsSuccessStatusCode)
                throw new Exception(message: "Unable to get loan Request from Rainmaker");
            return loanRequestCallResponse.ResponseObject;
        }

    }
}
