using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TokenCacheHelper.Models;

namespace TokenCacheHelper.TokenManager
{
    public interface ITokenManager
    {
        Task<bool> AddAuthTokenToWhiteListAsync(TokenData tokenData);
        Task<TokenData> FindUserTokenAsync(string token);
        Task<bool> HasExpiredAsync(string token);
        Task<bool> RemoveUserTokenAsync(string token);
        Task<bool> RevokeToken(string token);
        Task<bool> RevokeAllToken(string token);
        Task<TokenData> GetForSignOn(string refreshToken);
        Task<bool> AddRefreshTokenTokenAsync(TokenData tokenData);
        Task<bool> CheckPair(string token,
                             string refreshToken);


        Task CleanUpAuthTokenWhiteListAsync(TokenData tokenData);
    }
}
