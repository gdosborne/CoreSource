using System;
using System.IO;
#if !DOTNET3_5
using System.Threading.Tasks;
#endif
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GregOsborne.Application.Media {
    public delegate void ConversionCompleteHandler(object sender, ConversionCompleteEventArgs e);

    public class ConversionCompleteEventArgs : EventArgs {
        public ConversionCompleteEventArgs(string fileName) {
			this.FileName = fileName;
        }

        public ConversionCompleteEventArgs(BitmapSource source) {
			this.Source = source;
        }

        public ConversionCompleteEventArgs(System.Exception ex) {
			this.Exception = ex;
        }

        public System.Exception Exception { get; }

        public string FileName { get; }

        public BitmapSource Source { get; }
    }

    public class XamlToPngConverter {
        private static ConverterWorker _worker;

        public event ConversionCompleteHandler ConversionComplete;

#if !DOTNET3_5
		public async Task Convert(FrameworkElement element, double width, double height, string pngOutput) {
            width = Math.Round(width);
            height = Math.Round(height);
            await this.InnerConvert(width, height, element, pngOutput);
        }
#endif

#if !DOTNET3_5
		public async Task Convert(FrameworkElement element) {
            var width = Math.Round(element.ActualWidth);
            var height = Math.Round(element.ActualHeight);
            await this.InnerConvert(width, height, element);
        }
#endif

#if !DOTNET3_5
        private async Task InnerConvert(double width, double height, FrameworkElement element, string pngOutput = null) {
            _worker = new ConverterWorker {
                Width = width,
                Height = height,
                XamlElement = element,
                PngOutputFileName = pngOutput
            };
            _worker.ConversionComplete += this.Worker_ConversionComplete;
            await Task.Factory.StartNew(_worker.Start);
        }
#endif

        private void Worker_ConversionComplete(object sender, ConversionCompleteEventArgs e) {
            ConversionComplete?.Invoke(this, e);
        }
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
                    }
                    else {
                        ConversionComplete?.Invoke(this, new ConversionCompleteEventArgs(xamlBitmap));
                    }
                }
                catch (System.Exception ex) {
                    ConversionComplete?.Invoke(this, new ConversionCompleteEventArgs(ex));
                }
            }
            else {
				this.XamlElement.Dispatcher.BeginInvoke(new Action(this.Start), null);
            }
        }

        private BitmapSource RenderToBitmap(FrameworkElement target) {
            var bounds = VisualTreeHelper.GetDescendantBounds(target);
            var renderBitmap = new RenderTargetBitmap((int) target.ActualWidth, (int) target.ActualHeight, 96, 96, PixelFormats.Pbgra32);
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