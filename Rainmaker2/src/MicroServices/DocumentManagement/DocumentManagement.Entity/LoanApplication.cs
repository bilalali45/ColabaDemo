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
        public int customerId { get; set; }
        public int tenantId { get; set; }
        public int loanApplicationId { get; set; }
        public List<Request> requests { get; set; }
    }
}
