using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LosIntegration.Model.Model.ServiceResponseModels;

namespace LosIntegration.Service.InternalServices
{
    public interface IMilestoneService
    {
        Task<IActionResult> SyncRainmakerLoanStatusFromByte(int tenantId,
                                                            int rainmakerApplicationId,
                                                            short byteStatusId,
                                                            string byteFileName);


        Task<List<MilestoneMappingResponse>> GetMappingAll(int tenantId,
                                                           short losId);
    }
}
