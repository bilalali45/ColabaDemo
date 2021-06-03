using Extensions.ExtensionClasses;
using Identity.Data;
using Identity.Entity.Models;
using Identity.Model;
using Identity.Model.Mobile;
using Identity.Service.Helpers.Interfaces;
using Identity.Service.Mobile.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
//using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TenantConfig.Common;
using TenantConfig.Common.DistributedCache;
using TenantConfig.Data;
using TenantConfig.Entity.Models;
using TokenCacheHelper.Models;
using TokenCacheHelper.TokenManager;
using TrackableEntities.Common.Core;
using URF.Core.Abstractions;

namespace Identity.Service.Mobile
{
    public class McuAccountService : ServiceBase<IdentityContext, User>, IMcuAccountService
    {
        private const string DEFAULT_CHANNEL = "sms";

        //private readonly IUnitOfWork<IdentityContext> _identityUow;
        private readonly IUnitOfWork<TenantConfigContext> _uowTenantConfig;
        private readonly ILogger<CustomerAccountService> _logger;
        private readonly ITokenService _tokenService;
        private readonly ITokenManager _tokenManager;
        private readonly IConfiguration _configuration;
        private readonly ITwoFaHelperV2 _twoFaHelperV2;
        private readonly IRestClient _twilioClient;
        private readonly IKeyStoreService _keyStoreService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public McuAccountService(IUnitOfWork<IdentityContext> identityUow, IServiceProvider services, IUnitOfWork<TenantConfigContext> uowTenantConfig,
                                 ILogger<CustomerAccountService> logger, ITokenService tokenService, ITokenManager tokenManager, IConfiguration configuration, ITwoFaHelperV2 twoFaHelperV2, IRestClient client, IKeyStoreService keyStoreService, IHttpContextAccessor httpContextAccessor) : base(identityUow, services)
        {
            //_previousUow = identityUow;
            _uowTenantConfig = uowTenantConfig;
            _logger = logger;
            this._logger = logger;
            _tokenService = tokenService;
            _tokenManager = tokenManager;
            _configuration = configuration;
            _twoFaHelperV2 = twoFaHelperV2;
            _keyStoreService = keyStoreService;
            _httpContextAccessor = httpContextAccessor;
            _twilioClient = client;
            _twilioClient.BaseUrl = new Uri(_twoFaHelperV2.TwilioEndPoint);
            var twilioAccountSid = keyStoreService.GetTwilioAccountSidAsync().Result;
            var twilioAuthToken = keyStoreService.GetTwilioAuthTokenAsync().Result;
            _twilioClient.Authenticator = new HttpBasicAuthenticator(twilioAccountSid, twilioAuthToken);
        }

        public async Task<ApiResponse> Signin(MobileSigninModel model)
        {
            var userProfile = await Uow.Repository<User>().Query(x => x.UserName.ToLower() == model.Email.ToLower() && x.UserTypeId == (byte)TenantConfig.Common.UserType.Employee && x.IsActive).FirstOrDefaultAsync();
            if (userProfile == null)
            {
                return new ApiResponse { Code = "400", Message = "User does not exist" };
            }
            var tempPwd = model.Password.ApplyPasswordEncryptionWithSalt((PasswordEncryptionFormat)userProfile.PasswordFormatId, userProfile.PasswordSalt);
            PasswordPolicy passwordPolicy = await Uow.Repository<PasswordPolicy>().Query().FirstAsync();
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
            var contact = await _uowTenantConfig.Repository<Employee>().Query(x => x.IsActive && x.UserId == userProfile.Id).Include(x => x.Contact)
                                                .Select(x => x.Contact).FirstOrDefaultAsync();

            if (contact == null)
            {
                return new ApiResponse { Code = "400", Message = "Contact does not exist" };
            }

            // TODO: 2FA code here

            ApiResponse response = new ApiResponse { Code = "200" };
            var employeeInfo = await GetEmployeeByUserIdAsync(userProfile.Id,
                                                                                    new List<EmployeeRelatedEntities>()
                                                                                    {
                                                                                        EmployeeRelatedEntities.ContactPhoneInfo
                                                                                    });
            return await this.GenerateToken(userProfile.Id, employeeInfo.TenantId);
        }

        public async Task<string> GetVerifiedMobileNumber(int userId, TenantModel tenant)
        {
            string mobileNumber = null;

            var mcuInfo = await _uowTenantConfig.Repository<Employee>()
                .Query(x => x.UserId == userId && x.IsActive && x.TenantId == tenant.Id)
                .Include(x => x.Contact).ThenInclude(c => c.ContactPhoneInfoes)
                .FirstOrDefaultAsync();

            if (mcuInfo.Contact != null)
            {
                if(mcuInfo.Contact.ContactPhoneInfoes != null && mcuInfo.Contact.ContactPhoneInfoes.Count > 0)
                {
                    var verifiedNumber =
                        mcuInfo.Contact.ContactPhoneInfoes.FirstOrDefault(phone => phone.IsDeleted == false && phone.IsValid == true);
                    if (verifiedNumber != null)
                    {
                        mobileNumber = verifiedNumber.Phone;
                    }
                }
            }

            return mobileNumber;
        }

