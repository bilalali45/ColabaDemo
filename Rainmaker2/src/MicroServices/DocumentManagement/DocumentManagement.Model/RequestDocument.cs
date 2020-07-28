using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace DocumentManagement.Model
{
   public class RequestDocument
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = ValidationMessages.ValidationFailed)]
        public string typeId { get; set; }
        //[RegularExpression(@"^[A-Za-z0-9\s-]{0,255}$", ErrorMessage = ValidationMessages.ValidationFailed)]
        public string displayName { get; set; }
        [Required(ErrorMessage = ValidationMessages.ValidationFailed)]
        public string message { get; set; }
        public string status { get; set; }
        public List<RequestFile> files { get; set; }
    }
}
