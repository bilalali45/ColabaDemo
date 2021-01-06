using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Extensions.ExtensionClasses;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using ServiceCallHelper;
using LosIntegration.Model.Model.ServiceRequestModels.Milestone;
using LosIntegration.Model.Model.ServiceResponseModels;

namespace LosIntegration.Service.InternalServices
{
    public class MilestoneService : IMilestoneService
    {
        private readonly ILogger<MilestoneService> _logger;
        private readonly HttpClient _httpclient;
        private readonly IConfiguration _configuration;
        private string _baseUrl;

        public MilestoneService(ILogger<MilestoneService> logger, HttpClient httpClient, IConfiguration configuration)
        {
            this._logger = logger;
            this._httpclient = httpClient;            
            this._baseUrl = configuration[key: "ServiceAddress:Milestone:Url"];
        }
        public async Task<IActionResult> SyncRainmakerLoanStatusFromByte(int tenantId, int rainmakerApplicationId, short byteStatusId, string byteFileName)
        {
            LosMilestoneRequest content = new LosMilestoneRequest() {
                loanId = byteFileName,
                losId = 1,
                milestone = byteStatusId,
                rainmakerLosId = 2,
                tenantId = 1
            };
            var response = await this._httpclient.EasyPostAsync<IActionResult>(requestUri: $"{this._baseUrl}/api/milestone/milestone/SetLosMilestone", content: content, attachAuthorizationHeadersFromCurrentRequest: true);
            return response.ResponseObject;
        }


        public async Task<List<MilestoneMappingResponse>> GetMappingAll(int tenantId,
                                                                        short losId)
        {
            var response = await this._httpclient.EasyGetAsync<List<MilestoneMappingResponse>>(requestUri: $"{this._baseUrl}/api/milestone/milestone/GetMappingAll?tenantId={tenantId}&losId={losId}", attachAuthorizationHeadersFromCurrentRequest: true);
            return response.ResponseObject;
        }

        //public Task<IActionResult> SyncRainmakerLoanStatusFromByte(int tenantId, int rainmakerApplicationId, short byteStatusId, string byteFileName)
        //{
        //    return this.SyncRainmakerLoanStatusFromByte(tenantId: tenantId, rainmakerApplicationId: rainmakerApplicationId, byteStatusId, byteFileName);
        //}
    }
}
