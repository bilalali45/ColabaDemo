using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TokenCacheHelper.CacheHandler;
using TokenCacheHelper.Models;
using Xunit;
using static System.Net.Mime.MediaTypeNames;

namespace TokenCacheHelper.Test
{
    public class CacheHandlerTest
    {
        [Fact]
        public async Task SetCacheItemAsyncTest()
        {
            Mock<ILogger<RedisCacheHandler>> mockCacheHandler = new Mock<ILogger<RedisCacheHandler>>();
            Mock<IDistributedCache> mockDistributedCache = new Mock<IDistributedCache>();

            var service = new RedisCacheHandler(mockCacheHandler.Object,
                                                         mockDistributedCache.Object);
            var expiredToken = new TokenData()
            {
                ValidTo = DateTime.Parse("2020-01-27"),
                ValidFrom = DateTime.Parse("2020-01-27"),
                Token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJPbmxpbmUgSldUIEJ1aWxkZXIiLCJpYXQiOjE1ODAwODMyMDAsImV4cCI6MTYwOTQ1OTIwMCwiYXVkIjoid3d3LmV4YW1wbGUuY29tIiwic3ViIjoianJvY2tldEBleGFtcGxlLmNvbSIsIlVzZXJQcm9maWxlSWQiOiIxIiwiVGVuYW50SWQiOiIxIn0.Vphx6HtU-yQAPx7oHWnOq91fLIE4coErE8xYUfdkk84"
            };
            List<TokenData> tokenList = new List<TokenData>()
                                        {
                                            expiredToken
                                        };
            service.SetCacheItemAsync<List<TokenData>>("abc", tokenList);
            //   await Task.Run(() => service.SetCacheItemAsync<List<TokenData>>("USER_1_1", tokenList));
        }

