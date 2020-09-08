using System;

namespace KeyStore.API
{
    public class KeyStoreException : Exception
    {
        public KeyStoreException(string message) : base(message)
        { }
    }
}
