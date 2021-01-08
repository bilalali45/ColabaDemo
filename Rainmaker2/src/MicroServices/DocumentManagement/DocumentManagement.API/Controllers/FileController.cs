using DocumentManagement.Entity;
using DocumentManagement.Model;
using DocumentManagement.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
                              ILogger<FileController> logger, ILosIntegrationService losIntegration,
                              INotificationService notificationService,
                              IRainmakerService rainmakerService,
                              IByteProService byteProService,
                              IRequestService requestService)
        {
            this.fileService = fileService;
            this.fileEncryptionFactory = fileEncryptionFactory;
            this.ftpClient = ftpClient;
            this.settingService = settingService;
            this.keyStoreService = keyStoreService;
            this.config = config;
            this.logger = logger;
            this.losIntegration = losIntegration;
            this.notificationService = notificationService;
            this.rainmakerService = rainmakerService;
            this.byteProService = byteProService;
            this.requestService = requestService;
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
        private readonly ILosIntegrationService losIntegration;
        private readonly INotificationService notificationService;
        private readonly IRainmakerService rainmakerService;
        private readonly IByteProService byteProService;
        private readonly IRequestService requestService;
        #endregion

        #region Action Methods

        #region Post Actions

        [HttpPost(template: "[action]")]
        public async Task<IActionResult> Submit([Required(ErrorMessage = ValidationMessages.ValidationFailed)]
                                                [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = ValidationMessages.ValidationFailed)]
                                                [FromForm] string id,
                                                [Required(ErrorMessage = ValidationMessages.ValidationFailed)]
                                                [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = ValidationMessages.ValidationFailed)]
                                                [FromForm] string requestId,
                                                [Required(ErrorMessage = ValidationMessages.ValidationFailed)]
                                                [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = ValidationMessages.ValidationFailed)]
                                                [FromForm] string docId,
                                                [Required(ErrorMessage = ValidationMessages.ValidationFailed)]
                                                [FromForm] string order,
                                                List<IFormFile> files)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
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
                if (file.Length<=0)
                    return BadRequest("Something went wrong. Please try again");
                if (!setting.allowedExtensions.Contains(Path.GetExtension(file.FileName.ToLower())))
                    //throw new DocumentManagementException("This file type is not allowed for uploading");
                    return BadRequest("File type is not supported. Allowed types: PDF, JPEG, PNG");
                if (file.Length > setting.maxFileSize)
                    //throw new DocumentManagementException("File size exceeded limit");
                    return BadRequest($"File size must be under {((decimal)setting.maxFileSize)/(1024*1024)} mb");
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
                                                            userProfileId: userProfileId,
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
            logger.LogInformation($"DocSync ipAddress ={ipAddress}" );

