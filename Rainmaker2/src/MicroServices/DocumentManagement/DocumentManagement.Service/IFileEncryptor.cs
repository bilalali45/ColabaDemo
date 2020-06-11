using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DocumentManagement.Service
{
    public interface IFileEncryptor
    {
        string EncryptFile(Stream inputFile, string password);
        Stream DecrypeFile(string inputFile, string password, string originalFileName);
    }
}
