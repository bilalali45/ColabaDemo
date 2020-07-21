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
        }

        public class InsertTemplateModel
        {
            public int tenantId { get; set; }
            public string name { get; set; }
        }

        public class TemplateQuery
        {
            [BsonId]
            [BsonRepresentation(BsonType.ObjectId)]
            public string id { get; set; }
            public string type { get; set; }
            public string name { get; set; }
        }
        public class DocumentTypeModel
        {
            public string docTypeId { get; set; }
            public string docType { get; set; }
            public string docMessage { get; set; }
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
        }
        public class AddDocumentModel
        {
            public string templateId { get; set; }
            public int tenantId { get; set; }
            public string typeId { get; set; }
            public string docName { get; set; }
        }


        public class AddTemplateModel
        {
            public int tenantId { get; set; }
            public string name { get; set; }
            public  List<TemplateDocument> documentTypes { get; set; }
        }

        public class RenameTemplateModel
        {
         public   string id { get; set; }
         public int tenantId { get; set; }
            public string name { get; set; }

        }

        public class DeleteTemplateModel
        {
            public string templateId { get; set; }
            public int tenantId { get; set; }
    

        }

        public class GetTemplates
        {
            [Required(ErrorMessage = "Field Can't be empty")]
            public int tenantId { get; set; }

        }
        public class GetTemplateDocuments
        {
            [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = "Validation Failed")]
            [Required(ErrorMessage = "Field Can't be empty")]
            public string id { get; set; }

        }

        public class GetCategoryDocument
        {
            [Required(ErrorMessage = "Field Can't be empty")]
            public int tenantId { get; set; }

        }
    }
}
