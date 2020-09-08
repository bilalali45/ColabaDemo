using System;

namespace DocumentManagement.Service
{
    public class DocumentManagementException : Exception
    {
        public DocumentManagementException(string message) : base(message)
        { }
    }
}
