using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LosIntegration.Service.InternalServices
{
    public interface IMilestoneService
    {
        Task<IActionResult> SyncRainmakerLoanStatusFromByte(int tenantId,
                                                            int rainmakerApplicationId,
                                                            short byteStatusId,
                                                            string byteFileName);
    }
}
