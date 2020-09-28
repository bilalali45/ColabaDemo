using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System;
using System.Collections.Generic;
using System.IO;

namespace ByteWebConnector.API.Utility
{
    public static class Helper
    {
        public static List<byte[]> WrapImagesInPdf(List<byte[]> intput)
        {
            List<byte[]> pdfbyte = new List<byte[]>();
            foreach (var item in intput)
            {
                PdfDocument pdf = new PdfDocument();

                PdfPage pdfPage = pdf.AddPage();

                XGraphics graph = XGraphics.FromPdfPage(pdfPage);

                XImage image = XImage.FromStream(() => new MemoryStream(item));

                pdfPage.Width = image.PointWidth;
                pdfPage.Height = image.PointHeight;

                graph.DrawImage(image, 0, 0);

                string pdfFilename1 = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".pdf");

                pdf.Save(pdfFilename1);
                pdfbyte.Add(File.ReadAllBytes(pdfFilename1));
            }
            return pdfbyte;
        }
    }
}
