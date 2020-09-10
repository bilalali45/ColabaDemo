using System.Linq;
using System.Threading.Tasks;
using LosIntegration.Entity.Models;
using LosIntegration.Service;
using LosIntegration.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace LosIntegration.API.Controllers
{
    [ApiController]
    [Route(template: "api/LosIntegration/[controller]")]
    public class HomeController : Controller
    {
        private readonly IByteDocCategoryMappingService _byteDocCategoryMappingService;
        private readonly IByteDocTypeMappingService _byteDocTypeMappingService;
        private readonly IMappingService _mappingService;


        public HomeController(IByteDocTypeMappingService byteDocTypeMappingService,
                              IMappingService mappingService,
                              IByteDocCategoryMappingService byteDocCategoryMappingService)
        {
            _byteDocTypeMappingService = byteDocTypeMappingService;
            _mappingService = mappingService;
            _byteDocCategoryMappingService = byteDocCategoryMappingService;
        }


        [HttpGet(template: "[action]")]
        [Route(template: "/")]
        public string Index()
        {
            return "LosIntegration.API is running";
        }


        [HttpGet(template: "[action]")]
        [Route(template: "/")]
        public async Task<string> Test()
        {
            var byteDocTypeMapping = _byteDocTypeMappingService
                                     .GetByteDocTypeMappingWithDetails(docType: "Brokerage Statements - Two Months",
                                                                       includes: ByteDocTypeMappingService.RelatedEntity.ByteDocCategoryMapping)
                                     .SingleOrDefault();

            //var mapping = new Mapping
            //              {
            //                  RMEnittyId = "test",
            //                  RMEntityName = "File",
            //                  ExtOriginatorEntityId = "test",
            //                  ExtOriginatorEntityName = "Document",
            //                  ExtOriginatorId = 1
            //              };

            //_mappingService.Insert(item: mapping);
            await _byteDocTypeMappingService.SaveChangesAsync();

            return "test";
        }
    }
}