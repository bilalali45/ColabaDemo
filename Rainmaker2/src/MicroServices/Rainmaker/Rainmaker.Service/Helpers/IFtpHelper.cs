using System.IO;
using System.Threading.Tasks;

namespace Rainmaker.Service.Helpers
{
    public interface IFtpHelper
    {
        Task<Stream> DownloadStream(string remoteFile);
    }
}
