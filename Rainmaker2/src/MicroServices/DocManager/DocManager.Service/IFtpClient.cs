using System.IO;
using System.Threading.Tasks;

namespace DocManager.Service
{
    public interface IFtpClient
    {
        void Setup(string hostIp, string userName, string password);
        Task DownloadAsync(string remoteFile, MemoryStream localFileStream);
        Task UploadAsync(string remoteFile, MemoryStream localFileStream);
    }
}
