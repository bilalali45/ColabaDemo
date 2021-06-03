using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter;
using LoanApplication.API.Controllers;
using LoanApplication.Model;
using LoanApplication.Service;
using LoanApplicationDb.Data;
using LoanApplicationDb.Entity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using TenantConfig.Common.DistributedCache;
using TenantConfig.Data;
using TenantConfig.Entity.Models;
using URF.Core.EF;
using URF.Core.EF.Factories;
using Xunit;

namespace LoanApplication.Tests
{
    public partial class IncomeTest
    {

        [Fact]
        public async Task TestGetRetirementIncomeTypes()
        {
            // Arrange
            Mock<IInfoService> infoServiceMock = new Mock<IInfoService>();

            TenantModel contextTenant = new TenantModel()
            {
                Branches = new List<BranchModel>()
                {
                    new BranchModel()
                    {
                        Code = "someBranchCode", Id = 1, IsCorporate = true, LoanOfficers = new List<LoanOfficerModel>()
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

            // Act
            var controller = new IncomeController(null, infoServiceMock.Object, Mock.Of<IEmploymentService>());
            controller.ControllerContext.HttpContext = contextMock.HttpContext;
            var results = await controller.GetRetirementIncomeTypes();

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }

        [Fact]
        public async Task TestGetRetirementIncomeInfo()
        {
            // Arrange
            Mock<IIncomeService> incomeServiceMock = new Mock<IIncomeService>();

            TenantModel contextTenant = new TenantModel()
            {
                Branches = new List<BranchModel>()
                {
                    new BranchModel()
                    {
                        Code = "someBranchCode", Id = 1, IsCorporate = true, LoanOfficers = new List<LoanOfficerModel>()
                    }
                }
            };

            int fakeUserId = 1;
            int incomeInfoId = 5;
            int borrowerId = 4002;

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("UserId", fakeUserId.ToString()),
            }, "mock"));

            var contextMock = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = user } };
            contextMock.HttpContext.Items = new ConcurrentDictionary<object, object>();
            contextMock.HttpContext.Items[Constants.COLABA_TENANT] = contextTenant;

