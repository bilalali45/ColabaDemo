using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagement.Model
{
    public class StatusNameQuery
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
    }

    public class LoanApplicationIdQuery
    {
        public int loanApplicationId { get; set; }
    }
}
