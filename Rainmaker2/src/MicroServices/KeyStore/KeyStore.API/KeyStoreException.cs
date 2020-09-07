using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeyStore.API
{
    public class KeyStoreException : Exception
    {
        public KeyStoreException(string message) : base(message)
        { }
    }
}
