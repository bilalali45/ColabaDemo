using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentManagement.Entity;
using DocumentManagement.Service;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace DocumentManagement.API.Controllers
{
    [ApiController]
    [Route("api/DocumentManagement/[controller]")]
    public class DashboardController : Controller
    {
        private readonly IDashboardService dashboardService;
        private readonly IMongoServiceAggregate<Request> requestService;
        private readonly IMongoServiceAggregate<BsonDocument> bsonService;
        public DashboardController(IDashboardService dashboardService, IMongoServiceAggregate<Request> requestService, IMongoServiceAggregate<BsonDocument> bsonService)
        {
            this.dashboardService = dashboardService;
            this.requestService = requestService;
            this.bsonService = bsonService;
        }
        [HttpGet("GetPendingDocuments")]
        public async Task<IActionResult> GetPendingDocuments(int loanApplicationId, int tenantId)
        {
            var docQuery = await dashboardService.GetPendingDocuments(loanApplicationId,tenantId,requestService,bsonService);
            return Ok(docQuery);
        }
        [HttpGet("GetSubmittedDocuments")]
        public async Task<IActionResult> GetSubmittedDocuments(int loanApplicationId, int tenantId)
        {
            var docQuery = await dashboardService.GetSubmittedDocuments(loanApplicationId, tenantId);
            return Ok(docQuery);
        }
    }
}