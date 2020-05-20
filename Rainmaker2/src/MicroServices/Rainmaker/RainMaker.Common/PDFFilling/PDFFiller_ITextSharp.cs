//using System;
//using System.Collections.Generic;
//using System.IO;
//using iTextSharp.text.pdf;

//namespace RainMaker.Common.PDFFilling
//{
// public  class PdfFillerITextSharp
//    {

//     public static void FillForm(string filePath ,Dictionary<string, string> fieldValues)
//     {
        
//         string newFile = Path.GetTempPath() + Guid.NewGuid() + Path.GetExtension(filePath);
//         FillForm(filePath, newFile, fieldValues);
//         File.Delete(filePath);//delete old file
//         File.Move(newFile, filePath); //rename new file to old file 

//     }


//     public static void FillForm(string srcFilePath, string trgPath, Dictionary<string, string> fieldValues)
//     {
      
//        var pdfReader = new PdfReader(srcFilePath);
//         var pdfStamper = new PdfStamper(pdfReader, new FileStream(
//                     trgPath, FileMode.Create));

//         var pdfFormFields = pdfStamper.AcroFields;

//         pdfStamper.SetEncryption(
//             null,
//             System.Text.Encoding.UTF8.GetBytes(GenerateRandomPassword(15)),
//             PdfWriter.ALLOW_PRINTING,
//             PdfWriter.ENCRYPTION_AES_128
//             );

//         foreach (var fieldValue in fieldValues)
//         {
//             pdfFormFields.SetField(fieldValue.Key, fieldValue.Value);
//         }
//         // flatten the form to remove editing options, set it to false
//         // to leave the form open to subsequent manual edits
//         pdfStamper.FormFlattening = false;
        
         
//         // close the pdf
//         pdfStamper.Close();
//         pdfReader.Dispose();
//         pdfStamper.Dispose();
//     }

//     private static string GenerateRandomPassword(int length)
//     {
//         const string allowedLetterChars = "abcdefghijkmnpqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ!@$?_-{}()<>[]^";
//         const string allowedNumberChars = "0123456789";
//         var chars = new char[length];
//         var rd = new Random();

//         var useLetter = true;
//         for (var i = 0; i < length; i++)
//         {
//             if (useLetter)
//             {
//                 chars[i] = allowedLetterChars[rd.Next(0, allowedLetterChars.Length)];
//                 useLetter = false;
//             }
//             else
//             {
//                 chars[i] = allowedNumberChars[rd.Next(0, allowedNumberChars.Length)];
//                 useLetter = true;
//             }

//         }

//         return new string(chars);
//     }

//    }
//}
