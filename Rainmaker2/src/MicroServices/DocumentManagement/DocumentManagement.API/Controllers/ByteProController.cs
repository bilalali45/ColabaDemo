using DocumentManagement.Model;
using DocumentManagement.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;

namespace DocumentManagement.API.Controllers
{
    [Authorize(Roles = "MCU,Customer")]
    [Route(template: "api/DocumentManagement/[controller]")]
    [ApiController]
    public class ByteProController : ControllerBase
    {
        #region Constructors

        public ByteProController(IDocumentService documentService,
            IFileEncryptionFactory fileEncryptionFactory,
            IFtpClient ftpClient,
            ISettingService settingService,
            IKeyStoreService keyStoreService,
            ILogger<DocumentController> logger,
            IByteProService byteProService,
            IAdminDashboardService adminDashboardService,
            ITemplateService templateService
            )
        {
            this.documentService = documentService;
            this.fileEncryptionFactory = fileEncryptionFactory;
            this.ftpClient = ftpClient;
            this.settingService = settingService;
            this.keyStoreService = keyStoreService;
            this.logger = logger;
            this.byteProService = byteProService;
            this.adminDashboardService = adminDashboardService;
            this.templateService = templateService;
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
        private readonly IAdminDashboardService adminDashboardService;
        private readonly ITemplateService templateService;

        #endregion

        #region Action Methods

        #region Get

        [HttpGet(template: "[action]")]
        public async Task<IActionResult> View([FromQuery] View view)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            logger.LogInformation($"document {view.docId} is viewed by {userProfileId}");
            logger.LogInformation(message: $"request.FileId = {view.fileId}");
            var model = new AdminFileViewModel
            {
                docId = view.docId,
                fileId = view.fileId,
                id = view.id,
                requestId = view.requestId
            };

            var fileviewdto = await byteProService.View(model,tenantId);
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
                        fileviewdto.contentType,
                        fileviewdto.clientName);
        }

        [HttpGet("GetDocuments")]
        public async Task<IActionResult> GetDocuments([FromQuery] GetDocuments moGetDocuments)
        {
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());
            logger.LogInformation($"GetDocuments requested for {moGetDocuments.loanApplicationId} from tenantId {tenantId} and value of pending is {moGetDocuments.pending}");
            var docQuery = await adminDashboardService.GetDocument(moGetDocuments.loanApplicationId, tenantId, moGetDocuments.pending,userProfileId);
            return Ok(docQuery);
        }

        [HttpGet(template: "GetCategoryDocument")]
        public async Task<IActionResult> GetCategoryDocument()
        {
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            var docQuery = await templateService.GetCategoryDocument(tenantId: tenantId);
            return Ok(value: docQuery);
        }
        #endregion

        #region Post

        [HttpPost(template: "[action]")]
        public async Task<IActionResult> UpdateByteProStatus(UpdateByteProStatus updateByteProStatus)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            var docQuery = await documentService.UpdateByteProStatus(id: updateByteProStatus.id,
                                                                requestId: updateByteProStatus.requestId,
                                                                docId: updateByteProStatus.docId,
                                                                fileId: updateByteProStatus.fileId,updateByteProStatus.isUploaded,userProfileId,tenantId);
            if (docQuery)
                return Ok();
            return NotFound();
        }

        #endregion

        #endregion
    }
}
