using Microsoft.EntityFrameworkCore;
using Moq;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TenantConfig.Common;
using TenantConfig.Data;
using TenantConfig.Entity.Models;
using URF.Core.EF;
using URF.Core.EF.Factories;
using Xunit;

namespace TenantConfig.Tests
{
    public class SettingTests
    {
        [Fact]
        public async Task SettingBranchCache()
        {
            var mockRedis = new Mock<IRedisCacheClient>();
            var mockDatabase = new Mock<IRedisDatabase>();
            mockRedis.Setup(x => x.Db0).Returns(mockDatabase.Object);
            mockDatabase.Setup(x => x.HashGetAsync<string>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<StackExchange.Redis.CommandFlags>())).ReturnsAsync("test");

            var setting = new SettingService(mockRedis.Object, null);
            var val = await setting.GetSetting<string>("",null,1,null,false);

            Assert.Equal("test",val);
        }

        [Fact]
        public async Task SettingTenantCache()
        {
            var mockRedis = new Mock<IRedisCacheClient>();
            var mockDatabase = new Mock<IRedisDatabase>();
            mockRedis.Setup(x => x.Db0).Returns(mockDatabase.Object);
            mockDatabase.Setup(x => x.HashGetAsync<string>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<StackExchange.Redis.CommandFlags>())).ReturnsAsync("test");

            var setting = new SettingService(mockRedis.Object, null);
            var val = await setting.GetSetting<string>("", 1, null, null, false);