        private async Task<ApiResponse> GenerateToken(int userId, int? tenantId)
        {
            ApiResponse response = new ApiResponse();
            var tenantInfo = this._uowTenantConfig.Repository<Tenant>()
                .Query(x => x.Id == tenantId && x.IsActive)
                .FirstOrDefault();
            if (tenantInfo == null)
            {
                this._logger.LogWarning($"Tenant detail not found. Tenant Id : {tenantId}");
                response.Code = Convert.ToInt32(HttpStatusCode.BadRequest).ToString();
                response.Status = HttpStatusCode.BadRequest.ToString();
                response.Message = "Tenant detail not found.";
                return response;
            }

            var userInfo = await Uow.Repository<User>()
                .Query(u => u.Id == userId && u.TenantId == tenantInfo.Id)
                .FirstOrDefaultAsync();

            var mcuInfo = await _uowTenantConfig.Repository<Employee>()
                .Query(emp => emp.UserId == userId && emp.TenantId == tenantId)
                .FirstOrDefaultAsync();

            var tenantTwoFaConfig = await _uowTenantConfig.Repository<TwoFaConfig>()
                .Query(config => config.TenantId == tenantId)
                .Include(config => config.Tenant)
                .FirstOrDefaultAsync();

            var twoFaSetting = (TwoFaStatus) tenantTwoFaConfig.McuTwoFaMobileModeId;

            if (twoFaSetting == TwoFaStatus.InactiveForAll)
            {
                response = await this.GenerateNewAccessToken(userId, tenantId);
                return response;
            }

            if (!this._twoFaHelperV2.TwoFaCookieExists(tenantInfo.Code, userId))
            {
                if (twoFaSetting == TwoFaStatus.UserPreference && userInfo.TwoFaEnabled == false)
                {
                    response = await this.GenerateNewAccessToken(userId, tenantId);
                    return response;
                }

                List<Claim> twoFaClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Role, "MCU"),
                    //new Claim(type: "UserProfileId", value: userInfo.Id.ToString()),
                    new Claim(type: "IntermediateUserId", value: userInfo.Id.ToString()),
                    new Claim(type: "ContactId", mcuInfo.ContactId.ToString()),
                    new Claim(type: "Email", userInfo.UserName),
                    new Claim(type: "TenantCode", tenantInfo.Code.ToLower())
                };
                var twoFaJwtToken = await this._tokenService.Generate2FaTokenAsync(twoFaClaims);
                var twoFaToken = new JwtSecurityTokenHandler().WriteToken(token: twoFaJwtToken);

                //twoFaToken = await this._tokenService.Generate2FaTokenAsync(twoFaClaims);

                response.Code = Convert.ToString((int)HttpStatusCode.OK);
                response.Status = HttpStatusCode.OK.ToString();
                var tokenData = new TokenData()
                {
                    TokenType = TokenType.IntermediateToken,
                    UserName = userInfo.UserName,
                    Token = twoFaToken,
                    UserProfileId = userInfo.Id,
                    ValidFrom = twoFaJwtToken.ValidFrom,
                    ValidTo = twoFaJwtToken.ValidTo,
                    RefreshToken = Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(1, 10),
                    RefreshTokenValidTo = twoFaJwtToken.ValidTo
                };
                await AddTokenToRedis(tokenData);
                response.Data = tokenData;
                return response;
            }


