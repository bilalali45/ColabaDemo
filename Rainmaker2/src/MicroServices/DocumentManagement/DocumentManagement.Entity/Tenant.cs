using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DocumentManagement.Entity
{
    public class Tenant
    {
      [BsonId]
      [BsonRepresentation(BsonType.ObjectId)]
      public string id { get; set; }
      public int tenantId { get; set; }
      public string emailTemplate { get; set; }
      public int syncToBytePro { get; set; }
      public int autoSyncToBytePro { get; set; }
    }
}
