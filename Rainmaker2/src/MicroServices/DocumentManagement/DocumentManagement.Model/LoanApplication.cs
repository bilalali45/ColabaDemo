using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DocumentManagement.Model
{
    public class LoanApplicationModel
    {
        public int? OpportunityId { get; set; }
        public int? LoanRequestId { get; set; }
        public int? BusinessUnitId { get; set; }
    }
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
        public int tenantId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string status { get; set; }
        public int userId { get; set; }
        public string userName { get; set; }
        public List<Request> requests { get; set; }
    }

    public class UploadFileModel
    {
        public int loanApplicationId { get; set; }
        public string documentType { get; set; }
        public string fileName { get; set; }
        public string fileData { get; set; }
    }
}
