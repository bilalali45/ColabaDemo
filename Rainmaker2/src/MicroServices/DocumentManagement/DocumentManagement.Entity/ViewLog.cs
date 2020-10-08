using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DocumentManagement.Entity
{
    public class ViewLog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public int userProfileId { get; set; }
        public DateTime createdOn { get; set; }
        public string ipAddress { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string loanApplicationId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string requestId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string documentId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string fileId { get; set; }
    }
}
