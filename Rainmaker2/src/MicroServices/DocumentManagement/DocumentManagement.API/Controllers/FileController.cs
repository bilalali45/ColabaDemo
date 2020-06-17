using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DocumentManagement.Entity;
using DocumentManagement.Model;
using DocumentManagement.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        private readonly IHttpClientFactory clientFactory;
        private readonly IConfiguration config;
        public FileController(IFileService fileService, IFileEncryptionFactory fileEncryptionFactory,IFtpClient ftpClient,ISettingService settingService, IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            this.fileService = fileService;
            this.fileEncryptionFactory = fileEncryptionFactory;
            this.ftpClient = ftpClient;
            this.settingService = settingService;
            this.clientFactory = httpClientFactory;
            this.config = config;
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Submit([FromForm]string id, [FromForm] string requestId, [FromForm] string docId, [FromForm] string order, List<IFormFile> files)
        {
            var httpClient = clientFactory.CreateClient();
            var key = config["File:Key"];
            var ftpKey = config["File:FtpKey"];
            var algo = config["File:Algo"];
            var csResponse = await httpClient.GetAsync($"{config["KeyStore:Url"]}/api/keystore/keystore?key={key}");
            var ftpKeyResponse = await httpClient.GetAsync($"{config["KeyStore:Url"]}/api/keystore/keystore?key={ftpKey}");
            if (!csResponse.IsSuccessStatusCode)
            {
                throw new Exception("Unable to load key from key store");
            }

            if (!ftpKeyResponse.IsSuccessStatusCode)
            {
                throw new Exception("Unable to load key from key store");
            }
            Setting setting = await settingService.GetSetting();

            ftpClient.Setup(setting.ftpServer, setting.ftpUser, AESCryptography.Decrypt(setting.ftpPassword,await ftpKeyResponse.Content.ReadAsStringAsync()));
            // save
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var filePath = fileEncryptionFactory.GetEncryptor(algo).EncryptFile(formFile.OpenReadStream(),await csResponse.Content.ReadAsStringAsync());
                    // upload to ftp
                    await ftpClient.UploadAsync(Path.GetFileName(filePath),filePath);
                    // insert into mongo
                    var docQuery = await fileService.Submit(formFile.ContentType,id, requestId, docId,formFile.FileName,Path.GetFileName(filePath),(int)formFile.Length,key,algo);
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
        public async Task<IActionResult> View(string id, string requestId, string docId, string fileId)
        {
            FileViewModel model = new FileViewModel { docId = docId, fileId = fileId, id = id, requestId = requestId };
            var httpClient = clientFactory.CreateClient();
            var fileviewdto = await fileService.View(model);
            Setting setting = await settingService.GetSetting();
            var key = await httpClient.GetAsync($"{config["KeyStore:Url"]}/api/keystore/keystore?key={config["File:FtpKey"]}");
            if(!key.IsSuccessStatusCode)
            {
                throw new Exception("Unable to load key from key store");
            }
            ftpClient.Setup(setting.ftpServer, setting.ftpUser, AESCryptography.Decrypt(setting.ftpPassword,await key.Content.ReadAsStringAsync()));
            var filepath = Path.GetTempFileName();
            await ftpClient.DownloadAsync(fileviewdto.serverName, filepath);


            var csResponse = await httpClient.GetAsync($"{config["KeyStore:Url"]}/api/keystore/keystore?key={fileviewdto.encryptionKey}");
            if (!csResponse.IsSuccessStatusCode)
            {
                throw new Exception("Unable to load key from key store");
            }

            return File(fileEncryptionFactory.GetEncryptor(fileviewdto.encryptionAlgorithm).DecrypeFile(filepath, await csResponse.Content.ReadAsStringAsync(), fileviewdto.clientName), fileviewdto.contentType, fileviewdto.clientName);

        }
    }
}
