using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DocumentManagement.Model
{

    public class EmailLogDto
    {

        public string id { get; set; }
        public int userId { get; set; }
        public string userName { get; set; }
        public string emailText { get; set; }
        public DateTime dateTime { get; set; }
        public string loanId { get; set; }
        public string message { get; set; }
        public string fromAddress { get; set; }
        public string toAddress { get; set; }
        public string CCAddress { get; set; }
        public string subject { get; set; }
    }

    public class EmailLogQuery
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public int userId { get; set; }
        public string userName { get; set; }
        public DateTime dateTime { get; set; }
        public string emailText { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string loanId { get; set; }
        public string message { get; set; }
        public string fromAddress { get; set; }
        public string toAddress { get; set; }
        public string CCAddress { get; set; }
        public string subject { get; set; }
    }

}
