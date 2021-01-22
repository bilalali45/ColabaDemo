using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DocManager.Model;
using DocManager.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DocManager.API.Controllers
{
    [Route("api/DocManager/[controller]")]
    [ApiController]
    [Authorize(Roles = "MCU")]
    public class ThumbnailController : Controller
    {
        private readonly IThumbnailService thumbnailService;
        private readonly IConfiguration config;
        private readonly ISettingService settingService;
        private readonly IFtpClient ftpClient;
        private readonly IKeyStoreService keyStoreService;
        private readonly ILogger<ThumbnailController> logger;
        private readonly IFileEncryptionFactory fileEncryptionFactory;
        public ThumbnailController(IThumbnailService thumbnailService, IConfiguration config, ISettingService settingService,
            IFtpClient ftpClient, IKeyStoreService keyStoreService, ILogger<ThumbnailController> logger, IFileEncryptionFactory fileEncryptionFactory)
        {
            this.thumbnailService = thumbnailService;
            this.config = config;
            this.settingService = settingService;
            this.ftpClient = ftpClient;
            this.keyStoreService = keyStoreService;
            this.logger = logger;
            this.fileEncryptionFactory = fileEncryptionFactory;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SaveWorkbenchDocument([FromForm] string id,
                                                [FromForm] string fileId,
                                                IFormFile file)
        {
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());
            string userName = User.FindFirst("FirstName").Value.ToString() + ' ' + User.FindFirst("LastName").Value.ToString();
            var algo = config[key: "File:Algo"];
            var key = config[key: "File:Key"];
            var setting = await settingService.GetSetting();

            ftpClient.Setup(hostIp: setting.ftpServer,
                            userName: setting.ftpUser,
                            password: AesCryptography.Decrypt(text: setting.ftpPassword,
                                                                key: await keyStoreService.GetFtpKey()));

            if (!setting.allowedExtensions.Contains(Path.GetExtension(file.FileName.ToLower())))
                return BadRequest("File type is not supported. Allowed types: PDF, JPEG, PNG");
            if (file.FileName.Length > setting.maxFileNameSize)
                return BadRequest("File Name size exceeded limit");
            if (file.Length <= 0)
                return BadRequest("Something went wrong. Please try again");

            if (file.Length > 0)
            {
                var filePath = fileEncryptionFactory.GetEncryptor(name: algo).EncryptFile(inputFile: file.OpenReadStream(),
                                                                                            password: await keyStoreService.GetFileKey());
                // upload to ftp
                await ftpClient.UploadAsync(remoteFile: Path.GetFileName(path: filePath),
                                            localFile: filePath);
                System.IO.File.Delete(path: filePath);

                // update file
                var docQuery = await thumbnailService.SaveWorkbenchDocument(id, fileId, tenantId, Path.GetFileName(path: filePath), file.FileName, (int)file.Length, file.ContentType,
                            userProfileId, userName, algo, key);
                return Ok(docQuery.fileId);
            }
            return BadRequest();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SaveTrashDocument([FromForm] string id,
                                               [FromForm] string fileId,
                                               IFormFile file)
        {
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());
            string userName = User.FindFirst("FirstName").Value.ToString() + ' ' + User.FindFirst("LastName").Value.ToString();
            var algo = config[key: "File:Algo"];
            var key = config[key: "File:Key"];
            var setting = await settingService.GetSetting();

            ftpClient.Setup(hostIp: setting.ftpServer,
                            userName: setting.ftpUser,
                            password: AesCryptography.Decrypt(text: setting.ftpPassword,
                                                                key: await keyStoreService.GetFtpKey()));

            if (!setting.allowedExtensions.Contains(Path.GetExtension(file.FileName.ToLower())))
                return BadRequest("File type is not supported. Allowed types: PDF, JPEG, PNG");
            if (file.FileName.Length > setting.maxFileNameSize)
                return BadRequest("File Name size exceeded limit");
            if (file.Length <= 0)
                return BadRequest("Something went wrong. Please try again");

            if (file.Length > 0)
            {
                var filePath = fileEncryptionFactory.GetEncryptor(name: algo).EncryptFile(inputFile: file.OpenReadStream(),
                                                                                            password: await keyStoreService.GetFileKey());
                // upload to ftp
                await ftpClient.UploadAsync(remoteFile: Path.GetFileName(path: filePath),
                                            localFile: filePath);
                System.IO.File.Delete(path: filePath);

                // update file
                var docQuery = await thumbnailService.SaveTrashDocument(id, fileId, tenantId, Path.GetFileName(path: filePath), file.FileName, (int)file.Length, file.ContentType,
                            userProfileId, userName, algo, key);
                return Ok(docQuery.fileId);
            }
            return BadRequest();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SaveCategoryDocument([FromForm] string id, [FromForm] string requestId, [FromForm] string docId,
                                               [FromForm] string fileId,
                                               IFormFile file)
        {
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());
            string userName = User.FindFirst("FirstName").Value.ToString() + ' ' + User.FindFirst("LastName").Value.ToString();
            var algo = config[key: "File:Algo"];
            var key = config[key: "File:Key"];
            var setting = await settingService.GetSetting();

            ftpClient.Setup(hostIp: setting.ftpServer,
                            userName: setting.ftpUser,
                            password: AesCryptography.Decrypt(text: setting.ftpPassword,
                                                                key: await keyStoreService.GetFtpKey()));

            if (!setting.allowedExtensions.Contains(Path.GetExtension(file.FileName.ToLower())))
                return BadRequest("File type is not supported. Allowed types: PDF, JPEG, PNG");
            if (file.FileName.Length > setting.maxFileNameSize)
                return BadRequest("File Name size exceeded limit");
            if (file.Length <= 0)
                return BadRequest("Something went wrong. Please try again");

            if (file.Length > 0)
            {
                var filePath = fileEncryptionFactory.GetEncryptor(name: algo).EncryptFile(inputFile: file.OpenReadStream(),
                                                                                            password: await keyStoreService.GetFileKey());
                // upload to ftp
                await ftpClient.UploadAsync(remoteFile: Path.GetFileName(path: filePath),
                                            localFile: filePath);
                System.IO.File.Delete(path: filePath);

                // update file
                var docQuery = await thumbnailService.SaveCategoryDocument(id, requestId, docId, fileId, tenantId, Path.GetFileName(path: filePath), file.FileName, (int)file.Length, file.ContentType,
                            userProfileId, userName, algo, key);

                return Ok(docQuery.fileId);
            }
            return BadRequest();
        }
    }
}
