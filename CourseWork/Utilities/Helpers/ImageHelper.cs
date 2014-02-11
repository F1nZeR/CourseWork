using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using GMap.NET;
using Color = System.Drawing.Color;

namespace CourseWork.Utilities.Helpers
{
    public class ImageHelper
    {
        public static Stream ResizeTo(Bitmap img, int width, int height)
        {
            var brush = new SolidBrush(Color.White);
            float scale = Math.Min((float) width/img.Width, (float) height/img.Height);

            var bmp = new Bitmap(width, height);
            var graph = Graphics.FromImage(bmp);

            graph.InterpolationMode = InterpolationMode.High;
            graph.CompositingQuality = CompositingQuality.HighQuality;
            graph.SmoothingMode = SmoothingMode.AntiAlias;

            var scaleWidth = (int) (img.Width*scale);
            var scaleHeight = (int) (img.Height*scale);

            graph.FillRectangle(brush, new RectangleF(0, 0, width, height));
            graph.DrawImage(img,
                new Rectangle((width - scaleWidth)/2, (height - scaleHeight)/2, scaleWidth, scaleHeight));

            var resultStream = new MemoryStream();
            bmp.Save(resultStream, img.RawFormat);
            return resultStream;
        }

        public static Stream TakePartFromOriginal(Bitmap img, GPoint pos)
        {
            const int size = 512;
            var brush = new SolidBrush(Color.White);
            var resizedImg = new Bitmap(ResizeTo(img, size, size));
            var bmp = new Bitmap(256, 256);
            var graph = Graphics.FromImage(bmp);
            graph.FillRectangle(brush, new RectangleF(0, 0, size, size));
            var offsetX = (int) pos.X*256;
            var offsetY = (int) pos.Y*256;
            for (int x = 0; x < 256; x++)
            {
                for (int y = 0; y < 256; y++)
                {
                    var curX = x + offsetX;
                    var curY = y + offsetY;
                    bmp.SetPixel(x, y, resizedImg.GetPixel(curX, curY));
                }
            }
            var resultStream = new MemoryStream();
            bmp.Save(resultStream, img.RawFormat);
            return resultStream;
        }
    }
}
