using Rainmaker.Service.Helpers;
using RainMaker.Common;
using RainMaker.Common.Extensions;
using RainMaker.Common.FTP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RainMaker.Service.Helpers
{
    public class FtpHelper : IFtpHelper
    {
        private Lazy<string> FtpHost = null;
        private Lazy<string> FtpUser = null;
        private Lazy<string> FtpPass = null;
        private Lazy<FtpClient> Ftp = null;
        private readonly ICommonService commonService;
        public FtpHelper(ICommonService commonService)
        {
            this.commonService = commonService;
            FtpHost = new Lazy<string>(()=>commonService.GetSettingValueByKeyAsync<string>(SystemSettingKeys.FtpHost).Result);
            FtpUser = new Lazy<string>(() => commonService.GetSettingValueByKeyAsync<string>(SystemSettingKeys.FtpUser).Result);
            FtpPass = new Lazy<string>(() => commonService.GetSettingValueByKeyAsync<string>(SystemSettingKeys.FtpPass).Result.Decrypt(Constants.EncryptionKey));
            Ftp = new Lazy<FtpClient>(() => new FtpClient(FtpHost.Value, FtpUser.Value, FtpPass.Value));
        }

        public async Task Download(string remoteFile, string localFile)
        {
            await Ftp.Value.DownloadAsync(remoteFile, localFile);
        }

        public async Task<Stream> DownloadStream(string remoteFile)
        {
            return await Ftp.Value.DownloadStreamAsync(remoteFile);
        }

        public async Task DownloadLoanDoc(string remoteFile, string localFile)
        {
            await Ftp.Value.DownloadLoanDocAsync(remoteFile, localFile);
        }

        public async Task Upload(string remoteFile, string localFile)
        {
            await Ftp.Value.UploadAsync(remoteFile, localFile);
        }
        public async Task Upload(string localFile, string remoteDirectory, string fileName)
        {
            var remoteFile = remoteDirectory + "/" + fileName;
            await Ftp.Value.UploadAsync(remoteFile, localFile);
        }

        public async Task<bool> Exists(string remoteFile)
        {
            return await Ftp.Value.ExistsAsync(remoteFile);
        }
        public async Task CreateDirectory(string newDirectory)
        {
            await Ftp.Value.CreateDirectoryAsync(newDirectory);
        }
   
        public async Task UploadString(Stream localFile, string remoteDirectory, string fileName, bool closeStream = true)
        {
            var status = await CheckFtpFolder(remoteDirectory);

            var remoteFile = remoteDirectory + "/" + fileName;

            if (status)
                await Ftp.Value.UploadStringAsync(remoteFile, localFile, closeStream);
            else
            {
                var xDrivePath = await commonService.GetSettingValueByKeyAsync<string>(SystemSettingKeys.XDrivePath);
                await Ftp.Value.UploadToXDriveAsync(remoteFile, localFile, xDrivePath, closeStream);
            }

        }

        public async Task Delete(string remoteFile)
        {
            await Ftp.Value.DeleteAsync(remoteFile);
        }

        public async Task UploadToDirectory(string localFile, string remoteDirectory)
        {
            var remoteFile = remoteDirectory + "/" + Path.GetFileName(localFile);
            await Ftp.Value.UploadAsync(remoteFile, localFile);
        }

        public async Task<EmailFtpPath> UploadEmail(string body, string fileKey, string emailSavePath, FileProperties[] attachmentPaths)
        {
            var hasFtpConnection = await CheckFtpFolder(emailSavePath); //main folder will create there

            if (!hasFtpConnection)
            {
                var xDrivePath = await commonService.GetSettingValueByKeyAsync<string>(SystemSettingKeys.XDrivePath);
                CreateXDriveSubFolder(xDrivePath, emailSavePath + "//" + fileKey);
                return await EmailFileUploader.UploadEmailOnXDriveAsync(body, fileKey, FtpHost.Value, FtpUser.Value, FtpPass.Value, emailSavePath, xDrivePath);
            }

            return await EmailFileUploader.UploadEmailAsync(body, fileKey, FtpHost.Value, FtpUser.Value, FtpPass.Value, emailSavePath, attachmentPaths);
        }

        public async Task<string> GetFileText(string filePath)
        {
            return await Ftp.Value.DownloadFileTextAsync(filePath);
        }

        public void CreateXDriveSubFolder(string xDrivePath, string subFolderPath)
        {
            var folderPath = xDrivePath + subFolderPath;

            if (!Directory.Exists(@folderPath))
                Directory.CreateDirectory(@folderPath);
        }

        public async Task<bool> CheckFtpFolder(string remoteDirectory)
        {
            var status = true;

            var ftpServerPath = await commonService.GetSettingValueByKeyAsync<string>(SystemSettingKeys.FtpHost);
            var list = await GetFileList();

            //null list means connection with ftp failed.
            if (list == null)
            {
                //create folder on system define loaction
                var xDrivePath = await commonService.GetSettingValueByKeyAsync<string>(SystemSettingKeys.XDrivePath);
                var folderPath = xDrivePath + remoteDirectory;

                if (!Directory.Exists(@folderPath))
                    Directory.CreateDirectory(@folderPath);

                status = false;
            }
            else if (!list.Contains(remoteDirectory))
            {
                try
                {
                    var ftpRequest =
                        (FtpWebRequest)
                            WebRequest.Create(new Uri(string.Format("{0}{1}", ftpServerPath, remoteDirectory)));

                    ftpRequest.Credentials = new NetworkCredential(FtpUser.Value, FtpPass.Value);

                    ftpRequest.UseBinary = false;
                    ftpRequest.UsePassive = true;
                    ftpRequest.KeepAlive = false;
                    ftpRequest.Proxy = null;

                    ftpRequest.Method = WebRequestMethods.Ftp.MakeDirectory;

                    var ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();

                    ftpResponse.Close();

                }
                catch (WebException)
                {
                    //Web exception ignored
                }
            }

            return status;
        }

        private async Task<IEnumerable<string>> GetFileList()
        {
            var result = new System.Text.StringBuilder();
            FtpWebRequest reqFtp;
            try
            {
                var ftpServerPath = await commonService.GetSettingValueByKeyAsync<string>(SystemSettingKeys.FtpHost);

                reqFtp = (FtpWebRequest)WebRequest.Create(new Uri(ftpServerPath));
                reqFtp.Credentials = new NetworkCredential(FtpUser.Value, FtpPass.Value);
                reqFtp.Method = WebRequestMethods.Ftp.ListDirectory;
                reqFtp.UseBinary = false;
                reqFtp.Proxy = null;
                reqFtp.KeepAlive = false;
                reqFtp.UsePassive = true;

                using (var response = reqFtp.GetResponse())
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        var line = reader.ReadLine();
                        while (line != null)
                        {
                            result.Append(line);
                            result.Append("\n");
                            line = reader.ReadLine();
                        }

                        if (!string.IsNullOrWhiteSpace(result.ToString()))
                            result.Remove(result.ToString().LastIndexOf('\n'), 1);
                        else
                            result.Append("\n");
                    }
                }

                return result.ToString().Split('\n');
            }
            catch (Exception ex)
            {
                Console.WriteLine("FTP ERROR: {0}", ex.Message);
                return null;
            }
        }
    }
}
