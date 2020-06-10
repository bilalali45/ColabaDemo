using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentManagement.Model;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagement.API.Controllers
{
    [ApiController]
    [Route("api/DocumentManagement/[controller]")]
    public class FileController : Controller
    {
        [HttpPost("[action]")]
        public async Task<IActionResult> Submit(FileSubmitModel model)
        {
            return null;
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Done(DoneModel model)
        {
            return null;
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
