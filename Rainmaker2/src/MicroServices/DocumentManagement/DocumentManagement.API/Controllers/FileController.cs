using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DocumentManagement.Entity;
using DocumentManagement.Model;
using DocumentManagement.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace DocumentManagement.API.Controllers
{
    [ApiController]
    [Route("api/DocumentManagement/[controller]")]
    public class FileController : Controller
    {
        private readonly IFileService fileService;
        private readonly IFileEncryptionFactory fileEncryptionFactory;
        private readonly IFtpClient ftpClient;
        private readonly ISettingService settingService;
        public FileController(IFileService fileService, IFileEncryptionFactory fileEncryptionFactory,IFtpClient ftpClient,ISettingService settingService)
        {
            this.fileService = fileService;
            this.fileEncryptionFactory = fileEncryptionFactory;
            this.ftpClient = ftpClient;
            this.settingService = settingService;
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Submit([FromForm]string id, [FromForm] string requestId, [FromForm] string docId, [FromForm] string order, List<IFormFile> files)
        {
            Setting setting = await settingService.GetSetting();
            ftpClient.Setup(setting.ftpServer, setting.ftpUser, setting.ftpPassword);
            // save
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var filePath = fileEncryptionFactory.GetEncryptor("AES").EncryptFile(formFile.OpenReadStream(),"this is a very long password");
                    // upload to ftp
                    await ftpClient.UploadAsync(Path.GetFileName(filePath),filePath);
                    // insert into mongo
                    var docQuery = await fileService.Submit(formFile.ContentType,id, requestId, docId,formFile.FileName,Path.GetFileName(filePath),(int)formFile.Length,"","AES");
                    System.IO.File.Delete(filePath);
                }
            }
            // set order
            FileOrderModel model = new FileOrderModel
            {
                id = id,
                docId = docId,
                requestId = requestId,
                files = JsonConvert.DeserializeObject<List<FileNameModel>>(order)
            };
            await fileService.Order(model);
            return Ok();
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Done(DoneModel model)
        {
            var docQuery = await fileService.Done(model);
            if (docQuery)
                return Ok();
            else
                return NotFound();
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Rename(FileRenameModel model)
        {
            var docQuery = await fileService.Rename(model);
            if (docQuery)
                return Ok();
            else
                return NotFound();
        }
        [HttpPut("[action]")]
        public async Task<IActionResult> Order(FileOrderModel model)
        {
            await fileService.Order(model);
            return Ok();


        }
        [HttpGet("[action]")]
        public async Task<IActionResult> View(FileViewModel model)
        {
            var fileviewdto = await fileService.View(model);
            Setting setting = await settingService.GetSetting();
            ftpClient.Setup(setting.ftpServer, setting.ftpUser, setting.ftpPassword);
            var filepath = Path.GetTempFileName();
            await ftpClient.DownloadAsync(fileviewdto.serverName, filepath);
            return File(fileEncryptionFactory.GetEncryptor(fileviewdto.encryptionAlgorithm).DecrypeFile(filepath, "this is a very long password",fileviewdto.clientName),fileviewdto.contentType);
        }
    }
}
