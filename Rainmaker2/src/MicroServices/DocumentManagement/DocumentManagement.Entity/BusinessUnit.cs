using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DocumentManagement.Entity
{
    public class BusinessUnit
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public int tenantId { get; set; }
        public int businessUnitId { get; set; }
        public string footerText { get; set; }
    }
}
