using Microsoft.EntityFrameworkCore;
using RainMaker.Common;
using RainMaker.Data;
using RainMaker.Entity.Models;
using RainMaker.Service;
using System;
using System.Linq;
using System.Threading.Tasks;
using URF.Core.Abstractions;

namespace Rainmaker.Service
{
    public class ActivityService : ServiceBase<RainMakerContext, Activity>, IActivityService
    {
        public ActivityService(IUnitOfWork<RainMakerContext> previousUow, IServiceProvider services)
           : base(previousUow, services)
        {
            
        }

        public async Task<Activity> GetCustomerActivity(int? businessUnitId, ActivityForType activityFor)
        {
            var activity = Uow.Repository<Activity>().Query(item => (item.BusinessUnitId == null || item.BusinessUnitId == businessUnitId) && item.ActivityForId == (int)activityFor && (item.IsCustomerDefault != null && item.IsCustomerDefault.Value) && item.IsActive == true && item.IsDeleted != true)
                .OrderByDescending(x => x.BusinessUnitId).FirstOrDefaultAsync();

            return await activity;
        }
    }
}
