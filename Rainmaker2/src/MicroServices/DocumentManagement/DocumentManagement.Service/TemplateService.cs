using DocumentManagement.Entity;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static DocumentManagement.Model.Template;

namespace DocumentManagement.Service
{
    public class TemplateService : ITemplateService
    {
        private readonly IMongoService mongoService;

        public TemplateService(IMongoService mongoService)
        {
            this.mongoService = mongoService;
        }
        public async Task<List<TemplateModel>> GetTemplates(int tenantId, int userProfileId)
        {
            IMongoCollection<Template> collection = mongoService.db.GetCollection<Template>("Template");

           
            List<TemplateModel> templateModels = new List<TemplateModel>();
            return templateModels;
        }
    }
}
