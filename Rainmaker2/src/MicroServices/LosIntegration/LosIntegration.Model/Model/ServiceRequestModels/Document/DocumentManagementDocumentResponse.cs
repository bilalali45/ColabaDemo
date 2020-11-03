using System;
using System.Collections.Generic;

namespace LosIntegration.Model.Model.ServiceRequestModels.Document
{
    public class File
    {
        public string Id { get; set; }
        public string ClientName { get; set; }
        public DateTime FileUploadedOn { get; set; }
        public string McuName { get; set; }
        public string ByteProStatus { get; set; }
        public object Status { get; set; }
    }

    public class DocumentManagementDocument
    {
        public string Id { get; set; }
        public string RequestId { get; set; }
        public string DocId { get; set; }
        public string DocName { get; set; }
        public string Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public List<File> Files { get; set; }
        public string TypeId { get; set; }
        public string UserName { get; set; }
    }
}
