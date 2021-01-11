using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    [Authorize(Roles ="MCU")]
    public class RequestController : Controller
    {
        private readonly IRequestService requestService;
        private readonly IRainmakerService rainmakerService;
        private readonly IConfiguration config;
        private readonly ISettingService settingService;
        private readonly IKeyStoreService keyStoreService;
        private readonly ILogger<RequestController> logger;
        private readonly IFtpClient ftpClient;
        private readonly IFileEncryptionFactory fileEncryptionFactory;
        private readonly IByteProService byteProService;
        private readonly ILosIntegrationService losIntegration;
        public RequestController(IRequestService requestService, IRainmakerService rainmakerService 
            ,IConfiguration config, ISettingService settingService, IKeyStoreService keyStoreService, ILogger<RequestController> logger
            , IFtpClient ftpClient, IFileEncryptionFactory fileEncryptionFactory, IByteProService byteProService, ILosIntegrationService losIntegration)
        {
            this.requestService = requestService;
            this.rainmakerService = rainmakerService;
            this.config = config;
            this.settingService = settingService;
            this.keyStoreService = keyStoreService;
            this.logger = logger;
            this.ftpClient = ftpClient;
            this.fileEncryptionFactory = fileEncryptionFactory;
            this.byteProService = byteProService;
            this.losIntegration = losIntegration;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Save(SaveModel loanApplication)
        {
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            loanApplication.tenantId = tenantId;
            string userName = User.FindFirst("FirstName").Value.ToString() + ' ' + User.FindFirst("LastName").Value.ToString();

            var responseBody = await rainmakerService.PostLoanApplication(loanApplication.loanApplicationId, false, Request.Headers["Authorization"].Select(x => x.ToString()));

            if (!String.IsNullOrEmpty(responseBody))
            {
                User user = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(responseBody);
                loanApplication.userId = user.userId;
                loanApplication.userName = user.userName;
                loanApplication.request.userId = userProfileId;
                loanApplication.request.userName = userName;

                await requestService.Save(loanApplication, Request.Headers["Authorization"].Select(x => x.ToString()));

                return Ok();
            }
            else
                return NotFound();
        }

        [HttpPost(template: "[action]")]
        public async Task<IActionResult> Submit([FromForm] string id,
                                                [FromForm] string requestId,
                                                [FromForm] string docId,
                                                List<IFormFile> files)
        {
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());
            string userName = User.FindFirst("FirstName").Value.ToString() + ' ' + User.FindFirst("LastName").Value.ToString();
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            var algo = config[key: "File:Algo"];
            var key = config[key: "File:Key"];
            var setting = await settingService.GetSetting();

            ftpClient.Setup(hostIp: setting.ftpServer,
                            userName: setting.ftpUser,
                            password: AesCryptography.Decrypt(text: setting.ftpPassword,
                                                              key: await keyStoreService.GetFtpKey()));
            foreach (var file in files)
            {
                if (file.Length <= 0)
                    return BadRequest("Something went wrong. Please try again");
                if (!setting.allowedExtensions.Contains(Path.GetExtension(file.FileName.ToLower())))
                    //throw new DocumentManagementException("This file type is not allowed for uploading");
                    return BadRequest("File type is not supported. Allowed types: PDF, JPEG, PNG");
                if (file.Length > setting.maxMcuFileSize)
                    //throw new DocumentManagementException("File size exceeded limit");
                    return BadRequest($"File size must be under {((decimal)setting.maxMcuFileSize) / (1024 * 1024)} mb");
                if (file.FileName.Length > setting.maxFileNameSize)
                    return BadRequest("File Name size exceeded limit");
                //throw new DocumentManagementException("File Name size exceeded limit");
            }
            // save
            List<string> fileId = new List<string>();
            foreach (var formFile in files)
                if (formFile.Length > 0)
                {
                    logger.LogInformation($"DocSync uploading file {formFile.FileName}");
                    var filePath = fileEncryptionFactory.GetEncryptor(name: algo).EncryptFile(inputFile: formFile.OpenReadStream(),
                                                                                              password: await keyStoreService.GetFileKey());
                    logger.LogInformation($"DocSync filePath {filePath}");
                    // upload to ftp
                    await ftpClient.UploadAsync(remoteFile: Path.GetFileName(path: filePath),
                                                localFile: filePath);
                    logger.LogInformation($"DocSync After UploadAsync");
                    // insert into mongo
                    var docQuery = await requestService.Submit(contentType: formFile.ContentType,
                                                            id: id,
                                                            requestId: requestId,
                                                            docId: docId,
                                                            mcuName: formFile.FileName,
                                                            serverName: Path.GetFileName(path: filePath),
                                                            size: (int)formFile.Length,
                                                            encryptionKey: key,
                                                            encryptionAlgorithm: algo,
                                                            tenantId: tenantId,
                                                            userId:userProfileId,
                                                            userName:userName,
                                                             authHeader: Request.Headers["Authorization"].Select(x => x.ToString()));
                    logger.LogInformation($"DocSync After Submit into Mongo");
                    System.IO.File.Delete(path: filePath);
                    logger.LogInformation($"DocSync After Delete");
                    if (String.IsNullOrEmpty(docQuery))
                        //throw new DocumentManagementException("unable to update file in mongo");
                        return BadRequest("unable to upload file");
                    fileId.Add(docQuery);
                    logger.LogInformation($"DocSync docQuery is not empty");
                }
            logger.LogInformation($"DocSync Before Request header Select");
            var auth = Request.Headers["Authorization"].Select(x => x.ToString()).ToList();
            logger.LogInformation($"DocSync After Request header Select");
            string ipAddress = HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            logger.LogInformation($"DocSync ipAddress ={ipAddress}");

#pragma warning disable 4014
            Task.Run(async () =>
#pragma warning restore 4014
            {
                logger.LogInformation($"DocSync Before SendFilesToBytePro ");
                try
                {
                    Tenant tenant = await byteProService.GetTenantSetting(tenantId);
                    logger.LogInformation($"DocSync tenant ={tenant}");
                    if (tenant.syncToBytePro == (int)SyncToBytePro.Auto && tenant.autoSyncToBytePro == (int)AutoSyncToBytePro.OnSubmit)
                    {
                        logger.LogInformation($"DocSync if check = true");
                        foreach (var fileid in fileId)
                        {
                            FileViewModel fileViewModel = new FileViewModel();
                            fileViewModel.id = id;
                            fileViewModel.requestId = requestId;
                            fileViewModel.docId = docId;
                            fileViewModel.fileId = fileid;
                            var files = await requestService.GetFileByDocId(fileViewModel, ipAddress, tenantId);
                            logger.LogInformation(message: $"DocSync fileid {fileid} is getting from submit file");

                            if (files.Count > 0)
                            {

                                logger.LogInformation(message: $"DocSync SendFilesToBytePro service has been started :fileid {fileid} is getting from submit file");
                                await losIntegration.SendFilesToBytePro(files[0].loanApplicationId,
                                                                        id,
                                                                        requestId,
                                                                        docId,
                                                                        fileid,
                                                                        auth);
                                logger.LogInformation(message: $"DocSync SendFilesToBytePro service has been finished :fileid {fileid} is getting from submit file");

                            }
                        }
                    }
                }
                catch
                {
                    // this exception can be ignored
                }
            });
            return Ok();
        }
    }
}
