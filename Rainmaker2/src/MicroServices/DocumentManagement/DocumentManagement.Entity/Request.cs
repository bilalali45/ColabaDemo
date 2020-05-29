using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagement.Entity
{
    public class Request
    {
        public int employeeId { get; set; }
        public DateTime createdOn { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        public List<RequestDocument> documents { get; set; }
    }
}
