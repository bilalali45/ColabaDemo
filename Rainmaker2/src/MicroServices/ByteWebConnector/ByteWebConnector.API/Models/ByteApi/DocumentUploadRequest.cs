using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ByteWebConnector.API.Models.ByteApi
{
    public class DocumentUploadRequest
    {

        public long FileDataId { get; set; }
        public string DocumentName { get; set; }
        public string DocumentType { get; set; }
        public string DocumentCategory { get; set; }
        public string DocumentStatus { get; set; }
        public string DocumentExension { get; set; }
        public string DocumentData { get; set; }
    }

    public class DocumentUploadResponse
    {

       
    }
}
