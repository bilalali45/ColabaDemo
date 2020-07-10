using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagement.Entity
{
    public class LoanApplication
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public int loanApplicationId { get; set; }
        public int tenantId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string status { get; set; }
        public int userId { get; set; }
        public string userName { get; set; }
        public List<Request> requests { get; set; }
    }
}
