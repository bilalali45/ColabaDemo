using DocumentManagement.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Service
{
    public class DashboardService : IDashboardService
    {
        private readonly IMongoService mongoService;
        public DashboardService(IMongoService mongoService)
        {
            this.mongoService = mongoService;
        }
        public async Task<List<DashboardQuery>> GetPendingDocuments(int loadApplicationId, int tenantId)
        {
            return null;
        }
    }
}
