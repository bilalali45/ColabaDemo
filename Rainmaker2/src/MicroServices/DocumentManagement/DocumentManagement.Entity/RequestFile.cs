using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

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
        public string contentType { get; set; }
        public string status { get; set; }
        public string byteProStatus { get; set; }
        public int? userId { get; set; }
        public string userName { get; set; }
        public bool? isRead { get; set; }
        public string annotations { get; set; }
        public bool? isMcuVisible { get; set; }
        public DateTime? fileModifiedOn { get; set; }
    }
}
