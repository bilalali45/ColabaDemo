using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DocumentManagement.Entity
{
    public class TokenParam
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string name { get; set; }
        public string symbol { get; set; }
        public string description { get; set; }
        public string key { get; set; }
        public bool fromAddess { get; set; }
        public bool ccAddess { get; set; }
        public bool emailBody { get; set; }
        public bool emailSubject { get; set; }
    }
}