            // Act
            var controller = new IncomeController(incomeServiceMock.Object, null, null);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;
            var results = await controller.GetRetirementIncomeInfo(incomeInfoId, borrowerId);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }

        [Fact]
        public async Task TestAddOrUpdateRetirementIncomeInfo()
        {
            // Arrange
            Mock<IIncomeService> incomeServiceMock = new Mock<IIncomeService>();
            TenantModel contextTenant = new TenantModel()
            {
                Branches = new List<BranchModel>()
                {
                    new BranchModel()
                    {
                        Code = "someBranchCode", Id = 1, IsCorporate = true, LoanOfficers = new List<LoanOfficerModel>()
                    }
                }
            };

            int fakeUserId = 1;
            int fakeBorrowerId = 1;
            int fakeLoanApplicationId = 1;

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("UserId", fakeUserId.ToString()),
            }, "mock"));

            var contextMock = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = user } };
            contextMock.HttpContext.Items = new ConcurrentDictionary<object, object>();
            contextMock.HttpContext.Items[Constants.COLABA_TENANT] = contextTenant;

            RetirementIncomeInfoModel fakeModelReceived = new RetirementIncomeInfoModel()
            {
                BorrowerId = fakeBorrowerId,
                IncomeInfoId = 5,
                IncomeTypeId = 9,
                MonthlyBaseIncome = 0,
                EmployerName = "Rainsoft Financials Private Limited",
                Description = "ABC",
                LoanApplicationId = fakeLoanApplicationId
            };

            // Act
            var controller = new IncomeController(incomeServiceMock.Object, null, null);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;
            var results = await controller.AddOrUpdateRetirementIncomeInfo(fakeModelReceived);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }
        [Fact]
        public async Task TestGetAllBusinessTypes()
        {
            // Arrange
            Mock<IIncomeService> incomeServiceMock = new Mock<IIncomeService>();
            TenantModel contextTenant = new TenantModel()
            {
                Branches = new List<BranchModel>()
                {
                    new BranchModel()
                    {
                        Code = "someBranchCode", Id = 1, IsCorporate = true, LoanOfficers = new List<LoanOfficerModel>()
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

            // Act
            var controller = new IncomeController(incomeServiceMock.Object, Mock.Of<IInfoService>(), Mock.Of<IEmploymentService>());
            controller.ControllerContext.HttpContext = contextMock.HttpContext;
            var results = await controller.GetAllBusinessTypes();

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }
        [Fact]
        public async Task TestGetBusinessIncome()
        {
            // Arrange
            Mock<IIncomeService> incomeServiceMock = new Mock<IIncomeService>();
            TenantModel contextTenant = new TenantModel()
            {
                Branches = new List<BranchModel>()
                {
                    new BranchModel()
                    {
                        Code = "someBranchCode", Id = 1, IsCorporate = true, LoanOfficers = new List<LoanOfficerModel>()
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

            // Act
            var controller = new IncomeController(incomeServiceMock.Object, Mock.Of<IInfoService>(), null);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;
            var results = await controller.GetBusinessIncome(1, 1);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }
        [Fact]
        public async Task TestGetSelfBusinessIncome()
        {
            // Arrange
            Mock<IIncomeService> incomeServiceMock = new Mock<IIncomeService>();
            TenantModel contextTenant = new TenantModel()
            {
                Branches = new List<BranchModel>()
                {
                    new BranchModel()
                    {
                        Code = "someBranchCode", Id = 1, IsCorporate = true, LoanOfficers = new List<LoanOfficerModel>()
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

            // Act
            var controller = new IncomeController(incomeServiceMock.Object, Mock.Of<IInfoService>(), null);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;
            var results = await controller.GetSelfBusinessIncome(1, 1);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }
        [Fact]
        public async Task TestAddOrUpdateSelfBusiness()
        {
            // Arrange
            Mock<IIncomeService> incomeServiceMock = new Mock<IIncomeService>();
            TenantModel contextTenant = new TenantModel()
            {
                Branches = new List<BranchModel>()
                {
                    new BranchModel()
                    {
                        Code = "someBranchCode", Id = 1, IsCorporate = true, LoanOfficers = new List<LoanOfficerModel>()
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

            // Act
            var controller = new IncomeController(incomeServiceMock.Object, Mock.Of<IInfoService>(), null);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;
            var results = await controller.AddOrUpdateSelfBusiness(new SelfBusinessModel() { });

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }
        [Fact]
        public async Task TestAddOrUpdateBusiness()
        {
            // Arrange
            Mock<IIncomeService> incomeServiceMock = new Mock<IIncomeService>();
            TenantModel contextTenant = new TenantModel()
            {
                Branches = new List<BranchModel>()
                {
                    new BranchModel()
                    {
                        Code = "someBranchCode", Id = 1, IsCorporate = true, LoanOfficers = new List<LoanOfficerModel>()
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
            //Cooperation
            BusinessModel fakemodel = new BusinessModel()
                                      {
                                          IncomeTypeId =4
                                      };
            // Act
            var controller = new IncomeController(incomeServiceMock.Object, Mock.Of<IInfoService>(), null);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;
            var results = await controller.AddOrUpdateBusiness(fakemodel);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }

        [Fact]
        public async Task GetSelfBusinessIncomeServiceTest()
        {
            TenantModel model = new TenantModel();
            model.Id = 5;
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
                            Id=5,
                            IsCorporate=true,
                            LoanOfficers= new List<LoanOfficerModel>
                            {
                                 new LoanOfficerModel
                                 {
                                     Id=5,
                                     Code="33"
                                 }
                            }

                        }
                    };

            //Arrange
            DbContextOptions<LoanApplicationContext> options1;
            var builder1 = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder1.UseInMemoryDatabase("LoanApplicationGetSelfBusinessIncomeServiceTest");
            options1 = builder1.Options;
            using LoanApplicationContext dataContext1 = new LoanApplicationContext(options1);
            dataContext1.Database.EnsureCreated();
            IncomeInfo incomeInfo = new IncomeInfo
            {
                Id = 5,
                BorrowerId = 5,
                TenantId = 5,
                StartDate = DateTime.Now,
                AnnualBaseIncome = 123,
                EmployerAddressId = 5
            };

            dataContext1.Set<IncomeInfo>().Add(incomeInfo);
            LoanApplicationDb.Entity.Models.AddressInfo addressInfo = new LoanApplicationDb.Entity.Models.AddressInfo
            {
                Id = 5,
                TenantId = 5
            };

            dataContext1.Set<LoanApplicationDb.Entity.Models.AddressInfo>().Add(addressInfo);
            Borrower borrower = new Borrower
            {
                Id = 5,
                LoanApplicationId = 5,
                TenantId = 5
            };

            dataContext1.Set<Borrower>().Add(borrower);

            LoanApplicationDb.Entity.Models.LoanApplication loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication
            {
                Id = 5,
                UserId = 5,
                TenantId = 5
            };
            dataContext1.Set<LoanApplicationDb.Entity.Models.LoanApplication>().Add(loanApplication);
            dataContext1.SaveChanges();

            var service = new IncomeService(new UnitOfWork<LoanApplicationContext>(dataContext1, new RepositoryProvider(new RepositoryFactories())), Mock.Of<IServiceProvider>(), Mock.Of<ILogger<IncomeService>>(), null);

            var result = await service.GetSelfBusinessIncome(model, 5, 5, 5);
            //Assert
            Assert.NotNull(result);

        }
        [Fact]
        public async Task GetBusinessIncomeServiceTest()
        {
            TenantModel model = new TenantModel();
            model.Id = 5;
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
                            Id=5,
                            IsCorporate=true,
                            LoanOfficers= new List<LoanOfficerModel>
                            {
                                 new LoanOfficerModel
                                 {
                                     Id=5,
                                     Code="33"
                                 }
                            }

                        }
                    };

            //Arrange
            DbContextOptions<LoanApplicationContext> options1;
            var builder1 = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder1.UseInMemoryDatabase("LoanApplicationGetBusinessIncomeServiceTest");
            options1 = builder1.Options;
            using LoanApplicationContext dataContext1 = new LoanApplicationContext(options1);
            dataContext1.Database.EnsureCreated();
            IncomeInfo incomeInfo = new IncomeInfo
            {
                Id = 5,
                BorrowerId = 5,
                TenantId = 5,
                StartDate = DateTime.Now,
                AnnualBaseIncome = 123,
                EmployerAddressId = 5,
                OwnershipPercentage = 1,
                IncomeTypeId = 5
            };

            dataContext1.Set<IncomeInfo>().Add(incomeInfo);
            LoanApplicationDb.Entity.Models.AddressInfo addressInfo = new LoanApplicationDb.Entity.Models.AddressInfo
            {
                Id = 5,
                TenantId = 5
            };

            dataContext1.Set<LoanApplicationDb.Entity.Models.AddressInfo>().Add(addressInfo);
            Borrower borrower = new Borrower
            {
                Id = 5,
                LoanApplicationId = 5,
                TenantId = 5
            };

            dataContext1.Set<Borrower>().Add(borrower);

            LoanApplicationDb.Entity.Models.LoanApplication loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication
            {
                Id = 5,
                UserId = 5,
                TenantId = 5
            };
            dataContext1.Set<LoanApplicationDb.Entity.Models.LoanApplication>().Add(loanApplication);
            dataContext1.SaveChanges();

            var service = new IncomeService(new UnitOfWork<LoanApplicationContext>(dataContext1, new RepositoryProvider(new RepositoryFactories())), Mock.Of<IServiceProvider>(), Mock.Of<ILogger<IncomeService>>(), null);

            var result = await service.GetBusinessIncome(model, 5, 5, 5);
            //Assert
            Assert.NotNull(result);

        }
        [Fact]
        public async Task AddOrUpdateSelfBusinessServiceTest()
        {
            TenantModel model = new TenantModel();
            model.Id = 5;
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
                            Id=5,
                            IsCorporate=true,
                            LoanOfficers= new List<LoanOfficerModel>
                            {
                                 new LoanOfficerModel
                                 {
                                     Id=5,
                                     Code="33"
                                 }
                            }

                        }
                    };

            //Arrange
            DbContextOptions<LoanApplicationContext> options1;
            var builder1 = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder1.UseInMemoryDatabase("LoanApplicationAddOrUpdateSelfBusinessServiceTest");
            options1 = builder1.Options;
            using LoanApplicationContext dataContext1 = new LoanApplicationContext(options1);
            dataContext1.Database.EnsureCreated();
            IncomeInfo incomeInfo = new IncomeInfo
            {
                Id = 5,
                BorrowerId = 5,
                TenantId = 5,
                StartDate = DateTime.Now,
                AnnualBaseIncome = 123,
                EmployerAddressId = 5,
                OwnershipPercentage = 1,
                IncomeTypeId = 5
            };

            dataContext1.Set<IncomeInfo>().Add(incomeInfo);
            LoanApplicationDb.Entity.Models.AddressInfo addressInfo = new LoanApplicationDb.Entity.Models.AddressInfo
            {
                Id = 5,
                TenantId = 5
            };

            dataContext1.Set<LoanApplicationDb.Entity.Models.AddressInfo>().Add(addressInfo);
            Borrower borrower = new Borrower
            {
                Id = 5,
                LoanApplicationId = 5,
                TenantId = 5
            };

            dataContext1.Set<Borrower>().Add(borrower);

            LoanApplicationDb.Entity.Models.LoanApplication loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication
            {
                Id = 5,
                UserId = 5,
                TenantId = 5
            };
            dataContext1.Set<LoanApplicationDb.Entity.Models.LoanApplication>().Add(loanApplication);
            dataContext1.SaveChanges();

            var service = new IncomeService(new UnitOfWork<LoanApplicationContext>(dataContext1, new RepositoryProvider(new RepositoryFactories())), Mock.Of<IServiceProvider>(), Mock.Of<ILogger<IncomeService>>(), null);

            var result = await service.AddOrUpdateSelfBusiness(model, new SelfBusinessModel { Id = 5, BorrowerId = 5, LoanApplicationId = 5, Address = new GenericAddressModel { } }, 5);
            //Assert
            Assert.NotNull(result);

        }
        [Fact]
        public async Task AddOrUpdateSelfBusinessNoResServiceTest()
        {
            TenantModel model = new TenantModel();
            model.Id = 5;
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
                            Id=5,
                            IsCorporate=true,
                            LoanOfficers= new List<LoanOfficerModel>
                            {
                                 new LoanOfficerModel
                                 {
                                     Id=5,
                                     Code="33"
                                 }
                            }

                        }
                    };

            //Arrange
            DbContextOptions<LoanApplicationContext> options1;
            var builder1 = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder1.UseInMemoryDatabase("LoanApplicationAddOrUpdateSelfBusinessNoResServiceTest");
            options1 = builder1.Options;
            using LoanApplicationContext dataContext1 = new LoanApplicationContext(options1);
            dataContext1.Database.EnsureCreated();
            IncomeInfo incomeInfo = new IncomeInfo
            {
                Id = 5,
                BorrowerId = 5,
                TenantId = 5,
                StartDate = DateTime.Now,
                AnnualBaseIncome = 123,
                EmployerAddressId = 5,
                OwnershipPercentage = 1,
                IncomeTypeId = 5
            };

            dataContext1.Set<IncomeInfo>().Add(incomeInfo);
            LoanApplicationDb.Entity.Models.AddressInfo addressInfo = new LoanApplicationDb.Entity.Models.AddressInfo
            {
                Id = 5,
                TenantId = 5
            };

            dataContext1.Set<LoanApplicationDb.Entity.Models.AddressInfo>().Add(addressInfo);
            Borrower borrower = new Borrower
            {
                Id = 5,
                LoanApplicationId = 5,
                TenantId = 5
            };

            dataContext1.Set<Borrower>().Add(borrower);

            LoanApplicationDb.Entity.Models.LoanApplication loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication
            {
                Id = 5,
                UserId = 5,
                TenantId = 5
            };
            dataContext1.Set<LoanApplicationDb.Entity.Models.LoanApplication>().Add(loanApplication);
            dataContext1.SaveChanges();

            var service = new IncomeService(new UnitOfWork<LoanApplicationContext>(dataContext1, new RepositoryProvider(new RepositoryFactories())), Mock.Of<IServiceProvider>(), Mock.Of<ILogger<IncomeService>>(), null);

            var result = await service.AddOrUpdateSelfBusiness(model, new SelfBusinessModel { Id = null, BorrowerId = 5, LoanApplicationId = 5, Address = new GenericAddressModel { } }, 5);
            //Assert
            Assert.NotNull(result);

        }
        [Fact]
        public async Task AddOrUpdateBusinessServiceTest()
        {
            TenantModel model = new TenantModel();
            model.Id = 5;
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
                            Id=5,
                            IsCorporate=true,
                            LoanOfficers= new List<LoanOfficerModel>
                            {
                                 new LoanOfficerModel
                                 {
                                     Id=5,
                                     Code="33"
                                 }
                            }

                        }
                    };

            //Arrange
            DbContextOptions<LoanApplicationContext> options1;
            var builder1 = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder1.UseInMemoryDatabase("LoanApplicationAddOrUpdateBusinessServiceTest");
            options1 = builder1.Options;
            using LoanApplicationContext dataContext1 = new LoanApplicationContext(options1);
            dataContext1.Database.EnsureCreated();
            IncomeInfo incomeInfo = new IncomeInfo
            {
                Id = 5,
                BorrowerId = 5,
                TenantId = 5,
                StartDate = DateTime.Now,
                AnnualBaseIncome = 123,
                EmployerAddressId = 5,
                OwnershipPercentage = 1,
                IncomeTypeId = 5
            };

            dataContext1.Set<IncomeInfo>().Add(incomeInfo);
            LoanApplicationDb.Entity.Models.AddressInfo addressInfo = new LoanApplicationDb.Entity.Models.AddressInfo
            {
                Id = 5,
                TenantId = 5
            };

            dataContext1.Set<LoanApplicationDb.Entity.Models.AddressInfo>().Add(addressInfo);
            Borrower borrower = new Borrower
            {
                Id = 5,
                LoanApplicationId = 5,
                TenantId = 5
            };

            dataContext1.Set<Borrower>().Add(borrower);

            LoanApplicationDb.Entity.Models.LoanApplication loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication
            {
                Id = 5,
                UserId = 5,
                TenantId = 5
            };
            dataContext1.Set<LoanApplicationDb.Entity.Models.LoanApplication>().Add(loanApplication);
            dataContext1.SaveChanges();

            var service = new IncomeService(new UnitOfWork<LoanApplicationContext>(dataContext1, new RepositoryProvider(new RepositoryFactories())), Mock.Of<IServiceProvider>(), Mock.Of<ILogger<IncomeService>>(), null);

            var result = await service.AddOrUpdateBusiness(model, new BusinessModel { Id = 5, BorrowerId = 5, LoanApplicationId = 5, Address = new GenericAddressModel { } }, 5);
            //Assert
            Assert.NotNull(result);

        }
        [Fact]
        public async Task AddOrUpdateBusinessNoResServiceTest()
        {
            TenantModel model = new TenantModel();
            model.Id = 5;
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
                            Id=5,
                            IsCorporate=true,
                            LoanOfficers= new List<LoanOfficerModel>
                            {
                                 new LoanOfficerModel
                                 {
                                     Id=5,
                                     Code="33"
                                 }
                            }

                        }
                    };

            //Arrange
            DbContextOptions<LoanApplicationContext> options1;
            var builder1 = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder1.UseInMemoryDatabase("LoanApplicationAddOrUpdateBusinessNoResServiceTest");
            options1 = builder1.Options;
            using LoanApplicationContext dataContext1 = new LoanApplicationContext(options1);
            dataContext1.Database.EnsureCreated();
            IncomeInfo incomeInfo = new IncomeInfo
            {
                Id = 5,
                BorrowerId = 5,
                TenantId = 5,
                StartDate = DateTime.Now,
                AnnualBaseIncome = 123,
                EmployerAddressId = 5,
                OwnershipPercentage = 1,
                IncomeTypeId = 5
            };

            dataContext1.Set<IncomeInfo>().Add(incomeInfo);
            LoanApplicationDb.Entity.Models.AddressInfo addressInfo = new LoanApplicationDb.Entity.Models.AddressInfo
            {
                Id = 5,
                TenantId = 5
            };

            dataContext1.Set<LoanApplicationDb.Entity.Models.AddressInfo>().Add(addressInfo);
            Borrower borrower = new Borrower
            {
                Id = 5,
                LoanApplicationId = 5,
                TenantId = 5
            };

            dataContext1.Set<Borrower>().Add(borrower);

            LoanApplicationDb.Entity.Models.LoanApplication loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication
            {
                Id = 5,
                UserId = 5,
                TenantId = 5
            };
            dataContext1.Set<LoanApplicationDb.Entity.Models.LoanApplication>().Add(loanApplication);
            dataContext1.SaveChanges();

            var service = new IncomeService(new UnitOfWork<LoanApplicationContext>(dataContext1, new RepositoryProvider(new RepositoryFactories())), Mock.Of<IServiceProvider>(), Mock.Of<ILogger<IncomeService>>(), null);

            var result = await service.AddOrUpdateBusiness(model, new BusinessModel { Id = null, BorrowerId = 5, LoanApplicationId = 5, Address = new GenericAddressModel { } }, 5);
            //Assert
            Assert.NotNull(result);

        }

        [Fact]
        public async Task AddOrUpdateRetirementIncomeInfoPensionTest()
        {
            TenantModel model = new TenantModel();
            model.Id = 5;
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
                            Id=5,
                            IsCorporate=true,
                            LoanOfficers= new List<LoanOfficerModel>
                            {
                                 new LoanOfficerModel
                                 {
                                     Id=5,
                                     Code="33"
                                 }
                            }

                        }
                    };


            //Arrange
            DbContextOptions<LoanApplicationContext> options1;
            var builder1 = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder1.UseInMemoryDatabase("LoanApplicationAddOrUpdateRetirementIncomeInfoPensionTest");
            options1 = builder1.Options;
            using LoanApplicationContext dataContext1 = new LoanApplicationContext(options1);
            dataContext1.Database.EnsureCreated();
            Borrower borrower = new Borrower
            {
                Id = 5,
                LoanApplicationId = 5,
                TenantId = 5,
            };

            dataContext1.Set<Borrower>().Add(borrower);
            LoanApplicationDb.Entity.Models.AddressInfo addressInfo = new LoanApplicationDb.Entity.Models.AddressInfo
            {
                Id = 5,
                TenantId = 5,
                CityName = "Test",
                CountryId = 1,
                StateId = 1,
            };

            dataContext1.Set<LoanApplicationDb.Entity.Models.AddressInfo>().Add(addressInfo);
            IncomeInfo incomeInfo = new IncomeInfo
            {
                Id = 5,
                BorrowerId = 5,
                EmployerAddressId = 5,
                TenantId = 5
            };
            dataContext1.Set<LoanApplicationDb.Entity.Models.IncomeInfo>().Add(incomeInfo);
            LoanApplicationDb.Entity.Models.LoanApplication loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication
            {
                Id = 5,
                UserId = 5,
                TenantId = 5

            };
            dataContext1.Set<LoanApplicationDb.Entity.Models.LoanApplication>().Add(loanApplication);
            dataContext1.SaveChanges();
            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();
            List<IncomeType> fakeIncomeTypes = new List<IncomeType>()
                                               {
                                                   new IncomeType()
                                                   {
                                                       Id = 1,
                                                       Name = "XYZ",
                                                   },
                                                   new IncomeType()
                                                   {
                                                       Id = 2,
                                                       Name = "Secondary"
                                                   }
                                               };


            dbFunctionServiceMock.Setup(x => x.UdfIncomeType(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeIncomeTypes.AsQueryable());
            var service = new IncomeService(new UnitOfWork<LoanApplicationContext>(dataContext1, new RepositoryProvider(new RepositoryFactories())), Mock.Of<IServiceProvider>(), Mock.Of<ILogger<IncomeService>>(), dbFunctionServiceMock.Object);

            var result = await service.AddOrUpdateRetirementIncomeInfo(new RetirementIncomeInfoModel { IncomeInfoId = 5, LoanApplicationId = 5, BorrowerId = 5, IncomeTypeId = 7 }, model, 5);
            //Assert
            Assert.Equal(5, result);

        }

        [Fact]
        public async Task AddOrUpdateRetirementIncomeInfoSocialSecurityTest()
        {
            TenantModel model = new TenantModel();
            model.Id = 5;
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
                            Id=5,
                            IsCorporate=true,
                            LoanOfficers= new List<LoanOfficerModel>
                            {
                                 new LoanOfficerModel
                                 {
                                     Id=5,
                                     Code="33"
                                 }
                            }

                        }
                    };


            //Arrange
            DbContextOptions<LoanApplicationContext> options1;
            var builder1 = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder1.UseInMemoryDatabase("LoanApplicationAddOrUpdateRetirementIncomeInfoSocialSecurityTest");
            options1 = builder1.Options;
            using LoanApplicationContext dataContext1 = new LoanApplicationContext(options1);
            dataContext1.Database.EnsureCreated();
            Borrower borrower = new Borrower
            {
                Id = 5,
                LoanApplicationId = 5,
                TenantId = 5,
            };

            dataContext1.Set<Borrower>().Add(borrower);
            LoanApplicationDb.Entity.Models.AddressInfo addressInfo = new LoanApplicationDb.Entity.Models.AddressInfo
            {
                Id = 5,
                TenantId = 5,
                CityName = "Test",
                CountryId = 1,
                StateId = 1,
            };

            dataContext1.Set<LoanApplicationDb.Entity.Models.AddressInfo>().Add(addressInfo);
            IncomeInfo incomeInfo = new IncomeInfo
            {
                Id = 5,
                BorrowerId = 5,
                EmployerAddressId = 5,
                TenantId = 5
            };
            dataContext1.Set<LoanApplicationDb.Entity.Models.IncomeInfo>().Add(incomeInfo);
            LoanApplicationDb.Entity.Models.LoanApplication loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication
            {
                Id = 5,
                UserId = 5,
                TenantId = 5

            };
            dataContext1.Set<LoanApplicationDb.Entity.Models.LoanApplication>().Add(loanApplication);
            dataContext1.SaveChanges();
            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();
            List<IncomeType> fakeIncomeTypes = new List<IncomeType>()
                                               {
                                                   new IncomeType()
                                                   {
                                                       Id = 1,
                                                       Name = "XYZ",
                                                   },
                                                   new IncomeType()
                                                   {
                                                       Id = 2,
                                                       Name = "Secondary"
                                                   }
                                               };


            dbFunctionServiceMock.Setup(x => x.UdfIncomeType(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeIncomeTypes.AsQueryable());
            var service = new IncomeService(new UnitOfWork<LoanApplicationContext>(dataContext1, new RepositoryProvider(new RepositoryFactories())), Mock.Of<IServiceProvider>(), Mock.Of<ILogger<IncomeService>>(), dbFunctionServiceMock.Object);

            var result = await service.AddOrUpdateRetirementIncomeInfo(new RetirementIncomeInfoModel { IncomeInfoId = 5, LoanApplicationId = 5, BorrowerId = 5, IncomeTypeId = 6 }, model, 5);
            //Assert
            Assert.Equal(5, result);

        }

        [Fact]
        public async Task AddOrUpdateRetirementIncomeInfoIraTest()
        {
            TenantModel model = new TenantModel();
            model.Id = 5;
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
                            Id=5,
                            IsCorporate=true,
                            LoanOfficers= new List<LoanOfficerModel>
                            {
                                 new LoanOfficerModel
                                 {
                                     Id=5,
                                     Code="33"
                                 }
                            }

                        }
                    };

            //Arrange
            DbContextOptions<LoanApplicationContext> options1;
            var builder1 = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder1.UseInMemoryDatabase("LoanApplicationAddOrUpdateRetirementIncomeInfoIraTest");
            options1 = builder1.Options;
            using LoanApplicationContext dataContext1 = new LoanApplicationContext(options1);
            dataContext1.Database.EnsureCreated();
            Borrower borrower = new Borrower
            {
                Id = 5,
                LoanApplicationId = 5,
                TenantId = 5,
            };

            dataContext1.Set<Borrower>().Add(borrower);
            LoanApplicationDb.Entity.Models.AddressInfo addressInfo = new LoanApplicationDb.Entity.Models.AddressInfo
            {
                Id = 5,
                TenantId = 5,
                CityName = "Test",
                CountryId = 1,
                StateId = 1,
            };

            dataContext1.Set<LoanApplicationDb.Entity.Models.AddressInfo>().Add(addressInfo);
            IncomeInfo incomeInfo = new IncomeInfo
            {
                Id = 5,
                BorrowerId = 5,
                EmployerAddressId = 5,
                TenantId = 5
            };
            dataContext1.Set<LoanApplicationDb.Entity.Models.IncomeInfo>().Add(incomeInfo);
            LoanApplicationDb.Entity.Models.LoanApplication loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication
            {
                Id = 5,
                UserId = 5,
                TenantId = 5

            };
            dataContext1.Set<LoanApplicationDb.Entity.Models.LoanApplication>().Add(loanApplication);
            dataContext1.SaveChanges();
            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();

           

            List<IncomeType> fakeIncomeTypes = new List<IncomeType>()
                                            {
                                                new IncomeType()
                                                {
                                                    Id = 1,
                                                    Name = "XYZ",
                                                },
                                                new IncomeType()
                                                {
                                                    Id = 2,
                                                    Name = "Secondary"
                                                }
                                            };


            dbFunctionServiceMock.Setup(x => x.UdfIncomeType(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeIncomeTypes.AsQueryable());

            var service = new IncomeService(new UnitOfWork<LoanApplicationContext>(dataContext1, new RepositoryProvider(new RepositoryFactories())), Mock.Of<IServiceProvider>(), Mock.Of<ILogger<IncomeService>>(), dbFunctionServiceMock.Object);
            
          
            
            var result = await service.AddOrUpdateRetirementIncomeInfo(new RetirementIncomeInfoModel { IncomeInfoId = 5, LoanApplicationId = 5, BorrowerId = 5, IncomeTypeId = 8 }, model, 5);
            //Assert
            Assert.Equal(5, result);

        }

        [Fact]
        public async Task AddOrUpdateRetirementIncomeInfoOtherTest()
        {
            TenantModel model = new TenantModel();
            model.Id = 5;
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
                            Id=5,
                            IsCorporate=true,
                            LoanOfficers= new List<LoanOfficerModel>
                            {
                                 new LoanOfficerModel
                                 {
                                     Id=5,
                                     Code="33"
                                 }
                            }

                        }
                    };

            //Arrange
            DbContextOptions<LoanApplicationContext> options1;
            var builder1 = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder1.UseInMemoryDatabase("LoanApplicationAddOrUpdateRetirementIncomeInfoOtherTest");
            options1 = builder1.Options;
            using LoanApplicationContext dataContext1 = new LoanApplicationContext(options1);
            dataContext1.Database.EnsureCreated();
            Borrower borrower = new Borrower
            {
                Id = 5,
                LoanApplicationId = 5,
                TenantId = 5,
            };

            dataContext1.Set<Borrower>().Add(borrower);
            LoanApplicationDb.Entity.Models.AddressInfo addressInfo = new LoanApplicationDb.Entity.Models.AddressInfo
            {
                Id = 5,
                TenantId = 5,
                CityName = "Test",
                CountryId = 1,
                StateId = 1,
            };

            dataContext1.Set<LoanApplicationDb.Entity.Models.AddressInfo>().Add(addressInfo);
            IncomeInfo incomeInfo = new IncomeInfo
            {
                Id = 5,
                BorrowerId = 5,
                EmployerAddressId = 5,
                TenantId = 5
            };
            dataContext1.Set<LoanApplicationDb.Entity.Models.IncomeInfo>().Add(incomeInfo);
            LoanApplicationDb.Entity.Models.LoanApplication loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication
            {
                Id = 5,
                UserId = 5,
                TenantId = 5

            };
            dataContext1.Set<LoanApplicationDb.Entity.Models.LoanApplication>().Add(loanApplication);
            dataContext1.SaveChanges();
            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();
            List<IncomeType> fakeIncomeTypes = new List<IncomeType>()
                                               {
                                                   new IncomeType()
                                                   {
                                                       Id = 1,
                                                       Name = "XYZ",
                                                   },
                                                   new IncomeType()
                                                   {
                                                       Id = 2,
                                                       Name = "Secondary"
                                                   }
                                               };


            dbFunctionServiceMock.Setup(x => x.UdfIncomeType(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeIncomeTypes.AsQueryable());
            var service = new IncomeService(new UnitOfWork<LoanApplicationContext>(dataContext1, new RepositoryProvider(new RepositoryFactories())), Mock.Of<IServiceProvider>(), Mock.Of<ILogger<IncomeService>>(), dbFunctionServiceMock.Object);

            var result = await service.AddOrUpdateRetirementIncomeInfo(new RetirementIncomeInfoModel { IncomeInfoId = 5, LoanApplicationId = 5, BorrowerId = 5, IncomeTypeId = 9 }, model, 5);
            //Assert
            Assert.Equal(5, result);

        }

        [Fact]
        public async Task GetOtherIncomeInfoTest()
        {
            // Arrange
            Mock<IInfoService> infoServiceMock = new Mock<IInfoService>();
            Mock<IIncomeService> incomeServiceMock = new Mock<IIncomeService>();

            TenantModel contextTenant = new TenantModel()
            {
                Branches = new List<BranchModel>()
                {
                    new BranchModel()
                    {
                        Code = "someBranchCode", Id = 1, IsCorporate = true, LoanOfficers = new List<LoanOfficerModel>()
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
            int fakeInfoId = 1;
            // Act
            GetOtherIncomeInfoModel fakeDataRetrived = new GetOtherIncomeInfoModel();

            incomeServiceMock.Setup(x => x.GetOtherIncomeInfo(contextTenant, fakeUserId, fakeInfoId)).ReturnsAsync(fakeDataRetrived);

            var controller = new IncomeController(incomeServiceMock.Object, infoServiceMock.Object, Mock.Of<IEmploymentService>());
            controller.ControllerContext.HttpContext = contextMock.HttpContext;


            var results = await controller.GetOtherIncomeInfo(fakeInfoId);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);

        }

        [Fact]
        public async Task UpdateOtherMonthlyIncomeTest()
        {
            // Arrange
            Mock<IInfoService> infoServiceMock = new Mock<IInfoService>();
            Mock<IIncomeService> incomeServiceMock = new Mock<IIncomeService>();

            TenantModel contextTenant = new TenantModel()
            {
                Branches = new List<BranchModel>()
                {
                    new BranchModel()
                    {
                        Code = "someBranchCode", Id = 1, IsCorporate = true, LoanOfficers = new List<LoanOfficerModel>()
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
            int fakeInfoIdRetrived = 1;
            // Act
            AddOrUpdateOtherIncomeModel fakeDataRetrived = new AddOrUpdateOtherIncomeModel()
            {
                IncomeTypeId = IncomeTypes.Alimony,
                MonthlyBaseIncome = 1100000
            };

            incomeServiceMock.Setup(x => x.AddOrUpdateEmployerOtherMonthyIncome(contextTenant, fakeUserId, fakeDataRetrived)).ReturnsAsync(fakeInfoIdRetrived);

            var controller = new IncomeController(incomeServiceMock.Object, infoServiceMock.Object, Mock.Of<IEmploymentService>());
            controller.ControllerContext.HttpContext = contextMock.HttpContext;


            var results = await controller.AddOrUpdateOtherIncome(fakeDataRetrived);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);

        }

        //[Fact]
        //public async Task UpdateOtherMonthlyIncomeWithDescriptionTest()
        //{
        //    // Arrange
        //    Mock<IInfoService> infoServiceMock = new Mock<IInfoService>();
        //    Mock<IIncomeService> incomeServiceMock = new Mock<IIncomeService>();

        //    TenantModel contextTenant = new TenantModel()
        //    {
        //        Branches = new List<BranchModel>()
        //        {
        //            new BranchModel()
        //            {
        //                Code = "someBranchCode", Id = 1, IsCorporate = true, LoanOfficers = new List<LoanOfficerModel>()
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
        //    int fakeInfoIdRetrived = 1;
        //    // Act
        //    UpdateOtherMonthlyIncomeWithDescriptionModel fakeDataRetrived = new UpdateOtherMonthlyIncomeWithDescriptionModel()
        //    {
        //        IncomeTypeId = EmployerIncomeTypes.Annuity,
        //        MonthlyBaseIncome = 1100000
        //    };

        //    incomeServiceMock.Setup(x => x.AddOrUpdateEmployerMonthlyIncomeWithDescription(contextTenant, fakeUserId, fakeDataRetrived)).ReturnsAsync(fakeInfoIdRetrived);

        //    var controller = new IncomeController(incomeServiceMock.Object, infoServiceMock.Object, Mock.Of<IEmploymentService>());
        //    controller.ControllerContext.HttpContext = contextMock.HttpContext;


        //    var results = await controller.UpdateOtherMonthlyIncomeWithDescription(fakeDataRetrived);

        //    // Assert
        //    Assert.NotNull(results);
        //    Assert.IsType<OkObjectResult>(results);

        //}

        //[Fact]
        //public async Task UpdateOtherAnnualIncomeTest()
        //{
        //    // Arrange
        //    Mock<IInfoService> infoServiceMock = new Mock<IInfoService>();
        //    Mock<IIncomeService> incomeServiceMock = new Mock<IIncomeService>();

        //    TenantModel contextTenant = new TenantModel()
        //    {
        //        Branches = new List<BranchModel>()
        //        {
        //            new BranchModel()
        //            {
        //                Code = "someBranchCode", Id = 1, IsCorporate = true, LoanOfficers = new List<LoanOfficerModel>()
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
        //    int fakeInfoIdRetrived = 1;
        //    // Act
        //    UpdateOtherAnnualIncomeModel fakeDataRetrived = new UpdateOtherAnnualIncomeModel()
        //    {
        //        IncomeTypeId = EmployerIncomeTypes.InterestDividends,
        //        AnnualBaseIncome = 1100000
        //    };

        //    incomeServiceMock.Setup(x => x.AddOrUpdateEmployerYearlyIncome(contextTenant, fakeUserId, fakeDataRetrived)).ReturnsAsync(fakeInfoIdRetrived);

        //    var controller = new IncomeController(incomeServiceMock.Object, infoServiceMock.Object, Mock.Of<IEmploymentService>());
        //    controller.ControllerContext.HttpContext = contextMock.HttpContext;


        //    var results = await controller.UpdateOtherAnnualIncome(fakeDataRetrived);

        //    // Assert
        //    Assert.NotNull(results);
        //    Assert.IsType<OkObjectResult>(results);

        //}

        //[Fact]
        //public async Task UpdateOtherAnnualIncomeWithDescriptionTest()
        //{
        //    // Arrange
        //    Mock<IInfoService> infoServiceMock = new Mock<IInfoService>();
        //    Mock<IIncomeService> incomeServiceMock = new Mock<IIncomeService>();

        //    TenantModel contextTenant = new TenantModel()
        //    {
        //        Branches = new List<BranchModel>()
        //        {
        //            new BranchModel()
        //            {
        //                Code = "someBranchCode", Id = 1, IsCorporate = true, LoanOfficers = new List<LoanOfficerModel>()
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
        //    int fakeInfoIdRetrived = 1;
        //    // Act
        //    UpdateOtherAnnualIncomeWithDescriptionModel fakeDataRetrived = new UpdateOtherAnnualIncomeWithDescriptionModel()
        //    {
        //        IncomeTypeId = EmployerIncomeTypes.OtherIncomeSource,
        //        AnnualBaseIncome = 1100000,
        //        Description="fake descrition"
        //    };

        //    incomeServiceMock.Setup(x => x.AddOrUpdateOtherAnnualIncomeWithDescription(contextTenant, fakeUserId, fakeDataRetrived)).ReturnsAsync(fakeInfoIdRetrived);

        //    var controller = new IncomeController(incomeServiceMock.Object, infoServiceMock.Object, Mock.Of<IEmploymentService>());
        //    controller.ControllerContext.HttpContext = contextMock.HttpContext;


        //    var results = await controller.UpdateOtherAnnualIncomeWithDescription(fakeDataRetrived);

        //    // Assert
        //    Assert.NotNull(results);
        //    Assert.IsType<OkObjectResult>(results);

        //}


        [Fact]
        public async Task GetSummaryTest()
        {
            // Arrange
            Mock<IInfoService> infoServiceMock = new Mock<IInfoService>();
            Mock<IIncomeService> incomeServiceMock = new Mock<IIncomeService>();

            TenantModel contextTenant = new TenantModel()
            {
                Branches = new List<BranchModel>()
                {
                    new BranchModel()
                    {
                        Code = "someBranchCode", Id = 1, IsCorporate = true, LoanOfficers = new List<LoanOfficerModel>()
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

            int fakeLoanId = 1;
            // Act
            List<EmployerDetailBaseModels> fakeReturnModel = new List<EmployerDetailBaseModels>();


            incomeServiceMock.Setup(x => x.GetSummary(contextTenant, fakeUserId, fakeLoanId)).ReturnsAsync(fakeReturnModel);

            var controller = new IncomeController(incomeServiceMock.Object, infoServiceMock.Object, Mock.Of<IEmploymentService>());
            controller.ControllerContext.HttpContext = contextMock.HttpContext;


            var results = await controller.GetSummary(fakeLoanId);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);

        }


        [Fact]
        public async Task GetRetirementIncomeInfoIncomeServiceTest()
        {
            TenantModel model = new TenantModel();
            model.Id = 5;
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
                            Id=5,
                            IsCorporate=true,
                            LoanOfficers= new List<LoanOfficerModel>
                            {
                                 new LoanOfficerModel
                                 {
                                     Id=5,
                                     Code="33"
                                 }
                            }

                        }
                    };

            //Arrange
            DbContextOptions<LoanApplicationContext> options1;
            var builder1 = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder1.UseInMemoryDatabase("LoanApplicationGetRetirementIncomeInfoIncomeServiceTest");
            options1 = builder1.Options;
            using LoanApplicationContext dataContext1 = new LoanApplicationContext(options1);
            dataContext1.Database.EnsureCreated();
            IncomeInfo incomeInfo = new IncomeInfo
            {
                Id = 5,
                BorrowerId = 5,
                TenantId = 5,
                StartDate = DateTime.Now,
                AnnualBaseIncome = 123,
                EmployerAddressId = 5
            };

            dataContext1.Set<IncomeInfo>().Add(incomeInfo);
            LoanApplicationDb.Entity.Models.AddressInfo addressInfo = new LoanApplicationDb.Entity.Models.AddressInfo
            {
                Id = 5,
                TenantId = 5
            };

            dataContext1.Set<LoanApplicationDb.Entity.Models.AddressInfo>().Add(addressInfo);
            Borrower borrower = new Borrower
            {
                Id = 5,
                LoanApplicationId = 5,
                TenantId = 5
            };

            dataContext1.Set<Borrower>().Add(borrower);

            LoanApplicationDb.Entity.Models.LoanApplication loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication
            {
                Id = 5,
                UserId = 5,
                TenantId = 5
            };
            dataContext1.Set<LoanApplicationDb.Entity.Models.LoanApplication>().Add(loanApplication);
            dataContext1.SaveChanges();

            var service = new IncomeService(new UnitOfWork<LoanApplicationContext>(dataContext1, new RepositoryProvider(new RepositoryFactories())), Mock.Of<IServiceProvider>(), Mock.Of<ILogger<IncomeService>>(), null);

            var result = await service.GetRetirementIncomeInfo(5, 5, 5, model);
            //Assert
            Assert.NotNull(result);

        }

        [Fact]
        public async Task AddOrUpdateOtherIncome_MonthlyIncomeTypes_OK()
        {
            // Arrange
            Mock<IInfoService> infoServiceMock = new Mock<IInfoService>();
            Mock<IIncomeService> incomeServiceMock = new Mock<IIncomeService>();

            TenantModel contextTenant = new TenantModel()
            {
                Branches = new List<BranchModel>()
                {
                    new BranchModel()
                    {
                        Code = "someBranchCode", Id = 1, IsCorporate = true, LoanOfficers = new List<LoanOfficerModel>()
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

            int fakeIntReturn = 1;
            // Act
            AddOrUpdateOtherIncomeModel fakeReturnModel = new AddOrUpdateOtherIncomeModel();
            AddOrUpdateOtherIncomeModel fakeModeltoPost = new AddOrUpdateOtherIncomeModel()
            {
                MonthlyBaseIncome = 1000,

                IncomeTypeId = IncomeTypes.Alimony
            };

            incomeServiceMock.Setup(x => x.AddOrUpdateEmployerOtherMonthyIncome(contextTenant, fakeUserId, fakeReturnModel)).ReturnsAsync(fakeIntReturn);

            var controller = new IncomeController(incomeServiceMock.Object, infoServiceMock.Object, Mock.Of<IEmploymentService>());
            controller.ControllerContext.HttpContext = contextMock.HttpContext;


            var results = await controller.AddOrUpdateOtherIncome(fakeModeltoPost);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);

        }

        [Fact]
        public async Task AddOrUpdateOtherIncome_MonthlyIncomeTypesAnnuity_OK()
        {
            // Arrange
            Mock<IInfoService> infoServiceMock = new Mock<IInfoService>();
            Mock<IIncomeService> incomeServiceMock = new Mock<IIncomeService>();

            TenantModel contextTenant = new TenantModel()
            {
                Branches = new List<BranchModel>()
                {
                    new BranchModel()
                    {
                        Code = "someBranchCode", Id = 1, IsCorporate = true, LoanOfficers = new List<LoanOfficerModel>()
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

            int fakeIntReturn = 1;
            // Act
            AddOrUpdateOtherIncomeModel fakeReturnModel = new AddOrUpdateOtherIncomeModel();
            AddOrUpdateOtherIncomeModel fakeModeltoPost = new AddOrUpdateOtherIncomeModel()
            {
                MonthlyBaseIncome = 1000,
                Description = "fake description",
                IncomeTypeId = IncomeTypes.Annuity
            };

            incomeServiceMock.Setup(x => x.AddOrUpdateEmployerOtherMonthyIncome(contextTenant, fakeUserId, fakeReturnModel)).ReturnsAsync(fakeIntReturn);

            var controller = new IncomeController(incomeServiceMock.Object, infoServiceMock.Object, Mock.Of<IEmploymentService>());
            controller.ControllerContext.HttpContext = contextMock.HttpContext;


            var results = await controller.AddOrUpdateOtherIncome(fakeModeltoPost);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);

        }


        [Fact]
        public async Task AddOrUpdateOtherIncome_MonthlyIncomeTypesInterestDividends_OK()
        {
            // Arrange
            Mock<IInfoService> infoServiceMock = new Mock<IInfoService>();
            Mock<IIncomeService> incomeServiceMock = new Mock<IIncomeService>();

            TenantModel contextTenant = new TenantModel()
            {
                Branches = new List<BranchModel>()
                {
                    new BranchModel()
                    {
                        Code = "someBranchCode", Id = 1, IsCorporate = true, LoanOfficers = new List<LoanOfficerModel>()
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

            int fakeIntReturn = 1;
            // Act
            AddOrUpdateOtherIncomeModel fakeReturnModel = new AddOrUpdateOtherIncomeModel();
            AddOrUpdateOtherIncomeModel fakeModeltoPost = new AddOrUpdateOtherIncomeModel()
            {
                AnnualBaseIncome = 1000,

                IncomeTypeId = IncomeTypes.InterestDividends
            };

            incomeServiceMock.Setup(x => x.AddOrUpdateEmployerOtherMonthyIncome(contextTenant, fakeUserId, fakeReturnModel)).ReturnsAsync(fakeIntReturn);

            var controller = new IncomeController(incomeServiceMock.Object, infoServiceMock.Object, Mock.Of<IEmploymentService>());
            controller.ControllerContext.HttpContext = contextMock.HttpContext;


            var results = await controller.AddOrUpdateOtherIncome(fakeModeltoPost);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);

        }

        [Fact]
        public async Task AddOrUpdateOtherIncome_MonthlyIncomeTypesOtherIncomeSource_OK()
        {
            // Arrange
            Mock<IInfoService> infoServiceMock = new Mock<IInfoService>();
            Mock<IIncomeService> incomeServiceMock = new Mock<IIncomeService>();

            TenantModel contextTenant = new TenantModel()
            {
                Branches = new List<BranchModel>()
                {
                    new BranchModel()
                    {
                        Code = "someBranchCode", Id = 1, IsCorporate = true, LoanOfficers = new List<LoanOfficerModel>()
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

            int fakeIntReturn = 1;
            // Act
            AddOrUpdateOtherIncomeModel fakeReturnModel = new AddOrUpdateOtherIncomeModel();
            AddOrUpdateOtherIncomeModel fakeModeltoPost = new AddOrUpdateOtherIncomeModel()
            {
                AnnualBaseIncome = 1000,
                Description = "fake description requerid",
                IncomeTypeId = IncomeTypes.OtherIncomeSource
            };

            incomeServiceMock.Setup(x => x.AddOrUpdateEmployerOtherMonthyIncome(contextTenant, fakeUserId, fakeReturnModel)).ReturnsAsync(fakeIntReturn);

            var controller = new IncomeController(incomeServiceMock.Object, infoServiceMock.Object, Mock.Of<IEmploymentService>());
            controller.ControllerContext.HttpContext = contextMock.HttpContext;


            var results = await controller.AddOrUpdateOtherIncome(fakeModeltoPost);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);

        }
    }
}
