using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using RainMaker.Common.Util;

namespace RainMaker.Common.FTP
{
    public class EmailFileUploader
    {
        public static async Task<EmailFtpPath> UploadEmailAsync(string html, string fileUniqueKey, string ftphost, string userName, string pass, string emailDirectoryPath, params FileProperties[] attachmentPaths)
        {
            var client = new FtpClient(ftphost, userName, pass);
            string localfileName = await FileHelpers.CreateTmpFileAsync(html);
            
            var directory = emailDirectoryPath + "/" + fileUniqueKey;
            await client.CreateDirectoryAsync(directory);
            string remoteFileName = directory + "/" + fileUniqueKey + ".html";
            await client.UploadAsync(remoteFileName, localfileName);
            var remoteAttachPaths = new List<FileProperties>();
            if (attachmentPaths != null)
            {
                var attDirectory = directory + "/attachments";
                await client.CreateDirectoryAsync(attDirectory);
                foreach (var path in attachmentPaths)
                {
                  
                    var aFileName = Path.GetFileName(path.FilePath);
                    var aFileExt = Path.GetExtension(path.FilePath);
                    var aSize = FileHelpers.GetFileSize(path.FilePath);
                    var aFileSize = aSize != 0 ? aSize / 1024 : 0;
                    
                    var remoteAttFile = attDirectory + "/" + aFileName;


                    remoteAttachPaths.Add(new FileProperties { DisplayName = path.DisplayName, FilePath = remoteAttFile, Extension = aFileExt, SizeKBs = (int)aFileSize });

                    await client.UploadAsync(remoteAttFile, path.FilePath);
                }
            }
            FileHelpers.DeleteFile(localfileName);


            return new EmailFtpPath { EmailDirectoryPath = directory, EmailFilePath = remoteFileName, AttachmentPaths = remoteAttachPaths };
        }

        public static async Task<EmailFtpPath> UploadEmailOnXDriveAsync(string html, string fileUniqueKey, string ftphost, string userName, string pass, string emailDirectoryPath, string xDrivePath)
        {
            var client = new FtpClient(ftphost, userName, pass);
            var localfileName = await FileHelpers.CreateTmpFileAsync(html);
            var directory = emailDirectoryPath + "/" + fileUniqueKey;
            var remoteFileName = directory + "/" + fileUniqueKey + ".html";
            var localFileStream = new FileStream(localfileName, FileMode.Open);

            await client.UploadToXDriveAsync(remoteFileName, localFileStream, xDrivePath, true);

            FileHelpers.DeleteFile(localfileName);

            return new EmailFtpPath
            {
                EmailDirectoryPath = directory,
                EmailFilePath = remoteFileName,
                AttachmentPaths = new List<FileProperties>()
            };

        }

    }

    public class EmailFtpPath
    {
        public string EmailDirectoryPath { get; set; }
        public string EmailFilePath { get; set; }
        public List<FileProperties> AttachmentPaths { get; set; }
    }
    
    public class FileProperties
    {
        public string DisplayName { get; set; }
        public string FilePath { get; set; }
        public int SizeKBs { get; set; }
        public string Extension { get; set; }
    }
}
