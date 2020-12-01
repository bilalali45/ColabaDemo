using Microsoft.EntityFrameworkCore;
using Rainmaker.Model;
using RainMaker.Common.Extensions;
using RainMaker.Data;
using RainMaker.Entity.Models;
using RainMaker.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using TrackableEntities.Common.Core;
using URF.Core.Abstractions;

namespace Rainmaker.Service
{
    public class UserProfileService : ServiceBase<RainMakerContext, UserProfile>, IUserProfileService
    {
        [Flags]
        public enum RelatedEntities
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
                                                                          && !ss.IsDeleted
                                                                          && ss.Id == userProfileId)
                            .FirstOrDefaultAsync();
        }


        public async Task<UserProfile> GetUserProfileEmployeeDetail(int? id = null,
                                                          RelatedEntities? includes = null)
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
                                                        RelatedEntities includes)
        {
            // @formatter:off 
            if (includes.HasFlag(flag: RelatedEntities.Employees)) query = query.Include(navigationPropertyPath: userProfile => userProfile.Employees);
            if (includes.HasFlag(flag: RelatedEntities.Employees_EmployeeBusinessUnitEmails_EmailAccount)) query = query.Include(navigationPropertyPath: userProfile => userProfile.Employees).ThenInclude(navigationPropertyPath: employee => employee.EmployeeBusinessUnitEmails).ThenInclude(navigationPropertyPath: employeeBusinessUnitEmail => employeeBusinessUnitEmail.EmailAccount);

            // @formatter:on 
            return query;
        }


        public async Task<List<Model.UserRole>> GetUserRoles(int userId)
        {
            var userRoles = await Uow.Repository<RainMaker.Entity.Models.UserRole>().Query(role => role.IsDeleted == false && role.IsActive && role.IsCustomerRole == false).ToListAsync();

            var userInRole = await Uow.Repository<UserInRole>().Query(role => role.UserId == userId).ToListAsync();

            List<Model.UserRole> lstUserRole = new List<Model.UserRole>();

            foreach (var role in userRoles)
            {
                lstUserRole.Add(new Model.UserRole { RoleId = role.Id, RoleName = role.RoleName, IsRoleAssigned = userInRole.Any(s => s.RoleId == role.Id) });
            }

            return lstUserRole;
        }

        public async Task UpdateUserRoles(List<Model.UserRole> userRoles, int userId)
        {
            var lstUserRole = await Uow.Repository<UserInRole>().Query(role => role.UserId == userId).Include(x=>x.UserRole).ToListAsync();

            if (lstUserRole != null && lstUserRole.Count > 0)
            {
                foreach (var role in lstUserRole)
                {
                    role.TrackingState = TrackingState.Deleted;

                    Uow.Repository<UserInRole>().Delete(role);
                    await Uow.SaveChangesAsync();
                }
               
            }
            if(userRoles.Any(x => x.IsRoleAssigned))
            {
                foreach (var role in userRoles)
                {
                    if (role.IsRoleAssigned)
                    {
                        var userProfile = await Uow.Repository<UserProfile>().Query(x => x.Id == userId).FirstOrDefaultAsync();
                        var userRole = await Uow.Repository<RainMaker.Entity.Models.UserRole>().Query(x => x.Id == role.RoleId).FirstOrDefaultAsync();

                        UserInRole userInRole = new UserInRole();
                        userInRole.UserId = userId;
                        userInRole.RoleId = role.RoleId;
                        userInRole.UserRole = userRole;
                        userInRole.UserProfile = userProfile;
                        userInRole.TrackingState = TrackingState.Added;

                        Uow.Repository<UserInRole>().Insert(userInRole);
                        await Uow.SaveChangesAsync();
                    }
                }
              
            }
        }
    }
}