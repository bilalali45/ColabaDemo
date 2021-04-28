using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RainMaker.Common;
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
    public class MembershipService : ServiceBase<RainMakerContext, UserProfile>, IMembershipService
    {
        public MembershipService(IUnitOfWork<RainMakerContext> previousUow,
                                 IServiceProvider services, ILogger<MembershipService> logger) : base(previousUow: previousUow,
                                                                   services: services)
        {
            this.logger = logger;
        }

        private readonly ILogger<MembershipService> logger;

        public async Task<UserProfile> ValidateUser(int tenantId, string userName,
                                        string password,
                                        bool employee = false)
        {
            if (string.IsNullOrEmpty(value: userName))
                throw new ArgumentException(message: "Value cannot be null or empty.",
                                            paramName: "userName");
            if (string.IsNullOrEmpty(value: password))
                throw new ArgumentException(message: "Value cannot be null or empty.",
                                            paramName: "password");

            UserProfile userProfile = null;
            if (!employee)
                userProfile = GetUser(userName: userName);
            else
                userProfile = GetEmployeeUser(userName: userName);

            if (userProfile != null)
            {
                var tempPwd = password.ApplyPasswordEncryptionUsingSalt(format: (PasswordEncryptionFormates)userProfile.PasswordFormatId,
                                                                        key: userProfile.PasswordSalt);

                PasswordPolicy passwordPolicy = await Uow.Repository<PasswordPolicy>().Query(x => x.TenantId == tenantId).FirstAsync();
                var incorrectPasswordCount = passwordPolicy.IncorrectPasswordCount;

                if (tempPwd != userProfile.Password)
                {
                    logger.LogInformation($"password is incorrect for user {userName}");
                    //update wrong attempt count
                    if (userProfile.FailedPasswordAttemptCount == null ||
                        userProfile.FailedPasswordAttemptCount.Value < incorrectPasswordCount)
                    {
                        userProfile.FailedPasswordAttemptCount = userProfile.FailedPasswordAttemptCount == null
                            ?
                            1
                            : (userProfile.FailedPasswordAttemptCount.Value == incorrectPasswordCount
                                ? incorrectPasswordCount
                                : userProfile.FailedPasswordAttemptCount.Value + 1);

                        userProfile.ModifiedOnUtc = DateTime.UtcNow;
                        logger.LogInformation($"Failed attempt count {userProfile.FailedPasswordAttemptCount}");
                        if (userProfile.FailedPasswordAttemptCount == incorrectPasswordCount &&
                            (userProfile.IsLockedOut == null || !userProfile.IsLockedOut.Value))
                        {
                            // lock the account
                            logger.LogInformation($"locking the account since threshold reached");
                            userProfile.IsLockedOut = true;
                            userProfile.LastLockedOutDateUtc = DateTime.UtcNow;
                        }

                        userProfile.TrackingState = TrackableEntities.Common.Core.TrackingState.Modified;
                        await Uow.SaveChangesAsync();
                    }
                    else if ((userProfile.IsLockedOut != null && userProfile.IsLockedOut.Value) && userProfile.LastLockedOutDateUtc != null &&
                        (userProfile.FailedPasswordAttemptCount != null && userProfile.FailedPasswordAttemptCount.Value >= incorrectPasswordCount))
                    {
                        logger.LogInformation($"account is locked");
                        var hasLockTimeOver = DateTime.UtcNow > userProfile.LastLockedOutDateUtc.Value.AddMinutes(passwordPolicy.AccountLockDurationInMinutes.ToInt());

                        if (hasLockTimeOver)
                        {
                            // reset wrong password attempt as 1
                            userProfile.IsLockedOut = false;
                            userProfile.LastLockedOutDateUtc = null;
                            userProfile.FailedPasswordAttemptCount = 1;
                            logger.LogInformation($"removing lock since threshold time has been passed");
                            userProfile.TrackingState = TrackableEntities.Common.Core.TrackingState.Modified;
                            await Uow.SaveChangesAsync();
                        }
                    }

                    userProfile = null;
                }
                else
                {
                    //password is correct

                    if (userProfile.IsLockedOut != null && userProfile.IsLockedOut.Value)
                    {
                        //check lock out time expiration

                        if (userProfile.LastLockedOutDateUtc != null)
                        {
                            var hasLockTimeOver = DateTime.UtcNow > userProfile.LastLockedOutDateUtc.Value.AddMinutes(passwordPolicy.AccountLockDurationInMinutes.ToInt());

                            if (hasLockTimeOver)
                            {
                                userProfile.IsLockedOut = false;
                                userProfile.LastLockedOutDateUtc = null;
                                userProfile.FailedPasswordAttemptCount = null;
                                logger.LogInformation($"removing lock since threshold time has been passed");
                                userProfile.TrackingState = TrackableEntities.Common.Core.TrackingState.Modified;
                                await Uow.SaveChangesAsync();
                            }
                            else
                            {
                                logger.LogInformation($"account is locked");
                                // account is locked
                                userProfile = null;
                            }
                        }
                    }
                    else if (userProfile.FailedPasswordAttemptCount != null)
                    {
                        userProfile.IsLockedOut = false;
                        userProfile.LastLockedOutDateUtc = null;
                        userProfile.FailedPasswordAttemptCount = null;
                        logger.LogInformation($"updated failed count to be null");
                        userProfile.TrackingState = TrackableEntities.Common.Core.TrackingState.Modified;
                        await Uow.SaveChangesAsync();
                    }
                }
            }

            return userProfile;
        }


        public UserProfile GetUser(string userName)
        {
            try
            {
               
                return Repository.Query(query: userProfile => userProfile.UserName.ToLower().Trim() == userName.ToLower().Trim() && userProfile.IsActive && !userProfile.IsDeleted)
                                 .Include(navigationPropertyPath: x => x.Customers).ThenInclude(customer=>customer.Contact)
                                 .FirstOrDefault();
            }
            catch (ArgumentException)
            {
                throw new RainMakerException("Unable to get user from repository");
            }
        }


        public UserProfile GetEmployeeUser(string userName)
        {
            var query = Repository.Query(query: userProfile => userProfile.UserName.ToLower().Trim() == userName.ToLower().Trim() && userProfile.IsActive && !userProfile.IsDeleted)
                                  .Include(navigationPropertyPath: userProfile => userProfile.Employees).ThenInclude(employee => employee.Contact)
                                  .Include(x=>x.Employees).ThenInclude(navigationPropertyPath: employee => employee.EmployeePhoneBinders).ThenInclude(navigationPropertyPath: employeePhoneBinder => employeePhoneBinder.CompanyPhoneInfo)
                                  .ToList();

            if (query.Count > 1)
                throw new RainMakerException("Multiple users found.");

            return query.FirstOrDefault();
        }
    }
}