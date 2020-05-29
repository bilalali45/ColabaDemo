using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagement.Entity
{
    public class RequestDocument
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string typeId { get; set; }
        public string displayName { get; set; }
        public string message { get; set; }
        public string status { get; set; }
        public List<RequestFile> files { get; set; }

    }
}
