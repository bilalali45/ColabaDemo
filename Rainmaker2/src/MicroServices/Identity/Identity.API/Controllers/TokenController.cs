using Identity.Models;
using Identity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Controllers
{
    [Route(template: "api/Identity/[controller]")]
    [ApiController]
    public class TokenController : Controller
    {
        #region Constructors

        public TokenController(IHttpClientFactory clientFactory,
                               IConfiguration configuration,
                               ITokenService tokenService)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
            _tokenService = tokenService;
        }

        #endregion

        #region Private Variables

        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;

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

            TokenPair tokenPair = null;
            if (TokenService.RefreshTokens.ContainsKey(key: username)) tokenPair = TokenService.RefreshTokens[key: username]?.FirstOrDefault(predicate: pair => pair.JwtToken == token && pair.RefreshToken == refreshToken);

            var user = await GetUser(userName: username);
            if (user == null || tokenPair == null)
            {

                response.Status = ApiResponse.ApiResponseStatus.Fail;
                response.Message = "Bad request";
                return Ok(value: response);
            }

            var newJwtToken = await _tokenService.GenerateAccessToken(claims: principal.Claims);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token: newJwtToken);

            
            TokenService.RefreshTokens[key: username].Remove(item: tokenPair);
            TokenService.RefreshTokens[key: username].Add(item: new TokenPair
                                                                {
                                                                    JwtToken = tokenString,
                                                                    RefreshToken = newRefreshToken
                                                                });

            
            response.Data = new
                            {
                                token = tokenString,
                                refreshToken = newRefreshToken
                            };
            response.Status = ApiResponse.ApiResponseStatus.Success;

            return Ok(value: response);
        }


        [Route(template: "[action]")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Revoke()
        {
            var response = new ApiResponse();
            var username = User.Identity.Name;

           
            var user = await GetUser(userName: username);
            if (user == null)
            {
    
                response.Status = ApiResponse.ApiResponseStatus.Fail;
                response.Message = "Bad request";
                return Ok(value: response);
            }


            TokenService.RefreshTokens.Remove(key: username);


            response.Status = ApiResponse.ApiResponseStatus.Success;
            response.Message = "Token revoked";
            return Ok(value: response);
        }

        [Route(template: "[action]")]
        [HttpGet]
        public IActionResult SingleSignOn(string key)
        {
            foreach(var pair in TokenService.RefreshTokens)
            {
                foreach(var tokens in pair.Value)
                {
                    if(tokens.RefreshToken==key)
                    {
                        return Ok(tokens.JwtToken);
                    }
                }
            }
            return BadRequest();
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

                return Ok(value: response);
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

            if (!TokenService.RefreshTokens.ContainsKey(key: userProfile.UserName)) TokenService.RefreshTokens[key: userProfile.UserName] = new List<TokenPair>();

            TokenService.RefreshTokens[key: userProfile.UserName].Add(item: new TokenPair
                                                                            {
                                                                                JwtToken = tokenString,
                                                                                RefreshToken = refreshToken
                                                                            });

            response.Status = ApiResponse.ApiResponseStatus.Success;
            response.Data = new
                            {
                                Token = tokenString,
                                refreshToken,
                                UserProfileId = userProfile.Id,
                                userProfile.UserName,
                                //CompanyPhones = userProfile.Employees.Single().EmployeePhoneBinders.Select(binder => binder.CompanyPhoneInfo.Phone),
                                jwtToken.ValidFrom,
                                jwtToken.ValidTo
                            };

            return Ok(value: response);
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