using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagement.Service
{
    public class FileEncryptionFactory : IFileEncryptionFactory
    {
        public IFileEncryptor GetEncryptor(string name)
        {
            switch(name)
            {
                case "AES":
                    return new AESFileEncryptor();
            }
            throw new DocumentManagementException($"{name} File encryptor not found");
        }
    }
}
