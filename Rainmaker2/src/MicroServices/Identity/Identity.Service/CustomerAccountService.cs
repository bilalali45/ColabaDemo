using Identity.Data;
using Identity.Entity.Models;
using Identity.Model;
using Identity.Model.TwoFA;
using Identity.Models.TwoFA;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TenantConfig.Common;
using TenantConfig.Common.DistributedCache;
using TenantConfig.Data;
using TenantConfig.Entity.Models;
using URF.Core.Abstractions;
using TokenCacheHelper.Models;
using Microsoft.Extensions.Configuration;
using TokenCacheHelper.TokenManager;

namespace Identity.Service
{
    public class CustomerAccountService : ServiceBase<IdentityContext, User>, ICustomerAccountService
    {
        private readonly IUnitOfWork<IdentityContext> _previousUow;
        private readonly IUnitOfWork<TenantConfigContext> _uowTenantConfig;
        private readonly ILogger<CustomerAccountService> _logger;
        private readonly ITokenService _tokenService;
        private readonly ITenantConfigService _tenantConfigService;
        private readonly ICustomerService _customerService;
        private readonly ITwoFactorAuth _twoFactorAuthService;
        private readonly IActionContextAccessor _accessor;
        private readonly IOtpTracingService _otpTracingService;
        private readonly ITwoFaHelper _twoFaHelper;
        private readonly IConfiguration _configuration;
        private readonly ITokenManager _tokenManager;
        public CustomerAccountService(IUnitOfWork<IdentityContext> previousUow, IServiceProvider services, IUnitOfWork<TenantConfigContext> uowTenantConfig,
            ILogger<CustomerAccountService> logger, ITokenService tokenService, ITenantConfigService tenantConfigService, ICustomerService customerService, ITwoFactorAuth twoFactorAuthService, IActionContextAccessor accessor, IOtpTracingService opTracingService, ITwoFaHelper twoFaHelper, IConfiguration configuration, ITokenManager tokenManager) : base(previousUow, services)
        {
            _previousUow = previousUow;
            _uowTenantConfig = uowTenantConfig;
            _logger = logger;
            this._logger = logger;
            _tokenService = tokenService;
            _tenantConfigService = tenantConfigService;
            _customerService = customerService;
            _twoFactorAuthService = twoFactorAuthService;
            _accessor = accessor;
            _otpTracingService = opTracingService;
            _twoFaHelper = twoFaHelper;
            _tokenManager = tokenManager;
            _configuration = configuration;
        }
        //public async Task<ApiResponse> DeleteUser(string email, int tenantId)
        //{
        //    User user = await Uow.Repository<User>().Query(x => x.IsActive == true && x.UserName.ToLower() == email.ToLower() && x.TenantId == tenantId && x.UserTypeId == (byte)TenantConfig.Common.UserType.Customer).SingleAsync();
        //    user.UserName = DateTime.Now.ToString("yyyyMMddHHmmss") + user.UserName;
        //    Uow.Repository<User>().Update(user);
        //    await Uow.SaveChangesAsync();
        //    return new ApiResponse { Message="User has been deleted." };
        //}
        public async Task<bool> DoesCustomerAccountExist(string email, int tenantId)
        {
            return (await Uow.Repository<User>().Query(x => x.IsActive==true && x.UserName.ToLower() == email.ToLower() && x.TenantId == tenantId && x.UserTypeId == (byte)TenantConfig.Common.UserType.Customer).CountAsync()) >= 1;
        }

        public async Task<int> Register(RegisterModel model, int tenantId, bool is2FaVerified)
        {
            User user = new User
            {
                IsActive = true,
                UserTypeId = (byte)TenantConfig.Common.UserType.Customer,
                UserName = model.Email.ToLower(),
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                TenantId = tenantId,
                CreatedOnUtc = DateTime.UtcNow
            };

            user.Password = model.Password.ApplyPasswordEncryption(PasswordEncryptionFormat.ShaHashed, out string key);
            user.PasswordSalt = key;
            user.PasswordFormatId = (int)PasswordEncryptionFormat.ShaHashed;

            var tenant2FaConfig = await this._tenantConfigService.GetTenant2FaConfigAsync(tenantId);
            switch(tenant2FaConfig.BorrowerTwoFaModeId)
            {
                case 1: // Required For All
                    user.TwoFaEnabled = true;
                    break;

                case 2: // Required For None
                    user.TwoFaEnabled = false;
                    break;
            }

            Uow.Repository<User>().Insert(user);
            await Uow.SaveChangesAsync();

            Contact contact = new Contact
            {
                ContactEmailInfoes = new HashSet<ContactEmailInfo>
                {
                    new ContactEmailInfo
                    {
                        Email=model.Email.ToLower(),
                        IsDeleted=false,
                        IsValid=false,
                        TenantId=tenantId,
                        TypeId=(int)EmailType.Primary,
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                    }
                },
                FirstName = model.FirstName,
                LastName = model.LastName,
                TenantId = tenantId,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                Customer = new Customer
                {
                    IsActive = true,
                    TenantId = tenantId,
                    UserId = user.Id,
                    CreatedOnUtc = DateTime.UtcNow,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                }
            };
            if (!string.IsNullOrEmpty(model.Phone))
            {
                contact.ContactPhoneInfoes.Add(new ContactPhoneInfo
                {
                    Phone = PhoneHelper.UnMask(model.Phone),
                    IsDeleted = false,
                    IsValid = is2FaVerified,
                    TenantId = tenantId,
                    TypeId = (int)PhoneType.Mobile,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                });
            }

            _uowTenantConfig.Repository<Contact>().Insert(contact);
            await _uowTenantConfig.SaveChangesAsync();
            return user.Id;
        }

