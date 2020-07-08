using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DocumentManagement.Entity
{
    public class ActivityLog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }

        public string userId { get; set; }
        public DateTime createdOn { get; set; }
        public string username { get; set; }
        public string requestId { get; set; }
        public string docId { get; set; }
    }
}
