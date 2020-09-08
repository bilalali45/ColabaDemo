namespace DocumentManagement.Service
{
    public class FileEncryptionFactory : IFileEncryptionFactory
    {
        public IFileEncryptor GetEncryptor(string name)
        {
            switch(name)
            {
                case "AES":
                    return new AesFileEncryptor();
            }
            throw new DocumentManagementException($"{name} File encryptor not found");
        }
    }
}
