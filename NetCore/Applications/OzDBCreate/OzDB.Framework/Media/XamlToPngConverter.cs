namespace OzDB.Application.Media {
	using System;
	using System.IO;
	using System.Threading.Tasks;
	using System.Windows;
	using System.Windows.Media;
	using System.Windows.Media.Imaging;
	using OzDB.Application.Primitives;

	public delegate void ConversionCompleteHandler(object sender, ConversionCompleteEventArgs e);

	public class ConversionCompleteEventArgs : EventArgs {
		public ConversionCompleteEventArgs(string fileName) => this.FileName = fileName;

		public ConversionCompleteEventArgs(BitmapSource source) => this.Source = source;

		public ConversionCompleteEventArgs(System.Exception ex) => this.Exception = ex;

		public System.Exception Exception { get; }

		public string FileName { get; }

		public BitmapSource Source { get; }
	}

	public class XamlToPngConverter {
		private static ConverterWorker worker;

		public event ConversionCompleteHandler ConversionComplete;

		public async Task Convert(FrameworkElement element, double width, double height, string pngOutput) {
			width = System.Math.Round(width);
			height = System.Math.Round(height);
			await this.InnerConvert(width, height, element, pngOutput);
		}

		public async Task Convert(FrameworkElement element) {
			var width = System.Math.Round(element.ActualWidth);
			var height = System.Math.Round(element.ActualHeight);
			await this.InnerConvert(width, height, element);
		}

		private async Task InnerConvert(double width, double height, FrameworkElement element, string pngOutput = null) {
			worker = new ConverterWorker {
				Width = width,
				Height = height,
				XamlElement = element,
				PngOutputFileName = pngOutput
			};
			worker.ConversionComplete += this.Worker_ConversionComplete;
			await Task.Factory.StartNew(worker.Start);
		}

		private void Worker_ConversionComplete(object sender, ConversionCompleteEventArgs e) => ConversionComplete?.Invoke(this, e);
	}

	internal class ConverterWorker {
		public double Height { get; set; }

		public string PngOutputFileName { get; set; }

		public double Width { get; set; }

		public FrameworkElement XamlElement { get; set; }

		public event ConversionCompleteHandler ConversionComplete;

		public void Start() {
			if (this.XamlElement.Dispatcher.CheckAccess()) {
				var renderingSize = new Size(this.Width, this.Height);
				try {
					this.XamlElement.Measure(renderingSize);
					var renderingRectangle = new Rect(renderingSize);
					this.XamlElement.Arrange(renderingRectangle);
					var xamlBitmap = this.RenderToBitmap(this.XamlElement);
					var enc = new PngBitmapEncoder();
					enc.Frames.Add(BitmapFrame.Create(xamlBitmap));
					if (!string.IsNullOrEmpty(this.PngOutputFileName)) {
						using (var fs = new FileStream(this.PngOutputFileName, FileMode.Create, FileAccess.Write, FileShare.None)) {
							enc.Save(fs);
						}
						ConversionComplete?.Invoke(this, new ConversionCompleteEventArgs(this.PngOutputFileName));
					} else {
						ConversionComplete?.Invoke(this, new ConversionCompleteEventArgs(xamlBitmap));
					}
				}
				catch (System.Exception ex) {
					ConversionComplete?.Invoke(this, new ConversionCompleteEventArgs(ex));
				}
			} else {
				this.XamlElement.Dispatcher.BeginInvoke(new Action(this.Start), null);
			}
		}

		private BitmapSource RenderToBitmap(FrameworkElement target) {
			var bounds = VisualTreeHelper.GetDescendantBounds(target);
			if (bounds.Left < 1) {
				bounds = new Rect(1, bounds.Top, bounds.Width + System.Math.Abs(bounds.Left) + 1, bounds.Height);
			}
			if (bounds.Top < 1) {
				bounds = new Rect(bounds.Left, 1, bounds.Width, bounds.Height + System.Math.Abs(bounds.Top) + 1);
			}
			var renderBitmap = new RenderTargetBitmap(bounds.Width.CastTo<int>(), bounds.Height.CastTo<int>(), 96, 96, PixelFormats.Pbgra32);
			var visual = new DrawingVisual();
			using (var context = visual.RenderOpen()) {
				var brush = new VisualBrush(target);
				context.DrawRectangle(brush, null, new Rect(new Point(), bounds.Size));
			}
			renderBitmap.Render(visual);
			return renderBitmap;
		}
	}
}