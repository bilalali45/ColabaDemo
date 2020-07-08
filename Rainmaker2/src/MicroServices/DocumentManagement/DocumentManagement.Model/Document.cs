using DocumentManagement.Entity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace DocumentManagement.Model
{


    public class DocumentQuery
    {
        

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string docId { get; set; }
        public string docName { get; set; }
        public string typeName { get; set; }
        public List<RequestFile> files { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string requestId;
    }


  
    public class DocumendDTO
    {
       

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string docId { get; set; }
        public string docName { get; set; }
       
        public string requestId { get; set; }
        public  List<DocumentFileDTO> files { get; set; }
       
    }

    public class DocumentFileDTO
    {
        public string fileId { get; set; }
        public string clientName { get; set; }
        public string mcuName { get; set; }
        public DateTime fileUploadedOn { get; set; }

    }
}
