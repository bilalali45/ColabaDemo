using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DocumentManagement.Model
{
    public static class JobType
    {
        public const int EmailReminder = 1; 
        public const int LoanStatusUpdate = 2; 
    }
    public class AddEmailReminder
    {
        [Required(ErrorMessage = ValidationMessages.ValidationFieldEmpty)]
        public int noOfDays { get; set; }
        [Required(ErrorMessage = ValidationMessages.ValidationFieldEmpty)]
        public DateTime recurringTime { get; set; }
        public List<Email> email { get; set; }
    }

    public class UpdateEmailReminder
    {
        [Required(ErrorMessage = ValidationMessages.ValidationFieldEmpty)]
        [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = ValidationMessages.ValidationFailed)]
        public string id { get; set; }
        [Required(ErrorMessage = ValidationMessages.ValidationFieldEmpty)]
        public int noOfDays { get; set; }
        [Required(ErrorMessage = ValidationMessages.ValidationFieldEmpty)]
        public DateTime recurringTime { get; set; }
        public List<Email> email { get; set; }
    }

    public class Email
    {
        [Required(ErrorMessage = ValidationMessages.ValidationFieldEmpty)]
        public string fromAddress { get; set; }
        public string CCAddress { get; set; }
        [Required(ErrorMessage = ValidationMessages.ValidationFieldEmpty)]
        public string subject { get; set; }
        [Required(ErrorMessage = ValidationMessages.ValidationFieldEmpty)]
        public string emailBody { get; set; }
    }
    public class EmailReminderModel
    {
        public bool isActive { get; set; }
        public List<EmailReminder> emailReminders { get; set; }
    }
    public class EmailReminder
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public int noOfDays { get; set; }
        public DateTime recurringTime { get; set; }
        public bool isActive { get; set; }
        public EmailDetail email { get; set; }
    }
    public class EmailDetail
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string fromAddress { get; set; }
        public string ccAddress { get; set; }
        public string subject { get; set; }
        public string emailBody { get; set; }
    }

    public class DeleteEmailReminderModel
    {
        [Required(ErrorMessage = ValidationMessages.ValidationFieldEmpty)]
        [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = ValidationMessages.ValidationFailed)]
        public string id { get; set; }
    }

    public class EnableDisableEmailReminderModel
    {
        [Required(ErrorMessage = ValidationMessages.ValidationFieldEmpty)]
        [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = ValidationMessages.ValidationFailed)]
        public string id { get; set; }
        [Required(ErrorMessage = ValidationMessages.ValidationFieldEmpty)]
        public bool isActive { get; set; }
    }
    public class EnableDisableAllEmailReminderModel
    {
        [Required(ErrorMessage = ValidationMessages.ValidationFieldEmpty)]
        public bool isActive { get; set; }
    }
    public class DisableAllEmailReminderModel
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
    }

    public class EmailReminderLogModel
    {
        public int tenantId { get; set; }
        public int loanApplicationId { get; set; }
        public int jobTypeId { get; set; }
    }

    public class RemainingDocuments
    {
        public int loanApplicationId { get; set; }
        public bool isDocumentRemaining { get; set; }
    }
    public class RemainingDocumentsModel
    { 
        public List<RemainingDocuments> remainingDocuments { get; set; }
    }
    public class RemainingDocumentQuery
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
    }
    public class EmailReminderByIdModel
    {
        [Required(ErrorMessage = ValidationMessages.ValidationFieldEmpty)]
        [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = ValidationMessages.ValidationFailed)]
        public string id { get; set; }
    }

    public class GetEmailRemidersByIds
    {
        [Required(ErrorMessage = ValidationMessages.ValidationFailed)]
        [ArrayRegularExpression(@"^[A-Fa-f\d]{24}$")]
        public string[] id { get; set; }
    }
    public class DeleteEmailReminderLogModel
    {
        public string id { get; set; }
    }
    public class EnableDisableReminderModel
    {
        public string[] id { get; set; }
        public bool isActive { get; set; }
    }
    public class JobTypeModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool isActive { get; set; }
    }
    public class GetJobTypeModel
    {
        public int jobTypeId { get; set; }
    }
}
