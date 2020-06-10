using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagement.Entity
{
    [BsonNoId]
    public class RequestFile
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string clientName { get; set; }
        public string serverName { get; set; }
        public string mcuName { get; set; }
        public DateTime fileUploadedOn { get; set; }
        public int size { get; set; }
        public string encryptionKey { get; set; }
        public string encryptionAlgorithm { get; set; }
        public int order { get; set; }
    }
}
