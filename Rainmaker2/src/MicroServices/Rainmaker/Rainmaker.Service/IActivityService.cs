using RainMaker.Common;
using RainMaker.Entity.Models;
using RainMaker.Service;
using System.Threading.Tasks;

namespace Rainmaker.Service
{
    public interface IActivityService : IServiceBase<Activity>
    {
        Task<Activity> GetCustomerActivity(int? businessUnitId, ActivityForType activityFor);
    }
}