        public async Task<ApiResponse> Signin(SigninModel model, TenantModel tenant)
        {
            var userProfile = await Uow.Repository<User>().Query(x => x.UserName.ToLower() == model.Email.ToLower() && x.TenantId == tenant.Id && x.UserTypeId == (byte)TenantConfig.Common.UserType.Customer && x.IsActive).FirstOrDefaultAsync();
            if (userProfile == null)
            {
                return new ApiResponse { Code = "400", Message = "User does not exist" };
            }
            var tempPwd = model.Password.ApplyPasswordEncryptionWithSalt((PasswordEncryptionFormat)userProfile.PasswordFormatId, userProfile.PasswordSalt);
            PasswordPolicy passwordPolicy = await Uow.Repository<PasswordPolicy>().Query(x => x.TenantId == tenant.Id).FirstAsync();
            var incorrectPasswordCount = passwordPolicy.IncorrectPasswordCount;

            if (tempPwd != userProfile.Password)
            {
                _logger.LogInformation($"password is incorrect for user {model.Email}");
                //update wrong attempt count
                if (userProfile.FailedPasswordAttemptCount == null ||
                    userProfile.FailedPasswordAttemptCount.Value < incorrectPasswordCount)
                {
                    userProfile.FailedPasswordAttemptCount = userProfile.FailedPasswordAttemptCount == null
                        ?
                        1
                        : userProfile.FailedPasswordAttemptCount.Value == incorrectPasswordCount
                            ? incorrectPasswordCount
                            : userProfile.FailedPasswordAttemptCount.Value + 1;

                    userProfile.ModifiedOnUtc = DateTime.UtcNow;
                    _logger.LogInformation($"Failed attempt count {userProfile.FailedPasswordAttemptCount}");
                    if (userProfile.FailedPasswordAttemptCount == incorrectPasswordCount &&
                        (userProfile.IsLockedOut == null || userProfile.IsLockedOut.Value == false))
                    {
                        // lock the account_logger.LogInformation($"locking the account since threshold reached");
                        userProfile.IsLockedOut = true;
                        userProfile.LastLockedOutDateUtc = DateTime.UtcNow;
                    }

                    userProfile.TrackingState = TrackableEntities.Common.Core.TrackingState.Modified;
                    await Uow.SaveChangesAsync();
                }
                else if ((userProfile.IsLockedOut != null && userProfile.IsLockedOut.Value) && userProfile.LastLockedOutDateUtc != null &&
                    (userProfile.FailedPasswordAttemptCount != null && userProfile.FailedPasswordAttemptCount.Value >= incorrectPasswordCount))
                {
                    _logger.LogInformation($"account is locked");
                    var hasLockTimeOver = DateTime.UtcNow > userProfile.LastLockedOutDateUtc.Value.AddMinutes(passwordPolicy.AccountLockDurationInMinutes);

                    if (hasLockTimeOver)
                    {
                        // reset wrong password attempt as 1
                        userProfile.IsLockedOut = false;
                        userProfile.LastLockedOutDateUtc = null;
                        userProfile.FailedPasswordAttemptCount = 1;
                        _logger.LogInformation($"removing lock since threshold time has been passed");
                        userProfile.TrackingState = TrackableEntities.Common.Core.TrackingState.Modified;
                        await Uow.SaveChangesAsync();
                    }
                }

                return new ApiResponse { Code = "400", Message = "User does not exist" };
            }
            else
            {
                //password is correct

                if (userProfile.IsLockedOut != null && userProfile.IsLockedOut.Value)
                {
                    //check lock out time expiration

                    if (userProfile.LastLockedOutDateUtc != null)
                    {
                        var hasLockTimeOver = DateTime.UtcNow > userProfile.LastLockedOutDateUtc.Value.AddMinutes(passwordPolicy.AccountLockDurationInMinutes);

                        if (hasLockTimeOver)
                        {
                            userProfile.IsLockedOut = false;
                            userProfile.LastLockedOutDateUtc = null;
                            userProfile.FailedPasswordAttemptCount = null;
                            _logger.LogInformation($"removing lock since threshold time has been passed");
                            userProfile.TrackingState = TrackableEntities.Common.Core.TrackingState.Modified;
                            await Uow.SaveChangesAsync();
                        }
                        else
                        {
                            _logger.LogInformation($"account is locked");
                            // account is locked
                            return new ApiResponse { Code = "400", Message = "Your account has been locked" };
                        }
                    }
                }
                else if (userProfile.FailedPasswordAttemptCount != null)
                {
                    userProfile.IsLockedOut = false;
                    userProfile.LastLockedOutDateUtc = null;
                    userProfile.FailedPasswordAttemptCount = null;
                    _logger.LogInformation($"updated failed count to be null");
                    userProfile.TrackingState = TrackableEntities.Common.Core.TrackingState.Modified;
                    await Uow.SaveChangesAsync();
                }
            }

            var contact = await _uowTenantConfig.Repository<Customer>().Query(x => x.IsActive && x.TenantId == tenant.Id && x.UserId == userProfile.Id).Include(x => x.Contact)
                .Select(x => x.Contact).FirstOrDefaultAsync();

            if (contact == null)
            {
                return new ApiResponse { Code = "400", Message = "Contact does not exist" };
            }

            ApiResponse response = new ApiResponse { Code = "200" };

            bool dontAskCookieExists =
                this._accessor.ActionContext.HttpContext.Request.Cookies[
                    this._twoFaHelper.CreateCookieName(tenant.Code, userProfile.Id)] != null;

            if ((!model.IsDevMode) // TODO : REMOVE
                && (!dontAskCookieExists) && await this.Requires2Fa(tenant.Id, userProfile.Id)) 
            {
                var tenant2FaConfig = await this._tenantConfigService.GetTenant2FaConfigAsync(tenant.Id,
                    new List<TwoFaConfigEntities>()
                    {
                        TwoFaConfigEntities.Tenant
                    });

                if (tenant2FaConfig == null)
                {
                    throw new Exception("Tenant 2FA configuration not found.");
                }
                else
                {
                    if (string.IsNullOrEmpty(tenant2FaConfig.TwilioVerifyServiceId))
                    {
                        this._logger.LogWarning("2FA requires verification SID which is missing in tenant 2FA config.");
                    }
                    else
                    {
                        this._twoFactorAuthService.SetServiceSid(tenant2FaConfig.TwilioVerifyServiceId);
                        var customerInfo = await this._customerService.GetCustomerByUserIdAsync(userProfile.Id, tenant.Id,
                            new List<CustomerRelatedEntities>()
                            {
                                CustomerRelatedEntities.ContactPhoneInfo
                            });

                        customerInfo.Contact.ContactPhoneInfoes = customerInfo.Contact.ContactPhoneInfoes
                            .Where(phone => phone.IsDeleted == false)
                            .ToList();

                        var verifiedPhoneCount = customerInfo.Contact.ContactPhoneInfoes
                            .Count(phone => phone.IsValid == true);
                        if (verifiedPhoneCount > 0)
                        {
                            customerInfo.Contact.ContactPhoneInfoes = customerInfo.Contact.ContactPhoneInfoes
                                .Where(phone => phone.IsValid == true)
                                .ToList();
                        }

                        List<Claim> twoFaClaims = new List<Claim>()
                            {
                                new Claim(ClaimTypes.Role, "Customer"),
                                new Claim(type: "UserProfileId", value: userProfile.Id.ToString()),
                                new Claim(type: "ContactId", customerInfo.ContactId.ToString()),
                                new Claim(type: "Email", model.Email),
                                new Claim(type: "TenantCode", tenant.Code.ToLower())
                            };

                        var twoFaJwtToken = await this._tokenService.Generate2FaTokenAsync(twoFaClaims);
                        var twoFaToken = new JwtSecurityTokenHandler().WriteToken(token: twoFaJwtToken);
                        TwoFaAuthModel twoFaAuth = new TwoFaAuthModel()
                        {
                            PhoneNoMissing = customerInfo.Contact.ContactPhoneInfoes.Count == 0,
                            RequiresTwoFa = true,
                            Status = ApiResponse.ApiResponseStatus.Success,
                            TokenData = twoFaToken
                        };
                        TwilioTwoFaResponseModel twoFaResponse = new TwilioTwoFaResponseModel();
                        if (!twoFaAuth.PhoneNoMissing)
                        {
                            twoFaResponse =
                                await this._twoFactorAuthService.Create2FaRequestAsync(customerInfo.Contact.ContactPhoneInfoes.First().Phone, null, this._twoFaHelper.SendDigits);
                            #region Removed Code
                            //if (!twoFaResponse.CanSend2Fa)
                            //{
                            //    if (twoFaResponse.StatusCode == (int) HttpStatusCode.TooManyRequests)
                            //    {
                            //        response.Message = string.Format(Constants.MAX_RESEND_MESSAGE,
                            //            this._twoFaHelper.TwoFaRecycleMinutes);
                            //        //response.Status = HttpStatusCode.TooManyRequests.ToString();
                            //        //response.Code = ((int)HttpStatusCode.TooManyRequests).ToString();
                            //        var lastAttempt = await this._otpTracingService.GetLastSendAttemptAsync(
                            //            customerInfo.Contact.ContactPhoneInfoes.First().Phone, null);
                            //        if (lastAttempt != null && (!string.IsNullOrEmpty(lastAttempt.ResponseJson)))
                            //        {
                            //            var lastAttemptResponse =
                            //                JsonConvert.DeserializeObject<TwilioTwoFaResponseModel>(lastAttempt
                            //                    .ResponseJson);
                            //            if (!string.IsNullOrEmpty(lastAttemptResponse.Sid))
                            //            {
                            //                twoFaResponse.VerifyAttemptsCount =
                            //                    await this._otpTracingService.GetVerificatioAttemptsCountAsync(
                            //                        lastAttemptResponse.Sid);
                            //            }
                            //        }
                            //    }
                            //    //else
                            //    //{
                            //    //    response.Status = HttpStatusCode.BadRequest.ToString();
                            //    //    response.Code = ((int)HttpStatusCode.BadRequest).ToString();
                            //    //}

                            //    response.Data = twoFaResponse ;
                            //    return response;
                            //}
                            //var next2FaModel = await
                            //    this._twoFaHelper.CanSendOtpAsync(
                            //        customerInfo.Contact.ContactPhoneInfoes.First().Phone, null);
                            //if (twoFaResponse.StatusCode == (int)HttpStatusCode.TooManyRequests)
                            //{
                            //    response.Status = HttpStatusCode.TooManyRequests.ToString();
                            //    response.Code = twoFaResponse.StatusCode.ToString();
                            //    //twoFaResponse.Next2FaInSeconds = next2Fa;
                            //    twoFaResponse.Next2FaInSeconds = this._twoFaHelper.Resend2FaIntervalSeconds;
                            //    twoFaResponse.VerifyAttemptsCount = await this._otpTracingService.GetVerificatioAttemptsCountAsync(twoFaResponse.Sid);
                            //    response.Data = twoFaResponse;
                            //    response.Message = string.Format(Constants.MAX_RESEND_MESSAGE, this._twoFaHelper.TwoFaRecycleMinutes);
                            //    return response;
                            //}

                            //twoFaAuth.VerificationSid = twoFaResponse.Sid;
                            //await this._otpTracingService.Create2FaLogAsync(twoFaAuth.PhoneNoMissing
                            //        ? null
                            //        : customerInfo.Contact
                            //            .ContactPhoneInfoes.First().Phone,
                            //    null, contact.Id, model.Email, "OTP Created.", twoFaResponse.Sid, tenant, twoFaResponse); 
                            #endregion

                        }

                        response.Status = ApiResponse.ApiResponseStatus.Success;

                        if (!twoFaResponse.CanSend2Fa && (!twoFaAuth.PhoneNoMissing))
                        {
                            var lastLog = await this._otpTracingService.GetLastSendAttemptAsync(
                                    customerInfo.Contact.ContactPhoneInfoes.First().Phone, null);
                            if (lastLog != null && !string.IsNullOrEmpty(lastLog.ResponseJson))
                            {
                                TwilioTwoFaResponseModel twilioResponseModel = JsonConvert.DeserializeObject<TwilioTwoFaResponseModel>(lastLog.ResponseJson);
                                if (!string.IsNullOrEmpty(twilioResponseModel.Sid))
                                {
                                    if (string.IsNullOrEmpty(twoFaResponse.Sid))
                                    {
                                        twoFaResponse.Sid = twilioResponseModel.Sid;
                                    }
                                    twoFaResponse.VerifyAttemptsCount = await this._otpTracingService.GetVerificationAttemptsCountAsync( twilioResponseModel.Sid);
                                    if (lastLog.Message == "approved" && twilioResponseModel.SendCodeAttempts == null)
                                    {
                                        twilioResponseModel.SendCodeAttempts = new List<SendCodeAttempt>();
                                    }
                                    twoFaResponse.SendCodeAttempts = twilioResponseModel.SendCodeAttempts;

                                    twoFaResponse.DateCreated = twilioResponseModel.DateCreated;
                                    if (twoFaResponse.VerifyAttemptsCount >= 5)
                                    {
                                        response.Message = string.Format(Constants.MAX_VERIFICATION_MESSAGE, this._twoFaHelper.TwoFaRecycleMinutes);
                                    }
                                    else
                                    {
                                        if (twilioResponseModel.SendCodeAttempts != null && twilioResponseModel.SendCodeAttempts.Count >= 5)
                                        {
                                            response.Message = string.Format(Constants.MAX_RESEND_MESSAGE, this._twoFaHelper.TwoFaRecycleMinutes);
                                        } 
                                    }
                                }
                            }
                        }
                        else
                        {
                            if(!twoFaAuth.PhoneNoMissing)
                            {
                                string message = "OTP Created.";
                                if (twoFaResponse.SendCodeAttempts != null && twoFaResponse.SendCodeAttempts.Count > 0)
                                {
                                    if (twoFaResponse.SendCodeAttempts.Count >= 5)
                                    {
                                        message = "max_attempts_reached";
                                    }
                                    else
                                    {
                                        if (twoFaResponse.SendCodeAttempts.Count > 1)
                                        {
                                            message = "OTP Resend.";
                                        }
                                    }
                                }

                                await this._otpTracingService.Create2FaLogAsync(
                                    customerInfo.Contact.ContactPhoneInfoes.First().Phone, null, customerInfo.ContactId,
                                    userProfile.UserName, message, twoFaResponse.Sid, tenant, twoFaResponse);
                            }
                            
                        }
                        var tokenData = new TokenData
                        {
                            ValidTo = twoFaJwtToken.ValidTo,
                            ValidFrom = twoFaJwtToken.ValidFrom,
                            RefreshToken = Guid.NewGuid().ToString(),
                            RefreshTokenValidTo = DateTime.UtcNow.AddMinutes(5),
                            UserProfileId = userProfile.Id,
                            UserName = userProfile.UserName,
                            Token = twoFaToken
                        };

                        await _tokenManager.AddAuthTokenToWhiteListAsync(tokenData: tokenData);
                        await _tokenManager.AddRefreshTokenTokenAsync(tokenData: tokenData);
                        await _tokenManager.CleanUpAuthTokenWhiteListAsync(tokenData: tokenData);

                        response.Data = new
                        {
                            Tenant2FaStatus = tenant2FaConfig.BorrowerTwoFaModeId,
                            UserPreference = userProfile.TwoFaEnabled,
                            IsLoggedIn = false,
                            PhoneNoMissing = customerInfo.Contact.ContactPhoneInfoes.Count == 0,
                            RequiresTwoFa = true,
                            PhoneNo = customerInfo.Contact.ContactPhoneInfoes.Count == 0 ? null : customerInfo.Contact.ContactPhoneInfoes.First().Phone,
                            //twoFaAuth.VerificationSid,
                            VerificationSid = twoFaResponse?.Sid,
                            Token = twoFaToken,
                            UserProfileId = userProfile.Id,
                            userProfile.UserName,
                            twoFaJwtToken.ValidFrom,
                            twoFaJwtToken.ValidTo,
                            verify_attempts_count = twoFaResponse.VerifyAttemptsCount,
                            //verificationSid = twoFaResponse.Sid,
                            //twoFaResponse.SendCodeAttempts,
                            //otp_valid_till = twoFaResponse?.DateCreated.AddMinutes(this._twoFaHelper.Read2FaConfig<int>("TwoFaRecycleMinutes", 10)),
                            otp_valid_till = twoFaResponse?.DateCreated.AddMinutes(this._twoFaHelper.TwoFaRecycleMinutes),
                            next2FaInSeconds = this._twoFaHelper.Resend2FaIntervalSeconds,
                            TwoFaRecycleMinutes = this._twoFaHelper.TwoFaRecycleMinutes,
                            SendAttemptsCount = twoFaAuth.PhoneNoMissing ? new List<SendCodeAttempt>() : twoFaResponse?.SendCodeAttempts,
                            CookiePath = $"/{tenant.Branches[0].Code}/"
                        };
                        return response;
                    }
                }

            }

            return await this.GenerateNewAccessToken(userProfile.Id, tenant.Id, tenant.Branches[0].Code);
        }
        public async Task<ApiResponse> ChangePassword(int userId, string oldPassword, string newPassword, int tenantId)
        {
            var user = await Uow.Repository<User>().Query(x => x.Id == userId && x.TenantId == tenantId && x.IsActive && x.UserTypeId == (byte)TenantConfig.Common.UserType.Customer).FirstOrDefaultAsync();
            if (user != null)
            {
                var oldPwd = oldPassword.ApplyPasswordEncryptionWithSalt((PasswordEncryptionFormat)user.PasswordFormatId, user.PasswordSalt);

                var newPwd = newPassword.ApplyPasswordEncryption(PasswordEncryptionFormat.ShaHashed, out string key);

                if (oldPwd == user.Password && oldPassword != newPassword)
                {
                    user.Password = newPwd;
                    user.PasswordFormatId = (int)PasswordEncryptionFormat.ShaHashed;
                    user.PasswordSalt = key;
                    user.UserResetPasswordLogs.Add(new UserResetPasswordLog
                    {
                        ChangeTypeId = (int)PasswordChangeType.ChangePassword,
                        CreatedBy = userId,
                        CreatedOnUtc = DateTime.UtcNow,
                        TenantId = tenantId,
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                        UserId = userId
                    });
                    Uow.Repository<User>().Update(user);
                    await Uow.SaveChangesAsync();
                    return new ApiResponse { Code = "200", Message = "" };
                }
                else
                {
                    return new ApiResponse { Code = "400", Message = "User doesn't exist" };
                }
            }
            else
            {
                return new ApiResponse { Code = "400", Message = "User doesn't exist" };
            }
        }



