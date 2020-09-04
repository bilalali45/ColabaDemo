using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagement.Service
{
    public class DocumentManagementException : Exception
    {
        public DocumentManagementException(string message) : base(message)
        { }
    }
}
