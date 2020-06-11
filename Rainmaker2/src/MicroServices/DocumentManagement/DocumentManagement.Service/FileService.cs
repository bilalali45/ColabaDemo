using DocumentManagement.Entity;
using DocumentManagement.Model;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using DocumentManagement.Entity;
using MongoDB.Driver;
using System.Threading.Tasks;
using DocumentManagement.Model;
using MongoDB.Bson;
using Microsoft.AspNetCore.Http;

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
                    new JsonArrayFilterDefinition<Request>("{ \"request.id\": ObjectId(\""+model.requestId+"\")}"),
                    new JsonArrayFilterDefinition<Request>("{ \"document.id\": ObjectId(\""+model.docId+"\")}"),
                    new JsonArrayFilterDefinition<Request>("{ \"file.id\": ObjectId(\""+model.fileId+"\")}")
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
                    new JsonArrayFilterDefinition<Request>("{ \"request.id\": ObjectId(\""+model.requestId+"\")}"),
                    new JsonArrayFilterDefinition<Request>("{ \"document.id\": ObjectId(\""+model.docId+"\")}"),
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
                        new JsonArrayFilterDefinition<Request>("{ \"request.id\": ObjectId(\""+model.requestId+"\")}"),
                        new JsonArrayFilterDefinition<Request>("{ \"document.id\": ObjectId(\""+model.docId+"\")}"),
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
                    new JsonArrayFilterDefinition<Request>("{ \"request.id\": ObjectId(\""+requestId+"\")}"),
                    new JsonArrayFilterDefinition<Request>("{ \"document.id\": ObjectId(\""+docId+"\")}")
                }
            });
            return result.ModifiedCount==1;
        }
    }
}