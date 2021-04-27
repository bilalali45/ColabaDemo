using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core.Abstractions;
using TokenCacheHelper.Models;
using TokenCacheHelper.TokenManager;
using Xunit;


namespace TokenCacheHelper.Test.TokenManager
{
    public class TokenManagerTests
    {
        [Fact]
        public async Task AddAuthTokenToWhiteList_AddingValidToken_True()
        {
            var mockcacheHandler = new Mock<IRedisCacheClient>();
            var mockLoggerHandler = new Mock<ILogger<TokenCacheHelper.TokenManager.TokenManager>>();

            mockcacheHandler.Setup(expression: x => x.Db0.HashSetAsync("USER_8408_1",
                                                                       new Dictionary<string, TokenData>(),
                                                                       CommandFlags.None));
            mockcacheHandler.Setup(expression: x => x.Db0.UpdateExpiryAsync("USER_8408_1",
                                                                            DateTimeOffset.Now,
                                                                            CommandFlags.None));

            var service = new TokenCacheHelper.TokenManager.TokenManager(logger: mockLoggerHandler.Object,
                                                                         cacheHandler: mockcacheHandler.Object);

            var validToken = new TokenData
                             {
                                 ValidTo = DateTime.Parse(s: "2022-01-28"),
                                 ValidFrom = DateTime.Parse(s: "2021-01-27"),
                                 Token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJPbmxpbmUgSldUIEJ1aWxkZXIiLCJpYXQiOjE2MTE3MDU2MDAsImV4cCI6MTY0MzMyODAwMCwiYXVkIjoid3d3LmV4YW1wbGUuY29tIiwic3ViIjoianJvY2tldEBleGFtcGxlLmNvbSIsIlVzZXJQcm9maWxlSWQiOiIxIiwiVGVuYW50SWQiOiIxIn0.WbBiv5AU1V5ivTXH_4hF_wcSVM-AYlp783YXP0NBJFM",
                                 RefreshToken = "refreshToken"
                             };
            var result = await service.AddAuthTokenToWhiteListAsync(tokenData: validToken);
            Assert.True(condition: result);
        }


        [Fact]
        public async Task AddAuthTokenToWhiteList_AddingNullToken_Fail()
        {
            var mockCacheHandler = new Mock<IRedisCacheClient>();
            var mockLoggerHandler = new Mock<ILogger<TokenCacheHelper.TokenManager.TokenManager>>();

            var service = new TokenCacheHelper.TokenManager.TokenManager(logger: mockLoggerHandler.Object,
                                                                         cacheHandler: mockCacheHandler.Object);
            var result = await service.AddAuthTokenToWhiteListAsync(tokenData: null);
            Assert.False(condition: result);
        }


        [Fact]
        public async Task FindUserTokenAsyncDoesNotExistsTest()
        {
            var mockcacheHandler = new Mock<IRedisCacheClient>();
            var mockLoggerHandler = new Mock<ILogger<TokenCacheHelper.TokenManager.TokenManager>>();

            mockcacheHandler.Setup(expression: x => x.Db0.HashGetAllAsync<TokenData>("USER_8408_1",
                                                                                     CommandFlags.None)).ReturnsAsync(value: new Dictionary<string, TokenData>());

            var service = new TokenCacheHelper.TokenManager.TokenManager(logger: mockLoggerHandler.Object,
                                                                         cacheHandler: mockcacheHandler.Object);

            var validToken = new TokenData
                             {
                                 ValidTo = DateTime.Parse(s: "2022-01-28"),
                                 ValidFrom = DateTime.Parse(s: "2021-01-27"),
                                 Token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJPbmxpbmUgSldUIEJ1aWxkZXIiLCJpYXQiOjE2MTE3MDU2MDAsImV4cCI6MTY0MzMyODAwMCwiYXVkIjoid3d3LmV4YW1wbGUuY29tIiwic3ViIjoianJvY2tldEBleGFtcGxlLmNvbSIsIlVzZXJQcm9maWxlSWQiOiIxIiwiVGVuYW50SWQiOiIxIn0.WbBiv5AU1V5ivTXH_4hF_wcSVM-AYlp783YXP0NBJFM"
                             };

            var result = await service.FindUserTokenAsync(token: validToken.Token);
            Assert.Null(@object: result);
        }


