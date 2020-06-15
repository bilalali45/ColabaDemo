using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Identity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Controllers
{
    [Route(template: "api/Identity/[controller]")]
    [ApiController]
    public class TokenController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory clientFactory;


        public TokenController(IHttpClientFactory clientFactory,
                               IConfiguration configuration)
        {
            this.clientFactory = clientFactory;
            _configuration = configuration;
        }
        //static HttpClient client = new HttpClient();


        [Route(template: "authorize")]
        [HttpPost]
        public async Task<IActionResult> GenerateToken(GenerateTokenRequest request)
        {
            var userName = request.UserName;
            var password = request.Password;
            var employee = request.Employee;

            var response = new ApiResponse();

            var httpClient = clientFactory.CreateClient();
            var values = new Dictionary<string, string>
                         {
                             {"userName", userName},
                             {"password", password},
                             {"employee", employee.ToString()}
                         };
            var callResponse = await httpClient.PostAsync(requestUri: $"{_configuration[key: "RainMaker:Url"]}/api/rainmaker/membership/validateuser",
                                                          content: new StringContent(request.ToJson(),Encoding.UTF8, "application/json"));
            if (callResponse.IsSuccessStatusCode)
            {
                var userProfile = await callResponse.Content.ReadAsAsync<UserProfile>();
                var jwtKeyResponse = await httpClient.GetAsync($"{_configuration["KeyStore:Url"]}/api/keystore/keystore?key=JWT");
                if (jwtKeyResponse.IsSuccessStatusCode)
                {
                    //security key
                    var securityKey = await jwtKeyResponse.Content.ReadAsStringAsync();
                    //symmetric security key
                    var symmetricSecurityKey = new SymmetricSecurityKey(key: Encoding.UTF8.GetBytes(s: securityKey));

                    //signing credentials
                    var signingCredentials =
                        new SigningCredentials(key: symmetricSecurityKey,
                                               algorithm: SecurityAlgorithms.HmacSha256Signature);

                    //add claims
                    var claims = new List<Claim>();

                    claims.Add(item: new Claim(type: ClaimTypes.Role,
                                               value: userProfile.Employees.FirstOrDefault() != null ? "MCU" : "Customer"));
                    claims.Add(item: new Claim(type: "UserProfileId",
                                               value: userProfile.Id.ToString()));
                    claims.Add(item: new Claim(type: "UserName",
                                               value: userProfile.UserName.ToLower()));
                    if (userProfile.Employees.FirstOrDefault() != null)
                        claims.Add(item: new Claim(type: "EmployeeId",
                                                   value: userProfile.Employees.Single().Id.ToString()));

                    //create token
                    var token = new JwtSecurityToken(
                                                     issuer: "rainsoftfn",
                                                     audience: "readers",
                                                     expires: DateTime.Now.AddHours(value: 1),
                                                     signingCredentials: signingCredentials,
                                                     claims: claims
                                                    );

                    //return token

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(token: token);
                    response.Status = ApiResponse.ApiResponseStatus.Success;
                    response.Data = new
                    {
                        Token = tokenString,
                        UserProfileId = userProfile.Id,
                        userProfile.UserName,
                        //CompanyPhones = userProfile.Employees.Single().EmployeePhoneBinders.Select(binder => binder.CompanyPhoneInfo.Phone),
                        token.ValidFrom,
                        token.ValidTo
                    };

                    return Ok(value: response);
                }
            }

            response.Status = ApiResponse.ApiResponseStatus.Fail;
            response.Message = "User not found.";

            return Ok(value: response);

            //var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            //{
            //    Address = "http://localhost:5010/connect/token",
            //    ClientId = "ClientId",
            //    ClientSecret = "ClientSecret",
            //    Scope = "SampleService"
            //});
            //return Ok(tokenResponse.Json);
        }
    }
}