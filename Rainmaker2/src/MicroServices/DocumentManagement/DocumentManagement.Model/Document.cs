using System;
using System.Collections.Generic;
using System.Text;
using DocumentManagement.Entity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DocumentManagement.Model
{
    public class TemplateIdModel
    {
        public List<string> id { get; set; }
        public int tenantId { get; set; }
    }
    public class DocumentModel
    {
        public string docId { get; set; }
        public string docName { get; set; }
        public string docMessage { get; set; }
    }

    public class DocumentQuery
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string docId { get; set; }
        public string typeName { get; set; }
        public string docMessage { get; set; }
        public List<Message> messages { get; set; }
        public string docName { get; set; }
    }
}