        [Fact]
        public async Task FindUserTokenAsyncTest()
        {
            var mockcacheHandler = new Mock<IRedisCacheClient>();
            var mockLoggerHandler = new Mock<ILogger<TokenCacheHelper.TokenManager.TokenManager>>();

            var dictionary = new Dictionary<string, TokenData>();
            var tokenData = new TokenData
                            {
                                ValidTo = DateTime.Now.AddDays(value: 1),
                                RefreshToken = "abc",
                                Token = "abc",
                                UserName = "abc",
                                UserProfileId = 1,
                                ValidFrom = DateTime.Now.AddDays(value: -1)
                            };

            dictionary.Add(key: "abc",
                           value: tokenData);

            mockcacheHandler.Setup(expression: x => x.Db0.HashGetAllAsync<TokenData>("USER_8408_1",
                                                                                     CommandFlags.None)).ReturnsAsync(value: dictionary);

            var service = new TokenCacheHelper.TokenManager.TokenManager(logger: mockLoggerHandler.Object,
                                                                         cacheHandler: mockcacheHandler.Object);

            var validToken = new TokenData
                             {
                                 ValidTo = DateTime.Parse(s: "2022-01-28"),
                                 ValidFrom = DateTime.Parse(s: "2021-01-27"),
                                 Token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJPbmxpbmUgSldUIEJ1aWxkZXIiLCJpYXQiOjE2MTE3MDU2MDAsImV4cCI6MTY0MzMyODAwMCwiYXVkIjoid3d3LmV4YW1wbGUuY29tIiwic3ViIjoianJvY2tldEBleGFtcGxlLmNvbSIsIlVzZXJQcm9maWxlSWQiOiIxIiwiVGVuYW50SWQiOiIxIn0.WbBiv5AU1V5ivTXH_4hF_wcSVM-AYlp783YXP0NBJFM"
                             };

            var result = await service.FindUserTokenAsync(token: validToken.Token);
            Assert.Null(@object: result);
        }


        [Fact]
        public async Task HasExpiredAsyncDoesNotExistsTest()
        {
            var mockcacheHandler = new Mock<IRedisCacheClient>();
            var mockLoggerHandler = new Mock<ILogger<TokenCacheHelper.TokenManager.TokenManager>>();

            var validToken = new TokenData
                             {
                                 ValidTo = DateTime.Now.AddDays(value: -1),
                                 ValidFrom = DateTime.Parse(s: "2021-01-27"),
                                 Token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJPbmxpbmUgSldUIEJ1aWxkZXIiLCJpYXQiOjE2MTE3MDU2MDAsImV4cCI6MTY0MzMyODAwMCwiYXVkIjoid3d3LmV4YW1wbGUuY29tIiwic3ViIjoianJvY2tldEBleGFtcGxlLmNvbSIsIlVzZXJQcm9maWxlSWQiOiIxIiwiVGVuYW50SWQiOiIxIn0.WbBiv5AU1V5ivTXH_4hF_wcSVM-AYlp783YXP0NBJFM"
                             };

            mockcacheHandler.Setup(expression: x => x.Db0.HashGetAllAsync<TokenData>("USER_8408_1",
                                                                                     CommandFlags.None)).ReturnsAsync(value: new Dictionary<string, TokenData>());

            var service = new TokenCacheHelper.TokenManager.TokenManager(logger: mockLoggerHandler.Object,
                                                                         cacheHandler: mockcacheHandler.Object);

            var result = await service.HasExpiredAsync(token: validToken.Token);
            Assert.False(condition: result);
        }


