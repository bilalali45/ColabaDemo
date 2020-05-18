using DocumentManagement.Entity;
using DocumentManagement.Model;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<List<DashboardDTO>> GetPendingDocuments(int loanApplicationId, int tenantId)
        {
            var projectionDefinition = new BsonDocument
            {
                { "_id" , 1 },
                { "createdOn" , "$requests.createdOn" },
                { "docId" , "$requests.documents.id" },
                { "docName" , "$requests.documents.displayName" },
                { "docMessage" , "$requests.documents.message" },
                { "typeName" , "$documentObjects.name" },
                { "typeMessage" , "$documentObjects.message" },
                { "messages" , "$documentObjects.messages" },
                { "files" , "$requests.documents.files"}
            };
            IMongoCollection<Request> collection = mongoService.db.GetCollection<Request>("Request");
            using var asyncCursor = await collection.Aggregate()
                .Match(Builders<Request>.Filter.And(
                    Builders<Request>.Filter.Eq("loanApplicationId", loanApplicationId),
                    Builders<Request>.Filter.Eq("tenantId", tenantId)
                    ))
                .Unwind("requests")
                .Unwind("requests.documents")
                .Match(Builders<BsonDocument>.Filter.Eq("requests.documents.status", Status.Requested))
                .Lookup("DocumentType", "requests.documents.typeId", "_id", "documentObjects")
                .Unwind("documentObjects", new AggregateUnwindOptions<BsonDocument>() { PreserveNullAndEmptyArrays = true })
                .Project(projectionDefinition)
                .ToCursorAsync();
            List<DashboardDTO> result = new List<DashboardDTO>();
            while (await asyncCursor.MoveNextAsync())
            {
                foreach (var current in asyncCursor.Current)
                {
                    DashboardQuery query = BsonSerializer.Deserialize<DashboardQuery>(current);
                    DashboardDTO dto = new DashboardDTO();
                    dto.Id = query.Id;
                    dto.docId = query.docId;
                    dto.docName = string.IsNullOrEmpty(query.docName) ? query.typeName : query.docName;
                    dto.docMessage = string.IsNullOrEmpty(query.docMessage) ? 
                        (query.messages?.Any(x=>x.tenantId==tenantId)==true ? 
                        query.messages.Where(x => x.tenantId == tenantId).First().message : 
                        query.typeName) : 
                        query.docMessage;
                    dto.files = query.files;
                    result.Add(dto);
                }
            }
            return result;
        }

        public async Task<List<DashboardDTO>> GetSubmittedDocuments(int loanApplicationId, int tenantId)
        {
            var projectionDefinition = new BsonDocument
            {
                { "_id" , 1 },
                { "createdOn" , "$requests.createdOn" },
                { "docId" , "$requests.documents.id" },
                { "docName" , "$requests.documents.displayName" },
                { "docMessage" , "$requests.documents.message" },
                { "typeName" , "$documentObjects.name" },
                { "typeMessage" , "$documentObjects.message" },
                { "messages" , "$documentObjects.messages" },
                { "files" , "$requests.documents.files"}
            };
            IMongoCollection<Request> collection = mongoService.db.GetCollection<Request>("Request");
            using var asyncCursor = await collection.Aggregate()
                .Match(Builders<Request>.Filter.And(
                    Builders<Request>.Filter.Eq("loanApplicationId", loanApplicationId),
                    Builders<Request>.Filter.Eq("tenantId", tenantId)
                    ))
                .Unwind("requests")
                .Unwind("requests.documents")
                .Match(Builders<BsonDocument>.Filter.Eq("requests.documents.status", Status.Submitted))
                .Lookup("DocumentType", "requests.documents.typeId", "_id", "documentObjects")
                .Unwind("documentObjects", new AggregateUnwindOptions<BsonDocument>() { PreserveNullAndEmptyArrays = true })
                .Project(projectionDefinition)
                .ToCursorAsync();
            List<DashboardDTO> result = new List<DashboardDTO>();
            while (await asyncCursor.MoveNextAsync())
            {
                foreach (var current in asyncCursor.Current)
                {
                    DashboardQuery query = BsonSerializer.Deserialize<DashboardQuery>(current);
                    DashboardDTO dto = new DashboardDTO();
                    dto.Id = query.Id;
                    dto.docId = query.docId;
                    dto.docName = string.IsNullOrEmpty(query.docName) ? query.typeName : query.docName;
                    dto.docMessage = string.IsNullOrEmpty(query.docMessage) ?
                        (query.messages?.Any(x => x.tenantId == tenantId) == true ?
                        query.messages.Where(x => x.tenantId == tenantId).First().message :
                        query.typeName) :
                        query.docMessage;
                    dto.files = query.files;
                    result.Add(dto);
                }
            }
            return result;
        }
    }
}
