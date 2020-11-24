using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace DocumentManagement.Entity
{
    public class Request
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public int userId { get; set; }
        public string userName { get; set; }
        public DateTime createdOn { get; set; }
        public string status { get; set; }
        public RequestEmail email { get; set; }
        public List<RequestDocument> documents { get; set; }
    }
}
