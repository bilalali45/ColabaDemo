using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter;
using Identity.Data;
using Identity.Entity.Models;
using Identity.Model;
using Identity.Models;
using Identity.Service;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MockQueryable.Moq;
using Moq;
using TenantConfig.Common;
using TenantConfig.Common.DistributedCache;
using TenantConfig.Data;
using TenantConfig.Entity.Models;
using TokenCacheHelper.TokenManager;
using URF.Core.Abstractions;
using URF.Core.EF;
using URF.Core.EF.Factories;
using Xunit;
using Customer = Identity.Models.Customer;

namespace Identity.Tests
{
    public partial class CustomerAccountTest2
    {
        [Fact]
        public async Task TestGenerateNewAccessToken()
        {
            // Arrange
            Mock<IServiceProvider> servicProviderMock = new Mock<IServiceProvider>();
            var logger = Mock.Of<ILogger<CustomerAccountService>>();
            Mock<ITokenService> tokenServiceMock = new Mock<ITokenService>();
            Mock<ITenantConfigService> tenantConfigServiceMock = new Mock<ITenantConfigService>();
            Mock<ICustomerService> customerServiceMock = new Mock<ICustomerService>();
            Mock<ITwoFactorAuth> twoFaAuthMock = new Mock<ITwoFactorAuth>();
            Mock<IActionContextAccessor> contextAccessorMock = new Mock<IActionContextAccessor>();
            Mock<IOtpTracingService> otpTracingServiceMock = new Mock<IOtpTracingService>();
            Mock<ITwoFaHelper> twoFaHelperMock = new Mock<ITwoFaHelper>();
            Mock<ITokenManager> mockTokenmanager = new Mock<ITokenManager>();
            Mock<IConfiguration> _configuration = new Mock<IConfiguration>();
            var tenantBuilder = new DbContextOptionsBuilder<TenantConfigContext>();
            tenantBuilder.UseInMemoryDatabase("TenantContext");
            var tenantOptions = tenantBuilder.Options;
            using TenantConfigContext tenantContext = new TenantConfigContext(tenantOptions);
            await tenantContext.Database.EnsureCreatedAsync();
            await tenantContext.SaveChangesAsync();

            var identityBuilder = new DbContextOptionsBuilder<IdentityContext>();
            identityBuilder.UseInMemoryDatabase("IdentityContext");
            var identityOptions = identityBuilder.Options;
            using IdentityContext identityContext = new IdentityContext(identityOptions);
            await identityContext.Database.EnsureCreatedAsync();
            await identityContext.SaveChangesAsync();

            TenantConfig.Entity.Models.Customer fakeCustomer = new TenantConfig.Entity.Models.Customer()
            {
                Contact = new TenantConfig.Entity.Models.Contact()
                {
                    FirstName = "fakeFirstName",
                    LastName = "fakeLastName"
                },
                UserId = 1,
                TenantId = 1,
                Tenant = new Tenant()
                {
                    Code = "fakeTenantCode"
                }
            };
            TenantModel fakeContextTenant = new TenantModel()
            {
                Branches = new EditableList<BranchModel>()
                {
                    new BranchModel()
                    {
                        Code = "fakeBranchCode"
                    }
                },
                Code = "fakeTenantCode",
                Id = 1,
            };
            var includes = new List<CustomerRelatedEntities>()
            {
                CustomerRelatedEntities.Contact,
                CustomerRelatedEntities.Tenant
            };

            customerServiceMock
                .Setup(x => x.GetCustomerByUserIdAsync(fakeCustomer.UserId.Value, fakeContextTenant.Id, includes))
                .ReturnsAsync(fakeCustomer);

            User fakeUserProfile = new User()
            {
                Id = fakeCustomer.UserId.Value,
                UserName = "loginName@fake.com",
                IsActive = true,
                TenantId=1
            };
            
            identityContext.Add<User>(fakeUserProfile);
            await identityContext.SaveChangesAsync();

            string fakeBranchCode = "fakeBranchCode";
            var usersClaims = new List<Claim>
            {
                new Claim(type: ClaimTypes.Role,
                    value: "Customer"),
                new Claim(type: "UserId",
                    value: fakeCustomer.UserId.ToString()),
                new Claim(type: ClaimTypes.Name,
                    value: fakeUserProfile.UserName.ToLower()),
                new Claim(type: "FirstName",
                    value: fakeCustomer.Contact.FirstName),
                new Claim(type: "LastName",
                    value: fakeCustomer.Contact.LastName),
                new Claim(type: "TenantCode",
                    value: fakeCustomer.Tenant.Code.ToLower()),
                new Claim(type: "BranchCode", fakeBranchCode)
            };

            string fakeSecurityKey = "a_quick_brown_fox_jumps_over_a_lazy_dog";
            var symmetricSecurityKey = new SymmetricSecurityKey(key: Encoding.UTF8.GetBytes(s: fakeSecurityKey));
            var signingCredentials = new SigningCredentials(key: symmetricSecurityKey, algorithm: SecurityAlgorithms.HmacSha256);
            var fakeJwtToken = new JwtSecurityToken(
                issuer: "fakeissue",
                audience: "fakereaders",
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: signingCredentials,
                claims: usersClaims
            );

            tokenServiceMock.Setup(x => x.GenerateAccessToken(It.IsAny<List<Claim>>())).ReturnsAsync(fakeJwtToken);
            tokenServiceMock.Setup(x => x.GenerateRefreshToken()).Returns("token");
       
            // Act
            CustomerAccountService service = new CustomerAccountService(new UnitOfWork<IdentityContext>(identityContext, new RepositoryProvider(new RepositoryFactories())),
                servicProviderMock.Object, new UnitOfWork<TenantConfigContext>(tenantContext, new RepositoryProvider(new RepositoryFactories()))
                , logger, tokenServiceMock.Object, tenantConfigServiceMock.Object, customerServiceMock.Object,
                twoFaAuthMock.Object, contextAccessorMock.Object, otpTracingServiceMock.Object, twoFaHelperMock.Object, _configuration.Object, mockTokenmanager.Object);
            var results = await service.GenerateNewAccessToken(fakeUserProfile.Id, fakeCustomer.TenantId.Value, fakeBranchCode);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<ApiResponse>(results);
            Assert.Equal(Convert.ToString((int)HttpStatusCode.OK), results.Code);
        }
    }
}