        [Fact]
        public async Task HasExpiredAsyncTest()
        {
            var mockcacheHandler = new Mock<IRedisCacheClient>();
            var mockLoggerHandler = new Mock<ILogger<TokenCacheHelper.TokenManager.TokenManager>>();

            var dictionary = new Dictionary<string, TokenData>();
            var expiredToken = new TokenData
                               {
                                   ValidTo = DateTime.Parse(s: "2020-01-27"),
                                   ValidFrom = DateTime.Parse(s: "2020-01-27"),
                                   Token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJPbmxpbmUgSldUIEJ1aWxkZXIiLCJpYXQiOjE1ODAwODMyMDAsImV4cCI6MTYwOTQ1OTIwMCwiYXVkIjoid3d3LmV4YW1wbGUuY29tIiwic3ViIjoianJvY2tldEBleGFtcGxlLmNvbSIsIlVzZXJQcm9maWxlSWQiOiIxIiwiVGVuYW50SWQiOiIxIn0.Vphx6HtU-yQAPx7oHWnOq91fLIE4coErE8xYUfdkk84",
                                   RefreshToken = "abc"
                               };

            dictionary.Add(key: "abc",
                           value: expiredToken);

            mockcacheHandler.Setup(expression: x => x.Db0.HashGetAllAsync<TokenData>("USER_1_1",
                                                                                     CommandFlags.None)).ReturnsAsync(value: dictionary);


            var service = new TokenCacheHelper.TokenManager.TokenManager(logger: mockLoggerHandler.Object,
                                                                         cacheHandler: mockcacheHandler.Object);

            var result = await service.HasExpiredAsync(token: expiredToken.Token);
            Assert.True(condition: result);
        }


        [Fact]
        public async Task RemoveUserToken_TokenDoesNotExists_False()
        {
            var mockcacheHandler = new Mock<IRedisCacheClient>();
            var mockLoggerHandler = new Mock<ILogger<TokenCacheHelper.TokenManager.TokenManager>>();

            mockcacheHandler.Setup(expression: x => x.Db0.HashGetAllAsync<TokenData>("USER_8408_1",
                                                                                     CommandFlags.None)).ReturnsAsync(value: new Dictionary<string, TokenData>());

            var service = new TokenCacheHelper.TokenManager.TokenManager(logger: mockLoggerHandler.Object,
                                                                         cacheHandler: mockcacheHandler.Object);

            var validToken = new TokenData
                             {
                                 ValidTo = DateTime.Parse(s: "2022-01-28"),
                                 ValidFrom = DateTime.Parse(s: "2021-01-27"),
                                 Token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJPbmxpbmUgSldUIEJ1aWxkZXIiLCJpYXQiOjE2MTE3MDU2MDAsImV4cCI6MTY0MzMyODAwMCwiYXVkIjoid3d3LmV4YW1wbGUuY29tIiwic3ViIjoianJvY2tldEBleGFtcGxlLmNvbSIsIlVzZXJQcm9maWxlSWQiOiIxIiwiVGVuYW50SWQiOiIxIn0.WbBiv5AU1V5ivTXH_4hF_wcSVM-AYlp783YXP0NBJFM"
                             };

            var result = await service.RemoveUserTokenAsync(token: validToken.Token);
            Assert.False(condition: result);
        }


        [Fact]
        public async Task RemoveUserToken_TokenExists_True()
        {
            var mockCacheHandler = new Mock<IRedisCacheClient>();
            var mockLoggerHandler = new Mock<ILogger<TokenCacheHelper.TokenManager.TokenManager>>();

            var dictionary = new Dictionary<string, TokenData>();

            var tokenData = new TokenData
                            {
                                ValidTo = DateTime.Parse(s: "2022-01-28"),
                                ValidFrom = DateTime.Parse(s: "2021-01-27"),
                                Token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJPbmxpbmUgSldUIEJ1aWxkZXIiLCJpYXQiOjE2MTE3MDU2MDAsImV4cCI6MTY0MzMyODAwMCwiYXVkIjoid3d3LmV4YW1wbGUuY29tIiwic3ViIjoianJvY2tldEBleGFtcGxlLmNvbSIsIlVzZXJQcm9maWxlSWQiOiIxIiwiVGVuYW50SWQiOiIxIn0.WbBiv5AU1V5ivTXH_4hF_wcSVM-AYlp783YXP0NBJFM",
                                RefreshToken = "refreshToken"
                            };

            dictionary.Add(key: "refreshToken",
                           value: tokenData);

            mockCacheHandler.Setup(expression: x => x.Db0.HashGetAllAsync<TokenData>("USER_1_1",
                                                                                     CommandFlags.None)).ReturnsAsync(value: dictionary);

            mockCacheHandler.Setup(expression: x => x.Db0.HashDeleteAsync("USER_1_1",
                                                                          tokenData.RefreshToken,
                                                                          CommandFlags.None)).ReturnsAsync(value: true);

            mockCacheHandler.Setup(expression: x => x.Db1.RemoveAsync(tokenData.RefreshToken,
                                                                      CommandFlags.None)).ReturnsAsync(value: true);

            var service = new TokenCacheHelper.TokenManager.TokenManager(logger: mockLoggerHandler.Object,
                                                                         cacheHandler: mockCacheHandler.Object);

            var result = await service.RemoveUserTokenAsync(token: tokenData.Token);

            Assert.True(condition: result);
        }


