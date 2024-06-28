/* File="Extensions"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using OzFramework.Primitives;
using OzFramework.Text;
using Common.MVVMFramework;

using Universal.Common;

using Color = System.Windows.Media.Color;
using FontFamily = System.Windows.Media.FontFamily;
using Size = System.Windows.Size;

namespace OzFramework.Media {
    public static class Extensions {
        public static Icon To16BitIcon(this ImageSource src) {
            var img = src.ImageSourceToGDIImage();
            var iconHandle = (img as Bitmap).GetHicon();
            var icon = Icon.FromHandle(iconHandle);
            return icon;
        }

        public static Icon To32BitIcon(this ImageSource imageSource) {
            var tmpFileName = Path.GetTempFileName() + ".png";
            var icoFileName = Path.GetTempFileName() + ".ico";
            var p = new PngBitmapEncoder();
            p.Frames.Add(BitmapFrame.Create((BitmapSource)imageSource));
            using (var fs = new FileStream(tmpFileName, FileMode.Create, FileAccess.Write)) {
                p.Save(fs);
            }
            //using (var stream = File.OpenWrite(icoFileName)) {
            using (var bitmap = (Bitmap)Image.FromFile(tmpFileName)) {
                return Icon.FromHandle(bitmap.GetHicon());
            }
            //}
        }

        public static Bitmap ConvertToBitmap(this BitmapSource bitmapSource) {
            var width = bitmapSource.PixelWidth;
            var height = bitmapSource.PixelHeight;
            var stride = width * ((bitmapSource.Format.BitsPerPixel + 7) / 8);
            var memoryBlockPointer = Marshal.AllocHGlobal(height * stride);
            bitmapSource.CopyPixels(new Int32Rect(0, 0, width, height), memoryBlockPointer, height * stride, stride);
            var bitmap = new Bitmap(width, height, stride, System.Drawing.Imaging.PixelFormat.Format32bppPArgb, memoryBlockPointer);
            return bitmap;
        }

        private static Image ImageSourceToGDIImage(this ImageSource image) {
            using (var ms = new MemoryStream()) {
                var encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image.As<BitmapSource>()));
                encoder.Save(ms);
                ms.Flush();
                return Image.FromStream(ms);
            }
        }

        public static Bitmap BitmapImageToBitmap(this BitmapImage bitmapImage) {
            using (var outStream = new MemoryStream()) {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);
                var bitmap = new Bitmap(outStream);
                return new Bitmap(bitmap);
            }
        }

        public static IEnumerable<char> GetCharacters(this FontFamily family) {
            var result = new List<char>();
            var typefaces = family.GetTypefaces();
            foreach (var typeface in typefaces) {
                var glyph = default(GlyphTypeface);
                typeface.TryGetGlyphTypeface(out glyph);
                glyph.CharacterToGlyphMap.ForEach(c => result.Add((char)c.Key));
            }
            return result;
        }

        public static IEnumerable<string> GetAllFontNames() {
            var results = new List<string>();
            results.AddRange(GetAllFontFamiles().Select(x => x.Source));
            return results;
        }

        public static IEnumerable<FontFamily> GetAllFontFamiles() {
            var temp = System.Drawing.FontFamily.Families;
            var result = new List<FontFamily>(temp.Select(x => new FontFamily(x.Name)));
            return result;
        }

        public static BitmapImage GetBitmapImage(this string fileName) {
            BitmapImage bitmapImage;
            var extension = Path.GetExtension(fileName);
            if (!extension.IsNull() && extension.EqualsIgnoreCase(".ico")) {
                using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read)) {
                    bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = fs;
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.EndInit();
                }
                bitmapImage.DecodePixelWidth = bitmapImage.PixelWidth;
                bitmapImage.DecodePixelHeight = bitmapImage.PixelHeight;
            } else {
                bitmapImage = new BitmapImage(new Uri(fileName, UriKind.Relative));
            }
            return bitmapImage;
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

        public static ImageSource GetImageSourceAbsolute(this string fileName) {
            var oUri = new Uri(fileName, UriKind.Absolute);
            var result = BitmapFrame.Create(oUri);
            return result;
        }

        public static ImageSource GetImageSourceAbsolute(this FileInfo file) {
            return file.FullName.GetImageSourceAbsolute();
        }

        public static ImageSource GetImageSourceByName(this Assembly assy, string resourceName) {
            //set the Build Action to Resource, not Embedded resource
            //always include the folder name and extension in the filename
            //if an image named "image1.png" is in a folder "resources" the resource name
            // is "/resources/image1.png"
            var psAssemblyName = assy.GetName().Name;
            var oUri = new Uri("pack://application:,,,/" + psAssemblyName + ";component/" + resourceName, UriKind.RelativeOrAbsolute);
            return BitmapFrame.Create(oUri);
        }

        public static ImageSource GetImageSourceByName(this Assembly assy, string resourceName, double size) {
            var temp = (BitmapSource)assy.GetImageSourceByName(resourceName);
            var scale = size / temp.PixelWidth;
            var transform = new ScaleTransform(scale, scale);
            var result = new TransformedBitmap(temp, transform);
            return result;
        }

        public static void GetThumbnailImage(this string fileName, int width, int height, string outputFileName, ImageFormat format, bool overwrite = false) {
            if (!File.Exists(fileName)) {
                throw new FileNotFoundException("Cannot find image file", fileName);
            }

            if (File.Exists(outputFileName) && !overwrite) {
                throw new ApplicationException("Output file already exists");
            }

            if (outputFileName.EqualsIgnoreCase(fileName)) {
                return;
            }

            Bitmap bmp;
            if (Path.GetExtension(outputFileName).EqualsIgnoreCase(".ico")) {
                bmp = GetBitmapImage(fileName).BitmapImageToBitmap();
                bmp.MakeTransparent();
                outputFileName = outputFileName.Replace("ico", ".png");
            } else {
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

        public static Color Invert(this Color value) {
            var argb = value.ToColor().ToArgb();
            var inverse = argb ^ 0xffffff;
            var c = System.Drawing.Color.FromArgb(inverse);
            return c.ToColor();
        }

        public static SolidColorBrush Invert(this SolidColorBrush value) => new(value.Color.Invert());
        public static SolidColorBrush Invert(this SolidColorBrush value, byte alpha) => new(value.Color.Invert().ReduceAlpha(alpha));

        public static Color ReduceAlpha(this Color original, byte alpha) => Color.FromArgb(alpha, original.R, original.G, original.B);

        public static SolidColorBrush ToMediaBrush(this System.Drawing.Color value) => new(Color.FromArgb(value.A, value.R, value.G, value.B));

        public static SolidColorBrush ToSolidBrush(this Color value) => new(value);

        public static System.Drawing.Color ToColor(this Color color) =>
            System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);

        public static Color ToColor(this System.Drawing.Color color) =>
            Color.FromArgb(color.A, color.R, color.G, color.B);

        public static Color ToColor(this string value) {
            var result = Colors.Transparent;
            if (!value.StartsWith("#")) {
                return result;
            }

            byte a = 255;
            byte r;
            byte g;
            byte b;
            // ReSharper disable once ConvertIfStatementToSwitchStatement
            if (value.Length == 9) {
                a = (byte)Convert.ToUInt32(value.Substring(1, 2), 16);
                r = (byte)Convert.ToUInt32(value.Substring(3, 2), 16);
                g = (byte)Convert.ToUInt32(value.Substring(5, 2), 16);
                b = (byte)Convert.ToUInt32(value.Substring(7, 2), 16);
                result = Color.FromArgb(a, r, g, b);
            } else if (value.Length == 7) {
                r = (byte)Convert.ToUInt32(value.Substring(1, 2), 16);
                g = (byte)Convert.ToUInt32(value.Substring(3, 2), 16);
                b = (byte)Convert.ToUInt32(value.Substring(5, 2), 16);
                result = Color.FromArgb(a, r, g, b);
            }
            return result;
        }

        public static string ToHexValue(this Color value) => value.ToString();

        private static bool ThumbnailCallback() => false;
        
        public delegate void CharInfoSelectedHandler(object sender, CharInfoSelectedEventArgs e);
        public class CharInfoSelectedEventArgs : EventArgs {
            public CharInfoSelectedEventArgs(CharInfo c) {
                CharInfo = c;
            }
            public CharInfo CharInfo { get; private set; }
        }

        public class CharInfo : INotifyPropertyChanged {
            public int Index { get; set; }
            public string Ascii { get; set; }
            public string Hex { get; set; }
            public string Image { get; set; }
            public System.Windows.Media.FontFamily FontFamily { get; set; }

            private double _FontSize = default;

            public event PropertyChangedEventHandler? PropertyChanged;
            public event CharInfoSelectedHandler? CharInfoSelected;

            private void OnPropertyChanged([CallerMemberName] string propertyName = default) =>
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            public double FontSize {
                get => _FontSize;
                set {
                    _FontSize = value;
                    EnclosureSize = FontSize + 10;
                    OnPropertyChanged();
                }
            }

            #region Selected Command
            private DelegateCommand _SelectedCommand = default;
            public DelegateCommand SelectedCommand => _SelectedCommand ??= new DelegateCommand(Selected, ValidateSelectedState);
            private bool ValidateSelectedState(object state) => true;
            private void Selected(object state) {
                CharInfoSelected.Invoke(null, new CharInfoSelectedEventArgs(this));
            }
            #endregion

            private double _EnclosureSize = default;
            public double EnclosureSize {
                get => _EnclosureSize;
                set {
                    _EnclosureSize = value;
                    OnPropertyChanged();
                }
            }
        }

        public static List<CharInfo> GetCharacters(this FontFamily fontFamily, double size, out bool isSymbolFont) {
            isSymbolFont = false;
            var chars = new List<CharInfo>();
            size = size < 10 ? 10 : size;
            if (!fontFamily.IsNull()) {
                GlyphTypeface? gtf = default;
                foreach (var tf in fontFamily.GetTypefaces()) {
                    tf.TryGetGlyphTypeface(out gtf);
                    if (!gtf.IsNull()) {
                        isSymbolFont = gtf.Symbol;
                        var charMap = gtf.CharacterToGlyphMap;
                        var exceptKeys = new int[] { 0, 10, 13, 32 };
                        foreach (var kvp in charMap) {
                            if (exceptKeys.Contains(kvp.Key)) {
                                continue;
                            }
                            var ascii = kvp.Key.ToString("00000");
                            var hex = kvp.Key.ToString("X");
                            var ci = new CharInfo {
                                FontFamily = fontFamily,
                                FontSize = size,
                                EnclosureSize = size * 1.5,
                                Ascii = ascii,
                                Index = kvp.Key,
                                Image = char.ConvertFromUtf32(kvp.Key),
                                Hex = hex,
                            };
                            chars.Add(ci);
                        }
                        break;
                    }
                }
            }
            return chars;
        }
    }
}
