using System.Threading.Tasks;

namespace DocManager.Service
{
    public interface IKeyStoreService
    {
        Task<string> GetFtpKey();
        Task<string> GetFileKey();
    }
}
