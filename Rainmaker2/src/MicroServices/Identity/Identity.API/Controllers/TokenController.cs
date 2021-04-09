using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Identity.Models;
using Identity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TokenCacheHelper.Models;
using TokenCacheHelper.TokenManager;

namespace Identity.Controllers
{
    [Route(template: "api/Identity/[controller]")]
    [ApiController]
    public class TokenController : Controller
    {
        /*
        static TokenController()
        {
            Task.Factory.StartNew(async ()=> 
            { 
                while(true)
                {
                    await Task.Delay(5 * 60 * 1000);
                    try
                    {
                        lock (TokenService.lockObject)
                        {
                            foreach (var pair in TokenService.RefreshTokens)
                            {
                                for (int i=0;i<pair.Value.Count;i++)
                                {
                                    if ((DateTime.UtcNow-pair.Value[i].RefreshIssueDate).TotalMinutes>5*24*60)
                                    {
                                        pair.Value.RemoveAt(i);
                                        i--;
                                    }
                                }
                            }
                        }
                    }
                    catch
                    { }
                }
            },TaskCreationOptions.LongRunning);
        }*/

        #region Constructors

        public TokenController(IHttpClientFactory clientFactory,
                               IConfiguration configuration,
                               ITokenService tokenService,
                               ITokenManager tokenManager)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
            _tokenService = tokenService;
            _tokenManager = tokenManager;
        }

        #endregion

        #region Private Variables

        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;
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
            var username = principal.Identity.Name; //this is mapped to the Name claim by default
            var userId = int.Parse(s: principal.Claims.Where(predicate: x => x.Type == "UserProfileId").First().Value);
            //TokenPair tokenPair = null;
            //lock (TokenService.lockObject)
            //{
            //    if (TokenService.RefreshTokens.ContainsKey(key: username)) tokenPair = TokenService.RefreshTokens[key: username]?.FirstOrDefault(predicate: pair => pair.JwtToken == token && pair.RefreshToken == refreshToken);
            //}

            var tokenPairValid = await _tokenManager.CheckPair(token: token,
                                                               refreshToken: refreshToken);

            if (!tokenPairValid)
            {
   				response.Status = ApiResponse.ApiResponseStatus.Fail;
                response.Message = "Unable to find token";
                return BadRequest(response);

            }

            var user = await GetUser(userName: username);

            if (user == null)
            {
               response.Status = ApiResponse.ApiResponseStatus.Fail;
                response.Message = "Unable to find user";
                return BadRequest(response);
            }

            var newJwtToken = await _tokenService.GenerateAccessToken(claims: principal.Claims);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            var newTokenString = new JwtSecurityTokenHandler().WriteToken(token: newJwtToken);

            //TokenService.RefreshTokens[key: username].Remove(item: tokenPair);
            //lock (TokenService.lockObject)
            //{
            //    TokenService.RefreshTokens[key: username].Add(item: new TokenPair
            //    {
            //        JwtToken = tokenString,
            //        RefreshToken = newRefreshToken,
            //        RefreshIssueDate = DateTime.UtcNow
            //    });
            //}
            //await _tokenService.InsertToken(tokenString,newRefreshToken,DateTime.UtcNow.AddMinutes(double.Parse(_configuration["Token:RefreshTokenExpiryInMinutes"])),userId);

            var tokenData = new TokenData
                            {
                                ValidTo = newJwtToken.ValidTo,
                                ValidFrom = newJwtToken.ValidFrom,
                                RefreshToken = newRefreshToken,
                                RefreshTokenValidTo = DateTime.UtcNow.AddMinutes(value: double.Parse(s: _configuration[key: "Token:RefreshTokenExpiryInMinutes"])),
                                UserProfileId = user.Id,
                                UserName = user.UserName,
                                Token = newTokenString
                            };

            response.Data = new
                            {
                                token = newTokenString,
                                refreshToken = newRefreshToken
                            };