            Assert.Equal("test", val);
        }

        [Fact]
        public async Task SettingGlobalCache()
        {
            var mockRedis = new Mock<IRedisCacheClient>();
            var mockDatabase = new Mock<IRedisDatabase>();
            mockRedis.Setup(x => x.Db0).Returns(mockDatabase.Object);
            mockDatabase.Setup(x => x.HashGetAsync<string>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<StackExchange.Redis.CommandFlags>())).ReturnsAsync("test");

            var setting = new SettingService(mockRedis.Object, null);
            var val = await setting.GetSetting<string>("", null, null, null, false);

            Assert.Equal("test", val);
        }

        [Fact]
        public async Task SettingGlobalDatabase()
        {
            var mockRedis = new Mock<IRedisCacheClient>();
            var mockDatabase = new Mock<IRedisDatabase>();
            mockRedis.Setup(x => x.Db0).Returns(mockDatabase.Object);
            mockDatabase.Setup(x => x.HashGetAsync<string>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<StackExchange.Redis.CommandFlags>())).ReturnsAsync((string)null);

            DbContextOptions<TenantConfigContext> options1;
            var builder1 = new DbContextOptionsBuilder<TenantConfigContext>();
            builder1.UseInMemoryDatabase("TenantConfig");
            options1 = builder1.Options;
            using TenantConfigContext dataContext1 = new TenantConfigContext(options1);
            dataContext1.Database.EnsureCreated();

            Setting settingEntity = new Setting
            {
                Id=1,
                IsActive=true,
                Name="test1",
                Value="test1"
            };
            dataContext1.Set<Setting>().Add(settingEntity);

            dataContext1.SaveChanges();


            var setting = new SettingService(mockRedis.Object, new UnitOfWork<TenantConfigContext>(dataContext1, new RepositoryProvider(new RepositoryFactories())));
            var val = await setting.GetSetting<string>("test1", null, null, null, false);

            Assert.Equal("test1", val);
        }

        [Fact]
        public async Task SettingTenantDatabase()
        {
            var mockRedis = new Mock<IRedisCacheClient>();
            var mockDatabase = new Mock<IRedisDatabase>();
            mockRedis.Setup(x => x.Db0).Returns(mockDatabase.Object);
            mockDatabase.Setup(x => x.HashGetAsync<string>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<StackExchange.Redis.CommandFlags>())).ReturnsAsync((string)null);

            DbContextOptions<TenantConfigContext> options1;
            var builder1 = new DbContextOptionsBuilder<TenantConfigContext>();
            builder1.UseInMemoryDatabase("TenantConfig");
            options1 = builder1.Options;
            using TenantConfigContext dataContext1 = new TenantConfigContext(options1);
            dataContext1.Database.EnsureCreated();

            Setting settingEntity = new Setting
            {
                Id = 2,
                IsActive = true,
                Name = "test2",
                Value = "test2",
                TenantId=1
            };
            dataContext1.Set<Setting>().Add(settingEntity);

            dataContext1.SaveChanges();


            var setting = new SettingService(mockRedis.Object, new UnitOfWork<TenantConfigContext>(dataContext1, new RepositoryProvider(new RepositoryFactories())));
            var val = await setting.GetSetting<string>("test2", 1, null, null, false);

            Assert.Equal("test2", val);
        }

        [Fact]
        public async Task SettingBranchDatabase()
        {
            var mockRedis = new Mock<IRedisCacheClient>();
            var mockDatabase = new Mock<IRedisDatabase>();
            mockRedis.Setup(x => x.Db0).Returns(mockDatabase.Object);
            mockDatabase.Setup(x => x.HashGetAsync<string>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<StackExchange.Redis.CommandFlags>())).ReturnsAsync((string)null);

            DbContextOptions<TenantConfigContext> options1;
            var builder1 = new DbContextOptionsBuilder<TenantConfigContext>();
            builder1.UseInMemoryDatabase("TenantConfig");
            options1 = builder1.Options;
            using TenantConfigContext dataContext1 = new TenantConfigContext(options1);
            dataContext1.Database.EnsureCreated();

            Setting settingEntity = new Setting
            {
                Id = 3,
                IsActive = true,
                Name = "test3",
                Value = "test3",
                BranchId = 1
            };
            dataContext1.Set<Setting>().Add(settingEntity);

            dataContext1.SaveChanges();


            var setting = new SettingService(mockRedis.Object, new UnitOfWork<TenantConfigContext>(dataContext1, new RepositoryProvider(new RepositoryFactories())));
            var val = await setting.GetSetting<string>("test3", null, 1, null, false);

            Assert.Equal("test3", val);
        }
    }

    public class StringResourceTests
    {
        [Fact]
        public async Task StringResourceBranchCache()
        {
            var mockRedis = new Mock<IRedisCacheClient>();
            var mockDatabase = new Mock<IRedisDatabase>();
            mockRedis.Setup(x => x.Db0).Returns(mockDatabase.Object);
            mockDatabase.Setup(x => x.HashGetAsync<string>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<StackExchange.Redis.CommandFlags>())).ReturnsAsync("test");

            var setting = new StringResourceService(mockRedis.Object, null);
            var val = await setting.GetString("", null, 1, null, false);

            Assert.Equal("test", val);
        }

        [Fact]
        public async Task StringResourceTenantCache()
        {
            var mockRedis = new Mock<IRedisCacheClient>();
            var mockDatabase = new Mock<IRedisDatabase>();
            mockRedis.Setup(x => x.Db0).Returns(mockDatabase.Object);
            mockDatabase.Setup(x => x.HashGetAsync<string>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<StackExchange.Redis.CommandFlags>())).ReturnsAsync("test");

            var setting = new StringResourceService(mockRedis.Object, null);
            var val = await setting.GetString("", 1, null, null, false);

            Assert.Equal("test", val);
        }

        [Fact]
        public async Task StringResourceGlobalCache()
        {
            var mockRedis = new Mock<IRedisCacheClient>();
            var mockDatabase = new Mock<IRedisDatabase>();
            mockRedis.Setup(x => x.Db0).Returns(mockDatabase.Object);
            mockDatabase.Setup(x => x.HashGetAsync<string>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<StackExchange.Redis.CommandFlags>())).ReturnsAsync("test");

            var setting = new StringResourceService(mockRedis.Object, null);
            var val = await setting.GetString("", null, null, null, false);

            Assert.Equal("test", val);
        }

        [Fact]
        public async Task StringResourceGlobalDatabase()
        {
            var mockRedis = new Mock<IRedisCacheClient>();
            var mockDatabase = new Mock<IRedisDatabase>();
            mockRedis.Setup(x => x.Db0).Returns(mockDatabase.Object);
            mockDatabase.Setup(x => x.HashGetAsync<string>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<StackExchange.Redis.CommandFlags>())).ReturnsAsync((string)null);

            DbContextOptions<TenantConfigContext> options1;
            var builder1 = new DbContextOptionsBuilder<TenantConfigContext>();
            builder1.UseInMemoryDatabase("TenantConfig");
            options1 = builder1.Options;
            using TenantConfigContext dataContext1 = new TenantConfigContext(options1);
            dataContext1.Database.EnsureCreated();

            StringResource settingEntity = new StringResource
            {
                Id = 1,
                IsActive = true,
                Name = "test1",
                Value = "test1"
            };
            dataContext1.Set<StringResource>().Add(settingEntity);

            dataContext1.SaveChanges();


            var setting = new StringResourceService(mockRedis.Object, new UnitOfWork<TenantConfigContext>(dataContext1, new RepositoryProvider(new RepositoryFactories())));
            var val = await setting.GetString("test1", null, null, null, false);

            Assert.Equal("test1", val);
        }

        [Fact]
        public async Task StringResourceTenantDatabase()
        {
            var mockRedis = new Mock<IRedisCacheClient>();
            var mockDatabase = new Mock<IRedisDatabase>();
            mockRedis.Setup(x => x.Db0).Returns(mockDatabase.Object);
            mockDatabase.Setup(x => x.HashGetAsync<string>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<StackExchange.Redis.CommandFlags>())).ReturnsAsync((string)null);

            DbContextOptions<TenantConfigContext> options1;
            var builder1 = new DbContextOptionsBuilder<TenantConfigContext>();
            builder1.UseInMemoryDatabase("TenantConfig");
            options1 = builder1.Options;
            using TenantConfigContext dataContext1 = new TenantConfigContext(options1);
            dataContext1.Database.EnsureCreated();

            StringResource settingEntity = new StringResource
            {
                Id = 2,
                IsActive = true,
                Name = "test2",
                Value = "test2",
                TenantId = 1
            };
            dataContext1.Set<StringResource>().Add(settingEntity);

            dataContext1.SaveChanges();


            var setting = new StringResourceService(mockRedis.Object, new UnitOfWork<TenantConfigContext>(dataContext1, new RepositoryProvider(new RepositoryFactories())));
            var val = await setting.GetString("test2", 1, null, null, false);

            Assert.Equal("test2", val);
        }

        [Fact]
        public async Task StringResourceBranchDatabase()
        {
            var mockRedis = new Mock<IRedisCacheClient>();
            var mockDatabase = new Mock<IRedisDatabase>();
            mockRedis.Setup(x => x.Db0).Returns(mockDatabase.Object);
            mockDatabase.Setup(x => x.HashGetAsync<string>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<StackExchange.Redis.CommandFlags>())).ReturnsAsync((string)null);

            DbContextOptions<TenantConfigContext> options1;
            var builder1 = new DbContextOptionsBuilder<TenantConfigContext>();
            builder1.UseInMemoryDatabase("TenantConfig");
            options1 = builder1.Options;
            using TenantConfigContext dataContext1 = new TenantConfigContext(options1);
            dataContext1.Database.EnsureCreated();

            StringResource settingEntity = new StringResource
            {
                Id = 3,
                IsActive = true,
                Name = "test3",
                Value = "test3",
                BranchId = 1
            };
            dataContext1.Set<StringResource>().Add(settingEntity);

            dataContext1.SaveChanges();


            var setting = new StringResourceService(mockRedis.Object, new UnitOfWork<TenantConfigContext>(dataContext1, new RepositoryProvider(new RepositoryFactories())));
            var val = await setting.GetString("test3", null, 1, null, false);

            Assert.Equal("test3", val);
        }
    }
}
