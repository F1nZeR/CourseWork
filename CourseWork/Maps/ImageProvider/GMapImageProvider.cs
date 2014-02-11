using System;
using System.Drawing;
using System.IO;
using CourseWork.Utilities.Helpers;
using GMap.NET;
using GMap.NET.MapProviders;

namespace CourseWork.Maps.ImageProvider
{
    internal class GMapImageProvider : GMapProvider
    {
        private readonly string _originalImage;

        private GMapProvider[] _overlays;

        public override GMapProvider[] Overlays
        {
            get { return _overlays ?? (_overlays = new GMapProvider[] {this}); }
        }

        public GMapImageProvider(string originalImage)
        {
            _originalImage = originalImage;
        }

        private Stream GetOriginalImageStream()
        {
            MemoryStream newStream;
            using (var stream = File.Open(_originalImage, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                newStream = CopyStream(stream, true);
            }
            return newStream;
        }

        private Stream GetImgStreamForZoomPos(GPoint pos, int zoom)
        {
            if (zoom == 0)
            {
                var origStream = GetOriginalImageStream();
                var bmp = new Bitmap(origStream);
                var resizedImg = ImageHelper.ResizeTo(bmp, 256, 256);
                return resizedImg;
            }

            if (zoom == 1)
            {
                var origStream = GetOriginalImageStream();
                var bmp = new Bitmap(origStream);
                var resizedImg = ImageHelper.TakePartFromOriginal(bmp, pos);
                return resizedImg;
            }

            return null;
        }

        public override PureImage GetTileImage(GPoint pos, int zoom)
        {
            Console.Out.WriteLine("zoom = {0}, pos = {1}", zoom, pos);
            var imgStream = GetImgStreamForZoomPos(pos, zoom);
            if (imgStream == null) return null;

            var newStream = CopyStream(imgStream, true);
            var ret = TileImageProxy.FromStream(imgStream);
            ret.Data = newStream;
            ret.Data.Position = 0;
            newStream.Dispose();
            return ret;
        }
        
        private MemoryStream CopyStream(Stream inputStream, bool seekOriginBegin)
        {
            const int readSize = 32 * 1024;
            var buffer = new byte[readSize];
            var ms = new MemoryStream();
            {
                int count = 0;
                while ((count = inputStream.Read(buffer, 0, readSize)) > 0)
                {
                    ms.Write(buffer, 0, count);
                }
            }
            if (seekOriginBegin)
            {
                inputStream.Seek(0, SeekOrigin.Begin);
            }
            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }               
   
        public override Guid Id
        {
            get { return Guid.NewGuid(); }
        }

        public override string Name
        {
            get { return "IMG"; }
        }

        public override PureProjection Projection
        {
            get { return GMap.NET.Projections.MercatorProjection.Instance; }
        }
    }
}