        [Fact]
        public async Task GetCacheItemAsyncTest()
        {
            Mock<ICacheHandler> mockcacheHandler = new Mock<ICacheHandler>();

            Mock<ILogger<RedisCacheHandler>> mockCacheHandler = new Mock<ILogger<RedisCacheHandler>>();
            Mock<IDistributedCache> mockDistributedCache = new Mock<IDistributedCache>();
            var expiredToken = new TokenData()
            {
                ValidTo = DateTime.Parse("2020-01-27"),
                ValidFrom = DateTime.Parse("2020-01-27"),
                Token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJPbmxpbmUgSldUIEJ1aWxkZXIiLCJpYXQiOjE1ODAwODMyMDAsImV4cCI6MTYwOTQ1OTIwMCwiYXVkIjoid3d3LmV4YW1wbGUuY29tIiwic3ViIjoianJvY2tldEBleGFtcGxlLmNvbSIsIlVzZXJQcm9maWxlSWQiOiIxIiwiVGVuYW50SWQiOiIxIn0.Vphx6HtU-yQAPx7oHWnOq91fLIE4coErE8xYUfdkk84"
            };
            List<TokenData> tokenList = new List<TokenData>()
                                        {
                                            expiredToken
                                        };
            var binaryFormatter = new BinaryFormatter();
            var memoryStream = new MemoryStream();
            binaryFormatter.Serialize(memoryStream, tokenList);
            var dataAsBytes = memoryStream.ToArray();
            mockDistributedCache.Setup(x => x.GetAsync("USER_1_1", It.IsAny<CancellationToken>())).ReturnsAsync(dataAsBytes);
            var service = new RedisCacheHandler(mockCacheHandler.Object,
                                                         mockDistributedCache.Object);


            var result = await service.GetCacheItemAsync<List<TokenData>>("USER_1_1");
            Assert.NotNull(result);
            Assert.Equal("eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJPbmxpbmUgSldUIEJ1aWxkZXIiLCJpYXQiOjE1ODAwODMyMDAsImV4cCI6MTYwOTQ1OTIwMCwiYXVkIjoid3d3LmV4YW1wbGUuY29tIiwic3ViIjoianJvY2tldEBleGFtcGxlLmNvbSIsIlVzZXJQcm9maWxlSWQiOiIxIiwiVGVuYW50SWQiOiIxIn0.Vphx6HtU-yQAPx7oHWnOq91fLIE4coErE8xYUfdkk84", result[0].Token);
        }
        [Fact]
        public async Task GetCacheItemAsyncDoesNotExistTest()
        {
            Mock<ICacheHandler> mockcacheHandler = new Mock<ICacheHandler>();

            Mock<ILogger<RedisCacheHandler>> mockCacheHandler = new Mock<ILogger<RedisCacheHandler>>();
            Mock<IDistributedCache> mockDistributedCache = new Mock<IDistributedCache>();
            var expiredToken = new TokenData()
            {
                ValidTo = DateTime.Parse("2020-01-27"),
                ValidFrom = DateTime.Parse("2020-01-27"),
                Token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJPbmxpbmUgSldUIEJ1aWxkZXIiLCJpYXQiOjE1ODAwODMyMDAsImV4cCI6MTYwOTQ1OTIwMCwiYXVkIjoid3d3LmV4YW1wbGUuY29tIiwic3ViIjoianJvY2tldEBleGFtcGxlLmNvbSIsIlVzZXJQcm9maWxlSWQiOiIxIiwiVGVuYW50SWQiOiIxIn0.Vphx6HtU-yQAPx7oHWnOq91fLIE4coErE8xYUfdkk84"
            };
            List<TokenData> tokenList = new List<TokenData>()
                                        {
                                            expiredToken
                                        };
            var binaryFormatter = new BinaryFormatter();
            var memoryStream = new MemoryStream();
            binaryFormatter.Serialize(memoryStream, tokenList);
            var dataAsBytes = memoryStream.ToArray();
            mockDistributedCache.Setup(x => x.GetAsync("USER_1_1", It.IsAny<CancellationToken>())).ReturnsAsync(() => null);
            var service = new RedisCacheHandler(mockCacheHandler.Object,
                                                         mockDistributedCache.Object);


            var result = await service.GetCacheItemAsync<List<TokenData>>("USER_1_1");
            Assert.Null(result);
        }
        [Fact]
        public async Task RemoveCacheItemAsyncDoesNotExistsTest()
        {
            Mock<ICacheHandler> mockcacheHandler = new Mock<ICacheHandler>();

            Mock<ILogger<RedisCacheHandler>> mockCacheHandler = new Mock<ILogger<RedisCacheHandler>>();
            Mock<IDistributedCache> mockDistributedCache = new Mock<IDistributedCache>();
            var expiredToken = new TokenData()
            {
                ValidTo = DateTime.Parse("2020-01-27"),
                ValidFrom = DateTime.Parse("2020-01-27"),
                Token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJPbmxpbmUgSldUIEJ1aWxkZXIiLCJpYXQiOjE1ODAwODMyMDAsImV4cCI6MTYwOTQ1OTIwMCwiYXVkIjoid3d3LmV4YW1wbGUuY29tIiwic3ViIjoianJvY2tldEBleGFtcGxlLmNvbSIsIlVzZXJQcm9maWxlSWQiOiIxIiwiVGVuYW50SWQiOiIxIn0.Vphx6HtU-yQAPx7oHWnOq91fLIE4coErE8xYUfdkk84"
            };
            List<TokenData> tokenList = new List<TokenData>()
                                        {
                                            expiredToken
                                        };
            mockDistributedCache.Setup(x => x.GetAsync("USER_1_1", It.IsAny<CancellationToken>())).ReturnsAsync(() => null);
            var service = new RedisCacheHandler(mockCacheHandler.Object,
                                                         mockDistributedCache.Object);


            var result = await service.RemoveCacheItemAsync("USER_1_1");
            Assert.False(result);
        }



        [Fact]
        public async Task RemoveCacheItemAsyncTest()
        {
            Mock<ICacheHandler> mockcacheHandler = new Mock<ICacheHandler>();

            Mock<ILogger<RedisCacheHandler>> mockCacheHandler = new Mock<ILogger<RedisCacheHandler>>();
            Mock<IDistributedCache> mockDistributedCache = new Mock<IDistributedCache>();
            var expiredToken = new TokenData()
            {
                ValidTo = DateTime.Parse("2020-01-27"),
                ValidFrom = DateTime.Parse("2020-01-27"),
                Token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJPbmxpbmUgSldUIEJ1aWxkZXIiLCJpYXQiOjE1ODAwODMyMDAsImV4cCI6MTYwOTQ1OTIwMCwiYXVkIjoid3d3LmV4YW1wbGUuY29tIiwic3ViIjoianJvY2tldEBleGFtcGxlLmNvbSIsIlVzZXJQcm9maWxlSWQiOiIxIiwiVGVuYW50SWQiOiIxIn0.Vphx6HtU-yQAPx7oHWnOq91fLIE4coErE8xYUfdkk84"
            };
            List<TokenData> tokenList = new List<TokenData>()
                                        {
                                            expiredToken
                                        };

            mockDistributedCache.SetupSequence(x => x.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(Encoding.ASCII.GetBytes("USER_1_1"));
            var service = new RedisCacheHandler(mockCacheHandler.Object,
                                                         mockDistributedCache.Object);


            var result = await service.RemoveCacheItemAsync(string.Empty);
            Assert.True(result);
        }
    }
}
