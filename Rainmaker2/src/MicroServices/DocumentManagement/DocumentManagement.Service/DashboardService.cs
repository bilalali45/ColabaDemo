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
        public async Task<List<DashboardDTO>> GetPendingDocuments(int loanApplicationId, int tenantId, int userProfileId)
        {
            IMongoCollection<Entity.Request> collection = mongoService.db.GetCollection<Entity.Request>("Request");

            using var asyncCursor = collection.Aggregate(PipelineDefinition<Entity.Request, BsonDocument>.Create(
                @"{""$match"": {

                  ""loanApplicationId"": " + loanApplicationId + @",
                  ""tenantId"": " + tenantId + @",
                  ""userId"": " + userProfileId + @"
                            }
                        }", @"{
                            ""$unwind"": ""$requests""
                        }", @"{
                            ""$match"": {""requests.status"": """ + RequestStatus.Active + @"""}
                        }", @"{
                            ""$unwind"": ""$requests.documents""
                        }", @"{
                            ""$match"": { ""$or"":[
                                {""requests.documents.status"": """ + DocumentStatus.BorrowerTodo + @"""},
                                {""requests.documents.status"": """ + DocumentStatus.Started + @"""}
                            ]}
                        }", @"{
                            ""$lookup"": {
                                ""from"": ""DocumentType"",
                                ""localField"": ""requests.documents.typeId"",
                                ""foreignField"": ""_id"",
                                ""as"": ""documentObjects""
                            }
                        }", @"{
                            ""$unwind"": {
                                ""path"": ""$documentObjects"",
                                ""preserveNullAndEmptyArrays"": true
                            }
                        }", @"{
                            ""$project"": {
                                ""_id"": 1,
                                ""createdOn"": ""$requests.createdOn"",
                                ""requestId"": ""$requests.id"",
                                ""docId"": ""$requests.documents.id"",
                                ""docName"": ""$requests.documents.displayName"",
                                ""docMessage"": ""$requests.documents.message"",
                                ""typeName"": ""$documentObjects.name"",
                                ""typeMessage"": ""$documentObjects.message"",
                                ""messages"": ""$documentObjects.messages"",
                                ""files"": ""$requests.documents.files""
                            }
                        }"
                ));

            List<DashboardDTO> result = new List<DashboardDTO>();
            while (await asyncCursor.MoveNextAsync())
            {
                foreach (var current in asyncCursor.Current)
                {
                    DashboardQuery query = BsonSerializer.Deserialize<DashboardQuery>(current);
                    DashboardDTO dto = new DashboardDTO();
                    dto.id = query.id;
                    dto.docId = query.docId;
                    dto.requestId = query.requestId;
                    dto.docName = string.IsNullOrEmpty(query.docName) ? query.typeName : query.docName;
                    if (string.IsNullOrEmpty(query.docMessage))
                    {
                        if (query.messages?.Any(x => x.tenantId == tenantId) == true)
                        {
                            dto.docMessage = query.messages.Where(x => x.tenantId == tenantId).First().message;
                        }
                        else
                        {
                            dto.docMessage = query.typeMessage;
                        }
                    }
                    else
                    {
                        dto.docMessage = query.docMessage;
                    }
                    dto.files = query.files?.Where(x => x.status != FileStatus.RejectedByMcu).Select(x => new FileDTO()
                    {
                        clientName = x.clientName,
                        fileUploadedOn = DateTime.SpecifyKind(x.fileUploadedOn, DateTimeKind.Utc),
                        id = x.id,
                        order = x.order,
                        size = x.size
                    }).OrderBy(x => x.order).ToList();
                    result.Add(dto);
                }
            }
            return result;
        }

        public async Task<List<DashboardDTO>> GetSubmittedDocuments(int loanApplicationId, int tenantId, int userProfileId)
        {
            IMongoCollection<Entity.Request> collection = mongoService.db.GetCollection<Entity.Request>("Request");
            using var asyncCursor = collection.Aggregate(PipelineDefinition<Entity.Request, BsonDocument>.Create(
                @"{""$match"": {

                  ""loanApplicationId"": " + loanApplicationId + @",
                  ""tenantId"": " + tenantId + @",
                  ""userId"": " + userProfileId + @"
                            }
                        }", @"{
                            ""$unwind"": ""$requests""
                        }", @"{
                            ""$match"": {""requests.status"": """ + RequestStatus.Active + @"""}
                        }", @"{
                            ""$unwind"": ""$requests.documents""
                        }", @"{
                            ""$match"": { ""$or"":[
                                {""requests.documents.status"": """ + DocumentStatus.PendingReview + @"""},
                                {""requests.documents.status"": """ + DocumentStatus.Completed + @"""}
                            ]}
                        }", @"{
                            ""$lookup"": {
                                ""from"": ""DocumentType"",
                                ""localField"": ""requests.documents.typeId"",
                                ""foreignField"": ""_id"",
                                ""as"": ""documentObjects""
                            }
                        }", @"{
                            ""$unwind"": {
                                ""path"": ""$documentObjects"",
                                ""preserveNullAndEmptyArrays"": true
                            }
                        }", @"{
                            ""$project"": {
                                ""_id"": 1,
                                ""createdOn"": ""$requests.createdOn"",
                                ""requestId"": ""$requests.id"",
                                ""docId"": ""$requests.documents.id"",
                                ""docName"": ""$requests.documents.displayName"",
                                ""docMessage"": ""$requests.documents.message"",
                                ""typeName"": ""$documentObjects.name"",
                                ""typeMessage"": ""$documentObjects.message"",
                                ""messages"": ""$documentObjects.messages"",
                                ""files"": ""$requests.documents.files""
                            }
                        }"
                ));
            List<DashboardDTO> result = new List<DashboardDTO>();
            while (await asyncCursor.MoveNextAsync())
            {
                foreach (var current in asyncCursor.Current)
                {
                    DashboardQuery query = BsonSerializer.Deserialize<DashboardQuery>(current);
                    DashboardDTO dto = new DashboardDTO();
                    dto.id = query.id;
                    dto.docId = query.docId;
                    dto.requestId = query.requestId;
                    dto.docName = string.IsNullOrEmpty(query.docName) ? query.typeName : query.docName;
                    if (string.IsNullOrEmpty(query.docMessage))
                    {
                        if (query.messages?.Any(x => x.tenantId == tenantId) == true)
                        {
                            dto.docMessage = query.messages.Where(x => x.tenantId == tenantId).First().message;
                        }
                        else
                        {
                            dto.docMessage = query.typeMessage;
                        }
                    }
                    else
                    {
                        dto.docMessage = query.docMessage;
                    }
                    dto.files = query.files?.Where(x => x.status != FileStatus.RejectedByMcu).Select(x => new FileDTO()
                    {
                        clientName = x.clientName,
                        fileUploadedOn = DateTime.SpecifyKind(x.fileUploadedOn, DateTimeKind.Utc),
                        id = x.id,
                        order = x.order,
                        size = x.size
                    }).OrderBy(x => x.order).ToList();
                    result.Add(dto);
                }
            }
            return result;
        }

        public async Task<List<DashboardStatus>> GetDashboardStatus(int loanApplicationId, int tenantId, int userProfileId)
        {
            List<DashboardStatus> statuses = new List<DashboardStatus>();
            IMongoCollection<StatusList> collection = mongoService.db.GetCollection<StatusList>("StatusList");
            using (var asyncCursor = await collection.FindAsync<StatusList>(FilterDefinition<StatusList>.Empty))
            {
                while (await asyncCursor.MoveNextAsync())
                {
                    foreach (var current in asyncCursor.Current)
                    {
                        statuses.Add(new DashboardStatus()
                        {
                            id = current.id,
                            name = current.name,
                            description = current.description,
                            order = current.order,
                            isCurrentStep = false
                        });
                    }
                }
            }
            IMongoCollection<Entity.LoanApplication> collection1 = mongoService.db.GetCollection<Entity.LoanApplication>("Request");
            using var asyncCursor1 = await collection1.FindAsync(new BsonDocument() {
                {"loanApplicationId",loanApplicationId },
                {"tenantId",tenantId },
                {"userId",userProfileId }
            }, new FindOptions<Entity.LoanApplication, BsonDocument>()
            {
                Projection = new BsonDocument() { {"status", 1 }
            }
            });
            if (await asyncCursor1.MoveNextAsync() && asyncCursor1.Current?.Count() > 0)
            {
                string status = asyncCursor1.Current.First()["status"].ToString();
                statuses.Where(x => x.id == status).First().isCurrentStep = true;
            }
            else
                statuses.Where(x => x.order == 3).First().isCurrentStep = true;
            return statuses.OrderBy(x => x.order).ToList();
        }

        public async Task<string> GetFooterText(int tenantId, int businessUnitId)
        {
            IMongoCollection<BusinessUnit> collection = mongoService.db.GetCollection<BusinessUnit>("BusinessUnit");

            using var asyncCursor = collection.Aggregate(PipelineDefinition<BusinessUnit, BsonDocument>.Create(
                @"{""$match"": {
                  ""tenantId"": " + tenantId + @",
                  ""businessUnitId"": " + businessUnitId + @"}
                        }", @"{
                            ""$project"": {
                                ""_id"": 0,
                                ""footerText"": ""$footerText"",
                            }
                        }"
                ));

            while (await asyncCursor.MoveNextAsync())
            {
                string footerText = string.Empty;
                if (asyncCursor.Current.Count() > 0)
                {
                    FooterQuery query = BsonSerializer.Deserialize<FooterQuery>(asyncCursor.Current.First());
                    footerText = query.footerText;
                }
                return footerText;
            }
            return string.Empty;
        }

    }
}
