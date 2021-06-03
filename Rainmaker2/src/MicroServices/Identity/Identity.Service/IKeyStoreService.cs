using System.Threading.Tasks;

namespace Identity.Service
{
    public interface IKeyStoreService
    {
        Task<string> GetJwtSecurityKeyAsync();

        Task<string> GetTwilioAccountSidAsync();

        Task<string> GetTwilioAuthTokenAsync();
    }
}