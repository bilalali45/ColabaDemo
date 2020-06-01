using System;
using System.IO;
using System.Threading.Tasks;

namespace RainMaker.Common.Util
{
   public class FileHelpers
   {
       public enum FileSize
       {
           Bytes=0,
           Kb = 1,
           Mb = 2,
           Gb = 3,
       }
       //public static String[] sizeArray = new String[] { "Byes", "KB", "MB", "GB" };
       public static bool IsFileLocked(string filepath)
       {
           FileStream stream = null;

           try
           {
               var file = new FileInfo(filepath);
               stream =file .Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
           }
           catch (IOException)
           {
               //the file is unavailable because it is:
               //still being written to
               //or being processed by another thread
               //or does not exist (has already been processed)
               return true;
           }
           finally
           {
               if (stream != null)
                   stream.Close();
           }

           //file is not locked
           return false;
       }

       public static long GetFileSize(string path)
       {
           var fi = new FileInfo(path);

           return fi.Length;
       }

       public static String Get_Size_in_KB_MB_GB(ulong sizebytes, FileSize index)
       {
           if (sizebytes < 1000) return sizebytes + index.ToString();

           return Get_Size_in_KB_MB_GB(sizebytes / 1024, ++index);
       }

       public static async Task<string> CreateTmpFileAsync(string text)
       {
           string filePath = CreateEmptyTmpFile();
           await AppendTextToFileAsync(filePath, text);

           return filePath;
       }
       public static string CreateEmptyTmpFile()
       {
           // Get the full name of the newly created Temporary file. 
               // Note that the GetTempFileName() method actually creates
               // a 0-byte file and returns the name of the created file.
               string fileName = Path.GetTempFileName();

               // Create a FileInfo object to set the file's attributes
               var fileInfo = new FileInfo(fileName);

               // Set the Attribute property of this file to Temporary. 
               // Although this is not completely necessary, the .NET Framework is able 
               // to optimize the use of Temporary files by keeping them cached in memory.
               fileInfo.Attributes = FileAttributes.Temporary;

           return fileName;
       }

       public static string GetTempFileName(string extension)
       {
           string fileName;
           int attempt = 0;
           bool exit = false;
           do
           {
               fileName = Path.GetRandomFileName();
               fileName = Path.ChangeExtension(fileName, extension);
               fileName = Path.Combine(Path.GetTempPath(), fileName);

               try
               {
                   using (new FileStream(fileName, FileMode.CreateNew)) { }

                   exit = true;
               }
               catch (IOException ex)
               {
                   if (++attempt == 100)
                       throw new IOException("No unique temporary file name is available.", ex);
               }

           } while (!exit);

           return fileName;
       }

       public static async Task AppendTextToFileAsync(string filePath, string text)
       {
           await File.AppendAllTextAsync(filePath, text);
           
               //// Write to the temp file.
               //StreamWriter streamWriter = File.AppendText(filePath);
               //streamWriter.Write(text);
               //streamWriter.Flush();
               //streamWriter.Close();


           
       }

       public static async Task UpdateTextFileAsync(string filePath, string text)
       {
         
           
           await File.WriteAllTextAsync(filePath,text);

           //// Write to the temp file.
           //StreamWriter streamWriter = File.AppendText(filePath);
           //streamWriter.Write(text);
           //streamWriter.Flush();
           //streamWriter.Close();



       }
       public static void DeleteFile(string filePath)
       {
               // Delete the temp file (if it exists)
           if (File.Exists(filePath))
               {
                   File.Delete(filePath);
                  
               }
         
       }

       public static async Task<string> ReadFileTextAsync(string filePath)
       {
           return await File.ReadAllTextAsync(filePath);
       }
   }
}
