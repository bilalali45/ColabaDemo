using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagement.Entity
{
   public  class Tenant
    {
      [BsonId]
      [BsonRepresentation(BsonType.ObjectId)]
      public string id { get; set; }
      public int tenantId { get; set; }
      public string emailTemplate { get; set; }   
    }
}
