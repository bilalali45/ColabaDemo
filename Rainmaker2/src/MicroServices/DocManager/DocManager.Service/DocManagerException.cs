using System;

namespace DocManager.Service
{
    public class DocManagerException : Exception
    {
        public DocManagerException(string message) : base(message)
        { }
    }
}
