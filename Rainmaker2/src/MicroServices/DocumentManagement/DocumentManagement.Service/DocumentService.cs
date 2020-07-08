using DocumentManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentManagement.Entity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace DocumentManagement.Service
{
    public class DocumentService : IDocumentService
    {
        private readonly IMongoService mongoService;

        public DocumentService(IMongoService mongoService)
        {
            this.mongoService = mongoService;
        }
        public async Task<List<Document>> GetDocumemntsByTemplateIds(TemplateIdModel templateIdsModel)
        {

            IMongoCollection<Entity.Template> collection = mongoService.db.GetCollection<Entity.Template>("Template");

            using var asyncCursor = collection.Aggregate(PipelineDefinition<Entity.Template, BsonDocument>.Create(
                @"{""$match"": { ""_id"": { ""$in"": " + new BsonArray(templateIdsModel.id.Select(x => new ObjectId(x))).ToJson() + @"
                              }}
                        }", @"{
                            ""$unwind"": ""$documentTypes""
                        }", @"{
                            ""$lookup"": {
                                ""from"": ""DocumentType"",
                                ""localField"": ""documentTypes.typeId"",
                                ""foreignField"": ""_id"",
                                ""as"": ""documents""
                            }
                        }", @"{
                            ""$unwind"": ""$documents""
                        }", @"{
                            ""$project"": {
                                ""_id"": 0,
                                ""docId"": ""$documents._id"",
                                ""typeName"": ""$documents.name"",
                                ""docMessage"": ""$documents.message"",
                                ""messages"": ""$documents.messages"",
                                ""docName"":""$documentTypes.docName""
                            }
                        }"
                ));

            List<Document> result = new List<Document>();
            while (await asyncCursor.MoveNextAsync())
            {
                foreach (var current in asyncCursor.Current)
                {
                    DocumentQuery query = BsonSerializer.Deserialize<DocumentQuery>(current);
                    Document dto = new Document();
                    dto.docId = query.docId;
                    dto.docName = string.IsNullOrEmpty(query.docName) ? query.typeName : query.docName;
                    if (query.messages?.Any(x => x.tenantId == templateIdsModel.tenantId) == true)
                    {
                            dto.docMessage = query.messages.Where(x => x.tenantId == templateIdsModel.tenantId).First().message;
                    }
                    else
                    {
                        dto.docMessage = query.docMessage;
                    }
                    result.Add(dto);
                }
            }
            return result.GroupBy(x=>x.docId).Select(x=>x.First()).ToList();
        }

       
    }
}
