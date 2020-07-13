using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DocumentManagement.Entity;
using DocumentManagement.Model;
using DocumentManagement.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Newtonsoft.Json;
using System.Web;

namespace DocumentManagement.API.Controllers
{
    [Authorize(Roles = "Customer")]
    [ApiController]
    [Route("api/DocumentManagement/[controller]")]
    public class FileController : Controller
    {
        private readonly IFileService fileService;
        private readonly IFileEncryptionFactory fileEncryptionFactory;
        private readonly IFtpClient ftpClient;
        private readonly ISettingService settingService;
        private readonly IKeyStoreService keyStoreService;
        private readonly IConfiguration config;
        public FileController(IFileService fileService, IFileEncryptionFactory fileEncryptionFactory,IFtpClient ftpClient,ISettingService settingService, IKeyStoreService keyStoreService, IConfiguration config)
        {
            this.fileService = fileService;
            this.fileEncryptionFactory = fileEncryptionFactory;
            this.ftpClient = ftpClient;
            this.settingService = settingService;
            this.keyStoreService = keyStoreService;
            this.config = config;
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Submit([FromForm]string id, [FromForm] string requestId, [FromForm] string docId, [FromForm] string order,[FromForm] int tenantId, List<IFormFile> files)
        {
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());
            var algo = config["File:Algo"];
            var key = config["File:Key"];
            Setting setting = await settingService.GetSetting();

            ftpClient.Setup(setting.ftpServer, setting.ftpUser, AESCryptography.Decrypt(setting.ftpPassword,await keyStoreService.GetFtpKey()));
            // save
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {

                    if (formFile.Length > setting.maxFileSize)
                        throw new Exception("File size exceeded limit");
                    var filePath = fileEncryptionFactory.GetEncryptor(algo).EncryptFile(formFile.OpenReadStream(),await keyStoreService.GetFileKey());
                    // upload to ftp
                    await ftpClient.UploadAsync(Path.GetFileName(filePath),filePath);
                    // insert into mongo
                    var docQuery = await fileService.Submit(formFile.ContentType,id, requestId, docId,formFile.FileName,Path.GetFileName(filePath),(int)formFile.Length,key,algo,tenantId,userProfileId);
                    System.IO.File.Delete(filePath);
                }
            }
            // set order
            FileOrderModel model = new FileOrderModel
            {
                id = id,
                docId = docId,
                requestId = requestId,
                files = JsonConvert.DeserializeObject<List<FileNameModel>>(order),
                tenantId=tenantId
            };
            await fileService.Order(model,userProfileId);
            return Ok();
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Done(DoneModel model)
        {
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());
            var docQuery = await fileService.Done(model,userProfileId);
            if (docQuery)
                return Ok();
            else
                return NotFound();
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Rename(FileRenameModel model)
        {
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());
            var docQuery = await fileService.Rename(model,userProfileId);
            if (docQuery)
                return Ok();
            else
                return NotFound();
        }
        [HttpPut("[action]")]
        public async Task<IActionResult> Order(FileOrderModel model)
        {
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());
            await fileService.Order(model,userProfileId);
            return Ok();
        }
        [Authorize(Roles = "MCU,Customer")]
        [HttpGet("[action]")]
        public async Task<IActionResult> View(string id, string requestId, string docId, string fileId, int tenantId)
        {
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());
            FileViewModel model = new FileViewModel { docId = docId, fileId = fileId, id = id, requestId = requestId,tenantId=tenantId };

            var fileviewdto = await fileService.View(model,userProfileId, HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString());
            Setting setting = await settingService.GetSetting();
            
            ftpClient.Setup(setting.ftpServer, setting.ftpUser, AESCryptography.Decrypt(setting.ftpPassword,await keyStoreService.GetFtpKey()));
            var filepath = Path.GetTempFileName();
            await ftpClient.DownloadAsync(fileviewdto.serverName, filepath);

            return File(fileEncryptionFactory.GetEncryptor(fileviewdto.encryptionAlgorithm).DecrypeFile(filepath, await keyStoreService.GetFileKey(), fileviewdto.clientName), fileviewdto.contentType, fileviewdto.clientName);

        }
    }
}
