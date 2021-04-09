using System.IO;

namespace DocumentManagement.Service
{
    public interface IFileEncryptor
    {
        (MemoryStream,string) EncryptFile(Stream inputFile, string password);
        Stream DecrypeFile(MemoryStream fsCrypt, string password, string originalFileName, string salt);
    }
}
