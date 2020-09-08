using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace DocumentManagement.Entity
{
    public class ActivityLog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public int userId { get; set; }
        public string userName { get; set; }
        public DateTime dateTime { get; set; }
        public string activity { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string typeId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string docId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string requestId { get; set; }
        public string docName { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string loanId { get; set; }
        public string message { get; set; }
        public List<Log> log { get; set; }
    }
}
