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

        public async Task<Stream> DownloadStream(string remoteFile)
        {
            return await Ftp.Value.DownloadStreamAsync(remoteFile);
        }
    }
}
