using RainMaker.Entity.Models;
using RainMaker.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Rainmaker.Service
{
    public interface IUserProfileService : IServiceBase<UserProfile>
    {
        Task<UserProfile> GetUserProfile(int userProfileId);
    }
}
