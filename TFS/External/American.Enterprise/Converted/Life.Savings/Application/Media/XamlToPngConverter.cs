using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GregOsborne.Application.Media {
    public delegate void ConversionCompleteHandler(object sender, ConversionCompleteEventArgs e);

    public class ConversionCompleteEventArgs : EventArgs {
        public ConversionCompleteEventArgs(string fileName) {
            FileName = fileName;
        }

        public ConversionCompleteEventArgs(BitmapSource source) {
            Source = source;
        }

        public ConversionCompleteEventArgs(System.Exception ex) {
            Exception = ex;
        }

        public System.Exception Exception { get; }

        public string FileName { get; }

        public BitmapSource Source { get; }
    }

    public class XamlToPngConverter {
        private static ConverterWorker _worker;

        public event ConversionCompleteHandler ConversionComplete;

        public async Task Convert(FrameworkElement element, double width, double height, string pngOutput) {
            width = Math.Round(width);
            height = Math.Round(height);
            await InnerConvert(width, height, element, pngOutput);
        }

        public async Task Convert(FrameworkElement element) {
            var width = Math.Round(element.ActualWidth);
            var height = Math.Round(element.ActualHeight);
            await InnerConvert(width, height, element);
        }

        private async Task InnerConvert(double width, double height, FrameworkElement element, string pngOutput = null) {
            _worker = new ConverterWorker {
                Width = width,
                Height = height,
                XamlElement = element,
                PngOutputFileName = pngOutput
            };
            _worker.ConversionComplete += Worker_ConversionComplete;
            await Task.Factory.StartNew(_worker.Start);
        }

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
            if (XamlElement.Dispatcher.CheckAccess()) {
                var renderingSize = new Size(Width, Height);
                try {
                    XamlElement.Measure(renderingSize);
                    var renderingRectangle = new Rect(renderingSize);
                    XamlElement.Arrange(renderingRectangle);
                    var xamlBitmap = RenderToBitmap(XamlElement);
                    var enc = new PngBitmapEncoder();
                    enc.Frames.Add(BitmapFrame.Create(xamlBitmap));
                    if (!string.IsNullOrEmpty(PngOutputFileName)) {
                        using (var fs = new FileStream(PngOutputFileName, FileMode.Create, FileAccess.Write, FileShare.None)) {
                            enc.Save(fs);
                        }
                        ConversionComplete?.Invoke(this, new ConversionCompleteEventArgs(PngOutputFileName));
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
                XamlElement.Dispatcher.BeginInvoke(new Action(Start), null);
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