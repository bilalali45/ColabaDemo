using DocumentManagement.Entity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
namespace DocumentManagement.Model
{


    public class DocumentDetailQuery
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string docId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string typeId { get; set; }
        public string docName { get; set; }
        public string typeName { get; set; }
        public List<RequestFile> files { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string requestId;
        public string userName { get; set; }
    }


  
    public class DocumendDTO
    {
       

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string docId { get; set; }
        public string docName { get; set; }
        public string typeId { get; set; }
        public string requestId { get; set; }
        public  List<DocumentFileDTO> files { get; set; }
        public string userName { get; set; }

    }

    public class DocumentFileDTO
    {
        public string fileId { get; set; }
        public string clientName { get; set; }
        public string mcuName { get; set; }
        public DateTime fileUploadedOn { get; set; }

    }
    public class TemplateIdModel
    {
        public List<string> id { get; set; }
        public int tenantId { get; set; }
    }
    public class DocumentModel
    {
        public string docId { get; set; }
        public string docName { get; set; }
        public string docMessage { get; set; }
    }

    public class DocumentQuery
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string docId { get; set; }
        public string typeName { get; set; }
        public string docMessage { get; set; }
        public List<Message> messages { get; set; }
        public string docName { get; set; }
    }

    public class AcceptDocumentModel
    {
        public string id { get; set; }
        public string requestId { get; set; }
        public string docId { get; set; }
    }

    public class RejectDocumentModel
    {
        public string id { get; set; }
        public string requestId { get; set; }
        public string docId { get; set; }
        public string message { get; set; }
    }
    public class DeleteDocumentModel
    {
        public string id { get; set; }
        public int tenantId { get; set; }
        public string documentId { get; set; } 
    }
    
}


 
   
 