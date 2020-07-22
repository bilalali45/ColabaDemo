using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace DocumentManagement.Model
{
    public class Request
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public int userId { get; set; }
        public string userName { get; set; }
        public DateTime createdOn { get; set; }
        public string status { get; set; }
        [Required(ErrorMessage = ValidationMessages.ValidationFailed)]
        public string message { get; set; }
        public List<RequestDocument> documents { get; set; }
    }
    public static class ActivityStatus
    {
        public const string RequestedBy = "Requested By : {0}";
        public const string RerequestedBy = "Re-requested By : {0}";
        public const string AcceptedBy = "Accepted By : {0}"; 
        public const string StatusChanged = "Status Changed : {0}";
        public const string RejectedBy = "Rejected By : {0}";
        public const string FileSubmitted = "File submitted : {0}";
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

    public class ExistingActivityLog
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string typeId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string docId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string requestId { get; set; }
        public string docName { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string loanId { get; set; }
        public string message { get; set; }
    }

    public class User
    {
        public int userId { get; set; }
        public string userName { get; set; }
    }
}
