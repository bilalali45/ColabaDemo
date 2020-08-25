using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Extensions.ExtensionClasses
{
    public static class ByteExtensions
    {
      

        public static T FromByteArray<T>(this byte[] data)
        {
            if (data == null)
                return default(T);
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(data))
            {
                object obj = bf.Deserialize(ms);
                return (T)obj;
            }
        }
    }
}

