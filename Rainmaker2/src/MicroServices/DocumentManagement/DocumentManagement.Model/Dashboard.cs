using DocumentManagement.Entity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace DocumentManagement.Model
{
    public class DashboardQuery
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTime createdOn { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string docId { get; set; }
        public string docName { get; set; }
        public string docMessage { get; set; }
        public string typeName { get; set; }
        public string typeMessage { get; set; }
        public List<Message> messages { get; set; }
    }

    public class DashboardDTO
    {
        public string Id { get; set; }
        public string docId { get; set; }
        public string docName { get; set; }
        public string docMessage { get; set; }
    }
}
