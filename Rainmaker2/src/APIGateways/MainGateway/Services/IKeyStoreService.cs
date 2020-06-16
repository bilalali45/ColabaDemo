using System.Threading.Tasks;

namespace MainGateway.Services
{
    public interface IKeyStoreService
    {
        Task<string> GetJwtSecurityKeyAsync();
        string GetJwtSecurityKey();
    }
}