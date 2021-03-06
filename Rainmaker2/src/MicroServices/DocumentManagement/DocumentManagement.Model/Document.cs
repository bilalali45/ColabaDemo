using DocumentManagement.Entity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace DocumentManagement.Model
{
    public class TenantSetting
    {
        public int syncToBytePro { get; set; }
        public int autoSyncToBytePro { get; set; }
    }
    public class DocumentDetailQuery
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string docId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string typeId { get; set; }
        public string docName { get; set; }
        public string typeName { get; set; }
        public List<RequestFile> files { get; set; }
        public List<RequestFile> mcuFiles { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string requestId { get; set; }
        public string userName { get; set; }
    }

    public class DocumentDto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string docId { get; set; }
        public string docName { get; set; }
        public string typeId { get; set; }
        public string requestId { get; set; }
        public  List<DocumentFileDto> files { get; set; }
        public string userName { get; set; }
    }

    public class DocumentFileDto
    {
        public string fileId { get; set; }
        public string clientName { get; set; }
        public string mcuName { get; set; }
        public DateTime fileUploadedOn { get; set; }
        public bool isRead { get; set; }
        public int? userId { get; set; }
        public string userName { get; set; }
        public DateTime? fileModifiedOn { get; set; }
    }
    public class TemplateIdModel
    {
        public List<string> id { get; set; }
        public int tenantId { get; set; }
    }
    public class DocumentModel
    {
        public string docId { get; set; }
        public string typeId { get; set; }
        public string docName { get; set; }
        public string docMessage { get; set; }
    }
    public class TemplateDocumentModel
    {
        public string typeId { get; set; }
        public string docName { get; set; }
        public string docMessage { get; set; }
    }
    public class GetTemplateModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public List<TemplateDocumentModel> docs { get; set; }
    }
    public class DocumentQuery
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string docId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string typeId { get; set; }
        public string typeName { get; set; }
        public string docMessage { get; set; }
        public List<Message> messages { get; set; }
        public string docName { get; set; }
    }
    public class TempDocumentQuery
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string name { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string typeId { get; set; }
        public string typeName { get; set; }
        public string docMessage { get; set; }
        public List<Message> messages { get; set; }
        public string docName { get; set; }
    }
    public class AcceptDocumentModel
    {
        [Required(ErrorMessage = ValidationMessages.ValidationFailed)]
        [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = ValidationMessages.ValidationFailed)]
        public string id { get; set; }
        [Required(ErrorMessage = ValidationMessages.ValidationFailed)]
        [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = ValidationMessages.ValidationFailed)]
        public string requestId { get; set; }
        [Required(ErrorMessage = ValidationMessages.ValidationFailed)]
        [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = ValidationMessages.ValidationFailed)]
        public string docId { get; set; }
    }

    public class RejectDocumentModel
    {
        [Required(ErrorMessage = ValidationMessages.ValidationFailed)]
        public int loanApplicationId { get; set; }
        [Required(ErrorMessage = ValidationMessages.ValidationFailed)]
        [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = ValidationMessages.ValidationFailed)]
        public string id { get; set; }
        [Required(ErrorMessage = ValidationMessages.ValidationFailed)]
        [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = ValidationMessages.ValidationFailed)]
        public string requestId { get; set; }
        [Required(ErrorMessage = ValidationMessages.ValidationFailed)]
        [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = ValidationMessages.ValidationFailed)]
        public string docId { get; set; }
        [Required(ErrorMessage = ValidationMessages.ValidationFailed)]
        public string message { get; set; }
    }
    public class DeleteDocumentModel
    {
        [Required(ErrorMessage = "Field Can't be empty")]
        [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = "Validation Failed")]
        public string id { get; set; }

        [Required(ErrorMessage = "Field Can't be empty")]
        [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = "Validation Failed")]
        public string documentId { get; set; } 
    }

    public class GetFiles
    {
        [Required(ErrorMessage = ValidationMessages.ValidationFailed)]
        [RegularExpression(@"^[A-Fa-f\d]{24}$",ErrorMessage = ValidationMessages.ValidationFailed)]
        [FromQuery(Name = "id")]
        public string id { get; set; }
        [Required(ErrorMessage = ValidationMessages.ValidationFailed)]
        [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = ValidationMessages.ValidationFailed)]
        [FromQuery(Name = "requestId")]
        public string requestId { get; set; }
        [Required(ErrorMessage = ValidationMessages.ValidationFailed)]
        [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = ValidationMessages.ValidationFailed)]
        [FromQuery(Name = "docId")]
        public string docId { get; set; }
    }

    public class GetActivityLog
    {
        [Required(ErrorMessage = ValidationMessages.ValidationFailed)]
        [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = ValidationMessages.ValidationFailed)]
        [FromQuery(Name = "id")]
        public string id { get; set; }
        [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = ValidationMessages.ValidationFailed)]
        [FromQuery(Name = "requestId")]
        public string requestId { get; set; }
        [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = ValidationMessages.ValidationFailed)]
        [FromQuery(Name = "docId")]
        public string docId { get; set; }
    }

    public class GetEmailLog
    {
        [Required(ErrorMessage = ValidationMessages.ValidationFailed)]
        [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = ValidationMessages.ValidationFailed)]
        [FromQuery(Name = "id")]
        public string id { get; set; }
        [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = ValidationMessages.ValidationFailed)]
        [FromQuery(Name = "requestId")]
        public string requestId { get; set; }
        [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = ValidationMessages.ValidationFailed)]
        [FromQuery(Name = "docId")]
        public string docId { get; set; }
    }
    public class View
    {
        [Required(ErrorMessage = ValidationMessages.ValidationFailed)]
        [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = ValidationMessages.ValidationFailed)]
        [FromQuery(Name = "id")]
        public string id { get; set; }
        [Required(ErrorMessage = ValidationMessages.ValidationFailed)]
        [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = ValidationMessages.ValidationFailed)]
        [FromQuery(Name = "requestId")]
        public string requestId { get; set; }
        [Required(ErrorMessage = ValidationMessages.ValidationFailed)]
        [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = ValidationMessages.ValidationFailed)]
        [FromQuery(Name = "docId")]
        public string docId { get; set; }
        [Required(ErrorMessage = ValidationMessages.ValidationFailed)]
        [RegularExpression(@"^[A-Fa-f\d]{24}$",ErrorMessage = ValidationMessages.ValidationFailed)]
        [FromQuery(Name = "fileId")]
        public string fileId { get; set; }
    }
    public class GetDocumentsByTemplateIds
    {
        [Required(ErrorMessage = ValidationMessages.ValidationFailed)]
        [ArrayRegularExpression(@"^[A-Fa-f\d]{24}$")]
        public  string[] id { get; set; }
    }

    public class UpdateByteProStatus
    {
        [Required(ErrorMessage = ValidationMessages.ValidationFailed)]
        [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = ValidationMessages.ValidationFailed)]
        public string id { get; set; }
        [Required(ErrorMessage = ValidationMessages.ValidationFailed)]
        [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = ValidationMessages.ValidationFailed)]
        public string requestId { get; set; }
        [Required(ErrorMessage = ValidationMessages.ValidationFailed)]
        [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = ValidationMessages.ValidationFailed)]
        public string docId { get; set; }
        [Required(ErrorMessage = ValidationMessages.ValidationFailed)]
        [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = ValidationMessages.ValidationFailed)]
        public string fileId { get; set; }
        [Required(ErrorMessage = ValidationMessages.ValidationFailed)]
        public bool isUploaded { get; set; }
    }

    public class DeleteFile
    {
        [Required(ErrorMessage = ValidationMessages.ValidationFailed)]
        public int loanApplicationId { get; set; }
        [Required(ErrorMessage = ValidationMessages.ValidationFailed)]
        [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = ValidationMessages.ValidationFailed)]
        public string fileId { get; set; }
    }

    public class ArrayRegularExpressionAttribute : ValidationAttribute
    {
        private readonly string regex;

        public ArrayRegularExpressionAttribute(string regex)
        {
            this.regex = regex;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string[] array = value as string[];
            if(array==null || array.Length<=0)
                return new ValidationResult(ValidationMessages.ValidationFailed);
            foreach (var item in array)
            {
                if(!Regex.IsMatch(item,this.regex))
                    return new ValidationResult(ValidationMessages.ValidationFailed);
            }
            return ValidationResult.Success;
        }
    }

}


 
   
 