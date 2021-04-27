using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using StackExchange.Redis.Extensions.Core.Abstractions;
using TokenCacheHelper.Models;

namespace TokenCacheHelper.TokenManager
{
    public class TokenManager : ITokenManager
    {
        private readonly IRedisCacheClient _cacheHandler;
        private readonly ILogger<TokenManager> _logger;


        public TokenManager(ILogger<TokenManager> logger,
                            IRedisCacheClient cacheHandler)
        {
            _logger = logger;
            _cacheHandler = cacheHandler;
        }


        public async Task<bool> AddAuthTokenToWhiteListAsync(TokenData tokenData)
        {
            if (tokenData == null)
            {
                _logger.LogWarning(message: "Cannot add null token to white list");
                return false;
            }

            var redisKey = GetCacheKeyFromAuthToken(token: tokenData.Token);

            await _cacheHandler.Db0.HashSetAsync(hashKey: redisKey,
                                                 values: new Dictionary<string, TokenData>
                                                         {
                                                             {tokenData.RefreshToken, tokenData}
                                                         }
                                                );

            await _cacheHandler.Db0.UpdateExpiryAsync(key: redisKey,
                                                      expiresAt: tokenData.ValidTo);

            return true;
        }


        public async Task<bool> AddRefreshTokenTokenAsync(TokenData tokenData)
        {
            if (tokenData == null)
            {
                _logger.LogWarning(message: "Cannot add null token to Refresh token list");
                return false;
            }

            await _cacheHandler.Db1.AddAsync(key: tokenData.RefreshToken,
                                             value: tokenData);

            await _cacheHandler.Db1.UpdateExpiryAsync(key: tokenData.RefreshToken,
                                                      expiresAt: tokenData.RefreshTokenValidTo);

            return true;
        }


        public async Task<bool> CheckPair(string token,
                                          string refreshToken)
        {
            var redisKey = GetCacheKeyFromAuthToken(token: token);
            var userToken = await _cacheHandler.Db0.HashGetAsync<TokenData>(hashKey: redisKey,
                                                                            key: refreshToken);
            return userToken != null;
        }


        public async Task CleanUpAuthTokenWhiteListAsync(TokenData tokenData)
        {
            var redisKey = GetCacheKeyFromAuthToken(token: tokenData.Token);
            var tokens = await _cacheHandler.Db0.HashGetAllAsync<TokenData>(hashKey: redisKey);
            var keysToBeRemoved = tokens.Values.Where(data => data.ValidTo < DateTime.UtcNow).Select(data=>data.RefreshToken);

            await _cacheHandler.Db0.HashDeleteAsync(redisKey,
                                                    keysToBeRemoved);
        }


        public async Task<TokenData> FindUserTokenAsync(string token)
        {
            var redisKey = GetCacheKeyFromAuthToken(token: token);
           
            var userTokens = await _cacheHandler.Db0.HashGetAllAsync<TokenData>(hashKey: redisKey);
            KeyValuePair<string, TokenData> tokenFound;
            if (userTokens != null)
                tokenFound = userTokens
                    .FirstOrDefault(predicate: t => t.Value.Token == token);

            return tokenFound.Value;
        }


        public async Task<bool> RemoveUserTokenAsync(string token)
        {
            var redisKey = GetCacheKeyFromAuthToken(token: token);
            
            var userTokens = await _cacheHandler.Db0.HashGetAllAsync<TokenData>(hashKey: redisKey);

            var tokenFound = userTokens?.FirstOrDefault(predicate: t => t.Value.Token == token).Value;

            if (tokenFound == null) return false;

            var tokenRemoved = await _cacheHandler.Db0.HashDeleteAsync(hashKey: redisKey,
                                                                       key: tokenFound.RefreshToken);

            var refreshTokenRemoved = await _cacheHandler.Db1.RemoveAsync(key: tokenFound.RefreshToken);

            return tokenRemoved && refreshTokenRemoved;
        }


        public async Task<bool> HasExpiredAsync(string token)
        {
            var hasExpired = false;
            var tokenFound = await FindUserTokenAsync(token: token);
            if (tokenFound != null)
                // Make sure token has not expired.
                hasExpired = DateTime.Now > tokenFound.ValidTo;
            return hasExpired;
        }


        public async Task<bool> RevokeToken(string token)
        {
            return await RemoveUserTokenAsync(token: token);
        }


        public async Task<bool> RevokeAllToken(string token)
        {
            var cacheKey = GetCacheKeyFromAuthToken(token: token);

            var tokens = await _cacheHandler.Db0.HashGetAllAsync<TokenData>(cacheKey);

            var refreshTokensToBeRemoved = tokens.Select(pair => pair.Key).ToList();

            await _cacheHandler.Db1.RemoveAllAsync(refreshTokensToBeRemoved);

            return await _cacheHandler.Db0.RemoveAsync(key: cacheKey);
        }


        public async Task<TokenData> GetForSignOn(string refreshToken)
        {
            return await _cacheHandler.Db1.GetAsync<TokenData>(key: refreshToken);
        }


        private string GetCacheKeyFromAuthToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token: token);

            var profileId = jwtToken.Claims.FirstOrDefault(predicate: claim => claim.Type == "UserProfileId");
            var tenantId = jwtToken.Claims.FirstOrDefault(predicate: claim => claim.Type == "TenantId");

            var redisKey = $"USER_{profileId?.Value}_{tenantId?.Value}";
            return redisKey;
        }
    }
}