            return await this.GenerateNewAccessToken(userId, tenantInfo.Id);
        }

        public async Task<ApiResponse> GenerateNewAccessToken(int userId, int? tenantId)
        {
            ApiResponse response = new ApiResponse();

            var EmployeeInfo = await GetEmployeeByUserIdAsync(userId, tenantId, new List<EmployeeRelatedEntities>()
                                                                                                      {
                                                                                                          EmployeeRelatedEntities.Contact,
                                                                                                          EmployeeRelatedEntities.Tenant
                                                                                                      });

            var userProfile = await Uow.Repository<User>().Query(x => x.Id == userId && x.IsActive && x.TenantId == tenantId).FirstOrDefaultAsync();

            var userRoleBinder = Uow.Repository<UserRoleBinder>().Query(x => x.UserId == userProfile.Id)
                .Include(x => x.Role);


            var usersClaims = new List<Claim>
            {
                //new Claim(type: ClaimTypes.Role,
                //    value: "Customer"),
                new Claim(type: "UserId",
                    value: EmployeeInfo.UserId.ToString()),
                new Claim(type: ClaimTypes.Name,
                    value: userProfile.UserName.ToLower()),
                new Claim(type: "FirstName",
                    value: EmployeeInfo.Contact.FirstName),
                new Claim(type: "LastName",
                    value: EmployeeInfo.Contact.LastName),
                new Claim(type: "TenantCode",
                    value: EmployeeInfo.Tenant.Code.ToLower()),
                //new Claim(type: "BranchCode", customerInfo.Tenant.Branches.FirstOrDefault().Code) //TODO
            };
            if (userRoleBinder.Count() > 0)
            {
                foreach (var userRole in userRoleBinder)
                {
                    usersClaims.Add(new Claim(type: ClaimTypes.Role,
                                              value: userRole.Role.Name
                                              ));


                }
            }

            var jwtToken = await _tokenService.GenerateAccessToken(claims: usersClaims);
            var refreshToken = _tokenService.GenerateRefreshToken();
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token: jwtToken);
            var tokenData = new TokenData
            {
                ValidTo = jwtToken.ValidTo,
                ValidFrom = jwtToken.ValidFrom,
                RefreshToken = refreshToken,
                RefreshTokenValidTo = DateTime.UtcNow.AddMinutes(value: double.Parse(s: _configuration[key: "Token:RefreshTokenExpiryInMinutes"])),
                UserProfileId = userProfile.Id,
                UserName = userProfile.UserName,
                Token = tokenString,
                TokenType = TokenType.AccessToken
            };

            //await _tokenManager.AddAuthTokenToWhiteListAsync(tokenData: tokenData);
            //await _tokenManager.AddRefreshTokenTokenAsync(tokenData: tokenData);
            //await _tokenManager.CleanUpAuthTokenWhiteListAsync(tokenData: tokenData);
            await AddTokenToRedis(tokenData);

            response.Code = Convert.ToString((int)HttpStatusCode.OK);
            response.Status = ApiResponse.ApiResponseStatus.Success;
            //response.Data = new
            //{
            //    IsLoggedIn = true,
            //    Token = tokenString,
            //    refreshToken,
            //    RefreshTokenValidTo = tokenData.RefreshTokenValidTo,
            //    jwtToken.ValidFrom,
            //    jwtToken.ValidTo,
            //    UserProfileId = userProfile.Id,
            //    UserName = userProfile.UserName,
            //    //cookiePath = $"/{branchCode}/"
            //};
            response.Data = tokenData;

            return response;
            //var jwtToken = await _tokenService.GenerateAccessToken(claims: usersClaims);
            //var refreshToken = _tokenService.GenerateRefreshToken();
            //var tokenString = new JwtSecurityTokenHandler().WriteToken(token: jwtToken);
            //lock (TokenService.lockObject)
            //{
            //    if (!TokenService.RefreshTokens.ContainsKey(key: userProfile.UserName.ToLower())) TokenService.RefreshTokens[key: userProfile.UserName.ToLower()] = new List<TokenPair>();

            //    TokenService.RefreshTokens[key: userProfile.UserName.ToLower()].Add(item: new TokenPair
            //    {
            //        JwtToken = tokenString,
            //        RefreshToken = refreshToken,
            //        RefreshIssueDate = DateTime.UtcNow
            //    });
            //}

            //response.Code = Convert.ToString((int)HttpStatusCode.OK);
            //response.Status = ApiResponse.ApiResponseStatus.Success;
            //response.Data = new
            //{
            //    IsLoggedIn = true,
            //    Token = tokenString,
            //    refreshToken,
            //    jwtToken.ValidFrom,
            //    jwtToken.ValidTo,
            //    //cookiePath = $"/{customerInfo.Tenant.Branches.FirstOrDefault().Code}/" //TODO
            //};

            //return response;
        }

        public async Task<ApiResponse> ForgotPasswordRequest(ForgotPasswordRequestModel model)
        {
            User user = await Uow.Repository<User>().Query(x => x.UserName.ToLower() == model.Email.ToLower() && x.UserTypeId == (byte)TenantConfig.Common.UserType.Employee && x.IsActive).FirstOrDefaultAsync();
            if (user == null)
            {
                return new ApiResponse { Code = "400", Message = "User does not exist" };
            }
            Guid guid = Guid.NewGuid();
            PasswordPolicy passwordPolicy = await Uow.Repository<PasswordPolicy>().Query().FirstAsync();
            user.UserResetPasswordKeys.Add(new UserResetPasswordKey
            {
                CreatedBy = user.Id,
                CreatedOnUtc = DateTime.UtcNow,
                ExpireOnUtc = DateTime.UtcNow.AddMinutes(passwordPolicy.ForgotTokenExpiryInMinutes),
                IsActive = true,
                PasswordKey = guid.ToString(),
                TenantId = user.TenantId,
                UserId = user.Id,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added
            });


            //var tenant = await _uowTenantConfig.Repository<Tenant>().Query(x=>x.Id== user.TenantId).FirstOrDefaultAsync();
            //var branch = await _uowTenantConfig.Repository<Branch>().Query(x => x.IsCorporate == true && x.TenantId == user.TenantId).FirstOrDefaultAsync();
            //tenant.Branches.Add(branch); //added one branch

            //TenantModel tenantmodel = new TenantModel()
            //                          {
            //                              Id = tenant.Id,
            //                              Code = tenant.Code,
            //                              Urls = tenant.TenantUrls,
            //                              Branches = tenant.Branches

            //                          };
            var tenantInfo = await _uowTenantConfig.Repository<Tenant>().Query(t => t.Id == user.TenantId)
                                                   .Include(t => t.Branches)
                                                   .Include(b => b.TenantUrls)
                                                   .FirstOrDefaultAsync();

            TenantModel tenantModel = new TenantModel()
            {
                Id = tenantInfo.Id,
                Code = tenantInfo.Code,
                Urls = tenantInfo.TenantUrls.Select(url => new UrlModel()
                {
                    //Type = (TenantUrlType)url.TypeId,
                    Type = (TenantUrlType)url.TypeId,
                    Url = url.Url
                }).ToList(),
                Branches = tenantInfo.Branches.Select(b => new BranchModel()
                {
                    Code = b.Code,
                    Id = b.Id,
                    IsCorporate = b.IsCorporate
                }).Where(x => x.IsCorporate == true).ToList()
            };
            try
            {
                Repository.Update(user);
                await Uow.SaveChangesAsync();
                await SendForgotPasswordEmail(user, guid.ToString(), tenantModel);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

         
            return new ApiResponse { Code = "200" };
        }

        private async Task SendForgotPasswordEmail(User user, string key, TenantModel tenant)
        {
            var branch = await _uowTenantConfig.Repository<Branch>().Query(x => x.Id == tenant.Branches[0].Id)
                .Include(x => x.AddressInfo)
                .Include(x => x.BranchEmailBinders).ThenInclude(x => x.EmailAccount)
                .Include(x => x.BranchPhoneBinders).ThenInclude(x => x.CompanyPhoneInfo)
                .FirstAsync();
            WorkQueue queue = new WorkQueue();
            queue.ActivityId = (int)TenantConfig.Common.Activity.McuForgotPassword;
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
            queue.WorkQueueTokens.Add(new WorkQueueToken
            {
                Key = EmailTokens.FROM_EMAIL,
                Value = branch.BranchEmailBinders.First(y => y.TypeId == (int)EmailType.AutoReply && y.EmailAccount.IsActive == true).EmailAccount.Email,
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
                Value = $"https://{tenant.Urls[0].Url}/{tenant.Branches[0].Code}/" + ((tenant.Branches[0].LoanOfficers == null || tenant.Branches[0].LoanOfficers?.Count <= 0) ? string.Empty : $"{tenant.Branches[0].LoanOfficers?[0].Code}/") + $"{Constants.SPA_PATH}/ResetPassword?key={Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{user.Id}|{key}"))}",
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added
            });
            queue.WorkQueueTokens.Add(new WorkQueueToken
            {
                Key = EmailTokens.BRANCH_LOGO_URL,
                Value = CommonHelper.GenerateCdnUrl(tenant, branch.Logo),
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

        public async Task<ApiResponse> ForgotPasswordResponse(ForgotPasswordResponseModel model)
        {
            var userKey = await Uow.Repository<UserResetPasswordKey>().Query(x => x.IsActive && x.ExpireOnUtc >= DateTime.UtcNow && x.PasswordKey == model.Key
                                                                                  && x.UserId == model.UserId).FirstOrDefaultAsync();
            if (userKey == null)
            {
                return new ApiResponse { Code = "400", Message = "The link has been expired" };
            }
            var user = await Uow.Repository<User>().Query(x => x.Id == model.UserId && x.IsActive && x.UserTypeId == (byte)TenantConfig.Common.UserType.Employee).FirstOrDefaultAsync();
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
                TenantId = user.TenantId,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                UserId = user.Id
            });
            Uow.Repository<User>().Update(user);
            await Uow.SaveChangesAsync();
            return new ApiResponse { Code = "200" };
        }

        public async Task<ApiResponse> IsPasswordLinkExpired(int userId, string key)
        {
            var userKey = await Uow.Repository<UserResetPasswordKey>().Query(x => x.IsActive && x.ExpireOnUtc >= DateTime.UtcNow && x.PasswordKey == key
                                                                                 && x.UserId == userId).FirstOrDefaultAsync();
            if (userKey == null)
            {
                return new ApiResponse { Code = "200", Message = "The link has been expired", Data = true };
            }
            return new ApiResponse { Code = "200", Message = "The link is valid", Data = false };
        }

        public async Task<Employee> GetEmployeeByUserIdAsync(long userId, int? tenandId, List<EmployeeRelatedEntities> includes = null)
        {
            var query = _uowTenantConfig.Repository<Employee>()
                .Query(x => x.TenantId == tenandId && x.UserId == userId && x.IsActive);

            if (includes != null)
            {
                query = base.ProcessIncludes<Employee, EmployeeRelatedEntities>(query, includes);
            }
            var results = await query.FirstOrDefaultAsync();
            return results;
        }

        public async Task<Employee> GetEmployeeByUserIdAsync(long userId, List<EmployeeRelatedEntities> includes = null)
        {
            var query = _uowTenantConfig.Repository<Employee>()
                .Query(x => x.UserId == userId && x.IsActive);

            if (includes != null)
            {
                query = base.ProcessIncludes<Employee, EmployeeRelatedEntities>(query, includes);
            }
            var results = await query.FirstOrDefaultAsync();
            return results;
        }

        public async Task<ApiResponse> SendTwoFaToNumber(string phoneNumber, int userId, TenantModel tenant, string ipAddress)
        {
            var isValid = _twoFaHelperV2.ValidatePhoneNumber(phoneNumber);
            if (isValid != null)
            {
                return isValid;
            }

            var verifiedNumber = await GetVerifiedMobileNumber(userId, tenant);
            // Mobile Number Other Than Mapped?
            if (!string.IsNullOrEmpty(verifiedNumber)) // If not missing
            {
                if (phoneNumber != verifiedNumber) // Yes, Mobile Number Other Than Mapped
                {
                    return new ApiResponse()
                    {
                        Data = null,
                        Message = "Phone number does not match verified number.",
                        Code = Convert.ToString((int) HttpStatusCode.BadRequest),
                        Status = Convert.ToString(HttpStatusCode.BadRequest)
                    };
                }
            }

            // No, Mapped mobile number is missing OR received mapped mobile number in request
            var timeoutDetail = await HasCompletedTimeout(phoneNumber); // Check if user has completed timeout
            if (!timeoutDetail.HasCompleted)
            {
                return new ApiResponse()
                {
                    Message = "You need to complete your timeout.",
                    Code = Convert.ToString((int) HttpStatusCode.BadRequest),
                    Data = timeoutDetail,
                    Status = Convert.ToString(HttpStatusCode.BadRequest)
                };
            }

            var twoFaConfig = await GetTenantTwoFaConfig(tenant); // Get twilio Service SID
            // Twilio SID mapped with tenant found?
            if (string.IsNullOrEmpty(twoFaConfig.TwilioVerifyServiceId)) // No, Twilio SID is not mapped with tenant
            {
                return new ApiResponse()
                {
                    Message = "Unable to send OTP. Please try again later.",
                    Code = Convert.ToString((int)HttpStatusCode.NotFound),
                    Data = null,
                    Status = Convert.ToString(HttpStatusCode.NotFound)
                };
            }

            // Yes, Twilio SID is mapped with tenant
            var response = await SendTwoFaRequest(phoneNumber, twoFaConfig.TwilioVerifyServiceId);
            await LogSendTwoFaResponse(response, userId, ipAddress, tenantId:tenant.Id); // Save Twilio Response To Database

            var sendTwoFaResponse = JsonConvert.DeserializeObject<SendTwoFaResponse>(response.Content);

            var apiResponse = new ApiResponse();

            // Check if success
            if (response.StatusCode == HttpStatusCode.Created) // Yes, OTP sending was successful
            {
                apiResponse.Message = sendTwoFaResponse.send_code_attempts.Count == 1 ? "Otp Sent" : "Otp Resend";
                apiResponse.Code = Convert.ToString((int)HttpStatusCode.OK);
                apiResponse.Data = new
                {
                    AttemptsCount = sendTwoFaResponse.send_code_attempts.Count,
                    PhoneNumber = phoneNumber
                    //, VerificationSid = sendTwoFaResponse.sid // This sid is required to verify otp from twilio
                };
                apiResponse.Status = Convert.ToString(HttpStatusCode.OK);
            }
            else // No, OTP sending was not successful
            {
                // Check if max attempts reached
                if (response.StatusCode == HttpStatusCode.TooManyRequests) // Yes, Max Attempts Reached
                {
                    apiResponse.Message = "Max attempt(s) reached";
                    apiResponse.Code = Convert.ToString((int)HttpStatusCode.TooManyRequests);
                    apiResponse.Data = new
                    {
                        AttemptsCount = sendTwoFaResponse.send_code_attempts.Count,
                        PhoneNumber = phoneNumber
                    };
                    apiResponse.Status = Convert.ToString(HttpStatusCode.TooManyRequests);
                }
                else // General Error To Client
                {
                    apiResponse.Message = "Unknown error in sending two fa request.";
                    apiResponse.Code = Convert.ToString((int)HttpStatusCode.InternalServerError);
                    apiResponse.Data = null;
                    apiResponse.Status = Convert.ToString(HttpStatusCode.InternalServerError);
                }
            }

            return apiResponse;
        }

        public async Task<ApiResponse> VerifyTwoFa(VerifyTwoFaModel model, int userId, TenantModel tenant, string ipAddress)
        {
            var isValid = _twoFaHelperV2.ValidatePhoneNumber(model.PhoneNumber);
            if (isValid != null)
            {
                return isValid;
            }

            var twoFaConfig = await GetTenantTwoFaConfig(tenant); // Get twilio Service SID
            // Twilio SID mapped with tenant found?
            if (string.IsNullOrEmpty(twoFaConfig.TwilioVerifyServiceId)) // No, Twilio SID is not mapped with tenant
            {
                return new ApiResponse()
                {
                    Message = "Unable to verify OTP. Please try again later.",
                    Code = Convert.ToString((int)HttpStatusCode.NotFound),
                    Data = null,
                    Status = Convert.ToString(HttpStatusCode.NotFound)
                };
            }

            // Verify Otp from twilio
            var verifyTwoFaResponse = await VerifyTwoFaRequest(model, twoFaConfig.TwilioVerifyServiceId);
            await LogVerifyTwoFaResponse(model, verifyTwoFaResponse, userId, ipAddress, tenant.Id); // Log Otp Verify Attempt
            // If Otp request exists
            if (verifyTwoFaResponse.StatusCode == HttpStatusCode.NotFound) // No, Otp request does not exist
            {
                return new ApiResponse()
                {
                    Message = "Otp request not found.",
                    Code = Convert.ToString((int) HttpStatusCode.NotFound),
                    Data = null,
                    Status = Convert.ToString(HttpStatusCode.NotFound)
                };
            }
            if (verifyTwoFaResponse.StatusCode == HttpStatusCode.OK) // Yes, otp request was found
            {
                var obj = JsonConvert.DeserializeObject<VerifyTwoFaResponse>(verifyTwoFaResponse.Content);
                // Check if otp verified
                if (obj.status == "approved") // Yes, Otp verified
                {
                    return await GenerateNewAccessToken(userId, tenant.Id); // Generate access token
                }

                return new ApiResponse() // No, Otp is invalid
                {
                    Message = "Invalid otp code.",
                    Code = Convert.ToString((int)HttpStatusCode.BadRequest),
                    Data = null,
                    Status = Convert.ToString(HttpStatusCode.BadRequest)
                };
            }
            return new ApiResponse()
            {
                Message = "Unable to verify otp request.",
                Code = Convert.ToString((int)HttpStatusCode.InternalServerError),
                Data = null,
                Status = Convert.ToString(HttpStatusCode.InternalServerError)
            };
        }

        public async Task<ApiResponse> GetTwoFaValuesToSkip(int userId, TenantModel tenant)
        {
            var tenantTwoFaConfig = await GetTenantTwoFaConfig(tenant);

            var userInfo = await Uow.Repository<User>()
                .Query(x => x.Id == userId && x.TenantId == tenant.Id && x.IsActive)
                .SingleAsync();

            var twoFaEnabledByUser = userInfo.TwoFaEnabled;

            return new ApiResponse()
            {
                Code = Convert.ToString((int)HttpStatusCode.OK),
                Data = new
                {
                    TenantTwoFaSetting = tenantTwoFaConfig.McuTwoFaMobileModeId,
                    UserTwoFaSetting = twoFaEnabledByUser
                },
                Status = Convert.ToString(HttpStatusCode.OK)
            };

            //if (twoFaSetting == TwoFaStatus.RequiredForAll)
            //{
            //    return new ApiResponse()
            //    {
            //        Code = Convert.ToString((int) HttpStatusCode.OK),
            //        Message = "User cannot skip two fa step.",
            //        Data =
            //        {
            //            CanSkipTwoFa = false
            //        },
            //        Status = Convert.ToString(HttpStatusCode.OK)
            //    };
            //}

            
            //if (userInfo.TwoFaEnabled == null)
            //{
            //    return new ApiResponse()
            //    {
            //        Code = Convert.ToString((int)HttpStatusCode.OK),
            //        Message = "User can skip two fa step.",
            //        Data =
            //        {
            //            CanSkipTwoFa = true
            //        },
            //        Status = Convert.ToString(HttpStatusCode.OK)
            //    };
            //}
            //return new ApiResponse()
            //{
            //    Code = Convert.ToString((int)HttpStatusCode.OK),
            //    Message = "User cannot skip two fa step.",
            //    Data =
            //    {
            //        CanSkipTwoFa = false
            //    },
            //    Status = Convert.ToString(HttpStatusCode.OK)
            //};
        }

        public async Task<ApiResponse> SkipTwoFa(int userId, TenantModel tenant)
        {
            var tenantTwoFaConfig = await GetTenantTwoFaConfig(tenant);
            if ((TwoFaStatus) tenantTwoFaConfig.McuTwoFaMobileModeId == TwoFaStatus.RequiredForAll)
            {
                return new ApiResponse()
                {
                    Code = Convert.ToString((int)HttpStatusCode.BadRequest),
                    Message = "User cannot skip two fa step.",
                    Data = new 
                        {
                            tenantTwoFaConfig.McuTwoFaMobileModeId
                        },
                    Status = Convert.ToString(HttpStatusCode.BadRequest)
                };
            }

            var userInfo = await Uow.Repository<User>()
                .Query(x => x.Id == userId && x.TenantId == tenant.Id && x.IsActive)
                .SingleAsync();

            if (userInfo.TwoFaEnabled != null)
            {
                return new ApiResponse()
                {
                    Code = Convert.ToString((int)HttpStatusCode.BadRequest),
                    Message = "User cannot skip two fa step.",
                    Data = new 
                    {
                        userInfo.TwoFaEnabled
                    },
                    Status = Convert.ToString(HttpStatusCode.BadRequest)
                };
            }

            userInfo.TwoFaEnabled = false;
            userInfo.TrackingState = TrackingState.Modified;
            Uow.Repository<User>().Update(userInfo);
            await Uow.SaveChangesAsync();
            return new ApiResponse()
            {
                Code = Convert.ToString((int)HttpStatusCode.OK),
                Message = null,
                Data = await GenerateNewAccessToken(userId, tenant.Id),
                Status = Convert.ToString(HttpStatusCode.OK)
            };
        }

        public async Task<ApiResponse> DontAskTwoFa(string token, int tenantId, string tenantCode)
        {
            if (string.IsNullOrEmpty(token))
            {
                return new ApiResponse()
                {
                    Code = Convert.ToString((int)HttpStatusCode.Unauthorized),
                    Status = HttpStatusCode.Unauthorized.ToString()
                };
            }

            token = token.Split(' ')[1];
            if (string.IsNullOrEmpty(token))
            {
                return new ApiResponse()
                {
                    Code = Convert.ToString((int)HttpStatusCode.Unauthorized),
                    Status = HttpStatusCode.Unauthorized.ToString()
                };
            }

            //security key
            var securityKey = await _keyStoreService.GetJwtSecurityKeyAsync();
            //symmetric security key
            var symmetricSecurityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key: Encoding.UTF8.GetBytes(s: securityKey));
            //signing credentials
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = symmetricSecurityKey,
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
            {
                return new ApiResponse()
                {
                    Code = Convert.ToString((int)HttpStatusCode.Unauthorized),
                    Status = HttpStatusCode.Unauthorized.ToString(),
                    Message = "Invalid token."
                };
            }

            var userIdClaim = principal.Claims.FirstOrDefault(c => c.Type == "UserId");
            if (userIdClaim == null)
            {
                return new ApiResponse()
                {
                    Code = Convert.ToString((int)HttpStatusCode.Unauthorized),
                    Status = HttpStatusCode.Unauthorized.ToString(),
                    Message = "Invalid token."
                };
            }

            var userProfile = await Uow.Repository<User>().Query(x => x.TenantId == tenantId && x.Id == int.Parse(userIdClaim.Value) && x.UserTypeId == (byte)TenantConfig.Common.UserType.Employee && x.IsActive).FirstOrDefaultAsync();
            if (userProfile == null)
            {
                return new ApiResponse()
                {
                    Code = Convert.ToString((int)HttpStatusCode.Unauthorized),
                    Status = HttpStatusCode.Unauthorized.ToString(),
                    Message = "User does not exist."
                };
            }

            CreateDontAskTwoFa(userProfile.UserName, tenantCode, int.Parse(userIdClaim.Value));
            return new ApiResponse()
            {
                Message = "Dont ask 2FA cookie created.",
                Code = Convert.ToString((int)HttpStatusCode.OK),
                Status = HttpStatusCode.OK.ToString()
            };
        }

        public void CreateDontAskTwoFa(string email, string tenantCode, int userId)
        {
            CookieOptions cookieOptions = new CookieOptions()
            {
                //Path = $"/{branchCode}/",
                Path = "/api/mobile/identity/mcuaccount/signin",
                Expires = DateTime.Now.AddDays(_twoFaHelperV2.DoNotAsk2FaCookieDays),
                IsEssential = false,
                HttpOnly = true, // TODO
                Secure = true // TODO
            };

            this._httpContextAccessor.HttpContext.Response.Cookies.Append(CreateCookieName(tenantCode, userId),
                email.ComputeCustomHash(System.Security.Cryptography.SHA256.Create()), cookieOptions);
        }

        public string CreateCookieName(string tenantCode, int userId)
        {
            var cookieName = $"{Constants.DONT_ASK_COOKIE_NAME}_{tenantCode}_{userId}";
            var partToHash = $"{tenantCode}_{userId}";
            var userProfile = Uow.Repository<User>().Query(u => u.Id == userId)
                .FirstOrDefault();
            if (userProfile != null && userProfile.PasswordSalt.HasValue())
            {
                System.Security.Cryptography.HashAlgorithm algorithm = new System.Security.Cryptography.SHA256Managed();
                var plainText = Encoding.ASCII.GetBytes(partToHash);
                var salt = Encoding.ASCII.GetBytes(userProfile.PasswordSalt);

                byte[] plainTextWithSaltBytes =
                    new byte[plainText.Length + salt.Length];

                for (int i = 0; i < plainText.Length; i++)
                {
                    plainTextWithSaltBytes[i] = plainText[i];
                }
                for (int i = 0; i < salt.Length; i++)
                {
                    plainTextWithSaltBytes[plainText.Length + i] = salt[i];
                }

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < plainTextWithSaltBytes.Length; i++)
                {
                    sb.Append(plainTextWithSaltBytes[i].ToString("x2"));
                }
                //string hashString = Encoding.ASCII.GetString(algorithm.ComputeHash(plainTextWithSaltBytes));
                string hashString = sb.ToString();
                cookieName = $"{Constants.DONT_ASK_COOKIE_NAME}_{hashString}";
            }
            return cookieName;
        }

        private async Task<IRestResponse> VerifyTwoFaRequest(VerifyTwoFaModel model, string serviceId)
        {
            var request = new RestRequest($"/Services/{serviceId}/VerificationCheck", Method.POST);
            request.AlwaysMultipartFormData = true;
            request.AddParameter("Code", model.Otp);
            //request.AddParameter("VerificationSid", model.VerificationSid);
            if (!model.PhoneNumber.StartsWith("+1"))
            {
                model.PhoneNumber = $"+1{model.PhoneNumber}";
            }
            request.AddParameter("To", model.PhoneNumber);
            IRestResponse response = await _twilioClient.ExecuteAsync(request);
            return response;
        }

        private async Task<HasCompletedTimeoutModel> HasCompletedTimeout(string phoneNumber)
        {
            var modelToReturn = new HasCompletedTimeoutModel()
            {
                HasCompleted = true,
                PhoneNumber = phoneNumber
            };

            var lastOtpLog = await Uow.Repository<OtpTracing>()
                .Query(x => ((x.Phone == $"+1{phoneNumber.Replace("+1", string.Empty)}")))
                .OrderByDescending(x => x.DateUtc)
                .FirstOrDefaultAsync();

            if (lastOtpLog == null) // No OTP log found against mobile number
            {
                return modelToReturn;
            }

            if (lastOtpLog.Message == "approved") // If last verification request was successful
            {
                return modelToReturn;
            }

        var currentUtc = DateTime.UtcNow;
            Debug.Assert(lastOtpLog.DateUtc != null, "lastOtpLog.DateUtc != null");
            var diffInSeconds = (currentUtc - lastOtpLog.DateUtc.Value).TotalSeconds.ToInt();

            if (diffInSeconds > _twoFaHelperV2.Resend2FaIntervalSeconds) // Yes, User completed timeout
            {
                modelToReturn.HasCompleted = true;
            }
            else // No, User has not completed timeout
            {
                modelToReturn.HasCompleted = false;
                modelToReturn.RemainingTimeout = (_twoFaHelperV2.Resend2FaIntervalSeconds - diffInSeconds);
                modelToReturn.LastSendAt = lastOtpLog.DateUtc;
                var lastTwilioResponse = JsonConvert.DeserializeObject<SendTwoFaResponse>(lastOtpLog.ResponseJson);
                modelToReturn.AttemptsCount = lastTwilioResponse.send_code_attempts?.Count;
            }
            return modelToReturn;
        }

        private async Task<TwoFaConfig> GetTenantTwoFaConfig(TenantModel tenant)
        {
            var config = await _uowTenantConfig.Repository<TwoFaConfig>()
                .Query(x => x.IsActive && x.TenantId == tenant.Id)
                .SingleAsync();
            return config;
        }

        private async Task<IRestResponse> SendTwoFaRequest(string toPhoneNumber, string serviceId)
        {
            var request = new RestRequest($"/Services/{serviceId}/Verifications", Method.POST);
            request.AlwaysMultipartFormData = true;

            
            if (!toPhoneNumber.Trim().StartsWith("+1"))
            {
                toPhoneNumber = $"+1{toPhoneNumber}";
            }
            request.AddParameter("To", toPhoneNumber);
            request.AddParameter("Channel", DEFAULT_CHANNEL);
            IRestResponse response = await _twilioClient.ExecuteAsync(request);
            return response;
        }

        private async Task LogSendTwoFaResponse(IRestResponse restResponse, int userId, string ipAddress,
            int? tenantId)
        {

            var sendTwoFaResponse = JsonConvert.DeserializeObject<SendTwoFaResponse>(restResponse.Content);

            string message = null;

            switch (restResponse.StatusCode)
            {
                case HttpStatusCode.Created:
                    message = sendTwoFaResponse.send_code_attempts.Count == 1 ? "OTP Created" : "OTP Resend";
                    break;
                case HttpStatusCode.TooManyRequests:
                    message = "Max attempts reached";
                    break;
                default:
                    message = "Unknown error";
                    break;
            }

            var userInfo = await Uow.Repository<User>()
                .Query(x => x.Id == userId && x.TenantId == tenantId && x.IsActive)
                .SingleAsync();

            OtpTracing log = new OtpTracing()
            {
                BranchId = null,
                CarrierName = sendTwoFaResponse.lookup?.carrier?.name,
                CarrierType = sendTwoFaResponse.lookup?.carrier?.type,
                CodeEntered = null,
                ContactId = null,
                DateUtc = DateTime.UtcNow,
                Email = userInfo?.UserName,
                EntityIdentifier = Guid.NewGuid(),
                IpAddress = ipAddress,
                Message = message,
                OtpCreatedOn = sendTwoFaResponse.date_created ?? DateTime.UtcNow,
                OtpRequestId = sendTwoFaResponse.sid,
                Phone = sendTwoFaResponse.to,
                ResponseJson = restResponse.Content,
                TenantId = tenantId,
                TracingTypeId = null,
                StatusCode = restResponse.StatusCode.ToInt(),
                TrackingState = TrackingState.Added,
                OtpUpdatedOn = sendTwoFaResponse.date_updated
            };

            if (!string.IsNullOrEmpty(log.Phone) && (!log.Phone.StartsWith("+1")))
            {
                log.Phone = $"+1{log.Phone}";
            }

            //if (sendTwoFaResponse.lookup != null && sendTwoFaResponse.lookup.carrier != null)
            //{
            //    log.CarrierName = sendTwoFaResponse.lookup.carrier.name;
            //    log.CarrierType = sendTwoFaResponse.lookup.carrier.type;
            //}

            Uow.Repository<OtpTracing>().Insert(log);
            await Uow.SaveChangesAsync();
        }

        private async Task LogVerifyTwoFaResponse(VerifyTwoFaModel model, IRestResponse restResponse, int userId, string ipAddress,
            int? tenantId)
        {
            VerifyTwoFaResponse verifyTwoFaResponse = null;

            string message = null;

            switch (restResponse.StatusCode)
            {
                case HttpStatusCode.OK:
                    verifyTwoFaResponse = JsonConvert.DeserializeObject<VerifyTwoFaResponse>(restResponse.Content);
                    //if (verifyTwoFaResponse.status == "approved")
                    //{
                    //    message = verifyTwoFaResponse.send_code_attempts.Count == 1 ? "OTP Created" : "OTP Resend";
                    //}

                    message = verifyTwoFaResponse.status;
                    break;
                case HttpStatusCode.NotFound:
                    message = "Request Not Found";
                    break;
                default:
                    message = "Unknown error";
                    break;
            }

            var userInfo = await Uow.Repository<User>()
                .Query(x => x.Id == userId && x.TenantId == tenantId && x.IsActive)
                .SingleAsync();

            OtpTracing log = null;
            if (verifyTwoFaResponse != null)
            {
                log = new OtpTracing()
                {
                    BranchId = null,
                    //CarrierName = verifyTwoFaResponse.lookup.carrier.name,
                    //CarrierType = verifyTwoFaResponse.lookup.carrier.type,
                    CodeEntered = null,
                    ContactId = null,
                    DateUtc = DateTime.UtcNow,
                    Email = userInfo?.UserName,
                    EntityIdentifier = Guid.NewGuid(),
                    IpAddress = ipAddress,
                    Message = message,
                    OtpCreatedOn = verifyTwoFaResponse.date_created,
                    OtpRequestId = verifyTwoFaResponse.sid,
                    Phone = verifyTwoFaResponse.to,
                    ResponseJson = restResponse.Content,
                    TenantId = tenantId,
                    TracingTypeId = null,
                    StatusCode = restResponse.StatusCode.ToInt(),
                    TrackingState = TrackingState.Added,
                    OtpUpdatedOn = verifyTwoFaResponse.date_updated
                };
                if(verifyTwoFaResponse.lookup != null && verifyTwoFaResponse.lookup.carrier != null)
                {
                    log.CarrierName = verifyTwoFaResponse.lookup.carrier.name;
                    log.CarrierType = verifyTwoFaResponse.lookup.carrier.type;
                }
            }
            else
            {
                log = new OtpTracing()
                {
                    BranchId = null,
                    CarrierName = null,
                    CarrierType = null,
                    CodeEntered = model.Otp,
                    ContactId = null,
                    DateUtc = DateTime.UtcNow,
                    Email = userInfo?.UserName,
                    EntityIdentifier = Guid.NewGuid(),
                    IpAddress = ipAddress,
                    Message = message,
                    Phone = model.PhoneNumber,
                    ResponseJson = restResponse.Content,
                    StatusCode = (int) restResponse.StatusCode,
                    TenantId = tenantId,
                    TrackingState = TrackingState.Added
                };
            }
            if (!string.IsNullOrEmpty(log.Phone) && (!log.Phone.StartsWith("+1")))
            {
                log.Phone = $"+1{log.Phone}";
            }
            Uow.Repository<OtpTracing>().Insert(log);
            await Uow.SaveChangesAsync();
        }

        private async Task AddTokenToRedis(TokenData tokenData)
        {
            await _tokenManager.AddAuthTokenToWhiteListAsync(tokenData: tokenData);
            await _tokenManager.AddRefreshTokenTokenAsync(tokenData: tokenData);
            await _tokenManager.CleanUpAuthTokenWhiteListAsync(tokenData: tokenData);
        }
    }
}
