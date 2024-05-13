namespace OzDB.Application.Media {
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Drawing.Imaging;
	using System.IO;
	using System.Linq;
	using System.Reflection;
	using System.Runtime.InteropServices;
	using System.Windows;
	using System.Windows.Media;
	using System.Windows.Media.Imaging;
	using OzDB.Application.Primitives;
	using Color = System.Windows.Media.Color;
	using Size = System.Windows.Size;

	public static class Extensions {
		public static Icon To16BitIcon(this ImageSource src) {
			var img = src.ImageSourceToGDIImage();
			var iconHandle = img.As<Bitmap>().GetHicon();
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

		public static IList<string> GetAllFontNames() {
			var results = new List<string>();
			var regKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts");
			results.AddRange(regKey.GetValueNames().Select(x => x.Replace(" (TrueType)", string.Empty)));
			return results;
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

		//public static string GetMusicTitle(string mp3FileName)
		//{
		//    var result = string.Empty;
		//    using (var s = new Mp3Stream(new FileStream(mp3FileName, FileMode.Open, FileAccess.Read, FileShare.None)))
		//    {
		//        var tags = s.GetAllTags();
		//        // ReSharper disable once AssignNullToNotNullAttribute
		//        var enumerable = tags ?? tags.ToArray();
		//        if (!enumerable.Any()) return result;
		//        while (string.IsNullOrEmpty(result)) result = enumerable.First().Title;
		//    }
		//    return result;
		//}

		public static void GetThumbnailImage(this string fileName, int width, int height, string outputFileName, ImageFormat format, bool overwrite = false) {
			if (!File.Exists(fileName)) {
				throw new FileNotFoundException("Cannot find image file", fileName);
			}

			if (File.Exists(outputFileName) && !overwrite) {
				throw new ApplicationException("Output file already exists");
			}

			if (outputFileName.Equals(fileName, StringComparison.OrdinalIgnoreCase)) {
				return;
			}

			Bitmap bmp;
			if (Path.GetExtension(outputFileName).Equals(".ico", StringComparison.OrdinalIgnoreCase)) {
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

		public static Color ReduceAlpha(this Color original, byte alpha) => Color.FromArgb(alpha, original.R, original.G, original.B);

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

		//public static Color ToColor(this string value)
		//{
		//    var val = Convert.ToInt32(value, 16);
		//    byte[] bytes = BitConverter.GetBytes(val);
		//    return new SolidColorBrush(Color.FromArgb(bytes[3], bytes[2], bytes[1], bytes[0])).Color;
		//}

		private static bool ThumbnailCallback() => false;

		public struct CharInfo {
			public int Index { get; set; }
			public string Ascii { get; set; }
			public string Hex { get; set; }
			public string Image { get; set; }
			public System.Windows.Media.FontFamily FontFamily { get; set; }
		}

		public static List<CharInfo> GetCharacters(this System.Windows.Media.FontFamily fontFamily) {
			var chars = new List<CharInfo>();
			if (fontFamily != null) {
				GlyphTypeface gtf = null;
				foreach (var tf in fontFamily.GetTypefaces()) {
					tf.TryGetGlyphTypeface(out gtf);
					if (gtf != null) {
						var charMap = gtf.CharacterToGlyphMap;
						foreach (var kvp in charMap) {
							if (kvp.Key == 13 || kvp.Key == 32) {
								continue;
							}
							var ascii = kvp.Key.ToString("00000");
							var hex = kvp.Key.ToString("X");
							var ci = new CharInfo {
								FontFamily = fontFamily,
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