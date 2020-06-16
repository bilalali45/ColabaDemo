using DocumentManagement.Entity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace DocumentManagement.Model
{
    public static class Status
    {
        public const string Requested = "requested";
        public const string Submitted = "submitted";
        public const string Draft = "draft";
    }
    public class DashboardQuery
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public DateTime createdOn { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string docId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string requestId { get; set; }
        public string docName { get; set; }
        public string docMessage { get; set; }
        public string typeName { get; set; }
        public string typeMessage { get; set; }
        public List<Message> messages { get; set; }
        public List<RequestFile> files { get; set; }
    }

    public class DashboardDTO
    {
        public string id { get; set; }
        public string requestId { get; set; }
        public string docId { get; set; }
        public string docName { get; set; }
        public string docMessage { get; set; }
        public List<FileDTO> files { get; set; }
    }

    public class FileDTO
    {
        public string id { get; set; }
        public string clientName { get; set; }
        public DateTime fileUploadedOn { get; set; }
        public int size { get; set; }
        public int order { get; set; }
    }


    public class FileViewDTO
    {
        public string serverName { get; set; }
        public string encryptionKey { get; set; }
        public string encryptionAlgorithm { get; set; }
        public string clientName { get; set; }
        public string contentType { get; set; }
    }

    public class DashboardStatus
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public int order { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public bool isCurrentStep { get; set; }
    }
}
