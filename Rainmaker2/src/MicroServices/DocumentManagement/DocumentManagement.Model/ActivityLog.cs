using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace DocumentManagement.Model
{
    public class ErrorModel
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }
    public class DocumentStatusQuery
    {
        public string status { get; set; }

    }
    public enum SyncToBytePro
    {
        Off=0,
        Auto=1,
        Manual=2
    }

    public enum AutoSyncToBytePro
    {
        OnSubmit=0,
        OnDone=1,
        OnAccept=2
    }
    [BsonNoId]
    public class FileIdModel
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string fileId { get; set; }
    }
    public class ActivityLogDto
    {
        public string id { get; set; }
        public int userId { get; set; }
        public string userName { get; set; }
        public string typeId { get; set; }
        public string docId { get; set; }
        public string activity { get; set; }
        public DateTime dateTime { get; set; }
        public string loanId { get; set; }
        public string message { get; set; }
        public List<ActivityDetailLog> log { get; set; }
    }

    public class ActivityLogQuery
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public int userId { get; set; }
        public string userName { get; set; }
        public DateTime dateTime { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string typeId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string docId { get; set; }
        public string activity { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string loanId { get; set; }
        public string message { get; set; }
        public List<ActivityDetailLog> log { get; set; }
    }

    [BsonNoId]
    public class ActivityDetailLog
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public DateTime dateTime { get; set; }
        public string activity { get; set; }
    }
}
