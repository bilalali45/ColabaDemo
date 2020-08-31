using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Service
{
    public interface IFtpClient
    {
        void Setup(string hostIp, string userName, string password);
        Task DownloadAsync(string remoteFile, string localFile);
        Task UploadAsync(string remoteFile, string localFile);
    }
}
