using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DocumentManagement.Entity
{
    public class EmailTemplate
    {
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
        public bool isDeleted { get; set; }
        public int createdBy { get; set; }
        public DateTime createdOn { get; set; }
        public int? modifiedBy { get; set; }
        public DateTime? modifiedOn { get; set; }

    }
}
