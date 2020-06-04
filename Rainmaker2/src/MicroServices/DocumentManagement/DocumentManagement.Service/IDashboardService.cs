﻿using DocumentManagement.Entity;
using DocumentManagement.Model;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Service
{
    public interface IDashboardService
    {
        Task<List<DashboardDTO>> GetPendingDocuments(int loanApplicationId, int tenantId, IMongoAggregateService<Request> requestService, IMongoAggregateService<BsonDocument> bsonService);
        Task<List<DashboardDTO>> GetSubmittedDocuments(int loanApplicationId, int tenantId);
    }
}
