using LoanApplication.API.Controllers;
using LoanApplication.Model;
using LoanApplication.Service;
using LoanApplicationDb.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TenantConfig.Common.DistributedCache;
using TenantConfig.Data;
using TenantConfig.Entity.Models;
using TenantConfig.Service;
using URF.Core.EF;
using URF.Core.EF.Factories;
using Xunit;
using BranchModel = TenantConfig.Model.BranchModel;
namespace LoanApplication.Tests
{
    public class LoanTest
    {
        [Fact]
        public async Task GetLoInfoControllerTest()
        {
            Mock<IInfoService> mockInfoService = new Mock<IInfoService>();
            Mock<ITermConditionService> mockTermConditionService = new Mock<ITermConditionService>();
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
            model.Branches = new List<TenantConfig.Common.DistributedCache.BranchModel>

            {
                new TenantConfig.Common.DistributedCache.BranchModel
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
            var controller = new LoanController(mockInfoService.Object,null);
            var httpContext = new DefaultHttpContext();
            httpContext.Items.Add(Constants.COLABA_TENANT, model);
            controller.ControllerContext = new ControllerContext(new ActionContext(httpContext, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));
            var result = await controller.GetLoInfo();
            //Assert
            Assert.NotNull(result);
            var res = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, res.StatusCode);
        }

        [Fact]
        public async Task GetBranchInfoControllerTest()
        {
            Mock<IInfoService> mockInfoService = new Mock<IInfoService>();
            Mock<ITermConditionService> mockTermConditionService = new Mock<ITermConditionService>();
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
            model.Branches = new List<TenantConfig.Common.DistributedCache.BranchModel>

            {
                new TenantConfig.Common.DistributedCache.BranchModel
                {
                    Code="texas",
                    Id=9,
                    IsCorporate=true,
                    LoanOfficers= new List<LoanOfficerModel>
                    {
                         
                    }

                }
            };
            var controller = new LoanController(mockInfoService.Object, null);
            var httpContext = new DefaultHttpContext();
            httpContext.Items.Add(Constants.COLABA_TENANT, model);
            controller.ControllerContext = new ControllerContext(new ActionContext(httpContext, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));
            var result = await controller.GetLoInfo();
            //Assert
            Assert.NotNull(result);
            var res = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, res.StatusCode);
        }


