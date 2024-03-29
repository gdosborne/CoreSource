using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Media;
using System.Windows.Media.Imaging;
//using Id3;
using Color = System.Windows.Media.Color;
using Size = System.Windows.Size;

namespace GregOsborne.Application.Media {
    public static class Extensions {
        private static bool ThumbnailCallback() {
            return false;
        }

        public static Bitmap BitmapImage2Bitmap(this BitmapImage bitmapImage) {
            using (var outStream = new MemoryStream()) {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);
                var bitmap = new Bitmap(outStream);
                return new Bitmap(bitmap);
            }
        }

        public static ImageSource GetImageSource(this Image image) {
            BitmapImage result;
            using (var memory = new MemoryStream()) {
                image.Save(memory, ImageFormat.Png);
                memory.Position = 0;
                result = new BitmapImage();
                result.BeginInit();
                memory.Seek(0, SeekOrigin.Begin);
                result.StreamSource = memory;
                result.CacheOption = BitmapCacheOption.OnLoad;
                result.EndInit();
            }
            return result;
        }

        public static BitmapImage GetBitmapImage(this string fileName) {
            BitmapImage bitmapImage;
            var extension = Path.GetExtension(fileName);
            if (extension != null && extension.Equals(".ico", StringComparison.OrdinalIgnoreCase)) {
                using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read)) {
                    bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = fs;
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.EndInit();
                }
                bitmapImage.DecodePixelWidth = bitmapImage.PixelWidth;
                bitmapImage.DecodePixelHeight = bitmapImage.PixelHeight;
            }
            else {
                bitmapImage = new BitmapImage(new Uri(fileName, UriKind.Relative));
            }
            return bitmapImage;
        }

        public static ImageSource GetImageSourceAbsolute(this string fileName) {
            var oUri = new Uri(fileName, UriKind.Absolute);
            var result = BitmapFrame.Create(oUri);
            return result;
        }

        public static ImageSource GetImageSourceByName(this Assembly assy, string resourceName) {
            var psAssemblyName = assy.GetName().Name;
            var oUri = new Uri("pack://application:,,,/" + psAssemblyName + ";component/" + resourceName, UriKind.RelativeOrAbsolute);
            return BitmapFrame.Create(oUri);
        }

        public static ImageSource GetImageSourceByName(this Assembly assy, string resourceName, double size) {
            var temp = (BitmapSource) assy.GetImageSourceByName(resourceName);
            var scale = size / temp.PixelWidth;
            var transform = new ScaleTransform(scale, scale);
            var result = new TransformedBitmap(temp, transform);
            return result;
        }

        public static void GetThumbnailImage(this string fileName, int width, int height, string outputFileName, ImageFormat format, bool overwrite = false) {
            if (!File.Exists(fileName))
                throw new FileNotFoundException("Cannot find image file", fileName);
            if (File.Exists(outputFileName) && !overwrite)
                throw new ApplicationException("Output file already exists");
            if (outputFileName.Equals(fileName, StringComparison.OrdinalIgnoreCase))
                return;
            Bitmap bmp;
            if (Path.GetExtension(outputFileName).Equals(".ico", StringComparison.OrdinalIgnoreCase)) {
                bmp = GetBitmapImage(fileName).BitmapImage2Bitmap();
                bmp.MakeTransparent();
                outputFileName = outputFileName.Replace("ico", ".png");
            }
            else {
                bmp = new Bitmap(fileName);
            }
            Image.GetThumbnailImageAbort myCallback = ThumbnailCallback;
            var output = bmp.GetThumbnailImage(width, height, myCallback, IntPtr.Zero);
            output.Save(outputFileName, format);
            bmp.Dispose();
        }

        public static Size ImageDimensions(this string fileName) {
            BitmapImage bitmapImage;
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read)) {
                bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = fs;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
            }
            var result = new Size(bitmapImage.PixelWidth, bitmapImage.PixelHeight);
            return result;
        }

        public static Color ReduceAlpha(this Color original, byte alpha) {
            return Color.FromArgb(alpha, original.R, original.G, original.B);
        }

        public static Color ToColor(this string value) {
            var result = Colors.Transparent;
            if (!value.StartsWith("#")) return result;
            byte a = 255;
            byte r;
            byte g;
            byte b;
            // ReSharper disable once ConvertIfStatementToSwitchStatement
            if (value.Length == 9) {
                a = (byte) Convert.ToUInt32(value.Substring(1, 2), 16);
                r = (byte) Convert.ToUInt32(value.Substring(3, 2), 16);
                g = (byte) Convert.ToUInt32(value.Substring(5, 2), 16);
                b = (byte) Convert.ToUInt32(value.Substring(7, 2), 16);
                result = Color.FromArgb(a, r, g, b);
            }
            else if (value.Length == 7) {
                r = (byte) Convert.ToUInt32(value.Substring(1, 2), 16);
                g = (byte) Convert.ToUInt32(value.Substring(3, 2), 16);
                b = (byte) Convert.ToUInt32(value.Substring(5, 2), 16);
                result = Color.FromArgb(a, r, g, b);
            }
            return result;
        }

        public static string ToHexValue(this Color value) {
            return "#" + value.A.ToString("X2") + value.R.ToString("X2") + value.G.ToString("X2") + value.B.ToString("X2");
        }

        //public static string GetMusicTitle(string mp3FileName) {
        //    var result = string.Empty;
        //    using (var s = new Mp3Stream(new FileStream(mp3FileName, FileMode.Open, FileAccess.Read, FileShare.None))) {
        //        var tags = s.GetAllTags();
        //        // ReSharper disable once AssignNullToNotNullAttribute
        //        var enumerable = tags ?? tags.ToArray();
        //        if (!enumerable.Any()) return result;
        //        while (string.IsNullOrEmpty(result)) result = enumerable.First().Title;
        //    }
        //    return result;
        //}
    }
}