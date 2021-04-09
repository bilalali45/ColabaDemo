using RainMaker.Entity.Models;
using RainMaker.Service;
using System.Threading.Tasks;

namespace Rainmaker.Service
{
    public interface IMembershipService : IServiceBase<UserProfile>
    {
        Task<UserProfile> ValidateUser(int tenantId,string userName,
                                 string password,
                                 bool employee = false);


        UserProfile GetUser(string userName);
        UserProfile GetEmployeeUser(string userName);
    }
}