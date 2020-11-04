using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using RainMaker.Data;
using RainMaker.Entity.Models;
using RainMaker.Service;
using URF.Core.Abstractions;

namespace Rainmaker.Service
{
    public class ThirdPartyCodeService : ServiceBase<RainMakerContext, ThirdPartyCode>, IThirdPartyCodeService
    {
        public ThirdPartyCodeService(IUnitOfWork<RainMakerContext> previousUow,
                                      IServiceProvider services, ICommonService commonService) : base(previousUow: previousUow,
                                                                                                      services: services)
        {
            //this.commonService = commonService;
        }


        public List<ThirdPartyCode> GetRefIdByThirdPartyId(int thirdPartyId)
        {
            return Uow.Repository<ThirdPartyCode>().Query(x => x.ThirdPartyId == thirdPartyId).ToList();
        }


    }
}
