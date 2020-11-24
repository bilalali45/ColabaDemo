using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Setting.Model
{
    public class InsertEmailTemplateModel
    {
        [Required(ErrorMessage = ValidationMessages.ValidationFieldEmpty)]
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = ValidationMessages.ValidationFailed)]
        public int templateTypeId { get; set; }
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
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = ValidationMessages.ValidationFailed)]
        public int id { get; set; }
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
    public class EmailTemplate
    {
        public int id { get; set; }
        public int templateTypeId { get; set; }
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
    public class DeleteTemplateModel
    {
        [Required(ErrorMessage = ValidationMessages.ValidationFieldEmpty)]
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = ValidationMessages.ValidationFailed)]
        public int id { get; set; }
    }

    public class TokenModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string symbol { get; set; }
        public string description { get; set; }
        public string key { get; set; }
    }

    public class TemplateIdModel
    {
        [Required(ErrorMessage = ValidationMessages.ValidationFieldEmpty)]
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = ValidationMessages.ValidationFailed)]
        public int id { get; set; }
        public int loanApplicationId { get; set; }
    }
    public class RenderTemplateIdModel
    {
        [Required(ErrorMessage = ValidationMessages.ValidationFieldEmpty)]
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = ValidationMessages.ValidationFailed)]
        public int id { get; set; }
        [Required(ErrorMessage = ValidationMessages.ValidationFieldEmpty)]
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = ValidationMessages.ValidationFailed)]
        public int loanApplicationId { get; set; }
    }

    public class EmailTemplateModel
    {
        public int id { get; set; }
        public int loanApplicationId { get; set; }
        public string fromAddress { get; set; }
        public string toAddress { get; set; }
        public string subject { get; set; }
        public string emailBody { get; set; }
        public List<TokenModel> lstTokens { get; set; }
    }
}
