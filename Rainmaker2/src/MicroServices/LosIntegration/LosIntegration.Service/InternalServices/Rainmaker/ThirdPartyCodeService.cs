using System;
using System.Collections.Generic;
using System.Net.Http;
using Extensions.ExtensionClasses;
using LosIntegration.Model.Model.ServiceResponseModels.Rainmaker;
using Microsoft.Extensions.Configuration;
using ServiceCallHelper;

namespace LosIntegration.Service.InternalServices.Rainmaker
{
    public class ThirdPartyCodeService : IThirdPartyCodeService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl;


        [Flags]
        public enum RelatedEntities
        {
            BusinessUnit = 1 << 0,
        }

        public ThirdPartyCodeService(HttpClient _httpClient, IConfiguration _configuration)
        {
            this._httpClient = _httpClient;
            this._configuration = _configuration;
            _baseUrl = _configuration[key: "ServiceAddress:RainMaker:Url"];
        }

        public List<ThirdPartyCode> GetRefIdByThirdPartyId(int thirdPartyId)
        {

            _httpClient.EasyGet<List<ThirdPartyCode>>(out var thirdPartyCodeCallResponse,
                                                      requestUri: $"{_baseUrl}/api/RainMaker/ThirdPartyCode/GetRefIdByThirdPartyId?thirdPartyId={thirdPartyId}",
                                                 
                                                      attachAuthorizationHeadersFromCurrentRequest: true
                                                     );

            string rawResult = thirdPartyCodeCallResponse.RawResult;

            if (!thirdPartyCodeCallResponse.HttpResponseMessage.IsSuccessStatusCode)
                throw new Exception(message: "Unable to get ThirdParty Code from Rainmaker");
            return thirdPartyCodeCallResponse.ResponseObject;
        }

    }
}
