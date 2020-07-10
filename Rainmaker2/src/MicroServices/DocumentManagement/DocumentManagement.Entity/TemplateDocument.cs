using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagement.Entity
{
    public class TemplateDocument
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string typeId { get; set; }

        public string docName { get; set; }
    }
}
