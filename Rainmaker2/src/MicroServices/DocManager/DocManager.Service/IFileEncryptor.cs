using System.IO;

namespace DocManager.Service
{
    public interface IFileEncryptor
    {
        string EncryptFile(Stream inputFile, string password);
        Stream DecrypeFile(string inputFile, string password, string originalFileName);
    }
}
