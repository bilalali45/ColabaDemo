using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagement.Model
{
    public class FileNameModel
    {
        [Required(ErrorMessage = ValidationMessages.ValidationFailed)]
        public string fileName { get; set; }
        public int order { get; set; }
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

    public class FileViewModel
    {
        [FromQuery(Name = "id")]
        [Required(ErrorMessage = "Field Can't be empty")]
        [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = "Validation Failed")]
        public string id { get; set; }

        [FromQuery(Name = "requestId")]
        [Required(ErrorMessage = "Field Can't be empty")]
        [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = "Validation Failed")]
        public string requestId { get; set; }

        [FromQuery(Name = "docId")]
        [Required(ErrorMessage = "Field Can't be empty")]
        [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = "Validation Failed")]
        public string docId { get; set; }

        [FromQuery(Name = "fileId")]
        [Required(ErrorMessage = "Field Can't be empty")]
        [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = "Validation Failed")]
        public string fileId { get; set; }
    }

    public class AdminFileViewModel
    {
        [FromQuery(Name = "id")]
        [Required(ErrorMessage = "Field Can't be empty")]
        [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = "Validation Failed")]
        public string id { get; set; }

        [FromQuery(Name = "requestId")]
        [Required(ErrorMessage = "Field Can't be empty")]
        [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = "Validation Failed")]
        public string requestId { get; set; }

        [FromQuery(Name = "docId")]
        [Required(ErrorMessage = "Field Can't be empty")]
        [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = "Validation Failed")]
        public string docId { get; set; }

        [FromQuery(Name = "fileId")]
        [Required(ErrorMessage = "Field Can't be empty")]
        [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = "Validation Failed")]
        public string fileId { get; set; }
    }
    public class FileRenameModel
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
        [Required(ErrorMessage = ValidationMessages.ValidationFailed)]
        [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = ValidationMessages.ValidationFailed)]
        public string fileId { get; set; }
        [Required(ErrorMessage = ValidationMessages.ValidationFailed)]
        public string fileName { get; set; }
    }

    public class FileOrderModel
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
        public List<FileNameModel> files { get; set; }
    }

}
