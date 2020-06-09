using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagement.Entity
{
    public class RequestFile
    {
        public string clientName { get; set; }
        public string serverName { get; set; }
        public string borrowerName { get; set; }
        public DateTime fileUploadedOn { get; set; }
        public int size { get; set; }
        public string encryptionKey { get; set; }
        public string encryptionAlgorithm { get; set; }
        public int order { get; set; }
    }
}
