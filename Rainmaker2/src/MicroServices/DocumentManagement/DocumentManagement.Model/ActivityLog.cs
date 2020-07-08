using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DocumentManagement.Model
{
     public class ActivityLogDTO
    {
        public string id { get; set; }
        public int userId { get; set; }
        public string userName { get; set; }
        public string requestId { get; set; }
        public string docId { get; set; }
        public string activity { get; set; }
        public DateTime dateTime { get; set; }
        public string loanId { get; set; }
    }

    public class ActivityLogQuery
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public int userId { get; set; }
        public string userName { get; set; }
        public DateTime dateTime { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string requestId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string docId { get; set; }
        public string activity { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string loanId { get; set; }
        
    }
}
