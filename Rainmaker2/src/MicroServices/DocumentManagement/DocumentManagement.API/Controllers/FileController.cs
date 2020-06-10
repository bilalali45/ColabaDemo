using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentManagement.Model;
using DocumentManagement.Service;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Submit(FileSubmitModel model)
        {
            return null;
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
            return null;
        }
        [HttpPut("[action]")]
        public async Task<IActionResult> Order(List<FileNameModel> model)
        {
            return null;
        }
        [HttpGet("[action]")]
        public async Task<byte[]> View(FileViewModel model)
        {
            return null;
        }
    }
}
