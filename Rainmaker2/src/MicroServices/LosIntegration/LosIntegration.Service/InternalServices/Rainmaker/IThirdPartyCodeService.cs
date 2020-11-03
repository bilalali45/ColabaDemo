using System.Collections.Generic;
using LosIntegration.Model.Model.ServiceResponseModels.Rainmaker;

namespace LosIntegration.Service.InternalServices.Rainmaker
{
    public interface IThirdPartyCodeService
    {
        List<ThirdPartyCode> GetRefIdByThirdPartyId(int thirdPartyId);
    }
}