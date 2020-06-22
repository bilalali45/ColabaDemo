using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RainMaker.Common;
using RainMaker.Common.Extensions;
using RainMaker.Data;
using RainMaker.Entity.Models;
using RainMaker.Service;
using URF.Core.Abstractions;

namespace Rainmaker.Service
{
    public class MembershipService : ServiceBase<RainMakerContext, UserProfile>, IMembershipService
    {
        public MembershipService(IUnitOfWork<RainMakerContext> previousUow,
                                 IServiceProvider services) : base(previousUow: previousUow,
                                                                   services: services)
        {
        }


        public UserProfile ValidateUser(string userName,
                                        string password,
                                        bool employee = false)
        {
            if (string.IsNullOrEmpty(value: userName))
                throw new ArgumentException(message: "Value cannot be null or empty.",
                                            paramName: "userName");
            if (string.IsNullOrEmpty(value: password))
                throw new ArgumentException(message: "Value cannot be null or empty.",
                                            paramName: "password");

            UserProfile requiredUser = null;
            if (!employee)
                requiredUser = GetUser(userName: userName);
            else
                requiredUser = GetEmployeeUser(userName: userName);

            if (requiredUser != null)
            {
                var tempPwd = password.ApplyPasswordEncryptionUsingSalt(format: (PasswordEncryptionFormates) requiredUser.PasswordFormatId,
                                                                        key: requiredUser.PasswordSalt);

                if (tempPwd != requiredUser.Password)
                    requiredUser = null;
            }

            return requiredUser;
        }


        public UserProfile GetUser(string userName)
        {
            try
            {
                //return Uow.Repository<UserProfile>().Query(x => x.UserName.ToLower().Trim() == userName.ToLower().Trim() && x.IsActive != false && x.IsDeleted != true)
                //          .Include(x => x.Customers)
                //          .Select().FirstOrDefault();

                return Repository.Query(query: userProfile => userProfile.UserName.ToLower().Trim() == userName.ToLower().Trim() && userProfile.IsActive && userProfile.IsDeleted != true)
                                 //.Include(navigationPropertyPath: x => x.Customers)
                                 .Include(navigationPropertyPath: x => x.Customers).ThenInclude(customer=>customer.Contact)
                                 .FirstOrDefault();
            }
            catch (ArgumentException)
            {
                throw new Exception();
            }
        }


        public UserProfile GetEmployeeUser(string userName)
        {
            var query = Repository.Query(query: userProfile => userProfile.UserName.ToLower().Trim() == userName.ToLower().Trim() && userProfile.IsActive && userProfile.IsDeleted != true)
                                  .Include(navigationPropertyPath: userProfile => userProfile.Employees).ThenInclude(employee => employee.Contact)
                                  .ThenInclude(navigationPropertyPath: employee => employee.EmployeePhoneBinders).ThenInclude(navigationPropertyPath: employeePhoneBinder => employeePhoneBinder.CompanyPhoneInfo)
                                  .ToList();

            if (query.Count() > 1)
                throw new Exception(message: "Multiple users found.");

            return query.FirstOrDefault();
        }
    }
}