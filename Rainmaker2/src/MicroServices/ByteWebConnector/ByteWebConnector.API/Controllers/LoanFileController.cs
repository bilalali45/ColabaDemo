using System.Threading.Tasks;
using ByteWebConnector.Model.Models.ActionModels.LoanFile;
using ByteWebConnector.Model.Models.OwnModels;
using ByteWebConnector.Model.Models.ServiceRequestModels.Los;
using ByteWebConnector.Service.ExternalServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Extensions.ExtensionClasses;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ByteWebConnector.API.Controllers
{
    [Route(template: "api/ByteWebConnector/[controller]")]
    [ApiController]
    public class LoanFileController : ControllerBase
    {
        #region Constructor

        public LoanFileController(ILogger<LoanFileController> logger,
                                  IByteProService byteProService)
        {
            _logger = logger;
            _byteProService = byteProService;
        }

        #endregion

        #region Action Method

        [Route(template: "[action]")]
        [HttpPost]
        [DisableRequestSizeLimit]
        public IActionResult SendLoanFile([FromBody] LoanFileRequest loanFileRequest)
        {
            var apiResponse = new ApiResponse<LoanFileInfo>();

            var byteFile = _byteProService.SendFile(loanFileRequest: loanFileRequest);
            var info = new LoanFileInfo()
            {
                FileDataId = byteFile.FileDataID,
                FileName = byteFile.FileData.FileName
            };
            apiResponse.Data = info;
            apiResponse.Code = ApiResponseStatus.Success;

            return Ok(value: apiResponse);
        }

        [Route(template: "[action]")]
        [HttpGet]
        public async Task<IActionResult> GetLoanStatus([FromQuery] string fileDataId)
        {
            int loanStatusId = 0;
            if (string.IsNullOrEmpty(fileDataId))
            {
                return BadRequest("File data ID cannot be null or empty.");
            }
            else
            {
                int id = 0;
                if (!int.TryParse(fileDataId, out id))
                {
                    return BadRequest("File dada id is not valid.");
                }
                else
                {
                    loanStatusId = await this._byteProService.GetLoanStatusAsync(id);
                }


            }
            return Ok(new { loanStatusId = loanStatusId });
        }

        #endregion

        #region Private Fields

        private readonly ILogger<LoanFileController> _logger;
        private readonly IByteProService _byteProService;

        #endregion
    }
}