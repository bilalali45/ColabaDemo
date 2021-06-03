using Identity.Controllers;
using Identity.Data;
using Identity.Entity.Models;
using Identity.Model;
using Identity.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TenantConfig.Common;
using TenantConfig.Common.DistributedCache;
using TenantConfig.Data;
using TenantConfig.Entity.Models;
using TokenCacheHelper.TokenManager;
using URF.Core.Abstractions;
using URF.Core.EF;
using URF.Core.EF.Factories;
using Xunit;
namespace Identity.Tests
{
    public partial class CustomerAccountTest
    {
        [Fact]
        public async Task TestRegisterController()
        {
            Mock<ICustomerAccountService> mockCustomerAccountService = new Mock<ICustomerAccountService>();
            Mock<ITwoFactorAuth> mockTwoFactorAuth = new Mock<ITwoFactorAuth>();
            Mock<ITokenService> mockTokenService = new Mock<ITokenService>();
            Mock<ITenantConfigService> mockTenantConfigService = new Mock<ITenantConfigService>();
            Mock<ITwoFaHelper> mockTwoFaHelper = new Mock<ITwoFaHelper>();
            Mock<ICustomerService> mockCustomerService = new Mock<ICustomerService>();
            Mock<IOtpTracingService> mockOtpService = new Mock<IOtpTracingService>();
            var httpContext = new DefaultHttpContext();

            TenantModel model = new TenantModel();
            model.Id = 9;
            model.Code = "33";
            model.Urls = new List<UrlModel>
            {  new UrlModel
            {
                Type=TenantUrlType.Customer,
                Url="apply.lendova9.com:9003"
            }
            };
            model.Branches = new List<BranchModel>

            {
                new BranchModel
                {
                    Code="texas",
                    Id=9,
                    IsCorporate=true,
                    LoanOfficers= new List<LoanOfficerModel>
                    {
                         new LoanOfficerModel
                         {
                             Id=9,
                             Code="33"
                         }
                    }

                }
            };
            httpContext.Items.Add(Constants.COLABA_TENANT, model);
            mockCustomerAccountService.Setup(x => x.GenerateNewAccessToken(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(new ApiResponse
            {
                Code = "200",
            });
            var context = new ControllerContext(new ActionContext(httpContext, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            var controller = new CustomerAccountController(mockCustomerAccountService.Object, mockTwoFaHelper.Object, mockCustomerService.Object, mockTenantConfigService.Object, mockOtpService.Object);
            controller.ControllerContext = context;

            //Act
            RegisterModel registerModel = new RegisterModel
            {
                Email = "abc@mail.com",
                FirstName = "abc",
                LastName = "xyz",
                Password = "123",
                Phone = "0364598522"
            };
            IActionResult result = await controller.Register(registerModel);

            //Assert
            Assert.NotNull(result);
            var res = Assert.IsType<OkObjectResult>(result);
            var res1 = Assert.IsType<ApiResponse>(res.Value);
            Assert.Equal("200", res1.Code);


        }
        [Fact]
        public async Task TestRegisterDoesCustomerAccountExistIsTrueController()
        {
            Mock<ICustomerAccountService> mockCustomerAccountService = new Mock<ICustomerAccountService>();
            Mock<ITwoFactorAuth> mockTwoFactorAuth = new Mock<ITwoFactorAuth>();
            Mock<ITokenService> mockTokenService = new Mock<ITokenService>();
            Mock<ITenantConfigService> mockTenantConfigService = new Mock<ITenantConfigService>();
            Mock<ITwoFaHelper> mockTwoFaHelper = new Mock<ITwoFaHelper>();
            Mock<ICustomerService> mockCustomerService = new Mock<ICustomerService>();
            Mock<IOtpTracingService> mockOtpService = new Mock<IOtpTracingService>();
            var httpContext = new DefaultHttpContext();

            TenantModel model = new TenantModel();
            model.Id = 9;
            model.Code = "33";
            model.Urls = new List<UrlModel>
            {  new UrlModel
            {
                Type=TenantUrlType.Customer,
                Url="apply.lendova9.com:9003"
            }
            };
            model.Branches = new List<BranchModel>

            {
                new BranchModel
                {
                    Code="texas",
                    Id=9,
                    IsCorporate=true,
                    LoanOfficers= new List<LoanOfficerModel>
                    {
                         new LoanOfficerModel
                         {
                             Id=9,
                             Code="33"
                         }
                    }

                }
            };
            httpContext.Items.Add(Constants.COLABA_TENANT, model);
            mockCustomerAccountService.Setup(x => x.DoesCustomerAccountExist(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(true);
            mockCustomerAccountService.Setup(x => x.GenerateNewAccessToken(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(new ApiResponse
            {
                Code = "200",
            });
            var context = new ControllerContext(new ActionContext(httpContext, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            var controller = new CustomerAccountController(mockCustomerAccountService.Object, mockTwoFaHelper.Object, mockCustomerService.Object, mockTenantConfigService.Object, mockOtpService.Object);
            controller.ControllerContext = context;

            //Act

            RegisterModel registerModel = new RegisterModel
            {
                Email = "abc@mail.com",
                FirstName = "abc",
                LastName = "xyz",
                Password = "123",
                Phone = "0364598522"
            };
            IActionResult result = await controller.Register(registerModel);

            //Assert
            Assert.NotNull(result);
            var res = Assert.IsType<BadRequestObjectResult>(result);
            var res1 = Assert.IsType<ApiResponse>(res.Value);
            Assert.Equal("400", res1.Code);


        }
        [Fact]
        public async Task TestDoesCustomerAccountExistController()
        {
            Mock<ICustomerAccountService> mockCustomerAccountService = new Mock<ICustomerAccountService>();
            Mock<ITwoFactorAuth> mockTwoFactorAuth = new Mock<ITwoFactorAuth>();
            Mock<ITokenService> mockTokenService = new Mock<ITokenService>();
            Mock<ITenantConfigService> mockTenantConfigService = new Mock<ITenantConfigService>();
            Mock<ITwoFaHelper> mockTwoFaHelper = new Mock<ITwoFaHelper>();
            Mock<ICustomerService> mockCustomerService = new Mock<ICustomerService>();
            Mock<IOtpTracingService> mockOtpService = new Mock<IOtpTracingService>();
            var httpContext = new DefaultHttpContext();

            TenantModel model = new TenantModel();
            model.Id = 9;
            model.Code = "33";
            model.Urls = new List<UrlModel>
            {  new UrlModel
            {
                Type=TenantUrlType.Customer,
                Url="apply.lendova9.com:9003"
            }
            };
            model.Branches = new List<BranchModel>

            {
                new BranchModel
                {
                    Code="texas",
                    Id=9,
                    IsCorporate=true,
                    LoanOfficers= new List<LoanOfficerModel>
                    {
                         new LoanOfficerModel
                         {
                             Id=9,
                             Code="33"
                         }
                    }

                }
            };
            httpContext.Items.Add(Constants.COLABA_TENANT, model);
            mockCustomerAccountService.Setup(x => x.DoesCustomerAccountExist(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(true);
            var context = new ControllerContext(new ActionContext(httpContext, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            var controller = new CustomerAccountController(mockCustomerAccountService.Object, mockTwoFaHelper.Object, mockCustomerService.Object, mockTenantConfigService.Object, mockOtpService.Object);
            controller.ControllerContext = context;

            //Act


            IActionResult result = await controller.DoesCustomerAccountExist("abc@mail.com");

            //Assert
            Assert.NotNull(result);
            var res = Assert.IsType<OkObjectResult>(result);
            var res1 = Assert.IsType<ApiResponse>(res.Value);

            Assert.Equal("200", res1.Code);


        }
        [Fact]
        public async Task TestDoesCustomerAccountExistIsFalseController()
        {
            Mock<ICustomerAccountService> mockCustomerAccountService = new Mock<ICustomerAccountService>();
            Mock<ITwoFactorAuth> mockTwoFactorAuth = new Mock<ITwoFactorAuth>();
            Mock<ITokenService> mockTokenService = new Mock<ITokenService>();
            Mock<ITenantConfigService> mockTenantConfigService = new Mock<ITenantConfigService>();
            Mock<ITwoFaHelper> mockTwoFaHelper = new Mock<ITwoFaHelper>();
            Mock<ICustomerService> mockCustomerService = new Mock<ICustomerService>();
            Mock<IOtpTracingService> mockOtpService = new Mock<IOtpTracingService>();
            var httpContext = new DefaultHttpContext();

            TenantModel model = new TenantModel();
            model.Id = 9;
            model.Code = "33";
            model.Urls = new List<UrlModel>
            {  new UrlModel
            {
                Type=TenantUrlType.Customer,
                Url="apply.lendova9.com:9003"
            }
            };
            model.Branches = new List<BranchModel>

            {
                new BranchModel
                {
                    Code="texas",
                    Id=9,
                    IsCorporate=true,
                    LoanOfficers= new List<LoanOfficerModel>
                    {
                         new LoanOfficerModel
                         {
                             Id=9,
                             Code="33"
                         }
                    }

                }
            };
            httpContext.Items.Add(Constants.COLABA_TENANT, model);
            mockCustomerAccountService.Setup(x => x.DoesCustomerAccountExist(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(false);
            var context = new ControllerContext(new ActionContext(httpContext, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            var controller = new CustomerAccountController(mockCustomerAccountService.Object, mockTwoFaHelper.Object, mockCustomerService.Object, mockTenantConfigService.Object, mockOtpService.Object);
            controller.ControllerContext = context;

            //Act


            IActionResult result = await controller.DoesCustomerAccountExist("abc@mail.com");

            //Assert
            Assert.NotNull(result);
            var res = Assert.IsType<OkObjectResult>(result);
            var res1 = Assert.IsType<ApiResponse>(res.Value);

            Assert.Equal("200", res1.Code);


        }
        [Fact]
        public async Task TestSigninController()
        {
            Mock<ICustomerAccountService> mockCustomerAccountService = new Mock<ICustomerAccountService>();
            Mock<ITwoFactorAuth> mockTwoFactorAuth = new Mock<ITwoFactorAuth>();
            Mock<ITokenService> mockTokenService = new Mock<ITokenService>();
            Mock<ITenantConfigService> mockTenantConfigService = new Mock<ITenantConfigService>();
            Mock<ITwoFaHelper> mockTwoFaHelper = new Mock<ITwoFaHelper>();
            Mock<ICustomerService> mockCustomerService = new Mock<ICustomerService>();
            Mock<IOtpTracingService> mockOtpService = new Mock<IOtpTracingService>();
            var httpContext = new DefaultHttpContext();

            TenantModel model = new TenantModel();
            model.Id = 9;
            model.Code = "33";
            model.Urls = new List<UrlModel>
            {  new UrlModel
            {
                Type=TenantUrlType.Customer,
                Url="apply.lendova9.com:9003"
            }
            };
            model.Branches = new List<BranchModel>

            {
                new BranchModel
                {
                    Code="texas",
                    Id=9,
                    IsCorporate=true,
                    LoanOfficers= new List<LoanOfficerModel>
                    {
                         new LoanOfficerModel
                         {
                             Id=9,
                             Code="33"
                         }
                    }

                }
            };
            SigninModel signinModel = new SigninModel
            {
                Email = "abc@mail.com",
                Password = "1233333333"
            };
            httpContext.Items.Add(Constants.COLABA_TENANT, model);
            mockCustomerAccountService.Setup(x => x.Signin(It.IsAny<SigninModel>(), It.IsAny<TenantModel>())).ReturnsAsync(new ApiResponse
            {
                Code = "200"
            });
            var context = new ControllerContext(new ActionContext(httpContext, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            var controller = new CustomerAccountController(mockCustomerAccountService.Object, mockTwoFaHelper.Object, mockCustomerService.Object, mockTenantConfigService.Object, mockOtpService.Object);
            controller.ControllerContext = context;

            //Act


            IActionResult result = await controller.Signin(signinModel);

            //Assert
            Assert.NotNull(result);
            var res = Assert.IsType<OkObjectResult>(result);
            var res1 = Assert.IsType<ApiResponse>(res.Value);

            Assert.Equal("200", res1.Code);


        }
        [Fact]
        public async Task TestForgotPasswordRequestController()
        {
            Mock<ICustomerAccountService> mockCustomerAccountService = new Mock<ICustomerAccountService>();
            Mock<ITwoFaHelper> mockTwoFaHelper = new Mock<ITwoFaHelper>();
            Mock<ICustomerService> mockCustomerService = new Mock<ICustomerService>();
            Mock<ITokenService> mockTokenService = new Mock<ITokenService>();
            Mock<ITenantConfigService> mockTenantConfigService = new Mock<ITenantConfigService>();
            Mock<IOtpTracingService> mockOtpService = new Mock<IOtpTracingService>();


            var httpContext = new DefaultHttpContext();

            TenantModel model = new TenantModel();
            model.Id = 9;
            model.Code = "33";
            model.Urls = new List<UrlModel>
            {  new UrlModel
            {
                Type=TenantUrlType.Customer,
                Url="apply.lendova9.com:9003"
            }
            };
            model.Branches = new List<BranchModel>

            {
                new BranchModel
                {
                    Code="texas",
                    Id=9,
                    IsCorporate=true,
                    LoanOfficers= new List<LoanOfficerModel>
                    {
                         new LoanOfficerModel
                         {
                             Id=9,
                             Code="33"
                         }
                    }

                }
            };
            ForgotPasswordRequestModel forgotPasswordRequestModel = new ForgotPasswordRequestModel
            { Email = "abc@mail.com" };

            httpContext.Items.Add(Constants.COLABA_TENANT, model);
            mockCustomerAccountService.Setup(x => x.ForgotPasswordRequest(It.IsAny<ForgotPasswordRequestModel>(), It.IsAny<TenantModel>())).ReturnsAsync(new ApiResponse
            {
                Code = "200"
            });
            var context = new ControllerContext(new ActionContext(httpContext, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            var controller = new CustomerAccountController(mockCustomerAccountService.Object, mockTwoFaHelper.Object, mockCustomerService.Object, mockTenantConfigService.Object, mockOtpService.Object);

            controller.ControllerContext = context;

            //Act


            IActionResult result = await controller.ForgotPasswordRequest(forgotPasswordRequestModel);

            //Assert
            Assert.NotNull(result);
            var res = Assert.IsType<OkObjectResult>(result);
            var res1 = Assert.IsType<ApiResponse>(res.Value);

            Assert.Equal("200", res1.Code);


        }
        [Fact]
        public async Task TestChangePasswordController()
        {
            Mock<ICustomerAccountService> mockCustomerAccountService = new Mock<ICustomerAccountService>();
            Mock<ITwoFactorAuth> mockTwoFactorAuth = new Mock<ITwoFactorAuth>();
            Mock<ITokenService> mockTokenService = new Mock<ITokenService>();
            Mock<ITenantConfigService> mockTenantConfigService = new Mock<ITenantConfigService>();
            Mock<ITwoFaHelper> mockTwoFaHelper = new Mock<ITwoFaHelper>();
            Mock<ICustomerService> mockCustomerService = new Mock<ICustomerService>();
            Mock<IOtpTracingService> mockOtpService = new Mock<IOtpTracingService>();
            var httpContext = new DefaultHttpContext();

            TenantModel model = new TenantModel();
            model.Id = 9;
            model.Code = "33";
            model.Urls = new List<UrlModel>
            {  new UrlModel
            {
                Type=TenantUrlType.Customer,
                Url="apply.lendova9.com:9003"
            }
            };
            model.Branches = new List<BranchModel>

            {
                new BranchModel
                {
                    Code="texas",
                    Id=9,
                    IsCorporate=true,
                    LoanOfficers= new List<LoanOfficerModel>
                    {
                         new LoanOfficerModel
                         {
                             Id=9,
                             Code="33"
                         }
                    }

                }
            };
            ChangePasswordModel changePassword = new ChangePasswordModel
            {
                newPassword = "test124344",
                oldPassword = "test123838383"
            };


            var httpContext1 = new Mock<HttpContext>();
            httpContext1.Setup(m => m.User.FindFirst("UserId")).Returns(new Claim("UserId", "1"));
            Dictionary<object, object> keyValuePairs = new Dictionary<object, object>();
            keyValuePairs.Add(Constants.COLABA_TENANT, model);
            httpContext1.Setup(x => x.Items).Returns(keyValuePairs);
            mockCustomerAccountService.Setup(x => x.ChangePassword(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(new ApiResponse
            {
                Code = "200"
            });
            var context = new ControllerContext(new ActionContext(httpContext1.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            var controller = new CustomerAccountController(mockCustomerAccountService.Object, mockTwoFaHelper.Object, mockCustomerService.Object, mockTenantConfigService.Object, mockOtpService.Object);
            controller.ControllerContext = context;

            //Act


            IActionResult result = await controller.ChangePassword(changePassword);

            //Assert
            Assert.NotNull(result);
            var res = Assert.IsType<OkObjectResult>(result);
            var res1 = Assert.IsType<ApiResponse>(res.Value);

            Assert.Equal("200", res1.Code);


        }
        [Fact]
        public async Task TestForgotPasswordResponseController()
        {
            Mock<ICustomerAccountService> mockCustomerAccountService = new Mock<ICustomerAccountService>();
            Mock<ITwoFactorAuth> mockTwoFactorAuth = new Mock<ITwoFactorAuth>();
            Mock<ITokenService> mockTokenService = new Mock<ITokenService>();
            Mock<ITenantConfigService> mockTenantConfigService = new Mock<ITenantConfigService>();
            Mock<ITwoFaHelper> mockTwoFaHelper = new Mock<ITwoFaHelper>();
            Mock<ICustomerService> mockCustomerService = new Mock<ICustomerService>();
            Mock<IOtpTracingService> mockOtpService = new Mock<IOtpTracingService>();
            var httpContext = new DefaultHttpContext();

            TenantModel model = new TenantModel();
            model.Id = 9;
            model.Code = "33";
            model.Urls = new List<UrlModel>
            {  new UrlModel
            {
                Type=TenantUrlType.Customer,
                Url="apply.lendova9.com:9003"
            }
            };
            model.Branches = new List<BranchModel>

            {
                new BranchModel
                {
                    Code="texas",
                    Id=9,
                    IsCorporate=true,
                    LoanOfficers= new List<LoanOfficerModel>
                    {
                         new LoanOfficerModel
                         {
                             Id=9,
                             Code="33"
                         }
                    }

                }
            };
            ForgotPasswordResponseModel forgotPasswordResponseModel = new ForgotPasswordResponseModel
            {
                Key = "",
                Password = "",
                UserId = 1
            };


            var httpContext1 = new Mock<HttpContext>();
            httpContext1.Setup(m => m.User.FindFirst("UserId")).Returns(new Claim("UserId", "1"));
            Dictionary<object, object> keyValuePairs = new Dictionary<object, object>();
            keyValuePairs.Add(Constants.COLABA_TENANT, model);
            httpContext1.Setup(x => x.Items).Returns(keyValuePairs);
            mockCustomerAccountService.Setup(x => x.ForgotPasswordResponse(It.IsAny<ForgotPasswordResponseModel>(), It.IsAny<TenantModel>())).ReturnsAsync(new ApiResponse
            {
                Code = "200"
            });
            var context = new ControllerContext(new ActionContext(httpContext1.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            var controller = new CustomerAccountController(mockCustomerAccountService.Object, mockTwoFaHelper.Object, mockCustomerService.Object, mockTenantConfigService.Object, mockOtpService.Object);
            controller.ControllerContext = context;

            //Act


            IActionResult result = await controller.ForgotPasswordResponse(forgotPasswordResponseModel);

            //Assert
            Assert.NotNull(result);
            var res = Assert.IsType<OkObjectResult>(result);
            var res1 = Assert.IsType<ApiResponse>(res.Value);

            Assert.Equal("200", res1.Code);


        }

        [Fact]
        public async Task TestDoesCustomerAccountExistsService()

        {
            Mock<ICustomerAccountService> mockCustomerAccountService = new Mock<ICustomerAccountService>();
            Mock<IServiceProvider> mockServiceProvider = new Mock<IServiceProvider>();
            Mock<IUnitOfWork<TenantConfigContext>> mockTenantService = new Mock<IUnitOfWork<TenantConfigContext>>();
            Mock<ILogger<CustomerAccountService>> mockLoggerService = new Mock<ILogger<CustomerAccountService>>();
            Mock<ITokenService> mockTokenService = new Mock<ITokenService>();
            Mock<ITenantConfigService> mockTenantConfigService = new Mock<ITenantConfigService>();
            Mock<ICustomerService> mockCustomerService = new Mock<ICustomerService>();
            Mock<ITwoFactorAuth> mockTwoFactorAuth = new Mock<ITwoFactorAuth>();
            Mock<IActionContextAccessor> mockActionContextAccessor = new Mock<IActionContextAccessor>();
            Mock<IOtpTracingService> mockOtpTracingService = new Mock<IOtpTracingService>();
            Mock<ITwoFaHelper> mockTwoFaHelper = new Mock<ITwoFaHelper>();

            //Arrange
            DbContextOptions<IdentityContext> options;
            var builder = new DbContextOptionsBuilder<IdentityContext>();
            builder.UseInMemoryDatabase("Identity");
            options = builder.Options;
            using IdentityContext dataContext = new IdentityContext(options);

            dataContext.Database.EnsureCreated();
            User user = new User
            {
                Id = 1,
                UserName = "abc",
                TenantId = 1
                ,
                UserTypeId = 1

            };
            dataContext.SaveChanges();

            mockCustomerAccountService.Setup(x => x.DoesCustomerAccountExist(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(true);
            var service = new CustomerAccountService(new UnitOfWork<IdentityContext>(dataContext, new RepositoryProvider(new RepositoryFactories())),
                mockServiceProvider.Object, mockTenantService.Object, mockLoggerService.Object, mockTokenService.Object
                , mockTenantConfigService.Object, mockCustomerService.Object, mockTwoFactorAuth.Object, mockActionContextAccessor.Object
               , mockOtpTracingService.Object, mockTwoFaHelper.Object,null,null);
            //Act
            var result = await service.DoesCustomerAccountExist("abc", 1);
            //Assert
            Assert.False(result);

        }

        [Fact]
        public async Task TestRegisterService()

        {
            Mock<ICustomerAccountService> mockCustomerAccountService = new Mock<ICustomerAccountService>();
            Mock<IServiceProvider> mockServiceProvider = new Mock<IServiceProvider>();
            Mock<ILogger<CustomerAccountService>> mockLoggerService = new Mock<ILogger<CustomerAccountService>>();
            Mock<ITokenService> mockTokenService = new Mock<ITokenService>();
            Mock<ITenantConfigService> mockTenantConfigService = new Mock<ITenantConfigService>();
            Mock<ICustomerService> mockCustomerService = new Mock<ICustomerService>();
            Mock<ITwoFactorAuth> mockTwoFactorAuth = new Mock<ITwoFactorAuth>();
            Mock<IActionContextAccessor> mockActionContextAccessor = new Mock<IActionContextAccessor>();
            Mock<IOtpTracingService> mockOtpTracingService = new Mock<IOtpTracingService>();
            Mock<ITwoFaHelper> mockTwoFaHelper = new Mock<ITwoFaHelper>();

            //Arrange
            DbContextOptions<IdentityContext> options;
            var builder = new DbContextOptionsBuilder<IdentityContext>();
            builder.UseInMemoryDatabase("Identity");
            options = builder.Options;
            using IdentityContext dataContext = new IdentityContext(options);

            //TenantConfigContext
            DbContextOptions<TenantConfigContext> options1;
            var builder1 = new DbContextOptionsBuilder<TenantConfigContext>();
            builder1.UseInMemoryDatabase("TenantConfig");
            options1 = builder1.Options;
            using TenantConfigContext dataContext1 = new TenantConfigContext(options1);



            dataContext.Database.EnsureCreated();
            dataContext.SaveChanges();
            dataContext1.Database.EnsureCreated();
            Contact contact = new Contact
            {
                ContactEmailInfoes = new HashSet<ContactEmailInfo>
                {
                    new ContactEmailInfo
                    {
                        Email="abc@mail.com",
                        IsDeleted=false,
                        IsValid=false,
                        TenantId=1,
                        TypeId=(int)EmailType.Primary,
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                    }
                },
                FirstName = "abc",
                LastName = "xyz",
                TenantId = 1,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                Customer = new Customer
                {
                    IsActive = true,
                    TenantId = 1,
                    UserId = 1,
                    CreatedOnUtc = DateTime.UtcNow,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                }
            };

            dataContext1.SaveChanges();
            RegisterModel registerModel = new RegisterModel
            {
                FirstName = "abc",
                LastName = "xyz",
                Email = "abc@mail.com",
                Password = "test123456",
                Phone = "00000000000000"
            };
            mockTenantConfigService.Setup(x => x.GetTenant2FaConfigAsync(It.IsAny<int>(), It.IsAny<List<TwoFaConfigEntities>>())).ReturnsAsync(new TwoFaConfig { BorrowerTwoFaModeId = 1 });
            //   mockCustomerAccountService.Setup(x => x.DoesCustomerAccountExist(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(true);
            var service = new CustomerAccountService(new UnitOfWork<IdentityContext>(dataContext, new RepositoryProvider(new RepositoryFactories())),
                mockServiceProvider.Object, new UnitOfWork<TenantConfigContext>(dataContext1, new RepositoryProvider(new RepositoryFactories())), mockLoggerService.Object, mockTokenService.Object
                , mockTenantConfigService.Object, mockCustomerService.Object, mockTwoFactorAuth.Object, mockActionContextAccessor.Object
               , mockOtpTracingService.Object, mockTwoFaHelper.Object, null, null);
            //Act
            var result = await service.Register(registerModel, 1, true);
            //Assert
            Assert.NotNull(result);

        }
        [Fact]
        public async Task TestChangePasswordService()

        {
            Mock<ICustomerAccountService> mockCustomerAccountService = new Mock<ICustomerAccountService>();
            Mock<IServiceProvider> mockServiceProvider = new Mock<IServiceProvider>();
            Mock<ILogger<CustomerAccountService>> mockLoggerService = new Mock<ILogger<CustomerAccountService>>();
            Mock<ITokenService> mockTokenService = new Mock<ITokenService>();
            Mock<ITenantConfigService> mockTenantConfigService = new Mock<ITenantConfigService>();
            Mock<ICustomerService> mockCustomerService = new Mock<ICustomerService>();
            Mock<ITwoFactorAuth> mockTwoFactorAuth = new Mock<ITwoFactorAuth>();
            Mock<IActionContextAccessor> mockActionContextAccessor = new Mock<IActionContextAccessor>();
            Mock<IOtpTracingService> mockOtpTracingService = new Mock<IOtpTracingService>();
            Mock<ITwoFaHelper> mockTwoFaHelper = new Mock<ITwoFaHelper>();

            //Arrange
            DbContextOptions<IdentityContext> options;
            var builder = new DbContextOptionsBuilder<IdentityContext>();
            builder.UseInMemoryDatabase("Identity1");
            options = builder.Options;
            using IdentityContext dataContext = new IdentityContext(options);
            dataContext.Database.EnsureCreated();
            User user = new User
            {
                Id = 20011,
                TenantId = 1,
                IsActive = true,
                UserTypeId = 1,
                Password = "20-5C-3B-C1-73-4F-69-EC-CF-5C-F2-DC-EE-6D-BD-82-D3-4A-1D-00-A6-A4-9F-82-7A-88-C9-DC-AA-5D-F8-E9",
                PasswordFormatId = 3,
                PasswordSalt = "13"
            };
            dataContext.Set<User>().Add(user);
            dataContext.SaveChanges();

            var service = new CustomerAccountService(new UnitOfWork<IdentityContext>(dataContext, new RepositoryProvider(new RepositoryFactories())),
                mockServiceProvider.Object, null, mockLoggerService.Object, mockTokenService.Object
                , mockTenantConfigService.Object, mockCustomerService.Object, mockTwoFactorAuth.Object, mockActionContextAccessor.Object
               , mockOtpTracingService.Object, mockTwoFaHelper.Object, null, null);
            //Act
            var result = await service.ChangePassword(20011, "test123456", "test1234563", 1);

            //Assert
            Assert.NotNull(result);

            var res1 = Assert.IsType<ApiResponse>(result);
            Assert.Equal("200", res1.Code);
            Assert.Equal("", res1.Message);
        }

        [Fact]
        public async Task TestChangePasswordIsSameService()

        {
            Mock<ICustomerAccountService> mockCustomerAccountService = new Mock<ICustomerAccountService>();
            Mock<IServiceProvider> mockServiceProvider = new Mock<IServiceProvider>();
            Mock<ILogger<CustomerAccountService>> mockLoggerService = new Mock<ILogger<CustomerAccountService>>();
            Mock<ITokenService> mockTokenService = new Mock<ITokenService>();
            Mock<ITenantConfigService> mockTenantConfigService = new Mock<ITenantConfigService>();
            Mock<ICustomerService> mockCustomerService = new Mock<ICustomerService>();
            Mock<ITwoFactorAuth> mockTwoFactorAuth = new Mock<ITwoFactorAuth>();
            Mock<IActionContextAccessor> mockActionContextAccessor = new Mock<IActionContextAccessor>();
            Mock<IOtpTracingService> mockOtpTracingService = new Mock<IOtpTracingService>();
            Mock<ITwoFaHelper> mockTwoFaHelper = new Mock<ITwoFaHelper>();

            //Arrange
            DbContextOptions<IdentityContext> options;
            var builder = new DbContextOptionsBuilder<IdentityContext>();
            builder.UseInMemoryDatabase("Identity");
            options = builder.Options;
            using IdentityContext dataContext = new IdentityContext(options);


            dataContext.Database.EnsureCreated();

            User user = new User
            {
                Id = 1022,
                TenantId = 1,
                IsActive = true,
                UserTypeId = 1,
                Password = "10-5C-3B-C1-73-4F-69-EC-CF-5C-F2-DC-EE-6D-BD-82-D3-4A-1D-00-A6-A4-9F-82-7A-88-C9-DC-AA-5D-F8-E9",
                PasswordFormatId = 3,
                PasswordSalt = "13"
            };
            dataContext.Set<User>().Add(user);

            dataContext.SaveChanges();

            var service = new CustomerAccountService(new UnitOfWork<IdentityContext>(dataContext, new RepositoryProvider(new RepositoryFactories())),
                mockServiceProvider.Object, null, mockLoggerService.Object, mockTokenService.Object
                , mockTenantConfigService.Object, mockCustomerService.Object, mockTwoFactorAuth.Object, mockActionContextAccessor.Object
               , mockOtpTracingService.Object, mockTwoFaHelper.Object, null, null);
            //Act
            var result = await service.ChangePassword(2, "test123456", "test123456", 1);

            //Assert
            Assert.NotNull(result);

            var res1 = Assert.IsType<ApiResponse>(result);
            Assert.Equal("400", res1.Code);
            Assert.Equal("User doesn't exist", res1.Message);

        }
        [Fact]
        public async Task TestForgotPasswordRequestService()

        {
            Mock<ICustomerAccountService> mockCustomerAccountService = new Mock<ICustomerAccountService>();
            Mock<IServiceProvider> mockServiceProvider = new Mock<IServiceProvider>();
            Mock<ILogger<CustomerAccountService>> mockLoggerService = new Mock<ILogger<CustomerAccountService>>();
            Mock<ITokenService> mockTokenService = new Mock<ITokenService>();
            Mock<ITenantConfigService> mockTenantConfigService = new Mock<ITenantConfigService>();
            Mock<ICustomerService> mockCustomerService = new Mock<ICustomerService>();
            Mock<ITwoFactorAuth> mockTwoFactorAuth = new Mock<ITwoFactorAuth>();
            Mock<IActionContextAccessor> mockActionContextAccessor = new Mock<IActionContextAccessor>();
            Mock<IOtpTracingService> mockOtpTracingService = new Mock<IOtpTracingService>();
            Mock<ITwoFaHelper> mockTwoFaHelper = new Mock<ITwoFaHelper>();

            //Arrange
            DbContextOptions<IdentityContext> options;
            var builder = new DbContextOptionsBuilder<IdentityContext>();
            builder.UseInMemoryDatabase("Identity");
            options = builder.Options;
            using IdentityContext dataContext = new IdentityContext(options);
            //TenantConfigContext
            DbContextOptions<TenantConfigContext> options1;
            var builder1 = new DbContextOptionsBuilder<TenantConfigContext>();
            builder1.UseInMemoryDatabase("TenantConfig");
            options1 = builder1.Options;
            using TenantConfigContext dataContext1 = new TenantConfigContext(options1);
            dataContext1.Database.EnsureCreated();
            Branch branch = new Branch
            {
                Id = 1,
                AddressInfoId=1

            };
            dataContext1.Set<Branch>().Add(branch);
            BranchEmailBinder branchEmailBinders = new BranchEmailBinder
            {
                Id = 1,
                BranchId = 1,
                TypeId = 3,
                EmailAccount = new EmailAccount
                {
                    Id = 1,
                    IsActive = true,
                    Email = "ABC@MAIL.COM"
                }
            };
            dataContext1.Set<BranchEmailBinder>().Add(branchEmailBinders);
            AddressInfo addressInfo = new AddressInfo
            {
                Id = 1,
                TenantId=1,
              

            };
            dataContext1.Set<AddressInfo>().Add(addressInfo);

            BranchPhoneBinder branchPhoneBinders = new BranchPhoneBinder
            {
                Id = 1,
                BranchId = 1,
                CompanyPhoneInfoId=1,
                TypeId=3
                

            };
            dataContext1.Set<BranchPhoneBinder>().Add(branchPhoneBinders);
            CompanyPhoneInfo companyPhoneInfo = new CompanyPhoneInfo
            {
                Id = 1,
                IsActive=true,
                Phone="0323222222"
            };
            dataContext1.Set<CompanyPhoneInfo>().Add(companyPhoneInfo);

            dataContext1.SaveChanges();


            dataContext.Database.EnsureCreated();
            User user = new User
            {
                Id = 30,
                UserName = "abc@mail.com",
                TenantId = 1,
                IsActive = true,
                UserTypeId = 1,
                Password = "10-5C-3B-C1-73-4F-69-EC-CF-5C-F2-DC-EE-6D-BD-82-D3-4A-1D-00-A6-A4-9F-82-7A-88-C9-DC-AA-5D-F8-E9",
                PasswordFormatId = 3,
                PasswordSalt = "13"
            };
            dataContext.Set<User>().Add(user);
            PasswordPolicy passwordPolicy = new PasswordPolicy
            {
                Id = 10,
                TenantId = 1
            };
            dataContext.Set<PasswordPolicy>().Add(passwordPolicy);

            dataContext.SaveChanges();

            ForgotPasswordRequestModel forgotPasswordRequestModel = new ForgotPasswordRequestModel
            {
                Email = "abc@mail.com"
            };
            TenantModel tenantModel = new TenantModel
            {
                Id = 1,
                Code = "200",
                Branches = new List<BranchModel>
                {
                    new BranchModel
                    {
                        Id=1,
                        Code="3200",
                       
                        LoanOfficers= new List<LoanOfficerModel>
                        {
                            new LoanOfficerModel
                        {
                            Id=11,
                            Code="200"
                        } 
                       } 
                    },
                  
                },
               
                Urls = new List<UrlModel>
                {
                    new UrlModel
                    {
                        Url=""
                    }

                }
            };
            var service = new CustomerAccountService(new UnitOfWork<IdentityContext>(dataContext, new RepositoryProvider(new RepositoryFactories())),
             mockServiceProvider.Object, new UnitOfWork<TenantConfigContext>(dataContext1, new RepositoryProvider(new RepositoryFactories())), mockLoggerService.Object, mockTokenService.Object
             , mockTenantConfigService.Object, mockCustomerService.Object, mockTwoFactorAuth.Object, mockActionContextAccessor.Object
            , mockOtpTracingService.Object, mockTwoFaHelper.Object, null, null);
            //Act
            var result = await service.ForgotPasswordRequest(forgotPasswordRequestModel, tenantModel);

            //Assert
            Assert.NotNull(result);

            var res1 = Assert.IsType<ApiResponse>(result);
            Assert.Equal("200", res1.Code);

        }
        [Fact]
        public async Task TestForgotPasswordResponseUserIsNullService()

        {
            Mock<ICustomerAccountService> mockCustomerAccountService = new Mock<ICustomerAccountService>();
            Mock<IServiceProvider> mockServiceProvider = new Mock<IServiceProvider>();
            Mock<ILogger<CustomerAccountService>> mockLoggerService = new Mock<ILogger<CustomerAccountService>>();
            Mock<ITokenService> mockTokenService = new Mock<ITokenService>();
            Mock<ITenantConfigService> mockTenantConfigService = new Mock<ITenantConfigService>();
            Mock<ICustomerService> mockCustomerService = new Mock<ICustomerService>();
            Mock<ITwoFactorAuth> mockTwoFactorAuth = new Mock<ITwoFactorAuth>();
            Mock<IActionContextAccessor> mockActionContextAccessor = new Mock<IActionContextAccessor>();
            Mock<IOtpTracingService> mockOtpTracingService = new Mock<IOtpTracingService>();
            Mock<ITwoFaHelper> mockTwoFaHelper = new Mock<ITwoFaHelper>();

            //Arrange
            DbContextOptions<IdentityContext> options;
            var builder = new DbContextOptionsBuilder<IdentityContext>();
            builder.UseInMemoryDatabase("Identity");
            options = builder.Options;
            using IdentityContext dataContext = new IdentityContext(options);
            dataContext.Database.EnsureCreated();
            dataContext.SaveChanges();
            ForgotPasswordResponseModel forgotPasswordResponse = new ForgotPasswordResponseModel
            {
                Password = "test128882828"
            };
            TenantModel tenantModel = new TenantModel
            {
                Code = "200"
            };
            var service = new CustomerAccountService(new UnitOfWork<IdentityContext>(dataContext, new RepositoryProvider(new RepositoryFactories())),
             mockServiceProvider.Object, null, mockLoggerService.Object, mockTokenService.Object
             , mockTenantConfigService.Object, mockCustomerService.Object, mockTwoFactorAuth.Object, mockActionContextAccessor.Object
            , mockOtpTracingService.Object, mockTwoFaHelper.Object, null, null);
            //Act
            var result = await service.ForgotPasswordResponse(forgotPasswordResponse, tenantModel);

            //Assert
            Assert.NotNull(result);

            var res1 = Assert.IsType<ApiResponse>(result);
            Assert.Equal("400", res1.Code);

        }
        [Fact]
        public async Task TestForgotPasswordResponseService()

        {
            Mock<ICustomerAccountService> mockCustomerAccountService = new Mock<ICustomerAccountService>();
            Mock<IServiceProvider> mockServiceProvider = new Mock<IServiceProvider>();
            Mock<ILogger<CustomerAccountService>> mockLoggerService = new Mock<ILogger<CustomerAccountService>>();
            Mock<ITokenService> mockTokenService = new Mock<ITokenService>();
            Mock<ITenantConfigService> mockTenantConfigService = new Mock<ITenantConfigService>();
            Mock<ICustomerService> mockCustomerService = new Mock<ICustomerService>();
            Mock<ITwoFactorAuth> mockTwoFactorAuth = new Mock<ITwoFactorAuth>();
            Mock<IActionContextAccessor> mockActionContextAccessor = new Mock<IActionContextAccessor>();
            Mock<IOtpTracingService> mockOtpTracingService = new Mock<IOtpTracingService>();
            Mock<ITwoFaHelper> mockTwoFaHelper = new Mock<ITwoFaHelper>();

            //Arrange
            DbContextOptions<IdentityContext> options;
            var builder = new DbContextOptionsBuilder<IdentityContext>();
            builder.UseInMemoryDatabase("IdentityTestForgotPasswordResponseService");
            options = builder.Options;
            using IdentityContext dataContext = new IdentityContext(options);

            dataContext.Database.EnsureCreated();

            UserResetPasswordKey userResetPasswordKey = new UserResetPasswordKey
            {
                IsActive = true,
                ExpireOnUtc = DateTime.Now.AddDays(1),
                PasswordKey = "abc123",
                TenantId = 1,
                UserId = 4
            };
            dataContext.Set<UserResetPasswordKey>().Add(userResetPasswordKey);
            User user = new User
            {
                Id = 4,
                UserName = "abc@mail.com",
                TenantId = 1,
                IsActive = true,
                UserTypeId = 1,
                Password = "10-5C-3B-C1-73-4F-69-EC-CF-5C-F2-DC-EE-6D-BD-82-D3-4A-1D-00-A6-A4-9F-82-7A-88-C9-DC-AA-5D-F8-E9",
                PasswordFormatId = 3,
                PasswordSalt = "13"
            };
            dataContext.Set<User>().Add(user);
            dataContext.SaveChanges();

            ForgotPasswordResponseModel forgotPasswordResponse = new ForgotPasswordResponseModel
            {
                UserId = 4,
                Password = "test128882828",
                Key = "abc123"

            };
            TenantModel tenantModel = new TenantModel
            {
                Id = 1,
                Code = "200"
            };
            var service = new CustomerAccountService(new UnitOfWork<IdentityContext>(dataContext, new RepositoryProvider(new RepositoryFactories())),
             mockServiceProvider.Object, null, mockLoggerService.Object, mockTokenService.Object
             , mockTenantConfigService.Object, mockCustomerService.Object, mockTwoFactorAuth.Object, mockActionContextAccessor.Object
            , mockOtpTracingService.Object, mockTwoFaHelper.Object, null, null);
            //Act
            var result = await service.ForgotPasswordResponse(forgotPasswordResponse, tenantModel);

            //Assert
            Assert.NotNull(result);

            var res1 = Assert.IsType<ApiResponse>(result);
            Assert.Equal("200", res1.Code);

        }
        [Fact]
        public async Task TestSignService()

        {
            Mock<ICustomerAccountService> mockCustomerAccountService = new Mock<ICustomerAccountService>();
            Mock<IServiceProvider> mockServiceProvider = new Mock<IServiceProvider>();
            Mock<ILogger<CustomerAccountService>> mockLoggerService = new Mock<ILogger<CustomerAccountService>>();
            Mock<ITokenService> mockTokenService = new Mock<ITokenService>();
            Mock<ITenantConfigService> mockTenantConfigService = new Mock<ITenantConfigService>();
            Mock<ICustomerService> mockCustomerService = new Mock<ICustomerService>();
            Mock<ITwoFactorAuth> mockTwoFactorAuth = new Mock<ITwoFactorAuth>();
            Mock<IActionContextAccessor> mockActionContextAccessor = new Mock<IActionContextAccessor>();
            Mock<IOtpTracingService> mockOtpTracingService = new Mock<IOtpTracingService>();
            Mock<ITwoFaHelper> mockTwoFaHelper = new Mock<ITwoFaHelper>();

            //Arrange
            DbContextOptions<IdentityContext> options;
            var builder = new DbContextOptionsBuilder<IdentityContext>();
            builder.UseInMemoryDatabase("Identity");
            options = builder.Options;
            using IdentityContext dataContext = new IdentityContext(options);

            dataContext.Database.EnsureCreated();
            User user = new User
            {
                Id = 5,
                UserName = "abc@mail.com",
                TenantId = 1,
                IsActive = true,
                UserTypeId = 1,
                Password = "10-5C-3B-C1-73-4F-69-EC-CF-5C-F2-DC-EE-6D-BD-82-D3-4A-1D-00-A6-A4-9F-82-7A-88-C9-DC-AA-5D-F8-E9",
                PasswordFormatId = 3,
                PasswordSalt = "13"
            };
            dataContext.Set<User>().Add(user);
            PasswordPolicy passwordPolicy = new PasswordPolicy
            {
                Id = 1,
                TenantId = 1,
                IncorrectPasswordCount = 1
            };
            dataContext.Set<PasswordPolicy>().Add(passwordPolicy);
            dataContext.SaveChanges();

            SigninModel signinModel = new SigninModel
            {
                Email = "abc@mail.com",
                IsDevMode = true,
                Password = "test1234"
            };
            TenantModel tenantModel = new TenantModel
            {
                Id = 1,
                Code = "200"
            };
            var service = new CustomerAccountService(new UnitOfWork<IdentityContext>(dataContext, new RepositoryProvider(new RepositoryFactories())),
             mockServiceProvider.Object, null, mockLoggerService.Object, mockTokenService.Object
             , mockTenantConfigService.Object, mockCustomerService.Object, mockTwoFactorAuth.Object, mockActionContextAccessor.Object
            , mockOtpTracingService.Object, mockTwoFaHelper.Object, null, null);
            //Act
            var result = await service.Signin(signinModel, tenantModel);

            //Assert
            Assert.NotNull(result);

            var res1 = Assert.IsType<ApiResponse>(result);
            Assert.Equal("400", res1.Code);

        }
        [Fact]
        public async Task TestSignFailedPasswordAttemptCountIsNotNullService()

        {
            Mock<ICustomerAccountService> mockCustomerAccountService = new Mock<ICustomerAccountService>();
            Mock<IServiceProvider> mockServiceProvider = new Mock<IServiceProvider>();
            Mock<ILogger<CustomerAccountService>> mockLoggerService = new Mock<ILogger<CustomerAccountService>>();
            Mock<ITokenService> mockTokenService = new Mock<ITokenService>();
            Mock<ITenantConfigService> mockTenantConfigService = new Mock<ITenantConfigService>();
            Mock<ICustomerService> mockCustomerService = new Mock<ICustomerService>();
            Mock<ITwoFactorAuth> mockTwoFactorAuth = new Mock<ITwoFactorAuth>();
            Mock<IActionContextAccessor> mockActionContextAccessor = new Mock<IActionContextAccessor>();
            Mock<IOtpTracingService> mockOtpTracingService = new Mock<IOtpTracingService>();
            Mock<ITwoFaHelper> mockTwoFaHelper = new Mock<ITwoFaHelper>();

            //Arrange
            DbContextOptions<IdentityContext> options;
            var builder = new DbContextOptionsBuilder<IdentityContext>();
            builder.UseInMemoryDatabase("Identity");
            options = builder.Options;
            using IdentityContext dataContext = new IdentityContext(options);

            dataContext.Database.EnsureCreated();
            User user = new User
            {
                Id = 50,
                UserName = "abc@mail.com",
                TenantId = 1,
                IsActive = true,
                UserTypeId = 1,
                Password = "10-5C-3B-C1-73-4F-69-EC-CF-5C-F2-DC-EE-6D-BD-82-D3-4A-1D-00-A6-A4-9F-82-7A-88-C9-DC-AA-5D-F8-E9",
                PasswordFormatId = 3,
                PasswordSalt = "13",
                FailedPasswordAttemptCount = 0
            };
            dataContext.Set<User>().Add(user);
            PasswordPolicy passwordPolicy = new PasswordPolicy
            {
                Id = 3,
                TenantId = 1,
                IncorrectPasswordCount = 1
            };
            dataContext.Set<PasswordPolicy>().Add(passwordPolicy);
            dataContext.SaveChanges();

            SigninModel signinModel = new SigninModel
            {
                Email = "abc@mail.com",
                IsDevMode = true,
                Password = "test1234"
            };
            TenantModel tenantModel = new TenantModel
            {
                Id = 1,
                Code = "200"
            };
            var service = new CustomerAccountService(new UnitOfWork<IdentityContext>(dataContext, new RepositoryProvider(new RepositoryFactories())),
             mockServiceProvider.Object, null, mockLoggerService.Object, mockTokenService.Object
             , mockTenantConfigService.Object, mockCustomerService.Object, mockTwoFactorAuth.Object, mockActionContextAccessor.Object
            , mockOtpTracingService.Object, mockTwoFaHelper.Object, null, null);
            //Act
            var result = await service.Signin(signinModel, tenantModel);

            //Assert
            Assert.NotNull(result);

            var res1 = Assert.IsType<ApiResponse>(result);
            Assert.Equal("400", res1.Code);

        }

        [Fact]
        public async Task TestSignTemporaryPasswordIsNotEqualIsLockedOutService()

        {
            Mock<ICustomerAccountService> mockCustomerAccountService = new Mock<ICustomerAccountService>();

            Mock<IServiceProvider> mockServiceProvider = new Mock<IServiceProvider>();
            Mock<ILogger<CustomerAccountService>> mockLoggerService = new Mock<ILogger<CustomerAccountService>>();
            Mock<ITokenService> mockTokenService = new Mock<ITokenService>();
            Mock<ITenantConfigService> mockTenantConfigService = new Mock<ITenantConfigService>();
            Mock<ICustomerService> mockCustomerService = new Mock<ICustomerService>();
            Mock<ITwoFactorAuth> mockTwoFactorAuth = new Mock<ITwoFactorAuth>();
            Mock<IActionContextAccessor> mockActionContextAccessor = new Mock<IActionContextAccessor>();
            Mock<IOtpTracingService> mockOtpTracingService = new Mock<IOtpTracingService>();
            Mock<ITwoFaHelper> mockTwoFaHelper = new Mock<ITwoFaHelper>();
            Mock<IRequestCookieCollection> mockRequestCookieCollection = new Mock<IRequestCookieCollection>();
            Mock<ITokenManager> mockTokenManager = new Mock<ITokenManager>();
            
            var httpContext = new DefaultHttpContext();
            //Arrange
            DbContextOptions<IdentityContext> options;
            var builder = new DbContextOptionsBuilder<IdentityContext>();
            builder.UseInMemoryDatabase("Identity");
            options = builder.Options;
            using IdentityContext dataContext = new IdentityContext(options);
            //TenantConfigContext
            DbContextOptions<TenantConfigContext> options1;
            var builder1 = new DbContextOptionsBuilder<TenantConfigContext>();
            builder1.UseInMemoryDatabase("TenantConfig");
            options1 = builder1.Options;
            using TenantConfigContext dataContext1 = new TenantConfigContext(options1);

            dataContext.Database.EnsureCreated();
            User user = new User
            {
                Id = 52,
                UserName = "abc1@mail.com",
                TenantId = 1,
                IsActive = true,
                UserTypeId = 1,
                Password = "89-60-C9-84-65-C9-6B-0C-64-EC-7A-12-89-EF-46-5A-5F-3D-7B-8D-FD-C3-C9-20-B8-93-BB-35-B5-E5-77-9E",
                PasswordFormatId = 3,
                PasswordSalt = "13",
                FailedPasswordAttemptCount = 0,
                IsLockedOut = true,
                LastLockedOutDateUtc = DateTime.UtcNow
            };
            dataContext.Set<User>().Add(user);
            PasswordPolicy passwordPolicy = new PasswordPolicy
            {
                Id = 30,
                TenantId = 1,
                IncorrectPasswordCount = 1
            };
            dataContext.Set<PasswordPolicy>().Add(passwordPolicy);

            dataContext.SaveChanges();

            dataContext1.Database.EnsureCreated();
            Contact contact = new Contact
            {
                ContactEmailInfoes = new HashSet<ContactEmailInfo>
                {
                    new ContactEmailInfo
                    {
                        Email="abc@mail.com",
                        IsDeleted=false,
                        IsValid=false,
                        TenantId=1,
                        TypeId=(int)EmailType.Primary,
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                    }
                },
                FirstName = "abc",
                LastName = "xyz",
                TenantId = 1,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                Customer = new Customer
                {
                    IsActive = true,
                    TenantId = 1,
                    UserId = 52,
                    CreatedOnUtc = DateTime.UtcNow,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                }
            };
            dataContext1.Set<Contact>().Add(contact);
            TwoFaConfig twoFaConfig = new TwoFaConfig
            {
                TenantId = 1,
                IsActive = true
            };
            dataContext1.Set<TwoFaConfig>().Add(twoFaConfig);

            dataContext1.SaveChanges();

            SigninModel signinModel = new SigninModel
            {
                Email = "abc1@mail.com",
                IsDevMode = false,
                Password = "test1234"
            };
            TenantModel tenantModel = new TenantModel
            {
                Id = 1,
                Code = "200",
                Branches = new List<BranchModel>
                {
                    new BranchModel
                    {
                        Code="200"
                    }
                }

            };



            ActionContext c = new ActionContext(httpContext, new Microsoft.AspNetCore.Routing.RouteData(), new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor());
            mockActionContextAccessor.Setup(x => x.ActionContext).Returns(c);
            mockTwoFaHelper.Setup(x => x.CreateCookieName(It.IsAny<string>(), It.IsAny<int>())).Returns("a");

            mockTenantConfigService.Setup(x => x.GetTenant2FaConfigAsync(It.IsAny<int>(), It.IsAny<List<TwoFaConfigEntities>>())).ReturnsAsync(new TwoFaConfig
            {
                TwilioVerifyServiceId = "a",
                BorrowerTwoFaModeId = 1
            }
            );
            mockCustomerAccountService.Setup(x => x.GenerateNewAccessToken(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(new ApiResponse
            {
                Code = "200"
            });

            Customer fakeCustomer = new Customer
            {
                UserId = 52,
                Contact = new Contact
                {
                    FirstName = "asas",
                    LastName = "sdsd"
                },
                Tenant = new Tenant
                {
                    Code = "200"
                }
            };

            var includeList = new List<CustomerRelatedEntities>()
            {
                CustomerRelatedEntities.ContactPhoneInfo
            };

            mockCustomerService.Setup(x => x.GetCustomerByUserIdAsync(52, tenantModel.Id, includeList)).ReturnsAsync(fakeCustomer);
            mockTokenService.Setup(x => x.GenerateAccessToken(It.IsAny<List<Claim>>())).ReturnsAsync(

            new JwtSecurityToken(
                                             issuer: "rainsoftfn",
                                             audience: "readers",
                                             expires: DateTime.Now.AddMinutes(value: Convert.ToDouble("360")),
                                             signingCredentials: new SigningCredentials(key: new SymmetricSecurityKey(key: Encoding.UTF8.GetBytes(s: "0Tj7rvpULEL2LVMD1hLujBlNMVMZdgLp/kU07Fto2VbfsrFts3rFU8sFRFOBzl+rhP7EZ09iWCkiVaZ3HO0oNHV65gRG/VioLhxwb9grZ3HQN+pG4CUX3pjpuXBO4YXxSNST3QFXK04TUHucN3X6okkF/7tAH4L11YJiHV8agLo=")),
                                       algorithm: SecurityAlgorithms.HmacSha256),
                                             claims: new List<Claim>()
                                            ));
            mockTokenService.Setup(x => x.Generate2FaTokenAsync(It.IsAny<List<Claim>>())).ReturnsAsync(new JwtSecurityToken(
                issuer: "rainsoftfn",
                audience: "readers",
                //expires: DateTime.Now.AddMinutes(value: Convert.ToDouble(_configuration["Token:TimeoutInMinutes"])),
                expires: DateTime.Now.AddMinutes(5),
                 signingCredentials: new SigningCredentials(key: new SymmetricSecurityKey(key: Encoding.UTF8.GetBytes(s: "0Tj7rvpULEL2LVMD1hLujBlNMVMZdgLp/kU07Fto2VbfsrFts3rFU8sFRFOBzl+rhP7EZ09iWCkiVaZ3HO0oNHV65gRG/VioLhxwb9grZ3HQN+pG4CUX3pjpuXBO4YXxSNST3QFXK04TUHucN3X6okkF/7tAH4L11YJiHV8agLo=")),
                                       algorithm: SecurityAlgorithms.HmacSha256),
                claims: new List<Claim>()
            ));

            mockTokenService.Setup(x => x.GenerateRefreshToken()).Returns("sdfsdfdsF");
         


            var service = new CustomerAccountService(new UnitOfWork<IdentityContext>(dataContext, new RepositoryProvider(new RepositoryFactories())),
                                                     mockServiceProvider.Object, new UnitOfWork<TenantConfigContext>(dataContext1, new RepositoryProvider(new RepositoryFactories())), mockLoggerService.Object, mockTokenService.Object
                                                     , mockTenantConfigService.Object, mockCustomerService.Object, mockTwoFactorAuth.Object, mockActionContextAccessor.Object
                                                     , mockOtpTracingService.Object, mockTwoFaHelper.Object, null, mockTokenManager.Object);
            //Act

            var result = await service.Signin(signinModel, tenantModel);

            //Assert
            Assert.NotNull(result);

            var res1 = Assert.IsType<ApiResponse>(result);
            Assert.Equal("200", res1.Code);

        }
    }
}
