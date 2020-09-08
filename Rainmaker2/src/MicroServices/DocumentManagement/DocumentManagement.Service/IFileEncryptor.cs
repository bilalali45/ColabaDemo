using System.IO;

namespace DocumentManagement.Service
{
    public interface IFileEncryptor
    {
        string EncryptFile(Stream inputFile, string password);
        Stream DecrypeFile(string inputFile, string password, string originalFileName);
    }
}
