using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System;
using System.Collections.Generic;
using System.IO;

namespace ByteWebConnector.API.Utility
{
    public   class Helper
    {
        public static List<byte[]> WrapImagesInPdf(List<byte[]> intput)
        {
            List<byte[]> pdfbyte = new List<byte[]>();
            foreach (var item in intput)
            {

                PdfDocument pdf = new PdfDocument();

                PdfPage pdfPage = pdf.AddPage();

                XGraphics graph = XGraphics.FromPdfPage(pdfPage);
             
                XImage image = XImage.FromStream(()=> new MemoryStream(item));


                pdfPage.Width = image.PixelWidth + 100;
                pdfPage.Height = image.PixelHeight + 100;
                graph.DrawImage(image, 170, 100);

                string pdfFilename1 = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".pdf");
                string pdfFilename = $"\\tempfiles\\{Guid.NewGuid().ToString()}.pdf";

                pdf.Save(pdfFilename1);
                pdfbyte.Add(File.ReadAllBytes(pdfFilename1));
            }
            return pdfbyte;
        }
    }
}
