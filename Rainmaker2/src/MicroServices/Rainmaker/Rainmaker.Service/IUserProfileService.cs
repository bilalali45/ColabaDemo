using System.Collections.Generic;
using RainMaker.Entity.Models;
using RainMaker.Service;
using System.Threading.Tasks;
using Rainmaker.Model;

namespace Rainmaker.Service
{
    public interface IUserProfileService : IServiceBase<UserProfile>
    {
        Task<UserProfile> GetUserProfile(int userProfileId);
        Task<UserProfile> GetUserProfileEmployeeDetail(int? id = null,
                                                           UserProfileService.RelatedEntities? includes = null);

        Task<List<Model.UserRole>> GetUserRoles(int userId);
        Task UpdateUserRoles(List<Model.UserRole> userRoles, int userId);
        Task<List<ByteUserNameModel>> GetLoanOfficers();
        Task UpdateByteUserName(List<Model.ByteUserNameModel> byteUserNameModel, int userId);
        Task<List<ByteBusinessUnitModel>> GetBusinessUnits();
    }
}
