using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Data;
using Identity.Entity.Models;
using Identity.Model.TwoFA;
using Identity.Models.TwoFA;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TenantConfig.Common.DistributedCache;
using URF.Core.Abstractions;

namespace Identity.Service
{
    public interface IOtpTracingService :  IServiceBase<OtpTracing>
    {
        Task<int> AddLogAsync(OtpTracing tracing);
        Task<int> Create2FaLogAsync(string phoneNumber, string codeEntered, int? contactId, string email, string message, string otpRequestId,
            TenantModel contextTenant, TwilioTwoFaResponseModel verifyResponse);
        Task<int> GetVerificationAttemptsCountAsync(string sId);
        Task<IList<OtpTracing>> GetSendAttemptsAsync(string sId);
        Task<OtpTracing> GetLastSendAttemptAsync(string phoneNumber, string sId);
        Task<bool> OtpVerificationExists(string phoneNumber);
        Task<bool> VerifyPhoneSidCombination(string phoneNumber, string sId);
    }

    public class OtpTracingService : ServiceBase<IdentityContext, OtpTracing>, IOtpTracingService
    {
        private readonly IUnitOfWork<IdentityContext> _identityUow;
        private readonly IActionContextAccessor _accessor;
        private readonly ITokenService _tokenService;
        private readonly ILogger<OtpTracingService> _logger;

        public OtpTracingService(IUnitOfWork<IdentityContext> identityUow, IServiceProvider services, IActionContextAccessor accessor, ITokenService tokenService, ILogger<OtpTracingService> logger) : base(identityUow, services)
        {
            _identityUow = identityUow;
            _accessor = accessor;
            _tokenService = tokenService;
            _logger = logger;
        }

        public async Task<int> AddLogAsync(OtpTracing tracing)
        {
            base.Insert(tracing);
            return await this._identityUow.SaveChangesAsync();
        }

        public async Task<int> Create2FaLogAsync(string phoneNumber, string codeEntered, int? contactId, string email, string message, string otpRequestId, TenantModel contextTenant, TwilioTwoFaResponseModel verifyResponse)
        {
            int recordCount = 0;

            if (!string.IsNullOrEmpty(phoneNumber) && (!phoneNumber.StartsWith("+1")))
            {
                phoneNumber = $"+1{phoneNumber}";
            }
            OtpTracing traceLog = new OtpTracing()
            {
                BranchId = contextTenant.Branches[0].Id,
                CarrierName = verifyResponse.Lookup == null ? null :  verifyResponse.Lookup.Carrier.Name,
                CarrierType = verifyResponse.Lookup == null ? null : verifyResponse.Lookup.Carrier.Type,
                CodeEntered = codeEntered,
                IpAddress = Convert.ToString(this._accessor.ActionContext.HttpContext.Connection.RemoteIpAddress),
                Phone = phoneNumber,
                ContactId = contactId,
                DateUtc = DateTime.UtcNow,
                Email = email,
                EntityIdentifier = new Guid(), // TODO,
                Message = message,
                OtpCreatedOn = verifyResponse.DateCreated,
                OtpRequestId = otpRequestId,
                OtpUpdatedOn = verifyResponse.DateUpdated,
                ResponseJson = JsonConvert.SerializeObject(verifyResponse),
                StatusCode = verifyResponse.StatusCode,
                TenantId = contextTenant.Id,
                TracingTypeId = null
            };
            var token = Convert.ToString(this._accessor.ActionContext.HttpContext.Request.Headers["Authorization"]);
            if (!string.IsNullOrEmpty(token))
            {
                var principal = await this._tokenService.GetPrincipalFrom2FaToken(token.Split(' ')[1]);
                if (principal == null)
                {
                    this._logger.LogWarning("Cannot read principal from token data. Skipping OTP Tracing.");
                }
                else
                {
                    traceLog.ContactId = int.Parse(principal.Claims.Where(c => c.Type == "ContactId")
                        .FirstOrDefault().Value);
                    traceLog.Email = principal.Claims.Where(c => c.Type == "Email").FirstOrDefault().Value;
                }
            }
            recordCount = await this.AddLogAsync(traceLog);

            return recordCount;
        }

        public async Task<int> GetVerificationAttemptsCountAsync(string sId)
        {
            var recycleMinutes = 10;
            DateTime startDate = DateTime.UtcNow.AddMinutes(recycleMinutes * -1);
            DateTime endDate = DateTime.UtcNow;
            var results = await Uow.Repository<OtpTracing>()
                .Query(x => x.OtpRequestId == sId && x.CodeEntered != null && (x.DateUtc >= startDate && x.DateUtc <= endDate)).ToListAsync();

            if (results == null)
            {
                return 0;
            }

            return results.Count;
        }

        public async Task<IList<OtpTracing>> GetSendAttemptsAsync(string sId)
        {
            var results = await Uow.Repository<OtpTracing>()
                .Query(x => x.OtpRequestId == sId && x.CodeEntered != null).ToListAsync();

            return results;
        }

        public async Task<OtpTracing> GetLastSendAttemptAsync(string phoneNumber, string sId)
        {
            OtpTracing results = null;
            if (string.IsNullOrEmpty(phoneNumber))
            {
                results = await Uow.Repository<OtpTracing>()
                    .Query(x => (x.OtpRequestId == sId)
                    ).OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            }
            else
            {
                if (!phoneNumber.StartsWith("+1"))
                {
                    phoneNumber = $"+1{phoneNumber}";
                }
                results = await Uow.Repository<OtpTracing>()
                    .Query(x => ((x.Phone == phoneNumber && x.OtpRequestId != null)
                                 //|| (x.Phone == "+1" + phoneNumber)
                                 )
                    ).OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            }

            return results;
        }

        public async Task<bool> OtpVerificationExists(string phoneNumber)
        {
            if (!phoneNumber.StartsWith("+1"))
            {
                phoneNumber = $"+1{phoneNumber}";
            }
            var results = await Uow.Repository<OtpTracing>()
                .Query(x => ((x.Phone == phoneNumber && x.Message == "approved")
                        //|| (x.Phone == "+1" + phoneNumber)
                    )
                ).OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            return results != null;
        }

        public async Task<bool> VerifyPhoneSidCombination(string phoneNumber, string sId)
        {
            if (!phoneNumber.StartsWith("+1"))
            {
                phoneNumber = $"+1{phoneNumber}";
            }
            var results = await Uow.Repository<OtpTracing>()
                .Query(x => x.Phone == phoneNumber && x.OtpRequestId == sId).FirstOrDefaultAsync();

            return results != null;
        }
    }
}
