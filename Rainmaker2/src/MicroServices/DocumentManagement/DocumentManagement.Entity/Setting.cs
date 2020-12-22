using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DocumentManagement.Entity
{
    public class Setting
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string ftpServer { get; set; }
        public string ftpUser { get; set; }
        public string ftpPassword { get; set; }
        public int maxFileSize { get; set; }
        public int maxFileNameSize { get; set; }
        public int maxMcuFileSize { get; set; }
        public string[] allowedExtensions { get; set; }
    }
}
