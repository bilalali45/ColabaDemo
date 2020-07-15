using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagement.Model
{
    public static class ActivityStatus
    {
        public const string RequestedBy = "Requested By : {0}";
        public const string RerequestedBy = "Re-requested By : {0}";
        public const string AcceptedBy = "Accepted By : {0}"; 
        public const string StatusChanged = "Status Changed : {0}";
        public const string RejectedBy = "Rejected By : {0}";
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

    public class User
    {
        public int userId { get; set; }
        public string userName { get; set; }
    }
}
