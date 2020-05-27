using System;
using System.Collections.Generic;
using System.Text;

namespace KeyStore.Service
{
    public interface IKeyStore
    {
        string Get(string key);
    }
}