        [Fact]
        public async Task RevokeAll_TokenDoesNotExists_False()
        {
            var mockCacheHandler = new Mock<IRedisCacheClient>();
            var mockLoggerHandler = new Mock<ILogger<TokenCacheHelper.TokenManager.TokenManager>>();

            var dictionary = new Dictionary<string, TokenData>();

            dictionary.Add(key: "refreshToken1",
                           value: new TokenData
                                  {
                                      ValidTo = DateTime.Parse(s: "2022-01-28"),
                                      ValidFrom = DateTime.Parse(s: "2021-01-27"),
                                      Token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJPbmxpbmUgSldUIEJ1aWxkZXIiLCJpYXQiOjE2MTE3MDU2MDAsImV4cCI6MTY0MzMyODAwMCwiYXVkIjoid3d3LmV4YW1wbGUuY29tIiwic3ViIjoianJvY2tldEBleGFtcGxlLmNvbSIsIlVzZXJQcm9maWxlSWQiOiIxIiwiVGVuYW50SWQiOiIxIn0.WbBiv5AU1V5ivTXH_4hF_wcSVM-AYlp783YXP0NBJFM",
                                      RefreshToken = "refreshToken"
                                  });


            mockCacheHandler.Setup(expression: x => x.Db0.HashGetAllAsync<TokenData>("USER_1_1",
                                                                                     CommandFlags.None)).ReturnsAsync(value: new Dictionary<string, TokenData>());

            mockCacheHandler.Setup(expression: x => x.Db1.RemoveAllAsync(dictionary.Select(pair => pair.Key).ToList(),
                                                                         CommandFlags.None));

            mockCacheHandler.Setup(expression: x => x.Db0.RemoveAsync("USER_1_1",
                                                                      CommandFlags.None)).ReturnsAsync(value: true);

            var service = new TokenCacheHelper.TokenManager.TokenManager(logger: mockLoggerHandler.Object,
                                                                         cacheHandler: mockCacheHandler.Object);

            var results = await service.RevokeAllToken(token: dictionary[key: "refreshToken1"].Token);

            Assert.True(condition: results);
        }


        [Fact]
        public async Task RevokeAll_TokenExists_True()
        {
            var mockCacheHandler = new Mock<IRedisCacheClient>();
            var mockLoggerHandler = new Mock<ILogger<TokenCacheHelper.TokenManager.TokenManager>>();

            var dictionary = new Dictionary<string, TokenData>();

            dictionary.Add(key: "refreshToken1",
                           value: new TokenData
                                  {
                                      ValidTo = DateTime.Parse(s: "2022-01-28"),
                                      ValidFrom = DateTime.Parse(s: "2021-01-27"),
                                      Token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJPbmxpbmUgSldUIEJ1aWxkZXIiLCJpYXQiOjE2MTE3MDU2MDAsImV4cCI6MTY0MzMyODAwMCwiYXVkIjoid3d3LmV4YW1wbGUuY29tIiwic3ViIjoianJvY2tldEBleGFtcGxlLmNvbSIsIlVzZXJQcm9maWxlSWQiOiIxIiwiVGVuYW50SWQiOiIxIn0.WbBiv5AU1V5ivTXH_4hF_wcSVM-AYlp783YXP0NBJFM",
                                      RefreshToken = "refreshToken"
                                  });

            dictionary.Add(key: "refreshToken2",
                           value: new TokenData
                                  {
                                      ValidTo = DateTime.Parse(s: "2022-01-29"),
                                      ValidFrom = DateTime.Parse(s: "2021-01-28"),
                                      Token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJPbmxpbmUgSldUIEJ1aWxkZXIiLCJpYXQiOjE2MTE3MDU2MDAsImV4cCI6MTY0MzMyODAwMCwiYXVkIjoid3d3LmV4YW1wbGUuY29tIiwic3ViIjoianJvY2tldEBleGFtcGxlLmNvbSIsIlVzZXJQcm9maWxlSWQiOiIxIiwiVGVuYW50SWQiOiIxIn0.WbBiv5AU1V5ivTXH_4hF_wcSVM-AYlp783YXP0NBJFN",
                                      RefreshToken = "refreshToken"
                                  });

            mockCacheHandler.Setup(expression: x => x.Db0.HashGetAllAsync<TokenData>("USER_1_1",
                                                                                     CommandFlags.None)).ReturnsAsync(value: dictionary);

            mockCacheHandler.Setup(expression: x => x.Db1.RemoveAllAsync(dictionary.Select(pair => pair.Key).ToList(),
                                                                         CommandFlags.None));

            mockCacheHandler.Setup(expression: x => x.Db0.RemoveAsync("USER_1_1",
                                                                      CommandFlags.None)).ReturnsAsync(value: true);

            var service = new TokenCacheHelper.TokenManager.TokenManager(logger: mockLoggerHandler.Object,
                                                                         cacheHandler: mockCacheHandler.Object);

            var results = await service.RevokeAllToken(token: dictionary[key: "refreshToken1"].Token);

            Assert.True(condition: results);
        }


