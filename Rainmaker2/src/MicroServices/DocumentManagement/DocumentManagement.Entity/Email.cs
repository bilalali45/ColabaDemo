using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagement.Entity
{
    public class Email
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string fromAddress { get; set; }
        public string ccAddress { get; set; }
        public string subject { get; set; }
        public string emailBody { get; set; }
    }
}
