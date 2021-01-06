using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ByteWebConnector.SDK
{
    public interface ITextFileWriter
    {
        string CreateFile(string rootDir,
                   string fileName, string fileContent);


        string CreateFile(string filePath, string fileContent);
    }
    public class TextFileWriter : ITextFileWriter
    {
        public string CreateFile(string rootDir,
                                 string fileNameWithExtension, string fileContent)
        {
            return this.CreateFile($"{rootDir}\\{fileNameWithExtension}", fileContent);
        }


        public string CreateFile(string filePath, string fileContent)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.Write(fileContent);
                writer.Flush();
            }

            return filePath;
        }
    }
}
