using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DocManager.Model
{
    public class MoveFromOneCategoryToAnotherCategory
    {
        public string id { get; set; }
        public string fromRequestId { get; set; }
        public string fromDocId { get; set; }
        public string fromFileId { get; set; }
        public string toRequestId { get; set; }
        public string toDocId { get; set; }
    }
    public class MoveFromWorkBenchToTrash
    {
        public string id { get; set; }
   
        public string fromFileId { get; set; }
    }
    public class MoveFromTrashToWorkBench
    {
        public string id { get; set; }

        public string fromFileId { get; set; }
    }
    public class MoveFromCategoryToWorkBench
    {
        public string id { get; set; }
        public string fromRequestId { get; set; }
        public string fromDocId { get; set; }
        public string fromFileId { get; set; }
    }
    public class MoveFromWorkBenchToCategory
    {
        public string id { get; set; }
        public string fromFileId { get; set; }
        public string toRequestId { get; set; }
        public string toDocId { get; set; }
    }
    public class MoveFromCategoryToTrash   
    {
        public string id { get; set; }
        public string fromRequestId { get; set; }
        public string fromDocId { get; set; }
        public string fromFileId { get; set; }

    }
    public class WorkbenchQuery
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public List<RequestFile> files { get; set; }
    }
    public class WorkbenchFile
    {
        public string id { get; set; }
        public string fileId { get; set; }
        public DateTime fileUploadedOn { get; set; }
        public string mcuName { get; set; }
        public int? userId { get; set; }
        public string userName { get; set; }
        public DateTime? fileModifiedOn { get; set; }
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
    public class StatusList
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public int order { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }
    public class StatusNameQuery
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
    }
    public class Request
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public int userId { get; set; }
        public string userName { get; set; }
        public DateTime createdOn { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        public RequestDocument document { get; set; }
    }
    public class RequestDocument
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string typeId { get; set; }
        public string displayName { get; set; }
        public string message { get; set; }
        public string status { get; set; }
        public List<RequestFile> files { get; set; }

    }
    [BsonNoId]
    public class RequestFile
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string clientName { get; set; }
        public string serverName { get; set; }
        public string mcuName { get; set; }
        public DateTime fileUploadedOn { get; set; }
        public int size { get; set; }
        public string encryptionKey { get; set; }
        public string encryptionAlgorithm { get; set; }
        public int order { get; set; }
        public string contentType { get; set; }
        public string status { get; set; }
        public string byteProStatus { get; set; }
        public int? userId { get; set; }
        public string userName { get; set; }
        public bool? isRead { get; set; }
        public string annotations { get; set; }
        public bool? isMcuVisible { get; set; }
        public DateTime? fileModifiedOn { get; set; }
    }
    public class LoanApplicationIdQuery
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public int loanApplicationId { get; set; }
    }
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
    public class ActivityLog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public int userId { get; set; }
        public string userName { get; set; }
        public DateTime dateTime { get; set; }
        public string activity { get; set; }
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
        public List<Log> log { get; set; }
    }
    public class Log
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public DateTime dateTime { get; set; }
        public string activity { get; set; }
    }
    public static class ActivityStatus
    {
        public const string RequestedBy = "Requested By";
        public const string RerequestedBy = "Re-requested By";
        public const string AcceptedBy = "Accepted By : {0}";
        public const string StatusChanged = "Status Changed : {0}";
        public const string RejectedBy = "Rejected By : {0}";
        public const string FileSubmitted = "File Submission : {0}";
        public const string RenamedBy = "Renamed By : {0} \r\n File Name : {1}";
        public const string AddedBy = "Added By";
        public const string AddBy = "Added By : {0}";
    }
    public class ActivityLogIdQuery
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
    }
    public class LastDocUploadQuery
    {
        public DateTime? LastDocUploadDate { get; set; }
    }
    public class LastDocRequestSentDateQuery
    {
        public DateTime? LastDocRequestSentDate { get; set; }
    }
    public class RemainingDocumentsQuery
    {
        public int? RemainingDocuments { get; set; }
    }
    public class OutstandingDocumentsQuery
    {
        public int? OutstandingDocuments { get; set; }
    }
    public class CompletedDocumentsQuery
    {
        public int? CompletedDocuments { get; set; }
    }
    public class SaveModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string status { get; set; }
        public int loanApplicationId { get; set; }
        public int tenantId { get; set; }
        public int userId { get; set; }
        public string userName { get; set; }
        public Request request { get; set; }
    }
    public class User
    {
        public int userId { get; set; }
        public string userName { get; set; }
    }
    public class LockSetting
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public int lockTimeInMinutes { get; set; }
    }
    public class Setting
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string ftpServer { get; set; }
        public string ftpUser { get; set; }
        public string ftpPassword { get; set; }
        public int maxFileSize { get; set; }
        public int maxFileNameSize { get; set; }
        public string[] allowedExtensions { get; set; }
    }
    public class DoneModel
    {
        [Required(ErrorMessage = ValidationMessages.ValidationFailed)]
        [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = ValidationMessages.ValidationFailed)]
        public string id { get; set; }
        [Required(ErrorMessage = ValidationMessages.ValidationFailed)]
        [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = ValidationMessages.ValidationFailed)]
        public string docId { get; set; }
        [Required(ErrorMessage = ValidationMessages.ValidationFailed)]
        [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = ValidationMessages.ValidationFailed)]
        public string requestId { get; set; }
    }
    public static class ValidationMessages
    {
        public const string ValidationFailed = "Validation Failed";
    }
    public class FileViewModel
    {

        public string id { get; set; }


        public string requestId { get; set; }


        public string docId { get; set; }


        public string fileId { get; set; }
    }
    public class Tenant
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public int tenantId { get; set; }
        public string emailTemplate { get; set; }
        public int syncToBytePro { get; set; }
        public int autoSyncToBytePro { get; set; }
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
    }
    public class AdminFileViewModel
    {

        public string id { get; set; }


        public string requestId { get; set; }


        public string docId { get; set; }

        public string fileId { get; set; }
    }
    public class FileIdModel
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string fileId { get; set; }
    }
    public class TenantSetting
    {
        public int syncToBytePro { get; set; }
        public int autoSyncToBytePro { get; set; }
    }
    public enum SyncToBytePro
    {
        Off = 0,
        Auto = 1,
        Manual = 2
    }
    public enum AutoSyncToBytePro
    {
        OnSubmit = 0,
        OnDone = 1,
        OnAccept = 2
    }
    public class ViewCategoryAnnotations
    {
        public string id { get; set; }
        public string fromRequestId { get; set; }
        public string fromDocId { get; set; }
        public string fromFileId { get; set; }

    }
    public class ViewWorkbenchAnnotations
    {
        public string id { get; set; }
        public string fromRequestId { get; set; }
        public string fromDocId { get; set; }
        public string fromFileId { get; set; }

    }
    public class SaveWorkbenchAnnotations
    {
        public string id { get; set; }
       
        public string fileId { get; set; }
        public string annotations { get; set; }

    }
    public class SaveWorkbenchDocument
    {
        public string oldFile { get; set; }

        public string fileId { get; set; }
    }
    public class DeleteModel
    {
        public string id { get; set; }
        public string requestId { get; set; }
        public string docId { get; set; }
    }
    public class SaveCategoryAnnotations
    {
        public string id { get; set; }

        public string requestId { get; set; }
        public string docId { get; set; }
        public string fileId { get; set; }
        public string annotations { get; set; }
    }
    public class SaveTrashAnnotations
    {
        public string id { get; set; }
        public string fileId { get; set; }
        public string annotations { get; set; }
    }
    public class Lock
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public int loanApplicationId { get; set; }
        public int lockUserId { get; set; }
     
        public string lockUserName { get; set; }
        public DateTime lockDateTime { get; set; }

    }
    public class LockModel
    {
        public int loanApplicationId { get; set; }
        
    }
    

}
