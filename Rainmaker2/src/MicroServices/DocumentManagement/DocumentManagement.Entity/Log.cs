using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DocumentManagement.Entity
{
    public class Log
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public DateTime dateTime { get; set; }
        public string activity { get; set; }
    }
}
