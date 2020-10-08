using System.Threading.Tasks;

namespace DocumentManagement.Service
{
    public interface IKeyStoreService
    {
        Task<string> GetFtpKey();
        Task<string> GetFileKey();
    }
}
