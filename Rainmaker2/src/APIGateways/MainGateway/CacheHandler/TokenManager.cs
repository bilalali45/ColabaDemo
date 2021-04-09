using MainGateway.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace MainGateway.CacheHandler
{
    public interface ITokenManager
    {
        void AddAuthTokenToWhiteListAsync(TokenData tokenData);
        Task<TokenData> FindUserTokenAsync(string token);
        Task<bool> HasExpiredAsync(string token);
        Task<bool> RemoveUserTokenAsync(string token);
        Task<bool> RevokeToken(string token);
        Task<bool> RevokeAllToken(string token);
    }
    public class TokenManager : ITokenManager
    {
        private readonly ILogger<TokenManager> _logger;
        private readonly ICacheHandler _cacheHandler;


        public TokenManager(ILogger<TokenManager> logger, ICacheHandler cacheHandler)
        {
            this._logger = logger;
            this._cacheHandler = cacheHandler;
        }

        public async void AddAuthTokenToWhiteListAsync(TokenData tokenData)
        {
            if (tokenData == null)
            {
                this._logger.LogWarning("Cannot add null token to white list");
            }
            else
            {
                var redisKey = this.GetCacheKeyFromAuthToken(tokenData.Token);
                var userExistingTokens = await this._cacheHandler.GetCacheItemAsync<List<TokenData>>(redisKey);
                if (userExistingTokens == null)
                {
                    this._cacheHandler.SetCacheItemAsync<List<TokenData>>(redisKey,
                                                            new List<TokenData>()
                                                            {
                                                                tokenData
                                                            });
                }
                else
                {
                    userExistingTokens.Add(tokenData);
                    this._cacheHandler.SetCacheItemAsync<List<TokenData>>(redisKey, userExistingTokens);
                }
            }
        }

        public async Task<TokenData> FindUserTokenAsync(string token)
        {
            TokenData tokenFound = null;
            var redisKey = this.GetCacheKeyFromAuthToken(token);
            var userTokens = await this._cacheHandler.GetCacheItemAsync<List<TokenData>>(redisKey);
            if (userTokens != null)
            {
                tokenFound = userTokens
                    .FirstOrDefault(t => t.Token == token);
            }
            return tokenFound;
        }

        public async Task<bool> RemoveUserTokenAsync(string token)
        {
            TokenData tokenFound = null;
            bool isSuccess = false;
            var redisKey = this.GetCacheKeyFromAuthToken(token);
            var userTokens = await this._cacheHandler.GetCacheItemAsync<List<TokenData>>(redisKey);
            if (userTokens != null)
            {
                tokenFound = userTokens
                    .FirstOrDefault(t => t.Token == token);
                if (tokenFound != null)
                {
                    // Make sure token has not expired.
                    if ((DateTime.Now > tokenFound.ValidTo))
                    {
                        userTokens.Remove(tokenFound);
                        this._cacheHandler.SetCacheItemAsync<List<TokenData>>(redisKey, userTokens);
                        tokenFound = null;
                        isSuccess = false;
                    }
                }
            }

            return isSuccess;
        }

        public async Task<bool> HasExpiredAsync(string token)
        {
            bool hasExpired = true;
            var tokenFound = await this.FindUserTokenAsync(token);
            if (tokenFound != null)
            {
                // Make sure token has not expired.
                hasExpired = DateTime.Now > tokenFound.ValidTo;
            }
            return hasExpired;
        }


        public async Task<bool> RevokeToken(string token)
        {
            return await RemoveUserTokenAsync(token);
        }


        public async Task<bool> RevokeAllToken(string token)
        {
            bool isSuccess = true;
            var cacheKey = this.GetCacheKeyFromAuthToken(token);
            isSuccess = await this._cacheHandler.RemoveCacheItemAsync(cacheKey);
            return isSuccess;
        }


        private string GetCacheKeyFromAuthToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            var profileId = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "UserProfileId");
            var tenantId = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "TenantId");

            var redisKey = $"USER_{profileId?.Value}_{tenantId?.Value}";
            return redisKey;
        }
    }
}
