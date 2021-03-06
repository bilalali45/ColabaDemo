using DocumentManagement.Entity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DocumentManagement.Model
{
    public class DashboardSettingModel
    {
        public int userId { get; set; }
        public bool pending { get; set; }
    }
    public static class RequestStatus
    {
        public const string Active = "Active"; // mcu submit
        public const string Draft = "Draft"; // mcu draft
    }
    public static class DocumentStatus
    {
        public const string Draft = "In draft"; // under mcu process
        public const string BorrowerTodo = "Borrower to do"; // mcu request
        public const string PendingReview = "Pending review"; // borrower submit
        public const string Started = "Started"; // borrower has added a file or rejected by mcu
        public const string Completed = "Completed"; // mcu has accepted
        public const string Deleted = "Deleted"; // deleted
        public const string ManuallyAdded = "Manually added"; // ManuallyAdded
    }
    public static class FileStatus
    {
        public const string SubmittedToMcu = "Submitted to MCU"; // borrower submit
        public const string RejectedByMcu = "Rejected by MCU"; // mcu has rejected, want file again
        public const string Deleted = "Deleted";
    }
    public static class ByteProStatus
    {
        public const string Synchronized = "Synchronized";
        public const string NotSynchronized = "Not synchronized";
        public const string Error = "Error";
    }
    public class DashboardQuery
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public DateTime createdOn { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string docId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string requestId { get; set; }
        public string docName { get; set; }
        public string docMessage { get; set; }
        public string typeName { get; set; }
        public string typeMessage { get; set; }
        public List<Message> messages { get; set; }
        public List<RequestFile> files { get; set; }
        public bool? isRejected { get; set; }
    }

    public class DashboardDTO
    {
        public string id { get; set; }
        public string requestId { get; set; }
        public string docId { get; set; }
        public string docName { get; set; }
        public string docMessage { get; set; }
        public bool isRejected { get; set; }
        public List<FileDto> files { get; set; }
    }

    public class FileDto
    {
        public string id { get; set; }
        public string clientName { get; set; }
        public DateTime fileUploadedOn { get; set; }
        public int size { get; set; }
        public int order { get; set; }
    }
    public class AdminDashboardQuery
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string docId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string requestId { get; set; }
        public string docName { get; set; }

        public string typeName { get; set; }

        public string status { get; set; }
        public DateTime? createdOn { get; set; }
        public List<RequestFile> files { get; set; }
        public List<RequestFile> mcuFiles { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string typeId { get; set; }
        public string userName { get; set; }
        public bool? isMcuVisible { get; set; }
    }
    public class AdminDashboardDto
    {
        public string id { get; set; }
        public string requestId { get; set; }
        public string docId { get; set; }
        public string docName { get; set; }
        public string status { get; set; }
        public DateTime? createdOn { get; set; }
        public List<AdminFileDto> files { get; set; }
        public string typeId { get; set; }
        public string userName { get; set; }
    }

    public class AdminFileDto
    {
        public string id { get; set; }
        public string clientName { get; set; }
        public DateTime fileUploadedOn { get; set; }
        public string mcuName { get; set; }
        public string byteProStatus { get; set; }
        public bool isRead { get; set; }
        public string status { get; set; }
        public int? userId { get; set; }
        public string userName { get; set; }
        public DateTime? fileModifiedOn { get; set; }
    }
    public class AdminDeleteModel
    {
        [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = "Validation Failed")]
        [Required(ErrorMessage = "Field Can't be empty")]
        public string id { get; set; }

        [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = "Validation Failed")]
        [Required(ErrorMessage = "Field Can't be empty")]
        public string docId { get; set; }

        [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = "Validation Failed")]
        [Required(ErrorMessage = "Field Can't be empty")]
        public string requestId { get; set; }
    }

    public class FileViewDto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public int loanApplicationId { get; set; }

        public string serverName { get; set; }
        public string encryptionKey { get; set; }
        public string encryptionAlgorithm { get; set; }
        public string clientName { get; set; }
        public string contentType { get; set; }
        public string mcuName { get; set; }
        public string salt { get; set; }
    }

    public class DashboardStatus
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public int order { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public bool isCurrentStep { get; set; }
    }
    public class FooterQuery
    {
        public string footerText { get; set; }
    }
    public class TemplateDocumentQuery
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string docId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string requestId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string typeId { get; set; }
        public string docName { get; set; }

        public string typeName { get; set; }

        public string status { get; set; }

        public List<RequestFile> files { get; set; }
    }
    public class TemplateDto
    {

        public string docId { get; set; }
        public string docName { get; set; }

        public string typeId { get; set; }
    }

    public class RequestIdQuery
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string requestId { get; set; }
        public bool? isMcuVisible { get; set; }
    }

    public class GetDocuments
    {
        [FromQuery(Name = "loanApplicationId")]
        [Required(ErrorMessage = "Field Can't be empty")]
        public int loanApplicationId { get; set; }

        [FromQuery(Name = "pending")]
        [Required(ErrorMessage = "Field Can't be empty")]
        public bool pending { get; set; }

    }
    public class IsDocumentDraft
    {
        [FromQuery(Name = "loanApplicationId")]
        [Required(ErrorMessage = "Field Can't be empty")]
        public int loanApplicationId { get; set; }
    }
    public class GetPendingDocuments
    {
        [FromQuery(Name = "loanApplicationId")]
        [Required(ErrorMessage = "Field Can't be empty")]
        public int loanApplicationId { get; set; }
    }
    public class GetPendingDocumentsByLoanApplication
    {
      
        public int[] loanApplicationId { get; set; }
    }
    public class GetSubmittedDocuments
    {
        [FromQuery(Name = "loanApplicationId")]
        [Required(ErrorMessage = "Field Can't be empty")]
        public int loanApplicationId { get; set; }
    }
    public class GetDashboardStatus
    {
        [FromQuery(Name = "loanApplicationId")]
        [Required(ErrorMessage = "Field Can't be empty")]
        public int loanApplicationId { get; set; }
    }
    public class GetFooterText
    {
        [FromQuery(Name = "loanApplicationId")]
        [Required(ErrorMessage = "Field Can't be empty")]
        public int loanApplicationId { get; set; }

    }

    public class TaskCountQuery
    {
        public int _id { get; set; }
        public int count { get; set; }

    }

    public class TaskCountDTO
    {
     public int loanId { get; set; }
        public int count { get; set; }

    }

}
