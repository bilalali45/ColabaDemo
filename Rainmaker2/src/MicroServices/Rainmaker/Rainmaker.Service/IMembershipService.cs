using RainMaker.Entity.Models;
using RainMaker.Service;

namespace Rainmaker.Service
{
    public interface IMembershipService : IServiceBase<UserProfile>
    {
        UserProfile ValidateUser(string userName,
                                 string password,
                                 bool employee = false);


        UserProfile GetUser(string userName);
        UserProfile GetEmployeeUser(string userName);
    }
}