        [Fact]
        public async Task GetLoInfoServiceTest()
        {
            Mock<IServiceProvider> mockServiceProvider = new Mock<IServiceProvider>();
            Mock<IInfoService> mockInfoService = new Mock<IInfoService>();
            Mock<IDbFunctionService> mockDbFunctionService = new Mock<IDbFunctionService>();
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
            model.Branches = new List<TenantConfig.Common.DistributedCache.BranchModel>

            {
                new TenantConfig.Common.DistributedCache.BranchModel
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
            BranchModel branchModel = new BranchModel
            {
                Color = "abcd",
                Favicon = "abc",
                Footer = "bc",
                Logo = "abcd"
            };
            LoInfo loInfo = new LoInfo
            {
                Email = "abc@mail.com",
                Image = "abc.png",
                Name = "abc",
                Phone = "00000000000",
                Url = "http://test.com"
            };
            mockInfoService.Setup(x => x.GetLoInfo(It.IsAny<int>(), It.IsAny<TenantModel>())).ReturnsAsync(loInfo);

            //Arrange
            DbContextOptions<TenantConfigContext> options;
            var builder = new DbContextOptionsBuilder<TenantConfigContext>();
            builder.UseInMemoryDatabase("TenantConfig");
            options = builder.Options;
            using TenantConfigContext dataContext = new TenantConfigContext(options);
            dataContext.Database.EnsureCreated();

            Employee employee = new Employee
            {
                Id = 1,
                IsActive = true,
                UserId = 1,
                ContactId=1,
                Photo = "0000000000",
                Url="http://test.com"
            };
            dataContext.Set<Employee>().Add(employee);


            EmployeeEmailBinder employeeEmailBinders = new EmployeeEmailBinder
            {
                Id = 1,
                TypeId = 1,
                EmailAccountId = 1,
                EmployeeId = 1
            };
            dataContext.Set<EmployeeEmailBinder>().Add(employeeEmailBinders);

            EmailAccount emailAccount = new EmailAccount
            {
                Id = 1,
                Email = "ABC",
                TenantId=1

            };
            dataContext.Set<EmailAccount>().Add(emailAccount);


            EmployeePhoneBinder employeePhoneBinders = new EmployeePhoneBinder
            {
                Id = 1,
                EmployeeId = 1,
                CompanyPhoneInfoId = 1,
                TypeId=3
            };
            dataContext.Set<EmployeePhoneBinder>().Add(employeePhoneBinders);

            CompanyPhoneInfo companyPhoneInfo = new CompanyPhoneInfo
            {
                Id = 1,
                Phone="0000000000"  
            };
            dataContext.Set<CompanyPhoneInfo>().Add(companyPhoneInfo);

            Contact contact = new Contact
            {
                Id = 1,
                NickName = "shehroz"
            };
            dataContext.Set<Contact>().Add(contact);

            dataContext.SaveChanges();
            var service = new InfoService(new UnitOfWork<TenantConfigContext>(dataContext, new RepositoryProvider(new RepositoryFactories())),null, mockServiceProvider.Object, mockDbFunctionService.Object);
            var result = await service.GetLoInfo(1, model);
            //Assert
            Assert.NotNull(result);
            var res = Assert.IsType<LoInfo>(result);
            Assert.Equal("(000) 000-0000", res.Phone);
        }
        [Fact]
        public async Task GetBranchInfoServiceTest()
        {
            TenantModel model = new TenantModel();
            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();
            model.Id = 9;
            model.Code = "33";
            model.Urls = new List<UrlModel>
            {  new UrlModel
            {
                Type=TenantUrlType.Customer,
                Url="apply.lendova9.com:9003"
            }
            };
            model.Branches = new List<TenantConfig.Common.DistributedCache.BranchModel>

            {
                new TenantConfig.Common.DistributedCache.BranchModel
                {
                    Code="texas",
                    Id=900,
                    IsCorporate=true,
                    LoanOfficers= new List<LoanOfficerModel>
                    {
                         new LoanOfficerModel
                         {
                             Id=900,
                             Code="33"
                         }
                    }

                }
            };

            //Arrange
            DbContextOptions<TenantConfigContext> options;
            var builder = new DbContextOptionsBuilder<TenantConfigContext>();
            builder.UseInMemoryDatabase("TenantConfig");
            options = builder.Options;
            using TenantConfigContext dataContext = new TenantConfigContext(options);
            dataContext.Database.EnsureCreated();

            Branch branch = new Branch
            {
                Id = 900,
                IsActive = true,
                Url = "http://test.com"
            };
            dataContext.Set<Branch>().Add(branch);


            BranchEmailBinder branchEmailBinders = new BranchEmailBinder
            {
                Id = 900,
                TypeId = 1,
                EmailAccountId = 900,
                BranchId = 900
            };
            dataContext.Set<BranchEmailBinder>().Add(branchEmailBinders);

            EmailAccount emailAccount = new EmailAccount
            {
                Id = 900,
                Email = "ABC",
                TenantId = 9

            };
            dataContext.Set<EmailAccount>().Add(emailAccount);


            BranchPhoneBinder branchPhoneBinders = new BranchPhoneBinder
            {
                Id = 900,
                BranchId = 900,
                CompanyPhoneInfoId = 900,
                TypeId = 3
            };
            dataContext.Set<BranchPhoneBinder>().Add(branchPhoneBinders);

            CompanyPhoneInfo companyPhoneInfo = new CompanyPhoneInfo
            {
                Id = 900,
                Phone = "0000000000"
            };
            dataContext.Set<CompanyPhoneInfo>().Add(companyPhoneInfo);

            dataContext.SaveChanges();
            var service = new InfoService(new UnitOfWork<TenantConfigContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, Mock.Of<IServiceProvider>(), dbFunctionServiceMock.Object);
            var result = await service.GetBranchInfo(900, model);
            //Assert
            Assert.NotNull(result);
            var res = Assert.IsType<LoInfo>(result);
            Assert.Equal("(000) 000-0000", res.Phone);
        }

        [Fact]
        public async Task GetAllMaritalStatusControllerTest()
        {
            Mock<IInfoService> mockInfoService = new Mock<IInfoService>();
            mockInfoService.Setup(x => x.GetAllMaritalStatus(It.IsAny<TenantModel>())).Returns(new List<MaritalStatusModel> { new MaritalStatusModel { Id=1} });
            var controller = new LoanController(mockInfoService.Object,null);
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
            model.Branches = new List<TenantConfig.Common.DistributedCache.BranchModel>

            {
                new TenantConfig.Common.DistributedCache.BranchModel
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
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserId")).Returns(new Claim("UserId", "1"));
            Dictionary<object, object> keyValuePairs = new Dictionary<object, object>();
            keyValuePairs.Add(Constants.COLABA_TENANT, model);
            httpContext.Setup(x => x.Items).Returns(keyValuePairs);
            controller.ControllerContext = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));
            var result = await controller.GetAllMaritalStatus();
            //Assert
            Assert.NotNull(result);
            var res = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, res.StatusCode);
            var val = Assert.IsType<List<MaritalStatusModel>>(res.Value);
            Assert.Equal(1,val[0].Id);
        }
        [Fact]
        public async Task GetPendingLoanApplicationControllerTest()
        {
            Mock<ILoanService> mockInfoService = new Mock<ILoanService>();
            mockInfoService.Setup(x => x.GetPendingLoanApplication(It.IsAny<TenantModel>(), It.IsAny<int>(), It.IsAny<int?>())).ReturnsAsync(new PendingLoanApplicationModel { LoanApplicationId=200});
            var controller = new LoanController(null,mockInfoService.Object);
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
            model.Branches = new List<TenantConfig.Common.DistributedCache.BranchModel>

            {
                new TenantConfig.Common.DistributedCache.BranchModel
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
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserId")).Returns(new Claim("UserId", "1"));
            Dictionary<object, object> keyValuePairs = new Dictionary<object, object>();
            keyValuePairs.Add(Constants.COLABA_TENANT, model);
            httpContext.Setup(x => x.Items).Returns(keyValuePairs);
            controller.ControllerContext = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));
            var result = await controller.GetPendingLoanApplication(1);
            //Assert
            Assert.NotNull(result);
            var res = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, res.StatusCode);
            var val = Assert.IsType<PendingLoanApplicationModel>(res.Value);
            Assert.Equal(200, val.LoanApplicationId);
        }
        [Fact]
        public async Task GetPendingLoanApplicationService()
        {
            TenantModel model = new TenantModel();
            model.Id = 1;
            model.Code = "33";
            model.Urls = new List<UrlModel>
            {  new UrlModel
            {
                Type=TenantUrlType.Customer,
                Url="apply.lendova9.com:9003"
            }
            };
            model.Branches = new List<TenantConfig.Common.DistributedCache.BranchModel>

            {
                new TenantConfig.Common.DistributedCache.BranchModel
                {
                    Code="texas",
                    Id=1,
                    IsCorporate=true,
                    LoanOfficers= new List<LoanOfficerModel>
                    {
                         new LoanOfficerModel
                         {
                             Id=1,
                             Code="33"
                         }
                    }

                }
            };

            //Arrange
            DbContextOptions<LoanApplicationContext> options;
            var builder = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder.UseInMemoryDatabase("LoanApplicationGetPendingLoanApplicationService");
            options = builder.Options;
            using LoanApplicationContext dataContext = new LoanApplicationContext(options);
            dataContext.Database.EnsureCreated();

            LoanApplicationDb.Entity.Models.LoanApplication loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication
            {
                Id=90000,
                MilestoneId=1,
                IsActive=true,
                UserId=1,
                TenantId=1
            };
            dataContext.Set<LoanApplicationDb.Entity.Models.LoanApplication>().Add(loanApplication);
            LoanApplicationDb.Entity.Models.Config config = new LoanApplicationDb.Entity.Models.Config
            {
                Id=90000,
                Name="Test"
            };
            dataContext.Set<LoanApplicationDb.Entity.Models.Config>().Add(config);
            dataContext.SaveChanges();

            var service = new LoanService(new UnitOfWork<LoanApplicationContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, null);
            var res = await service.GetPendingLoanApplication(model,1,90000);
            Assert.Equal(90000,res.LoanApplicationId);
        }
        [Fact]
        public async Task GetPendingLoanApplicationServiceNew()
        {
            TenantModel model = new TenantModel();
            model.Id = 1;
            model.Code = "33";
            model.Urls = new List<UrlModel>
            {  new UrlModel
            {
                Type=TenantUrlType.Customer,
                Url="apply.lendova9.com:9003"
            }
            };
            model.Branches = new List<TenantConfig.Common.DistributedCache.BranchModel>

            {
                new TenantConfig.Common.DistributedCache.BranchModel
                {
                    Code="texas",
                    Id=1,
                    IsCorporate=true,
                    LoanOfficers= new List<LoanOfficerModel>
                    {
                         new LoanOfficerModel
                         {
                             Id=1,
                             Code="33"
                         }
                    }

                }
            };

            //Arrange
            DbContextOptions<LoanApplicationContext> options;
            var builder = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder.UseInMemoryDatabase("LoanApplicationGetPendingLoanApplicationServiceNew");
            options = builder.Options;
            using LoanApplicationContext dataContext = new LoanApplicationContext(options);
            dataContext.Database.EnsureCreated();

            LoanApplicationDb.Entity.Models.LoanApplication loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication
            {
                Id = 6001,
                MilestoneId = 1,
                IsActive = true,
                UserId = 1,
                TenantId = 1,
                SettingHash="123"
            };
            dataContext.Set<LoanApplicationDb.Entity.Models.LoanApplication>().Add(loanApplication);
            LoanApplicationDb.Entity.Models.Config config = new LoanApplicationDb.Entity.Models.Config
            {
                Id = 1,
                Name = "Test"
            };
            dataContext.Set<LoanApplicationDb.Entity.Models.Config>().Add(config);
            dataContext.SaveChanges();

            var service = new LoanService(new UnitOfWork<LoanApplicationContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, null);
            var res = await service.GetPendingLoanApplication(model, 1, null);
            Assert.Equal(6001, res.LoanApplicationId);
        }

        [Fact]
        public async Task TestGetDashboardLoanInfo()
        {
            // Arrange
            Mock<IInfoService> infoServiceMock = new Mock<IInfoService>();
            Mock<ILoanService> loanServerMock = new Mock<ILoanService>();

            TenantModel contextTenant = new TenantModel()
            {
                Branches = new List<TenantConfig.Common.DistributedCache.BranchModel>()
                {
                    new TenantConfig.Common.DistributedCache.BranchModel()
                    {
                        Code = "someBranchCodeByZain", Id = 1, IsCorporate = true, LoanOfficers = new List<LoanOfficerModel>()
                    }
                }
            };


            int fakeUserId = 1;

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("UserId", fakeUserId.ToString()),
            }, "mock"));

            var contextMock = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = user } };
            contextMock.HttpContext.Items = new ConcurrentDictionary<object, object>();
            contextMock.HttpContext.Items[Constants.COLABA_TENANT] = contextTenant;

            LoanApplicationIdModel fakeApplicationIdModel = new LoanApplicationIdModel()
            {
                LoanApplicationId = 1
            };
            List<LoanSummary> fakeModelToreturn = new List<LoanSummary>();

            loanServerMock
                .Setup(x => x.GetDashboardLoanInfo(contextTenant, fakeUserId)).ReturnsAsync(fakeModelToreturn);

            var controller = new LoanController(infoServiceMock.Object, loanServerMock.Object);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;

            // Act

            var results = await controller.GetDashboardLoanInfo();


            // Assert

            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }


        //[Fact]
        //public async Task TestGetReviewBorrowerInfoSection()
        //{
        //    // Arrange
        //    Mock<IInfoService> infoServiceMock = new Mock<IInfoService>();
        //    Mock<ILoanService> loanServerMock = new Mock<ILoanService>();

        //    TenantModel contextTenant = new TenantModel()
        //    {
        //        Branches = new List<TenantConfig.Common.DistributedCache.BranchModel>()
        //        {
        //            new TenantConfig.Common.DistributedCache.BranchModel()
        //            {
        //                Code = "someBranchCodeByZain", Id = 1, IsCorporate = true, LoanOfficers = new List<LoanOfficerModel>()
        //            }
        //        }
        //    };


        //    int fakeUserId = 1;

        //    var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        //    {
        //        new Claim("UserId", fakeUserId.ToString()),
        //    }, "mock"));

        //    var contextMock = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = user } };
        //    contextMock.HttpContext.Items = new ConcurrentDictionary<object, object>();
        //    contextMock.HttpContext.Items[Constants.COLABA_TENANT] = contextTenant;

        //    LoanApplicationIdModel fakeApplicationIdModel = new LoanApplicationIdModel()
        //    {
        //        LoanApplicationId = 1
        //    };
        //    LoanApplicationReview fakeModelToReturn = new LoanApplicationReview();

        //    loanServerMock
        //        .Setup(x => x.GetLoanApplicationForBorrowersInfoSectionReview(contextTenant, fakeUserId,
        //            fakeApplicationIdModel.LoanApplicationId)).ReturnsAsync(fakeModelToReturn);

        //    var controller = new LoanController(infoServiceMock.Object, loanServerMock.Object);
        //    controller.ControllerContext.HttpContext = contextMock.HttpContext;

        //    // Act

        //    var results = await controller.GetReviewBorrowerInfoSection(fakeApplicationIdModel);


        //    // Assert

        //    Assert.NotNull(results);
        //    Assert.IsType<OkObjectResult>(results);
        //}

        [Fact]
        public async Task TestGetLoanApplicationSecondReview()
        {
            // Arrange
            Mock<IInfoService> infoServiceMock = new Mock<IInfoService>();
            Mock<ILoanService> loanServerMock = new Mock<ILoanService>();

            TenantModel contextTenant = new TenantModel()
            {
                Branches = new List<TenantConfig.Common.DistributedCache.BranchModel>()
                {
                    new TenantConfig.Common.DistributedCache.BranchModel()
                    {
                        Code = "someBranchCodeByZain", Id = 1, IsCorporate = true, LoanOfficers = new List<LoanOfficerModel>()
                    }
                }
            };


            int fakeUserId = 1;

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("UserId", fakeUserId.ToString()),
            }, "mock"));

            var contextMock = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = user } };
            contextMock.HttpContext.Items = new ConcurrentDictionary<object, object>();
            contextMock.HttpContext.Items[Constants.COLABA_TENANT] = contextTenant;

            LoanApplicationIdModel fakeApplicationIdModel = new LoanApplicationIdModel()
            {
                LoanApplicationId = 1
            };
            LoanApplicationFirstReview fakeModelToReturn = new LoanApplicationFirstReview();

            loanServerMock
                .Setup(x => x.GetLoanApplicationForFirstReview(contextTenant, fakeUserId,
                    fakeApplicationIdModel.LoanApplicationId)).ReturnsAsync(fakeModelToReturn);

            var controller = new LoanController(infoServiceMock.Object, loanServerMock.Object);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;

            // Act

            var results = await controller.GetLoanApplicationSecondReview(fakeApplicationIdModel);


            // Assert

            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }

         [Fact]
        public async Task TestUpdateState()
        {
            // Arrange
            Mock<IInfoService> infoServiceMock = new Mock<IInfoService>();
            Mock<ILoanService> loanServerMock = new Mock<ILoanService>();

            TenantModel contextTenant = new TenantModel()
            {
                Branches = new List<TenantConfig.Common.DistributedCache.BranchModel>()
                {
                    new TenantConfig.Common.DistributedCache.BranchModel()
                    {
                        Code = "someBranchCodeByZain", Id = 1, IsCorporate = true, LoanOfficers = new List<LoanOfficerModel>()
                    }
                }
            };


            int fakeUserId = 1;

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("UserId", fakeUserId.ToString()),
            }, "mock"));

            var contextMock = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = user } };
            contextMock.HttpContext.Items = new ConcurrentDictionary<object, object>();
            contextMock.HttpContext.Items[Constants.COLABA_TENANT] = contextTenant;

            UpdateStateModel fakeStateModel = new UpdateStateModel()
            {
                LoanApplicationId=1,
                State="fake state"
            };
            //LoanApplicationFirstReview fakeModelToReturn = new LoanApplicationFirstReview();
            int fakeReturnInt = 0;
            loanServerMock
                .Setup(x => x.UpdateState(contextTenant, fakeUserId,
                    fakeStateModel)).ReturnsAsync(fakeReturnInt);

            var controller = new LoanController(infoServiceMock.Object, loanServerMock.Object);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;

            // Act

            var results = await controller.UpdateState(fakeStateModel);


            // Assert

            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }


        [Fact]
        public async Task SubmitLoanApplication()
        {
            // Arrange
            Mock<IInfoService> infoServiceMock = new Mock<IInfoService>();
            Mock<ILoanService> loanServerMock = new Mock<ILoanService>();

            TenantModel contextTenant = new TenantModel()
            {
                Branches = new List<TenantConfig.Common.DistributedCache.BranchModel>()
                {
                    new TenantConfig.Common.DistributedCache.BranchModel()
                    {
                        Code = "someBranchCodeByZain", Id = 1, IsCorporate = true, LoanOfficers = new List<LoanOfficerModel>()
                    }
                }
            };


            int fakeUserId = 1;

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("UserId", fakeUserId.ToString()),
            }, "mock"));

            var contextMock = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = user } };
            contextMock.HttpContext.Items = new ConcurrentDictionary<object, object>();
            contextMock.HttpContext.Items[Constants.COLABA_TENANT] = contextTenant;

            LoanCommentsModel fakecommentsModel = new LoanCommentsModel()
            {
                LoanApplicationId = 1,
                State = "fake comments"
            };
            
            int fakeReturnInt = 0;
            loanServerMock
                .Setup(x => x.SubmitLoanApplication(contextTenant, fakeUserId,
                    fakecommentsModel)).ReturnsAsync(fakeReturnInt);

            var controller = new LoanController(infoServiceMock.Object, loanServerMock.Object);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;

            // Act

            var results = await controller.SubmitLoanApplication(fakecommentsModel);


            // Assert

            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }


        [Fact]
        public async Task TestGetLoanApplicationFirstReview()
        {
            // Arrange
            Mock<IInfoService> infoServiceMock = new Mock<IInfoService>();
            Mock<ILoanService> loanServerMock = new Mock<ILoanService>();

            TenantModel contextTenant = new TenantModel()
            {
                Branches = new List<TenantConfig.Common.DistributedCache.BranchModel>()
                {
                    new TenantConfig.Common.DistributedCache.BranchModel()
                    {
                        Code = "someBranchCodeByZain", Id = 1, IsCorporate = true, LoanOfficers = new List<LoanOfficerModel>()
                    }
                }
            };


            int fakeUserId = 1;

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("UserId", fakeUserId.ToString()),
            }, "mock"));

            var contextMock = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = user } };
            contextMock.HttpContext.Items = new ConcurrentDictionary<object, object>();
            contextMock.HttpContext.Items[Constants.COLABA_TENANT] = contextTenant;

            LoanApplicationIdModel fakeLoanApplicationIdModel = new LoanApplicationIdModel()
            {
                LoanApplicationId = 1,
               
            };
            LoanApplicationFirstReview fakeModelToReturn = new LoanApplicationFirstReview();
           // int fakeReturnInt = 0;
            loanServerMock
                .Setup(x => x.GetLoanApplicationForFirstReview(contextTenant, fakeUserId,
                    fakeLoanApplicationIdModel.LoanApplicationId)).ReturnsAsync(fakeModelToReturn);

            var controller = new LoanController(infoServiceMock.Object, loanServerMock.Object);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;

            // Act

            var results = await controller.GetLoanApplicationFirstReview(fakeLoanApplicationIdModel);


            // Assert

            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }
        


    }
}
