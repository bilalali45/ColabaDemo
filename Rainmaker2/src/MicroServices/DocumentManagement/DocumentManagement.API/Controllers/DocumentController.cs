using DocumentManagement.Entity;
using DocumentManagement.Model;
using DocumentManagement.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace DocumentManagement.API.Controllers
{
    [Authorize(Roles = "MCU")]
    [ApiController]
    [Route("api/DocumentManagement/[controller]")]
    public class DocumentController : Controller
    {
        private readonly IDocumentService documentService;
        private readonly IFileEncryptionFactory fileEncryptionFactory;
        private readonly IFtpClient ftpClient;
        private readonly ISettingService settingService;
        private readonly IKeyStoreService keyStoreService;
        public DocumentController(IDocumentService documentService, IFileEncryptionFactory fileEncryptionFactory, IFtpClient ftpClient, ISettingService settingService, IKeyStoreService keyStoreService)
        {
            this.documentService = documentService;
            this.fileEncryptionFactory = fileEncryptionFactory;
            this.ftpClient = ftpClient;
            this.settingService = settingService;
            this.keyStoreService = keyStoreService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetDocumentsByTemplateIds(TemplateIdModel templateIdsModel)
        {
            var docQuery = await documentService.GetDocumentsByTemplateIds(templateIdsModel);

            return Ok(docQuery);
        }
     

        [HttpPost("[action]")]
        public async Task<IActionResult> GetFiles(string id, string requestId, string docId)
        {
            return Ok(await documentService.GetFiles(id, requestId, docId));
        }
       
        [HttpPost("[action]")]
        public async Task<IActionResult> GetActivityLog(string id, string typeId, string docId)
     
        {
            return Ok(await documentService.GetActivityLog(id, typeId, docId));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> GetEmailLog(string id)

        {
            return Ok(await documentService.GetEmailLog(id));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> mcuRename(string id, string requestId, string docId, string fileId, string newName)

        {
            var docQuery = await documentService.mcuRename(id, requestId, docId, fileId, newName);
            if (docQuery)
                return Ok();
            else
                return NotFound();
            
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AcceptDocument(AcceptDocumentModel acceptDocumentModel)
        {
            var docQuery = await documentService.AcceptDocument(acceptDocumentModel.id, acceptDocumentModel.requestId, acceptDocumentModel.docId);
            if (docQuery)
                return Ok();
            else
                return NotFound();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RejectDocument(RejectDocumentModel rejectDocumentModel)
        {
            var docQuery = await documentService.RejectDocument(rejectDocumentModel.id, rejectDocumentModel.requestId, rejectDocumentModel.docId, rejectDocumentModel.message);
            if (docQuery)
                return Ok();
            else
                return NotFound();
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> View(string id, string requestId, string docId, string fileId, int tenantId)
        {
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());
            FileViewModel model = new FileViewModel { docId = docId, fileId = fileId, id = id, requestId = requestId, tenantId = tenantId };

            var fileviewdto = await documentService.View(model, userProfileId, HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString());
            Setting setting = await settingService.GetSetting();

            ftpClient.Setup(setting.ftpServer, setting.ftpUser, AESCryptography.Decrypt(setting.ftpPassword, await keyStoreService.GetFtpKey()));
            var filepath = Path.GetTempFileName();
            await ftpClient.DownloadAsync(fileviewdto.serverName, filepath);

            return File(fileEncryptionFactory.GetEncryptor(fileviewdto.encryptionAlgorithm).DecrypeFile(filepath, await keyStoreService.GetFileKey(), fileviewdto.clientName), fileviewdto.contentType, fileviewdto.clientName);

        }
    }
}
