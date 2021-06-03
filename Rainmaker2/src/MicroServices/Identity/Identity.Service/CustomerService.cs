using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Identity.Data;
using Identity.Entity.Models;
using Identity.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TenantConfig.Common;
using TenantConfig.Data;
using TenantConfig.Entity.Models;
using URF.Core.Abstractions;

namespace Identity.Service
{
    public interface  ICustomerService : IServiceBase<Customer>
    {
        Task<Customer> GetCustomerByUserIdAsync(long userId, int? tenandId, List<CustomerRelatedEntities> include = null);
        Task<Customer> GetCustomerByUserIdAsync(long userId, List<CustomerRelatedEntities> include = null);

        Task<ApiResponse> MapPhoneNumberFromOtpTracingAsync(int userId, int tenantId, string emailAddress, string sId,
                                                            bool markAsVerified);
    }
    public class CustomerService : ServiceBase<TenantConfigContext, Customer>, ICustomerService
    {
        private readonly IUnitOfWork<IdentityContext> _identityUow;
        private readonly ILogger<CustomerService> _logger;
        public CustomerService(IUnitOfWork<TenantConfigContext> previousUow, IServiceProvider services, IUnitOfWork<IdentityContext> identityUow, ILogger<CustomerService> logger) : base(previousUow, services)
        {
            this._identityUow = identityUow;
            this._logger = logger;
        }

        public async Task<Customer> GetCustomerByUserIdAsync(long userId, int? tenandId, List<CustomerRelatedEntities> includes = null)
        {
            var query = Uow.Repository<Customer>()
                .Query(x => x.TenantId == tenandId && x.UserId == userId && x.IsActive);

            if (includes != null)
            {
                query = base.ProcessIncludes<Customer, CustomerRelatedEntities>(query, includes);
            }
            var results = await query.FirstOrDefaultAsync();
            return results;
        }

        

        public async Task<Customer> GetCustomerByUserIdAsync(long userId, List<CustomerRelatedEntities> includes = null)
        {
            var query = Uow.Repository<Customer>()
                           .Query(x => x.UserId == userId && x.IsActive);

            if (includes != null)
            {
                query = base.ProcessIncludes<Customer, CustomerRelatedEntities>(query, includes);
            }
            var results = await query.FirstOrDefaultAsync();
            return results;
        }
        public async Task<ApiResponse> MapPhoneNumberFromOtpTracingAsync(int userId, int tenantId, string emailAddress, string sId, bool markAsVerified)
        {
            var otpLogs = await this._identityUow.Repository<OtpTracing>()
                .Query(x => x.TenantId == tenantId && x.OtpRequestId == sId && x.Email == emailAddress)
                .ToListAsync();

            if (otpLogs == null || otpLogs.Count == 0)
            {
                return new ApiResponse()
                {
                    Code = Convert.ToString((int)HttpStatusCode.NotFound),
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "OTP log against Sid not found."
                };
            }

            string otpLogPhoneNo = otpLogs?.FirstOrDefault().Phone;

            var customerInfo = await this.GetCustomerByUserIdAsync(userId, tenantId, new List<CustomerRelatedEntities>()
            {
                CustomerRelatedEntities.ContactPhoneInfo
            });

            if (customerInfo == null)
            {
                return new ApiResponse()
                {
                    Code = Convert.ToString((int)HttpStatusCode.NotFound),
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Customer detail not found."
                };
            }

            if (customerInfo.Contact == null)
            {
                return new ApiResponse()
                {
                    Code = Convert.ToString((int)HttpStatusCode.NotFound),
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Customer contact detail not found."
                };
            }

            var customerPhoneInfo = customerInfo.Contact.ContactPhoneInfoes
                .FirstOrDefault(phone => phone.Phone == otpLogPhoneNo);


            if (customerPhoneInfo == null)
            {
                ContactPhoneInfo phoneInfo = new ContactPhoneInfo()
                {
                    TenantId = tenantId,
                    Phone = otpLogPhoneNo,
                    TypeId = (int)PhoneType.Mobile,
                    ContactId = customerInfo.ContactId,
                    IsDeleted = false,
                    IsValid = markAsVerified
                };

                base.Uow.Repository<ContactPhoneInfo>().Insert(phoneInfo);
                await base.Uow.SaveChangesAsync();
                return new ApiResponse()
                {
                    Code = Convert.ToString((int) HttpStatusCode.Created),
                    Status = HttpStatusCode.Created.ToString(),
                    Message = "Phone number mapped with customer Id"
                };
            }

            var verifiedPhoneNumber = customerInfo.Contact.ContactPhoneInfoes
                .FirstOrDefault(phone => phone.IsValid == true);
            if (verifiedPhoneNumber == null && markAsVerified)
            {
                customerPhoneInfo.IsValid = true;
                base.Uow.Repository<ContactPhoneInfo>().Update(customerPhoneInfo);
                await base.Uow.SaveChangesAsync();
                return new ApiResponse()
                {
                    Code = Convert.ToString((int)HttpStatusCode.OK),
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Phone number marked as verified."
                };
            }

            return new ApiResponse()
            {
                Code = Convert.ToString((int)HttpStatusCode.BadRequest),
                Status = HttpStatusCode.BadRequest.ToString()
            }; ;
        }
    }
}
