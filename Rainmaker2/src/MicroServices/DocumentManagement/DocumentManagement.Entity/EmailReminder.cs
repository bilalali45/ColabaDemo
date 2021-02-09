using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagement.Entity
{
    public class EmailReminder
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public int tenantId { get; set; }
        public int noOfDays { get; set; }
        public DateTime recurringTime { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public int createdBy { get; set; }
        public DateTime createdOn { get; set; }
        public int? modifiedBy { get; set; }
        public DateTime? modifiedOn { get; set; }
        public Email email { get; set; }
    }
}