            await _tokenManager.RevokeToken(token: request.Token);
            await _tokenManager.AddAuthTokenToWhiteListAsync(tokenData: tokenData);
            await _tokenManager.CleanUpAuthTokenWhiteListAsync(tokenData: tokenData);
            await _tokenManager.AddRefreshTokenTokenAsync(tokenData: tokenData);

            response.Status = ApiResponse.ApiResponseStatus.Success;

            return Ok(value: response);
        }


        //[Route(template: "[action]")]
        //[HttpPost]
        //[Authorize]
        //public async Task<IActionResult> Revoke()
        //{
        //    var response = new ApiResponse();
        //    var username = User.Identity.Name;

        //    var user = await GetUser(userName: username);
        //    if (user == null)
        //    {

        //        response.Status = ApiResponse.ApiResponseStatus.Fail;
        //        response.Message = "Bad request";
        //        return Ok(value: response);
        //    }

        //    lock (TokenService.lockObject)
        //    {
        //        TokenService.RefreshTokens.Remove(key: username);
        //    }

        //    response.Status = ApiResponse.ApiResponseStatus.Success;
        //    response.Message = "Token revoked";
        //    return Ok(value: response);
        //}


        [Route(template: "[action]")]
        [HttpGet]
        public async Task<IActionResult> SingleSignOn(string key)
        {
            //var token = await _tokenService.GetForSignOn(key);
            var token = await _tokenManager.GetForSignOn(refreshToken: key);

            if (token != null) return Ok(value: token.Token);
            return BadRequest(new ApiResponse { Message="unable to find token"});
        }


        [Route(template: "[action]")]
        [HttpPost]
        public async Task<IActionResult> Authorize(GenerateTokenRequest request)
        {
            var response = new ApiResponse();

            var userProfile = await ValidateUser(userName: request.UserName,
                                                 password: request.Password,
                                                 employee: request.Employee);

            if (userProfile == null)
            {
                response.Status = ApiResponse.ApiResponseStatus.Fail;
                response.Message = "User not found.";

                return BadRequest(response);
            }

            #region Claims

            //add claims

            var contact = userProfile.Customers.Any() ? userProfile.Customers.First().Contact : userProfile.Employees.First().Contact;

            var usersClaims = new List<Claim>
                              {
                                  new Claim(type: ClaimTypes.Role,
                                            value: userProfile.Employees.Any() ? "MCU" : "Customer"),
                                  new Claim(type: "UserProfileId",
                                            value: userProfile.Id.ToString()),
                                  new Claim(type: "UserName",
                                            value: userProfile.UserName.ToLower()),
                                  new Claim(type: ClaimTypes.Name,
                                            value: userProfile.UserName.ToLower()),
                                  new Claim(type: "FirstName",
                                            value: contact.FirstName),
                                  new Claim(type: "LastName",
                                            value: contact.LastName),
                                  new Claim(type: "TenantId",
                                            value: "1")
                              };

            if (userProfile.Employees.FirstOrDefault() != null)
                usersClaims.Add(item: new Claim(type: "EmployeeId",
                                                value: userProfile.Employees.Single().Id.ToString()));

            #endregion

            var jwtToken = await _tokenService.GenerateAccessToken(claims: usersClaims);
            var refreshToken = _tokenService.GenerateRefreshToken();
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token: jwtToken);
            //lock (TokenService.lockObject)
            //{
            //    if (!TokenService.RefreshTokens.ContainsKey(key: userProfile.UserName)) TokenService.RefreshTokens[key: userProfile.UserName] = new List<TokenPair>();

            //    TokenService.RefreshTokens[key: userProfile.UserName].Add(item: new TokenPair
            //    {
            //        JwtToken = tokenString,
            //        RefreshToken = refreshToken,
            //        RefreshIssueDate = DateTime.UtcNow
            //    });
            //}
            //await _tokenService.InsertToken(tokenString,refreshToken, DateTime.UtcNow.AddMinutes(double.Parse(_configuration["Token:RefreshTokenExpiryInMinutes"])),(int)userProfile.Id);

            var tokenData = new TokenData
                            {
                                ValidTo = jwtToken.ValidTo,
                                ValidFrom = jwtToken.ValidFrom,
                                RefreshToken = refreshToken,
                                RefreshTokenValidTo = DateTime.UtcNow.AddMinutes(value: double.Parse(s: _configuration[key: "Token:RefreshTokenExpiryInMinutes"])),
                                UserProfileId = userProfile.Id,
                                UserName = userProfile.UserName,
                                Token = tokenString
                            };

            //response.Data = new
            //                {
            //                    Token = tokenString,
            //                    refreshToken,
            //                    UserProfileId = userProfile.Id,
            //                    userProfile.UserName,
            //                    //CompanyPhones = userProfile.Employees.Single().EmployeePhoneBinders.Select(binder => binder.CompanyPhoneInfo.Phone),
            //                    jwtToken.ValidFrom,
            //                    jwtToken.ValidTo
            //                };
            response.Data = tokenData;

            await _tokenManager.AddAuthTokenToWhiteListAsync(tokenData: tokenData);
            await _tokenManager.AddRefreshTokenTokenAsync(tokenData: tokenData);
            await _tokenManager.CleanUpAuthTokenWhiteListAsync(tokenData: tokenData);
            response.Status = ApiResponse.ApiResponseStatus.Success;

            return Ok(value: response);
        }


        [Route(template: "[action]")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RevokeAuthToken()
        {
            var token = Request.Headers[key: "Authorization"].FirstOrDefault()?.Split(separator: " ").LastOrDefault();
            if (!string.IsNullOrEmpty(value: token))
            {
                await _tokenManager.RevokeToken(token: token);
                return Ok();
            }

            return BadRequest(error: "Authorization token not found.");
        }


        [Route(template: "[action]")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RevokeAllAuthTokens()
        {
            var token = Request.Headers[key: "Authorization"].FirstOrDefault()?.Split(separator: " ").LastOrDefault();
            if (!string.IsNullOrEmpty(value: token))
            {
                await _tokenManager.RevokeAllToken(token: token);
                return Ok();
            }

            return BadRequest(error: "Authorization token not found.");
        }

        #endregion

        #endregion

        #region Helper Methods

        [NonAction]
        private async Task<UserProfile> ValidateUser(string userName,
                                                     string password,
                                                     bool employee)
        {
            var httpClient = _clientFactory.CreateClient(name: "clientWithCorrelationId");

            var content = new
                          {
                              userName,
                              password,
                              employee
                          };

            var callResponse = await httpClient.PostAsync(requestUri: $"{_configuration[key: "RainMaker:Url"]}/api/rainmaker/membership/validateUser",
                                                          content: new StringContent(content: content.ToJson(),
                                                                                     encoding: Encoding.UTF8,
                                                                                     mediaType: "application/json"));
            if (callResponse.IsSuccessStatusCode)
                return await callResponse.Content.ReadAsAsync<UserProfile>();
            return null;
        }


        [NonAction]
        private async Task<UserProfile> GetUser(string userName,
                                                bool employee = false)
        {
            var httpClient = _clientFactory.CreateClient(name: "clientWithCorrelationId");

            var content = new
                          {
                              userName,
                              employee
                          };

            var callResponse = await httpClient.PostAsync(requestUri: $"{_configuration[key: "RainMaker:Url"]}/api/rainmaker/membership/GetUser",
                                                          content: new StringContent(content: content.ToJson(),
                                                                                     encoding: Encoding.UTF8,
                                                                                     mediaType: "application/json"));
            if (callResponse.IsSuccessStatusCode)
                return await callResponse.Content.ReadAsAsync<UserProfile>();
            return null;
        }

        #endregion
    }
}