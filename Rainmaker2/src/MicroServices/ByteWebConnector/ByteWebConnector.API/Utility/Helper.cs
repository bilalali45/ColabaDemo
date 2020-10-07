using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;

namespace ByteWebConnector.API.Utility
{
    public static class Helper
    {
        private const int exifOrientationID = 0x112; //274


        public static List<byte[]> WrapImagesInPdf(List<byte[]> input)
        {
            var pdfbyte = new List<byte[]>();
            foreach (var imageBytes in input)
            {
                var orientationAdjustedImageStream = AdjustAccordingToOrientation(imageBytes: imageBytes);

                var pdf = new PdfDocument();

                var pdfPage = pdf.AddPage();

                var graph = XGraphics.FromPdfPage(page: pdfPage);

                var image = XImage.FromStream(stream: () => orientationAdjustedImageStream);

                pdfPage.Width = image.PointWidth;
                pdfPage.Height = image.PointHeight;

                graph.DrawImage(image: image,
                                x: 0,
                                y: 0);

                var pdfFilename1 = Path.Combine(path1: Path.GetTempPath(),
                                                path2: Guid.NewGuid() + ".pdf");

                pdf.Save(path: pdfFilename1);
                pdfbyte.Add(item: File.ReadAllBytes(path: pdfFilename1));
            }

            return pdfbyte;
        }


        public static MemoryStream AdjustAccordingToOrientation(byte[] imageBytes)
        {
            Stream imageStream = new MemoryStream(buffer: imageBytes);

            var inputImage = Image.FromStream(stream: imageStream);

            ExifRotate(img: inputImage);

            var orientationAdjustedImageStream = new MemoryStream();
            inputImage.Save(stream: orientationAdjustedImageStream,
                            format: ImageFormat.Png);

            // If you're going to read from the stream, you may need to reset the position to the start
            orientationAdjustedImageStream.Position = 0;
            return orientationAdjustedImageStream;
        }


        public static void ExifRotate(this Image img)
        {
            if (!img.PropertyIdList.Contains(value: exifOrientationID))
                return;

            var prop = img.GetPropertyItem(propid: exifOrientationID);
            int val = BitConverter.ToUInt16(value: prop.Value,
                                            startIndex: 0);
            var rot = RotateFlipType.RotateNoneFlipNone;

            if (val == 3 || val == 4)
                rot = RotateFlipType.Rotate180FlipNone;
            else if (val == 5 || val == 6)
                rot = RotateFlipType.Rotate90FlipNone;
            else if (val == 7 || val == 8)
                rot = RotateFlipType.Rotate270FlipNone;

            if (val == 2 || val == 4 || val == 5 || val == 7)
                rot |= RotateFlipType.RotateNoneFlipX;

            if (rot != RotateFlipType.RotateNoneFlipNone)
                img.RotateFlip(rotateFlipType: rot);
        }
    }
}