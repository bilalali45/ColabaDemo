using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagement.Model
{
    public static class ActivityStatus
    {
        public const string RequestedBy = "Requested By";
        public const string RerequestedBy = "Re-requested By";
        public const string AcceptedBy = "Accepted By"; 
        public const string StatusChanged = "Status Changed";
    }
    public class StatusNameQuery
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
    }

    public class LoanApplicationIdQuery
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public int loanApplicationId { get; set; }
    }

    public class ActivityLogIdQuery
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
    }
}
