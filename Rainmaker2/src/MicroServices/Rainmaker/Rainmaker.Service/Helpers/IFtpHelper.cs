using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Rainmaker.Service.Helpers
{
    public interface IFtpHelper
    {
        Task<Stream> DownloadStream(string remoteFile);
    }
}
