using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DocumentManagement.Entity
{
    public class ByteProLog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string loanId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string requestId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string docId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string fileId { get; set; }
        public string status { get; set; }
        public DateTime updatedOn { get; set; }
        public int userId { get; set; }
        public int tenantId { get; set; }
    }
    public class EmailLog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public int userId { get; set; }
        public string userName { get; set; }
        public DateTime dateTime { get; set; }
        public string emailText { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string loanId { get; set; }
        public string message { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string typeId { get; set; }
        public string docName { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string requestId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string docId { get; set; }
        public string fromAddress { get; set; }
        public string toAddress { get; set; }
        public string CCAddress { get; set; }
        public string subject { get; set; }
    }
}
