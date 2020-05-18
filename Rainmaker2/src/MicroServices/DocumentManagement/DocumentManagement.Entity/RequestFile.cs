using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagement.Entity
{
    public class RequestFile
    {
        public string clientName { get; set; }
        public string serverName { get; set; }
        public DateTime fileUploadedOn { get; set; }
    }
}
