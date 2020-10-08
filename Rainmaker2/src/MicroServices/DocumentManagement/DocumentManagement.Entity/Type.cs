using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace DocumentManagement.Entity
{
    public class Type
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string categoryId { get; set; }
        public string name { get; set; }
        public int tenantId { get; set; }
        public string message { get; set; }
        public DateTime? createdOn { get; set; }
        public int createdBy { get; set; }
        public bool isActive { get; set; }
        public List<Message> messages { get; set; }
    }

    public class StatusList
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public int order { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }
}