#pragma warning disable 4014
            Task.Run(async () =>
#pragma warning restore 4014
                     {
                         logger.LogInformation($"DocSync Task.Run Start ");
                         try
                         {
                             int loanApplicationId = await rainmakerService.GetLoanApplicationId(id);
                             await notificationService.DocumentsSubmitted(loanApplicationId, auth);
                         }
                         catch
                         {
                             // this exception can be ignored
                         }
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
                                     var loanApplicationId = await rainmakerService.GetLoanApplicationId(id);
                                     logger.LogInformation(message: $"DocSync fileid {fileid} is getting from submit file");

                                    logger.LogInformation(message: $"DocSync SendFilesToBytePro service has been started :fileid {fileid} is getting from submit file");
                                    await losIntegration.SendFilesToBytePro(loanApplicationId,
                                                                            id,
                                                                            requestId,
                                                                            docId,
                                                                            fileid,
                                                                            auth);
                                    logger.LogInformation(message: $"DocSync SendFilesToBytePro service has been finished :fileid {fileid} is getting from submit file");

                                 }
                             }
                         }
                         catch
                         {
                             // this exception can be ignored
                         }




                     });
            // set order
            var model = new FileOrderModel
            {
                id = id,
                docId = docId,
                requestId = requestId,
                files = JsonConvert.DeserializeObject<List<FileNameModel>>(value: order)
            };
            await fileService.Order(model: model,
                                    userProfileId: userProfileId, tenantId);


            return Ok();
        }

        [HttpPost(template: "[action]")]
        public async Task<IActionResult> SubmitByBorrower(
                                                [Required(ErrorMessage = ValidationMessages.ValidationFailed)]
                                                [FromForm] int loanApplicationId,
                                                [Required(ErrorMessage = ValidationMessages.ValidationFailed)]
                                                [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = ValidationMessages.ValidationFailed)]
                                                [FromForm] string docId,
                                                [Required(ErrorMessage = ValidationMessages.ValidationFailed)]
                                                [FromForm] string displayName,
                                                [Required(ErrorMessage = ValidationMessages.ValidationFailed)]
                                                [FromForm] string order,
                                                List<IFormFile> files)
        {
            //Create borrower request 
            var responseBody = await rainmakerService.PostLoanApplication(loanApplicationId: loanApplicationId, isDraft: false, authHeader: Request.Headers["Authorization"].Select(x => x.ToString()));

            if (!String.IsNullOrEmpty(responseBody))
            {
                var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
                var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
                string userName = User.FindFirst("FirstName").Value.ToString() + ' ' + User.FindFirst("LastName").Value.ToString();
                var algo = config[key: "File:Algo"];
                var key = config[key: "File:Key"];
                var setting = await settingService.GetSetting();

                RequestResponseModel requestResponseModel = new RequestResponseModel();

                Model.LoanApplication loanApplication = new Model.LoanApplication();

                User user = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(responseBody);

                loanApplication.userId = user.userId;
                loanApplication.userName = user.userName;
                loanApplication.loanApplicationId = loanApplicationId;
                loanApplication.tenantId = tenantId;

                Model.Request request = new Model.Request();
                loanApplication.requests = new List<Model.Request>() { };
                request.userId = userProfileId;
                request.userName = userName;
                request.documents = new List<Model.RequestDocument>() { };

                Model.RequestDocument requestDocument = new Model.RequestDocument();
                requestDocument.typeId = docId;
                requestDocument.displayName = displayName;

                request.documents.Add(requestDocument);
                loanApplication.requests.Add(request);

                requestResponseModel = await requestService.GetDocumentRequest(loanApplicationId:loanApplicationId,tenantId: tenantId, docId:docId);
                if(String.IsNullOrEmpty(requestResponseModel.requestId))
                    requestResponseModel = await requestService.SaveByBorrower(loanApplication: loanApplication, isDraft: false, isFromBorrower: true, authHeader: Request.Headers["Authorization"].Select(x => x.ToString()));

                ftpClient.Setup(hostIp: setting.ftpServer,
                                userName: setting.ftpUser,
                                password: AesCryptography.Decrypt(text: setting.ftpPassword,
                                                                  key: await keyStoreService.GetFtpKey()));
                foreach (var file in files)
                {
                    if (!setting.allowedExtensions.Contains(Path.GetExtension(file.FileName.ToLower())))
                        //throw new DocumentManagementException("This file type is not allowed for uploading");
                        return BadRequest("File type is not supported. Allowed types: PDF, JPEG, PNG");
                    if (file.Length > setting.maxFileSize)
                        //throw new DocumentManagementException("File size exceeded limit");
                        return BadRequest($"File size must be under {((decimal)setting.maxFileSize) / (1024 * 1024)} mb");
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
                        var docQuery = await fileService.SubmitByBorrower(contentType: formFile.ContentType,
                                                                id: requestResponseModel.id,
                                                                requestId: requestResponseModel.requestId,
                                                                docId: requestResponseModel.docId,
                                                                clientName: formFile.FileName,
                                                                serverName: Path.GetFileName(path: filePath),
                                                                size: (int)formFile.Length,
                                                                encryptionKey: key,
                                                                encryptionAlgorithm: algo,
                                                                tenantId: tenantId,
                                                                userProfileId: userProfileId,
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
                    logger.LogInformation($"DocSync Task.Run Start ");
                    try
                    {
                        int loanApplicationId = await rainmakerService.GetLoanApplicationId(requestResponseModel.id);
                        await notificationService.DocumentsSubmitted(loanApplicationId, auth);
                    }
                    catch
                    {
                    // this exception can be ignored
                }
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
                                var loanApplicationId = await rainmakerService.GetLoanApplicationId(requestResponseModel.id);
                                logger.LogInformation(message: $"DocSync fileid {fileid} is getting from submit file");

                                logger.LogInformation(message: $"DocSync SendFilesToBytePro service has been started :fileid {fileid} is getting from submit file");
                                await losIntegration.SendFilesToBytePro(loanApplicationId,
                                                                        requestResponseModel.id,
                                                                        requestResponseModel.requestId,
                                                                        docId,
                                                                        fileid,
                                                                        auth);
                                logger.LogInformation(message: $"DocSync SendFilesToBytePro service has been finished :fileid {fileid} is getting from submit file");

                            }
                        }
                    }
                    catch
                    {
                    // this exception can be ignored
                }




                });
                // set order
                var model = new FileOrderModel
                {
                    id = requestResponseModel.id,
                    docId = docId,
                    requestId = requestResponseModel.requestId,
                    files = JsonConvert.DeserializeObject<List<FileNameModel>>(value: order)
                };
                await fileService.Order(model: model,
                                        userProfileId: userProfileId, tenantId);
                return Ok(new FileViewModel { docId = requestResponseModel.docId, id = requestResponseModel.id, requestId = requestResponseModel.requestId, fileId = fileId.FirstOrDefault() }) ;
            }
            return NotFound();
        }
        #endregion

        #region Put Actions

        [HttpPut(template: "[action]")]
        public async Task<IActionResult> Done(DoneModel model)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            logger.LogInformation($"Sending for mcu review {model.docId}");
            var docQuery = await fileService.Done(model: model,
                                                  userProfileId: userProfileId,
                                                  tenantId: tenantId,
                                                  authHeader: Request.Headers["Authorization"].Select(x => x.ToString()));
            if (docQuery)
            {
                var auth = Request.Headers["Authorization"].Select(x => x.ToString()).ToList();
#pragma warning disable 4014
                Task.Run(async () =>
#pragma warning restore 4014
                {
                    Tenant tenant = await byteProService.GetTenantSetting(tenantId);
                    if (tenant.syncToBytePro == (int) SyncToBytePro.Auto &&
                        tenant.autoSyncToBytePro == (int) AutoSyncToBytePro.OnDone)
                    {
                        await byteProService.UploadFiles(model.id, model.requestId, model.docId, auth);
                    }
                });
                return Ok();
            }

            return NotFound();
        }


        [HttpPut(template: "[action]")]
        public async Task<IActionResult> Rename(FileRenameModel model)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            var setting = await settingService.GetSetting();
            if (model.fileName.Length > setting.maxFileNameSize)
                //throw new DocumentManagementException("File Name size exceeded limit");
                return BadRequest("File Name size exceeded limit");
            var docQuery = await fileService.Rename(model: model,
                                                    userProfileId: userProfileId, tenantId);
            if (docQuery)
                return Ok();
            return NotFound();
        }


        [HttpPut(template: "[action]")]
        public async Task<IActionResult> Order(FileOrderModel model)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            await fileService.Order(model: model,
                                    userProfileId: userProfileId, tenantId);
            return Ok();
        }

        #endregion

        #region Get Actions

        [HttpGet(template: "[action]")]
        public async Task<IActionResult> View([FromQuery] FileViewModel moView)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            var model = new FileViewModel
            {
                docId = moView.docId,
                fileId = moView.fileId,
                id = moView.id,
                requestId = moView.requestId
            };
            logger.LogInformation($"document { moView.docId} is viewed by {userProfileId}");
            var fileviewdto = await fileService.View(model: model,
                                                     userProfileId: userProfileId,
                                                     ipAddress: HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString(), tenantId);
            var setting = await settingService.GetSetting();

            ftpClient.Setup(hostIp: setting.ftpServer,
                            userName: setting.ftpUser,
                            password: AesCryptography.Decrypt(text: setting.ftpPassword,
                                                              key: await keyStoreService.GetFtpKey()));
            var filepath = Path.GetTempFileName();
            await ftpClient.DownloadAsync(remoteFile: fileviewdto.serverName,
                                          localFile: filepath);
            Stream ms = fileEncryptionFactory.GetEncryptor(name: fileviewdto.encryptionAlgorithm).DecrypeFile(inputFile: filepath,
                                                                                                                          password: await keyStoreService.GetFileKey(),
                                                                                                                          originalFileName: fileviewdto.clientName);
            System.IO.File.Delete(filepath);
            return File(ms,
                        contentType: fileviewdto.contentType,
                        fileDownloadName: fileviewdto.clientName);
        }

        #endregion

        #endregion
    }
}