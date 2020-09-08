namespace DocumentManagement.Service
{
    public interface IFileEncryptionFactory
    {
        IFileEncryptor GetEncryptor(string name);
    }
}
