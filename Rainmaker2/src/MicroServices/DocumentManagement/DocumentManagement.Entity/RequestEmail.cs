using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DocumentManagement.Entity
{
    public class RequestEmail
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string emailTemplateId { get; set; }
        public string fromAddress { get; set; }
        public string toAddress { get; set; }
        public string CCAddress { get; set; }
        public string subject { get; set; }
        public string emailBody { get; set; }
        public DateTime createdOn { get; set; }
    }
}
