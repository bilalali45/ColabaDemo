using LoanApplication.API.Controllers;
using LoanApplication.Service;
using LoanApplicationDb.Data;
using LoanApplicationDb.Entity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TenantConfig.Common.DistributedCache;
using URF.Core.EF;
using URF.Core.EF.Factories;
using Xunit;

namespace LoanApplication.Tests
{
    public class PropertyTests
    {
        [Fact]
        public async Task GetAllPropertyTypesControllerTest()
        {
            Mock<IInfoService> mockInfoService = new Mock<IInfoService>();
            Mock<IPropertyService> mockPropertyService = new Mock<IPropertyService>();
            var controller = new PropertyController(mockInfoService.Object, mockPropertyService.Object);
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

            var result = await controller.GetAllPropertyTypes();
            //Assert
            Assert.NotNull(result);
            var res = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, res.StatusCode);
        }

        [Fact]
        public async Task GetAllPropertyUsagesControllerTest()
        {
            Mock<IInfoService> mockInfoService = new Mock<IInfoService>();
            Mock<IPropertyService> mockPropertyService = new Mock<IPropertyService>();
            var controller = new PropertyController(mockInfoService.Object, mockPropertyService.Object);
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

            var result = await controller.GetAllPropertyUsages();
            //Assert
            Assert.NotNull(result);
            var res = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, res.StatusCode);
        }
        [Fact]
        public async Task GetPropertyTypeControllerTest()
        {
            Mock<IInfoService> mockInfoService = new Mock<IInfoService>();
            Mock<IPropertyService> mockPropertyService = new Mock<IPropertyService>();
            mockPropertyService.Setup(x => x.GetPropertyType(It.IsAny<TenantModel>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new Model.PropertyTypeModel { Id = 1 });
            var controller = new PropertyController(mockInfoService.Object, mockPropertyService.Object);
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

            var result = await controller.GetPropertyType(1);
            //Assert
            Assert.NotNull(result);
            var res = Assert.IsType<OkObjectResult>(result);
            var val = Assert.IsType<Model.PropertyTypeModel>(res.Value);
            Assert.Equal(1, val.Id);
        }
        [Fact]
        public async Task AddOrUpdatePropertyTypeControllerTest()
        {
            Mock<IInfoService> mockInfoService = new Mock<IInfoService>();
            Mock<IPropertyService> mockPropertyService = new Mock<IPropertyService>();
            mockPropertyService.Setup(x => x.AddOrUpdatePropertyType(It.IsAny<TenantModel>(), It.IsAny<Model.AddPropertyTypeModel>(), It.IsAny<int>())).ReturnsAsync(true);
            var controller = new PropertyController(mockInfoService.Object, mockPropertyService.Object);
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

            var result = await controller.AddOrUpdatePropertyType(new Model.AddPropertyTypeModel { });
            //Assert
            Assert.NotNull(result);
            var res = Assert.IsType<OkObjectResult>(result);
            var val = Assert.IsType<bool>(res.Value);
            Assert.True(val);
        }
        [Fact]
        public async Task AddOrUpdatePropertyUsageControllerTest()
        {
            Mock<IInfoService> mockInfoService = new Mock<IInfoService>();
            Mock<IPropertyService> mockPropertyService = new Mock<IPropertyService>();
            mockPropertyService.Setup(x => x.AddOrUpdatePropertyUsage(It.IsAny<TenantModel>(), It.IsAny<Model.AddPropertyUsageModel>(), It.IsAny<int>())).ReturnsAsync(true);
            var controller = new PropertyController(mockInfoService.Object, mockPropertyService.Object);
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

            var result = await controller.AddOrUpdatePropertyUsage(new Model.AddPropertyUsageModel { });
            //Assert
            Assert.NotNull(result);
            var res = Assert.IsType<OkObjectResult>(result);
            var val = Assert.IsType<bool>(res.Value);
            Assert.True(val);
        }
        [Fact]
        public async Task AddOrUpdatePropertyAddressControllerTest()
        {
            Mock<IInfoService> mockInfoService = new Mock<IInfoService>();
            Mock<IPropertyService> mockPropertyService = new Mock<IPropertyService>();
            mockPropertyService.Setup(x => x.AddOrUpdatePropertyAddress(It.IsAny<TenantModel>(), It.IsAny<Model.AddPropertyAddressModel>(), It.IsAny<int>())).ReturnsAsync(true);
            var controller = new PropertyController(mockInfoService.Object, mockPropertyService.Object);
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

            var result = await controller.AddOrUpdatePropertyAddress(new Model.AddPropertyAddressModel { });
            //Assert
            Assert.NotNull(result);
            var res = Assert.IsType<OkObjectResult>(result);
            var val = Assert.IsType<bool>(res.Value);
            Assert.True(val);
        }
        [Fact]
        public async Task GetPropertyUsageControllerTest()
        {
            Mock<IInfoService> mockInfoService = new Mock<IInfoService>();
            Mock<IPropertyService> mockPropertyService = new Mock<IPropertyService>();
            mockPropertyService.Setup(x => x.GetPropertyUsage(It.IsAny<TenantModel>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new Model.GetPropertyUsageModel { PropertyUsageId = 1 });
            var controller = new PropertyController(mockInfoService.Object, mockPropertyService.Object);
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

            var result = await controller.GetPropertyUsage(1);
            //Assert
            Assert.NotNull(result);
            var res = Assert.IsType<OkObjectResult>(result);
            var val = Assert.IsType<Model.GetPropertyUsageModel>(res.Value);
            Assert.Equal(1, val.PropertyUsageId);
        }
        [Fact]
        public async Task GetPropertyAddressControllerTest()
        {
            Mock<IInfoService> mockInfoService = new Mock<IInfoService>();
            Mock<IPropertyService> mockPropertyService = new Mock<IPropertyService>();
            mockPropertyService.Setup(x => x.GetPropertyAddress(It.IsAny<TenantModel>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new Model.GetPropertyAddressModel { Id = 1 });
            var controller = new PropertyController(mockInfoService.Object, mockPropertyService.Object);
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

            var result = await controller.GetPropertyAddress(1);
            //Assert
            Assert.NotNull(result);
            var res = Assert.IsType<OkObjectResult>(result);
            var val = Assert.IsType<Model.GetPropertyAddressModel>(res.Value);
            Assert.Equal(1, val.Id);
        }
        [Fact]
        public async Task AddOrUpdatePropertyAddressTest()
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
            builder1.UseInMemoryDatabase("LoanApplicationAddOrUpdatePropertyAddress");
            options1 = builder1.Options;
            using LoanApplicationContext dataContext1 = new LoanApplicationContext(options1);
            dataContext1.Database.EnsureCreated();
            Country country = new Country
            {
                Id = 1,
                Name = "US"
            };
            dataContext1.Set<Country>().Add(country);
            PropertyInfo property = new PropertyInfo
            {
                Id = 5,
                TenantId = 5,
                AddressInfoId = 5
            };

            dataContext1.Set<PropertyInfo>().Add(property);
            LoanApplicationDb.Entity.Models.AddressInfo addressInfo = new LoanApplicationDb.Entity.Models.AddressInfo
            {
                Id = 5,
                TenantId = 5,
                CityName = "Test",
                CountryId = 1,
                StateId = 1,
            };

            dataContext1.Set<LoanApplicationDb.Entity.Models.AddressInfo>().Add(addressInfo);

            LoanApplicationDb.Entity.Models.LoanApplication loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication
            {
                Id = 5,
                UserId = 5,
                TenantId = 5,
                SubjectPropertyDetailId = 5
            };
            dataContext1.Set<LoanApplicationDb.Entity.Models.LoanApplication>().Add(loanApplication);
            dataContext1.SaveChanges();


            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();
            var service = new PropertyService(new UnitOfWork<LoanApplicationContext>(dataContext1, new RepositoryProvider(new RepositoryFactories())), Mock.Of<IServiceProvider>(), dbFunctionServiceMock.Object);

            var result = await service.AddOrUpdatePropertyAddress(model, new Model.AddPropertyAddressModel { LoanApplicationId = 5 }, 5);
            //Assert
            Assert.True(result);

        }
        [Fact]
        public async Task AddOrUpdatePropertyAddressNoResTest()
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
            builder1.UseInMemoryDatabase("LoanApplicationAddOrUpdatePropertyAddressNoResTest");
            options1 = builder1.Options;
            using LoanApplicationContext dataContext1 = new LoanApplicationContext(options1);
            dataContext1.Database.EnsureCreated();
            Country country = new Country
            {
                Id = 1,
                Name = "US"
            };
            dataContext1.Set<Country>().Add(country);
            PropertyInfo property = new PropertyInfo
            {
                Id = 5,
                TenantId = 5,
                AddressInfoId = 6
            };

            dataContext1.Set<PropertyInfo>().Add(property);
            LoanApplicationDb.Entity.Models.AddressInfo addressInfo = new LoanApplicationDb.Entity.Models.AddressInfo
            {
                Id = 5,
                TenantId = 5,
                CityName = "Test",
                CountryId = 1,
                StateId = 1,
            };

            dataContext1.Set<LoanApplicationDb.Entity.Models.AddressInfo>().Add(addressInfo);

            LoanApplicationDb.Entity.Models.LoanApplication loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication
            {
                Id = 5,
                UserId = 5,
                TenantId = 5,
                SubjectPropertyDetailId = 5
            };
            dataContext1.Set<LoanApplicationDb.Entity.Models.LoanApplication>().Add(loanApplication);
            dataContext1.SaveChanges();

            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();
            var service = new PropertyService(new UnitOfWork<LoanApplicationContext>(dataContext1, new RepositoryProvider(new RepositoryFactories())), Mock.Of<IServiceProvider>(), dbFunctionServiceMock.Object);

            var result = await service.AddOrUpdatePropertyAddress(model, new Model.AddPropertyAddressModel { LoanApplicationId = 5 }, 5);
            //Assert
            Assert.True(result);

        }

        [Fact]
        public async Task GetPropertyAddressTest()
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
            builder1.UseInMemoryDatabase("LoanApplicationGetPropertyAddressTest");
            options1 = builder1.Options;
            using LoanApplicationContext dataContext1 = new LoanApplicationContext(options1);
            dataContext1.Database.EnsureCreated();
            Country country = new Country
            {
                Id = 1,
                Name = "US"
            };
            dataContext1.Set<Country>().Add(country);
            PropertyInfo property = new PropertyInfo
            {
                Id = 5,
                TenantId = 5,
                AddressInfoId = 5
            };

            dataContext1.Set<PropertyInfo>().Add(property);
            LoanApplicationDb.Entity.Models.AddressInfo addressInfo = new LoanApplicationDb.Entity.Models.AddressInfo
            {
                Id = 5,
                TenantId = 5,
                CityName = "Test",
                CountryId = 1,
                StateId = 1,
            };

            dataContext1.Set<LoanApplicationDb.Entity.Models.AddressInfo>().Add(addressInfo);

            LoanApplicationDb.Entity.Models.LoanApplication loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication
            {
                Id = 5,
                UserId = 5,
                TenantId = 5,
                SubjectPropertyDetailId = 5
            };
            dataContext1.Set<LoanApplicationDb.Entity.Models.LoanApplication>().Add(loanApplication);
            dataContext1.SaveChanges();

            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();
            var service = new PropertyService(new UnitOfWork<LoanApplicationContext>(dataContext1, new RepositoryProvider(new RepositoryFactories())), Mock.Of<IServiceProvider>(), dbFunctionServiceMock.Object);

            var result = await service.GetPropertyAddress(model, 5, 5);
            //Assert
            Assert.Equal(5, result.Id);

        }
        [Fact]
        public async Task AddOrUpdatePropertyTypeServiceTest()
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
            builder1.UseInMemoryDatabase("LoanApplicationAddOrUpdatePropertyTypeService");
            options1 = builder1.Options;
            using LoanApplicationContext dataContext1 = new LoanApplicationContext(options1);
            dataContext1.Database.EnsureCreated();
            PropertyType property = new PropertyType
            {
                Id = 5,
                NoOfUnits = 1
            };

            dataContext1.Set<PropertyType>().Add(property);

            PropertyInfo propertyInfo = new PropertyInfo
            {
                Id = 5,
            };

            dataContext1.Set<PropertyInfo>().Add(propertyInfo);

            LoanApplicationDb.Entity.Models.LoanApplication loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication
            {
                Id = 5,
                UserId = 5,
                TenantId = 5,
                SubjectPropertyDetailId = 5
            };
            dataContext1.Set<LoanApplicationDb.Entity.Models.LoanApplication>().Add(loanApplication);
            dataContext1.SaveChanges();

            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();
            var service = new PropertyService(new UnitOfWork<LoanApplicationContext>(dataContext1, new RepositoryProvider(new RepositoryFactories())), Mock.Of<IServiceProvider>(), dbFunctionServiceMock.Object);

            var result = await service.AddOrUpdatePropertyType(model, new Model.AddPropertyTypeModel { LoanApplicationId = 5, PropertyTypeId = 5 }, 5);
            //Assert
            Assert.True(result);

        }
        [Fact]
        public async Task AddOrUpdatePropertyTypeNoResServiceTest()
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
            builder1.UseInMemoryDatabase("LoanApplicationAddOrUpdatePropertyTypeNoResService");
            options1 = builder1.Options;
            using LoanApplicationContext dataContext1 = new LoanApplicationContext(options1);
            dataContext1.Database.EnsureCreated();
            PropertyType property = new PropertyType
            {
                Id = 5,
                NoOfUnits = 1
            };

            dataContext1.Set<PropertyType>().Add(property);

            PropertyInfo propertyInfo = new PropertyInfo
            {
                Id = 5,
            };

            dataContext1.Set<PropertyInfo>().Add(propertyInfo);

            LoanApplicationDb.Entity.Models.LoanApplication loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication
            {
                Id = 5,
                UserId = 5,
                TenantId = 5,
            };
            dataContext1.Set<LoanApplicationDb.Entity.Models.LoanApplication>().Add(loanApplication);
            dataContext1.SaveChanges();

            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();
            var service = new PropertyService(new UnitOfWork<LoanApplicationContext>(dataContext1, new RepositoryProvider(new RepositoryFactories())), Mock.Of<IServiceProvider>(), dbFunctionServiceMock.Object);

            var result = await service.AddOrUpdatePropertyType(model, new Model.AddPropertyTypeModel { LoanApplicationId = 5, PropertyTypeId = 5 }, 5);
            //Assert
            Assert.True(result);

        }
        [Fact]
        public async Task GetPropertyTypeServiceTest()
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
            builder1.UseInMemoryDatabase("LoanApplicationGetPropertyTypeServiceTest");
            options1 = builder1.Options;
            using LoanApplicationContext dataContext1 = new LoanApplicationContext(options1);
            dataContext1.Database.EnsureCreated();
            PropertyType property = new PropertyType
            {
                Id = 5,
                NoOfUnits = 1
            };

            dataContext1.Set<PropertyType>().Add(property);

            PropertyInfo propertyInfo = new PropertyInfo
            {
                Id = 5,
                PropertyTypeId = 5
            };

            dataContext1.Set<PropertyInfo>().Add(propertyInfo);

            LoanApplicationDb.Entity.Models.LoanApplication loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication
            {
                Id = 5,
                UserId = 5,
                TenantId = 5,
                SubjectPropertyDetailId = 5
            };
            dataContext1.Set<LoanApplicationDb.Entity.Models.LoanApplication>().Add(loanApplication);
            dataContext1.SaveChanges();

            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();
            var service = new PropertyService(new UnitOfWork<LoanApplicationContext>(dataContext1, new RepositoryProvider(new RepositoryFactories())), Mock.Of<IServiceProvider>(), dbFunctionServiceMock.Object);

            var result = await service.GetPropertyType(model, 5, 5);
            //Assert
            Assert.Equal(5, result.Id);

        }
        [Fact]
        public async Task GetPropertyUsageServiceTest()
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
            builder1.UseInMemoryDatabase("LoanApplicationGetPropertyUsageServiceTest");
            options1 = builder1.Options;
            using LoanApplicationContext dataContext1 = new LoanApplicationContext(options1);
            dataContext1.Database.EnsureCreated();
            PropertyUsage property = new PropertyUsage
            {
                Id = 5
            };

            dataContext1.Set<PropertyUsage>().Add(property);

            PropertyInfo propertyInfo = new PropertyInfo
            {
                Id = 5,
                PropertyUsageId = 5
            };

            dataContext1.Set<PropertyInfo>().Add(propertyInfo);

            LoanApplicationDb.Entity.Models.LoanApplication loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication
            {
                Id = 5,
                UserId = 5,
                TenantId = 5,
                SubjectPropertyDetailId = 5
            };
            dataContext1.Set<LoanApplicationDb.Entity.Models.LoanApplication>().Add(loanApplication);
            dataContext1.SaveChanges();

            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();
            var service = new PropertyService(new UnitOfWork<LoanApplicationContext>(dataContext1, new RepositoryProvider(new RepositoryFactories())), Mock.Of<IServiceProvider>(), dbFunctionServiceMock.Object);

            var result = await service.GetPropertyUsage(model, 5, 5);
            //Assert
            Assert.Equal(5, result.PropertyUsageId);

        }
        [Fact]
        public async Task GetPropertyUsageServiceNoResTest()
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
            builder1.UseInMemoryDatabase("LoanApplicationGetPropertyUsageServiceNoResTest");
            options1 = builder1.Options;
            using LoanApplicationContext dataContext1 = new LoanApplicationContext(options1);
            dataContext1.Database.EnsureCreated();
            PropertyUsage property = new PropertyUsage
            {
                Id = 5
            };

            dataContext1.Set<PropertyUsage>().Add(property);

            PropertyInfo propertyInfo = new PropertyInfo
            {
                Id = 5
            };

            dataContext1.Set<PropertyInfo>().Add(propertyInfo);

            LoanApplicationDb.Entity.Models.LoanApplication loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication
            {
                Id = 5,
                UserId = 5,
                TenantId = 5,
                SubjectPropertyDetailId = 5
            };
            dataContext1.Set<LoanApplicationDb.Entity.Models.LoanApplication>().Add(loanApplication);
            dataContext1.SaveChanges();

            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();
            var service = new PropertyService(new UnitOfWork<LoanApplicationContext>(dataContext1, new RepositoryProvider(new RepositoryFactories())), Mock.Of<IServiceProvider>(), dbFunctionServiceMock.Object);

            var result = await service.GetPropertyUsage(model, 5, 5);
            //Assert
            Assert.Null(result);

        }
        [Fact]
        public async Task AddOrUpdatePropertyUsageServiceTest()
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
            builder1.UseInMemoryDatabase("LoanApplicationAddOrUpdatePropertyUsageServiceTest");
            options1 = builder1.Options;
            using LoanApplicationContext dataContext1 = new LoanApplicationContext(options1);
            dataContext1.Database.EnsureCreated();
            PropertyUsage property = new PropertyUsage
            {
                Id = 5
            };

            dataContext1.Set<PropertyUsage>().Add(property);

            PropertyInfo propertyInfo = new PropertyInfo
            {
                Id = 5,
                PropertyUsageId = 5
            };

            dataContext1.Set<PropertyInfo>().Add(propertyInfo);

            Borrower borrower = new Borrower
            {
                Id = 5,
                TenantId = 5,
                LoanApplicationId = 5,
                OwnTypeId = 2
            };

            dataContext1.Set<Borrower>().Add(borrower);

            Borrower borrower1 = new Borrower
            {
                Id = 6,
                TenantId = 5,
                LoanApplicationId = 5,
                OwnTypeId = 1
            };

            dataContext1.Set<Borrower>().Add(borrower1);

            LoanApplicationDb.Entity.Models.LoanApplication loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication
            {
                Id = 5,
                UserId = 5,
                TenantId = 5,
                SubjectPropertyDetailId = 5
            };
            dataContext1.Set<LoanApplicationDb.Entity.Models.LoanApplication>().Add(loanApplication);
            dataContext1.SaveChanges();

            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();
            var service = new PropertyService(new UnitOfWork<LoanApplicationContext>(dataContext1, new RepositoryProvider(new RepositoryFactories())), Mock.Of<IServiceProvider>(), dbFunctionServiceMock.Object);

            var result = await service.AddOrUpdatePropertyUsage(model, new Model.AddPropertyUsageModel { LoanApplicationId = 5, PropertyUsageId = 5 }, 5);
            //Assert
            Assert.True(result);

        }
        [Fact]
        public async Task AddOrUpdatePropertyUsageServicePrimaryTest()
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
            builder1.UseInMemoryDatabase("LoanApplicationAddOrUpdatePropertyUsageServicePrimaryTest");
            options1 = builder1.Options;
            using LoanApplicationContext dataContext1 = new LoanApplicationContext(options1);
            dataContext1.Database.EnsureCreated();
            PropertyUsage property = new PropertyUsage
            {
                Id = 5
            };

            dataContext1.Set<PropertyUsage>().Add(property);

            PropertyInfo propertyInfo = new PropertyInfo
            {
                Id = 5,
                PropertyUsageId = 5
            };

            dataContext1.Set<PropertyInfo>().Add(propertyInfo);

            Borrower borrower = new Borrower
            {
                Id = 5,
                TenantId = 5,
                LoanApplicationId = 5,
                OwnTypeId = 2
            };

            dataContext1.Set<Borrower>().Add(borrower);
            Borrower borrower1 = new Borrower
            {
                Id = 6,
                TenantId = 5,
                LoanApplicationId = 5,
                OwnTypeId = 1
            };

            dataContext1.Set<Borrower>().Add(borrower1);

            LoanApplicationDb.Entity.Models.LoanApplication loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication
            {
                Id = 5,
                UserId = 5,
                TenantId = 5,
                SubjectPropertyDetailId = 5
            };
            dataContext1.Set<LoanApplicationDb.Entity.Models.LoanApplication>().Add(loanApplication);
            dataContext1.SaveChanges();

            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();
            var service = new PropertyService(new UnitOfWork<LoanApplicationContext>(dataContext1, new RepositoryProvider(new RepositoryFactories())), Mock.Of<IServiceProvider>(), dbFunctionServiceMock.Object);

            var result = await service.AddOrUpdatePropertyUsage(model, new Model.AddPropertyUsageModel { LoanApplicationId = 5, PropertyUsageId = 1 }, 5);
            //Assert
            Assert.True(result);

        }
    }
}
