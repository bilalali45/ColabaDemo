using System.Collections.Generic;
using RainMaker.Entity.Models;
using RainMaker.Service;

namespace Rainmaker.Service
{
    public interface IThirdPartyCodeService : IServiceBase<ThirdPartyCode>
    {
        List<ThirdPartyCode> GetRefIdByThirdPartyId(int thirdPartyId);
    }
}