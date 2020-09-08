using Microsoft.EntityFrameworkCore;
using RainMaker.Common.Extensions;
using RainMaker.Data;
using RainMaker.Entity.Models;
using RainMaker.Service;
using System;
using System.Linq;
using System.Threading.Tasks;
using URF.Core.Abstractions;

namespace Rainmaker.Service
{
    public class UserProfileService : ServiceBase<RainMakerContext, UserProfile>, IUserProfileService
    {
        [Flags]
        public enum RelatedEntity
        {
            Employees = 1 << 0,
            Employees_EmployeeBusinessUnitEmails_EmailAccount = 1 << 1
        }


        public UserProfileService(IUnitOfWork<RainMakerContext> previousUow,
                                  IServiceProvider services) : base(
                                                                    previousUow: previousUow,
                                                                    services: services)
        {
        }


        public async Task<UserProfile> GetUserProfile(int userProfileId)
        {
            return await Uow.Repository<UserProfile>().Query(query: ss => ss.IsActive
                                                                          && ss.IsDeleted == false
                                                                          && ss.Id == userProfileId)
                            .FirstOrDefaultAsync();
        }


        public async Task<UserProfile> GetUserProfileEmployeeDetail(int? id = null,
                                                          RelatedEntity? includes = null)
        {
            var userProfiles = Repository.Query().AsQueryable();

            // @formatter:off 

            if (id.HasValue()) userProfiles = userProfiles.Where(predicate: userProfile => userProfile.Id == id);
            

            // @formatter:on 

            if (includes.HasValue)
                userProfiles = ProcessIncludes(query: userProfiles,
                                               includes: includes.Value);

            return await userProfiles.FirstOrDefaultAsync();
        }


        private IQueryable<UserProfile> ProcessIncludes(IQueryable<UserProfile> query,
                                                        RelatedEntity includes)
        {
            // @formatter:off 
            if (includes.HasFlag(flag:RelatedEntity.Employees)) query = query.Include(navigationPropertyPath:userProfile => userProfile.Employees);
            if (includes.HasFlag(flag:RelatedEntity.Employees_EmployeeBusinessUnitEmails_EmailAccount)) query = query.Include(navigationPropertyPath:userProfile => userProfile.Employees).ThenInclude(navigationPropertyPath:employee=>employee.EmployeeBusinessUnitEmails).ThenInclude(navigationPropertyPath:employeeBusinessUnitEmail=>employeeBusinessUnitEmail.EmailAccount);

            // @formatter:on 
            return query;
        }
    }
}