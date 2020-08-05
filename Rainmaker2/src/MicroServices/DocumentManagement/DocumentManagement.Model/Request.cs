using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using DocumentManagement.Entity;

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
        public const string RenamedBy = "Renamed By : {0} \r\n File Name : {1}";
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

    public class GetDraft
    {
        [Required(ErrorMessage = ValidationMessages.ValidationFailed)]
        [FromQuery(Name = "loanApplicationId")]
        public int loanApplicationId { get; set; }
    }

    public class DraftDTO
    {
        public string message { get; set; }
        public List<DraftDocumentDTO> draftDocument { get; set; }
    }

    public class DraftDocumentDTO
    {
        public string typeId { get; set; }
        public string docName { get; set; }
        public string docMessage { get; set; }
        public string requestId { get; set; }
        public string docId { get; set; }
        public string message { get; set; }
    }
    [BsonNoId]
    public class DraftDocumentQuery
    {
        public string message { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string typeId { get; set; }
        public string docName { get; set; }
        public string docMessage { get; set; }
        public string typeName { get; set; }
        public string typeMessage { get; set; }
        public List<Message> messages { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string requestId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string docId { get; set; }

    }

   
    public class InDraftDocumentQuery
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string requestId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string docId { get; set; }

    }

    public class EmailTemplateQuery
    {
        public string emailTemplate { get; set; }
    }
}
