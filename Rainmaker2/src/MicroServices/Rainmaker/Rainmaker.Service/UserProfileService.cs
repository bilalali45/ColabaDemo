using Microsoft.EntityFrameworkCore;
using RainMaker.Data;
using RainMaker.Entity.Models;
using RainMaker.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URF.Core.Abstractions;

namespace Rainmaker.Service
{
    public class UserProfileService : ServiceBase<RainMakerContext, UserProfile>, IUserProfileService 
    {
        public UserProfileService(IUnitOfWork<RainMakerContext> previousUow, IServiceProvider services) : base(previousUow, services)
        {
        }
        public async Task<UserProfile>  GetUserProfile(int userProfileId)
        {
            return await Uow.Repository<UserProfile>().Query(ss => ss.IsActive == true
                       && ss.IsDeleted == false
                       && ss.Id == userProfileId).FirstOrDefaultAsync();
        }
    }
}