        [Fact]
        public async Task RevokeToken_TokenDoesNotExists_False()
        {
            var mockCacheHandler = new Mock<IRedisCacheClient>();
            var mockLoggerHandler = new Mock<ILogger<TokenCacheHelper.TokenManager.TokenManager>>();

            var dictionary = new Dictionary<string, TokenData>();
            var tokenData = new TokenData
                            {
                                ValidTo = DateTime.Parse(s: "2020-01-27"),
                                ValidFrom = DateTime.Parse(s: "2020-01-27"),
                                Token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJPbmxpbmUgSldUIEJ1aWxkZXIiLCJpYXQiOjE1ODAwODMyMDAsImV4cCI6MTYwOTQ1OTIwMCwiYXVkIjoid3d3LmV4YW1wbGUuY29tIiwic3ViIjoianJvY2tldEBleGFtcGxlLmNvbSIsIlVzZXJQcm9maWxlSWQiOiIxIiwiVGVuYW50SWQiOiIxIn0.Vphx6HtU-yQAPx7oHWnOq91fLIE4coErE8xYUfdkk84",
                                RefreshToken = "abc"
                            };

            dictionary.Add(key: "abc",
                           value: tokenData);

            mockCacheHandler.Setup(expression: x => x.Db0.HashGetAllAsync<TokenData>("USER_1_1",
                                                                                     CommandFlags.None)).ReturnsAsync(value: new Dictionary<string, TokenData>());

            mockCacheHandler.Setup(expression: x => x.Db0.HashDeleteAsync("USER_1_1",
                                                                          tokenData.RefreshToken,
                                                                          CommandFlags.None)).ReturnsAsync(value: false);

            var service = new TokenCacheHelper.TokenManager.TokenManager(logger: mockLoggerHandler.Object,
                                                                         cacheHandler: mockCacheHandler.Object);
            var result = await service.RevokeToken(token: tokenData.Token);
            Assert.False(condition: result);
        }


