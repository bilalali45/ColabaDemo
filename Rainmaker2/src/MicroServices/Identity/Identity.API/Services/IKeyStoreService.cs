using System.Threading.Tasks;

namespace Identity.Services
{
    public interface IKeyStoreService
    {
        Task<string> GetJwtSecurityKey();
    }
}