using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MongoDB.Bson;

namespace DocumentManagement.Model
{
    public class InsertEmailTemplateModel
    {
        [Required(ErrorMessage = ValidationMessages.ValidationFieldEmpty)]
        public string templateName { get; set; }
        public string templateDescription { get; set; }
        [Required(ErrorMessage = ValidationMessages.ValidationFieldEmpty)]
        public string fromAddress { get; set; }
        public string toAddress { get; set; }
        public string CCAddress { get; set; }
        [Required(ErrorMessage = ValidationMessages.ValidationFieldEmpty)]
        public string subject { get; set; }
        [Required(ErrorMessage = ValidationMessages.ValidationFieldEmpty)]
        public string emailBody { get; set; }
    }
    public class UpdateEmailTemplateModel
    {
        [Required(ErrorMessage = ValidationMessages.ValidationFieldEmpty)]
        [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = ValidationMessages.ValidationFailed)]
        public string id { get; set; }
        [Required(ErrorMessage = ValidationMessages.ValidationFieldEmpty)]
        public string templateName { get; set; }
        public string templateDescription { get; set; }
        [Required(ErrorMessage = ValidationMessages.ValidationFieldEmpty)]
        public string fromAddress { get; set; }
        public string CCAddress { get; set; }
        [Required(ErrorMessage = ValidationMessages.ValidationFieldEmpty)]
        public string subject { get; set; }
        [Required(ErrorMessage = ValidationMessages.ValidationFieldEmpty)]
        public string emailBody { get; set; }
    }
    public class EmailTemplate
    {
        public string id { get; set; }
        public int tenantId { get; set; }
        public string templateName { get; set; }
        public string templateDescription { get; set; }
        public string fromAddress { get; set; }
        public string toAddress { get; set; }
        public string CCAddress { get; set; }
        public string subject { get; set; }
        public string emailBody { get; set; }
        public int sortOrder { get; set; }
    }
    public class TemplateSortModel
    {
        [Required(ErrorMessage = ValidationMessages.ValidationFieldEmpty)]
        [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = ValidationMessages.ValidationFailed)]
        public string id { get; set; }
        public int tenantId { get; set; }
        public string templateName { get; set; }
        public string templateDescription { get; set; }
        public string fromAddress { get; set; }
        public string toAddress { get; set; }
        public string CCAddress { get; set; }
        public string subject { get; set; }
        public string emailBody { get; set; }
        [Required(ErrorMessage = ValidationMessages.ValidationFieldEmpty)]
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = ValidationMessages.ValidationFailed)]
        public int sortOrder { get; set; }
    }
    public class DeleteEmailTemplateModel
    {
        [Required(ErrorMessage = ValidationMessages.ValidationFieldEmpty)]
        [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = ValidationMessages.ValidationFailed)]
        public string id { get; set; }
    }

    public class TokenModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public string symbol { get; set; }
        public string description { get; set; }
        public string key { get; set; }
        public bool fromAddess { get; set; }
        public bool ccAddess { get; set; }
        public bool emailBody { get; set; }
        public bool emailSubject { get; set; }
    }

    public class EmailTemplateIdModel
    {
        [Required(ErrorMessage = ValidationMessages.ValidationFailed)]
        [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = ValidationMessages.ValidationFailed)]
        public string id { get; set; }
    }

    public class RenderTemplateIdModel
    {
        [Required(ErrorMessage = ValidationMessages.ValidationFieldEmpty)]
        [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = ValidationMessages.ValidationFailed)]
        public string id { get; set; }
        [Required(ErrorMessage = ValidationMessages.ValidationFieldEmpty)]
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = ValidationMessages.ValidationFailed)]
        public int loanApplicationId { get; set; }
    }
    public class EmailTemplateModelQuery
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public int tenantId { get; set; }
        public string templateName { get; set; }
        public string templateDescription { get; set; }
        public string fromAddress { get; set; }
        public string toAddress { get; set; }
        public string CCAddress { get; set; }
        public string subject { get; set; }
        public string emailBody { get; set; }
        public int sortOrder { get; set; }
    }
    public class EmailTemplateModel
    {
        public string id { get; set; }
        public int loanApplicationId { get; set; }
        public int tenantId { get; set; }
        public string templateName { get; set; }
        public string templateDescription { get; set; }
        public string fromAddress { get; set; }
        public string CCAddress { get; set; }
        public string toAddress { get; set; }
        public string subject { get; set; }
        public string emailBody { get; set; }
        public List<TokenModel> lstTokens { get; set; }
    }
    public class LastSortOrder
    {
        public int maxSortOrder { get; set; }
    }
    public class TokenParamQuery
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string name { get; set; }
        public string symbol { get; set; }
        public string description { get; set; }
        public string key { get; set; }
        public bool fromAddess { get; set; }
        public bool ccAddess { get; set; }
        public bool emailBody { get; set; }
        public bool emailSubject { get; set; }
    }

}