        public async Task<ApiResponse> ForgotPasswordRequest(ForgotPasswordRequestModel model, TenantModel tenant)
        {
            User user = await Uow.Repository<User>().Query(x => x.UserName.ToLower() == model.Email.ToLower() && x.TenantId == tenant.Id && x.UserTypeId == (byte)TenantConfig.Common.UserType.Customer && x.IsActive).FirstOrDefaultAsync();
            if (user == null)
            {
                return new ApiResponse { Code = "400", Message = "User does not exist" };
            }
            Guid guid = Guid.NewGuid();
            PasswordPolicy passwordPolicy = await Uow.Repository<PasswordPolicy>().Query(x => x.TenantId == tenant.Id).FirstAsync();
            user.UserResetPasswordKeys.Add(new UserResetPasswordKey
            {
                CreatedBy = user.Id,
                CreatedOnUtc = DateTime.UtcNow,
                ExpireOnUtc = DateTime.UtcNow.AddMinutes(passwordPolicy.ForgotTokenExpiryInMinutes),
                IsActive = true,
                PasswordKey = guid.ToString(),
                TenantId = tenant.Id,
                UserId = user.Id,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added
            });
            Repository.Update(user);
            await Uow.SaveChangesAsync();
            await SendForgotPasswordEmail(user,guid.ToString(),tenant);
            return new ApiResponse { Code = "200" };
        }

