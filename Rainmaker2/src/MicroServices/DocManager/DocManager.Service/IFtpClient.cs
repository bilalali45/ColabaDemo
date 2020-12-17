﻿using System.Threading.Tasks;

namespace DocManager.Service
{
    public interface IFtpClient
    {
        void Setup(string hostIp, string userName, string password);
        Task DownloadAsync(string remoteFile, string localFile);
        Task UploadAsync(string remoteFile, string localFile);
        Task DeleteAsync(string deleteFile);
    }
}
