using DocumentManagement.Entity;
using DocumentManagement.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocumentManagement.Service
{
    public class FileService : IFileService
    {
        private readonly IMongoService mongoService;

        public FileService(IMongoService mongoService)
        {
            this.mongoService = mongoService;
        }
        public async Task<bool> Rename(FileRenameModel model)
        {
            IMongoCollection<Request> collection = mongoService.db.GetCollection<Request>("Request");

            UpdateResult result = await collection.UpdateOneAsync(new BsonDocument()
            {
                { "_id", BsonObjectId.Create(model.id) }
            }, new BsonDocument()
            {
                { "$set", new BsonDocument()
                    {
                        { "requests.$[request].documents.$[document].files.$[file].clientName", model.fileName }
                    }
                }
            }, new UpdateOptions()
            {
                ArrayFilters = new List<ArrayFilterDefinition>()
                {
                    new JsonArrayFilterDefinition<Request>("{ \"request.id\": "+new ObjectId(model.requestId).ToJson()+"}"),
                    new JsonArrayFilterDefinition<Request>("{ \"document.id\": "+new ObjectId(model.docId).ToJson()+"}"),
                    new JsonArrayFilterDefinition<Request>("{ \"file.id\": "+new ObjectId(model.fileId).ToJson()+"}")
                }
            });

            return result.ModifiedCount == 1;
        }
        public async Task<bool> Done(DoneModel model)
        {
            IMongoCollection<Request> collection = mongoService.db.GetCollection<Request>("Request");
            UpdateResult result = await collection.UpdateOneAsync(new BsonDocument()
            {
                { "_id", BsonObjectId.Create(model.id) }
            }, new BsonDocument()
            {
                { "$set", new BsonDocument()
                    {
                        { "requests.$[request].documents.$[document].status", Status.Submitted}

                    }
                }
            }, new UpdateOptions()
            {
                ArrayFilters = new List<ArrayFilterDefinition>()
                {
                    new JsonArrayFilterDefinition<Request>("{ \"request.id\": "+new ObjectId(model.requestId).ToJson()+"}"),
                    new JsonArrayFilterDefinition<Request>("{ \"document.id\": "+new ObjectId(model.docId).ToJson()+"}"),
                }
            });
            return result.ModifiedCount == 1;

        }

        public async Task Order(FileOrderModel model)
        {
            IMongoCollection<Request> collection = mongoService.db.GetCollection<Request>("Request");

            foreach (var item in model.files)
            {
                UpdateResult result = await collection.UpdateOneAsync(new BsonDocument()
                {
                    { "_id", BsonObjectId.Create(model.id) }
                }, new BsonDocument()
                {
                    { "$set", new BsonDocument()
                        {
                            { "requests.$[request].documents.$[document].files.$[file].order", item.order }
                        }
                    }
                }, new UpdateOptions()
                {
                    ArrayFilters = new List<ArrayFilterDefinition>()
                    {
                        new JsonArrayFilterDefinition<Request>("{ \"request.id\": "+new ObjectId(model.requestId).ToJson()+"}"),
                        new JsonArrayFilterDefinition<Request>("{ \"document.id\": "+new ObjectId(model.docId).ToJson()+"}"),
                        new JsonArrayFilterDefinition<Request>("{ \"file.clientName\": \""+item.fileName.Replace("\"","\\\"")+"\"}")
                    }
                });
            }
        }

        public async Task<bool> Submit(string id, string requestId, string docId, string clientName, string serverName, int size, string encryptionKey, string encryptionAlgorithm)
        {
            IMongoCollection<Request> collection = mongoService.db.GetCollection<Request>("Request");

            UpdateResult result = await collection.UpdateOneAsync(new BsonDocument()
            {
                { "_id", BsonObjectId.Create(id) }
            }, new BsonDocument()
            {
                { "$push", new BsonDocument()
                    {
                        { "requests.$[request].documents.$[document].files", new BsonDocument() { { "id", ObjectId.GenerateNewId() }, { "clientName", clientName } , { "serverName", serverName }, { "fileUploadedOn", BsonDateTime.Create(DateTime.UtcNow) }, { "size", size }, { "encryptionKey", encryptionKey }, { "encryptionAlgorithm", encryptionAlgorithm }, { "order" , 0 }, { "mcuName", BsonString.Empty } }   }
                    }
                }
            }, new UpdateOptions()
            {
                ArrayFilters = new List<ArrayFilterDefinition>()
                {
                    new JsonArrayFilterDefinition<Request>("{ \"request.id\": "+new ObjectId(requestId).ToJson()+"}"),
                    new JsonArrayFilterDefinition<Request>("{ \"document.id\": "+new ObjectId(docId).ToJson()+"}")
                }
            });
            return result.ModifiedCount==1;
        }
    }
}