﻿using DocumentManagement.Entity;
using DocumentManagement.Model;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
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
    }
}