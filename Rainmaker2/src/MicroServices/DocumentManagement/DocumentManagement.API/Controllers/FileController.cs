using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DocumentManagement.Model;
using DocumentManagement.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DocumentManagement.API.Controllers
{
    [ApiController]
    [Route("api/DocumentManagement/[controller]")]
    public class FileController : Controller
    {
        private readonly IFileService fileService;
        public FileController(IFileService fileService)
        {
            this.fileService = fileService;
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Submit([FromForm]string id, [FromForm] string requestId, [FromForm] string docId, [FromForm] string order, List<IFormFile> files)
        {
            // save
            foreach(var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var filePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + Path.GetExtension(formFile.FileName)); 

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                    // todo: encrypt file
                    // todo: upload to ftp
                    // todo: insert into mongo
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
        public async Task<byte[]> View(FileViewModel model)
        {
            return null;
        }
    }
}
