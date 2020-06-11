using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
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
    public class FileSubmitModel
    {
        public List<IFormFile> documents { get; set; }
        public List<FileNameModel> fileNames { get; set; }
        public string id { get; set; }
        public string docId { get; set; }
        public string requestId { get; set; }
    }

    public class DoneModel
    {
        public string id { get; set; }
        public string docId { get; set; }
        public string requestId { get; set; }
     
    }

    public class FileViewModel
    {
        public string id { get; set; }
        public string docId { get; set; }
        public string requestId { get; set; }
        public string fileId { get; set; }
    }
    public class FileRenameModel
    {
        public string id { get; set; }
        public string docId { get; set; }
        public string requestId { get; set; }
        public string fileId { get; set; }
        public string fileName { get; set; }
    }

    public class FileOrderModel
    {
        public string id { get; set; }
        public string docId { get; set; }
        public string requestId { get; set; }
        public List<FileNameModel> files { get; set; }

    }

   
}
