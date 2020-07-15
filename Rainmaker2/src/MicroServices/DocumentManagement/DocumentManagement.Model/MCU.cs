using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagement.Model
{
    public class mcuRenameModel
    {
        public string id { get; set; }
        public string requestId { get; set; }
        public string docId { get; set; }
        public string fileId { get; set; }
        public string newName { get; set; }
    }
}
