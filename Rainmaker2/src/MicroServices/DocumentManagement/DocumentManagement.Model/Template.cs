using DocumentManagement.Entity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DocumentManagement.Model
{
    public class Template
    {
        public static class TemplateType
        {
            public const string SystemTemplate = "System Template"; // system template
            public const string TenantTemplate = "Tenant Template"; // tenant template
            public const string MCUTemplate = "MCU Template"; // mcu template
        }
        public class TemplateModel
        {
            public string id { get; set; }
            public string type { get; set; }
            public string name { get; set; }
            public List<DocumentTypes> docs { get; set; }
        }
        [BsonNoId]
        public class DocumentTypes
        {
            public string typeId { get; set; }
            public string docName { get; set; }
        }

        public class InsertTemplateModel
        {
            [Required(ErrorMessage = "Field Can't be empty")]
            //[RegularExpression(@"^[A-Za-z0-9\s-].{1,254}$", ErrorMessage = "Validation Failed")]
            public string name { get; set; }
        }

        public class TemplateQuery
        {
            [BsonId]
            [BsonRepresentation(BsonType.ObjectId)]
            public string id { get; set; }
            public string type { get; set; }
            public string name { get; set; }
            [BsonRepresentation(BsonType.ObjectId)]
            public string typeId { get; set; }
            public string typeName { get; set; }
            public string docName { get; set; }
        }
        public class DocumentTypeModel
        {
            public string docTypeId { get; set; }
            public string docType { get; set; }
            public string docMessage { get; set; }
            public bool isCommonlyUsed { get; set; }
        }
        public class CategoryDocumentTypeModel
        {
            public string catId { get; set; }
            public string catName { get; set; }
            public List<DocumentTypeModel> documents {get;set;}
        }
        public class CategoryDocumentQuery
        {
            [BsonId]
            [BsonRepresentation(BsonType.ObjectId)]
            public string id { get; set; }
            public string name { get; set; }
            [BsonRepresentation(BsonType.ObjectId)]
            public string docTypeId { get; set; }
            public string docType { get; set; }
            public string docMessage { get; set; }
            public List<Message> messages { get; set; }
            public bool? isCommonlyUsed { get; set; }
        }
        public class AddDocumentModel
        {
            [Required(ErrorMessage = "Field Can't be empty")]
            [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = "Validation Failed")]
            public string templateId { get; set; }

            [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = "Validation Failed")]
            public string typeId { get; set; }

           
          //  [RegularExpression(@"^[A-Za-z0-9\s-].{1,254}$", ErrorMessage = "Validation Failed")]
            public string docName { get; set; }
        }


        public class AddTemplateModel
        {
            [Required(ErrorMessage = "Field Can't be empty")]
            public string name { get; set; }

            [Required(ErrorMessage = "Field Can't be empty")]
            public  List<TemplateDocument> documentTypes { get; set; }
        }

        public class RenameTemplateModel
        {
            [Required(ErrorMessage = "Field Can't be empty")]
            [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = "Validation Failed")]
            public   string id { get; set; }

            [Required(ErrorMessage = "Field Can't be empty")]
            public string name { get; set; }

        }

        public class DeleteTemplateModel
        {
            [Required(ErrorMessage = "Field Can't be empty")]
            [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = "Validation Failed")]
            public string templateId { get; set; }
        }

        public class GetTemplateDocuments
        {
            [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = "Validation Failed")]
            [Required(ErrorMessage = "Field Can't be empty")]
            public string id { get; set; }
        }

        public class TemplateDocument
        {
            
             
            [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = "Validation Failed")]
            public string typeId { get; set; }

          
           // [RegularExpression(@"^[A-Za-z0-9\s-].{1,254}$", ErrorMessage = "Validation Failed")]
            public string docName { get; set; }
        }
    }
}
