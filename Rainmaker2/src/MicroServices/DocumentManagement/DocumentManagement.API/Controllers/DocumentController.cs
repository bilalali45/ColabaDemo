using DocumentManagement.Entity;
using DocumentManagement.Model;
using DocumentManagement.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
            IByteProService byteProService,
            IRainmakerService rainmakerService)
        {
            this.documentService = documentService;
            this.fileEncryptionFactory = fileEncryptionFactory;
            this.ftpClient = ftpClient;
            this.settingService = settingService;
            this.keyStoreService = keyStoreService;
            this.logger = logger;
            this.byteProService = byteProService;
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
        private readonly IByteProService byteProService;
        private readonly IRainmakerService rainmakerService;

        #endregion

        #region Action Methods

        #region Get

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
        public async Task<IActionResult> GetLoanApplicationId(int loanApplicationId)

        {
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            var responseBody = await rainmakerService.PostLoanApplication(loanApplicationId, false, Request.Headers["Authorization"].Select(x => x.ToString()));

            if (!string.IsNullOrEmpty(responseBody))
            {
                User user = null;
                try
                {
                    user = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(responseBody);
                }
                catch
                {
                    return BadRequest(new ErrorModel { Code = 400, Message = "Unable to find primary borrower" });
                }
                var id = await documentService.CreateLoanApplication(loanApplicationId, tenantId,user.userId,user.userName);
                if (!string.IsNullOrEmpty(id))
                    return Ok(id);
            }
            return BadRequest(new ErrorModel { Code = 400, Message = "Loan Application does not exist" });
        }

        [HttpGet(template: "[action]")]
        public async Task<IActionResult> GetActivityLog([FromQuery] GetActivityLog getActivityLog)

        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            logger.LogInformation($"GetActivityLog requested by {userProfileId}");
            return Ok(value: await documentService.GetActivityLog(id: getActivityLog.id,
                                                                  requestId: getActivityLog.requestId,
                                                                  docId: getActivityLog.docId));
        }


         
        [HttpGet(template: "[action]")]
        public async Task<IActionResult> GetEmailLog([FromQuery] GetEmailLog getEmailLog)

        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            logger.LogInformation($"GetEmailLog requested by {userProfileId}");
            return Ok(value: await documentService.GetEmailLog(id: getEmailLog.id,requestId: getEmailLog.requestId,docId: getEmailLog.docId));
        }

        [HttpGet(template: "[action]")]
        public async Task<IActionResult> View([FromQuery] View view)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            logger.LogInformation($"document {view.docId} is viewed by {userProfileId}");
            var model = new AdminFileViewModel
            {
                docId = view.docId,
                fileId = view.fileId,
                id = view.id,
                requestId = view.requestId
            };

            var fileviewdto = await documentService.View(model,
                                                         userProfileId,
                                                         HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString(), tenantId);
            var setting = await settingService.GetSetting();

            ftpClient.Setup(hostIp: setting.ftpServer,
                            userName: setting.ftpUser,
                            password: AesCryptography.Decrypt(text: setting.ftpPassword,
                                                              key: await keyStoreService.GetFtpKey()));
           
            using var memoryStream = new MemoryStream();
            await ftpClient.DownloadAsync(remoteFile: fileviewdto.serverName,
                                          memoryStream);
            Stream ms = fileEncryptionFactory.GetEncryptor(name: fileviewdto.encryptionAlgorithm).DecrypeFile(memoryStream,
                                                                                                              password: await keyStoreService.GetFileKey(),
                                                                                                              originalFileName: fileviewdto.clientName,fileviewdto.salt);
          
            return File(ms,
                        fileviewdto.contentType,
                        fileviewdto.clientName);
        }

        #endregion

        #region Post

        [HttpPost(template: "[action]")]
        public async Task<IActionResult> McuRename(McuRenameModel mcuRenameModel)

        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            string userName = User.FindFirst("FirstName").Value.ToString() + ' ' + User.FindFirst("LastName").Value.ToString();
            var setting = await settingService.GetSetting();
            if (mcuRenameModel.newName.Length > setting.maxFileNameSize)
                //throw new DocumentManagementException("File Name size exceeded limit");
                return BadRequest("File Name size exceeded limit");
            logger.LogInformation($"mcurename requested by {userProfileId}, new name is {mcuRenameModel.newName}");
            var docQuery = await documentService.McuRename(id: mcuRenameModel.id,
                                                           requestId: mcuRenameModel.requestId,
                                                           docId: mcuRenameModel.docId,
                                                           fileId: mcuRenameModel.fileId,
                                                           newName: mcuRenameModel.newName,
                                                           userName: userName);
            if (docQuery)
                return Ok();
            return NotFound(new ErrorModel { Code = 404, Message = "unable to find document to rename" });
        }

        [HttpPost(template: "[action]")]
        public async Task<IActionResult> AcceptDocument(AcceptDocumentModel acceptDocumentModel)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            string userName = User.FindFirst("FirstName").Value.ToString() + ' ' + User.FindFirst("LastName").Value.ToString();
            logger.LogInformation($"document {acceptDocumentModel.docId} is accepted by {userProfileId}");
            var docQuery = await documentService.AcceptDocument(id: acceptDocumentModel.id,
                                                                requestId: acceptDocumentModel.requestId,
                                                                docId: acceptDocumentModel.docId,
                                                                userName: userName,
                                                                authHeader: Request.Headers["Authorization"].Select(x => x.ToString()));
            if (docQuery)
            {
                var auth = Request.Headers["Authorization"].Select(x => x.ToString()).ToList();
#pragma warning disable 4014
                Task.Run(async () =>
#pragma warning restore 4014
                {
                        Tenant tenant = await byteProService.GetTenantSetting(tenantId);
                        if (tenant.syncToBytePro == (int)SyncToBytePro.Auto &&
                            tenant.autoSyncToBytePro == (int)AutoSyncToBytePro.OnAccept)
                        {
                            await byteProService.UploadFiles(acceptDocumentModel.id, acceptDocumentModel.requestId, acceptDocumentModel.docId, auth);
                        }
                    });
                return Ok();
            }

            return NotFound(new ErrorModel { Code = 404, Message = "unable to find document to accept" });
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
                                                                userName: userName,
                                                                authHeader: Request.Headers["Authorization"].Select(x => x.ToString()));
            if (docQuery)
            {
                return Ok();
            }

            return NotFound(new ErrorModel { Code = 404, Message = "unable to find document to reject" });
        }
       
        [HttpPost(template: "[action]")]
        public async Task<IActionResult> GetDocumentsByTemplateIds(GetDocumentsByTemplateIds getDocumentsByTemplateIds)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            logger.LogInformation($"GetDocumentsByTemplateIds requested by {userProfileId}");
            var docQuery = await documentService.GetDocumentsByTemplateIds(getDocumentsByTemplateIds.id.ToList(), tenantId);
            return Ok(value: docQuery);
        }

        [HttpDelete(template: "[action]")]
        public async Task<IActionResult> DeleteFile(DeleteFile deleteFile)
        {
            var docQuery = await documentService.DeleteFile(loanApplicationId: deleteFile.loanApplicationId,
                                                                fileId: deleteFile.fileId);
            if (docQuery)
                return Ok();
            return NotFound(new ErrorModel { Code = 404, Message = "unable to find file to delete" });
        }

        #endregion

        #endregion
    }
}