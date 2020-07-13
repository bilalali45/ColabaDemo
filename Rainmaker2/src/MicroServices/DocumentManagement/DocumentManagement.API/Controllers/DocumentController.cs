using System.IO;
using System.Threading.Tasks;
using DocumentManagement.Model;
using DocumentManagement.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            IKeyStoreService keyStoreService)
        {
            this.documentService = documentService;
            this.fileEncryptionFactory = fileEncryptionFactory;
            this.ftpClient = ftpClient;
            this.settingService = settingService;
            this.keyStoreService = keyStoreService;
        }

        #endregion

        #region Private Variables

        private readonly IDocumentService documentService;
        private readonly IFileEncryptionFactory fileEncryptionFactory;
        private readonly IFtpClient ftpClient;
        private readonly ISettingService settingService;
        private readonly IKeyStoreService keyStoreService;

        #endregion

        #region Action Methods

        //Todo Validations: Get actions should have httpGet attribute
        [HttpPost(template: "[action]")]
        public async Task<IActionResult> GetDocumentsByTemplateIds(TemplateIdModel templateIdsModel)
        {
            var docQuery = await documentService.GetDocumentsByTemplateIds(templateIdsModel: templateIdsModel);

            return Ok(value: docQuery);
        }


        //Todo Validations: Get actions should have httpGet attribute
        [HttpPost(template: "[action]")]
        public async Task<IActionResult> GetFiles(string id,
                                                  string requestId,
                                                  string docId)
        {
            return Ok(value: await documentService.GetFiles(id: id,
                                                            requestId: requestId,
                                                            docId: docId));
        }


        //Todo Validations: Get actions should have httpGet attribute
        [HttpPost(template: "[action]")]
        public async Task<IActionResult> GetActivityLog(string id,
                                                        string typeId,
                                                        string docId)

        {
            return Ok(value: await documentService.GetActivityLog(id: id,
                                                                  typeId: typeId,
                                                                  docId: docId));
        }


        //Todo Validations: Get actions should have httpGet attribute
        [HttpPost(template: "[action]")]
        public async Task<IActionResult> GetEmailLog(string id)

        {
            return Ok(value: await documentService.GetEmailLog(id: id));
        }


        //Todo Validations: Post actions should receive data in view models, so that its easy to apply validations
        [HttpPost(template: "[action]")]
        public async Task<IActionResult> McuRename(string id,
                                                   string requestId,
                                                   string docId,
                                                   string fileId,
                                                   string newName)

        {
            var docQuery = await documentService.mcuRename(id: id,
                                                           requestId: requestId,
                                                           docId: docId,
                                                           fileId: fileId,
                                                           newName: newName);
            if (docQuery)
                return Ok();
            return NotFound();
        }


        [HttpPost(template: "[action]")]
        public async Task<IActionResult> AcceptDocument(AcceptDocumentModel acceptDocumentModel)
        {
            var docQuery = await documentService.AcceptDocument(id: acceptDocumentModel.id,
                                                                requestId: acceptDocumentModel.requestId,
                                                                docId: acceptDocumentModel.docId);
            if (docQuery)
                return Ok();
            return NotFound();
        }


        [HttpPost(template: "[action]")]
        public async Task<IActionResult> RejectDocument(RejectDocumentModel rejectDocumentModel)
        {
            var docQuery = await documentService.RejectDocument(id: rejectDocumentModel.id,
                                                                requestId: rejectDocumentModel.requestId,
                                                                docId: rejectDocumentModel.docId,
                                                                message: rejectDocumentModel.message);
            if (docQuery)
                return Ok();
            return NotFound();
        }


        [HttpGet(template: "[action]")]
        public async Task<IActionResult> View(string id,
                                              string requestId,
                                              string docId,
                                              string fileId,
                                              int tenantId)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            var model = new FileViewModel
                        {
                            docId = docId,
                            fileId = fileId,
                            id = id,
                            requestId = requestId,
                            tenantId = tenantId
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