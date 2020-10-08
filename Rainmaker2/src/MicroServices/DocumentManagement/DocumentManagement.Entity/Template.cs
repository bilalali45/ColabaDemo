using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace DocumentManagement.Entity
{
    public class Template
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string name { get; set; }
        public int? tenantId { get; set; }
        public int? userId { get; set; }
        public DateTime? createdOn { get; set; }
        public bool isActive { get; set; }
        public List<TemplateDocument> documentTypes { get; set; }
    }
}
