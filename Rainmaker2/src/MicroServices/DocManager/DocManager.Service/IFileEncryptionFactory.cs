namespace DocManager.Service
{
    public interface IFileEncryptionFactory
    {
        IFileEncryptor GetEncryptor(string name);
    }
}
