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
using Microsoft.IdentityModel.Tokens;

namespace Identity.Controllers
{
    [Route(template: "api/Identity/[controller]")]
    [ApiController]
    public class TokenController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;

        private readonly ITokenService _tokenService;


        public TokenController(IHttpClientFactory clientFactory,
                               IConfiguration configuration,
                               ITokenService tokenService)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
            _tokenService = tokenService;
        }
        //static HttpClient client = new HttpClient();


        //[Route(template: "authorize")]
        //[HttpPost]
        //public async Task<IActionResult> GenerateToken(GenerateTokenRequest request)
        //{
        //    var userName = request.UserName;
        //    var password = request.Password;
        //    var employee = request.Employee;

        //    var response = new ApiResponse();

        //    var userProfile = await ValidateUser(userName: userName,
        //                                         password: password,
        //                                         employee: employee);
        //    if (userProfile != null)
        //    {
        //        //security key
        //        var securityKey = _configuration[key: "JWT:SecurityKey"];
        //        //symmetric security key
        //        var symmetricSecurityKey = new SymmetricSecurityKey(key: Encoding.UTF8.GetBytes(s: securityKey));

        //        //signing credentials
        //        var signingCredentials =
        //            new SigningCredentials(key: symmetricSecurityKey,
        //                                   algorithm: SecurityAlgorithms.HmacSha256Signature);

        //        //add claims
        //        var claims = new List<Claim>
        //                     {
        //                         new Claim(type: ClaimTypes.Role,
        //                                   value: userProfile.Employees.FirstOrDefault() != null ? "MCU" : "Customer"),
        //                         new Claim(type: "UserProfileId",
        //                                   value: userProfile.Id.ToString()),
        //                         new Claim(type: "UserName",
        //                                   value: userProfile.UserName.ToLower())
        //                     };

        //        if (userProfile.Employees.FirstOrDefault() != null)
        //            claims.Add(item: new Claim(type: "EmployeeId",
        //                                       value: userProfile.Employees.Single().Id.ToString()));

        //        //create token
        //        var token = new JwtSecurityToken(
        //                                         issuer: "rainsoftfn",
        //                                         audience: "readers",
        //                                         expires: DateTime.Now.AddHours(value: 1),
        //                                         signingCredentials: signingCredentials,
        //                                         claims: claims
        //                                        );

        //        //return token

        //        var tokenString = new JwtSecurityTokenHandler().WriteToken(token: token);
        //        response.Status = ApiResponse.ApiResponseStatus.Success;
        //        response.Data = new
        //        {
        //            Token = tokenString,
        //            UserProfileId = userProfile.Id,
        //            userProfile.UserName,
        //            //CompanyPhones = userProfile.Employees.Single().EmployeePhoneBinders.Select(binder => binder.CompanyPhoneInfo.Phone),
        //            token.ValidFrom,
        //            token.ValidTo
        //        };

        //        return Ok(value: response);
        //    }

        //    response.Status = ApiResponse.ApiResponseStatus.Fail;
        //    response.Message = "User not found.";

        //    return Ok(value: response);

        //    //var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        //    //{
        //    //    Address = "http://localhost:5010/connect/token",
        //    //    ClientId = "ClientId",
        //    //    ClientSecret = "ClientSecret",
        //    //    Scope = "SampleService"
        //    //});
        //    //return Ok(tokenResponse.Json);
        //}

        [Route(template: "[action]")]
        [HttpPost]
        public async Task<IActionResult> Refresh(RefreshTokenRequest request)
        {
            var response = new ApiResponse();

            var token = request.Token;
            var refreshToken = request.RefreshToken;
            var principal = await _tokenService.GetPrincipalFromExpiredToken(token: token);
            var username = principal.Identity.Name; //this is mapped to the Name claim by default
            var userRefreshToken = TokenService.RefreshTokens[key: username];

            var user = await GetUser(userName: username);
            if (user == null || userRefreshToken != refreshToken)
            {
                //return BadRequest();

                response.Status = ApiResponse.ApiResponseStatus.Fail;
                response.Message = "Bad request";
                return Ok(value: response);
            }

            var newJwtToken = await _tokenService.GenerateAccessToken(claims: principal.Claims);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token: newJwtToken);

            //user.RefreshToken = newRefreshToken;

            TokenService.RefreshTokens[username] = newRefreshToken;


            //await _usersDb.SaveChangesAsync();

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

            //var user = _usersDb.Users.SingleOrDefault(u => u.Username == username);
            var user = await GetUser(userName: username);
            if (user == null)
            {
                //return BadRequest();
                response.Status = ApiResponse.ApiResponseStatus.Fail;
                response.Message = "Bad request";
                return Ok(value: response);
            }

            //user.RefreshToken = null;
            TokenService.RefreshTokens.Remove(key: username);

            //await _usersDb.SaveChangesAsync();
            response.Status = ApiResponse.ApiResponseStatus.Success;
            response.Message = "Token revoked";
            return Ok(value: response);
        }


        private async Task<UserProfile> ValidateUser(string userName,
                                                     string password,
                                                     bool employee)
        {
            var httpClient = _clientFactory.CreateClient();

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


        private async Task<UserProfile> GetUser(string userName,
                                                bool employee = false)
        {
            var httpClient = _clientFactory.CreateClient();

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

        [Route(template: "[action]")]
        [HttpPost]
        public async Task<IActionResult> Authorize(GenerateTokenRequest request)
        {
            var response = new ApiResponse();

            var userProfile = await ValidateUser(userName: request.UserName,
                                                 password: request.Password,
                                                 employee: request.Employee);

            //if (user == null || !_passwordHasher.VerifyIdentityV3Hash(password,
            //                                                          user.Password)) return BadRequest();
            if (userProfile == null)
            {
                response.Status = ApiResponse.ApiResponseStatus.Fail;
                response.Message = "User not found.";

                return Ok(value: response);
            }

            #region Claims

            //var usersClaims = new[]
            //                  {
            //                      new Claim(type: ClaimTypes.Name,
            //                                value: userProfile.Username),
            //                      new Claim(type: ClaimTypes.NameIdentifier,
            //                                value: userProfile.Id.ToString())
            //                  };

            //add claims
            var usersClaims = new List<Claim>
                              {
                                  new Claim(type: ClaimTypes.Role,
                                            value: userProfile.Employees.FirstOrDefault() != null ? "MCU" : "Customer"),
                                  new Claim(type: "UserProfileId",
                                            value: userProfile.Id.ToString()),
                                  new Claim(type: "UserName",
                                            value: userProfile.UserName.ToLower()),
                                  new Claim(ClaimTypes.Name, userProfile.UserName.ToLower())
                              };

            if (userProfile.Employees.FirstOrDefault() != null)
                usersClaims.Add(item: new Claim(type: "EmployeeId",
                                                value: userProfile.Employees.Single().Id.ToString()));

            #endregion

            var jwtToken = await _tokenService.GenerateAccessToken(claims: usersClaims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            //userProfile.RefreshToken = refreshToken;
            //await _usersDb.SaveChangesAsync();

            TokenService.RefreshTokens[userProfile.UserName] = refreshToken;

            //return new ObjectResult(value: new
            //                               {
            //                                   token = jwtToken,
            //                                   refreshToken
            //                               });
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token: jwtToken);
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



        [Route(template: "[action]")]
        [HttpPost]
        public IActionResult TestException(GenerateTokenRequest request)
        {
           throw new Exception("test exception");

            return Ok(value: "ok");
        }
    }
}