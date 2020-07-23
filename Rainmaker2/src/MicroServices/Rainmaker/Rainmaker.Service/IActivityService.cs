using RainMaker.Entity.Models;
using RainMaker.Common;
using System.Threading.Tasks;
using RainMaker.Service;

namespace Rainmaker.Service
{
    public interface IActivityService : IServiceBase<Activity>
    {
        Task<Activity> GetCustomerActivity(int? businessUnitId, ActivityForType activityFor);
    }
}
