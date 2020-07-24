using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using DocumentManagement.Entity;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace DocumentManagement.Model
{
    public enum ActivityForType
    {
        LoanApplicationDocumentRequestActivity = 19,
        LoanApplicationDocumentRejectActivity = 20
    }
    public class LoanApplication
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        [Required(ErrorMessage = ValidationMessages.ValidationFailed)]
        public int loanApplicationId { get; set; }
        [Required(ErrorMessage = ValidationMessages.ValidationFailed)]
        public int tenantId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string status { get; set; }
        public int userId { get; set; }
        public string userName { get; set; }
        public List<Request> requests { get; set; }
    }
}
