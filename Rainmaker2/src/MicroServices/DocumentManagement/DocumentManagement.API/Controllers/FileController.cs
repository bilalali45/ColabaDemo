using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DocumentManagement.Model;
using DocumentManagement.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DocumentManagement.API.Controllers
{
    [Authorize(Roles = "Customer")]
    [ApiController]
    [Route(template: "api/DocumentManagement/[controller]")]
    public class FileController : Controller
    {
        #region Constructors

        public FileController(IFileService fileService,
                              IFileEncryptionFactory fileEncryptionFactory,
                              IFtpClient ftpClient,
                              ISettingService settingService,
                              IKeyStoreService keyStoreService,
                              IConfiguration config,
                              ILogger<FileController> logger)
        {
            this.fileService = fileService;
            this.fileEncryptionFactory = fileEncryptionFactory;
            this.ftpClient = ftpClient;
            this.settingService = settingService;
            this.keyStoreService = keyStoreService;
            this.config = config;
            this.logger = logger;
        }

        #endregion

        #region Private Variables

        private readonly IFileService fileService;
        private readonly IFileEncryptionFactory fileEncryptionFactory;
        private readonly IFtpClient ftpClient;
        private readonly ISettingService settingService;
        private readonly IKeyStoreService keyStoreService;
        private readonly IConfiguration config;
        private readonly ILogger<FileController> logger;

        #endregion

        #region Action Methods

        #region Post Actions

        [HttpPost(template: "[action]")]
        public async Task<IActionResult> Submit([FromForm] string id,
                                                [FromForm] string requestId,
                                                [FromForm] string docId,
                                                [FromForm] string order,
                                                [FromForm] int tenantId,
                                                List<IFormFile> files)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            var algo = config[key: "File:Algo"];
            var key = config[key: "File:Key"];
            var setting = await settingService.GetSetting();

            ftpClient.Setup(hostIp: setting.ftpServer,
                            userName: setting.ftpUser,
                            password: AESCryptography.Decrypt(text: setting.ftpPassword,
                                                              key: await keyStoreService.GetFtpKey()));
            foreach (var file in files)
            {
                if (file.Length > setting.maxFileSize)
                    throw new Exception(message: "File size exceeded limit");
                if (file.FileName.Length > setting.maxFileNameSize)
                    throw new Exception(message: "File Name size exceeded limit");
                if (!setting.allowedExtensions.Contains(Path.GetExtension(file.FileName.ToLower())))
                    throw new Exception(message: "This file type is not allowed for uploading");
            }
            // save
            foreach (var formFile in files)
                if (formFile.Length > 0)
                {
                    logger.LogInformation($"uploading file {formFile.FileName}");
                    var filePath = fileEncryptionFactory.GetEncryptor(name: algo).EncryptFile(inputFile: formFile.OpenReadStream(),
                                                                                              password: await keyStoreService.GetFileKey());
                    // upload to ftp
                    await ftpClient.UploadAsync(remoteFile: Path.GetFileName(path: filePath),
                                                localFile: filePath);
                    // insert into mongo
                    var docQuery = await fileService.Submit(contentType: formFile.ContentType,
                                                            id: id,
                                                            requestId: requestId,
                                                            docId: docId,
                                                            clientName: formFile.FileName,
                                                            serverName: Path.GetFileName(path: filePath),
                                                            size: (int)formFile.Length,
                                                            encryptionKey: key,
                                                            encryptionAlgorithm: algo,
                                                            tenantId: tenantId,
                                                            userProfileId: userProfileId);
                    System.IO.File.Delete(path: filePath);
                }

            // set order
            var model = new FileOrderModel
            {
                id = id,
                docId = docId,
                requestId = requestId,
                files = JsonConvert.DeserializeObject<List<FileNameModel>>(value: order),
                tenantId = tenantId
            };
            await fileService.Order(model: model,
                                    userProfileId: userProfileId);
            return Ok();
        }

        #endregion

        #region Put Actions

        [HttpPut(template: "[action]")]
        public async Task<IActionResult> Done(DoneModel model)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            logger.LogInformation($"Sending for mcu review {model.docId}");
            var docQuery = await fileService.Done(model: model,
                                                  userProfileId: userProfileId);
            if (docQuery)
                return Ok();
            return NotFound();
        }


        [HttpPut(template: "[action]")]
        public async Task<IActionResult> Rename(FileRenameModel model)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            var setting = await settingService.GetSetting();
            if (model.fileName.Length > setting.maxFileNameSize)
                throw new Exception(message: "File Name size exceeded limit");
            var docQuery = await fileService.Rename(model: model,
                                                    userProfileId: userProfileId);
            if (docQuery)
                return Ok();
            return NotFound();
        }


        [HttpPut(template: "[action]")]
        public async Task<IActionResult> Order(FileOrderModel model)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            await fileService.Order(model: model,
                                    userProfileId: userProfileId);
            return Ok();
        }

        #endregion

        #region Get Actions

        [HttpGet(template: "[action]")]
        public async Task<IActionResult> View([FromQuery] FileViewModel moView)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            var model = new FileViewModel
            {
                docId = moView.docId,
                fileId = moView.fileId,
                id = moView.id,
                requestId = moView.requestId,
                tenantId = moView.tenantId
            };
            logger.LogInformation($"document { moView.docId} is viewed by {userProfileId}");
            var fileviewdto = await fileService.View(model: model,
                                                     userProfileId: userProfileId,
                                                     ipAddress: HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString());
            var setting = await settingService.GetSetting();

            ftpClient.Setup(hostIp: setting.ftpServer,
                            userName: setting.ftpUser,
                            password: AESCryptography.Decrypt(text: setting.ftpPassword,
                                                              key: await keyStoreService.GetFtpKey()));
            var filepath = Path.GetTempFileName();
            await ftpClient.DownloadAsync(remoteFile: fileviewdto.serverName,
                                          localFile: filepath);

            return File(fileStream: fileEncryptionFactory.GetEncryptor(name: fileviewdto.encryptionAlgorithm).DecrypeFile(inputFile: filepath,
                                                                                                                          password: await keyStoreService.GetFileKey(),
                                                                                                                          originalFileName: fileviewdto.clientName),
                        contentType: fileviewdto.contentType,
                        fileDownloadName: fileviewdto.clientName);
        }

        #endregion

        #endregion
    }
}