        private async Task SendForgotPasswordEmail(User user, string key, TenantModel tenant)
        {
            var branch = await _uowTenantConfig.Repository<Branch>().Query(x => x.Id == tenant.Branches[0].Id)
                .Include(x=>x.AddressInfo)
                .Include(x=>x.BranchEmailBinders).ThenInclude(x=>x.EmailAccount)
                .Include(x=>x.BranchPhoneBinders).ThenInclude(x=>x.CompanyPhoneInfo)
                .FirstAsync();
            WorkQueue queue = new WorkQueue();
            queue.ActivityId = (int)TenantConfig.Common.Activity.ForgotPassword;
            queue.BranchId = tenant.Branches[0].Id;
            queue.CreatedDate = DateTime.UtcNow;
            queue.IsActive = true;
            queue.Priority = 1;
            queue.Retries = 3;
            queue.ScheduleDate = DateTime.UtcNow;
            queue.Subject = "Password reset request";
            queue.TenantId = tenant.Id;
            queue.To = user.UserName;
            queue.MessageId = Guid.NewGuid().ToString();
            queue.TrackingState = TrackableEntities.Common.Core.TrackingState.Added;
            queue.WorkQueueTokens.Add(new WorkQueueToken
            {
                Key = EmailTokens.PRIMARY_COLOR,
                Value = branch.PrimaryColor,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added
            });
            queue.WorkQueueTokens.Add(new WorkQueueToken { 
                Key=EmailTokens.FROM_EMAIL,
                Value=branch.BranchEmailBinders.First(y => y.TypeId == (int)EmailType.AutoReply && y.EmailAccount.IsActive == true).EmailAccount.Email,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added
            });
            queue.WorkQueueTokens.Add(new WorkQueueToken
            {
                Key = EmailTokens.FROM_EMAIL_DISPLAY,
                Value = branch.Name,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added
            });
            queue.WorkQueueTokens.Add(new WorkQueueToken
            {
                Key = EmailTokens.FORGOT_PASSWORD_LINK,
                Value = $"https://{tenant.Urls[0].Url}/{tenant.Branches[0].Code}/" + (tenant.Branches[0].LoanOfficers.Count <= 0 ? string.Empty : $"{tenant.Branches[0].LoanOfficers[0].Code}/") + $"{Constants.SPA_PATH}/ResetPassword?key={Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{user.Id}|{key}"))}",
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added
            });
            queue.WorkQueueTokens.Add(new WorkQueueToken
            {
                Key = EmailTokens.BRANCH_LOGO_URL,
                Value = CommonHelper.GenerateCdnUrl(tenant,branch.Logo),
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added
            });
            queue.WorkQueueTokens.Add(new WorkQueueToken
            {
                Key = EmailTokens.FORGOT_YOUR_PASSWORD,
                Value = CommonHelper.GenerateGlobalCdnUrl(tenant, "forget-Your-Password.png"),
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added
            });
            queue.WorkQueueTokens.Add(new WorkQueueToken
            {
                Key = EmailTokens.BRANCH_URL,
                Value = $"https://{tenant.Urls[0].Url}/{tenant.Branches[0].Code}/{Constants.SPA_PATH}/",
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added
            });
            queue.WorkQueueTokens.Add(new WorkQueueToken
            {
                Key = EmailTokens.BRANCH_NAME,
                Value = branch.Name,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added
            });
            queue.WorkQueueTokens.Add(new WorkQueueToken
            {
                Key = EmailTokens.PRIMARY_EMAIL_ADDRESS,
                Value = user.UserName,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added
            });
            _uowTenantConfig.Repository<WorkQueue>().Insert(queue);
            await _uowTenantConfig.SaveChangesAsync();
        }

        public async Task<ApiResponse> IsPasswordLinkExpired(int userId, string key, TenantModel tenant)
        {
            var userKey = await Uow.Repository<UserResetPasswordKey>().Query(x => x.IsActive && x.ExpireOnUtc >= DateTime.UtcNow && x.PasswordKey == key
                    && x.TenantId == tenant.Id && x.UserId == userId).FirstOrDefaultAsync();
            if (userKey == null)
            {
                return new ApiResponse { Code = "200", Message = "The link has been expired", Data=true };
            }
            return new ApiResponse { Code = "200", Message = "The link is valid", Data = false };
        }
        public async Task<ApiResponse> ForgotPasswordResponse(ForgotPasswordResponseModel model, TenantModel tenant)
        {
            var userKey = await Uow.Repository<UserResetPasswordKey>().Query(x => x.IsActive && x.ExpireOnUtc >= DateTime.UtcNow && x.PasswordKey == model.Key
                    && x.TenantId == tenant.Id && x.UserId == model.UserId).FirstOrDefaultAsync();
            if (userKey == null)
            {
                return new ApiResponse { Code = "400", Message = "The link has been expired" };
            }
            var user = await Uow.Repository<User>().Query(x => x.Id == model.UserId && x.TenantId == tenant.Id && x.IsActive && x.UserTypeId == (byte)TenantConfig.Common.UserType.Customer).FirstOrDefaultAsync();
            if (user == null)
            {
                return new ApiResponse { Code = "400", Message = "User does not exist" };
            }

            userKey.ResetOnUtc = DateTime.UtcNow;
            userKey.IsActive = false;
            Uow.Repository<UserResetPasswordKey>().Update(userKey);

            var newPwd = model.Password.ApplyPasswordEncryption(PasswordEncryptionFormat.ShaHashed, out string key);
            user.Password = newPwd;
            user.PasswordFormatId = (int)PasswordEncryptionFormat.ShaHashed;
            user.PasswordSalt = key;
            user.UserResetPasswordLogs.Add(new UserResetPasswordLog
            {
                ChangeTypeId = (int)PasswordChangeType.ForgotPassword,
                CreatedBy = user.Id,
                CreatedOnUtc = DateTime.UtcNow,
                TenantId = tenant.Id,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                UserId = user.Id
            });
            Uow.Repository<User>().Update(user);
            await Uow.SaveChangesAsync();
            return new ApiResponse { Code = "200" };
        }

        public async Task<User> GetUserById(int userId, int tenantId)
        {
            return await Uow.Repository<User>().Query(x => x.IsActive == true && x.Id == userId && x.TenantId == tenantId).FirstOrDefaultAsync();
        }

        public async Task<ApiResponse> GenerateNewAccessToken(int userId, int tenantId, string branchCode)
        {
            ApiResponse response = new ApiResponse();

            var customerInfo = await this._customerService.GetCustomerByUserIdAsync(userId, tenantId, new List<CustomerRelatedEntities>()
            {
                CustomerRelatedEntities.Contact,
                CustomerRelatedEntities.Tenant
            });

            var userProfile = await Uow.Repository<User>().Query(x => x.Id == userId && x.IsActive && x.TenantId==tenantId).FirstOrDefaultAsync();


            var usersClaims = new List<Claim>
            {
                new Claim(type: ClaimTypes.Role,
                    value: "Customer"),
                new Claim(type: "UserId",
                    value: customerInfo.UserId.ToString()),
                new Claim(type: ClaimTypes.Name,
                    value: userProfile.UserName.ToLower()),
                new Claim(type: "FirstName",
                    value: customerInfo.Contact.FirstName),
                new Claim(type: "LastName",
                    value: customerInfo.Contact.LastName),
                new Claim(type: "TenantCode",
                    value: customerInfo.Tenant.Code.ToLower()),
                new Claim(type: "BranchCode", branchCode)
            };

            var jwtToken = await _tokenService.GenerateAccessToken(claims: usersClaims);
            var refreshToken = _tokenService.GenerateRefreshToken();
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token: jwtToken);
            var tokenData = new TokenData
            {
                ValidTo = jwtToken.ValidTo,
                ValidFrom = jwtToken.ValidFrom,
                RefreshToken = refreshToken,
                RefreshTokenValidTo = DateTime.UtcNow.AddMinutes(value :36000000),
                UserProfileId = userProfile.Id,
                UserName = userProfile.UserName,
                Token = tokenString
            };

            await _tokenManager.AddAuthTokenToWhiteListAsync(tokenData: tokenData);
            await _tokenManager.AddRefreshTokenTokenAsync(tokenData: tokenData);
            await _tokenManager.CleanUpAuthTokenWhiteListAsync(tokenData: tokenData);

            response.Code = Convert.ToString((int) HttpStatusCode.OK);
            response.Status = ApiResponse.ApiResponseStatus.Success;
            response.Data = new
            {
                IsLoggedIn = true,
                Token = tokenString,
                refreshToken,
                RefreshTokenValidTo = tokenData.RefreshTokenValidTo,
                jwtToken.ValidFrom,
                jwtToken.ValidTo,
                UserProfileId = userProfile.Id,
                UserName = userProfile.UserName,
                cookiePath = $"/{branchCode}/"
            };

            return response;
        }

        public async Task<ApiResponse> Set2FaForUserAsync(int tenantId, int userId, bool userVerified2Fa)
        {
            var tenantConfig = await this._tenantConfigService.GetTenant2FaConfigAsync(tenantId, new List<TwoFaConfigEntities>());
            TwoFaStatus? tenant2FaStatus = (TwoFaStatus)tenantConfig.BorrowerTwoFaModeId;
            var userInfo = await _previousUow.Repository<User>()
                .Query(x => x.IsActive == true && x.TenantId == tenantId && x.Id == userId)
                .FirstOrDefaultAsync();
            if (userInfo == null)
            {
                return new ApiResponse()
                {
                    Code = Convert.ToString((int)HttpStatusCode.NotFound),
                    Message = "User not found.",
                    Status = Convert.ToString(HttpStatusCode.NotFound),
                };
            }

            userInfo.TwoFaEnabled = tenant2FaStatus == TwoFaStatus.RequiredForAll ||
                                    (tenant2FaStatus == TwoFaStatus.UserPreference && userVerified2Fa);
            this._previousUow.Repository<User>().Update(userInfo);
            await this._previousUow.SaveChangesAsync();

            return new ApiResponse()
            {
                Code = Convert.ToString((int)HttpStatusCode.OK),
                Message = "Successfully set user two fa preference.",
                Status = Convert.ToString(HttpStatusCode.OK),
            };
        }

        private async Task<bool> Requires2Fa(int tenantId, int userId)
        {
            bool twoFaRequired = false;
            var tenantConfig = await this._tenantConfigService.GetTenant2FaConfigAsync(tenantId, new List<TwoFaConfigEntities>()
            {
                TwoFaConfigEntities.Tenant
            });
            if (tenantConfig == null)
            {
                throw new Exception("Tenant configuration not found");
            }

            if (string.IsNullOrEmpty(tenantConfig.TwilioVerifyServiceId))
            {
                this._logger.LogWarning("No service id mapped with tenant for 2FA.");
            }
            else
            {
                TwoFaStatus? twoFaStatus = (TwoFaStatus)tenantConfig.BorrowerTwoFaModeId;

                var userInfo = await Uow.Repository<User>().Query(u => u.IsActive == true && u.Id == userId && u.TenantId==tenantId)
                    .FirstOrDefaultAsync();
                if (twoFaStatus == TwoFaStatus.RequiredForAll)
                {
                    twoFaRequired = true;
                }
                else
                {
                    twoFaRequired = (twoFaStatus == TwoFaStatus.UserPreference) && (userInfo != null && (userInfo.TwoFaEnabled == null || userInfo.TwoFaEnabled == true));
                }
            }
            return twoFaRequired;
        }
    }
}
