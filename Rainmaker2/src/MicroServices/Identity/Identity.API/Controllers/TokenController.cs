using Identity.Model;
using Identity.Models;
using Identity.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using TokenCacheHelper.Models;
using TokenCacheHelper.TokenManager;

namespace Identity.Controllers
{
    [Route(template: "api/Identity/[controller]")]
    [ApiController]
    public class TokenController : Controller
    {
        #region Constructors

        public TokenController(IHttpClientFactory clientFactory,
                               IConfiguration configuration,
                               ITokenService tokenService,
                               ITwoFactorAuth twoFactorAuthService,
                               ITenantConfigService tenantConfigService,
                               ILogger<TokenController> logger,
                               ICustomerAccountService customerAccountService,
                               ICustomerService customerService,
                               ITokenManager tokenManager)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
            _tokenService = tokenService;
            this._logger = logger;
            this._customerAccountService = customerAccountService;
            this._customerService = customerService;
            this._tenantConfigService = tenantConfigService;
            this._twoFactorAuthService = twoFactorAuthService;
            //this._twoFactorAuthService.SetServiceSid(GetTenantVerificationSid());
            DEFAULT_USERTYPE = TenantConfig.Common.UserType.Customer;
            _tokenManager = tokenManager;
        }

        #endregion

        #region Private Variables

        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;
        private readonly ITwoFactorAuth _twoFactorAuthService;
        private readonly ITenantConfigService _tenantConfigService;
        private readonly ICustomerAccountService _customerAccountService;
        private readonly ICustomerService _customerService;
        private readonly ILogger<TokenController> _logger;

        private readonly TenantConfig.Common.UserType DEFAULT_USERTYPE;
        private readonly ITokenManager _tokenManager;

        #endregion

        #region Action Methods

        #region Get

        #endregion

        #region Post

        [Route(template: "[action]")]
        [HttpPost]
        public async Task<IActionResult> Refresh(RefreshTokenRequest request)
        {
            var response = new ApiResponse();

            var token = request.Token;
            var refreshToken = request.RefreshToken;
            var principal = await _tokenService.GetPrincipalFromExpiredToken(token: token);
            var userId = long.Parse(principal.FindFirst("UserId").Value);
            var userName = principal.FindFirst(ClaimTypes.Name).Value;

            var tokenPairValid = await _tokenManager.CheckPair(token: token,
                                                               refreshToken: refreshToken);

            if (!tokenPairValid)
            {
                response.Status = ApiResponse.ApiResponseStatus.Fail;
                response.Message = "Unable to find token";
                return BadRequest(error: response);
            }

            var newJwtToken = await _tokenService.GenerateAccessToken(claims: principal.Claims);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            var newTokenString = new JwtSecurityTokenHandler().WriteToken(token: newJwtToken);

            var tokenData = new TokenData
                            {
                                ValidTo = newJwtToken.ValidTo,
                                ValidFrom = newJwtToken.ValidFrom,
                                RefreshToken = newRefreshToken,
                                RefreshTokenValidTo = DateTime.UtcNow.AddMinutes(value: double.Parse(s: _configuration[key: "Token:RefreshTokenExpiryInMinutes"])),
                                Token = newTokenString,
                                UserName = userName,
                                UserProfileId=userId
                            };

            response.Data = tokenData;

            await _tokenManager.RevokeToken(token: request.Token);
            await _tokenManager.AddAuthTokenToWhiteListAsync(tokenData: tokenData);
            await _tokenManager.CleanUpAuthTokenWhiteListAsync(tokenData: tokenData);
            await _tokenManager.AddRefreshTokenTokenAsync(tokenData: tokenData);

            response.Status = ApiResponse.ApiResponseStatus.Success;

            return Ok(value: response);
        }
        #endregion
        
        #endregion
        
    }
}