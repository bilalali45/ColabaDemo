using System.Linq;
using LosIntegration.Data;
using LosIntegration.Service;
using LosIntegration.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using URF.Core.Abstractions;

namespace LosIntegration.API.Controllers
{
    [ApiController]
    [Route(template: "api/LosIntegration/[controller]")]
    public class HomeController : Controller
    {
        private readonly IByteDocTypeMappingService _byteDocTypeMappingService;
        private readonly IUnitOfWork<LosIntegrationContext> _unitOfWork;

        public HomeController(IByteDocTypeMappingService byteDocTypeMappingService,
                              IUnitOfWork<LosIntegrationContext> unitOfWork)
        {
            _byteDocTypeMappingService = byteDocTypeMappingService;
            _unitOfWork = unitOfWork;
        }


        [HttpGet(template: "[action]")]
        [Route(template: "/")]
        public string Index()
        {
            return "LosIntegration.API is running";
        }
    }
}