        [Fact]
        public async Task RevokeToken_TokenExists_True()
        {
            var mockCacheHandler = new Mock<IRedisCacheClient>();
            var mockLoggerHandler = new Mock<ILogger<TokenCacheHelper.TokenManager.TokenManager>>();

            var dictionary = new Dictionary<string, TokenData>();
            var tokenData = new TokenData
                            {
                                ValidTo = DateTime.Parse(s: "2020-01-27"),
                                ValidFrom = DateTime.Parse(s: "2020-01-27"),
                                Token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJPbmxpbmUgSldUIEJ1aWxkZXIiLCJpYXQiOjE1ODAwODMyMDAsImV4cCI6MTYwOTQ1OTIwMCwiYXVkIjoid3d3LmV4YW1wbGUuY29tIiwic3ViIjoianJvY2tldEBleGFtcGxlLmNvbSIsIlVzZXJQcm9maWxlSWQiOiIxIiwiVGVuYW50SWQiOiIxIn0.Vphx6HtU-yQAPx7oHWnOq91fLIE4coErE8xYUfdkk84",
                                RefreshToken = "abc"
                            };

            dictionary.Add(key: "abc",
                           value: tokenData);

            mockCacheHandler.Setup(expression: x => x.Db0.HashGetAllAsync<TokenData>("USER_1_1",
                                                                                     CommandFlags.None)).ReturnsAsync(value: dictionary);

            mockCacheHandler.Setup(expression: x => x.Db0.HashDeleteAsync("USER_1_1",
                                                                          tokenData.RefreshToken,
                                                                          CommandFlags.None)).ReturnsAsync(value: true);

            mockCacheHandler.Setup(expression: x => x.Db1.RemoveAsync(tokenData.RefreshToken,
                                                                      CommandFlags.None)).ReturnsAsync(value: true);

            var service = new TokenCacheHelper.TokenManager.TokenManager(logger: mockLoggerHandler.Object,
                                                                         cacheHandler: mockCacheHandler.Object);
            var result = await service.RevokeToken(token: tokenData.Token);
            Assert.True(condition: result);
        }


        [Fact]
        public async Task GetForSignOn_TokenExists_Token()
        {
            var mockCacheHandler = new Mock<IRedisCacheClient>();
            var mockLoggerHandler = new Mock<ILogger<TokenCacheHelper.TokenManager.TokenManager>>();

            var tokenData = new TokenData
                            {
                                ValidTo = DateTime.Parse(s: "2020-01-27"),
                                ValidFrom = DateTime.Parse(s: "2020-01-27"),
                                Token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJPbmxpbmUgSldUIEJ1aWxkZXIiLCJpYXQiOjE1ODAwODMyMDAsImV4cCI6MTYwOTQ1OTIwMCwiYXVkIjoid3d3LmV4YW1wbGUuY29tIiwic3ViIjoianJvY2tldEBleGFtcGxlLmNvbSIsIlVzZXJQcm9maWxlSWQiOiIxIiwiVGVuYW50SWQiOiIxIn0.Vphx6HtU-yQAPx7oHWnOq91fLIE4coErE8xYUfdkk84",
                                RefreshToken = "refreshToken"
                            };

            mockCacheHandler.Setup(expression: x => x.Db1.GetAsync<TokenData>(tokenData.RefreshToken,
                                                                              CommandFlags.None)).ReturnsAsync(value: tokenData);

            var service = new TokenCacheHelper.TokenManager.TokenManager(logger: mockLoggerHandler.Object,
                                                                         cacheHandler: mockCacheHandler.Object);

            var result = await service.GetForSignOn(refreshToken: tokenData.RefreshToken);
            Assert.Equal(expected: tokenData,
                         actual: result);
        }


        [Fact]
        public async Task AddRefreshTokenToken_Called_True()
        {
            var mockCacheHandler = new Mock<IRedisCacheClient>();
            var mockLoggerHandler = new Mock<ILogger<TokenCacheHelper.TokenManager.TokenManager>>();

            var tokenData = new TokenData
                            {
                                ValidTo = DateTime.Parse(s: "2020-01-27"),
                                ValidFrom = DateTime.Parse(s: "2020-01-27"),
                                Token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJPbmxpbmUgSldUIEJ1aWxkZXIiLCJpYXQiOjE1ODAwODMyMDAsImV4cCI6MTYwOTQ1OTIwMCwiYXVkIjoid3d3LmV4YW1wbGUuY29tIiwic3ViIjoianJvY2tldEBleGFtcGxlLmNvbSIsIlVzZXJQcm9maWxlSWQiOiIxIiwiVGVuYW50SWQiOiIxIn0.Vphx6HtU-yQAPx7oHWnOq91fLIE4coErE8xYUfdkk84",
                                RefreshToken = "refreshToken",
                                RefreshTokenValidTo = DateTime.Parse(s: "2020-01-28"),
            };

            mockCacheHandler.Setup(expression: x => x.Db1.GetAsync<TokenData>(tokenData.RefreshToken,
                                                                              CommandFlags.None)).ReturnsAsync(value: tokenData);

            var service = new TokenCacheHelper.TokenManager.TokenManager(logger: mockLoggerHandler.Object,
                                                                         cacheHandler: mockCacheHandler.Object);

            var result = await service.AddRefreshTokenTokenAsync(tokenData: tokenData);

            Assert.True(condition: result);
        }


