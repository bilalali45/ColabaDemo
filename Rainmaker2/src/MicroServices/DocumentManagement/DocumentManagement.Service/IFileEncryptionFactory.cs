using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagement.Service
{
    public interface IFileEncryptionFactory
    {
        IFileEncryptor GetEncryptor(string name);
    }
}
