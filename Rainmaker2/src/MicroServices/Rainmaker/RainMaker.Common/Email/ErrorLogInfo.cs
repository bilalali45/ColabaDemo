using System;
using System.Collections.Generic;

namespace RainMaker.Common.Email
{
    public class ErrorLogInfo
    {
        public List<Exception> ErrorExceptions { get; set; }
        public string MemoryKeysDetail { get; set; }
    }
}