        [Fact]
        public async Task AddRefreshTokenToken_AddingNullRefreshToken_False()
        {
            var mockCacheHandler = new Mock<IRedisCacheClient>();
            var mockLoggerHandler = new Mock<ILogger<TokenCacheHelper.TokenManager.TokenManager>>();

            mockCacheHandler.Setup(expression: x => x.Db1.GetAsync<TokenData>(null,
                                                                              CommandFlags.None)).ReturnsAsync(value: null);

            var service = new TokenCacheHelper.TokenManager.TokenManager(logger: mockLoggerHandler.Object,
                                                                         cacheHandler: mockCacheHandler.Object);

            var result = await service.AddRefreshTokenTokenAsync(tokenData: null);

            Assert.False(condition: result);
        }


        [Fact]
        public async Task CheckPair_Exists_True()
        {
            var mockCacheHandler = new Mock<IRedisCacheClient>();
            var mockLoggerHandler = new Mock<ILogger<TokenCacheHelper.TokenManager.TokenManager>>();

            var tokenData = new TokenData
                            {
                                ValidTo = DateTime.Parse(s: "2020-01-27"),
                                ValidFrom = DateTime.Parse(s: "2020-01-27"),
                                Token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJPbmxpbmUgSldUIEJ1aWxkZXIiLCJpYXQiOjE1ODAwODMyMDAsImV4cCI6MTYwOTQ1OTIwMCwiYXVkIjoid3d3LmV4YW1wbGUuY29tIiwic3ViIjoianJvY2tldEBleGFtcGxlLmNvbSIsIlVzZXJQcm9maWxlSWQiOiIxIiwiVGVuYW50SWQiOiIxIn0.Vphx6HtU-yQAPx7oHWnOq91fLIE4coErE8xYUfdkk84",
                                RefreshToken = "refreshToken"
                            };

            mockCacheHandler.Setup(expression: x => x.Db0.HashGetAsync<TokenData>("USER_1_1",
                                                                                  tokenData.RefreshToken,
                                                                                  CommandFlags.None)).ReturnsAsync(value: tokenData);

            var service = new TokenCacheHelper.TokenManager.TokenManager(logger: mockLoggerHandler.Object,
                                                                         cacheHandler: mockCacheHandler.Object);

            var result = await service.CheckPair(token: tokenData.Token,
                                                 refreshToken: tokenData.RefreshToken);

            Assert.True(condition: result);
        }

        [Fact]
        public async Task CheckPair_DoesNotExists_False()
        {
            var mockCacheHandler = new Mock<IRedisCacheClient>();
            var mockLoggerHandler = new Mock<ILogger<TokenCacheHelper.TokenManager.TokenManager>>();

            var tokenData = new TokenData
                            {
                                ValidTo = DateTime.Parse(s: "2020-01-27"),
                                ValidFrom = DateTime.Parse(s: "2020-01-27"),
                                Token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJPbmxpbmUgSldUIEJ1aWxkZXIiLCJpYXQiOjE1ODAwODMyMDAsImV4cCI6MTYwOTQ1OTIwMCwiYXVkIjoid3d3LmV4YW1wbGUuY29tIiwic3ViIjoianJvY2tldEBleGFtcGxlLmNvbSIsIlVzZXJQcm9maWxlSWQiOiIxIiwiVGVuYW50SWQiOiIxIn0.Vphx6HtU-yQAPx7oHWnOq91fLIE4coErE8xYUfdkk84",
                                RefreshToken = "refreshToken"
                            };

            mockCacheHandler.Setup(expression: x => x.Db0.HashGetAsync<TokenData>("USER_1_1",
                                                                                  tokenData.RefreshToken,
                                                                                  CommandFlags.None)).ReturnsAsync(value: null);

            var service = new TokenCacheHelper.TokenManager.TokenManager(logger: mockLoggerHandler.Object,
                                                                         cacheHandler: mockCacheHandler.Object);

            var result = await service.CheckPair(token: tokenData.Token,
                                                 refreshToken: tokenData.RefreshToken);

            Assert.False(condition: result);
        }
    }
}