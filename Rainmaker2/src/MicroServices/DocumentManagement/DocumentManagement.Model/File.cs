using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagement.Model
{
    public class FileNameModel
    {
        public string fileName { get; set; }
        public int order { get; set; }
    }
    public class DoneModel
    {
        public string id { get; set; }
        public string docId { get; set; }
        public string requestId { get; set; }
        public int tenantId { get; set; }

    }

    public class FileViewModel
    {
        public string id { get; set; }
        public string docId { get; set; }
        public string requestId { get; set; }
        public string fileId { get; set; }
        public int tenantId { get; set; }
    }
    public class FileRenameModel
    {
        public string id { get; set; }
        public string docId { get; set; }
        public string requestId { get; set; }
        public string fileId { get; set; }
        public string fileName { get; set; }
        public int tenantId { get; set; }
    }

    public class FileOrderModel
    {
        public string id { get; set; }
        public string docId { get; set; }
        public string requestId { get; set; }
        public List<FileNameModel> files { get; set; }
        public int tenantId { get; set; }
    }

   
}
