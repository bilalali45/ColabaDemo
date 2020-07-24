﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DocumentManagement.Model;
using DocumentManagement.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DocumentManagement.API.Controllers
{
    [Authorize(Roles = "MCU")]
    [ApiController]
    [Route(template: "api/DocumentManagement/[controller]")]
    public class DocumentController : Controller
    {
        #region Constructors

        public DocumentController(IDocumentService documentService,
            IFileEncryptionFactory fileEncryptionFactory,
            IFtpClient ftpClient,
            ISettingService settingService,
            IKeyStoreService keyStoreService,
            ILogger<DocumentController> logger,
            IRainmakerService rainmakerService)
        {
            this.documentService = documentService;
            this.fileEncryptionFactory = fileEncryptionFactory;
            this.ftpClient = ftpClient;
            this.settingService = settingService;
            this.keyStoreService = keyStoreService;
            this.logger = logger;
            this.rainmakerService = rainmakerService;
        }

        #endregion

        #region Private Variables

        private readonly IDocumentService documentService;
        private readonly IFileEncryptionFactory fileEncryptionFactory;
        private readonly IFtpClient ftpClient;
        private readonly ISettingService settingService;
        private readonly IKeyStoreService keyStoreService;
        private readonly ILogger<DocumentController> logger;
        private readonly IRainmakerService rainmakerService;

        #endregion

        #region Action Methods

       
        [HttpPost(template: "[action]")]
        public async Task<IActionResult> GetDocumentsByTemplateIds(GetDocumentsByTemplateIds getDocumentsByTemplateIds)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            logger.LogInformation($"GetDocumentsByTemplateIds requested by {userProfileId}");
            var docQuery = await documentService.GetDocumentsByTemplateIds(getDocumentsByTemplateIds.id.ToList(), getDocumentsByTemplateIds.tenantId);
            return Ok(value: docQuery);
        }


        
        [HttpGet(template: "[action]")]
        public async Task<IActionResult> GetFiles([FromQuery] GetFiles getFiles)

        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            logger.LogInformation($"GetFiles requested by {userProfileId}");
            return Ok(value: await documentService.GetFiles(id: getFiles.id,
                                                            requestId: getFiles.requestId,
                                                            docId: getFiles.docId));
        }


         
        [HttpGet(template: "[action]")]
        public async Task<IActionResult> GetActivityLog([FromQuery] GetActivityLog getActivityLog)

        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            logger.LogInformation($"GetActivityLog requested by {userProfileId}");
            return Ok(value: await documentService.GetActivityLog(id: getActivityLog.id,
                                                                  typeId: getActivityLog.typeId,
                                                                  docName: getActivityLog.docName));
        }


         
        [HttpGet(template: "[action]")]
        public async Task<IActionResult> GetEmailLog([FromQuery] GetEmailLog getEmailLog)

        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            logger.LogInformation($"GetEmailLog requested by {userProfileId}");
            return Ok(value: await documentService.GetEmailLog(id: getEmailLog.id));
        }


        
        [HttpPost(template: "[action]")]
        public async Task<IActionResult> McuRename(mcuRenameModel mcuRenameModel)

        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            var setting = await settingService.GetSetting();
            if (mcuRenameModel.newName.Length > setting.maxFileNameSize)
                throw new Exception(message: "File Name size exceeded limit");
            logger.LogInformation($"mcurename requested by {userProfileId}, new name is {mcuRenameModel.newName}");
            var docQuery = await documentService.mcuRename(id: mcuRenameModel.id,
                                                           requestId: mcuRenameModel.requestId,
                                                           docId: mcuRenameModel.docId,
                                                           fileId: mcuRenameModel.fileId,
                                                           newName: mcuRenameModel.newName);
            if (docQuery)
                return Ok();
            return NotFound();
        }


        [HttpPost(template: "[action]")]
        public async Task<IActionResult> AcceptDocument(AcceptDocumentModel acceptDocumentModel)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            string userName = User.FindFirst("FirstName").Value.ToString() + ' ' + User.FindFirst("LastName").Value.ToString();
            logger.LogInformation($"document {acceptDocumentModel.docId} is accepted by {userProfileId}");
            var docQuery = await documentService.AcceptDocument(id: acceptDocumentModel.id,
                                                                requestId: acceptDocumentModel.requestId,
                                                                docId: acceptDocumentModel.docId,
                                                                userName: userName);
            if (docQuery)
                return Ok();
            return NotFound();
        }


        [HttpPost(template: "[action]")]
        public async Task<IActionResult> RejectDocument(RejectDocumentModel rejectDocumentModel)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            string userName = User.FindFirst("FirstName").Value.ToString() + ' ' + User.FindFirst("LastName").Value.ToString();
            logger.LogInformation($"document {rejectDocumentModel.docId} is rejected by {userProfileId}");
            var docQuery = await documentService.RejectDocument(id: rejectDocumentModel.id,
                                                                requestId: rejectDocumentModel.requestId,
                                                                docId: rejectDocumentModel.docId,
                                                                message: rejectDocumentModel.message,
                                                                userId:userProfileId,
                                                                userName: userName);
            if (docQuery)
            {
                await rainmakerService.SendBorrowerEmail(rejectDocumentModel.loanApplicationId, rejectDocumentModel.message, (int)ActivityForType.LoanApplicationDocumentRejectActivity, userProfileId,userName, Request.Headers["Authorization"].Select(x => x.ToString()));
                return Ok();
            }

            return NotFound();
        }


        [HttpGet(template: "[action]")]
        public async Task<IActionResult> View([FromQuery] View view)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            logger.LogInformation($"document {view.docId} is viewed by {userProfileId}");
            var model = new FileViewModel
                        {
                            docId = view.docId,
                            fileId = view.fileId,
                            id = view.id,
                            requestId = view.requestId,
                            tenantId = view.tenantId
            };

            var fileviewdto = await documentService.View(model,
                                                         userProfileId,
                                                         HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString());
            var setting = await settingService.GetSetting();

            ftpClient.Setup(hostIp: setting.ftpServer,
                            userName: setting.ftpUser,
                            password: AESCryptography.Decrypt(text: setting.ftpPassword,
                                                              key: await keyStoreService.GetFtpKey()));
            var filepath = Path.GetTempFileName();
            await ftpClient.DownloadAsync(remoteFile: fileviewdto.serverName,
                                          localFile: filepath);

            return File(fileEncryptionFactory.GetEncryptor(name: fileviewdto.encryptionAlgorithm).DecrypeFile(inputFile: filepath,
                                                                                                              password: await keyStoreService.GetFileKey(),
                                                                                                              originalFileName: fileviewdto.clientName),
                        fileviewdto.contentType,
                        fileviewdto.clientName);
        }

        #endregion
    }
}